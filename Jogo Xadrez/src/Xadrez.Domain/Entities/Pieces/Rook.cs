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
            // Same Position
            if (destiny.Equals(Position))
                return false;

            if (WalkedHorizontallyOrVertically(destiny) == false)
                return false;

            return FreeAway(destiny);
        }

        public override string ToString()
        {
            return "T";
        }

        #region Privates Methods

        private bool WalkedHorizontallyOrVertically(Position destiny)
        {
            if (destiny.Line != Position.Line && destiny.Column != Position.Column)
                return false;

            return true;
        }

        private bool FreeAway(Position destiny)
        {
            Position currentPosition = new Position(0, 0);

            for (int i = GetInitialCursorValue(destiny); ContinueIteration(destiny, i); i = NextIteration(destiny, i))
            {
                currentPosition.Line = (destiny.Line == Position.Line) ? Position.Line : (byte)i;
                currentPosition.Column = (destiny.Column == Position.Column) ? Position.Column : (byte)i;

                if (ChessHelper.PermittedPosition(Board, currentPosition, Color) == false || TherePieceInThePreviousHouse(currentPosition))
                    return false;
            }

            return true;
        }

        private bool TherePieceInThePreviousHouse(Position currentPosition)
        {
            Position previousPosition;

            if (currentPosition.Line == Position.Line)
                previousPosition = new Position(currentPosition.Line, (byte)(currentPosition.Column + (currentPosition.Column > Position.Column ? -1 : 1)));
            else
                previousPosition = new Position((byte)(currentPosition.Line + (currentPosition.Line > Position.Line ? -1 : 1)), currentPosition.Column);

            return Board.ExistsPiece(previousPosition) && Board.GetPiece(previousPosition)?.Color != Color;
        }

        private int GetInitialCursorValue(Position destiny)
        {
            // Horizontal
            if (destiny.Line == Position.Line)
                return Position.Column + (destiny.Column > Position.Column ? 1 : -1);

            // Vertical
            return Position.Line + (destiny.Line > Position.Line ? 1 : -1);
        }

        private bool ContinueIteration(Position destiny, int currentIteration)
        {
            // Horizontal
            if (destiny.Line == Position.Line)
                return destiny.Column > Position.Column ? currentIteration <= destiny.Column : currentIteration >= destiny.Column;

            // Vertical
            return destiny.Line > Position.Line ? currentIteration <= destiny.Line : currentIteration >= destiny.Line;
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