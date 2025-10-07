using eWorldCup.Core.Models;

namespace eWorldCup.Core.Interfaces.Repositories;

public interface IPlayerRepository
{
    Player Get(int id);
    IEnumerable<Player> GetAll();
    void Add(Player player);
    void Remove(int id);
}