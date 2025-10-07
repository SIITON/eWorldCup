namespace eWorldCup.Core.Models;

public class Match
{
    public long RoundNumber { get; set; }
    public IEnumerable<long> PlayerIds { get; set; } = new List<long>();
}
