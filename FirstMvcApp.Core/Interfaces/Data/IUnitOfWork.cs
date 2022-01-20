namespace FirstMvcApp.Core.Interfaces.Data;

public interface IUnitOfWork : IDisposable
{
    IArticleRepository Articles { get; }
    object Roles { get; }
    object Users { get; }
    object Sources { get; }
    object Comments { get; }

    Task<int> Commit();
}