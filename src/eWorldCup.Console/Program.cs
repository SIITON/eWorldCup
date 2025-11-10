using eWorldCup.Application;
using eWorldCup.ConsoleBackdoor.Menu;
using eWorldCup.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddApplication()
    .AddInfrastructure()
    .AddSingleton<StartMenu>()
    .BuildServiceProvider();

var menu = serviceProvider.GetRequiredService<StartMenu>();
