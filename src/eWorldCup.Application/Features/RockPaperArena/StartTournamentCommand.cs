using eWorldCup.Application.Services;
using eWorldCup.Core.Interfaces.Repositories;
using eWorldCup.Core.Models;
using eWorldCup.Core.Models.API;
using eWorldCup.Core.Models.API.Responses;
using eWorldCup.Core.Models.Games.RockPaperArena;
using eWorldCup.Core.Models.Tournaments;
using MediatR;

namespace eWorldCup.Application.Features.RockPaperArena;

public class StartTournamentCommand : IRequest<TournamentStartedResponse>
{
    public string PlayerName { get; init; } = string.Empty;
    public int NumberOfPlayers { get; init; }
}

public class StartTournamentHandler(IRockPaperArenaService rockPaperArena) 
    : IRequestHandler<StartTournamentCommand, TournamentStartedResponse>
{
    public async Task<TournamentStartedResponse> Handle(StartTournamentCommand request, CancellationToken cancellationToken)
    {
        // Setup tournament with players
        var tournament = rockPaperArena.Start(request.PlayerName, request.NumberOfPlayers);

        // Get the first match
        var userMatch = tournament.GetUserMatch();
        
        var user = tournament.User!;
        var opponent = tournament.GetParticipantByIndex(userMatch.SecondPlayerIndex());
        // Return tournament id and status
        return new TournamentStartedResponse
        {
            Id = tournament.TournamentId,
            Player = new PlayerApiModel
            {
                Id = user.Id,
                Name = user.Name
            },
            NextMatch = new TournamentMatchResponse
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
                    Player = userMatch.Score.Player,
                    Opponent = userMatch.Score.Opponent
                }
            }
        };
    }


}