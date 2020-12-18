using Xadrez.Domain.Entities;
using Xadrez.Domain.Entities.Enums;

namespace Xadrez.Domain
{
    public class ChessGame
    {
        public Board Board { get; private set; }
        public int Shift { get; private set; }
        public Color CurrentPlayer { get; private set; }

        public ChessGame()
        {
            Board = new Board(8, 8);
            Shift = 1;
            CurrentPlayer = Color.White;
        }

        public void AddPiece(Piece piece, byte line, char charColumn)
        {
            Position position = GetPosition(line, charColumn);

            Board.AddPiece(piece, position);
        }

        public Piece RemovePiece(byte line, char charColumn)
        {
            Position position = GetPosition(line, charColumn);

            return Board.RemovePiece(position);
        }

        public Piece GetPiece(byte line, char charColumn)
        {
            Position position = GetPosition(line, charColumn);

            return Board.GetPiece(position);
        }

        #region Privates Methods

        private Position GetPosition(byte line, char charColumn)
        {
            return new Position(line, (byte)(charColumn - 'a' + 1));
        }

        #endregion
    }
}
