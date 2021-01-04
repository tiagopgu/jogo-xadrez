using Xadrez.Domain.Entities.Exceptions;

namespace Xadrez.Domain.Entities
{
    public class Board
    {
        private readonly Piece[,] _pieces;

        public byte AmountLines { get; private set; }

        public byte AmountColumns { get; private set; }

        public Board(byte amountLines, byte amountColumns)
        {
            AmountLines = amountLines;
            AmountColumns = amountColumns;

            _pieces = new Piece[AmountLines, AmountColumns];
        }

        public void AddPiece(Piece piece, Position position)
        {
            if (ExistsPiece(position))
                throw new BoardException("A piece already exists in the informed position");

            Position adjustedPosition = GetAdjustedPosition(position);

            _pieces[adjustedPosition.Line, adjustedPosition.Column] = piece;

            piece.Position = position;
        }

        public Piece RemovePiece(Position position)
        {
            Piece piece = GetPiece(position);

            if (piece != null)
            {
                piece.Position = null;

                Position adjustedPosition = GetAdjustedPosition(position);
                
                _pieces[adjustedPosition.Line, adjustedPosition.Column] = null;
            }

            return piece;
        }

        public Piece GetPiece(Position position)
        {
            Position adjustedPosition = GetAdjustedPosition(position);

            return _pieces[adjustedPosition.Line, adjustedPosition.Column];
        }

        public bool ExistsPiece(Position position)
        {
            return GetPiece(position) != null;
        }

        #region Privates Methods

        private Position GetAdjustedPosition(Position position)
        {
            if (ValidPosition(position) == false)
                throw new BoardException("The reported position is invalid");

            byte line = (byte)(AmountLines - position.Line);
            byte column = (byte)(position.Column - 1);

            return new Position(line, column);
        }

        public bool ValidPosition(Position position)
        {
            bool validLine = position.Line > 0 && position.Line <= AmountLines;
            bool validColumn = position.Column > 0 && position.Column <= AmountColumns;

            return validLine && validColumn;
        }

        #endregion
    }
}
