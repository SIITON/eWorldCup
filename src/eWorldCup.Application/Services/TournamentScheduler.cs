using eWorldCup.Core.Models;
using eWorldCup.Core.Models.Tournaments;
namespace eWorldCup.Application.Services;

public class TournamentScheduler : ITournamentScheduler
{
    public IEnumerable<Match> Schedule(IEnumerable<Player> players, long roundNumber)
    {
        var tournament = new TwoPlayerRoundRobin(players.Count());
        return tournament.GetMatches(roundNumber);
    }

    public static Tournament Create(long numberOfPlayers)
    {
        var tournament = new Tournament(new TwoPlayerRoundRobin(numberOfPlayers));
        return tournament;
    }
    
}

public interface ITournamentScheduler
{
    IEnumerable<Match> Schedule(IEnumerable<Player> players, long roundNumber);
}
