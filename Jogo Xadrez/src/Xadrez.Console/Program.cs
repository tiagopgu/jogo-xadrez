using Xadrez.Domain.Entities;
using Xadrez.Domain.Entities.Enums;

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

            // Add Piece
            Position position = new Position(1, 6);
            Piece piece = new Piece(position, Color.Black, board);

            Position position2 = new Position(5, 8);
            Piece piece2 = new Piece(position2, Color.White, board);

            board.AddPiece(piece);
            board.AddPiece(piece2);

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
                    {
                        if (piece.Color == Color.Black)
                            System.Console.ForegroundColor = System.ConsoleColor.Yellow;

                        System.Console.Write($"{piece}  ");

                        System.Console.ForegroundColor = System.ConsoleColor.White;
                    }   

                    if (i == board.AmountLines - 1)
                        columnIdentificationLine += $"{j + 1}  ";
                }

                System.Console.WriteLine();
            }

            System.Console.Write($"\t   {columnIdentificationLine}");
        }
    }
}
