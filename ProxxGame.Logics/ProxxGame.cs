using Microsoft.Extensions.Logging;
using ProxxGame.Contract;

namespace ProxxGame.Logics
{
    public class ProxxGame : IProxxGame
    {
        private readonly ILogger<ProxxGame> _logger;
        private readonly IProxxEngine _engine;
        private readonly ICommunicationChannel _communicationChannel;

        public ProxxGame(IProxxEngine engine, ICommunicationChannel communicationChannel, 
            ILogger<ProxxGame> logger) {
            _engine = engine;
            _communicationChannel = communicationChannel;
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
                    var markMineCommand = 
                        _communicationChannel.ReadWithMessage("\n\nIf you want to mark a cell as a black hole, type 'b' otherwise - press any key to continue:");
                    if (markMineCommand == "b") {
                        var brow = _communicationChannel.ReadIntWithMessage("\n\nInput the X coordinate of a cell to mark as a black hole:");
                        var bcolumn = _communicationChannel.ReadIntWithMessage("Input the Y coordinate of a cell to mark as a black hole:");
                        _engine.MarkAsBlackHole(brow, bcolumn);
                    }
                    else
                    {
                        var row = _communicationChannel.ReadIntWithMessage("\n\nInput the X coordinate of a cell to open:");
                        var column = _communicationChannel.ReadIntWithMessage("Input the Y coordinate of a cell to open:");
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
                    _communicationChannel.ShowMessage("Congratulations! You have won!");
                }
                else
                {
                    _communicationChannel.ShowMessage("You have loose, don't worry, you may try again :)");
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
