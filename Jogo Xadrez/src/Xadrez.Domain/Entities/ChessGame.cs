using Xadrez.Domain.Entities;
using Xadrez.Domain.Entities.Enums;
using Xadrez.Domain.Entities.Exceptions;
using Xadrez.Domain.Entities.Pieces;
using Xadrez.Domain.Utils;

namespace Xadrez.Domain
{
    public class ChessGame
    {
        public Board Board { get; private set; }
        public byte Shift { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool GameEnded { get; private set; }

        public ChessGame()
        {
            Board = new Board(8, 8);
            Shift = 1;
            CurrentPlayer = Color.White;

            PutWhitePieces();
            PutBlackPieces();
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
            if (ValidPositionsForMovement(origin, destiny))
            {
                Piece piece = GetPiece(origin);

                if (piece == null)
                    throw new ChessGameException(SystemMessages.NoPiece);

                if (piece.ValidMovement(GetPosition(destiny)))
                {
                    RemovePiece(origin);

                    Piece capturedPiece = RemovePiece(destiny);

                    AddPiece(piece, destiny);

                    piece.IncreaseMovement();
                }
            }
        }

        #region Privates Methods

        private Position GetPosition(ChessPosition position)
        {
            Position newPosition = new Position(position.Line, (byte)(position.Column - 'a' + 1));

            Board.ValidatePosition(newPosition);

            return newPosition;
        }

        private bool ValidPositionsForMovement(ChessPosition origin, ChessPosition destiny)
        {
            return GetPosition(origin) != null && GetPosition(destiny) != null;
        }

        private void PutWhitePieces()
        {
            // Pawns

            AddPiece(new Pawn(Color.White, Board), new ChessPosition(2, 'a'));
            AddPiece(new Pawn(Color.White, Board), new ChessPosition(2, 'b'));
            AddPiece(new Pawn(Color.White, Board), new ChessPosition(2, 'c'));
            AddPiece(new Pawn(Color.White, Board), new ChessPosition(2, 'd'));
            AddPiece(new Pawn(Color.White, Board), new ChessPosition(2, 'e'));
            AddPiece(new Pawn(Color.White, Board), new ChessPosition(2, 'f'));
            AddPiece(new Pawn(Color.White, Board), new ChessPosition(2, 'g'));
            AddPiece(new Pawn(Color.White, Board), new ChessPosition(2, 'h'));

            // Rooks
            AddPiece(new Rook(Color.White, Board), new ChessPosition(1, 'a'));
            AddPiece(new Rook(Color.White, Board), new ChessPosition(1, 'h'));

            // Knight
            AddPiece(new Knight(Color.White, Board), new ChessPosition(1, 'b'));
            AddPiece(new Knight(Color.White, Board), new ChessPosition(1, 'g'));

            // Bishops
            AddPiece(new Bishop(Color.White, Board), new ChessPosition(1, 'c'));
            AddPiece(new Bishop(Color.White, Board), new ChessPosition(1, 'f'));

            // King
            AddPiece(new King(Color.White, Board), new ChessPosition(1, 'd'));

            // Queen
            AddPiece(new Queen(Color.White, Board), new ChessPosition(1, 'e'));
        }

        private void PutBlackPieces()
        {
            // Pawns

            AddPiece(new Pawn(Color.Black, Board), new ChessPosition(7, 'a'));
            AddPiece(new Pawn(Color.Black, Board), new ChessPosition(7, 'b'));
            AddPiece(new Pawn(Color.Black, Board), new ChessPosition(7, 'c'));
            AddPiece(new Pawn(Color.Black, Board), new ChessPosition(7, 'd'));
            AddPiece(new Pawn(Color.Black, Board), new ChessPosition(7, 'e'));
            AddPiece(new Pawn(Color.Black, Board), new ChessPosition(7, 'f'));
            AddPiece(new Pawn(Color.Black, Board), new ChessPosition(7, 'g'));
            AddPiece(new Pawn(Color.Black, Board), new ChessPosition(7, 'h'));

            // Rooks
            AddPiece(new Rook(Color.Black, Board), new ChessPosition(8, 'a'));
            AddPiece(new Rook(Color.Black, Board), new ChessPosition(8, 'h'));

            // Knight
            AddPiece(new Knight(Color.Black, Board), new ChessPosition(8, 'b'));
            AddPiece(new Knight(Color.Black, Board), new ChessPosition(8, 'g'));

            // Bishops
            AddPiece(new Bishop(Color.Black, Board), new ChessPosition(8, 'c'));
            AddPiece(new Bishop(Color.Black, Board), new ChessPosition(8, 'f'));

            // King
            AddPiece(new King(Color.Black, Board), new ChessPosition(8, 'd'));

            // Queen
            AddPiece(new Queen(Color.Black, Board), new ChessPosition(8, 'e'));
        }

        #endregion
    }
}
