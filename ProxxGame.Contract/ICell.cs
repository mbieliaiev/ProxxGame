namespace ProxxGame.Contract;
public interface ICell
{
    int Value { get; }

    ICellCoordinates Coordinates { get; }

    bool IsBlackHole { get; }

    bool IsOpen { get; }

    bool IsMarkedAsBlackHole { get; }

    void ToggleMarkAsBlackHole();

    void SetOpen();

    void IncrementNonHoleValue();

    void OpenAdjacentEmptyCells();

    /// <summary>
    /// Storing adjacent cells for each cell on the table allows us to easily calculate numbers of 
    /// black holes and easily open all the adjacent empty cells when clicking on an empty cell
    /// </summary>
    List<ICell> AdjacentCells { get; }
}
