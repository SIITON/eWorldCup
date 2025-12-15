using eWorldCup.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace eWorldCup.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly))
            .AddServices();
    }

    internal static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IRockPaperArenaService, RockPaperArenaService>();
    }
}