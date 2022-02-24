namespace FirstMvcApp.Core.Interfaces;

public interface IRoleService
{
    Task<Guid> GetRoleIdByNameAsync(string name);
    Task<Guid> CreateRole(string name);
}