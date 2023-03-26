using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProxxGame.Contract;
using ProxxGame.Logics;
using ProxxGame.Model;

namespace ProxxGame
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            var serviceProvider = serviceCollection
                .AddLogging(cfg => cfg.AddConsole())
                .AddTransient<Program>()
                .AddSingleton<IAdjacentCellsManager, AdjacentCellsManager>()
                .AddSingleton<IBlackHolesAllocator, BlackHolesAllocator>()
                .AddSingleton<ICommunicationChannel, ConsoleCommunicationChannel>()
                .AddSingleton<ICellTablePrinter, CellsTableConsolePrinter>()
                .AddSingleton<ICellTableParametersPicker, CellTableParametersPicker>()
                .AddSingleton<ICellsTable, CellsTable>()
                .AddSingleton<IProxxEngine, ProxxEngine>()
                .AddSingleton<IProxxGame, Logics.ProxxGame>()
                .Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Information)
                .BuildServiceProvider();

            var game = serviceProvider.GetService<IProxxGame>();
            game.Start();

            Console.ReadLine();
        }
    }
}