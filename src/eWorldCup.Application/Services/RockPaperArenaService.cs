using eWorldCup.Core.Interfaces.Repositories;
using eWorldCup.Core.Models;
using eWorldCup.Core.Models.Games.RockPaperArena;

namespace eWorldCup.Application.Services;

public class RockPaperArenaService(IPlayerRepository playerRepository, 
    ITournamentRepository tournamentRepository) : IRockPaperArenaService
{
    public RockPaperArenaTournament Start(string playerName, int numberOfPlayers)
    {
        var user = playerRepository.Add(new Player(0, playerName));
        var players = GetTournamentOpponents(numberOfPlayers, user);
        var tournament = RockPaperArenaTournament.CreateWithUserAndOpponents(user, players, settings =>
        {
            settings.MaximumRoundsInAMatch = 3;
            settings.PointsForWinningMatch = 3;
            settings.PointsForDrawingMatch = 1;
            settings.PointsForLosingMatch = 0;
        });
        tournament.CurrentMatch = tournament.GetUserMatch();
        tournamentRepository.Add(tournament);
        
        return tournament;
    }

    internal List<Player> GetTournamentOpponents(int numberOfPlayers, Player user)
    {
        var opponents = playerRepository.GetAll()
            .Where(p => p.Id != user.Id)
            .Take(numberOfPlayers - 1) // Since the user is one player
            .ToList();

        return opponents;
    }
}

public interface IRockPaperArenaService
{
    RockPaperArenaTournament Start(string playerName, int numberOfPlayers);
}