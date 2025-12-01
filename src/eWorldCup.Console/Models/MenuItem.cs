using eWorldCup.ConsoleBackdoor.Extensions;

namespace eWorldCup.ConsoleBackdoor.Models;

public record MenuItem(int Id, string Text, Func<Task<bool>> OnClick)
{
    public bool WasClicked(ConsoleKey key)
    {
        return key.ToInteger() == Id;
    }
}