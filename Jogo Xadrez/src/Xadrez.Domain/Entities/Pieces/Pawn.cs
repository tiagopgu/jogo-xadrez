using System;
using Xadrez.Domain.Entities.Enums;

namespace Xadrez.Domain.Entities.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Color color, Board board)
            : base(color, board)
        {

        }

        public override bool ValidMovement(Position destiny)
        {
            if (destiny.Equals(Position))
                return false;

            if (WalkedForward(destiny) == false)
                return false;

            if (ValidAdvancedSpaces(destiny) == false)
                return false;

            if (IsCaptureMovement(destiny) && ValidCaptureMovement(destiny) == false)
                return false;

            if (FreeWay(destiny) == false)
                return false;

            return true;
        }

        public bool ValidateCaptureMovement(Position destiny, Piece pieceDestiny)
        {
            if (pieceDestiny != null && pieceDestiny.Color != Color)
            {
                int totalLineWalked = destiny.Line - Position.Line;
                int totalColumnWalked = destiny.Column - Position.Column;

                if (Color == Color.White && totalLineWalked == 1 && (totalColumnWalked == 1 || totalColumnWalked == -1))
                    return true;

                if (Color == Color.Black && totalLineWalked == -1 && (totalColumnWalked == 1 || totalColumnWalked == -1))
                    return true;
            }

            return false;
        }

        public override string ToString()
        {
            return "P";
        }

        #region Privates Methods

        private bool WalkedForward(Position destiny)
        {
            if (destiny.Line == Position.Line && destiny.Column != Position.Column)
                return false;

            return Color == Color.White ? destiny.Line > Position.Line : destiny.Line < Position.Line;
        }

        private bool ValidAdvancedSpaces(Position destiny)
        {
            int totalValidLines = AmountMoviments == 0 ? 2 : 1;
            int advancedTotalLines = Math.Abs(destiny.Line - Position.Line);

            return advancedTotalLines <= totalValidLines;
        }

        private bool IsCaptureMovement(Position destiny)
        {
            return destiny.Line != Position.Line && destiny.Column != Position.Column;
        }

        private bool ValidCaptureMovement(Position destiny)
        {
            if (IsCaptureMovement(destiny))
            {
                // In the capture movement, only one space can be advanced
                if (Color == Color.White)
                {
                    if (destiny.Line > Position.Line + 1 || destiny.Column > Position.Column + 1 || destiny.Column < Position.Column - 1)
                        return false;
                }
                else
                {
                    if (destiny.Line < Position.Line - 1 || destiny.Column > Position.Column + 1 || destiny.Column < Position.Column - 1)
                        return false;
                }

                if (Board.ExistsPiece(destiny) == false)
                    return false;

                if (Board.GetPiece(destiny).Color == Color)
                    return false;
            }

            return true;
        }

        private bool FreeWay(Position destiny)
        {
            if (destiny.Line != Position.Line && destiny.Column == Position.Column)
                for (int i = GetInitialCursorValue(); ContinueIteration(destiny, i); i = NextIteration(i))
                    if (Board.ExistsPiece(new Position((byte)i, Position.Column)))
                        return false;

            return true;
        }

        private int GetInitialCursorValue()
        {
            return Position.Line + (Color == Color.White ? 1 : -1);
        }

        private bool ContinueIteration(Position destiny, int currentIteration)
        {
            return Color == Color.White ? currentIteration <= destiny.Line : currentIteration >= destiny.Line;
        }

        private int NextIteration(int currentIteration)
        {
            return Color == Color.White ? ++currentIteration : --currentIteration;
        }

        #endregion
    }
}
