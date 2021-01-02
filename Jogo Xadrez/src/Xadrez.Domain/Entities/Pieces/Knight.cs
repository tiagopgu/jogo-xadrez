using System;
using Xadrez.Domain.Entities.Enums;
using Xadrez.Domain.Utils;

namespace Xadrez.Domain.Entities.Pieces
{
    public class Knight : Piece
    {
        public Knight(Color color, Board board)
            : base(color, board)
        {

        }

        public override bool ValidMovement(Position destiny)
        {
            // Same Position
            if (destiny.Equals(Position))
                return false;

            // can only walk in an "L" shape
            return AllowedPosition(destiny);
        }

        public override string ToString()
        {
            return "C";
        }

        #region Privates Methods

        private bool AllowedPosition(Position destiny)
        {
            int totalLinesWalked = Math.Abs(destiny.Line - Position.Line);
            int totalColumnsWalked = Math.Abs(destiny.Column - Position.Column);

            if (totalLinesWalked == 2 && totalColumnsWalked == 1 && ChessHelper.PermittedPosition(Board, destiny, Color))
                return true;

            if (totalColumnsWalked == 2 && totalLinesWalked == 1 && ChessHelper.PermittedPosition(Board, destiny, Color))
                return true;

            return false;
        }

        #endregion
    }
}
