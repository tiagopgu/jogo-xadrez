using System;
using Xadrez.Domain;
using Xadrez.Domain.Entities;
using Xadrez.Domain.Entities.Exceptions;
using Xadrez.Domain.Utils;

namespace Xadrez.Console
{
    class Program
    {
        private static ChessGame game;

        static void Main(string[] args)
        {
            try
            {
                game = new ChessGame();

                while (game.GameEnded == false)
                {
                    RefreshScreen();

                    try
                    {
                        while (game.SomePawnCanBePromoted())
                        {
                            Screen.PrintMessage($"The {game.CurrentPlayer} pawn won a promotion.");
                            System.Console.Write("\tEnter the code for one of the following pieces to promote it: (D - Queen / T - Rook / B - Bishop / C - Knight): ");

                            char codPiece = Screen.ReadCharacter();
                            game.PromotePawn(codPiece);

                            RefreshScreen();
                        }

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
                            RefreshScreen();

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

        static void RefreshScreen()
        {
            if (game != null)
            {
                Screen.PrintBoard(game.Board);
                Screen.PrintInfo(game);
            }
        }
    }
}
