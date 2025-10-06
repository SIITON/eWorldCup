using eWorldCup.Application.Features.MatchesInASpecificRound;
using eWorldCup.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eWorldCup.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TournamentController(ISender sender) : ControllerBase
{
/*
Del ett
En spelturnering ska arrangeras med n deltagare (jämnt antal).
I varje runda paras deltagarna ihop två och två, och ingen får möta samma motståndare två gånger.
Efter totalt n−1 rundor har alla hunnit möta alla andra exakt en gång.

Du får en lista med deltagare (se nedan).
Din uppgift är att, givet antalet deltagare n och en specifik runda d,
skriva ut vilka par som ska spela i just den rundan.

Specifikation
Indata:
En lista med n deltagare (varje deltagare har id och name).
Ett heltal d (1 ≤ d ≤ n−1).
Utdata: n/2 rader där varje rad innehåller två namn nameA vs nameB.
Ordningen mellan raderna är valfri.
Paren ska konstrueras enligt round-robin-metoden (även kallad polygonmetoden),
så att inga par upprepas mellan olika rundor.
Exempel (med sample-data nedan)
Indata:
n = 6
d = 2

Möjlig utdata:
Alice vs Charlie
Bob vs Fiona
Diana vs Ethan
*/
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


    /*
    ---------------------------------------------------------
    Del två
    Turneringen blev snabbt en stor succé – så populär att den nu körs online med tusentals eller miljontals deltagare.
    Då går det inte längre att generera hela parlistor för varje runda. I stället behöver arrangörerna snabba beräkningar som ger svar direkt.

    Din uppgift är att räkna ut olika egenskaper kring rundorna utan att lista alla par.

    Specifikation
    Programmet ska kunna besvara tre typer av frågor:

    Max antal rundor
    Bestäm hur många rundor som kan spelas utan att något par upprepas.

    Återstående par
    Beräkna hur många unika par som ännu inte hunnit mötas efter att D rundor har genomförts.

    Direktfråga
    Givet en specifik spelare i (index i listan) och en runda d, räkna ut direkt vem hen möter i just den rundan – utan att bygga hela schemat.

    Indata
    n – antalet deltagare (2 ≤ n ≤ 10^18, och n måste vara jämnt).
    D – antal rundor som redan har spelats (0 ≤ D ≤ n−1).
    i – index för en specifik spelare (0 ≤ i < n).
    d – numret på en specifik runda (1 ≤ d ≤ n−1).
    Alla värden behöver inte användas i varje fråga – t.ex. används bara n för max antal rundor.

    Utdata
    Ett heltal: max antal rundor.
    Ett heltal: antal återstående par efter D rundor.
    Två namn: de två spelarna som möts i runda d.
    Exempel
    Exempel 1 – Max antal rundor

    Indata:
    n = 10

    Utdata:
    9
    Exempel 2 – Återstående par

    Indata:
    n = 10
    D = 3

    Utdata:
    30
    Exempel 3 – Direktfråga

    Indata:
    n = 10
    i = 4
    d = 2

    Utdata:
    Ethan vs Julia
    */
}