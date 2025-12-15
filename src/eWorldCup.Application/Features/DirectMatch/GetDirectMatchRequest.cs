using eWorldCup.Application.Services;
using eWorldCup.Core.Interfaces.Repositories;
using eWorldCup.Core.Models;
using eWorldCup.Core.Models.API;
using eWorldCup.Core.Models.Tournaments;
using MediatR;

namespace eWorldCup.Application.Features.DirectMatch;

public class GetDirectMatchRequest : IRequest<MatchApiModel>
{
    public long TotalPlayers { get; set; } = 0;
    public long PlayerIndex { get; set; }
    public long RoundNumber { get; set; }
}

public class GetDirectMatchHandler(IPlayerRepository players) : IRequestHandler<GetDirectMatchRequest, MatchApiModel>
{
    public async Task<MatchApiModel> Handle(GetDirectMatchRequest request, CancellationToken cancellationToken)
    {
        var totalPlayers = request.TotalPlayers == 0 ? players.GetAll().Count() : request.TotalPlayers;
        var tournament = new TwoPlayerTournament(new TwoPlayerRoundRobin(totalPlayers))
        {
            CurrentRound = request.RoundNumber
        };
        var match = tournament.Schedule.GetMatch(request.RoundNumber, request.PlayerIndex);

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