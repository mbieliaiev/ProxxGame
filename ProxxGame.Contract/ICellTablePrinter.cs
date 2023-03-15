namespace ProxxGame.Contract
{
    /// <summary>
    /// This is a generalized interface, providing methods for printing cells table either to Console, 
    /// or to any other place depending on a concrete class implementation
    /// </summary>
    public interface ICellTablePrinter
    {
        void Print(ICell[,] cells, int width, int height);
    }
}
