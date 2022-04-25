using FirstMvcApp.Core.DTOs;
using FirstMvcApp.Data;
using FirstMvcApp.Data.Entities;

namespace FirstMvcApp.Core.Interfaces;

public interface IAccountService
{
    //todo add model
    Task<bool> CheckUserWithThatEmailIsExistAsync(string email);
    Task<Guid> CreateUserAsync(string modelEmail);
    Task<int> SetRoleAsync(Guid userId, string roleName);
    Task<IEnumerable<string>> GetRolesAsync(Guid userId);
    Task<int> SetPasswordAsync(Guid userId, string password);
    Task<Guid?> GetUserIdByEmailAsync(string email);
    Task<UserDto> GetUserById(Guid id);
    Task<UserDto> GetUserByEmailAsync(string email);
    Task<UserDto> GetUserByRefreshTokenAsync(string refreshToken);
    Task<bool> CheckPassword(string email, string password);
}

