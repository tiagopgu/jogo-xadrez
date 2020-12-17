using Xadrez.Domain.Entities.Enums;

namespace Xadrez.Domain.Entities.Pieces
{
    public class Knight : Piece
    {
        public Knight(Color color, Board board)
            : base(color, board)
        {

        }

        public override string ToString()
        {
            return "C";
        }
    }
}
