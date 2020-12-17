using Xadrez.Domain.Entities;

namespace Xadrez.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            var boardPrint = board.Print();

            PrintBoard(boardPrint);
        }

        static void PrintBoard(Piece[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    System.Console.Write("- ");
                }

                System.Console.WriteLine();
            }
        }
    }
}
