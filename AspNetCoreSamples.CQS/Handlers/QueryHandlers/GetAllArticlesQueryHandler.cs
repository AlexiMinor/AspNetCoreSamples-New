using AspNetCoreSamples.CQS.Models.Queries;
using AutoMapper;
using FirstMvcApp.Core.DTOs;
using FirstMvcApp.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreSamples.CQS.Handlers.QueryHandlers;

public class GetAllArticlesQueryHandler : IRequestHandler<GetAllArticlesQuery, IEnumerable<ArticleDto>>
{
    private readonly NewsAggregatorContext _database;
    private readonly IMapper _mapper;

    public GetAllArticlesQueryHandler(NewsAggregatorContext database, IMapper mapper)
    {
        _database = database;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ArticleDto>> Handle(GetAllArticlesQuery query, CancellationToken token)
    {
            var articles = await _database.Articles
            .OrderBy(article => article.CreationDate)
            .Select(article => _mapper.Map<ArticleDto>(article))
            .ToArrayAsync(cancellationToken: token);

            return articles;
    }
}
