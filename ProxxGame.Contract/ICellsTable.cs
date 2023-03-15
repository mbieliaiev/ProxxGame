namespace ProxxGame.Contract
{
    public interface ICellsTable
    {
        ICell[,] Cells { get; }

        int Width { get; }

        int Height { get; }

        ICell[] BlackHoles { get; }

        void Initialize(int width = -1, int height = -1, ICell[] blackHoles = null);

        void Print();

        int GetOpenedCellsCount();
    }
}
