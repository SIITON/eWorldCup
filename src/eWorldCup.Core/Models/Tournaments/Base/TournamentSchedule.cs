namespace eWorldCup.Core.Models.Tournaments.Base;

public abstract class TournamentSchedule(long numberOfPlayers)
{
    public long NumberOfPlayers = numberOfPlayers;
    public long NumberOfRounds => NumberOfPlayers - 1;
    public long MatchesPerRound => NumberOfPlayers / 2;

    internal void EnsureRoundInRange(long roundNumber)
    {
        if (roundNumber < 1 || roundNumber > NumberOfRounds)
        {
            throw new ArgumentOutOfRangeException(nameof(roundNumber),
                $"Round must be in 1..{NumberOfRounds}.");
        }
    }

    internal bool Validate()
    {
        ThrowIfUneven(NumberOfPlayers);
        if (NumberOfPlayers < 2)
        {
            throw new ArgumentException("At least two players are required to schedule matches.");
        }
        return true;
    }

    internal static bool ThrowIfUneven(long number) => number % 2 != 0
        ? throw new ArgumentException("Number of players must be even.")
        : true;
}