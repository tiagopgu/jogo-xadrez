using System;
using Xadrez.Domain;
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

                game.AddPiece(new King(Color.White, game.Board), new ChessPosition(2, 'e'));
                game.AddPiece(new Rook(Color.White, game.Board), new ChessPosition(1, 'a'));
                game.AddPiece(new Rook(Color.White, game.Board), new ChessPosition(1, 'h'));

                // Peças Pretas
                game.AddPiece(new King(Color.Black, game.Board), new ChessPosition(6, 'e'));
                game.AddPiece(new Rook(Color.Black, game.Board), new ChessPosition(8, 'a'));
                game.AddPiece(new Rook(Color.Black, game.Board), new ChessPosition(7, 'd'));
                game.AddPiece(new Pawn(Color.Black, game.Board), new ChessPosition(7, 'c'));

                Screen.PrintBoard(game.Board);

                game.RemovePiece(new ChessPosition(7, 'c'));

                Screen.PrintBoard(game.Board);

                System.Console.WriteLine(game.GetPiece(new ChessPosition(7, 'd'))?.ToString() ?? "Nenhuma peça");
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
