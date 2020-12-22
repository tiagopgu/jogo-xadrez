using Xadrez.Domain.Entities.Enums;
using Xadrez.Domain.Entities.Exceptions;
using Xadrez.Domain.Utils;

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
            if (Color == Color.White)
            {
                // Can't go back
                if (destiny.Line <= Position.Line)
                    throw new ChessGameException(SystemMessages.InvalidMovement);

                // You can only advance a maximum of two spaces
                if (destiny.Line > Position.Line + 2)
                    throw new ChessGameException(SystemMessages.InvalidMovement);

                // You can only advance more than one space in the first move
                if (AmountMoviments > 0 && destiny.Line >= Position.Line + 2)
                    throw new ChessGameException(SystemMessages.InvalidMovement);

                // In the capture movement, only one space can be advanced
                if ((destiny.Line != Position.Line && destiny.Column != Position.Column) && (destiny.Line > Position.Line + 1 || destiny.Column > Position.Column + 1 || destiny.Column < Position.Column - 1))
                    throw new ChessGameException(SystemMessages.InvalidMovement);
            }
            else
            {
                // Can't go back
                if (destiny.Line >= Position.Line)
                    throw new ChessGameException(SystemMessages.InvalidMovement);

                // You can only advance a maximum of two spaces
                if (destiny.Line < Position.Line - 2)
                    throw new ChessGameException(SystemMessages.InvalidMovement);

                // You can only advance more than one space in the first move
                if (AmountMoviments > 0 && destiny.Line <= Position.Line - 2)
                    throw new ChessGameException(SystemMessages.InvalidMovement);

                // In the capture movement, only one space can be advanced
                if ((destiny.Line != Position.Line && destiny.Column != Position.Column) && (destiny.Line < Position.Line - 1 || destiny.Column > Position.Column + 1 || destiny.Column < Position.Column - 1))
                    throw new ChessGameException(SystemMessages.InvalidMovement);
            }

            // Cannot move horizontally
            if (destiny.Line == Position.Line && destiny.Column != Position.Column)
                throw new ChessGameException(SystemMessages.InvalidMovement);

            // Capture movement is only valid if piece exists at destination
            if ((destiny.Line != Position.Line && destiny.Column != Position.Column) && (Board.ExistsPiece(destiny) == false))
                throw new ChessGameException(SystemMessages.InvalidMovement);

            // Path to destination is not clear
            if (FreeWay(destiny) == false)
                throw new ChessGameException(SystemMessages.InvalidMovement);

            return true;
        }

        public override bool[,] PossibleMovements()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return "P";
        }

        #region Privates Methods

        private bool FreeWay(Position destiny)
        {
            if (Color == Color.White)
            {
                // check parts in advance
                if (destiny.Line != Position.Line && destiny.Column == Position.Column)
                    for (byte i = (byte)(Position.Line + 1); i <= destiny.Line; i++)
                        if (Board.ExistsPiece(new Position(i, Position.Column)))
                            return false;
            }
            else
            {
                if (destiny.Line != Position.Line && destiny.Column == Position.Column)
                    for (byte i = (byte)(Position.Line - 1); i >= destiny.Line; i--)
                        if (Board.ExistsPiece(new Position(i, Position.Column)))
                            return false;
            }

            // Check capture movement
            if ((destiny.Line != Position.Line && destiny.Column != Position.Column) && (Board.GetPiece(destiny).Color == Color))
                return false;

            return true;
        }

        #endregion
    }
}
