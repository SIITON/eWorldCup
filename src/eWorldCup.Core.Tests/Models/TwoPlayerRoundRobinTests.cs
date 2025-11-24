using eWorldCup.Core.Models.Tournaments;

namespace eWorldCup.Core.Tests.Models;

public class TwoPlayerRoundRobinTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Two_Players_Should_Not_Face_Each_Other_More_Than_Once()
    {
        // Arrange
        var t = new TwoPlayerRoundRobin(4);
        // Act
        var numberOfRounds = t.NumberOfRounds;

        var firstRound = t.GetMatchesInRound(1).Select(m => m.PlayerIndex);
        var secondRound = t.GetMatchesInRound(2).Select(m => m.PlayerIndex);
        var thirdRound = t.GetMatchesInRound(3).Select(m => m.PlayerIndex);
        // Assert

    }
}