using AspNetCoreSamples.CQS.Models.Commands;
using AspNetCoreSamples.CQS.Models.Queries;
using AutoMapper;
using FirstMvcApp.Core.DTOs;
using FirstMvcApp.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreSamples.CQS.Handlers.CommandHandlers;

public class RateArticleCommandHandler : IRequestHandler<RateArticleCommand, bool>
{
    private readonly NewsAggregatorContext _database;
    private readonly IMapper _mapper;

    public RateArticleCommandHandler(NewsAggregatorContext database, IMapper mapper)
    {
        _database = database;
        _mapper = mapper;
    }

    public async Task<bool> Handle(RateArticleCommand command, CancellationToken token)
    {
        var article = await _database.Articles.FirstOrDefaultAsync(a => a.Id.Equals(command.Id), cancellationToken: token);

        article.PositivityRate = command.Rate;

        var result = await _database.SaveChangesAsync(token);
        
        return true;
    }
}
