using System;
using DataScienceUA.Proxx.Logic;
using DataScienceUA.Proxx.Logic.Exceptions;

namespace DataScienceUA.Proxx.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int size;
            int holeCount;
            
            Console.Write("Please, enter board size NxN: ");
            while(!int.TryParse(Console.ReadLine(), out size))
            {
                Console.Write("Please, enter board size NxN: ");
            }
            
            Console.Write("Please, enter number of black holes: ");
            while(!int.TryParse(Console.ReadLine(), out holeCount))
            {
                Console.Write("Please, enter number of black holes: ");
            }
            
            Console.WriteLine();
            
            var board = new Board(size, holeCount);
            PrintBoard(board);

            while (true)
            {
                int x;
                int y;
                
                Console.Write("Please, enter X: ");
                while(!int.TryParse(Console.ReadLine(), out x))
                {
                    Console.Write("Please, enter X: ");
                }
                
                Console.Write("Please, enter Y: ");
                while(!int.TryParse(Console.ReadLine(), out y))
                {
                    Console.Write("Please, enter Y: ");
                }

                try
                {
                    board.OpenCell(x, y);
                }
                catch (BlackHoleCollisionException)
                {
                    PrintBoard(board);
                    Console.WriteLine("Game over!");
                    break;
                }

                PrintBoard(board);
            }
        }

        private static void PrintBoard(Board board)
        {
            Console.Write("  ");

            for (var i = 0; i < board.Size; i++)
            {
                Console.Write(i);
            }
            
            Console.WriteLine();
            Console.Write("  ");
            for (var i = 0; i < board.Size; i++)
            {
                Console.Write("_");
            }
            
            Console.WriteLine();
            
            for (var y = 0; y < board.Cells.GetLength(0); y++)
            {
                Console.Write(y + "|");
                for (var x = 0; x < board.Cells.GetLength(1); x++)
                {
                    if (board.Cells[y, x].Visibility == CellVisibilityType.Closed)
                    {
                        Console.Write(".");
                    }
                    else
                    {
                        if (board.Cells[y, x].Type == CellType.Hole)
                        {
                            Console.Write("x");
                        }
                        else
                        {
                            Console.Write(board.Cells[y, x].Value);
                        }
                    }
                }
                Console.WriteLine();
            }
        }
    }
}