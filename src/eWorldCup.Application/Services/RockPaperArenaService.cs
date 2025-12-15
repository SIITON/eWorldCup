using eWorldCup.Core.Interfaces.Repositories;
using eWorldCup.Core.Models;
using eWorldCup.Core.Models.Games.RockPaperArena;
using eWorldCup.Core.Models.Games.RockPaperArena.Exceptions;

namespace eWorldCup.Application.Services;

public class RockPaperArenaService(IPlayerRepository playerRepository, 
    ITournamentRepository tournamentRepository) : IRockPaperArenaService
{
    public RockPaperArenaTournament Start(string playerName, int numberOfPlayers)
    {
        var user = playerRepository.Add(new Player(0, playerName));
        var players = GetTournamentOpponents(numberOfPlayers, user);
        var tournament = RockPaperArenaTournament.CreateWithUserAndOpponents(user, players, settings =>
        {
            settings.MaximumRoundsInAMatch = 3;
            settings.PointsForWinningMatch = 3;
            settings.PointsForDrawingMatch = 1;
            settings.PointsForLosingMatch = 0;
        });
        tournament.CurrentMatch = tournament.GetUserMatch();
        tournamentRepository.Add(tournament);
        
        return tournament;
    }

    public RoundResults PlayRound(Guid tournamentId, HandShape playerMove)
    {
        var tournament = tournamentRepository.Get(tournamentId);
        var match = tournament.GetUserMatch();
        if (match.IsOver(tournament.Settings.MaximumRoundsInAMatch))
        {
            throw new MatchOverException();
        }
        var playerHand = new Hand().Show(playerMove);
        var opponentHand = new Hand().Randomize();
        var results = playerHand.Versus(opponentHand);
        match.UpdateScore(results);
        var matchIsOver = match.IsOver(tournament.Settings.MaximumRoundsInAMatch);
        // store the results
        tournament.CurrentMatch = match;
        Player? winningPlayer = null;
        if (matchIsOver)
        {
            tournament.RegisterFinishedMatch(match);
            winningPlayer = tournament.GetParticipantByIndex(match.GetWinnerIndex());
        }
        tournamentRepository.Update(tournament);
        
        return new RoundResults
        {
            CurrentRound = (int)match.RoundNumber,
            NumberOfRoundsPlayed = match.NumberOfRoundsPlayed,
            IsMatchOver = matchIsOver,
            PlayerMove = playerHand.Shape,
            OpponentMove = opponentHand.Shape,
            IsDraw = results.IsDraw,
            IsPlayerWin = results.PlayerOneWins,
            IsOpponentWin = results.PlayerTwoWins,
            Score = new RoundScore()
            {
                Player = match.Score.Player,
                Opponent = match.Score.Opponent
            },
            Winner = winningPlayer,
        };
    }

    internal List<Player> GetTournamentOpponents(int numberOfPlayers, Player user)
    {
        var opponents = playerRepository.GetAll()
            .Where(p => p.Id != user.Id)
            .Take(numberOfPlayers - 1) // Since the user is one player
            .ToList();

        return opponents;
    }
}

public interface IRockPaperArenaService
{
    RockPaperArenaTournament Start(string playerName, int numberOfPlayers);
    RoundResults PlayRound(Guid tournamentId, HandShape playerMove);
}