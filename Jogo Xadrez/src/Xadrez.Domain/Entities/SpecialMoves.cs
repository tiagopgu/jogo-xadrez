using Xadrez.Domain.Entities.Pieces;

namespace Xadrez.Domain.Entities
{
    public static class SpecialMoves
    {
        public static bool IsSmallRock(Piece piece, Position destiny, Board board)
        {
            if (piece == null || piece is King == false || piece.AmountMoviments > 0)
                return false;

            if (piece.Position.Line != destiny.Line)
                return false;

            if (destiny.Column - piece.Position.Column != -2)
                return false;

            Position positionRook = new Position(destiny.Line, (byte)(destiny.Column - 1));
            Piece rook = board.GetPiece(positionRook);

            if (rook == null || rook is Rook == false || rook.Color != piece.Color || rook.AmountMoviments > 0)
                return false;

            King king = piece as King;

            if (king.IsCheckMovement(king.Position))
                return false;

            Position positionTest = new Position(king.Position.Line, 0);

            for (int i = king.Position.Column - 1; i >= destiny.Column; i--)
            {
                positionTest.Column = (byte)i;

                if (board.ExistsPiece(positionTest))
                    return false;

                if (king.IsCheckMovement(positionTest))
                    return false;
            }

            return true;
        }

        public static bool IsSpecialMoves(Piece piece, Position destiny, Board board)
        {
            return IsSmallRock(piece, destiny, board);
        }
    }
}
