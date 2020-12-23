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
                SetCursorPosition(position);

                System.Console.ForegroundColor = ScreenConfig.ForegroundColorSelect;

                System.Console.Write('[');

                System.Console.SetCursorPosition(System.Console.CursorLeft + 1, System.Console.CursorTop);

                System.Console.Write("]");

                System.Console.ResetColor();

                ResetCursorPosition();
            }
        }

        public static void MarkPosition(bool[,] positionsMarked)
        {
            if (positionsMarked != null)
            {
                for (int i = positionsMarked.GetLength(0) - 1; i > 0; i--)
                {
                    for (int j = 0; j < positionsMarked.GetLength(1); j++)
                    {
                        if (positionsMarked[i, j])
                        {
                            ChessPosition position = new ChessPosition((byte)(positionsMarked.GetLength(0) - i), ScreenConfig.ColumnIdentification[j]);

                            SetCursorPosition(position);

                            System.Console.ForegroundColor = ConsoleColor.DarkRed;

                            System.Console.Write(" * ");

                            System.Console.ResetColor();

                            ResetCursorPosition();
                        }
                    }
                }
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

        private static void SetCursorPosition(ChessPosition position)
        {
            ScreenConfig.CurrentLeftPositionCursor = System.Console.CursorLeft;
            ScreenConfig.CurrentTopPositionCursor = System.Console.CursorTop;

            byte leftPosition = (byte)(ScreenConfig.LeftStartingPositionChessGame + (ScreenConfig.ColumnIdentification.IndexOf(position.Column) * ScreenConfig.ChessHouseWidth));
            byte topPosition = (byte)(ScreenConfig.TotalChessGameLines - position.Line + ScreenConfig.TopStartingPositionChessGame);

            SetBackgroundCheese((byte)(position.Line + 1), (byte)ScreenConfig.ColumnIdentification.IndexOf(position.Column));
            System.Console.SetCursorPosition(leftPosition, topPosition);
        }

        private static void ResetCursorPosition()
        {
            System.Console.SetCursorPosition(ScreenConfig.CurrentLeftPositionCursor.GetValueOrDefault(), ScreenConfig.CurrentTopPositionCursor.GetValueOrDefault());
            ScreenConfig.CurrentLeftPositionCursor = null;
            ScreenConfig.CurrentTopPositionCursor = null;
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

            public static ConsoleColor ForegroundColorMark => ConsoleColor.DarkRed;

            public static byte LeftStartingPositionChessGame => 10;

            public static byte TopStartingPositionChessGame => 2;

            public static int? CurrentTopPositionCursor { get; set; }

            public static int? CurrentLeftPositionCursor { get; set; }
        }
    }
}
