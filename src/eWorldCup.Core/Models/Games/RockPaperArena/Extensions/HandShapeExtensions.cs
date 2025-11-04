namespace eWorldCup.Core.Models.Games.RockPaperArena.Extensions;

public static class HandShapeExtensions
{
    public static bool Beats(this HandShape first, HandShape second)
    {
        return (first == HandShape.Rock && second == HandShape.Scissors) ||
               (first == HandShape.Paper && second == HandShape.Rock) ||
               (first == HandShape.Scissors && second == HandShape.Paper);
    }
}
