using System.Text.RegularExpressions;
using eWorldCup.Core.Interfaces.Repositories;
using eWorldCup.Core.Models;
using eWorldCup.Core.Models.API;
using eWorldCup.Core.Models.API.Responses;
using eWorldCup.Core.Models.Games.RockPaperArena;
using MediatR;

namespace eWorldCup.Application.Features.RockPaperArena;

public class AdvanceToNextRoundRequest(Guid tournamentId) : IRequest<TournamentAdvancedResponse>
{
    public Guid TournamentId { get; init; } = tournamentId;
}

public class AdvanceToNextRoundHandler(ITournamentRepository tournaments) : IRequestHandler<AdvanceToNextRoundRequest, TournamentAdvancedResponse>
{
    public Task<TournamentAdvancedResponse> Handle(AdvanceToNextRoundRequest request, CancellationToken cancellationToken)
    {
        var tournament = tournaments.Get(request.TournamentId);
        
        var matches = tournament.Schedule.GetMatchesInRound(tournament.CurrentRound);
        var user = tournament.User 
                   ?? throw new NullReferenceException("Missing user in tournament, please start a new tournament");
        var matchesToSimulate = matches
            .Where(m => m.FirstPlayerIndex() != tournament.PlayerIndexes[user] &&
                        m.SecondPlayerIndex() != tournament.PlayerIndexes[user])
            .ToList();

        var bestOf = tournament.Settings.MaximumRoundsInAMatch;
        foreach (var match in matchesToSimulate)
        {
            match.SimulateRandom(bestOf);
            tournament.RegisterFinishedMatch(match);
        }

        tournament.AdvanceRound();
        tournaments.Update(tournament);
        var nextMatch = tournament.GetUserMatch();
        var opponent = tournament.GetParticipantByIndex(nextMatch.SecondPlayerIndex());
        return Task.FromResult(new TournamentAdvancedResponse()
        {
            NextMatch = new TournamentMatchResponse()
            {
                PlayedRounds = 0,
                BestOf = tournament.Settings.MaximumRoundsInAMatch,
                Opponent = new PlayerApiModel
                {
                    Id = opponent.Id,
                    Name = opponent.Name
                },
                Score = new MatchScoreApiModel(),
            }
        });
    }

}