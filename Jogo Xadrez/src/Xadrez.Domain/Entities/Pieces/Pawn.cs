using Xadrez.Domain.Entities.Enums;

namespace Xadrez.Domain.Entities.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Color color, Board board)
            : base(color, board)
        {

        }

        public override string ToString()
        {
            return "P";
        }
    }
}
