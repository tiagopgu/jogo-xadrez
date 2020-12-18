﻿using System;
using Xadrez.Domain.Entities;
using Xadrez.Domain.Entities.Enums;

namespace Xadrez.Console
{
    public class ChessGame
    {
        public Board Board { get; private set; }

        public ChessGame()
        {
            Board = new Board(8, 8);
        }

        public void AddPiece(Piece piece, byte line, char charColumn)
        {
            Position position = GetPosition(line, charColumn);

            Board.AddPiece(piece, position);
        }

        public Piece GetPiece(byte line, char charColumn)
        {
            Position position = GetPosition(line, charColumn);

            return Board.GetPiece(position);
        }

        public void PrintBoard()
        {
            string columnIdentificationLine = "";
            Position position = new Position(0, 0);

            for (byte i = Board.AmountLines; i > 0; i--)
            {
                position.Line = i;

                System.Console.Write($"\t{i}  ");

                for (byte j = 1; j <= Board.AmountColumns; j++)
                {
                    position.Column = j;

                    Piece piece = Board.GetPiece(position);

                    if (piece == null)
                        System.Console.Write("-  ");
                    else
                    {
                        if (piece.Color == Color.Black)
                            System.Console.ForegroundColor = ConsoleColor.Yellow;

                        System.Console.Write($"{piece}  ");

                        System.Console.ForegroundColor = ConsoleColor.White;
                    }

                    if (i == Board.AmountLines)
                        columnIdentificationLine += $"{(char)('a' + j - 1)}  ";
                }

                System.Console.WriteLine();
            }

            System.Console.Write($"\t   {columnIdentificationLine}");
        }

        private Position GetPosition(byte line, char charColumn)
        {
            return new Position(line, (byte)(charColumn - 'a' + 1));
        }
    }
}
