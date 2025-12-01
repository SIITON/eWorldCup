using MediatR;

namespace eWorldCup.Application.Features.RockPaperArena;

public class GetTournamentStatusRequest(Guid tournamentId) : IRequest<bool>
{
    public Guid TournamentId { get; set; } = tournamentId;
}