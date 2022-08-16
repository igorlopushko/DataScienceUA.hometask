using System;
using Xunit;

namespace DataScienceUA.Proxx.Logic.Tests
{
    public class BoardTests
    {
        [Fact]
        public void Constructor_ValidHoleCount_HoleCountIsCorrect()
        {
            int size = 5;
            int holeCount = 10;
            var board = new Board(size, holeCount);
            
            int count = 0;
            for (int y = 0; y < board.Cells.GetLength(0); y++)
            {
                for (int x = 0; x < board.Cells.GetLength(1); x++)
                {
                    if (board.Cells[y, x] != null && board.Cells[y, x].Type == CellType.Hole)
                    {
                        count++;
                    }
                }
            }
            
            Assert.Equal(holeCount, count);
        }
        
        [Fact]
        public void Constructor_InvalidHoleCount_ThrowsException()
        {
            int size = 5;
            int holeCount = 30;
            
            Assert.Throws<ArgumentException>(() => new Board(size, holeCount));
        }
        
        [Fact]
        public void Constructor_NSize_BoardSizeIsCorrect()
        {
            int size = 5;
            var board = new Board(size, 1);
            
            Assert.Equal(size, board.Cells.GetLength(0));
            Assert.Equal(size, board.Cells.GetLength(1));
        }
        
        [Fact]
        public void GenerateBlackHole_NHoles_HoleCountIsCorrect()
        {
            int size = 5;
            int holeCount = 10;
            var board = new Board(size, holeCount);

            int count = 0;
            for (int y = 0; y < board.Cells.GetLength(0); y++)
            {
                for (int x = 0; x < board.Cells.GetLength(1); x++)
                {
                    if (board.Cells[y, x] != null && board.Cells[y, x].Type == CellType.Hole)
                    {
                        count++;
                    }
                }
            }
            
            Assert.Equal(holeCount, count);
        }

        [Fact]
        public void OpenCell_NegativeX_ThrowsException()
        {
            int size = 5;
            int holeCount = 10;
            var board = new Board(size, holeCount);
            
            Assert.Throws<ArgumentException>(() => board.OpenCell(-1, 0));
        }
        
        [Fact]
        public void OpenCell_NegativeY_ThrowsException()
        {
            int size = 5;
            int holeCount = 10;
            var board = new Board(size, holeCount);
            
            Assert.Throws<ArgumentException>(() => board.OpenCell(0, -1));
        }
        
        [Fact]
        public void OpenCell_InvalidX_ThrowsException()
        {
            int size = 5;
            int holeCount = 10;
            var board = new Board(size, holeCount);
            
            Assert.Throws<ArgumentException>(() => board.OpenCell(size, 0));
        }
        
        [Fact]
        public void OpenCell_InvalidY_ThrowsException()
        {
            int size = 5;
            int holeCount = 10;
            var board = new Board(size, holeCount);
            
            Assert.Throws<ArgumentException>(() => board.OpenCell(0, size));
        }
    }
}