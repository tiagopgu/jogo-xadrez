using Xadrez.Domain.Entities;
using Xadrez.Domain.Entities.Enums;
using Xadrez.Domain.Entities.Pieces;

namespace Xadrez.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            // Peças Brancas
            Position positionW = new Position(2, 5);
            Piece kingW = new King(Color.White, board);

            Position positionW1 = new Position(1, 1);
            Piece rookW = new Rook(Color.White, board);

            Position positionW2 = new Position(1, 8);
            Piece rookW2 = new Rook(Color.White, board);

            board.AddPiece(kingW, positionW);
            board.AddPiece(rookW, positionW1);
            board.AddPiece(rookW2, positionW2);

            // Peças Pretas

            Position positionB = new Position(6, 5);
            Piece kingB = new King(Color.Black, board);

            Position positionB1 = new Position(8, 1);
            Piece rookB = new Rook(Color.Black, board);

            Position positionB2 = new Position(7, 4);
            Piece rookB2 = new Rook(Color.Black, board);

            board.AddPiece(kingB, positionB);
            board.AddPiece(rookB, positionB1);
            board.AddPiece(rookB2, positionB2);

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
