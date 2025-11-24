namespace eWorldCup.Core.Models.API.Responses;

public class TournamentStartedResponse
{
    public Guid Id { get; set; }
    public PlayerApiModel Player { get; init; } = new();
    public TournamentMatchResponse NextMatch { get; init; } = new();
}

/// <summary>
/// The upcoming match
/// </summary>
public class TournamentMatchResponse
{
    public int CurrentRound { get; init; }
    public int BestOf { get; init; } = 3;
    public PlayerApiModel Opponent { get; init; } = new();
    /// <summary>
    /// Contains Player and Opponent score
    /// </summary>
    public MatchScoreApiModel Score { get; init; } = new();
}

public class MatchScoreApiModel
{
    public int Player { get; init; }
    public int Opponent { get; init; }
}

// When we play a hand

