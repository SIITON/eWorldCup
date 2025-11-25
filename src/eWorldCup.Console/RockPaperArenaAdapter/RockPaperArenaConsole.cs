using eWorldCup.Application.Features.RockPaperArena;
using eWorldCup.Application.Services;
using eWorldCup.Core.Interfaces.Repositories;
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
        Console.WriteLine($"Your first match is against {tournament.NextMatch.Opponent.Name}");
        Console.WriteLine($"The match is won in a best of {tournament.NextMatch.BestOf}");
        Console.WriteLine("Good luck \n");
    }

    internal async Task<TournamentStartedResponse> StartTournament()
    {
        var start = new StartTournamentCommand
        {
            PlayerName = GetPlayerName(),
            NumberOfPlayers = GetPlayerCount()
        };
        
        return await sender.Send(start);
    }

    internal static string GetPlayerName()
    {
        Console.Write("Enter player name: ");
        var name = Console.ReadLine() ?? "Player";
        var hellos = new[]{"Hello", "Hi", "hey", "God day", "Oh shit\n It's you", "is that really your name?"};
        var rnd = new Random(Guid.NewGuid().GetHashCode());
        Console.WriteLine($"{hellos[rnd.Next(0, hellos.Length)]} {name}");
        return name;
    }

    internal static int GetPlayerCount(int tries = 0)
    {
        while (true)
        {
            Console.Write("Enter number of opponents: ");
            if (int.TryParse(Console.ReadLine(), out var count) || count >= 1) return count;
            Console.WriteLine("Enter a valid integer");
            tries++;
            if (tries > 3)
            {
                return 10;
            }
        }
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
