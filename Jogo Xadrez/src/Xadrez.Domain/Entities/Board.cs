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
            Position adjustedPosition = GetAdjustedPosition(position);

            _pieces[adjustedPosition.Line, adjustedPosition.Column] = piece;

            piece.Position = position;
        }

        public Piece GetPiece(Position position)
        {
            Position adjustedPosition = GetAdjustedPosition(position);

            return _pieces[adjustedPosition.Line, adjustedPosition.Column];
        }

        private Position GetAdjustedPosition(Position position)
        {
            byte line = (byte)(AmountLines - position.Line);
            byte column = (byte)(position.Column - 1);

            return new Position(line, column);
        }
    }
}
