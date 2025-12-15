using eWorldCup.Application.Services;
using eWorldCup.Core.Interfaces.Repositories;
using eWorldCup.Core.Models.API;
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

public class PlayNextRoundHandler(ITournamentRepository tournaments,
    IRockPaperArenaService rockPaperArena) : IRequestHandler<PlayNextRoundRequest, MatchRoundResultsResponse>
{
    public Task<MatchRoundResultsResponse> Handle(PlayNextRoundRequest request, CancellationToken cancellationToken)
    {
        var results = rockPaperArena.PlayRound(request.TournamentId, request.PlayerMove);

        return Task.FromResult(new MatchRoundResultsResponse
        {
            CurrentRound = results.CurrentRound,
            NumberOfRoundsPlayed = results.NumberOfRoundsPlayed,
            RoundResults = new RoundResultsResponse()
            {
                PlayerMove = results.PlayerMove.ToString(),
                OpponentMove = results.OpponentMove.ToString(),
                IsDraw = results.IsDraw,
                IsPlayerWin = results.IsPlayerWin,
                IsOpponentWin = results.IsOpponentWin,
            },
            IsMatchOver = results.IsMatchOver,
            WinningPlayer = results is { IsMatchOver: true, Winner: not null }
                ? new PlayerApiModel
                {
                    Id = results.Winner.Id,
                    Name = results.Winner.Name
                }
                : null,
            PlayerScore = results.Score.Player,
            OpponentScore = results.Score.Opponent
        });
    }
}