using eWorldCup.Core.Models;
using eWorldCup.Core.Models.Tournaments;
namespace eWorldCup.Application.Services;

public class TournamentScheduler : ITournamentScheduler
{
    public IEnumerable<Match> Schedule(IEnumerable<Player> players, long roundNumber)
    {
        var tournament = new TwoPlayerRoundRobin(players.ToList());
        return tournament.GetMatches(roundNumber);
    }
    
}

public interface ITournamentScheduler
{
    IEnumerable<Match> Schedule(IEnumerable<Player> players, long roundNumber);
}
