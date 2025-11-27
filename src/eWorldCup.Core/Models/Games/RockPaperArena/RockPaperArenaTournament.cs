using eWorldCup.Core.Models.Tournaments;

namespace eWorldCup.Core.Models.Games.RockPaperArena;

public class RockPaperArenaTournament(int numberOfPlayers = 2, Guid? id = null) 
    : TwoPlayerTournament(new TwoPlayerRoundRobin(numberOfPlayers))
{
    public Guid TournamentId { get; init; } = id ?? Guid.NewGuid();
    public int NumberOfPlayers { get; init; } = numberOfPlayers;

    public List<Player> Participants { get; internal set; } = [];
    public Player User { get; internal set; }

    public void AddUser(Player player)
    {
        Participants.Add(player);
        
    }
    public void AddParticipants(IEnumerable<Player> players) => Participants.AddRange(players);
    // TODO add current match and scores statistics
}

public class TournamentScores
{
    
}