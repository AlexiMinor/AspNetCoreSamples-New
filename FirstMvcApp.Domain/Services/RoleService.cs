using System.Security.Cryptography;
using AutoMapper;
using FirstMvcApp.Core.Interfaces;
using FirstMvcApp.Core.Interfaces.Data;
using FirstMvcApp.Data;
using FirstMvcApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirstMvcApp.Domain.Services
{
    public class RoleService : IRoleService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> GetRoleIdByNameAsync(string name)
        {
            var id = await (await _unitOfWork.Roles
                    .FindBy(role => role.Name.Equals(name)))
                .Select(role => role.Id)
                .FirstOrDefaultAsync();
            return id;
        }

        public async Task<Guid> CreateRole(string name)
        {
            var id = Guid.NewGuid();
            await _unitOfWork.Roles.Add(new Role()
            {
                Id = id,
                Name = name
            });
            await _unitOfWork.Commit();
            return id;
        }
    }
}