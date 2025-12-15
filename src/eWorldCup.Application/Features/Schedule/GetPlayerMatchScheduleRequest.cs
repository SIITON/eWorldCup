using eWorldCup.Core.Interfaces.Repositories;
using eWorldCup.Core.Models.API;
using eWorldCup.Core.Models.Tournaments;
using MediatR;

namespace eWorldCup.Application.Features.Schedule;

public class GetPlayerMatchScheduleRequest(int id) : IRequest<PlayerScheduleApiModel>
{
    public int Id { get; init; } = id;
}

public class GetPlayerMatchScheduleHandler(IPlayerRepository repository) : IRequestHandler<GetPlayerMatchScheduleRequest, PlayerScheduleApiModel>
{
    public async Task<PlayerScheduleApiModel> Handle(GetPlayerMatchScheduleRequest request, CancellationToken cancellationToken)
    {
        var players = repository.GetAll().ToList();
        var tournament = new TwoPlayerRoundRobin(players.Count);

        var matches = tournament.GetMatchesForPlayer(request.Id);

        return await Task.FromResult(new PlayerScheduleApiModel
        {
            Matches = matches.ToDictionary(
                match => match.RoundNumber,
                match => new MatchApiModel()
                {
                    Players = match.PlayerIds.Select(id =>
                        new PlayerApiModel()
                        {
                            Id = id,
                            Name = players.First(p => p.Id == id).Name
                        })
                }
            )
        });
    }
}