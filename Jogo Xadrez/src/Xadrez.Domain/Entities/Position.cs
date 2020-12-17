namespace Xadrez.Domain.Entities
{
    public class Position
    {
        public byte Line { get; set; }

        public byte Column { get; set; }

        public Position(byte line, byte column)
        {
            Line = line;
            Column = column;
        }

        public override string ToString()
        {
            return $"Position ({Line}, {Column})";
        }
    }
}
