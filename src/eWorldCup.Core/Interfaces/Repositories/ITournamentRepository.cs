using eWorldCup.Core.Models.Games.RockPaperArena;

namespace eWorldCup.Core.Interfaces.Repositories;

public interface ITournamentRepository 
    : ICrudRepository<RockPaperArenaTournament, Guid>
{
    
}