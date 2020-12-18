﻿using System;
using Xadrez.Domain.Entities;
using Xadrez.Domain.Entities.Enums;
using Xadrez.Domain.Entities.Exceptions;
using Xadrez.Domain.Entities.Pieces;

namespace Xadrez.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessGame game = new ChessGame();

            try
            {
                // Peças Brancas

                game.AddPiece(new King(Color.White, game.Board), 2, 5);
                game.AddPiece(new Rook(Color.White, game.Board), 1, 1);
                game.AddPiece(new Rook(Color.White, game.Board), 1, 8);

                // Peças Pretas
                game.AddPiece(new King(Color.Black, game.Board), 6, 5);
                game.AddPiece(new Rook(Color.Black, game.Board), 8, 1);
                game.AddPiece(new Rook(Color.Black, game.Board), 7, 4);
                game.AddPiece(new Pawn(Color.Black, game.Board), 7, 3);

                System.Console.WriteLine();

                game.PrintBoard();

                System.Console.WriteLine();

                Position position = new Position(7, 4);

                System.Console.WriteLine(game.Board.GetPiece(position).ToString() ?? "Nenhuma peça");
            }
            catch (BoardException ex)
            {
                System.Console.WriteLine("\nError: " + ex.Message);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("\nUnexpected error: " + ex.Message);
            }
        }
    }
}
