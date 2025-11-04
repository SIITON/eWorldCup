using System.Text.Json;
using System.Text.Json.Serialization;
using eWorldCup.Core.Interfaces.Repositories;
using eWorldCup.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace eWorldCup.Infrastructure;

public static class DependencyInjection
{
    internal record PlayerSeed([property: JsonPropertyName("id")] int Id, [property: JsonPropertyName("name")] string Name);
    private const string Seed = """
                                [
                                  { "id": 1, "name": "Alice" },
                                  { "id": 2, "name": "Bob" },
                                  { "id": 3, "name": "Charlie" },
                                  { "id": 4, "name": "Diana" },
                                  { "id": 5, "name": "Ethan" },
                                  { "id": 6, "name": "Fiona" },
                                  { "id": 7, "name": "George" },
                                  { "id": 8, "name": "Hannah" },
                                  { "id": 9, "name": "Isaac" },
                                  { "id": 10, "name": "Julia" },
                                  { "id": 11, "name": "Kevin" },
                                  { "id": 12, "name": "Laura" },
                                  { "id": 13, "name": "Michael" },
                                  { "id": 14, "name": "Nina" },
                                  { "id": 15, "name": "Oscar" },
                                  { "id": 16, "name": "Paula" },
                                  { "id": 17, "name": "Quentin" },
                                  { "id": 18, "name": "Rachel" },
                                  { "id": 19, "name": "Samuel" },
                                  { "id": 20, "name": "Tina" }
                                ]
                                """;
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        var players = JsonSerializer.Deserialize<List<PlayerSeed>>(Seed) ?? throw new NullReferenceException();

        return services
            .AddSingleton<IPlayerRepository, PlayerRepository>(sp =>
            {
                var repo = new PlayerRepository();
                repo.Seed(players.Select(p => (p.Id, p.Name)));
                return repo;
            });
    }
}
