using eWorldCup.Core.Interfaces.Repositories;
using eWorldCup.Core.Models.API.Responses;
using eWorldCup.Core.Models.Games.RockPaperArena;
using MediatR;

namespace eWorldCup.Application.Features.RockPaperArena;

public class PlayNextRoundRequest(Guid tournamentId) : IRequest<MatchRoundResultsResponse>
{
    public Guid TournamentId { get; init; } = tournamentId;
    public HandShape PlayerMove { get; internal set; }

    public PlayNextRoundRequest ParsePlayerMove(string moveInput)
    {
        if (!Enum.TryParse<HandShape>(moveInput, out var hand))
        {
            hand = moveInput.ToLowerInvariant() switch
            {
                "rock" => HandShape.Rock,
                "r" => HandShape.Rock,
                "paper" => HandShape.Paper,
                "p" => HandShape.Paper,
                "scissors" => HandShape.Scissors,
                "s" => HandShape.Scissors,
                _ => HandShape.Undecided
            };
        }

        PlayerMove = hand;
        return this;
    }
}

public class PlayNextRoundHandler(ITournamentRepository tournaments) : IRequestHandler<PlayNextRoundRequest, MatchRoundResultsResponse>
{
    public Task<MatchRoundResultsResponse> Handle(PlayNextRoundRequest request, CancellationToken cancellationToken)
    {
        var tournament = tournaments.Get(request.TournamentId);
        var match = tournament.GetUserMatch();
        if (match.IsOver(tournament.Settings.MaximumRoundsInAMatch))
        {
            return Task.FromResult(new MatchRoundResultsResponse
            {
                IsMatchOver = true,
                CurrentMatchRound = (int)match.RoundNumber,
                PlayerScore = match.Score.Player,
                OpponentScore = match.Score.Opponent
            });
        }
        var playerHand = new Hand().Show(request.PlayerMove);
        var opponentHand = new Hand().Randomize();
        var results = playerHand.Versus(opponentHand);
        match.UpdateScore(results);
        var matchIsOver = match.IsOver(tournament.Settings.MaximumRoundsInAMatch);
        // store the results
        tournament.CurrentMatch = match;
        if (matchIsOver)
        {
            tournament.RegisterFinishedMatch(match);
        }
        tournaments.Update(tournament);
        // return the results

        return Task.FromResult(new MatchRoundResultsResponse
        {
            CurrentMatchRound = (int)match.RoundNumber,
            PlayerMove = playerHand.Shape.ToString(),
            OpponentMove = opponentHand.Shape.ToString(),
            IsDraw = results.IsDraw,
            IsPlayerWin = results.PlayerOneWins,
            IsMatchOver = matchIsOver,
            PlayerScore = match.Score.Player,
            OpponentScore = match.Score.Opponent
        });
    }
}