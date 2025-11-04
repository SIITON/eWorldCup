namespace eWorldCup.Core.Models.API;

public class TournamentMatchApiModel
{
    public long RoundNumber { get; set; }
    public IEnumerable<MatchApiModel> Matches { get; set; } = [];
}
