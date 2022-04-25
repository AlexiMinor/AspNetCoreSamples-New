using MediatR;

namespace AspNetCoreSamples.CQS.Models.Commands;

public class RateArticleCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public float Rate { get; set; }
}