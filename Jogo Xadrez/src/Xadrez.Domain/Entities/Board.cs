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

        public void AddPiece(Piece piece)
        {
            byte posicaoLine = (byte)(AmountLines - piece.Position.Line);
            byte posicaoColumn = (byte)(piece.Position.Column - 1);

            _pieces[posicaoLine, posicaoColumn] = piece;
        }

        public Piece GetPiece(Position position)
        {
            return _pieces[position.Line, position.Column];
        }
    }
}
