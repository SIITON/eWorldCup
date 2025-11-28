using eWorldCup.Application.Features.RockPaperArena;
using eWorldCup.Core.Models.Games.RockPaperArena;
using FluentAssertions;

namespace eWorldCup.Application.Tests.Features.RockPaperArena;

public class PlayNextRoundRequestTests
{
    private Guid _tournamentId;

    [SetUp]
    public void Setup()
    {
        _tournamentId = Guid.NewGuid();
    }

    [TestCase("rock", HandShape.Rock)]
    [TestCase("1", HandShape.Rock)]
    [TestCase("Rock", HandShape.Rock)]
    [TestCase("RocK", HandShape.Rock)]
    [TestCase("r", HandShape.Rock)]
    [TestCase("paper", HandShape.Paper)]
    [TestCase("2", HandShape.Paper)]
    [TestCase("Paper", HandShape.Paper)]
    [TestCase("papEr", HandShape.Paper)]
    [TestCase("p", HandShape.Paper)]
    [TestCase("scissors", HandShape.Scissors)]
    [TestCase("3", HandShape.Scissors)]
    [TestCase("Scissors", HandShape.Scissors)]
    [TestCase("SCiSSoRs", HandShape.Scissors)]
    [TestCase("s", HandShape.Scissors)]
    [TestCase("", HandShape.Undecided)]
    [TestCase("AsfASGAg", HandShape.Undecided)]
    [TestCase("ROCKPAPER", HandShape.Undecided)]
    [TestCase("0", HandShape.Undecided)]
    public void Can_Parse_Player_Moves(string playerMoveInput, HandShape expected)
    {
        // Act
        var request = new PlayNextRoundRequest(_tournamentId)
            .ParsePlayerMove(playerMoveInput);
        // Assert
        request.PlayerMove.Should().Be(expected);
    }
}