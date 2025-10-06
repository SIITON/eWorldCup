namespace eWorldCup.Core.Models.API;

public class TournamentMatchApiModel
{
    public long RoundNumber { get; set; }
    public IEnumerable<MatchApiModel> Players { get; set; } = [];
}

public class MatchApiModel
{
    public IEnumerable<Player> Players { get; set; } = [];
}