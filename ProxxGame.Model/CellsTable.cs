using Microsoft.Extensions.Logging;
using ProxxGame.Contract;

namespace ProxxGame.Model
{
    public class CellsTable : ICellsTable
    {
        private readonly ILogger<CellsTable> _logger;
        private readonly ICellTablePrinter _printer;
        private readonly IAdjacentCellsManager _adjacentCellsManager;
        private readonly IBlackHolesAllocator _blackHolesAllocator;
        private readonly ICellTableParametersPicker _cellTableParametersPicker;

        public CellsTable(ICellTablePrinter printer, IAdjacentCellsManager adjacentCellsManager,
            IBlackHolesAllocator blackHolesAllocator, ICellTableParametersPicker cellTableParametersPicker,
            ILogger<CellsTable> logger) {
            _logger = logger;
            _printer = printer;
            _adjacentCellsManager = adjacentCellsManager;
            _blackHolesAllocator = blackHolesAllocator;
            _cellTableParametersPicker = cellTableParametersPicker;
        }

        public CellsTable(ICell[,] cells, int height, int width, ICellTablePrinter printer = null, 
            IAdjacentCellsManager adjacentCellsManager = null, IBlackHolesAllocator blackHolesAllocator = null, 
            ICellTableParametersPicker cellTableParametersPicker = null, ILogger<CellsTable> logger = null) 
            : this(printer, adjacentCellsManager, blackHolesAllocator, cellTableParametersPicker, logger)
        {
            Cells = cells;
            Height = height;
            Width = width;
        }

        public ICell[,] Cells { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public int BlackHolesNumber { get; private set; }

        public ICell[] BlackHoles { get; private set; }

        public void Initialize(int width = -1, int height = -1, ICell[] blackHoles = null)
        {
            if (width < 0 || height < 0 || blackHoles == null)
            {
                PickTableParameters();

                BlackHoles = new Cell[BlackHolesNumber];
                Cells = new Cell[Height, Width];
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

        private void PickTableParameters()
        {
            int width, height, blackHolesNumber;
            _cellTableParametersPicker.PickParameters(out width, out height, out blackHolesNumber);
            Width = width; 
            Height = height;
            BlackHolesNumber = blackHolesNumber;

            _logger.LogInformation($"Cells table with width = {width}, height = {height}, {blackHolesNumber} black holes will be crearted");
        }
    }
}