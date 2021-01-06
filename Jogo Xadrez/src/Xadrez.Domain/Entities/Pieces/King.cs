using Xadrez.Domain.Entities.Enums;
using Xadrez.Domain.Utils;

namespace Xadrez.Domain.Entities.Pieces
{
    public class King : Piece
    {
        public King(Color color, Board board)
            : base(color, board)
        {

        }

        public override bool ValidMovement(Position destiny)
        {
            // Same Position
            if (destiny.Equals(Position))
                return false;

            // You can only move one house at a time
            if (MovedAHouse(destiny) == false)
                return false;

            // check that there is no piece of the same color in the destination
            if (FreeAway(destiny) == false)
                return false;

            // Put yourself in check
            if (IsCheckMovement(destiny))
                return false;

            // Put yourself in check after capture
            if (IsInCheckAfterCapture(destiny))
                return false;

            return true;
        }

        public bool IsCheckMovement(Position destiny)
        {
            Position currentPosition = new Position(0, 0);

            for (int i = 1; i <= Board.AmountLines; i++)
            {
                for (int j = 1; j <= Board.AmountColumns; j++)
                {
                    currentPosition.Line = (byte)i;
                    currentPosition.Column = (byte)j;
                    Piece piece = Board.GetPiece(currentPosition);

                    if (piece != null && piece.Color != Color)
                    {
                        if (piece is Pawn == false && piece.ValidMovement(destiny))
                            return true;

                        if (piece is Pawn && (piece as Pawn).CanCapture(destiny, this))
                            return true;
                    }
                }
            }

            return false;
        }

        public override string ToString()
        {
            return "R";
        }

        #region Privates Methods

        private bool MovedAHouse(Position destiny)
        {
            if (destiny.Line > Position.Line + 1 || destiny.Column > Position.Column + 1 || destiny.Line < Position.Line - 1 || destiny.Column < Position.Column - 1)
                return false;

            return true;
        }

        private bool FreeAway(Position destiny)
        {
            return ChessHelper.PermittedPosition(Board, destiny, Color);
        }

        private bool IsInCheckAfterCapture(Position destiny)
        {
            Piece piece = Board.GetPiece(destiny);
            bool isCheckMovement = false;

            if (piece != null && piece.Color != Color)
            {
                Position currentPositionPiece = piece.Position;

                Board.RemovePiece(destiny);

                isCheckMovement = IsCheckMovement(destiny);

                Board.AddPiece(piece, currentPositionPiece);
            }

            return isCheckMovement;
        }

        #endregion
    }
}
