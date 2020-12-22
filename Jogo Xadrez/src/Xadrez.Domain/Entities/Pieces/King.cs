using Xadrez.Domain.Entities.Enums;
using Xadrez.Domain.Entities.Exceptions;
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
            // You can only move one house at a time
            if (destiny.Line > Position.Line + 1 || destiny.Column > Position.Column + 1 || destiny.Line < Position.Line - 1 || destiny.Column < Position.Column - 1)
                throw new ChessGameException(SystemMessages.InvalidMovement);

            // Cannot have piece at destination
            if (Board.ExistsPiece(destiny))
                throw new ChessGameException(SystemMessages.InvalidMovement);

            // Put yourself in check
            if (ValidateCheckMovement(destiny))
                throw new ChessGameException(SystemMessages.InvalidMovement);

            return true;
        }

        public override bool[,] PossibleMovements()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return "R";
        }

        #region Privates Methods

        private bool ValidateCheckMovement(Position destiny)
        {
            // TODO: Implement validate Check movement
            return false;
        }

        #endregion
    }
}
