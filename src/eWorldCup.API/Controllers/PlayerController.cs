using eWorldCup.Application.Features.DirectMatch;
using eWorldCup.Application.Features.Players;
using eWorldCup.Application.Features.Schedule;
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
        var schedule = await sender.Send(new GetPlayerMatchScheduleRequest(id));
        return Ok(schedule);
    }

    [HttpGet("{id}/round/{d}")]
    public async Task<IActionResult> GetPlayerMatchesInRound(int id, int d)
    {
        var request = new GetDirectMatchRequest
        {
            PlayerIndex = id - 1,
            RoundNumber = d
        };
        var result = await sender.Send(request);
        return Ok(result);
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