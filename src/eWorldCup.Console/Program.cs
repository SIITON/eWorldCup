using eWorldCup.Application;
using eWorldCup.ConsoleBackdoor.Menu;
using eWorldCup.ConsoleBackdoor.RockPaperArenaAdapter;
using eWorldCup.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddApplication()
    .AddInfrastructure()
    .AddSingleton<StartMenu>()
    .AddScoped<IRockPaperArenaService, RockPaperArenaConsole>()
    .BuildServiceProvider();

var menu = serviceProvider.GetRequiredService<StartMenu>();

await menu.Run();
