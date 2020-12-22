using System;
using Xadrez.Domain;
using Xadrez.Domain.Entities;
using Xadrez.Domain.Entities.Exceptions;
using Xadrez.Domain.Entities.Pieces;
using Xadrez.Domain.Utils;

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
                        Piece piece = game.GetPiece(startPosition);

                        Screen.SelectPosition(startPosition);

                        Screen.MarkPosition(piece?.PossibleMovements());

                        System.Console.Write("\tEnter the final position of the piece (ex.: d8): ");
                        ChessPosition finalPosition = Screen.ReadChessPosition();

                        game.MovePiece(startPosition, finalPosition);
                    }
                    catch (FormatException)
                    {
                        System.Console.WriteLine($"\n\tTypo: {SystemMessages.Typo}. {SystemMessages.TryAgain}.");
                        System.Console.ReadKey();
                    }
                    catch (ChessGameException ex)
                    {
                        System.Console.WriteLine($"\n\tInvalid play: {ex.Message}. {SystemMessages.TryAgain}.");
                        System.Console.ReadKey();
                    }
                    catch (BoardException ex)
                    {
                        System.Console.WriteLine($"\n\tInvalid position: {ex.Message}. {SystemMessages.TryAgain}.");
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
