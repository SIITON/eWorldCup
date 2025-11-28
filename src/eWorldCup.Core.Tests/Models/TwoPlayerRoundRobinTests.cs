using eWorldCup.Core.Models.Tournaments;
using FluentAssertions;

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
        var rounds = new List<int[]>();
        for (var round = 1; round < t.NumberOfRounds + 1; round++)
        {
            var playerIndexesInEachMatchThisRound = t
                .GetMatchesInRound(round)
                .Select(m => m.PlayerIndex
                    .Select(idx => (int)idx)
                    .ToArray());
            rounds = rounds.Concat(playerIndexesInEachMatchThisRound).ToList();
        }
        // Assert
        rounds.Count.Should().Be(rounds.Distinct().Count());
    }
}