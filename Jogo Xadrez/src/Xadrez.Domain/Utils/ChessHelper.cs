using Xadrez.Domain.Entities;
using Xadrez.Domain.Entities.Enums;

namespace Xadrez.Domain.Utils
{
    public static class ChessHelper
    {
        public static bool PermittedPosition(Board board, Position position, Color currentColor)
        {
            Piece piece = board.GetPiece(position);

            if (piece != null && piece.Color == currentColor)
                return false;

            return true;
        }
    }
}
