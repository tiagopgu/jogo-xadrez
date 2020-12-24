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
            // It cannot be move to the same position
            if (destiny.Line == Position.Line && destiny.Column == Position.Column)
                return false;

            // Can only walk in a straight line in any direction
            if (destiny.Line != Position.Line && destiny.Column != Position.Column)
            {
                var diffLine = Math.Abs(destiny.Line - Position.Line);
                var diffColumn = Math.Abs(destiny.Column - Position.Column);

                if (diffLine != diffColumn)
                    return false;
            }

            return FreeAway(destiny);
        }

        public override string ToString()
        {
            return "D";
        }

        #region Private Methods

        private bool FreeAway(Position destiny)
        {
            Position currentPosition = new Position(0, 0);
            bool stopAtTheCurrentPosition = false;

            // Horizontal or vertical path
            if (destiny.Line == Position.Line || destiny.Column == Position.Column)
            {
                for (int i = GetInitialCursorValue(destiny); ContinueIteration(destiny, i); i = NextIteration(destiny, i))
                {
                    currentPosition.Line = (destiny.Line == Position.Line) ? Position.Line : (byte)i;
                    currentPosition.Column = (destiny.Column == Position.Column) ? Position.Column : (byte)i;

                    if (ChessHelper.PermittedPosition(Board, currentPosition, Color, ref stopAtTheCurrentPosition) == false)
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
                        int totalLineWalked = Math.Abs(i - Position.Line);
                        int totalColumnWalked = Math.Abs(j - Position.Column);

                        if (totalLineWalked == totalColumnWalked)
                        {
                            currentPosition.Line = (byte)i;
                            currentPosition.Column = (byte)j;

                            if (ChessHelper.PermittedPosition(Board, currentPosition, Color, ref stopAtTheCurrentPosition) == false)
                                return false;
                        }
                    }
                }
            }

            return true;
        }

        private int GetInitialCursorValue(Position destiny, bool secondCursor = false)
        {
            // Horizontal path
            if (destiny.Line == Position.Line)
            {
                if (destiny.Column > Position.Column) // To the rigth
                    return Position.Column + 1;
                else // To the left
                    return Position.Column - 1;
            }

            // Vertical path
            else if (destiny.Column == Position.Column)
            {
                if (destiny.Line > Position.Line) // Up
                    return Position.Line + 1;
                else // Down
                    return Position.Line - 1;
            }

            // Diagonal path
            else
            {
                if (destiny.Line > Position.Line) // Up
                {
                    if (destiny.Column > Position.Column) // To the right
                        return (secondCursor) ? Position.Column + 1 : Position.Line + 1;
                    else // To the left
                        return (secondCursor) ? Position.Column - 1 : Position.Line + 1;
                }
                else // Down
                {
                    if (destiny.Column < Position.Column) // To the left
                        return (secondCursor) ? Position.Column - 1 : Position.Line - 1;
                    else // To the right
                        return (secondCursor) ? Position.Column + 1 : Position.Line - 1;
                }
            }
        }

        private bool ContinueIteration(Position destiny, int currentIteration, bool secondCondition = false)
        {
            if (destiny.Line == Position.Line) // Horizontal path
            {
                if (destiny.Column > Position.Column) // To the right
                    return currentIteration <= destiny.Column;
                else // To the left
                    return currentIteration >= destiny.Column;
            }
            else if (destiny.Column == Position.Column) // Vertical path
            {
                if (destiny.Line > Position.Line) // Up
                    return currentIteration <= destiny.Line;
                else // Down
                    return currentIteration >= destiny.Line;
            } else // Diagonal path
            {
                if (destiny.Line > Position.Line) // Up
                {
                    if (destiny.Column > Position.Column) // To the right
                        return (secondCondition) ? currentIteration <= destiny.Column : currentIteration <= destiny.Line;
                    else // To the left
                        return (secondCondition) ? currentIteration >= destiny.Column : currentIteration <= destiny.Line;
                } else // Down
                {
                    if (destiny.Column < Position.Column) // To the left
                        return (secondCondition) ? currentIteration >= destiny.Column : currentIteration >= destiny.Line;
                    else
                        return (secondCondition) ? currentIteration <= destiny.Column : currentIteration >= destiny.Line;
                }
            }
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
