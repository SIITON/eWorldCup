using eWorldCup.Application.Features.MatchesInASpecificRound;
using eWorldCup.Application.Services;
using eWorldCup.Application.Tests.Helpers;
using FluentAssertions;

namespace eWorldCup.Application.Tests.Features;

public class GetMatchesInASpecificRoundHandlerTests
{
    private GetMatchesInASpecificRoundHandler _sut;
    private TournamentScheduler _tournament;

    [SetUp]
    public void Setup()
    {
        _tournament = new TournamentScheduler();
        _sut = new GetMatchesInASpecificRoundHandler(_tournament);
    }

    [Test]
    public async Task Should_Return_The_Correct_Amount_Of_Matches()
    {
        // Arrange
        var data = new FakePlayerRepository();
        var players = data.GetAll().ToList();
        
        var request = new GetMatchesInASpecificRoundRequest
        {
            Players = players,
            RoundNumber = 1,

        };
        // Act
        var result = await _sut.Handle(request, CancellationToken.None);
        // Assert
        result.Matches.Count().Should().Be(players.Count / 2);
    }

}