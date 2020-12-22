using Xadrez.Domain.Entities.Enums;
using Xadrez.Domain.Entities.Exceptions;

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

            for (byte i = 0; i < Board.AmountLines; i++)
            {
                for (byte j = 0; j < Board.AmountColumns; j++)
                {
                    try
                    {
                        position.Line = (byte)(i + 1);
                        position.Column = (byte)(j + 1);

                        ValidMovement(position);

                        matrixMovements[i, j] = true;
                    }
                    catch (ChessGameException)
                    {
                        matrixMovements[i, j] = false;
                    }
                }
            }

            return matrixMovements;
        }
    }
}
