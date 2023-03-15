namespace ProxxGame.Contract
{
    /// <summary>
    /// This interface provides methods for generating black holes addresses and for putting black holes on the table
    /// </summary>
    public interface IBlackHolesAllocator
    {
        List<int> GenerateBlackHolesAddresses(int tableSquare, ICell[] blackHoles);

        void PutBlackHolesOnTheTable(int tableWidth, int tableHeight, List<int> blackHolesIndexes, ICell[,] cells,
            ICell[] blackHoles);
    }
}
