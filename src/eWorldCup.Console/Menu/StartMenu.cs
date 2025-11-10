using eWorldCup.ConsoleBackdoor.Models;
using eWorldCup.Core.Interfaces.Repositories;
using eWorldCup.Core.Interfaces.Services;

namespace eWorldCup.ConsoleBackdoor.Menu;

public class StartMenu(IRockPaperArenaService rockPaper, IPlayerRepository players) : Base.Menu
{
    public UserAction Next()
    {
        throw new NotImplementedException();
    }

    public bool Run()
    {
        var shouldContinue = true;
        while (shouldContinue)
        {
            var menu = new NumberedMenu("START MENU",
                new MenuItem(1, "Play Rock paper scissors", OnClick: ShowRockPaper),
                new MenuItem(2, "Players", ShowPlayers),
                new MenuItem(3, "Flying simulator", ShowStatistics)
            );
            menu.Display();
            shouldContinue = menu.GotoUserSelection();
        }

        return false;
    }

    public bool DoNothing() => true;

    public bool ShowRockPaper()
    {
        Console.WriteLine("starting rock paper scissors tournament arena ...");
        rockPaper.Start();
        return true;
    }

    public bool ShowPlayers()
    {
        
        return true;
    }

    public bool ShowStatistics()
    {
        rockPaper.Statistics();

        return true;
    }
}