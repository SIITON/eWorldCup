using eWorldCup.Core.Interfaces.Repositories;
using eWorldCup.Core.Models;
using eWorldCup.Core.Models.API;
using eWorldCup.Core.Models.API.Responses;
using eWorldCup.Core.Models.Games.RockPaperArena;
using eWorldCup.Core.Models.Tournaments;
using MediatR;

namespace eWorldCup.Application.Features.RockPaperArena;

public class StartTournamentCommand : IRequest<TournamentStartedResponse>
{
    public string PlayerName { get; init; } = string.Empty;
    public int NumberOfPlayers { get; init; }
}

public class StartTournamentHandler(IPlayerRepository playerRepository, ITournamentRepository tournamentRepository) : IRequestHandler<StartTournamentCommand, TournamentStartedResponse>
{
    public async Task<TournamentStartedResponse> Handle(StartTournamentCommand request, CancellationToken cancellationToken)
    {
        // Setup tournament with players
        var user = playerRepository.Add(new Player(0, request.PlayerName));
        var players = GetTournamentOpponents(request, user);
        var tournament = new RockPaperArenaTournament(players.Count);
        tournament.AddUser(user);
        tournament.AddParticipants(players);
        tournamentRepository.Add(tournament);
        // Get the first match
        var matches = tournament.Schedule.GetMatchesInRound(1);
        var userMatch = tournament.Schedule.GetMatchesForPlayer(1).First();

        var opponentIndex = (int)userMatch.PlayerIndex.ToArray()[1];
        // Return tournament id and status
        return new TournamentStartedResponse
        {
            Player = new PlayerApiModel
            {
                Id = user.Id,
                Name = user.Name
            },
            NextMatch = new TournamentMatchResponse
            {
                CurrentRound = 1,
                BestOf = 3,
                Opponent = new PlayerApiModel
                {
                    Id = players[opponentIndex].Id,
                    Name = players[opponentIndex].Name
                },
                Score = new MatchScoreApiModel
                {
                    Player = 0,
                    Opponent = 0
                }
            }
        };
    }

    public List<Player> GetTournamentOpponents(StartTournamentCommand request, Player user)
    {
        var opponents = playerRepository.GetAll()
            .Where(p => p.Id != user.Id)
            .Take(request.NumberOfPlayers - 1) // Since the user is one player
            .ToList();
        
        return opponents;
    }

}