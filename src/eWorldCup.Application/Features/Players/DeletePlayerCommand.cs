using eWorldCup.Core.Interfaces.Repositories;
using MediatR;

namespace eWorldCup.Application.Features.Players;

public class DeletePlayerCommand(int id) : IRequest<bool>
{
    public int Id = id;
}

public class DeletePlayerHandler(IPlayerRepository players) : IRequestHandler<DeletePlayerCommand, bool>
{
    public async Task<bool> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            players.Remove(request.Id);
            return await Task.FromResult(true);
        }
        catch (Exception e)
        {
            return await Task.FromResult(false);
        }
    }
}