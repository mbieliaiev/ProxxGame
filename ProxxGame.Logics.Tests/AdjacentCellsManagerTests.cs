using ProxxGame.Contract;
using ProxxGame.Model;

namespace ProxxGame.Logics.Tests
{
    [TestFixture]
    public class AdjacentCellsManagerTests
    {
        private IAdjacentCellsManager _adjacentCellsManager;

        [SetUp]
        public void Setup()
        {
            _adjacentCellsManager = new AdjacentCellsManager();
        }

        [Test]
        public void CalculateAdjacentCellsValuesForBlackHoles_ShouldIncrementNonHoleValue_ForAdjacentCells()
        {
            // Arrange
            var adjacentCell1 = new Cell();
            var adjacentCell2 = new Cell();

            var blackHole1AdjacentCells = new List<ICell> { adjacentCell1 };
            var blackHole2AdjacentCells = new List<ICell> { adjacentCell1, adjacentCell2 };

            var blackHole1 = new Cell(blackHole1AdjacentCells);
            var blackHole2 = new Cell(blackHole2AdjacentCells);

            // Act
            _adjacentCellsManager.CalculateAdjacentCellsValuesForBlackHoles(new[] { blackHole1, blackHole2 });

            // Assert
            Assert.AreEqual(2, adjacentCell1.Value);
            Assert.AreEqual(1, adjacentCell2.Value);
        }

        [Test]
        public void FillAdjacentCells_ShouldAddAdjacentCells_ToEachCell()
        {
            // Arrange
            var cells = new ICell[3, 3];
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    cells[i, j] = new Cell();
                }
            }

            // Act
            _adjacentCellsManager.FillAdjacentCells(3, 3, cells);

            // Assert
            Assert.AreEqual(3, cells[0, 0].AdjacentCells.Count);
            Assert.AreEqual(5, cells[0, 1].AdjacentCells.Count);
            Assert.AreEqual(3, cells[0, 2].AdjacentCells.Count);

            Assert.AreEqual(5, cells[1, 0].AdjacentCells.Count);
            Assert.AreEqual(8, cells[1, 1].AdjacentCells.Count);
            Assert.AreEqual(5, cells[1, 2].AdjacentCells.Count);

            Assert.AreEqual(3, cells[2, 0].AdjacentCells.Count);
            Assert.AreEqual(5, cells[2, 1].AdjacentCells.Count);
            Assert.AreEqual(3, cells[2, 2].AdjacentCells.Count);
        }
    }

}