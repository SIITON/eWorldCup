namespace eWorldCup.Core.Models;

public class Match
{
    public long RoundNumber { get; set; }
    public IEnumerable<Player> Players { get; set; } = new List<Player>();
}
