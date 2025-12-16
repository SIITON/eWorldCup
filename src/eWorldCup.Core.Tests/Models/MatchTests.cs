using eWorldCup.Core.Models;
using eWorldCup.Core.Models.Games.RockPaperArena;
using FluentAssertions;

namespace eWorldCup.Core.Tests.Models;

public class MatchTests
{
    [Test]
    public void ToString_ShouldReturnCorrectFormat()
    {
        var match = new Match
        {
            PlayerIds = new List<long> { 1, 2 },
            PlayerIndex = new List<long> { 0, 1 }
        };

        match.ToString().Should().Be("id 1 vs 2 | index 0 vs 1");
    }

    [Test]
    public void UpdateScore_PlayerOneWins_IncrementsPlayerScore()
    {
        var match = new Match();
        var result = new HandResult(HandShape.Rock, HandShape.Scissors);

        match.UpdateScore(result);

        match.Score.Player.Should().Be(1);
        match.Score.Opponent.Should().Be(0);
        match.NumberOfRoundsPlayed.Should().Be(1);
    }

    [Test]
    public void UpdateScore_PlayerTwoWins_IncrementsOpponentScore()
    {
        var match = new Match();
        var result = new HandResult(HandShape.Rock, HandShape.Paper);

        match.UpdateScore(result);

        match.Score.Player.Should().Be(0);
        match.Score.Opponent.Should().Be(1);
        match.NumberOfRoundsPlayed.Should().Be(1);
    }

    [Test]
    public void HasAWinner_ReturnsTrue_WhenPlayerHasEnoughWins()
    {
        var match = new Match
        {
            Score = new MatchScore { Player = 2, Opponent = 0 },
            NumberOfRoundsPlayed = 3
        };

        match.HasAWinner(3).Should().BeTrue();
    }

    [Test]
    public void IsDraw_ReturnsTrue_WhenNoWinnerAndRoundsPlayedEqualsBestOf()
    {
        var match = new Match
        {
            Score = new MatchScore { Player = 1, Opponent = 1 },
            NumberOfRoundsPlayed = 2
        };

        match.IsDraw(2).Should().BeTrue();
    }

    [Test]
    public void GetWinnerIndex_ReturnsFirstPlayerIndex_WhenPlayerWins()
    {
        var match = new Match
        {
            PlayerIndex = new List<long> { 5, 6 },
            Score = new MatchScore { Player = 3, Opponent = 1 }
        };

        match.GetWinnerIndex().Should().Be(5);
    }

    [Test]
    public void GetWinnerIndex_ReturnsSecondPlayerIndex_WhenOpponentWins()
    {
        var match = new Match
        {
            PlayerIndex = new List<long> { 7, 8 },
            Score = new MatchScore { Player = 1, Opponent = 2 }
        };

        match.GetWinnerIndex().Should().Be(8);
    }

    [Test]
    public void GetWinnerIndex_ReturnsNull_WhenDraw()
    {
        var match = new Match
        {
            PlayerIndex = new List<long> { 1, 2 },
            Score = new MatchScore { Player = 2, Opponent = 2 }
        };

        match.GetWinnerIndex().Should().BeNull();
    }

    [Test]
    public void SimulateRandom_CompletesMatch()
    {
        var match = new Match
        {
            PlayerIndex = new List<long> { 1, 2 }
        };

        match.SimulateRandom(3);

        match.IsOver(3).Should().BeTrue();
        (match.Score.Player > 0 || match.Score.Opponent > 0).Should().BeTrue();
    }
}
