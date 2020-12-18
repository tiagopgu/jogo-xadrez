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
                Screen.PrintBoard(game.Board);

                game.RemovePiece(new ChessPosition(7, 'c'));

                Screen.PrintBoard(game.Board);

                game.MovePiece(new ChessPosition(8, 'a'), new ChessPosition(1, 'a'));

                Screen.PrintBoard(game.Board);
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
