using MediatR;

namespace eWorldCup.Application.Features.Players;

public class DeletePlayerCommand(int id) : IRequest<bool>
{
    public int Id = id;
}

public class DeletePlayerHandler : IRequestHandler<DeletePlayerCommand, bool>
{
    public Task<bool> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}