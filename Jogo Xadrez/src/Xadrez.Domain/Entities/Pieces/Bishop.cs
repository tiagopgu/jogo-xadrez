using System;
using Xadrez.Domain.Entities.Enums;
using Xadrez.Domain.Utils;

namespace Xadrez.Domain.Entities.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(Color color, Board board)
            : base(color, board)
        {

        }

        public override bool ValidMovement(Position destiny)
        {
            // It cannot be move to the same position
            if (destiny.Line == Position.Line && destiny.Column == Position.Column)
                return false;

            // Can only move diagonally
            int totalLinesMoved = Math.Abs(destiny.Line - Position.Line);
            int totalColumnsMoved = Math.Abs(destiny.Column - Position.Column);

            if (totalLinesMoved != totalColumnsMoved)
                return false;

            return FreeWay(destiny);
        }

        public override string ToString()
        {
            return "B";
        }

        #region Privates Methods

        private bool FreeWay(Position destiny)
        {
            Position currentPosition = new Position(0, 0);
            bool stopAtTheNextIteration = false;

            for (int i = GetInitialCursorValue(destiny); ContinueIteration(destiny, i); i = NextIteration(destiny, i))
            {
                for (int j = GetInitialCursorValue(destiny, true); ContinueIteration(destiny, j, true); j = NextIteration(destiny, j, true))
                {
                    int totalLinesWalked = Math.Abs(i - Position.Line);
                    int totalColumnsWalked = Math.Abs(j - Position.Column);

                    if (totalLinesWalked == totalColumnsWalked)
                    {
                        currentPosition.Line = (byte)i;
                        currentPosition.Column = (byte)j;

                        if (ChessHelper.PermittedPosition(Board, currentPosition, Color, ref stopAtTheNextIteration) == false)
                            return false;
                    }
                }
            }

            return true;
        }

        private int GetInitialCursorValue(Position destiny, bool secondCursor = false)
        {
            if (destiny.Line > Position.Line) // Up
            {
                if (destiny.Column > Position.Column) // At the rigth
                    return (secondCursor) ? Position.Column + 1 : Position.Line + 1;
                else // At the left
                    return (secondCursor) ? Position.Column - 1 : Position.Line + 1;
            }
            else // Down
            {
                if (destiny.Column < Position.Column) // At the left
                    return (secondCursor) ? Position.Column - 1 : Position.Line - 1;
                else
                    return (secondCursor) ? Position.Column + 1 : Position.Line - 1;
            }
        }

        private bool ContinueIteration(Position destiny, int currentIteration, bool secondIteration = false)
        {
            if (destiny.Line > Position.Line) // Up
            {
                if (destiny.Column > Position.Column) // At the rigth
                    return (secondIteration) ? currentIteration <= destiny.Column : currentIteration <= destiny.Line;
                else // At the left
                    return (secondIteration) ? currentIteration >= destiny.Column : currentIteration <= destiny.Line;
            }
            else // Down
            {
                if (destiny.Column < Position.Column) // At the left
                    return (secondIteration) ? currentIteration >= destiny.Column : currentIteration >= destiny.Line;
                else
                    return (secondIteration) ? currentIteration <= destiny.Column : currentIteration >= destiny.Line;
            }
        }

        private int NextIteration(Position destiny, int currentIteration, bool secondIterator = false)
        {
            if (destiny.Line > Position.Line && destiny.Column < Position.Column && secondIterator)
                return --currentIteration;

            if (destiny.Line < Position.Line && destiny.Column < Position.Column)
                return --currentIteration;

            if (destiny.Line < Position.Line && destiny.Column > Position.Column && secondIterator == false)
                return --currentIteration;

            return ++currentIteration;
        }

        #endregion
    }
}
