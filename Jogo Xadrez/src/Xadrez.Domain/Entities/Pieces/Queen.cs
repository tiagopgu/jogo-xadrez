using System;
using Xadrez.Domain.Entities.Enums;
using Xadrez.Domain.Utils;

namespace Xadrez.Domain.Entities.Pieces
{
    public class Queen : Piece
    {
        public Queen(Color color, Board board)
            : base(color, board)
        {

        }

        public override bool ValidMovement(Position destiny)
        {
            // Same Position
            if (destiny.Equals(Position))
                return false;

            if (WalkedStraight(destiny) == false)
                return false;

            return FreeAway(destiny);
        }

        public override string ToString()
        {
            return "D";
        }

        #region Private Methods

        private bool WalkedStraight(Position destiny)
        {
            if (destiny.Line == Position.Line && destiny.Column != Position.Column)
                return true;

            if (destiny.Line != Position.Line && destiny.Column == Position.Column)
                return true;

            var diffLine = Math.Abs(destiny.Line - Position.Line);
            var diffColumn = Math.Abs(destiny.Column - Position.Column);

            return diffLine == diffColumn;
        }

        private bool FreeAway(Position destiny)
        {
            Position currentPosition = new Position(0, 0);

            // Horizontal or vertical path
            if (destiny.Line == Position.Line || destiny.Column == Position.Column)
            {
                for (int i = GetInitialCursorValue(destiny); ContinueIteration(destiny, i); i = NextIteration(destiny, i))
                {
                    currentPosition.Line = (destiny.Line == Position.Line) ? Position.Line : (byte)i;
                    currentPosition.Column = (destiny.Column == Position.Column) ? Position.Column : (byte)i;

                    if (ChessHelper.PermittedPosition(Board, currentPosition, Color) == false || TherePieceInThePreviousHouse(currentPosition))
                        return false;
                }
            }
            else // Diagonal path
            {
                for (int i = GetInitialCursorValue(destiny); ContinueIteration(destiny, i); i = NextIteration(destiny, i))
                {
                    for (int j = GetInitialCursorValue(destiny, true); ContinueIteration(destiny, j, true); j = NextIteration(destiny, j, true))
                    {
                        // Check if you walked in a straight line
                        currentPosition.Line = (byte)i;
                        currentPosition.Column = (byte)j;

                        if (WalkedStraight(currentPosition) && (ChessHelper.PermittedPosition(Board, currentPosition, Color) == false || TherePieceInThePreviousHouse(currentPosition)))
                            return false;
                    }
                }
            }

            return true;
        }

        private bool TherePieceInThePreviousHouse(Position currentPosition)
        {
            Position previousPosition;

            // Horizontal Path
            if (currentPosition.Line == Position.Line)
                previousPosition = new Position(currentPosition.Line, (byte)(currentPosition.Column + (currentPosition.Column > Position.Column ? -1 : 1)));
            else if (currentPosition.Column == Position.Column) // Vertical Path
                previousPosition = new Position((byte)(currentPosition.Line + (currentPosition.Line > Position.Line ? -1 : 1)), currentPosition.Column);
            else // Diagonal Path
            {
                if (currentPosition.Line > Position.Line) // To Up
                    previousPosition = new Position((byte)(currentPosition.Line - 1), (byte)(currentPosition.Column + (currentPosition.Column > Position.Column ? -1 : 1)));
                else // To Down
                    previousPosition = new Position((byte)(currentPosition.Line + 1), (byte)(currentPosition.Column + (currentPosition.Column < Position.Column ? 1 : -1)));
            }

            return Board.ExistsPiece(previousPosition) && Board.GetPiece(previousPosition)?.Color != Color;
        }

        private int GetInitialCursorValue(Position destiny, bool secondCursor = false)
        {
            // Horizontal path
            if (destiny.Line == Position.Line)
            {
                // To the rigth
                if (destiny.Column > Position.Column)
                    return Position.Column + 1;

                // To the left
                return Position.Column - 1;
            }

            // Vertical path
            if (destiny.Column == Position.Column)
            {
                // Up
                if (destiny.Line > Position.Line)
                    return Position.Line + 1;

                // Down
                return Position.Line - 1;
            }

            // Diagonal path Up
            if (destiny.Line > Position.Line)
            {
                // To the right
                if (destiny.Column > Position.Column)
                    return (secondCursor) ? Position.Column + 1 : Position.Line + 1;

                // To the left
                return (secondCursor) ? Position.Column - 1 : Position.Line + 1;
            }

            // Diagonal path Down
            if (destiny.Column < Position.Column) // To the left
                return (secondCursor) ? Position.Column - 1 : Position.Line - 1;

            // To the right
            return (secondCursor) ? Position.Column + 1 : Position.Line - 1;
        }

        private bool ContinueIteration(Position destiny, int currentIteration, bool secondCondition = false)
        {
            // Horizontal path
            if (destiny.Line == Position.Line)
            {
                // To the right
                if (destiny.Column > Position.Column)
                    return currentIteration <= destiny.Column;

                // To the left
                return currentIteration >= destiny.Column;
            }

            // Vertical path
            if (destiny.Column == Position.Column)
            {
                // Up
                if (destiny.Line > Position.Line)
                    return currentIteration <= destiny.Line;

                // Down
                return currentIteration >= destiny.Line;
            }

            // Diagonal path Up
            if (destiny.Line > Position.Line)
            {
                // To the right
                if (destiny.Column > Position.Column)
                    return (secondCondition) ? currentIteration <= destiny.Column : currentIteration <= destiny.Line;

                // To the left
                return (secondCondition) ? currentIteration >= destiny.Column : currentIteration <= destiny.Line;
            }

            // Diagonal path  Down

            // To the left
            if (destiny.Column < Position.Column)
                return (secondCondition) ? currentIteration >= destiny.Column : currentIteration >= destiny.Line;

            // To the right
            return (secondCondition) ? currentIteration <= destiny.Column : currentIteration >= destiny.Line;
        }

        private int NextIteration(Position destiny, int currentIteration, bool secontIterator = false)
        {
            if (destiny.Line == Position.Line && destiny.Column < Position.Column)
                return --currentIteration;

            if (destiny.Column == Position.Column && destiny.Line < Position.Line)
                return --currentIteration;

            if (destiny.Line < Position.Line && secontIterator == false)
                return --currentIteration;

            if (destiny.Column < Position.Column && secontIterator)
                return --currentIteration;

            return ++currentIteration;
        }

        #endregion
    }
}
