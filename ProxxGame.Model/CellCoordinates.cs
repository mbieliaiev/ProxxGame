using ProxxGame.Contract;

namespace ProxxGame.Model
{
    public class CellCoordinates : ICellCoordinates
    {
        public CellCoordinates(int rowIndex, int columnIndex)
        {
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
        }

        public int RowIndex { get; private set; }

        public int ColumnIndex { get; private set; }
    }
}
