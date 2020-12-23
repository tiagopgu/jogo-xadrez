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

        public virtual bool[,] PossibleMovements()
        {
            bool[,] matrixMovements = new bool[Board.AmountLines, Board.AmountColumns];
            Position position = new Position(0, 0);

            for (int i = Board.AmountLines - 1; i > 0; i--)
            {
                for (int j = 0; j < Board.AmountColumns; j++)
                {
                    position.Line = (byte)(Board.AmountLines - i);
                    position.Column = (byte)(j + 1);

                    if (ValidMovement(position))
                        matrixMovements[i, j] = true;
                }
            }

            return matrixMovements;
        }
    }
}
