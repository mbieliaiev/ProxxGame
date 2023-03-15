using ProxxGame.Contract;
using ProxxGame.Model;

namespace ProxxGame.Logics
{
    public class BlackHolesAllocator : IBlackHolesAllocator
    {
        public List<int> GenerateBlackHolesAddresses(int tableSquare, ICell[] blackHoles)
        {
            var random = new Random();
            var blackHolesIndexes = new List<int>();
            for (int i = 0; i < blackHoles.Length; i++)
            {
                var randNumber = random.Next(i, tableSquare);
                while (blackHolesIndexes.Contains(randNumber))
                {
                    randNumber = random.Next(i, tableSquare);
                }
                blackHolesIndexes.Add(randNumber);
            }

            return blackHolesIndexes;
        }

        public void PutBlackHolesOnTheTable(int tableWidth, int tableHeight, List<int> blackHolesIndexes, ICell[,] cellsTable,
            ICell[] blackHoles)
        {
            var tableSquare = tableWidth * tableHeight;
            var blackHolesIndexesStack = new Stack<int>(blackHolesIndexes.OrderBy(x => x));
            var cellIndex = tableSquare - 1;
            var blackHoleIndex = 0;

            for (int i = 0; i < tableHeight; i++)
            {
                for (int j = 0; j < tableWidth; j++)
                {
                    if (blackHolesIndexesStack.Count > 0 && cellIndex == blackHolesIndexesStack.Peek())
                    {
                        cellsTable[i, j] = new Cell(-1, new CellCoordinates(i, j), true);
                        blackHoles[blackHoleIndex] = cellsTable[i, j];
                        blackHoleIndex++;
                        blackHolesIndexesStack.Pop();
                    }
                    else
                    {
                        cellsTable[i, j] = new Cell(new CellCoordinates(i, j));
                    }

                    cellIndex--;
                }
            }
        }
    }
}
