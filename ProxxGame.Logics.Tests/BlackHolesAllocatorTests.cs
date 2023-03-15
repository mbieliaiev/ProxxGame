using ProxxGame.Contract;

namespace ProxxGame.Logics.Tests
{
    [TestFixture]
    public class BlackHolesAllocatorTests
    {
        [Test]
        public void GenerateBlackHolesAddresses_Returns_Expected_Number_Of_Indices()
        {
            // Arrange
            int tableSquare = 16;
            int numBlackHoles = 3;
            ICell[] blackHoles = new ICell[numBlackHoles];
            BlackHolesAllocator allocator = new BlackHolesAllocator();

            // Act
            List<int> result = allocator.GenerateBlackHolesAddresses(tableSquare, blackHoles);

            // Assert
            Assert.AreEqual(numBlackHoles, result.Count);
        }

        [Test]
        public void GenerateBlackHolesAddresses_Returns_Unique_Indices()
        {
            // Arrange
            int tableSquare = 16;
            int numBlackHoles = 3;
            ICell[] blackHoles = new ICell[numBlackHoles];
            BlackHolesAllocator allocator = new BlackHolesAllocator();

            // Act
            List<int> result = allocator.GenerateBlackHolesAddresses(tableSquare, blackHoles);

            // Assert
            CollectionAssert.AllItemsAreUnique(result);
        }

        [Test]
        public void PutBlackHolesOnTheTable_Places_BlackHoles_In_Correct_Cells()
        {
            // Arrange
            int tableWidth = 4;
            int tableHeight = 4;
            List<int> blackHolesIndices = new List<int>() { 0, 4, 11 };
            ICell[,] cellsTable = new ICell[tableHeight, tableWidth];
            ICell[] blackHoles = new ICell[blackHolesIndices.Count];
            BlackHolesAllocator allocator = new BlackHolesAllocator();

            // Act
            allocator.PutBlackHolesOnTheTable(tableWidth, tableHeight, blackHolesIndices, cellsTable, blackHoles);

            // Assert
            Assert.IsTrue(cellsTable[1, 0].IsBlackHole);
            Assert.IsTrue(cellsTable[2, 3].IsBlackHole);
            Assert.IsTrue(cellsTable[3, 3].IsBlackHole);
        }
    }

}
