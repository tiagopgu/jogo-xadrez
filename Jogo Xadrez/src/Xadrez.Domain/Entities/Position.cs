namespace Xadrez.Domain.Entities
{
    public class Position
    {
        public sbyte Line { get; set; }

        public sbyte Column { get; set; }

        public Position(sbyte line, sbyte column)
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
