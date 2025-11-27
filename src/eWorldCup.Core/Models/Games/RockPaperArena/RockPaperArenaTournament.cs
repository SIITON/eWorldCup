using eWorldCup.Core.Models.Tournaments;

namespace eWorldCup.Core.Models.Games.RockPaperArena;

public class RockPaperArenaTournament(int numberOfPlayers = 2, Guid? id = null) 
    : TwoPlayerTournament(new TwoPlayerRoundRobin(numberOfPlayers))
{
    public Guid TournamentId { get; init; } = id ?? Guid.NewGuid();
    public int NumberOfPlayers { get; init; } = numberOfPlayers;
    // TODO add current match and scores statistics
}

public class TournamentScores
{
    
}