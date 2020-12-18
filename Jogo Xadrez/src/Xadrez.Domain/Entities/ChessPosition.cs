namespace Xadrez.Domain.Entities
{
    public class ChessPosition
    {
        public byte Line { get; set; }

        public char Column { get; set; }

        public ChessPosition(byte line, char column)
        {
            Line = line;
            Column = column;
        }

        public override string ToString()
        {
            return $"({Line}, {Column})";
        }
    }
}
