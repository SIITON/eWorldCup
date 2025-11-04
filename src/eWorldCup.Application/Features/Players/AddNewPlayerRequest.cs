using eWorldCup.Core.Interfaces.Repositories;
using eWorldCup.Core.Models.API;
using MediatR;

namespace eWorldCup.Application.Features.Players;

public class AddNewPlayerRequest : IRequest<PlayerApiModel>
{
    public string Name { get; set; } = string.Empty;
}

public class AddNewPlayerHandler(IPlayerRepository players) : IRequestHandler<AddNewPlayerRequest, PlayerApiModel>
{
    public async Task<PlayerApiModel> Handle(AddNewPlayerRequest request, CancellationToken cancellationToken)
    {
        var addedPlayer = players.Add(new Core.Models.Player(0, request.Name));

        return await Task.FromResult(new PlayerApiModel()
        {
            Id = addedPlayer.Id,
            Name = addedPlayer.Name
        });
    }
}