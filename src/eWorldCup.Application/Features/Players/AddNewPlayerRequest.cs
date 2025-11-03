using eWorldCup.Core.Models.API;
using MediatR;

namespace eWorldCup.Application.Features.Players;

public class AddNewPlayerRequest : IRequest<PlayerApiModel>
{
    public string Name { get; set; } = string.Empty;
}

public class AddNewPlayerHandler : IRequestHandler<AddNewPlayerRequest, PlayerApiModel>
{
    public Task<PlayerApiModel> Handle(AddNewPlayerRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}