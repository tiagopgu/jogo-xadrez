using Xadrez.Domain.Entities.Enums;

namespace Xadrez.Domain.Entities.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(Color color, Board board)
            : base(color, board)
        {

        }

        public override string ToString()
        {
            return "B";
        }
    }
}
