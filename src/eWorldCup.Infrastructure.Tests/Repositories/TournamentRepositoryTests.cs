using eWorldCup.Core.Models;
using eWorldCup.Core.Models.Games.RockPaperArena;
using eWorldCup.Infrastructure.Repository;
using FluentAssertions;

namespace eWorldCup.Infrastructure.Tests.Repositories;

public class TournamentRepositoryTests
{
    private TournamentRepository _sut;

    [SetUp]
    public void Setup()
    {
        _sut = new TournamentRepository();
    }

    [Test]
    public void Adding_An_Object_Returns_The_Same_Object()
    {
        // Arrange
        var user = new Player(1, "Testson");
        var opponents = new List<Player>
        {
            new(2, "Opponent1"),
            new(3, "Opponent2"),
            new(4, "Opponent3"),
        };
        var tournament = RockPaperArenaTournament.CreateWithUserAndOpponents(user, opponents);
        // Act
        var savedTournament = _sut.Add(tournament);
        // Assign
        savedTournament.TournamentId.Should().Be(tournament.TournamentId);
        savedTournament.NumberOfPlayers.Should().Be(tournament.NumberOfPlayers);
        savedTournament.CurrentMatch.Should().BeEquivalentTo(tournament.CurrentMatch);

    }
}