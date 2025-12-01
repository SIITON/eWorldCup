using System.ComponentModel;
using eWorldCup.Core.Interfaces.Repositories;
using eWorldCup.Core.Models.API;
using eWorldCup.Core.Models.API.Responses;
using eWorldCup.Core.Models.Games.RockPaperArena;
using eWorldCup.Core.RailwayOriented;
using MediatR;

namespace eWorldCup.Application.Features.RockPaperArena;

public class GetTournamentStatusRequest(Guid tournamentId) : IRequest<TournamentStatusResponse>
{
    public Guid TournamentId { get; set; } = tournamentId;
}

public class GetTournamentStatusHandler(ITournamentRepository tournaments) : IRequestHandler<GetTournamentStatusRequest, TournamentStatusResponse>
{
    public async Task<TournamentStatusResponse> Handle(GetTournamentStatusRequest request, CancellationToken cancellationToken)
    {
        var tournament = tournaments.Get(request.TournamentId);

        var scoresByIndex = tournament.Scores.ScoresByPlayerIndex;
        var participants = tournament.Participants.Select(((player, idx) => new PlayerScoreResponse
        {
            Id = player.Id,
            Index = idx,
            Name = player.Name,
            Score = scoresByIndex.GetValueOrDefault(idx, -1)
        }));

        var status = new TournamentStatusResponse()
        {
            CurrentRound = tournament.CurrentRound,
            Participants = participants.ToList(),
            CurrentMatch = CreateMatchResponse(tournament)
        };
        return await Task.FromResult(status);
    }

    public TournamentMatchResponse CreateMatchResponse(RockPaperArenaTournament tournament)
    {
        var match = tournament.GetUserMatch();
        var opponent = tournament.GetParticipantByIndex(match.SecondPlayerIndex());
        return new TournamentMatchResponse
        {
            PlayedRounds = 0,
            BestOf = tournament.Settings.MaximumRoundsInAMatch,
            Opponent = new PlayerApiModel
            {
                Id = opponent.Id,
                Name = opponent.Name
            },
            Score = new MatchScoreApiModel
            {
                Player = match.Score.Player,
                Opponent = match.Score.Opponent
            }
        };
    }
}