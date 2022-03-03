namespace FirstMvcApp.Core.Interfaces;

public interface IRoleService
{
    Task<Guid> GetRoleIdByNameAsync(string name);
    Task<string> GetRoleNameByIdAsync(Guid id);
    Task<Guid> CreateRole(string name);
}