namespace eWorldCup.Application.Features.DirectMatch;

public class GetDirectMatchRequest
{
    public long TotalPlayers { get; set; }
    public long PlayerIndex { get; set; }
    public long RoundNumber { get; set; }
}