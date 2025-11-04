using System.Collections.Concurrent;
using eWorldCup.Core.Interfaces.Repositories;
using eWorldCup.Core.Models;

namespace eWorldCup.Infrastructure.Repository;

public class PlayerRepository : IPlayerRepository
{
    internal ConcurrentDictionary<int, string> Players = new();
    internal int NextId() => Players.Keys.DefaultIfEmpty(0).Max() + 1;
    
    public void Seed(IEnumerable<(int Id, string Name)> players)
    {
        if (!Players.IsEmpty) return;
        foreach (var (id, name) in players)
        {
            Players[id] = name;
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