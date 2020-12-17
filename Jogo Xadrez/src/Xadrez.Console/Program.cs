using Xadrez.Domain.Entities;

namespace Xadrez.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            var boardPrint = board.Print();

            System.Console.WriteLine();

            PrintBoard(boardPrint);

            System.Console.WriteLine();
        }

        static void PrintBoard(Piece[,] board)
        {
            string columnIdentificationLine = "";

            for (int i = 0; i < board.GetLength(0); i++)
            {
                System.Console.Write($"\t{board.GetLength(0) - i}  ");

                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == null)
                        System.Console.Write("-  ");
                    else
                        System.Console.Write("P  ");

                    if (i == board.GetLength(0) - 1)
                        columnIdentificationLine += $"{j + 1}  ";
                }

                System.Console.WriteLine();
            }

            System.Console.Write($"\t   {columnIdentificationLine}");
        }
    }
}
