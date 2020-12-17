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

        public Piece GetPiece(Position position)
        {
            return _pieces[position.Line, position.Column];
        }
    }
}
