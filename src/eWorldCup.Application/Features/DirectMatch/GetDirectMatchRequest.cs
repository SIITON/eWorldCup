using eWorldCup.Application.Services;
using eWorldCup.Core.Interfaces.Repositories;
using eWorldCup.Core.Models.API;
using MediatR;

namespace eWorldCup.Application.Features.DirectMatch;

public class GetDirectMatchRequest : IRequest<MatchApiModel>
{
    public long TotalPlayers { get; set; }
    public long PlayerIndex { get; set; }
    public long RoundNumber { get; set; }
}

public class GetDirectMatchHandler(ITournamentScheduler tournament, IPlayerRepository players) : IRequestHandler<GetDirectMatchRequest, MatchApiModel>
{
    public async Task<MatchApiModel> Handle(GetDirectMatchRequest request, CancellationToken cancellationToken)
    {
        var t = tournament.Create(request.TotalPlayers);
        t.CurrentRound = request.RoundNumber;
        var match = t.Schedule.GetMatch(request.RoundNumber, request.PlayerIndex);

        var participants = match.Select(id => players.Get((int)id));
        return await Task.FromResult(new MatchApiModel
        {
            Players = participants.Select(p => new PlayerApiModel
            {
                Id = p.Id,
                Name = p.Name
            }).ToList()
        })
;    }
}