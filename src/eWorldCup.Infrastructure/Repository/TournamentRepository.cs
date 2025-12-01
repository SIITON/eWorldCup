using System.Collections.Concurrent;
using eWorldCup.Core.Interfaces.Repositories;
using eWorldCup.Core.Models;
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
        var currentMatch = value.CurrentMatch;
        return new TournamentResponseModel
        {
            Id = value.TournamentId.ToString(),
            Settings = value.Settings,
            NumberOfPlayers = value.NumberOfPlayers,
            CurrentRound = (int)value.CurrentRound,
            CurrentMatch = currentMatch is null ? null : new CurrentMatchResponseModel
            {
                Round = (int)currentMatch.RoundNumber,
                NumberOfRoundsPlayed = currentMatch.NumberOfRoundsPlayed,
                PlayerOne = new MatchParticipantResponseModel
                {
                    Id = (int)currentMatch.PlayerIds.ToArray()[0],
                    Index = currentMatch.FirstPlayerIndex(),
                    Score = currentMatch.Score.Player,
                },
                PlayerTwo = new MatchParticipantResponseModel
                {
                    Id = (int)currentMatch.PlayerIds.ToArray()[1],
                    Index = currentMatch.SecondPlayerIndex(),
                    Score = currentMatch.Score.Opponent
                }

            },
            IsFinished = false,
            PlayerScores = value.Scores.ScoresByPlayerIndex.ToDictionary(kv => kv.Key, kv => kv.Value),
            Participants = value.Participants
        };
    }

    internal RockPaperArenaTournament Map(TournamentResponseModel value)
    {
        var id = Guid.Parse(value.Id);
        var participants = value.Participants.ToList();
        var player = participants[0];
        var opponents = participants.Skip(1).ToList();
        var tournament =
            RockPaperArenaTournament.CreateWithUserAndOpponents(player, opponents,
                    settings =>
                    {
                        settings.MaximumRoundsInAMatch = value.Settings.MaximumRoundsInAMatch;
                        settings.PointsForWinningMatch = value.Settings.PointsForWinningMatch;
                        settings.PointsForDrawingMatch = value.Settings.PointsForDrawingMatch;
                        settings.PointsForLosingMatch = value.Settings.PointsForLosingMatch;
                    },
                    id);
        tournament.CurrentRound = value.CurrentRound;
        tournament.CurrentMatch = value.CurrentMatch is not null
            ? new Match
            {
                RoundNumber = value.CurrentMatch.Round,
                NumberOfRoundsPlayed = value.CurrentMatch.NumberOfRoundsPlayed,
                PlayerIds =
                [
                    value.CurrentMatch.PlayerOne.Id,
                    value.CurrentMatch.PlayerTwo.Id
                ],
                PlayerIndex =
                [
                    value.CurrentMatch.PlayerOne.Index,
                    value.CurrentMatch.PlayerTwo.Index
                ],
                Score = new MatchScore
                {
                    Player = value.CurrentMatch.PlayerOne.Score,
                    Opponent = value.CurrentMatch.PlayerTwo.Score
                },
            }
            : null;
        tournament.Scores = new TournamentScores
        {
            ScoresByPlayerIndex = value.PlayerScores
        };

        return tournament;
    }
}

