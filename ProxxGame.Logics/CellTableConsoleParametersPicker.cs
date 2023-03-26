using Microsoft.Extensions.Logging;
using ProxxGame.Contract;
using ProxxGame.Model;
using ProxxGame.Utilities;

namespace ProxxGame.Logics
{
    public class CellTableConsoleParametersPicker : ICellTableParametersPicker
    {
        ILogger<CellsTable> _logger;
        public CellTableConsoleParametersPicker(ILogger<CellsTable> logger) {
            _logger = logger;
        }
        public void PickParameters(out int width, out int height, out int numberBlackHoles)
        {
            width = Utility.PerformUserIntInput("Input table width:", _logger); ; 
            height = Utility.PerformUserIntInput("Input table height:", _logger); ; 
            numberBlackHoles = Utility.PerformUserIntInput("Input number of black holes:", _logger); ;
        }
    }
}
