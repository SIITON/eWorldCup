namespace eWorldCup.Core.Models.Games.RockPaperArena;

public class RoundResults
{
    public int CurrentRound { get; set; }
    public int NumberOfRoundsPlayed { get; set; }
    public bool IsMatchOver { get; set; }
    public HandShape PlayerMove { get; init; } 
    public HandShape OpponentMove { get; init; }
    public bool IsDraw { get; init; }
    public bool IsPlayerWin { get; init; }
    public bool IsOpponentWin { get; set; }
    public RoundScore Score { get; set; } = new();
    public Player? Winner { get; set; }
}

public class RoundScore
{

    public int Player { get; set; }
    public int Opponent { get; set; }
}