using Xadrez.Domain.Entities.Enums;

namespace Xadrez.Domain.Entities
{
    public abstract class Piece
    {
        public Position Position { get; set; }

        public Color Color { get; protected set; }

        public Board Board { get; protected set; }

        public uint AmountMoviments { get; protected set; }

        public Piece(Color color, Board board)
        {
            Color = color;
            Board = board;
        }

        public void IncreaseMovement()
        {
            AmountMoviments++;
        }

        public abstract bool ValidMovement(Position destiny);
    }
}
