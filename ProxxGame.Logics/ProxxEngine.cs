using ProxxGame.Contract;
using ProxxGame.Model;

namespace ProxxGame.Logics
{
    public class ProxxEngine : IProxxEngine
    {
        private ICellsTable _cellsTable;

        public ProxxEngine(ICellsTable cellsTable)
        {
            _cellsTable = cellsTable;
        }

        public void Initialize(int width = -1, int height = -1, ICell[] blackHoles = null)
        {
            _cellsTable.Initialize(width, height, blackHoles);
            _cellsTable.Print();
        }

        public GameResult MakeStep(int row, int column)
        {
            var openedCell = _cellsTable.Cells[row, column];
            if (openedCell.IsBlackHole) {
                openedCell.SetOpen();
                _cellsTable.Print();
                return GameResult.Loose;
            }
            if (openedCell.IsOpen)
            {
                openedCell.SetOpen();
                _cellsTable.Print();
                return GameResult.Continue;
            }
            if (openedCell.Value > 0)
            {
                openedCell.SetOpen();
            }
            if (openedCell.Value == 0)
            {
                OpenAdjacentEmptyCells(openedCell);
            }
            var numberOpenedCells = _cellsTable.GetOpenedCellsCount();
            if (numberOpenedCells + _cellsTable.BlackHoles.Length == _cellsTable.Width * _cellsTable.Height) {
                openedCell.SetOpen();
                _cellsTable.Print();
                return GameResult.Win; ;
            }
            _cellsTable.Print();
            return GameResult.Continue;
        }

        public void MarkAsBlackHole(int row, int column)
        {
            _cellsTable.Cells[row, column].ToggleMarkAsBlackHole();
            _cellsTable.Print();
        }

        private void OpenAdjacentEmptyCells(ICell cell)
        {
            cell.SetOpen();
            foreach (var adjacentCell in cell.AdjacentCells)
            {
                if (adjacentCell.Value == 0 && !adjacentCell.IsOpen)
                {                    
                    OpenAdjacentEmptyCells(adjacentCell);
                }
                else if (adjacentCell.Value > 0)
                {
                    adjacentCell.SetOpen();
                }
            }
        }
    }
}