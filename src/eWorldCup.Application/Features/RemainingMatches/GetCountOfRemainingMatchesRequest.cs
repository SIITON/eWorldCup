using eWorldCup.Core.Models;
using eWorldCup.Core.Models.Tournaments;
using MediatR;

namespace eWorldCup.Application.Features.RemainingMatches;

public class GetCountOfRemainingMatchesRequest : IRequest<long>
{
    public long TotalPlayers { get; init; }
    public long CompletedRounds { get; init; }
}

public class GetCountOfRemainingMatchesHandler : IRequestHandler<GetCountOfRemainingMatchesRequest, long>
{
    public async Task<long> Handle(GetCountOfRemainingMatchesRequest request, CancellationToken cancellationToken)
    {
        var tournament = new TwoPlayerTournament(new TwoPlayerRoundRobin(request.TotalPlayers))
        {
            CurrentRound = request.CompletedRounds + 1
        };
        
        return await Task.FromResult(tournament.NumberOfMatchesLeft);
    }
}
