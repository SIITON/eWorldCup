using eWorldCup.Application.Services;
using eWorldCup.Core.Models;
using eWorldCup.Core.Models.API;
using MediatR;

namespace eWorldCup.Application.Features.MatchesInASpecificRound;

public class GetMatchesInASpecificRoundRequest : IRequest<TournamentMatchApiModel>
{
    public IEnumerable<Player> Players { get; set; } = new List<Player>();
    public long RoundNumber { get; set; }
}

public class GetMatchesInASpecificRoundHandler(ITournamentScheduler tournament) : IRequestHandler<GetMatchesInASpecificRoundRequest, TournamentMatchApiModel>
{
    public async Task<TournamentMatchApiModel> Handle(GetMatchesInASpecificRoundRequest request, CancellationToken cancellationToken)
    {
        var schedule = tournament.Schedule(request.Players, request.RoundNumber);

        return await Task.FromResult(new TournamentMatchApiModel
        {
            RoundNumber = request.RoundNumber,
            Players = schedule.Select(match => new MatchApiModel
            {
                Players = match.Players
            })
        });
    }
}