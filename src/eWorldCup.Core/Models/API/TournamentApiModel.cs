namespace eWorldCup.Core.Models.API;

public class TournamentApiModel
{
    public long NumberOfRounds { get; init; }

    /// <summary>
    /// n - 1 - d (återstående rundor)
    /// </summary>
    public long NumberOfRoundsLeft { get; init; }

    /// <summary>
    /// Återstående par.
    /// number of rounds left * number of players)
    /// </summary>
    public long NumberOfMatchesLeft { get; init; }
}
