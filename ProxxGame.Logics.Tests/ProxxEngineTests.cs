using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ProxxGame.Contract;
using ProxxGame.Model;
using System.Linq;

namespace ProxxGame.Logics.Tests
{
    public class ProxxEngineTests
    {
        private ProxxEngine _proxxEngine;
        private ICellsTable _cellsTable;

        private Mock<ILogger<CellsTable>> _loggerMock;
        private Mock<ICellTablePrinter> _printerMock;
        private Mock<IAdjacentCellsManager> _adjacentCellsManagerMock;
        private Mock<IBlackHolesAllocator> _blackHolesAllocatorMock;
        private Mock<ICellTableParametersPicker> _cellTableParametersPickerMoq;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<CellsTable>>();
            _printerMock = new Mock<ICellTablePrinter>();
            _adjacentCellsManagerMock = new Mock<IAdjacentCellsManager>();
            _blackHolesAllocatorMock = new Mock<IBlackHolesAllocator>();
            _cellTableParametersPickerMoq = new Mock<ICellTableParametersPicker>();

            InitProxEngine();
        }

        private void InitProxEngine()
        {
            var cells = new ICell[3, 3];
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    cells[i, j] = new Cell();
                }
            }
            _cellsTable = new CellsTable(cells, 3, 3, _printerMock.Object, _adjacentCellsManagerMock.Object,
                _blackHolesAllocatorMock.Object, _cellTableParametersPickerMoq.Object, _loggerMock.Object);

            _proxxEngine = new ProxxEngine(_cellsTable);
            var blackHoles = new[] { new Cell(isBlackHole: true) };
            _proxxEngine.Initialize(3, 3, blackHoles);
        }

        [Test]
        public void MakeStep_WhenOpeningEmptyCell_ShouldOpenAdjacentEmptyCells()
        {
            // Arrange
            var emptyCell = _cellsTable.Cells[0, 0] = new Cell(new List<ICell> { new Cell(), new Cell() });

            // Check preconditions
            Assert.IsFalse(emptyCell.IsOpen);
            foreach (var adjacentCell in emptyCell.AdjacentCells)
            {
                Assert.IsFalse(adjacentCell.IsOpen);
            }

            // Act
            var result = _proxxEngine.MakeStep(0, 0);

            // Assert
            Assert.AreEqual(GameResult.Continue, result);
            Assert.IsTrue(emptyCell.IsOpen);
            foreach (var adjacentCell in emptyCell.AdjacentCells)
            {
                Assert.IsTrue(adjacentCell.IsOpen);
            }
        }

        [Test]
        public void MakeStep_WhenOpeningNonEmptyCell_ShouldOpenCellOnly()
        {
            // Arrange
            var nonEmptyCell = _cellsTable.Cells[0, 0] = new Cell(1);

            // Act
            var result = _proxxEngine.MakeStep(0, 0);

            // Assert
            Assert.AreEqual(GameResult.Continue, result);
            Assert.IsTrue(nonEmptyCell.IsOpen);
            foreach (var adjacentCell in nonEmptyCell.AdjacentCells)
            {
                Assert.IsFalse(adjacentCell.IsOpen);
            }
        }

        [Test]
        public void MakeStep_WhenOpeningBlackHole_ShouldReturnLoose()
        {
            // Arrange
            var blackHoleCell = _cellsTable.Cells[0, 0] = new Cell(isBlackHole: true);

            // Act
            var result = _proxxEngine.MakeStep(0, 0);

            // Assert
            Assert.AreEqual(GameResult.Loose, result);
            Assert.IsTrue(blackHoleCell.IsOpen);
            foreach (var cell in _cellsTable.Cells.Cast<Cell>())
            {
                if (cell.IsBlackHole)
                {
                    Assert.IsTrue(cell.IsOpen);
                }
                else
                {
                    Assert.IsFalse(cell.IsOpen);
                }
            }
        }

        [Test]
        public void MakeStep_WhenOpeningLastCell_ShouldReturnWin()
        {
            // Arrange
            for (int i = 0; i < _cellsTable.Width; i++)
            {
                for (int j = 0; j < _cellsTable.Height; j++)
                {
                    if (!_cellsTable.Cells[i, j].IsBlackHole)
                    {
                        _cellsTable.Cells[i, j] = new Cell(1);
                        _cellsTable.Cells[i, j].SetOpen();
                    }
                }
            }

            _cellsTable.Cells[2, 2] = new Cell(1);
            _cellsTable.Cells[0, 0] = new Cell(isBlackHole: true);

            // Act
            var result = _proxxEngine.MakeStep(2, 2);

            // Assert
            Assert.AreEqual(GameResult.Win, result);
            foreach (var cell in _cellsTable.Cells.Cast<Cell>())
            {
                if (!cell.IsBlackHole) { Assert.IsTrue(cell.IsOpen); }
            }
        }

        [Test]
        public void MarkAsBlackHole_WhenCellIsNotBlackHole_ShouldToggleIsMarkedAsBlackHole()
        {
            // Arrange
            var cell = _cellsTable.Cells[0, 0] = new Cell(isBlackHole: true);

            // Act
            _proxxEngine.MarkAsBlackHole(0, 0);

            // Assert
            Assert.IsTrue(cell.IsMarkedAsBlackHole);
        }
    }
}
