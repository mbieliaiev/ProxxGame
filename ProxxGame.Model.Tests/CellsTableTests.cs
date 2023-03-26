using Microsoft.Extensions.Logging;
using Moq;
using ProxxGame.Contract;

namespace ProxxGame.Model.Tests
{
    public class CellsTableTests
    {
        private Mock<ILogger<CellsTable>> _loggerMoq;
        private Mock<ICellTablePrinter> _printerMoq;
        private Mock<IAdjacentCellsManager> _adjacentCellsManagerMoq;
        private Mock<IBlackHolesAllocator> _blackHolesAllocatorMoq;
        private Mock<ICellTableParametersPicker> _cellTableParametersPickerMoq;

        private CellsTable _cells;

        [SetUp]
        public void Setup()
        {
            _loggerMoq = new Mock<ILogger<CellsTable>>();
            _printerMoq = new Mock<ICellTablePrinter>();
            _adjacentCellsManagerMoq = new Mock<IAdjacentCellsManager>();
            _blackHolesAllocatorMoq = new Mock<IBlackHolesAllocator>();
            _cellTableParametersPickerMoq = new Mock<ICellTableParametersPicker>();

            _cells = new CellsTable(_printerMoq.Object, _adjacentCellsManagerMoq.Object,
                _blackHolesAllocatorMoq.Object, _cellTableParametersPickerMoq.Object, _loggerMoq.Object);
        }

        [Test]
        public void GetOpenedCellsCount_ThereAreSomeOpenedCells_OpenedCellsCountISCorrect()
        {
            var openedCell1 = new Cell(new CellCoordinates(0, 1));
            openedCell1.SetOpen();
            var openedCell2 = new Cell(new CellCoordinates(1, 0));
            openedCell2.SetOpen();
            var cellsArray = new ICell[,] {
                { new Cell(new CellCoordinates(0, 0)), openedCell1 },
                { openedCell2, new Cell(new CellCoordinates(1, 1)) }
            };
            _cells = new CellsTable(cellsArray, 2, 2);
            var openCellsCount = _cells.GetOpenedCellsCount();
            Assert.That(openCellsCount, Is.EqualTo(2));
        }
    }
}