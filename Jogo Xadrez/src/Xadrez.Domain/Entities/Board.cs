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
            byte posicaoLine = (byte)(AmountLines - position.Line);
            byte posicaoColumn = (byte)(position.Column - 1);

            _pieces[posicaoLine, posicaoColumn] = piece;

            piece.Position = position;
        }

        public Piece GetPiece(Position position)
        {
            return _pieces[position.Line, position.Column];
        }
    }
}
