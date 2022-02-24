using FirstMvcApp.Data;
using FirstMvcApp.Data.Entities;

namespace FirstMvcApp.Core.Interfaces.Data;

public interface IUnitOfWork : IDisposable
{
    IArticleRepository Articles { get; }
    IRepository<Role> Roles { get; }
    IRepository<User> Users { get; }
    IRepository<Source> Sources { get; }
    IRepository<Comment> Comments { get; }
    IRepository<UserRole> UserRoles { get; }

    Task<int> Commit();
}