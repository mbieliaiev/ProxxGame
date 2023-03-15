using ProxxGame.Contract;

namespace ProxxGame.Model
{
    public class Cell : ICell
    {
        public Cell(int value = 0, bool isBlackHole = false) {
            Value = value;
            IsBlackHole = isBlackHole;
        }

        public Cell(List<ICell> adjacentCells) {
            AdjacentCells = adjacentCells;
        }

        public Cell(ICellCoordinates coordinates) : this(0, coordinates, false) { }

        public Cell(int value, ICellCoordinates coordinates, bool isBlackHole)
        {
            Value = isBlackHole? -1 : value;
            Coordinates = coordinates;
            IsBlackHole = isBlackHole;
        }

        public void SetOpen()
        {
            IsOpen = true;
        }

        public void IncrementNonHoleValue()
        {
            if (IsBlackHole) {
                return;            
            }
            Value++;
        }

        public void ToggleMarkAsBlackHole()
        {
            IsMarkedAsBlackHole = !IsMarkedAsBlackHole;
        }

        public int Value { get; private set; }

        public bool IsOpen { get; private set; }

        public ICellCoordinates Coordinates { get; private set; }

        public bool IsBlackHole { get; private set; }

        public List<ICell> AdjacentCells { get; private set; } = new List<ICell>();

        public bool IsMarkedAsBlackHole { get; private set; }
}
}