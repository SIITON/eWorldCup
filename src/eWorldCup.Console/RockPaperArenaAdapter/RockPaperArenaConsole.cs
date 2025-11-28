using System.Text;
using eWorldCup.Application.Features.RockPaperArena;
using eWorldCup.Core.Models.API;
using eWorldCup.Core.Models.API.Input;
using eWorldCup.Core.Models.API.Responses;
using MediatR;

namespace eWorldCup.ConsoleBackdoor.RockPaperArenaAdapter;

public class RockPaperArenaConsole(ISender sender) : IRockPaperArenaService
{
    public async Task RunAsync()
    {
        Console.WriteLine("Welcome to Rock paper scissors arena");
        Console.WriteLine("Please provide the following input");
        var tournament = await StartTournament();
        Console.Clear();
        var opponent = tournament.NextMatch.Opponent;
        Console.WriteLine($"Your first match is against {opponent.Name} with the id {opponent.Id}");
        Console.WriteLine($"The match is won in a best of {tournament.NextMatch.BestOf}");
        Console.WriteLine("Good luck \n");
        WriteScore(tournament.Player, tournament.NextMatch);
        for (var turns = 0; turns < tournament.NextMatch.BestOf; turns++)
        {
            var response = await PlayTournament(tournament.Id, ConsoleInput.GetPlayerMoveInput());
            WriteMatchRound(response);
            if (!response.IsMatchOver) continue;
            
            Console.WriteLine("\nThe match is over!");
            Console.WriteLine(response.IsPlayerWin 
                ? "You won the match! Congratulations!" 
                : "You lost the match! Better luck next time!");
            break;
        }

        await AdvanceTournament(tournament.Id);
        
        //
        Console.WriteLine("\nPress any key to go back");
        Console.ReadKey();
    }

    private async Task<bool> AdvanceTournament(Guid tournamentId)
    {
        var next = new GetNextMatchRequest();

        return await sender.Send(next);
    }

    private void WriteMatchRound(MatchRoundResultsResponse response)
    {
        Console.WriteLine("\nROUND RESULTS");
        Console.WriteLine($"You played {response.PlayerMove} \nOpponent played {response.OpponentMove}");
        Console.WriteLine(response.IsDraw
            ? "It's a draw!"
            : response.IsPlayerWin
                ? "You won this round!"
                : "You lost this round!");
    }

    internal void WriteScore(PlayerApiModel player, TournamentMatchResponse match)
    {
        var sb = new StringBuilder();
        var titleLeftPaddingLength = sb
            .Append(player.Name)
            .Append(" : ")
            .Append(match.Score.Player)
            .ToString().Length - 1;
        var scoreBoard = sb
            .Append(" - ")
            .Append(match.Score.Opponent)
            .Append(" : ")
            .Append(match.Opponent.Name)
            .ToString();
        
        var line = string.Concat(Enumerable.Repeat("-", scoreBoard.Length));
        const string title = "SCORE";
        var titleLeftPadding = string.Concat(Enumerable.Repeat(" ", titleLeftPaddingLength));
        Console.WriteLine(line);
        Console.WriteLine($"{titleLeftPadding}{title}");
        Console.WriteLine(scoreBoard);
        Console.WriteLine(line);
    }

    internal async Task<TournamentStartedResponse> StartTournament()
    {
        var start = new StartTournamentCommand
        {
            PlayerName = ConsoleInput.GetPlayerName(),
            NumberOfPlayers = ConsoleInput.GetPlayerCount()
        };
        
        return await sender.Send(start);
    }

    internal async Task<MatchRoundResultsResponse> PlayTournament(Guid tournamentId, PlayerMoveInput playerMove)
    {
        var request = new PlayNextRoundRequest(tournamentId)
            .ParsePlayerMove(playerMove.ChosenMove);
        return await sender.Send(request);
    }

}
