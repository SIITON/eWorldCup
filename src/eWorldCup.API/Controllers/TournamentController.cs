using eWorldCup.Application.Features.DirectMatch;
using eWorldCup.Application.Features.MatchesInASpecificRound;
using eWorldCup.Application.Features.RemainingMatches;
using eWorldCup.Application.Features.RockPaperArena;
using eWorldCup.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eWorldCup.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TournamentController(ISender sender) : ControllerBase
{
    [HttpPost("start")]
    public async Task<IActionResult> StartTournament() // input chosen player name and number of players
    {
        var request = new StartTournamentCommand();
        var result = await sender.Send(request);
        return Ok(result);
    }

    [HttpGet("{tournamentId}/status")]
    public async Task<IActionResult> GetTournamentStatus([FromRoute] Guid tournamentId)
    {
        var request = new GetTournamentStatusRequest();
        var result = await sender.Send(request);
        return Ok(result);
    }

    [HttpPost("{tournamentId}/play")]
    public async Task<IActionResult> PlayNextRound([FromRoute] Guid tournamentId) // move input
    {
        var request = new PlayNextRoundRequest();
        var result = await sender.Send(request);
        return Ok(result);
    }

    [HttpPost("{tournamentId}/advance")]
    public async Task<IActionResult> AdvanceRound([FromRoute] Guid tournamentId) // simulate CPU player matches and return result
    {
        var request = new GetNextMatchRequest();
        var result = await sender.Send(request);
        return Ok(result);
    }

    [HttpGet("{tournamentId}/final")]
    public async Task<IActionResult> GetFinalResults([FromRoute] Guid tournamentId)
    {
        var request = new GetFinalResultsRequest();
        var result = await sender.Send(request);
        return Ok(result);
    }

    // givet antalet deltagare n och en specifik runda d,
    // skriva ut vilka par som ska spela i just den rundan.
    // 
    // Indata:
    // En lista med n deltagare (varje deltagare har id och name).
    // Ett heltal d (1 ≤ d ≤ n−1).
    // n = 6
    // d = 2
    // 
    // Möjlig utdata:
    // Alice vs Charlie
    // Bob vs Fiona
    // Diana vs Ethan
    [HttpPost("rounds/{roundNumber}")]
    public async Task<IActionResult> GetPairsInRound(int roundNumber, [FromBody] IEnumerable<Player> players)
    {
        var request = new GetMatchesInASpecificRoundRequest()
        {
            Players = players,
            RoundNumber = roundNumber
        };
        var result = await sender.Send(request);
        return Ok(result);
    }

    // Max antal rundor
    // Bestäm hur många rundor som kan spelas utan att något par upprepas.
    // Indata:
    // n = 10
    // 
    // Utdata:
    // 9
    [HttpGet("rounds/max")]
    public IActionResult GetMaxRounds([FromQuery] ulong n)
    {
        if (n < 2 || n > 10000000000000000000 || n % 2 != 0)
        {
            return BadRequest("n must be an even number between 2 and 10^18.");
        }
        var maxRounds = n - 1;
        return Ok(maxRounds);
    }

    // Återstående par
    // Beräkna hur många unika par som ännu inte hunnit mötas efter att D rundor har genomförts.
    // Indata:
    // n = 10
    // D = 3
    // 
    // Utdata:
    // 30
    [HttpGet("match/remaining")]
    public async Task<IActionResult> CountRemainingMatches([FromQuery] long n, [FromQuery] long d)
    {
        var request = new GetCountOfRemainingMatchesRequest
        {
            TotalPlayers = n,
            CompletedRounds = d
        };
        var result = await sender.Send(request);
        return Ok(result);
    }

    // Direktfråga
    // Givet
    // en specifik spelare i (index i listan)
    // en runda d, räkna ut direkt vem hen möter i just den rundan – utan att bygga hela schemat.
    // Indata:
    // n = 10
    // i = 4
    // d = 2
    // 
    // Utdata:
    // Ethan vs Julia
    [HttpGet("match")]
    public async Task<IActionResult> GetDirectMatch([FromQuery] long n, [FromQuery] long i, [FromQuery] long d)
    {
        var request = new GetDirectMatchRequest
        {
            TotalPlayers = n,
            PlayerIndex = i,
            RoundNumber = d
        };
        var result = await sender.Send(request);
        return Ok(result);
    }

}
