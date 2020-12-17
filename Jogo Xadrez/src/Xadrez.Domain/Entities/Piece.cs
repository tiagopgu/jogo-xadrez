using Xadrez.Domain.Entities.Enums;

namespace Xadrez.Domain.Entities
{
    public class Piece
    {
        public Position Position { get; set; }

        public Color Color { get; protected set; }

        public Board Board { get; protected set; }

        public uint AmountMoviments { get; protected set; }

        public Piece(Position position, Color color, Board board)
        {
            Position = position;
            Color = color;
            Board = board;
        }
    }
}
