namespace eWorldCup.Infrastructure.ResponseModels;

public class TournamentResponseModel
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    public int CurrentRound { get; init; } = 1;
    public CurrentMatchResponseModel? CurrentMatch { get; set; }
    public bool IsFinished { get; init; } = false;
    public Dictionary<int, int> PlayerScores = new();
}

public class CurrentMatchResponseModel
{
    public int Round { get; set; }
    public int PlayerOneScore { get; init; } = 0;
    public int PlayerTwoScore { get; init; } = 0;
}
