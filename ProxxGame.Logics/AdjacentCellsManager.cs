using ProxxGame.Contract;

namespace ProxxGame.Logics
{
    public class AdjacentCellsManager : IAdjacentCellsManager
    {
        public void CalculateAdjacentCellsValuesForBlackHoles(ICell[] blackHoles)
        {
            foreach (var blackHole in blackHoles)
            {
                foreach (var blackHoleAdjacent in blackHole.AdjacentCells)
                {
                    blackHoleAdjacent.IncrementNonHoleValue();
                }
            }
        }

        public void FillAdjacentCells(int height, int width, ICell[,] cells)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var adjacentCells = cells[i, j].AdjacentCells;

                    AddAdjacentCell(i - 1, j - 1, adjacentCells, height, width, cells);
                    AddAdjacentCell(i - 1, j, adjacentCells, height, width, cells);
                    AddAdjacentCell(i - 1, j + 1, adjacentCells, height, width, cells);
                    AddAdjacentCell(i, j - 1, adjacentCells, height, width, cells);
                    AddAdjacentCell(i, j + 1, adjacentCells, height, width, cells);
                    AddAdjacentCell(i + 1, j - 1, adjacentCells, height, width, cells);
                    AddAdjacentCell(i + 1, j, adjacentCells, height, width, cells);
                    AddAdjacentCell(i + 1, j + 1, adjacentCells, height, width, cells);
                }
            }
        }

        private void AddAdjacentCell(int row, int column, List<ICell> adjacentCells, 
            int height, int width, ICell[,] allCells)
        {
            if (row < 0 || row >= height)
            {
                return;
            }

            if (column < 0 || column >= width)
            {
                return;
            }

            adjacentCells.Add(allCells[row, column]);
        }
    }
}
