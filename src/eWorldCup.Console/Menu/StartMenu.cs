using eWorldCup.ConsoleBackdoor.Models;
using eWorldCup.ConsoleBackdoor.RockPaperArenaAdapter;
using eWorldCup.Core.Interfaces.Repositories;

namespace eWorldCup.ConsoleBackdoor.Menu;

public class StartMenu(IRockPaperArenaService rockPaper, IPlayerRepository players) : Base.Menu
{
    public UserAction Next()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Run()
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
            shouldContinue = await menu.GotoUserSelection();
        }

        return false;
    }

    public bool DoNothing() => true;

    public async Task<bool> ShowRockPaper()
    {
        Console.WriteLine("starting rock paper scissors tournament arena ...");
        await rockPaper.RunAsync();
        return true;
    }

    public async Task<bool> ShowPlayers()
    {
        
        return true;
    }

    public async Task<bool> ShowStatistics()
    {
        //rockPaper.Statistics();

        return true;
    }
}