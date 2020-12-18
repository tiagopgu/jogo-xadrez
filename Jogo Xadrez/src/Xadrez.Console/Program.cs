using System;
using Xadrez.Domain;
using Xadrez.Domain.Entities;
using Xadrez.Domain.Entities.Exceptions;

namespace Xadrez.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessGame game = new ChessGame();

                Screen.PrintBoard(game.Board);

                while (game.GameEnded == false)
                {
                    System.Console.Write("\tEnter the position of the piece to be moved (ex.: c5): ");
                    ChessPosition startPosition = Screen.ReadChessPosition();

                    System.Console.Write("\tEnter the final position of the piece (ex.: d8): ");
                    ChessPosition finalPosition = Screen.ReadChessPosition();

                    game.MovePiece(startPosition, finalPosition);
                    
                    Screen.PrintBoard(game.Board);
                }
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
