using eWorldCup.Core.Interfaces.Repositories;
using eWorldCup.Core.Models.API.Responses;
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
            Participants = participants.ToList()
        };
        return await Task.FromResult(status);
    }
}