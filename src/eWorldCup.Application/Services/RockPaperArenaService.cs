using eWorldCup.Core.Interfaces.Repositories;
using eWorldCup.Core.Interfaces.Services;

namespace eWorldCup.Application.Services;

public class RockPaperArenaService(IPlayerRepository playersRepository, 
    ITournamentScheduler tournamentScheduler) : IRockPaperArenaService
{
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
}
