namespace eWorldCup.Core.Models.API.Responses;

public class MatchRoundResultsResponse
{
    public int CurrentRound { get; set; }
    public string PlayerMove { get; init; } = string.Empty;
    public string OpponentMove { get; init; } = string.Empty;
    public bool IsDraw { get; init; }
    public bool IsPlayerWin { get; init; }
    public bool IsMatchOver { get; set; }
    public int PlayerScore { get; set; }
    public int OpponentScore { get; set; }
    public int NumberOfRoundsPlayed { get; set; }
    public bool IsOpponentWin { get; set; }
}
