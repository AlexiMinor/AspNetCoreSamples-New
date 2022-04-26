using AspNetCoreSamples.CQS.Handlers.QueryHandlers;
using FirstMvcApp.Core.DTOs;
using MediatR;

namespace AspNetCoreSamples.CQS.Models.Queries
{
    public class GetAllArticlesQuery : IRequest<IEnumerable<ArticleDto>>
    {
    }
}