namespace eWorldCup.ConsoleBackdoor.Menu.Base;

public abstract class Menu
{
    internal static bool PressAnyKeyToReturn
    {
        get
        {
            Console.WriteLine("\n Click any key to return");
            ReadKey();
            return true;
        }
    }

    internal static ConsoleKeyInfo ReadKey() => Console.ReadKey(true);

    internal bool Return() => false;
}