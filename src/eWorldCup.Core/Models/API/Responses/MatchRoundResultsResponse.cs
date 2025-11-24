namespace eWorldCup.Core.Models.API.Responses;

public class MatchRoundResultsResponse
{
    public string PlayerMove { get; init; } = string.Empty;
    public string OpponentMove { get; init; } = string.Empty;
    public bool IsDraw { get; init; }
    public bool IsPlayerWin { get; init; }
}
