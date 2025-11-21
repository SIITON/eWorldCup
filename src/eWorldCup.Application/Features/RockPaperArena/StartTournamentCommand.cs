using eWorldCup.Core.Interfaces.Repositories;
using eWorldCup.Core.Models;
using eWorldCup.Core.Models.API.Responses;
using eWorldCup.Core.Models.Tournaments;
using MediatR;

namespace eWorldCup.Application.Features.RockPaperArena;

public class StartTournamentCommand : IRequest<TournamentStartedResponse>
{
    public string PlayerName { get; init; } = string.Empty;
    public int NumberOfPlayers { get; init; }
}

public class StartTournamentHandler(IPlayerRepository playerRepository) : IRequestHandler<StartTournamentCommand, TournamentStartedResponse>
{
    public async Task<TournamentStartedResponse> Handle(StartTournamentCommand request, CancellationToken cancellationToken)
    {
        // Setup tournament with players
        var players = playerRepository.GetAll()
            .Take(request.NumberOfPlayers)
            .ToList();
        var tournament = new TwoPlayerRoundRobin(players.Count);
        var matches = tournament.GetMatchesInRound(1);
        // Return tournament id and status
        return new TournamentStartedResponse
        {
            Player = null,
            NextMatch = null
        };
    }
}