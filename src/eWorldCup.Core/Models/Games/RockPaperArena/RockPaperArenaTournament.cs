using eWorldCup.Core.Models.Tournaments;

namespace eWorldCup.Core.Models.Games.RockPaperArena;

public class RockPaperArenaTournament(int numberOfPlayers = 2, Guid? id = null) 
    : TwoPlayerTournament(new TwoPlayerRoundRobin(numberOfPlayers))
{
    public static RockPaperArenaTournament CreateWithUserAndOpponents(
        Player user,
        IList<Player> opponents,
        Action<RockPaperArenaSettings>? settings = null, 
        Guid? id = null)
    {
        var tournament = new RockPaperArenaTournament(user, opponents);
        if (settings == null) return tournament;
        var s = new RockPaperArenaSettings();
        settings(s);
        tournament.Settings = s;
        return tournament;
    }

    private RockPaperArenaTournament(Player user,
        IList<Player> opponents, 
        Guid? id = null) : this(opponents.Count + 1)
    {
        TournamentId = id ?? Guid.NewGuid();
        SetUser(user);
        SetParticipants(opponents);
        for (var i = 0; i < Participants.Count; i++)
        {
            Scores.ScoresByPlayerIndex[i] = 0;
        }
    }
    
    public Guid TournamentId { get; init; } = id ?? Guid.NewGuid();
    public int NumberOfPlayers { get; init; } = numberOfPlayers;
    public RockPaperArenaSettings Settings { get; internal set; } = new();

    public List<Player> Participants { get; internal set; } = [];
    public Player? User { get; internal set; } = null;
    public Match? CurrentMatch { get; set; } = null;
    public TournamentScores Scores { get; set; } = new();
    public Dictionary<Player, int> PlayerIndexes { get; internal set; } = new();

    internal void SetUser(Player player)
    {
        Participants.Add(player);
        User = player;
        PlayerIndexes.Add(player, 1);
    }

    internal void SetParticipants(IList<Player> players)
    {
        Participants.AddRange(players);
        var idx = 2; // User is always at index 1
        foreach (var player in players)
        {
            PlayerIndexes.Add(player, idx);
            idx++;
        }
    }
    public Match GetUserMatch() => CurrentMatch ?? Schedule
        .GetMatchesForPlayer(PlayerIndexes[User])
        .ToArray()
        [(int)CurrentRound];

    public void RegisterFinishedMatch(Match match)
    {
        var playerOne = match.FirstPlayerIndex();
        var playerTwo = match.SecondPlayerIndex();
        if (match.IsDraw(Settings.MaximumRoundsInAMatch))
        {
            Scores.ScoresByPlayerIndex[playerOne] += Settings.PointsForDrawingMatch;
            Scores.ScoresByPlayerIndex[playerTwo] += Settings.PointsForDrawingMatch;
        }

        if (!match.HasAWinner(Settings.MaximumRoundsInAMatch)) return;
        var winnerIndex = match.GetWinnerIndex();
        Scores.ScoresByPlayerIndex[winnerIndex] += Settings.PointsForWinningMatch;
        var loserIndex = winnerIndex == playerOne ? playerTwo : playerOne;
        Scores.ScoresByPlayerIndex[loserIndex] += Settings.PointsForLosingMatch;
    }

}

public class TournamentScores
{
    public Dictionary<int, int> ScoresByPlayerIndex { get; set; } = new();
    
}

public class RockPaperArenaSettings
{
    public int MaximumRoundsInAMatch { get; set; } = 3;
    public int PointsForWinningMatch { get; set; } = 3;
    public int PointsForDrawingMatch { get; set; } = 1;
    public int PointsForLosingMatch { get; set; } = 0;
}