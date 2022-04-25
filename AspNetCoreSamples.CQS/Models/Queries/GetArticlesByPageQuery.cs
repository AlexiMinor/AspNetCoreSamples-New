using AspNetCoreSamples.CQS.Handlers.QueryHandlers;
using FirstMvcApp.Core.DTOs;
using MediatR;

namespace AspNetCoreSamples.CQS.Models.Queries
{
    public class GetArticlesByPageQuery : IRequest<IEnumerable<ArticleDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}