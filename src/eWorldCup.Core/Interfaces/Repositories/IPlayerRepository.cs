using eWorldCup.Core.Models;

namespace eWorldCup.Core.Interfaces.Repositories;

public interface IPlayerRepository
{
    /// <summary>
    /// Gets a player by ID.
    /// </summary>
    /// <exception cref="KeyNotFoundException"></exception>
    /// <param name="id"></param>
    /// <returns></returns>
    Player Get(int id);

    /// <summary>
    /// Gets all players.
    /// </summary>
    /// <returns></returns>
    IEnumerable<Player> GetAll();

    /// <summary>
    /// Adds a new player.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    Player Add(Player player);

    /// <summary>
    /// Deletes a player by ID.
    /// </summary>
    /// <exception cref="KeyNotFoundException"></exception>
    /// <param name="id"></param>
    void Remove(int id);
}