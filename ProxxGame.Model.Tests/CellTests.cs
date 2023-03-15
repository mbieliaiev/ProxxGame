namespace ProxxGame.Model.Tests
{
    public class CellTests
    {
        [Test]
        public void CreateBlackHoleCell_ProvidedValueIsNotNegative_CellValueIsNegative()
        {
            var testCell = new Cell(2, new CellCoordinates(1, 2), true);
            testCell.IncrementNonHoleValue();
            Assert.That(testCell.Value, Is.EqualTo(-1));
        }

        [Test]
        public void ToggleMarkAsBlackHole_BlackHoleIsNotMarked_BlackHoleIsMarkedAndViceVersa()
        {
            var testCell = new Cell(2, new CellCoordinates(1, 2), true);
            Assert.IsFalse(testCell.IsMarkedAsBlackHole);
            testCell.ToggleMarkAsBlackHole();
            Assert.IsTrue(testCell.IsMarkedAsBlackHole);
            testCell.ToggleMarkAsBlackHole();
            Assert.IsFalse(testCell.IsMarkedAsBlackHole);
        }

        [Test]
        public void IncrementNonHoleValue_ValueIsNotHole_Succeed()
        {
            var testCell = new Cell(0, new CellCoordinates(1,2), false);
            testCell.IncrementNonHoleValue();
            Assert.That(testCell.Value, Is.EqualTo(1));
        }

        [Test]
        public void IncrementNonHoleValue_ValueIsHole_Fail()
        {
            var testCell = new Cell(-1, new CellCoordinates(1, 2), true);
            testCell.IncrementNonHoleValue();
            Assert.That(testCell.Value, Is.EqualTo(-1));
        }

        [Test]
        public void SetOpen_CellIsHidden_CellBecomesOpen()
        {
            var testCell = new Cell(new CellCoordinates(1, 2));
            testCell.SetOpen();
            Assert.That(testCell.IsOpen, Is.EqualTo(true));
        }
    }
}