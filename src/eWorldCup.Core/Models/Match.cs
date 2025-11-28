using eWorldCup.Core.Models.Games.RockPaperArena;

namespace eWorldCup.Core.Models;

public class Match
{
    public long RoundNumber { get; set; }
    public IEnumerable<long> PlayerIds { get; set; } = new List<long>();
    public IEnumerable<long> PlayerIndex { get; set; } = new List<long>();
    
    public MatchScore Score { get; set; } = new();

    public void UpdateScore(HandResult results)
    {
        if (results.IsDraw) return;
        if (results.PlayerOneWins) Score.Player++;
        else if (results.PlayerTwoWins) Score.Opponent++;
        RoundNumber++;
    }
    
    public bool HasAWinner(int bestOf)
    {
        var neededToWin = (bestOf / 2) + 1;
        return Score.Player >= neededToWin || Score.Opponent >= neededToWin;
    }

    public int GetWinnerIndex()
    {
        var playerWon = Score.Player > Score.Opponent;
        if (playerWon)
        {
            return (int)PlayerIndex.ToList()[0];
        }
        var opponentWon = Score.Opponent > Score.Player;
        if (opponentWon)
        {
            return (int)PlayerIndex.ToList()[1];
        }
    }
}

public class MatchScore
{
    public int Player { get; set; }
    public int Opponent { get; set; }
}