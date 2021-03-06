using System.Linq.Expressions;
using FirstMvcApp.Data;
using FirstMvcApp.Data.Entities;

namespace FirstMvcApp.Core.Interfaces.Data;

//USE ONLY FOR REPOSITORY WITH CUSTOM LOGIC
public interface IArticleRepository : IRepository<Article>
{
    public IEnumerable<Article> Get5TopRatedNewsOrderedByCreationDate();
}