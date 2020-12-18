using Xadrez.Domain.Entities;
using Xadrez.Domain.Entities.Enums;
using Xadrez.Domain.Entities.Pieces;

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

            PutPieces();
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
                Piece capturedPiece = RemovePiece(destiny);

                AddPiece(piece, destiny);
                
                piece.IncreaseMovement();
            }
        }

        #region Privates Methods

        private Position GetPosition(ChessPosition position)
        {
            return new Position(position.Line, (byte)(position.Column - 'a' + 1));
        }

        private void PutPieces()
        {
            // White Pieces

            AddPiece(new King(Color.White, Board), new ChessPosition(2, 'e'));
            AddPiece(new Rook(Color.White, Board), new ChessPosition(1, 'a'));
            AddPiece(new Rook(Color.White, Board), new ChessPosition(1, 'h'));
            AddPiece(new Pawn(Color.White, Board), new ChessPosition(3, 'h'));

            // Black Pieces
            AddPiece(new King(Color.Black, Board), new ChessPosition(6, 'e'));
            AddPiece(new Rook(Color.Black, Board), new ChessPosition(8, 'a'));
            AddPiece(new Rook(Color.Black, Board), new ChessPosition(7, 'd'));
            AddPiece(new Pawn(Color.Black, Board), new ChessPosition(7, 'c'));
        }

        #endregion
    }
}
