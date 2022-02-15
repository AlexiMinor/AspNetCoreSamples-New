using FirstMvcApp.Core.DTOs;

namespace FirstMvcApp.Core.Interfaces;

public interface IRssService
{
    public IEnumerable<RssArticleDto?> GetArticlesInfoFromRss(string rssUrl);
}