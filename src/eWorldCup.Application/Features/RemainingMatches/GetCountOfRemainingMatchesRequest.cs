using MediatR;

namespace eWorldCup.Application.Features.RemainingMatches;

public class GetCountOfRemainingMatchesRequest : IRequest<long>
{
    public long TotalPlayers { get; init; }
    public long CompletedRounds { get; init; }
}