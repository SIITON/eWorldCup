namespace eWorldCup.Infrastructure.ResponseModels;

public class RockPaperScissorsMatchResponseModel
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    public int CurrentRound { get; init; } = 1;
    public int PlayerOneScore { get; init; } = 0;
    public int PlayerTwoScore { get; init; } = 0;
    public bool IsFinished { get; init; } = false;
}


