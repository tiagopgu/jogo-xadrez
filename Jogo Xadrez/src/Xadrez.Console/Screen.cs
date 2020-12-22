using System;
using Xadrez.Domain.Entities;
using Xadrez.Domain.Entities.Enums;

namespace Xadrez.Console
{
    public static class Screen
    {
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

        public static ChessPosition ReadChessPosition()
        {
            string input = System.Console.ReadLine();

            if (input.Length != 2)
                return null;

            byte line = byte.Parse(input[1].ToString());
            char column = input[0];

            return new ChessPosition(line, column);
        }

        public static void SelectPosition(ChessPosition position)
        {
            if (position != null && position.Line > 0 && position.Line <= ScreenConfig.TotalChessGameLines && ScreenConfig.ColumnIdentification.IndexOf(position.Column) != -1)
            {
                int currentLeftPosition = System.Console.CursorLeft;
                int currentTopPosition = System.Console.CursorTop;

                byte leftPosition = (byte)(ScreenConfig.LeftStartingPositionChessGame + (ScreenConfig.ColumnIdentification.IndexOf(position.Column) * ScreenConfig.ChessHouseWidth));
                byte topPosition = (byte)(ScreenConfig.TotalChessGameLines - position.Line + ScreenConfig.TopStartingPositionChessGame);

                SetBackgroundCheese((byte)(position.Line + 1), (byte)ScreenConfig.ColumnIdentification.IndexOf(position.Column));
                System.Console.ForegroundColor = ScreenConfig.ForegroundColorSelect;
                System.Console.SetCursorPosition(leftPosition, topPosition);

                System.Console.Write('[');

                System.Console.SetCursorPosition(leftPosition + 2, topPosition);

                System.Console.Write("]");

                System.Console.ResetColor();
                System.Console.SetCursorPosition(currentLeftPosition, currentTopPosition);
            }
        }

        #region Privates Methods

        private static void PrintPiece(Piece piece)
        {
            if (piece == null)
                System.Console.Write(new string(' ', ScreenConfig.ChessHouseWidth));
            else
            {
                if (piece.Color == Color.Black)
                    System.Console.ForegroundColor = ConsoleColor.Black;
                else
                    System.Console.ForegroundColor = ConsoleColor.White;

                string pieceName = piece.ToString();
                string output = pieceName.PadLeft(pieceName.Length + ((ScreenConfig.ChessHouseWidth - pieceName.Length) / 2));

                output = output.PadRight(output.Length + (ScreenConfig.ChessHouseWidth - pieceName.Length) / 2);

                System.Console.Write(output);

                System.Console.ResetColor();
            }
        }

        private static void SetBackgroundCheese(byte line = 0, byte column = 0, bool reset = false)
        {
            if (reset)
                System.Console.ResetColor();
            else
            {
                if ((line % 2 == 0 && column % 2 != 0) || (line % 2 != 0 && column % 2 == 0))
                    System.Console.BackgroundColor = ScreenConfig.FirstBackgroundColor;
                else
                    System.Console.BackgroundColor = ScreenConfig.SecondBackgroundColor;
            }
        }

        #endregion

        private static class ScreenConfig
        {
            public static byte ChessHouseWidth => 3;

            public static byte TotalChessGameLines => 8;

            public static string ColumnIdentification => "abcdefgh";

            public static ConsoleColor FirstBackgroundColor => ConsoleColor.DarkGray;

            public static ConsoleColor SecondBackgroundColor => ConsoleColor.DarkCyan;

            public static ConsoleColor ForegroundColorSelect => ConsoleColor.Yellow;

            public static byte LeftStartingPositionChessGame => 10;

            public static byte TopStartingPositionChessGame => 2;
        }
    }
}
