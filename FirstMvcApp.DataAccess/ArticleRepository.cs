using FirstMvcApp.Core.Interfaces.Data;
using FirstMvcApp.Data;
using Microsoft.EntityFrameworkCore;

namespace FirstMvcApp.DataAccess;

public class ArticleRepository : Repository<Article>, IArticleRepository
{
    public ArticleRepository(NewsAggregatorContext context) : base(context)
    {
    }

    public override async Task<Article> GetById(Guid id)
    {
        return await DbSet
            .Where(article => !string.IsNullOrEmpty(article.Body))
            .FirstOrDefaultAsync(article => article.Id.Equals(id));
    }

    public IEnumerable<Article> Get5TopRatedNewsOrderedByCreationDate()
    {
        return DbSet.OrderByDescending(article => article.PositivityRate).Take(5)
            .OrderBy(article => article.CreationDate).ToList();
    }
}
