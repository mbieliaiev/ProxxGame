namespace ProxxGame.Contract
{
    /// <summary>
    /// This interface provides methods for creating and filling adjacent cells on the table
    /// </summary>
    public interface IAdjacentCellsManager
    {
        void FillAdjacentCells(int height, int width, ICell[,] cells);

        void CalculateAdjacentCellsValuesForBlackHoles(ICell[] blackHoles);
    }
}
