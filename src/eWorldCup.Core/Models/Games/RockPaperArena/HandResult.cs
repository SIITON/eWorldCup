using eWorldCup.Core.Models.Games.RockPaperArena.Extensions;

namespace eWorldCup.Core.Models.Games.RockPaperArena;

public record HandResult(HandShape First, HandShape Second)
{
    public bool IsDraw => First == Second;
    public bool PlayerOneWins => First.Beats(Second);
    public bool PlayerTwoWins => Second.Beats(First);

    public override string ToString()
    {
        return PlayerOneWins 
            ? nameof(PlayerOneWins) 
            : PlayerTwoWins 
                ? nameof(PlayerTwoWins) 
                : nameof(IsDraw);
    }
}
