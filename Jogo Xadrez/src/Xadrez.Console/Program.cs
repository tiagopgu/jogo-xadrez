using Xadrez.Domain.Entities;

namespace Xadrez.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            System.Console.WriteLine();

            PrintBoard(board);

            System.Console.WriteLine();
        }

        static void PrintBoard(Board board)
        {
            string columnIdentificationLine = "";

            for (byte i = 0; i < board.AmountLines; i++)
            {
                System.Console.Write($"\t{board.AmountLines - i}  ");

                for (byte j = 0; j < board.AmountColumns; j++)
                {
                    Piece piece = board.GetPiece(new Position(i, j));

                    if (piece == null)
                        System.Console.Write("-  ");
                    else
                        System.Console.Write("P  ");

                    if (i == board.AmountLines - 1)
                        columnIdentificationLine += $"{j + 1}  ";
                }

                System.Console.WriteLine();
            }

            System.Console.Write($"\t   {columnIdentificationLine}");
        }
    }
}
