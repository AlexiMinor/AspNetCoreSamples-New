using FirstMvcApp.Core.DTOs;
using FirstMvcApp.Data;

namespace FirstMvcApp.Core.Interfaces;

public interface IAccountService
{
    //todo add model
    Task<bool> CheckUserWithThatEmailIsExistAsync(string email);
    Task<Guid> CreateUserAsync(string modelEmail);
    Task<int> SetRoleAsync(Guid userId, string roleName);
    Task<int> SetPasswordAsync(Guid userId, string password);
    Task<Guid?> GetUserIdByEmailAsync(string email);
    Task<bool> CheckPassword(string email, string password);
}