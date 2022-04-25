using AspNetCoreSamples.CQS;
using AspNetCoreSamples.CQS.Models.Queries;
using FirstMvcApp.Core.DTOs;
using FirstMvcApp.Core.Interfaces;
using MediatR;

namespace FirstMvcApp.Domain.Services;

public class ArticleCqsService : IArticlesService
{
    private readonly IMediator _mediator;

    public ArticleCqsService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IEnumerable<ArticleDto>> GetAllNewsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ArticleDto>> GetNewsByPageAsync(int page)
    {
        if (page >= 0)
        {
            var query = new GetArticlesByPageQuery()
            {
                PageSize = 15,
                PageNumber = page
            };
            var articles = await _mediator.Send(query);

            return articles;
        }

        throw new ArgumentException("Page number under 0", nameof(page));
    }

    public async Task<int> InsertNews(IEnumerable<ArticleDto> articles)
    {
        throw new NotImplementedException();
    }

    public async Task<ArticleDto> GetArticleWithAllNavigationProperties(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<ArticleDto> GetArticleAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ArticleDto>> GetArticlesByNameAsync(string name)
    {
        throw new NotImplementedException();
    }

    public async Task<int?> DeleteAsync(Guid modelId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<string>> GetAllExistingArticleUrls()
    {
        throw new NotImplementedException();
    }
}