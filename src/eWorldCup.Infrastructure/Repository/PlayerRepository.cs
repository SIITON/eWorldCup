using System.Collections.Concurrent;
using System.Text.Json;
using eWorldCup.Core.Interfaces.Repositories;
using eWorldCup.Core.Models;

namespace eWorldCup.Infrastructure.Repository;

public class PlayerRepository : IPlayerRepository
{
    internal record PlayerSeed(int Id, string Name);

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

    internal ConcurrentDictionary<int, string> Players = new();
    internal int NextId() => Players.Keys.DefaultIfEmpty(0).Max() + 1;

    public PlayerRepository()
    {
        // seed initial players
        var players = JsonSerializer.Deserialize<List<PlayerSeed>>(Seed) ?? throw new NullReferenceException();
        foreach (var player in players)
        {
            Players[player.Id] = player.Name;
        }
    }

    public Player Get(int id)
    {
        if (Players.TryGetValue(id, out var name))
        {
            return new Player(id, name);
        }
        throw new KeyNotFoundException($"Player with ID {id} not found.");
    }

    public IEnumerable<Player> GetAll()
    {
        return Players.Select(kv => new Player(kv.Key, kv.Value));
    }

    public Player Add(Player player)
    {
        var id = NextId();
        if (!Players.TryAdd(id, player.Name))
        {
            throw new ArgumentException($"Player with ID {id} already exists.");
        }

        return Get(id);
    }

    public void Remove(int id)
    {
        if (!Players.TryRemove(id, out _))
        {
            throw new KeyNotFoundException($"Player with ID {id} not found.");
        }
    }
}