using System.Collections.Concurrent;
using eWorldCup.Core.Interfaces.Repositories;
using eWorldCup.Core.Models.Games.RockPaperArena;
using eWorldCup.Infrastructure.ResponseModels;

namespace eWorldCup.Infrastructure.Repository;

public class TournamentRepository : ITournamentRepository
{
    internal ConcurrentDictionary<Guid, TournamentResponseModel> Tournaments = new();
    public RockPaperArenaTournament Add(RockPaperArenaTournament value)
    {
        var id = value.TournamentId;
        if (!Tournaments.TryAdd(id, Map(value)))
        {
            throw new ArgumentException($"Tournament with ID {id} already exists.");
        }

        return Get(id);
    }

    public RockPaperArenaTournament Get(Guid id)
    {
        if (Tournaments.TryGetValue(id, out var tournament))
        {
            return Map(tournament);
        }
        throw new KeyNotFoundException($"Tournament with ID {id} not found.");
    }

    public IEnumerable<RockPaperArenaTournament> GetAll()
    {
        return Tournaments.Select(kv => Map(kv.Value));
    }

    public RockPaperArenaTournament Update(RockPaperArenaTournament value)
    {
        var id = value.TournamentId;
        Tournaments[id] = Map(value);
        
        return Get(id);
    }

    public bool Delete(Guid id)
    {
        if (!Tournaments.TryRemove(id, out _))
        {
            throw new KeyNotFoundException($"Tournament with ID {id} not found.");
        }

        return true;
    }

    internal TournamentResponseModel Map(RockPaperArenaTournament value)
    {
        var currentMatch = value.Schedule.GetMatchesForPlayer(1).ToList()[(int)value.CurrentRound];
        return new TournamentResponseModel
        {
            Id = value.TournamentId.ToString(),
            NumberOfPlayers = value.NumberOfPlayers,
            CurrentRound = 0,
            CurrentMatch = new CurrentMatchResponseModel
            {
                Round = 0,
                PlayerOneScore = 0,
                PlayerTwoScore = 0
            },
            IsFinished = false,
            PlayerScores = null
        };
    }

    internal RockPaperArenaTournament Map(TournamentResponseModel value)
    {
        var id = Guid.Parse(value.Id);
        return new RockPaperArenaTournament(value.NumberOfPlayers, id)
        {
            CurrentRound = value.CurrentRound
        };
    }
}

