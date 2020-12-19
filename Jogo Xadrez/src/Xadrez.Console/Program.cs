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
                    try
                    {
                        System.Console.Write("\tEnter the position of the piece to be moved (ex.: c5): ");
                        ChessPosition startPosition = Screen.ReadChessPosition();

                        System.Console.Write("\tEnter the final position of the piece (ex.: d8): ");
                        ChessPosition finalPosition = Screen.ReadChessPosition();

                        game.MovePiece(startPosition, finalPosition);
                    }
                    catch (FormatException)
                    {
                        System.Console.WriteLine("\n\tError: You entered the position in an invalid format. Press any key to try the move again.");
                        System.Console.ReadKey();
                    }
                    catch (BoardException ex)
                    {
                        System.Console.WriteLine($"\n\tError: {ex.Message}. Press any key to try the move again.");
                        System.Console.ReadKey();
                    }

                    Screen.PrintBoard(game.Board);
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("\n\tUnexpected error: " + ex.Message);
            }
        }
    }
}
