using eWorldCup.Application.Services;
using eWorldCup.Core.Interfaces.Repositories;
using eWorldCup.Core.Models.API;
using eWorldCup.Core.Models.API.Responses;
using MediatR;

namespace eWorldCup.Application.Features.RockPaperArena;

public class AdvanceToNextRoundRequest(Guid tournamentId) : IRequest<TournamentAdvancedResponse>
{
    public Guid TournamentId { get; init; } = tournamentId;
}

public class AdvanceToNextRoundHandler(IRockPaperArenaService rockPaperArena) : IRequestHandler<AdvanceToNextRoundRequest, TournamentAdvancedResponse>
{
    public Task<TournamentAdvancedResponse> Handle(AdvanceToNextRoundRequest request, CancellationToken cancellationToken)
    {
        var tournament = rockPaperArena.Advance(request.TournamentId);
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