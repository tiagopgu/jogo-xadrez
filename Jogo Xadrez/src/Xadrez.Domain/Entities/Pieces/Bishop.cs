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
            // Same Position
            if (destiny.Equals(Position))
                return false;

            if (WalkedDiagonally(destiny) == false)
                return false;

            return FreeWay(destiny);
        }

        public override string ToString()
        {
            return "B";
        }

        #region Privates Methods

        private bool WalkedDiagonally(Position destiny)
        {
            int totalLinesMoved = Math.Abs(destiny.Line - Position.Line);
            int totalColumnsMoved = Math.Abs(destiny.Column - Position.Column);

            return totalLinesMoved == totalColumnsMoved;
        }

        private bool FreeWay(Position destiny)
        {
            Position currentPosition = new Position(0, 0);

            for (int i = GetInitialCursorValue(destiny); ContinueIteration(destiny, i); i = NextIteration(destiny, i))
            {
                for (int j = GetInitialCursorValue(destiny, true); ContinueIteration(destiny, j, true); j = NextIteration(destiny, j, true))
                {
                    currentPosition.Line = (byte)i;
                    currentPosition.Column = (byte)j;

                    if (WalkedDiagonally(currentPosition) && (ChessHelper.PermittedPosition(Board, currentPosition, Color) == false || TherePieceInThePreviousHouse(currentPosition)))
                        return false;
                }
            }

            return true;
        }

        private bool TherePieceInThePreviousHouse(Position currentPosition)
        {
            Position previousPosition;

            // Up
            if (currentPosition.Line > Position.Line)
                previousPosition = new Position((byte)(currentPosition.Line - 1), (byte)(currentPosition.Column + (currentPosition.Column > Position.Column ? -1 : 1)));
            else // Down
                previousPosition = new Position((byte)(currentPosition.Line + 1), (byte)(currentPosition.Column + (currentPosition.Column < Position.Column ? 1 : -1)));

            return Board.ExistsPiece(previousPosition) && Board.GetPiece(previousPosition)?.Color != Color;
        }

        private int GetInitialCursorValue(Position destiny, bool secondCursor = false)
        {
            // Up
            if (destiny.Line > Position.Line)
            {
                // At the rigth
                if (destiny.Column > Position.Column)
                    return (secondCursor) ? Position.Column + 1 : Position.Line + 1;

                // At the left
                return (secondCursor) ? Position.Column - 1 : Position.Line + 1;
            }

            // Down

            // At the left
            if (destiny.Column < Position.Column)
                return (secondCursor) ? Position.Column - 1 : Position.Line - 1;

            // At the rigth
            return (secondCursor) ? Position.Column + 1 : Position.Line - 1;
        }

        private bool ContinueIteration(Position destiny, int currentIteration, bool secondIteration = false)
        {
            // Up
            if (destiny.Line > Position.Line)
            {
                // At the rigth
                if (destiny.Column > Position.Column)
                    return (secondIteration) ? currentIteration <= destiny.Column : currentIteration <= destiny.Line;

                // At the left
                return (secondIteration) ? currentIteration >= destiny.Column : currentIteration <= destiny.Line;
            }

            // Down

            // At the left
            if (destiny.Column < Position.Column)
                return (secondIteration) ? currentIteration >= destiny.Column : currentIteration >= destiny.Line;

            // At the right
            return (secondIteration) ? currentIteration <= destiny.Column : currentIteration >= destiny.Line;
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
