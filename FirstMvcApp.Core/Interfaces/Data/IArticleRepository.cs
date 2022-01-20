using System.Linq.Expressions;
using FirstMvcApp.Data;

namespace FirstMvcApp.Core.Interfaces.Data;

public interface IArticleRepository
{
    public Task Add(Article obj);
    public Task AddRange(IEnumerable<Article> obj);

    public  Task<Article> GetById(Guid id);
    public  Task<Article> GetByIdWithIncludes(Guid id,
        params Expression<Func<Article, object>>[] includes);

    public  IQueryable<Article> Get();

    public Task<IQueryable<Article>> FindBy(Expression<Func<Article, bool>> predicate,
        params Expression<Func<Article, object>>[] includes);

    public  Task Update(Article obj);

    public  Task PatchAsync(Guid id, List<PatchModel> patchDtos);

    public  Task Remove(Guid id);

    public void Dispose();
}