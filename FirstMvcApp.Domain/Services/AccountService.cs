using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using FirstMvcApp.Core.DTOs;
using FirstMvcApp.Core.Interfaces;
using FirstMvcApp.Core.Interfaces.Data;
using FirstMvcApp.Data;
using FirstMvcApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FirstMvcApp.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoleService _roleService;

        public AccountService(IMapper mapper, 
            IUnitOfWork unitOfWork, 
            IRoleService roleService, 
            IConfiguration configuration)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _roleService = roleService;
            _configuration = configuration;
        }

        public async Task<bool> CheckUserWithThatEmailIsExistAsync(string email)
        {
            var normalizedEmail = email.ToUpperInvariant();

            return await _unitOfWork.Users.Get()
                .AnyAsync(user =>
                    user.NormalizedEmail
                        .Equals(normalizedEmail));
        }

        public async Task<Guid?> GetUserIdByEmailAsync(string email)
        {
            var normalizedEmail = email.ToUpperInvariant();

            return (await (await _unitOfWork.Users.FindBy(user =>
                user.NormalizedEmail != null && user.NormalizedEmail
                    .Equals(normalizedEmail))).FirstOrDefaultAsync())?.Id;
        }

        public async Task<UserDto> GetUserById(Guid id)
        {
            return _mapper.Map<UserDto>(await _unitOfWork.Users.GetById(id));

        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var normalizedEmail = email.ToUpperInvariant();

            var user= await _unitOfWork.Users
                .Get()
                .Where(user =>
                    user.NormalizedEmail != null && user.NormalizedEmail
                    .Equals(normalizedEmail))
                .Include(user => user.UserRoles)
                .ThenInclude(role => role.Role)
                .FirstOrDefaultAsync();

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserByRefreshTokenAsync(string refreshToken)
        {
            var user = (await (await _unitOfWork.RefreshTokens
                .FindBy(token => token.Token.Equals(refreshToken)))
                .FirstOrDefaultAsync())
                .User;

            return _mapper.Map<UserDto>(user);
        }

        public async Task<Guid> CreateUserAsync(string modelEmail)
        {
            var id = Guid.NewGuid();
            await _unitOfWork.Users.Add(new User()
            {
                Id = id,
                Email = modelEmail,
                NormalizedEmail = modelEmail.ToUpperInvariant(),
                RegistrationDate = DateTime.Now
            });
            await _unitOfWork.Commit();
            return id;
        }

        public async Task<int> SetRoleAsync(Guid userId, string roleName)
        {
            var roleId = await _roleService.GetRoleIdByNameAsync(roleName);
            if (roleId == Guid.Empty)
            {
                roleId = await _roleService.CreateRole(roleName);
            }

            await _unitOfWork.UserRoles.Add(new UserRole
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    RoleId = roleId
                });
            return await _unitOfWork.Commit();
        }

        public async Task<IEnumerable<string>> GetRolesAsync(Guid userId)
        {
            var userRoleIds = (await _unitOfWork
                .Users.GetByIdWithIncludes(userId, 
                        user => user.UserRoles))
                .UserRoles.Select(role => role.RoleId);

            var names = new List<string>();
            foreach (var userRoleId in userRoleIds)
            {
                names.Add(await _roleService.GetRoleNameByIdAsync(userRoleId));
            }

            return names;
        }

        public async Task<int> SetPasswordAsync(Guid userId, string password)
        {

            await _unitOfWork.Users.PatchAsync(userId, new List<PatchModel>
            {
                    new PatchModel()
                    {
                        PropertyName = "PasswordHash",
                        PropertyValue = GetPasswordHash(password,_configuration["ApplicationVariables:Salt"])
                    }
                });
            return await _unitOfWork.Commit();

        }

        public async Task<bool> CheckPassword(string email, string password)
        {
            var userId = await GetUserIdByEmailAsync(email);
            if (userId.GetValueOrDefault() != Guid.Empty)
            {
                var userPasswordHash = (await _unitOfWork.Users.GetById(userId.GetValueOrDefault())).PasswordHash;
                if (!string.IsNullOrEmpty(userPasswordHash))
                {
                    var enteredPasswordHash = GetPasswordHash(password, _configuration["ApplicationVariables:Salt"]);

                    if (userPasswordHash.Equals(enteredPasswordHash))
                        return true;
                }
            }
            return false;
        }

        private string GetPasswordHash(string password, string salt)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            var sha1Data = sha1.ComputeHash(Encoding.UTF8.GetBytes($"{salt}_{password}"));
            var hashedPassword = Encoding.UTF8.GetString(sha1Data);
            return hashedPassword;
        }
    }
}