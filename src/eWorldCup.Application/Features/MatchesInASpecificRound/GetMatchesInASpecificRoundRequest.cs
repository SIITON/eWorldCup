using eWorldCup.Core.Models;
using eWorldCup.Core.Models.API;
using eWorldCup.Core.Models.Tournaments;
using MediatR;

namespace eWorldCup.Application.Features.MatchesInASpecificRound;

public class GetMatchesInASpecificRoundRequest : IRequest<TournamentMatchApiModel>
{
    public IEnumerable<Player> Players { get; set; } = new List<Player>();
    public long RoundNumber { get; set; }
}

public class GetMatchesInASpecificRoundHandler : IRequestHandler<GetMatchesInASpecificRoundRequest, TournamentMatchApiModel>
{
    public async Task<TournamentMatchApiModel> Handle(GetMatchesInASpecificRoundRequest request, CancellationToken cancellationToken)
    {
        var tournament = new TwoPlayerRoundRobin(request.Players.Count());
        var schedule = tournament.GetMatchesInRound(request.RoundNumber);
        var players = request.Players.ToList();


        return await Task.FromResult(new TournamentMatchApiModel
        {
            RoundNumber = request.RoundNumber,
            Matches = schedule.Select(match => new MatchApiModel
            {
                Players = match.PlayerIds.Select(index => new PlayerApiModel
                {
                    Id = players[(int)index].Id,
                    Name = players[(int)index].Name
                }).ToList()
            })
        });
    }
}