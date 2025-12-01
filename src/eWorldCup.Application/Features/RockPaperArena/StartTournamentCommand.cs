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
        var tournament = RockPaperArenaTournament.CreateWithUserAndOpponents(user, players, settings =>
        {
            settings.MaximumRoundsInAMatch = 3;
            settings.PointsForWinningMatch = 3;
            settings.PointsForDrawingMatch = 1;
            settings.PointsForLosingMatch = 0;
        });
        tournament.CurrentMatch = tournament.GetUserMatch();
        
        tournamentRepository.Add(tournament);
        // Get the first match
        var userMatch = tournament.GetUserMatch();
        
        var opponent = tournament.Participants[userMatch.SecondPlayerIndex()];
        // Return tournament id and status
        return new TournamentStartedResponse
        {
            Id = tournament.TournamentId,
            Player = new PlayerApiModel
            {
                Id = user.Id,
                Name = user.Name
            },
            NextMatch = new TournamentMatchResponse
            {
                PlayedRounds = 0,
                BestOf = tournament.Settings.MaximumRoundsInAMatch,
                Opponent = new PlayerApiModel
                {
                    Id = opponent.Id,
                    Name = opponent.Name
                },
                Score = new MatchScoreApiModel
                {
                    Player = userMatch.Score.Player,
                    Opponent = userMatch.Score.Opponent
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