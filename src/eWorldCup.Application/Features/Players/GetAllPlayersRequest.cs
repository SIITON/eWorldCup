using eWorldCup.Core.Interfaces.Repositories;
using eWorldCup.Core.Models.API;
using MediatR;

namespace eWorldCup.Application.Features.Players;

public class GetAllPlayersRequest : IRequest<IEnumerable<PlayerApiModel>>
{
    
}

public class GetAllPlayersHandler(IPlayerRepository players) : IRequestHandler<GetAllPlayersRequest, IEnumerable<PlayerApiModel>>
{
    public async Task<IEnumerable<PlayerApiModel>> Handle(GetAllPlayersRequest request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(players.GetAll().Select(p => new PlayerApiModel()
        {
            Id = p.Id,
            Name = p.Name
        }));
    }
}