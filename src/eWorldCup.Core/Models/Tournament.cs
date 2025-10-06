using eWorldCup.Core.Models.Tournaments.Base;

namespace eWorldCup.Core.Models;

public class Tournament(TournamentSchedule schedule)
{
    public long CurrentRound { get; init; }
    public TournamentSchedule Schedule { get; init; } = schedule;

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
// D = completed rounds = d - 1

/*
IN
n = 10 players
UT
Max antal rundor 
= n - 1 = 9

IN
n = 10 players
D = 3 completed rounds
UT
Återstående par = återstående matcher 
= Återstående rundor * matcher per runda 
= (n - 1 - D) * (n / 2) 
= (10 - 1 - 3) * (10 / 2) 
= 6 * 5 = 30

 Återstående rundor 
 = Max antal rundor - spelade rundor 
 = n - 1 - D = 6
 Matcher per runda = n / 2 = 10 / 2 = 5

IN
n = 10 players
d = 2 specific round
i = 3 player index
Direktfråga = vem möter spelare i i runda d?
UT
4 vs 7
 */