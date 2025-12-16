using eWorldCup.Core.Models.Games.RockPaperArena;

namespace eWorldCup.Core.Models;

public class Match
{
    public override string ToString()
    {
        var ids = PlayerIds.ToArray();
        var idxs = PlayerIndex.ToArray();
        return $"id {ids[0]} vs {ids[1]} | index {idxs[0]} vs {idxs[1]}";
    }

    public long RoundNumber { get; set; }
    public int NumberOfRoundsPlayed { get; set; }
    public IEnumerable<long> PlayerIds { get; set; } = new List<long>();
    public IEnumerable<long> PlayerIndex { get; set; } = new List<long>();
    public int FirstPlayerIndex() => (int)PlayerIndex.ToList()[0];
    public int SecondPlayerIndex() => (int)PlayerIndex.ToList()[1];

    public MatchScore Score { get; set; } = new();

    public void UpdateScore(HandResult results)
    {
        if (results.PlayerOneWins) Score.Player++;
        else if (results.PlayerTwoWins) Score.Opponent++;
        NumberOfRoundsPlayed++;
    }
    
    public bool IsOver(int bestOf) => HasAWinner(bestOf) || IsDraw(bestOf);

    public bool HasAWinner(int bestOf)
    {
        var neededToWin = (bestOf / 2) + 1;
        if (NumberOfRoundsPlayed < neededToWin)
        {
            return false;
        }
        return Score.Player >= neededToWin 
               || Score.Opponent >= neededToWin
               || (Score.Player > Score.Opponent && NumberOfRoundsPlayed == bestOf)
               || (Score.Player < Score.Opponent && NumberOfRoundsPlayed == bestOf);
    }
    
    public bool IsDraw(int bestOf)
    {
        return !HasAWinner(bestOf) && NumberOfRoundsPlayed >= bestOf;
    }

    public int? GetWinnerIndex()
    {
        var playerWon = Score.Player > Score.Opponent;
        if (playerWon)
        {
            return FirstPlayerIndex();
        }
        var opponentWon = Score.Opponent > Score.Player;
        if (opponentWon)
        {
            return SecondPlayerIndex();
        }

        return null;
    }

    public void SimulateRandom(int bestOf)
    {
        while (!IsOver(bestOf))
        {
            var playerOneHand = new Hand().Randomize();
            var playerTwoHand = new Hand().Randomize();
            var results = playerOneHand.Versus(playerTwoHand);
            UpdateScore(results);
        }
    }
}

public class MatchScore
{
    public int Player { get; set; }
    public int Opponent { get; set; }
}