using eWorldCup.Application.Services;
using eWorldCup.Core.Interfaces.Repositories;
using MediatR;

namespace eWorldCup.Application.Features.RemainingMatches;

public class GetCountOfRemainingMatchesRequest : IRequest<long>
{
    public long TotalPlayers { get; init; }
    public long CompletedRounds { get; init; }
}

public class GetCountOfRemainingMatchesHandler(ITournamentScheduler tournament, IPlayerRepository players) : IRequestHandler<GetCountOfRemainingMatchesRequest, long>
{
    public async Task<long> Handle(GetCountOfRemainingMatchesRequest request, CancellationToken cancellationToken)
    {
        var t = tournament.Create(request.TotalPlayers);
        t.CurrentRound = request.CompletedRounds + 1;
        return await Task.FromResult(t.NumberOfMatchesLeft);
    }
}
