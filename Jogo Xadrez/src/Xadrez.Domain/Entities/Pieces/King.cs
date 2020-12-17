using Xadrez.Domain.Entities.Enums;

namespace Xadrez.Domain.Entities.Pieces
{
    public class King : Piece
    {
        public King(Color color, Board board)
            : base(color, board)
        {

        }

        public override string ToString()
        {
            return "R";
        }
    }
}
