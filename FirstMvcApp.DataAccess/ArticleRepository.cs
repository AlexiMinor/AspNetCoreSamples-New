using System.Linq.Expressions;
using FirstMvcApp.Core.Interfaces.Data;
using FirstMvcApp.Data;
using Microsoft.EntityFrameworkCore;

namespace FirstMvcApp.DataAccess;

public class ArticleRepository : IArticleRepository
{
    protected readonly NewsAggregatorContext Db;
    protected readonly DbSet<Article> DbSet;

    public ArticleRepository(NewsAggregatorContext context)
    {
        Db = context;
        DbSet = Db.Set<Article>();
    }

    public virtual async Task Add(Article obj)
    {
        await DbSet.AddAsync(obj);
    }

    public async Task AddRange(IEnumerable<Article> obj)
    {
        await DbSet.AddRangeAsync(obj);
    }

    public virtual async Task<Article> GetById(Guid id)
    {
        return await DbSet.AsNoTracking()
            .FirstOrDefaultAsync(entity => entity.Id.Equals(id));
    }

    public async Task<Article> GetByIdWithIncludes(Guid id, params Expression<Func<Article, object>>[] includes)
    {
        if (includes.Any())
        {
            return await includes.Aggregate(DbSet
                .Where(entity => entity.Id.Equals(id)), (current, include) => current.Include(include)).FirstOrDefaultAsync();
        }

        return await GetById(id);
    }

    public virtual IQueryable<Article> Get()
    {
        return DbSet;
    }

    public async Task<IQueryable<Article>> FindBy(Expression<Func<Article, bool>> predicate,
        params Expression<Func<Article, object>>[] includes)
    {
        var result = DbSet.Where(predicate);
        if (includes.Any())
        {
            result = includes.Aggregate(result, (current, include) => current.Include(include));
        }

        return result;
    }

    public virtual async Task Update(Article obj)
    {
        DbSet.Update(obj);
    }

    public virtual async Task PatchAsync(Guid id, List<PatchModel> patchDtos)
    {
        var model = await DbSet.FirstOrDefaultAsync(entity => entity.Id.Equals(id));

        var nameValuePairProperties = patchDtos
            .ToDictionary(a => a.PropertyName, a => a.PropertyValue);

        var dbEntityEntry = Db.Entry(model);
        dbEntityEntry.CurrentValues.SetValues(nameValuePairProperties);
        dbEntityEntry.State = EntityState.Modified;
    }


    public virtual async Task Remove(Guid id)
    {
        DbSet.Remove(await DbSet.FindAsync(id));
    }

    public void Dispose()
    {
        Db.Dispose();
        GC.SuppressFinalize(this);
    }
}
