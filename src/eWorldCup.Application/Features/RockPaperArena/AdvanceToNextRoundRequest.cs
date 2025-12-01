using eWorldCup.Core.Interfaces.Repositories;
using eWorldCup.Core.Models.Games.RockPaperArena;
using MediatR;

namespace eWorldCup.Application.Features.RockPaperArena;

public class AdvanceToNextRoundRequest(Guid tournamentId) : IRequest<bool>
{
    public Guid TournamentId { get; init; } = tournamentId;
}

public class AdvanceToNextRoundHandler(ITournamentRepository tournaments) : IRequestHandler<AdvanceToNextRoundRequest, bool>
{
    public Task<bool> Handle(AdvanceToNextRoundRequest request, CancellationToken cancellationToken)
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
            
            for (var matchRound = 0; matchRound < bestOf; matchRound++)
            {
                var playerOneHand = new Hand().Randomize();
                var playerTwoHand = new Hand().Randomize();
                var results = playerOneHand.Versus(playerTwoHand);
                match.UpdateScore(results);
                if (match.HasAWinner(bestOf) 
                    || match.IsDraw(bestOf))
                {
                    break;
                }
            }
            tournament.RegisterFinishedMatch(match);
        }

        tournament.AdvanceRound();
        tournaments.Update(tournament);
        return Task.FromResult(true);
    }
}