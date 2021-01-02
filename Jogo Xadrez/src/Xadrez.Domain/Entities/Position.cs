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
            return $"({Line}, {Column})";
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (obj is Position == false)
                return false;

            Position positionTest = obj as Position;

            return positionTest.Line == Line && positionTest.Column == Column;
        }
    }
}
