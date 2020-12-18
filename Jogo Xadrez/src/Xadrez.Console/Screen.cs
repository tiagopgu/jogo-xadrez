using System;
using Xadrez.Domain.Entities;
using Xadrez.Domain.Entities.Enums;

namespace Xadrez.Console
{
    public static class Screen
    {
        private static byte _chessHouseWidth = 3;
        private static ConsoleColor _defaultBackgroundColor = ConsoleColor.Black;
        private static ConsoleColor _defaultForegroundColor = ConsoleColor.Gray;

        public static void PrintBoard(Board board)
        {
            System.Console.Clear();

            string columnIdentificationLine = "";
            Position position = new Position(0, 0);

            System.Console.WriteLine("\n");

            for (byte i = board.AmountLines; i > 0; i--)
            {
                position.Line = i;

                System.Console.Write($"\t{i} ");

                for (byte j = 1; j <= board.AmountColumns; j++)
                {
                    position.Column = j;

                    Piece piece = board.GetPiece(position);

                    SetBackgroundCheese(i, j);

                    PrintPiece(piece);

                    SetBackgroundCheese(reset: true);

                    if (i == board.AmountLines)
                        columnIdentificationLine += $"{(char)('a' + j - 1)}  ";
                }

                System.Console.WriteLine();
            }

            System.Console.Write($"\t   {columnIdentificationLine}\n\n");
        }

        #region Privates Methods

        private static void PrintPiece(Piece piece)
        {
            if (piece == null)
                System.Console.Write(new string(' ', _chessHouseWidth));
            else
            {
                if (piece.Color == Color.Black)
                    System.Console.ForegroundColor = ConsoleColor.Black;
                else
                    System.Console.ForegroundColor = ConsoleColor.White;

                string pieceName = piece.ToString();
                string output = pieceName.PadLeft(pieceName.Length + ((_chessHouseWidth - pieceName.Length) / 2));

                output = output.PadRight(output.Length + (_chessHouseWidth - pieceName.Length) / 2);

                System.Console.Write(output);

                System.Console.ForegroundColor = _defaultForegroundColor;
            }
        }

        private static void SetBackgroundCheese(byte line = 0, byte column = 0, bool reset = false)
        {
            if (reset)
                System.Console.BackgroundColor = _defaultBackgroundColor;
            else
            {
                if ((line % 2 == 0 && column % 2 != 0) || (line % 2 != 0 && column % 2 == 0))
                    System.Console.BackgroundColor = ConsoleColor.DarkGray;
                else
                    System.Console.BackgroundColor = ConsoleColor.DarkCyan;
            }
        }

        #endregion
    }
}
