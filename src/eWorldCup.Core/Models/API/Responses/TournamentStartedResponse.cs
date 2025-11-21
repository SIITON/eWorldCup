namespace eWorldCup.Core.Models.API.Responses;

public class TournamentStartedResponse
{
    public PlayerApiModel Player { get; init; } = new();
    public TournamentMatchResponse NextMatch { get; init; } = new();
}

public class TournamentMatchResponse
{
    public int CurrentRound { get; init; }
    public int BestOf { get; init; } = 3;
    public PlayerApiModel Opponent { get; init; } = new();
    public MatchScoreApiModel Score { get; init; } = new();
}

public class MatchScoreApiModel
{
    public int PlayerScore { get; init; }
    public int OpponentScore { get; init; }
}