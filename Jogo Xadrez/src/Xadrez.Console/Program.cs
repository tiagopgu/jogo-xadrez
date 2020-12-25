using System;
using Xadrez.Domain;
using Xadrez.Domain.Entities;
using Xadrez.Domain.Entities.Exceptions;
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

                while (game.GameEnded == false)
                {
                    Screen.PrintBoard(game.Board);
                    Screen.PrintInfo(game);

                    try
                    {
                        if (game.Shift > 1 && game.KingIsInCheck(game.CurrentPlayer))
                        {
                            Screen.PrintAlert($"The {game.CurrentPlayer.ToString().ToLower()} king is in check.");
                        }

                        System.Console.Write("\tEnter the position of the piece to be moved (ex.: c5): ");
                        ChessPosition startPosition = Screen.ReadChessPosition();

                        if (startPosition != null)
                        {
                            Piece piece = game.GetPiece(startPosition);

                            if (game.CanMove(piece))
                            {
                                Screen.SelectPosition(startPosition);

                                Screen.MarkPosition(piece?.PossibleMovements());

                                System.Console.Write("\tEnter the final position of the piece (ex.: d8). Press enter without a position to cancel the selection: ");
                                ChessPosition finalPosition = Screen.ReadChessPosition();

                                if (finalPosition != null)
                                    game.MovePiece(startPosition, finalPosition);
                            }
                        }
                    }
                    catch (FormatException)
                    {
                        System.Console.Write($"\n\tTypo: {SystemMessages.Typo}. {SystemMessages.TryAgain}.");
                        System.Console.ReadKey(true);
                    }
                    catch (ChessGameException ex)
                    {
                        System.Console.Write($"\n\tInvalid play: {ex.Message}. {SystemMessages.TryAgain}.");
                        System.Console.ReadKey(true);
                    }
                    catch (BoardException ex)
                    {
                        System.Console.Write($"\n\tInvalid position: {ex.Message}. {SystemMessages.TryAgain}.");
                        System.Console.ReadKey(true);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("\n\tUnexpected error: " + ex.Message);
            }
        }
    }
}
