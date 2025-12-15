namespace eWorldCup.Core.Models.API.Responses;

public class MatchRoundResultsResponse
{
    public int CurrentRound { get; set; }
    public int NumberOfRoundsPlayed { get; set; }
    public RoundResultsResponse RoundResults { get; init; } = new();
    public bool IsMatchOver { get; set; }
    public int PlayerScore { get; set; }
    public int OpponentScore { get; set; }
    public PlayerApiModel? WinningPlayer { get; set; }
}

public class RoundResultsResponse
{
    public string PlayerMove { get; init; } = string.Empty;
    public string OpponentMove { get; init; } = string.Empty;
    public bool IsDraw { get; init; }
    public bool IsPlayerWin { get; init; }
    public bool IsOpponentWin { get; set; }
}