using MediatR;

namespace eWorldCup.Application.Features.RockPaperArena;

public class StartTournamentCommand : IRequest<bool>
{
    public string PlayerName { get; init; } = string.Empty;
    public int NumberOfPlayers { get; init; }
}

public class StartTournamentHandler : IRequestHandler<StartTournamentCommand, bool>
{
    public async Task<bool> Handle(StartTournamentCommand request, CancellationToken cancellationToken)
    {
        // Setup tournament with players
        // Return tournament id and status
        return true;
    }
}