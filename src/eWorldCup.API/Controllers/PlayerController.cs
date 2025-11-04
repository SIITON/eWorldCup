using eWorldCup.Application.Features.Players;
using eWorldCup.Core.Models.API;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eWorldCup.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayerController(ISender sender) : ControllerBase
{

    [HttpGet("{id}/schedule")]
    public async Task<IActionResult> GetPlayerSchedule(int id)
    {
        // Placeholder implementation
        var schedule = new
        {
            PlayerId = id,
            Matches = new[]
            {
                new { Opponent = "Player 2", Date = "2024-07-01" },
                new { Opponent = "Player 3", Date = "2024-07-05" }
            }
        };
        return Ok(schedule);
    }

    [HttpGet("{id}/round/{d}")]
    public async Task<IActionResult> GetPlayerMatchesInRound(int id, int d)
    {
        // Placeholder implementation
        var matches = new
        {
            PlayerId = id,
            Round = d,
            Matches = new[]
            {
                new { Opponent = "Player 4", Date = "2024-07-10" }
            }
        };
        return Ok(matches);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePlayer([FromBody] NewPlayerApiModel player)
    {
        var request = new AddNewPlayerRequest()
        {
            Name = player.Name
        };
        var result = await sender.Send(request);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPlayers()
    {
        var players = await sender.Send(new GetAllPlayersRequest());
        return Ok(players);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlayer(int id)
    {
        var hasDeleted = await sender.Send(new DeletePlayerCommand(id));
        
        return hasDeleted 
            ? Ok() 
            : NotFound();
    }

}