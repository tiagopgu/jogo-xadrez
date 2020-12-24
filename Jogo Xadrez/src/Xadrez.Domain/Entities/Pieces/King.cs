using Xadrez.Domain.Entities.Enums;

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
            // You can only move one house at a time
            if (destiny.Line > Position.Line + 1 || destiny.Column > Position.Column + 1 || destiny.Line < Position.Line - 1 || destiny.Column < Position.Column - 1)
                return false;

            // Cannot have piece at destination
            if (Board.ExistsPiece(destiny))
                return false;

            // Put yourself in check
            if (CheckMovement(destiny))
                return false;

            return true;
        }

        public override string ToString()
        {
            return "R";
        }

        #region Privates Methods

        private bool CheckMovement(Position destiny)
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
                        
                        if (piece is Pawn && (piece as Pawn).ValidateCaptureMovement(destiny, this))
                            return true;
                    }
                }
            }

            return false;
        }

        #endregion
    }
}
