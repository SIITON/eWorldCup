using eWorldCup.Core.Models;
using FluentAssertions;

namespace eWorldCup.Core.Tests.Models;

public class MatchTests
{
    [Test]
    public void SimulateRandom_Should_Go_Until_Match_IsOver()
    {
        // Arrange
        var match = new Match
        {
            RoundNumber = 1,
            NumberOfRoundsPlayed = 0,
            PlayerIds = [0, 9],
            PlayerIndex = [1, 10],
            Score = new MatchScore()
        };
        // Act
        match.SimulateRandom(300);
        // Assert
        match.IsOver(300).Should().BeTrue();
        match.NumberOfRoundsPlayed.Should().BeGreaterThan(0);
    }
}