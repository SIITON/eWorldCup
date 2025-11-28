using eWorldCup.Core.Models.Tournaments;

namespace eWorldCup.Core.Models.Games.RockPaperArena;

public class RockPaperArenaTournament(int numberOfPlayers = 2, Guid? id = null) 
    : TwoPlayerTournament(new TwoPlayerRoundRobin(numberOfPlayers))
{
    public Guid TournamentId { get; init; } = id ?? Guid.NewGuid();
    public int NumberOfPlayers { get; init; } = numberOfPlayers;
    public int MaximumRoundsInAMatch { get; init; } = 3;

    public List<Player> Participants { get; internal set; } = [];
    public Player User { get; internal set; }

    public void AddUser(Player player)
    {
        Participants.Add(player);
        User = player;
        PlayerIndexes.Add(player, 1);
    }

    public Dictionary<Player, int> PlayerIndexes { get; internal set; } = new();

    public void AddParticipants(IEnumerable<Player> players)
    {
        Participants.AddRange(players);
        var idx = 2; // User is always at index 1
        foreach (var player in players)
        {
            PlayerIndexes.Add(player, idx);
            idx++;
        }
    }
    public Match GetUserMatch() => Schedule
        .GetMatchesForPlayer(PlayerIndexes[User])
        .ToArray()
        [(int)CurrentRound];

    public Match? CurrentMatch { get; set; }
}

public class TournamentScores
{
    public Dictionary<int, int> ScoresByPlayerIndex { get; internal set; } = new();

    public void RegisterFinishedMatch(Match match)
    {
        
    }
}