using System.Text;
using eWorldCup.Application.Features.RockPaperArena;
using eWorldCup.Application.Services;
using eWorldCup.Core.Interfaces.Repositories;
using eWorldCup.Core.Models.API;
using eWorldCup.Core.Models.API.Input;
using eWorldCup.Core.Models.API.Responses;
using eWorldCup.Infrastructure.ResponseModels;
using MediatR;

namespace eWorldCup.ConsoleBackdoor.RockPaperArenaAdapter;

public class RockPaperArenaConsole(IPlayerRepository playersRepository, 
    ITournamentScheduler tournamentScheduler,
    ISender sender) : IRockPaperArenaService
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
        }
        
        //
        Console.WriteLine("\nPress any key to go back");
        Console.ReadKey();
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

    public void Statistics()
    {
        throw new NotImplementedException();
    }

    public void Start()
    {
        var players = playersRepository.GetAll().ToList();
        // todo tournament factory for different game types
        var tournament = tournamentScheduler.Create(players.Count);
        tournament.CurrentRound = 1;
        // run matches
        for (int round = 0; round < tournament.NumberOfRoundsLeft; round++)
        {
            var matches = tournament.Schedule.GetMatchesInRound(tournament.CurrentRound).ToList();
            
            tournament.AdvanceRound();
        }
        // display winner
        // store statistics
        // return results
    }

    public void Play()
    {
        
    }

    public void Start(StartTournamentCommand command)
    {
        throw new NotImplementedException();
    }

    public void Status(Guid tournamentId)
    {
        throw new NotImplementedException();
    }

    public void Play(Guid tournamentId)
    {
        throw new NotImplementedException();
    }

    public void Advance(Guid tournamentId)
    {
        throw new NotImplementedException();
    }

    public void Final(Guid tournamentId)
    {
        throw new NotImplementedException();
    }

}
