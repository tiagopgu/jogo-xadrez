using Xadrez.Domain.Entities.Enums;
using Xadrez.Domain.Utils;

namespace Xadrez.Domain.Entities.Pieces
{
    public class Rook : Piece
    {
        public Rook(Color color, Board board)
            : base(color, board)
        {

        }

        public override bool ValidMovement(Position destiny)
        {
            // It cannot be move to the same position
            if (destiny.Line == Position.Line && destiny.Column == Position.Column)
                return false;

            // You can only walk horizontally or vertically.
            if (destiny.Line != Position.Line && destiny.Column != Position.Column)
                return false;

            return FreeAway(destiny);
        }

        public override string ToString()
        {
            return "T";
        }

        #region Privates Methods

        private bool FreeAway(Position destiny)
        {
            Position currentPosition = new Position(0, 0);
            bool stopAtTheNexIteration = false;

            for (int i = GetInitialCursorValue(destiny); ContinueIteration(destiny, i); i = NextIteration(destiny, i))
            {
                currentPosition.Line = (destiny.Line == Position.Line) ? Position.Line : (byte)i;
                currentPosition.Column = (destiny.Column == Position.Column) ? Position.Column : (byte)i;

                if (ChessHelper.PermittedPosition(Board, currentPosition, Color, ref stopAtTheNexIteration) == false)
                    return false;
            }

            return true;
        }

        private int GetInitialCursorValue(Position destiny)
        {
            if (destiny.Line == Position.Line) // Horizontal
            {
                if (destiny.Column > Position.Column) // At the right
                    return Position.Column + 1;
                else // At the left
                    return Position.Column - 1;
            }
            else // Vertical
            {
                if (destiny.Line > Position.Line) // Up
                    return Position.Line + 1;
                else // Down
                    return Position.Line - 1;
            }
        }

        private bool ContinueIteration(Position destiny, int currentIteration)
        {
            if (destiny.Line == Position.Line) // Horizontal
            {
                if (destiny.Column > Position.Column) // At the right
                    return currentIteration <= destiny.Column;
                else // At the left
                    return currentIteration >= destiny.Column;
            }
            else // Vertical
            {
                if (destiny.Line > Position.Line) // Up
                    return currentIteration <= destiny.Line;
                else // Down
                    return currentIteration >= destiny.Line;
            }
        }

        private int NextIteration(Position destiny, int currentIteration)
        {
            if (destiny.Line == Position.Line && destiny.Column < Position.Column)
                return --currentIteration;

            if (destiny.Column == Position.Column && destiny.Line < Position.Line)
                return --currentIteration;

            return ++currentIteration;
        }

        #endregion
    }
}