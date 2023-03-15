using Microsoft.Extensions.Logging;
using ProxxGame.Contract;
using ProxxGame.Utilities;

namespace ProxxGame.Logics
{
    public class ProxxGame : IProxxGame
    {
        private readonly ILogger<ProxxGame> _logger;
        private IProxxEngine _engine;
        public ProxxGame(IProxxEngine engine, ILogger<ProxxGame> logger) {
            _engine = engine;
            _logger = logger;
        }
        public void Start()
        {
            try
            {
                _engine.Initialize();

                GameResult stepResult;
                while (true)
                {
                    Console.WriteLine("\n\nIf you want to mark a cell as a black hole, type 'b' otherwise - press any key to continue:");
                    var markMineCommand = Console.ReadLine();
                    if (markMineCommand == "b") {
                        var brow = Utility.PerformUserIntInput("\n\nInput the X coordinate of a cell to mark as a black hole:", _logger);
                        var bcolumn = Utility.PerformUserIntInput("Input the Y coordinate of a cell to mark as a black hole:", _logger);
                        _engine.MarkAsBlackHole(brow, bcolumn);
                    }
                    else
                    {
                        var row = Utility.PerformUserIntInput("\n\nInput the X coordinate of a cell to open:", _logger);
                        var column = Utility.PerformUserIntInput("Input the Y coordinate of a cell to open:", _logger);
                        stepResult = _engine.MakeStep(row, column);
                        if (stepResult == GameResult.Win || stepResult == GameResult.Loose)
                        {
                            break;
                        }
                    }
                }
                _logger.LogInformation("Game was successfully finished.");
                if (stepResult == GameResult.Win)
                {
                    Console.WriteLine("Congratulations! You have won!");
                }
                else
                {
                    Console.WriteLine("You have loose, don't worry, you may try again :)");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Unexpected error has happened: {e.Message}");
                throw;
            }
        }
    }
}
