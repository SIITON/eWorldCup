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
    public TwoPlayerRoundRobin(List<Player> players) : base(players.Count)
    {
        Validate();
        _players = players;
    }

    private readonly List<Player> _players;

    public IEnumerable<Match> GetMatches(long roundNumber)
    {
        var n = NumberOfPlayers;
        EnsureRoundInRange(roundNumber);
        var rotations = (roundNumber - 1) % NumberOfRounds;

        for (var i = 0; i < MatchesPerRound; i++)
        {
            var rightPos = n - 1 - i;

            var ai = MapPos(i, rotations);
            var a = _players[ai];

            var bi = MapPos(rightPos, rotations);
            var b = _players[bi];

            yield return new Match
            {
                RoundNumber = roundNumber,
                Players = [a, b]
            };
        }

        yield break;


    }

    internal int MapPos(long pos, long rotations)
    {
        if (pos == 0) return 0;
        var idx = 1 + (pos - 1 + rotations) % NumberOfRounds;
        return (int)idx;
    }

}