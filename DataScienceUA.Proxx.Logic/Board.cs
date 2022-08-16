using System;
using System.Collections.Generic;

namespace DataScienceUA.Proxx.Logic
{
    public class Board
    {
        private readonly Cell[,] _cells;
        public int Size { get; }
        public int HoleCount { get; }
        public Cell[,] Cells => _cells;

        public Board(int size, int holeCount)
        {
            if (holeCount > size * size)
            {
                throw new ArgumentException("The hole count is larger than the number of cells");
            }
            
            Size = size;
            HoleCount = holeCount;
            _cells = new Cell[size, size];

            GenerateBoard();
        }

        public void OpenCell(int x, int y)
        {
            if (x < 0 || y < 0 || x > Size - 1 || y > Size - 1)
            {
                throw new ArgumentException("Cell is out of range of board coordinates");
            }
            
            _cells[y, x].Open();

            if (_cells[y, x].Value == 0)
            {
                FloodFill(x, y);
            }
        }

        private void FloodFill(int x, int y)
        {
            var stack = new Stack<(int, int)>();
            stack.Push(new(x, y));

            while (stack.Count > 0)
            {
                var coordinates = stack.Pop();
                
                var square = GetInnerSquare(coordinates.Item1, coordinates.Item2);
                var innerX = square.Item1;
                var innerY = square.Item2;
                var xLength = square.Item3;
                var yLength = square.Item4;

                var yCount = 0;
                for (var iY = innerY; yCount < yLength; iY++)
                {
                    var xCount = 0;
                    for (var iX = innerX; xCount < xLength; iX++)
                    {
                        xCount++;
                        if (_cells[iY, iX].Type == CellType.Hole)
                        {
                            continue;
                        }
                        
                        if (_cells[iY, iX].Type == CellType.Empty && 
                            _cells[iY, iX].Visibility == CellVisibilityType.Closed &&
                            iX != coordinates.Item1 && 
                            iY != coordinates.Item2)
                        {
                            stack.Push(new(iX, iY));
                        }

                        _cells[iY, iX].Open();
                    }

                    yCount++;
                }
            }
        }
        
        private void GenerateBoard()
        {
            GenerateBlackHoles();
            GenerateNumbers();
        }

        private void GenerateBlackHoles()
        {
            for (var i = 0; i < HoleCount; i++)
            {
                while (true)
                {
                    var point = GetRandomCoordinates();
                    if (_cells[point.Item1, point.Item2] != null)
                    {
                        continue;
                    }

                    _cells[point.Item1, point.Item2] = new Cell(CellType.Hole);
                    break;
                }
            }
        }

        private void GenerateNumbers()
        {
            // set empty values
            for (int y = 0; y < _cells.GetLength(0); y++)
            {
                for (int x = 0; x < _cells.GetLength(1); x++)
                {
                    if (_cells[y, x] == null)
                    {
                        _cells[y, x] = new Cell(CellType.Empty);
                    }
                }
            }
            
            // update numbers according to holes location
            for (var y = 0; y < _cells.GetLength(0); y++)
            {
                for (var x = 0; x < _cells.GetLength(1); x++)
                {
                    if (_cells[y, x].Type == CellType.Hole)
                    {
                        var square = GetInnerSquare(x, y);
                        var innerX = square.Item1;
                        var innerY = square.Item2;
                        var xLength = square.Item3;
                        var yLength = square.Item4;

                        var yCount = 0;
                        for (var iY = innerY; yCount < yLength; iY++)
                        {
                            var xCount = 0;
                            for (var iX = innerX; xCount < xLength; iX++)
                            {
                                xCount++;
                                if (_cells[iY, iX].Type == CellType.Hole)
                                {
                                    continue;
                                }
                                
                                _cells[iY, iX].IncrementValue();
                            }

                            yCount++;
                        }
                    }
                }
            }
        }

        private (int, int, int, int) GetInnerSquare(int x, int y)
        {
            var innerX = x == 0 ? 0 : x - 1;
            var innerY = y == 0 ? 0 : y - 1;
            var xLength = 3;
            var yLength = 3;

            if (x - 1 < 0 || x - 1 + xLength > Size)
            {
                xLength = 2;
            }
                        
            if (y - 1 < 0 || y - 1 + yLength > Size)
            {
                yLength = 2;
            }

            return (innerX, innerY, xLength, yLength);
        }
        
        private (int, int) GetRandomCoordinates()
        {
            var x = new Random().Next(0, Size);
            var y = new Random().Next(0, Size);
            
            return (y, x);
        }
    }
}