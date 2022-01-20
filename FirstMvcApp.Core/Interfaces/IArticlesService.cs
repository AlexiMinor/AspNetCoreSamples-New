using FirstMvcApp.Core.DTOs;

namespace FirstMvcApp.Core.Interfaces;

public interface IArticlesService
{
    //todo add model
    Task<IEnumerable<ArticleDto>> GetAllNewsAsync();
    Task<ArticleDto> GetArticleWithAllNavigationProperties(Guid id);
    Task<int?> DeleteAsync(Guid modelId);
}