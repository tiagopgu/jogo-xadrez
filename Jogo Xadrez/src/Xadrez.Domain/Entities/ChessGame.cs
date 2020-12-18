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

        public void AddPiece(Piece piece, ChessPosition position)
        {
            Position newPosition = GetPosition(position);

            Board.AddPiece(piece, newPosition);
        }

        public Piece RemovePiece(ChessPosition position)
        {
            Position newPosition = GetPosition(position);

            return Board.RemovePiece(newPosition);
        }

        public Piece GetPiece(ChessPosition position)
        {
            Position newPosition = GetPosition(position);

            return Board.GetPiece(newPosition);
        }

        public void MovePiece(ChessPosition origin, ChessPosition destiny)
        {
            Piece piece = RemovePiece(origin);

            if (piece != null)
            {
                AddPiece(piece, destiny);
                
                piece.IncreaseMovement();
            }
        }

        #region Privates Methods

        private Position GetPosition(ChessPosition position)
        {
            return new Position(position.Line, (byte)(position.Column - 'a' + 1));
        }

        #endregion
    }
}
