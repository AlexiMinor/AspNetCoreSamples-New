using FirstMvcApp.Core.DTOs;

namespace FirstMvcApp.Core.Interfaces;

public interface IRssService
{
    IEnumerable<RssArticleDto?> GetArticlesInfoFromRss(string rssUrl);

    Task<int> AggregateArticleDataFromRssSources();
}