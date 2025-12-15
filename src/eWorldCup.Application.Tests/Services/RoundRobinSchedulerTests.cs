using eWorldCup.Application.Services;
using eWorldCup.Core.Models;
using eWorldCup.Core.Models.Tournaments;
using FluentAssertions;

namespace eWorldCup.Application.Tests.Services;

public class RoundRobinSchedulerTests
{

    [Test]
    public void Should_Return_All_Matches_For_A_Given_Round()
    {
        // Arrange
        var players = new List<Player>
        {
            new(1, "Alice"),
            new(2, "Bob"),
            new(3, "Charlie"),
            new(4, "Diana"),
            new(5, "Ethan"),
            new(6, "Fiona"),
            new(7, "George"),
            new(8, "Hannah"),
            new(9, "Isaac"),
            new(10, "Julia")
        };
        var tournament = new TwoPlayerRoundRobin(players.Count);
        // Act
        var matches = tournament.GetMatchesInRound(9).ToList();
        // Assert
        matches.Should().HaveCount(5);
    }
}
