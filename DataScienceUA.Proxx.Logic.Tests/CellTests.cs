using DataScienceUA.Proxx.Logic.Exceptions;
using Xunit;

namespace DataScienceUA.Proxx.Logic.Tests
{
    public class CellTests
    {
        [Fact]
        public void Constructor_ValidParams_CellIsCreated()
        {
            var cell = new Cell(CellType.Empty);
            
            Assert.Equal(CellType.Empty, cell.Type);
            Assert.Equal(CellVisibilityType.Closed, cell.Visibility);
            Assert.Equal(0, cell.Value);
        }
        
        [Fact]
        public void Constructor_NumberCellType_ThrowsException()
        {
            Assert.Throws<NotValidCellTypeException>(() => new Cell(CellType.Number));
        }

        [Fact]
        public void Open_EmptyCell_CellIsOpened()
        {
            var cell = new Cell(CellType.Empty);

            cell.Open();
            
            Assert.Equal(CellVisibilityType.Opened, cell.Visibility);
        }

        [Fact]
        public void Open_HoleCell_ThrowsException()
        {
            var cell = new Cell(CellType.Hole);

            Assert.Throws<BlackHoleCollisionException>(() => cell.Open());
        }

        [Fact]
        public void IncrementValue_ForEmptyCell_BecomesANumberCell()
        {
            var cell = new Cell(CellType.Empty);
            
            cell.IncrementValue();
            
            Assert.Equal(1, cell.Value);
            Assert.Equal(CellType.Number, cell.Type);
        }
        
        [Fact]
        public void IncrementValue_ForNumberCell_ValueChanges()
        {
            var cell = new Cell(CellType.Empty);
            
            cell.IncrementValue();
            cell.IncrementValue();
            
            Assert.Equal(2, cell.Value);
            Assert.Equal(CellType.Number, cell.Type);
        }
    }
}