using eWorldCup.Application.Features.RockPaperArena;
using eWorldCup.Core.Models.API.Input;
using eWorldCup.Core.Models.Games.RockPaperArena;

namespace eWorldCup.ConsoleBackdoor.RockPaperArenaAdapter;

public static class ConsoleInput
{
    public static string GetPlayerName()
    {
        Console.Write("Enter player name: ");
        var name = Console.ReadLine() ?? "Player";
        var hellos = new[] { "Hello", "Hi", "hey", "God day", "Oh shit\n It's you", "is that really your name?" };
        var rnd = new Random(Guid.NewGuid().GetHashCode());
        Console.WriteLine($"{hellos[rnd.Next(0, hellos.Length)]} {name}");
        return name;
    }

    public static int GetPlayerCount(int defaultPlayerCount = 10)
    {
        var tries = 0;
        while (true)
        {
            Console.Write("Enter number of opponents: ");
            if (int.TryParse(Console.ReadLine(), out var count) || count >= 1) return count;
            Console.WriteLine("Enter a valid integer");
            tries++;
            if (tries <= 3) continue;
            Console.Write($"Alright, I'm just gonna put down {defaultPlayerCount}");
            return defaultPlayerCount;
        }
    }

    public static PlayerMoveInput GetPlayerMoveInput()
    {
        Console.WriteLine("It's your turn.\n Do you choose rock, paper or scissor?");
        var tries = 0;
        while (true)
        {
            var moveInput = Console.ReadLine() ?? string.Empty;
            if (!Enum.TryParse<HandShape>(moveInput, out var hand))
            {
                hand = moveInput.ToLowerInvariant() switch
                {
                    "rock" => HandShape.Rock,
                    "r" => HandShape.Rock,
                    "paper" => HandShape.Paper,
                    "p" => HandShape.Paper,
                    "scissors" => HandShape.Scissors,
                    "s" => HandShape.Scissors,
                    _ => HandShape.Undecided
                };
            }

            if (hand != HandShape.Undecided)
            {
                Console.WriteLine($"Smart move going with {hand.ToString()}");
                return new PlayerMoveInput()
                {
                    ChosenMove = hand.ToString()
                };
            }
            tries++;
            if (tries <= 3) continue;
            var randomHand = new Hand().Randomize();
            Console.Write($"Alright, I'm just gonna randomize something, let's say {randomHand.Shape}");
            return new PlayerMoveInput()
            {
                ChosenMove = nameof(randomHand.Shape)
            };
        }
    }
}