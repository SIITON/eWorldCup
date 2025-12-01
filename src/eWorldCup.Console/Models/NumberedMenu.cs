using System.Text;

namespace eWorldCup.ConsoleBackdoor.Models;

public class NumberedMenu
{
    public string Header { get; set; }
    public List<MenuItem> Items { get; set; }

    public NumberedMenu(string header, params MenuItem[] menuItems)
    {
        Header = header;
        Items = menuItems.ToList();
    }

    public NumberedMenu(List<MenuItem> menuItems)
    {
        Items = menuItems;
    }

    public void Display(bool shouldClear = true)
    {
        if (shouldClear) Console.Clear();
        Console.WriteLine($" {Header:yellow} \n ");
        var sb = new StringBuilder();
        foreach (var menuItem in Items)
        {
            sb.Append(' ');
            sb.Append(menuItem.Id).Append(" - ").Append(menuItem.Text);
            sb.Append('\n');
        }


        sb.Append("\n Select number...");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(sb.ToString());
        Console.ForegroundColor = ConsoleColor.White;
    }

    public MenuItem GetUserSelectedChoice()
    {
        var key = System.Console.ReadKey(true);

        return Items.FirstOrDefault(menuItem => menuItem.WasClicked(key.Key)) ?? TryAgain();
    }

    public MenuItem TryAgain()
    {
        System.Console.WriteLine("Input must be a number from the menu");
        return GetUserSelectedChoice();
    }

    /// <summary>
    /// Gets the user selected menu item and executes its on-click function.
    /// </summary>
    /// <returns>true if the user want to continue, otherwise false.</returns>
    public async Task<bool> GotoUserSelection()
    {
        var selectedMenu = GetUserSelectedChoice();
        System.Console.Clear();
        return await selectedMenu.OnClick.Invoke();
    }
}