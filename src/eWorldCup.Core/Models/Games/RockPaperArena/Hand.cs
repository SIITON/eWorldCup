using eWorldCup.Core.Models.Games.RockPaperArena.Exceptions;

namespace eWorldCup.Core.Models.Games.RockPaperArena;

public class Hand
{
    public HandShape Shape { get; private set; } = HandShape.Undecided;
    public bool IsReady { get; private set; } = false;

    public Hand Randomize()
    {
        if (IsReady) return this;
        var seed = Guid.NewGuid().GetHashCode();
        var rnd = new Random(seed);
        var numberOfPossibleHandshapes = Enum
            .GetNames<HandShape>()
            .Where(hs => hs != nameof(HandShape.Undecided))
            .ToList()
            .Count;
        var shapeIdx = rnd.Next(1, numberOfPossibleHandshapes);
        
        return Show((HandShape)shapeIdx);
    }
    
    public Hand Reset()
    {
        Shape = HandShape.Undecided;
        IsReady = false;
        return this;
    }

    public Hand Show(HandShape shape)
    {
        Shape = shape;
        IsReady = true;
        return this;
    }
    
    public HandResult Versus(Hand other)
    {
        if (IsReady && other.IsReady)
        {
            return new HandResult(this.Shape, other.Shape);
        }
        throw new HandDecisionMissingException(this, other);
    }
}

