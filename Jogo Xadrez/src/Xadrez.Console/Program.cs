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
                        if (game.KingIsInCheck(game.CurrentPlayer))
                        {
                            Screen.PrintMessage($"The {game.CurrentPlayer.ToString().ToLower()} king is in check.", TypeInfo.Alert);
                        }

                        System.Console.Write("\tEnter the position of the piece to be moved (ex.: c5): ");
                        ChessPosition startPosition = Screen.ReadChessPosition();

                        if (startPosition != null)
                        {
                            Piece piece = game.GetPiece(startPosition);

                            if (game.CanMove(piece))
                            {
                                Screen.SelectPosition(startPosition);

                                Screen.MarkPosition(piece?.PossibleMovements(), game);

                                System.Console.Write("\tEnter the final position of the piece (ex.: d8). Press enter without a position to cancel the selection: ");
                                ChessPosition finalPosition = Screen.ReadChessPosition();

                                if (finalPosition != null)
                                    game.MovePiece(startPosition, finalPosition);
                            }
                        }

                        if (game.GameEnded)
                        {
                            Screen.PrintBoard(game.Board);
                            Screen.PrintInfo(game);

                            Screen.PrintMessage("CHECKMATE!!!", TypeInfo.Alert);
                            Screen.PrintMessage($"The game is over. The winner was the {game.CurrentPlayer.ToString().ToLower()} pieces");
                            System.Console.ReadKey();
                        }
                    }
                    catch (FormatException)
                    {
                        Screen.PrintMessage($"Typo: {SystemMessages.Typo}. {SystemMessages.TryAgain}.", TypeInfo.Warning);
                        System.Console.ReadKey(true);
                    }
                    catch (ChessGameException ex)
                    {
                        Screen.PrintMessage($"Invalid play: {ex.Message}. {SystemMessages.TryAgain}.", TypeInfo.Warning);
                        System.Console.ReadKey(true);
                    }
                    catch (BoardException ex)
                    {
                        Screen.PrintMessage($"Invalid position: {ex.Message}. {SystemMessages.TryAgain}.", TypeInfo.Warning);
                        System.Console.ReadKey(true);
                    }
                }
            }
            catch (Exception ex)
            {
                Screen.PrintMessage("Unexpected error: " + ex.Message, TypeInfo.Alert);
            }
        }
    }
}
