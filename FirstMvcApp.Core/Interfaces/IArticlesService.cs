using FirstMvcApp.Core.DTOs;

namespace FirstMvcApp.Core.Interfaces;

public interface IArticlesService
{
    //todo add model
    Task<IEnumerable<ArticleDto>> GetAllNewsAsync();
    Task<IEnumerable<ArticleDto>> GetNewsByPageAsync(int page);
    Task<int> InsertNews(IEnumerable<ArticleDto> articles);
    Task<ArticleDto> GetArticleWithAllNavigationProperties(Guid id);
    Task<ArticleDto> GetArticleAsync(Guid id);
    Task<IEnumerable<ArticleDto>> GetArticlesByNameAsync(string name);
    Task<int?> DeleteAsync(Guid modelId);
    Task<List<string>> GetAllExistingArticleUrls();
}