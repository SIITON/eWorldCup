using eWorldCup.Core.Models.Tournaments.Base;

namespace eWorldCup.Core.Models.Tournaments;

/*
10 players

d = 1  | d = 2  | d = 3  | d = 4  | d = 5  | d = 6  | d = 7  | d = 8  | d = 9  |

0 - 9  | 0 - 1  | 0 - 2  | 0 - 3  | 0 - 4  | 0 - 5  | 0 - 6  | 0 - 7  | 0 - 8  |
1 - 8  | 2 - 9  | 3 - 1  | 4 - 2  | 5 - 3  | 6 - 4  | 7 - 5  | 8 - 6  | 9 - 7  |
2 - 7  | 3 - 8  | 4 - 9  | 5 - 1  | 6 - 2  | 7 - 3  | 8 - 4  | 9 - 5  | 1 - 6  |
3 - 6  | 4 - 7  | 5 - 8  | 6 - 9  | 7 - 1  | 8 - 2  | 9 - 3  | 1 - 4  | 2 - 5  |
4 - 5  | 5 - 6  | 6 - 7  | 7 - 8  | 8 - 9  | 9 - 1  | 1 - 2  | 2 - 3  | 3 - 4  |

 */
public class TwoPlayerRoundRobin : TournamentSchedule
{
    public TwoPlayerRoundRobin(long numberOfPlayers) : base(numberOfPlayers)
    {
        Validate();
    }

    public IEnumerable<Match> GetMatches(long roundNumber)
    {
        var n = NumberOfPlayers;
        EnsureRoundInRange(roundNumber);
        var rotations = (roundNumber - 1) % NumberOfRounds;

        for (var i = 0; i < MatchesPerRound; i++)
        {
            var rightPos = n - 1 - i;

            var a = MapPos(i, rotations);

            var b = MapPos(rightPos, rotations);

            yield return new Match
            {
                RoundNumber = roundNumber,
                PlayerIds = [a + 1, b + 1]
            };
        }
    }

    public IEnumerable<Match> GetMatches(int playerId)
    {
        var n = NumberOfPlayers;
        var playerIdx = playerId - 1;
        for (var round = 1; round <= NumberOfRounds; round++)
        {
            EnsureRoundInRange(round);
            var rotations = (round - 1) % NumberOfRounds;

            for (var i = 0; i < MatchesPerRound; i++)
            {
                var rightPos = n - 1 - i;

                var a = MapPos(i, rotations);

                var b = MapPos(rightPos, rotations);
                if (a == playerIdx || b == playerIdx)
                {
                    yield return new Match
                    {
                        RoundNumber = round,
                        PlayerIds = [a + 1, b + 1]
                    };
                }
            }
        }
    }

    public IEnumerable<long> GetMatch(long roundNumber, long playerIndex)
    {
        EnsureRoundInRange(roundNumber);
        var rotations = (roundNumber - 1) % NumberOfRounds;
        var rightPos = NumberOfPlayers - 1 - playerIndex;
        var a1 = MapPos(playerIndex, rotations);
        var a2 = MapPos(rightPos, rotations);
        
        return [a1, a2];
    }

    internal long MapPos(long pos, long rotations)
    {
        if (pos == 0) return 0;
        var idx = 1 + (pos - 1 + rotations) % NumberOfRounds;
        return idx;
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