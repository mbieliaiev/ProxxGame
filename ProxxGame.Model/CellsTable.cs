using Microsoft.Extensions.Logging;
using ProxxGame.Contract;
using ProxxGame.Utilities;

namespace ProxxGame.Model
{
    public class CellsTable : ICellsTable
    {
        private readonly ILogger<CellsTable> _logger;
        private readonly ICellTablePrinter _printer;
        private readonly IAdjacentCellsManager _adjacentCellsManager;
        private readonly IBlackHolesAllocator _blackHolesAllocator;

        public CellsTable(ICellTablePrinter printer, IAdjacentCellsManager adjacentCellsManager,
            IBlackHolesAllocator blackHolesAllocator, ILogger<CellsTable> logger) {
            _logger = logger;
            _printer = printer;
            _adjacentCellsManager = adjacentCellsManager;
            _blackHolesAllocator = blackHolesAllocator;
        }

        public CellsTable(ICell[,] cells, int height, int width, ICellTablePrinter printer = null, 
            IAdjacentCellsManager adjacentCellsManager = null, IBlackHolesAllocator blackHolesAllocator = null, 
            ILogger<CellsTable> logger = null) : this(printer, adjacentCellsManager, blackHolesAllocator, logger)
        {
            Cells = cells;
            Height = height;
            Width = width;
        }

        public ICell[,] Cells { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public ICell[] BlackHoles { get; private set; }

        public void Initialize(int width = -1, int height = -1, ICell[] blackHoles = null)
        {
            if (width < 0 || height < 0 || blackHoles == null)
            {
                InputTableParameters();
            }
            else
            {
                Width = width; 
                Height = height;
                BlackHoles = blackHoles;
            }

            var blackHolesIndexes = _blackHolesAllocator.GenerateBlackHolesAddresses(Width * Height, BlackHoles);

            _blackHolesAllocator.PutBlackHolesOnTheTable(Width, Height, blackHolesIndexes, Cells, BlackHoles);

            _adjacentCellsManager.FillAdjacentCells(Height, Width, Cells);

            _adjacentCellsManager.CalculateAdjacentCellsValuesForBlackHoles(BlackHoles);
        }

        public void Print()
        {
            _printer.Print(Cells, Width, Height);
        }

        public int GetOpenedCellsCount()
        {
            var result = 0;
            for (var i = 0; i < Height; ++i)
            {
                for (var j = 0; j < Width; ++j)
                {
                    if (Cells[i, j].IsOpen)
                    {
                        result++;
                    }
                }
            }
            return result;
        }

        private void InputTableParameters()
        {
            Width = Utility.PerformUserIntInput("Input table width:", _logger); ;
            Height = Utility.PerformUserIntInput("Input table height:", _logger);
            var blackHolesNumber = Utility.PerformUserIntInput("Input number of black holes:", _logger);

            BlackHoles = new Cell[blackHolesNumber];
            Cells = new Cell[Height, Width];
        }
    }
}