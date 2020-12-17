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

        public Piece[,] Print()
        {
            var obj = _pieces.Clone() as Piece[,];

            return obj;
        }
    }
}
