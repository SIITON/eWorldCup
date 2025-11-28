namespace eWorldCup.Infrastructure.ResponseModels;

public class TournamentResponseModel
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    public int CurrentRound { get; init; } = 1;
    public CurrentMatchResponseModel? CurrentMatch { get; set; }
    public bool IsFinished { get; init; } = false;
    public int NumberOfPlayers { get; set; }

    public Dictionary<int, int> PlayerScores = new();
}

public class CurrentMatchResponseModel
{
    public int Round { get; set; }
    public MatchParticipantResponseModel PlayerOne { get; init; } = new();
    public MatchParticipantResponseModel PlayerTwo { get; init; } = new();
}

public class MatchParticipantResponseModel
{
    public int Id { get; init; }
    public int Index { get; init; }
    public int Score { get; init; }
}