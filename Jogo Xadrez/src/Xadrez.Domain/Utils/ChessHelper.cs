using Xadrez.Domain.Entities;
using Xadrez.Domain.Entities.Enums;

namespace Xadrez.Domain.Utils
{
    public static class ChessHelper
    {
        public static bool PermittedPosition(Board board, Position position, Color currentColor, ref bool stopAtTheNextIteration)
        {
            if (stopAtTheNextIteration)
                return false;

            Piece piece = board.GetPiece(position);

            if (piece != null)
            {
                if (piece.Color == currentColor)
                    return false;

                stopAtTheNextIteration = true;
            }

            return true;
        }
    }
}
