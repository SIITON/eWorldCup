namespace eWorldCup.Core.Models.API.Responses;

public class TournamentStatusResponse
{
    public IList<PlayerScoreResponse> Participants { get; set; } = [];
    public long CurrentRound { get; set; }
    public TournamentMatchResponse CurrentMatch { get; set; } = new();
}

public class PlayerScoreResponse
{
    public int Id { get; set; }
    public int Index { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Score { get; set; }
}