using System.Collections;

namespace eWorldCup.Core.Models.Games.RockPaperArena.Exceptions;

public class HandDecisionMissingException(Hand challenger, Hand opponent) 
    : Exception
{
    public override string Message => "Both hands must be ready to compare.";
}