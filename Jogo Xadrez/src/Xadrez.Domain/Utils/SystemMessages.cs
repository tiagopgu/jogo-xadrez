﻿namespace Xadrez.Domain.Utils
{
    public static class SystemMessages
    {
        // Chess Game Operations

        public static string Typo => "You entered the position in an invalid format";

        public static string TryAgain => "Press any key to perform the move again";

        public static string NoPiece => "No pieces in that position";

        public static string InvalidMovement => "Invalid movement for this piece";

        public static string InvalidPieceForCurrentPlayer => "The selected piece is invalid for the current player";
    }
}
