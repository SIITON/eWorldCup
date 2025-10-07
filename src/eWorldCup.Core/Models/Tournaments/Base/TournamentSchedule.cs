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
}