using eWorldCup.Core.Models.Tournaments;

namespace eWorldCup.Core.Models;

public class TwoPlayerTournament(TwoPlayerRoundRobin schedule)
{
    public long CurrentRound { get; set; } = 1;
    public TwoPlayerRoundRobin Schedule { get; init; } = schedule;

    /// <summary>
    /// n - 1 - D (återstående rundor)
    /// </summary>
    public long NumberOfRoundsLeft => Schedule.NumberOfRounds - NumberOfRoundsPlayed;
    /// <summary>
    /// (n - 1 - D) * (n / 2) (återstående par)
    /// </summary>
    public long NumberOfMatchesLeft => NumberOfRoundsLeft * Schedule.MatchesPerRound;
    public long NumberOfRoundsPlayed => CurrentRound - 1;

}
