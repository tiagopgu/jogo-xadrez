namespace Xadrez.Domain.Utils
{
    public static class SystemMessages
    {
        // Chess Game Operations

        public static string Typo => "You entered the position in an invalid format";

        public static string TryAgain => "Press any key to perform the move again";

        public static string NoPiece => "No pieces in that position";

        public static string InvalidMovement => "Invalid movement for this piece";

        public static string InvalidMovementToRemoveCheck => "This piece can only be moved to a position that takes the king out of check.";

        public static string InvalidPieceForCurrentPlayer => "The selected piece is invalid for the current player";

        public static string CanNotMove => "The selected piece has no possible movements";

        public static string KingIsInCheck => "The king is in check and only pieces that take him out of this condition can be moved";
    }
}
