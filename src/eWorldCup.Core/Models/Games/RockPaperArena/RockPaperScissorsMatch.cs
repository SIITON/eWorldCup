namespace eWorldCup.Core.Models.Games.RockPaperArena;

/// <summary>
/// Represents a match of Rock-Paper-Scissors.
/// </summary>
public class RockPaperScissorsMatch(int bestOf = 3)
{

    // 3 first to 2
    // 5 first to 3
    // 24 first to 13
    
    public void Start()
    {
        for (var round = 0; round < bestOf; round++)
        {
            
        }
    }
    
    public void GetStatistics()
    {
        // Implementation of statistics logic goes here.
    }
}