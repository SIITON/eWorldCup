using eWorldCup.Core.Interfaces.Repositories;
using eWorldCup.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace eWorldCup.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services
            .AddSingleton<IPlayerRepository, PlayerRepository>();
    }
}
