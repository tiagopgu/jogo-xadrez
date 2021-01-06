using System.Collections.Generic;
using System.Linq;
using Xadrez.Domain.Entities;
using Xadrez.Domain.Entities.Enums;
using Xadrez.Domain.Entities.Exceptions;
using Xadrez.Domain.Entities.Pieces;
using Xadrez.Domain.Utils;

namespace Xadrez.Domain
{
    public class ChessGame
    {
        private List<Piece> _piecesInPlay;
        private List<Piece> _capturedPieces;

        public Board Board { get; private set; }
        public byte Shift { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool GameEnded { get; private set; }

        public ChessGame()
        {
            _piecesInPlay = new List<Piece>();
            _capturedPieces = new List<Piece>();

            Board = new Board(8, 8);
            Shift = 1;
            CurrentPlayer = Color.White;

            PutWhitePieces();
            PutBlackPieces();
        }

        public void AddPiece(Piece piece, ChessPosition position)
        {
            Position newPosition = GetPosition(position);

            _piecesInPlay.Add(piece);
            Board.AddPiece(piece, newPosition);
        }

        public Piece RemovePiece(ChessPosition position)
        {
            Position newPosition = GetPosition(position);

            Piece piece = Board.RemovePiece(newPosition);

            _piecesInPlay.Remove(piece);

            return piece;
        }

        public Piece GetPiece(ChessPosition position)
        {
            Position newPosition = GetPosition(position);
            Piece piece = Board.GetPiece(newPosition);

            if (piece == null)
                throw new ChessGameException(SystemMessages.NoPiece);

            if (ValidPieceCurrentPlayer(piece) == false)
                throw new ChessGameException(SystemMessages.InvalidPieceForCurrentPlayer);

            return piece;
        }

        public Piece[] GetCapturedPieces(Color color)
        {
            return _capturedPieces.Where(p => p.Color == color).ToArray();
        }

        public bool CanMove(Piece piece)
        {
            if (KingIsInCheck(CurrentPlayer) && CanGetTheKingOutOfCheck(piece) == false)
            {
                throw new ChessGameException(SystemMessages.KingIsInCheck);
            }

            foreach (var isPossibleMovements in piece.PossibleMovements())
            {
                if (isPossibleMovements)
                    return true;
            }

            throw new ChessGameException(SystemMessages.CanNotMove);
        }

        public void MovePiece(ChessPosition origin, ChessPosition destiny)
        {
            if (ValidPositionsForMovement(origin, destiny))
            {
                Piece piece = GetPiece(origin);

                if (piece.ValidMovement(GetPosition(destiny)) == false && SpecialMoves.IsSpecialMoves(piece, GetPosition(destiny), Board) == false)
                    throw new ChessGameException(SystemMessages.InvalidMovement);

                if (KingIsInCheck(CurrentPlayer) && ValidPositionToGetTheKingOutOfCheck(piece, destiny) == false)
                    throw new ChessGameException(SystemMessages.InvalidMovementToRemoveCheck);

                if (SpecialMoves.IsSpecialMoves(piece, GetPosition(destiny), Board))
                {
                    bool isSmallCastling = SpecialMoves.IsSmallCastling(piece, GetPosition(destiny), Board);
                    bool isBigCastling = SpecialMoves.IsBigCastling(piece, GetPosition(destiny), Board);

                    if (isSmallCastling || isBigCastling)
                    {
                        RemovePiece(origin);
                        AddPiece(piece, destiny);

                        ChessPosition originRook = new ChessPosition(destiny.Line, isSmallCastling ? 'a' : 'h');
                        ChessPosition destinyRook = new ChessPosition(destiny.Line, isSmallCastling ? 'c' : 'e');

                        Piece rook = RemovePiece(originRook);
                        AddPiece(rook, destinyRook);

                        rook.IncreaseMovement();
                    }
                }
                else
                {
                    RemovePiece(origin);

                    CapturePiece(destiny);

                    AddPiece(piece, destiny);
                }

                piece.IncreaseMovement();

                UpdateGame();
            }
        }

        public bool KingIsInCheck(Color color)
        {
            King king = _piecesInPlay.Where(p => p.Color == color && p is King).FirstOrDefault() as King;

            return king != null && king.IsCheckMovement(king.Position);
        }

        public bool CheckmateOccurred(Color color)
        {
            if (KingIsInCheck(color) == false)
                return false;

            foreach (var piece in _piecesInPlay.Where(p => p.Color == color))
            {
                if (CanGetTheKingOutOfCheck(piece))
                    return false;
            }

            return true;
        }

        public string GetNameOfOpposingPiece(ChessPosition chessPosition)
        {
            Position position = GetPosition(chessPosition);
            Piece piece = Board.GetPiece(position);

            if (piece == null || piece.Color == CurrentPlayer)
                return null;

            return piece.ToString();
        }

        #region Privates Methods

        private Position GetPosition(ChessPosition position)
        {
            return new Position(position?.Line ?? 0, (byte)((position?.Column ?? 'a') - 'a' + 1));
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

        private void ChangePlayer()
        {
            CurrentPlayer = (CurrentPlayer == Color.White) ? Color.Black : Color.White;
        }

        private bool ValidPieceCurrentPlayer(Piece piece)
        {
            return piece != null && piece.Color == CurrentPlayer;
        }

        private void CapturePiece(ChessPosition position)
        {
            Piece capturedPiece = RemovePiece(position);

            if (capturedPiece != null)
                _capturedPieces.Add(capturedPiece);
        }

        private void UpdateGame()
        {
            ChangePlayer();

            if (CheckmateOccurred(CurrentPlayer))
            {
                ChangePlayer();
                GameEnded = true;
            }
            else
            {
                Shift++;
            }
        }

        private bool CanGetTheKingOutOfCheck(Piece piece)
        {
            King king = _piecesInPlay.Where(p => p is King && p.Color == CurrentPlayer).FirstOrDefault() as King;

            if (king != null && KingIsInCheck(CurrentPlayer))
            {
                ChessPosition position = new ChessPosition(0, ' ');
                bool[,] possibleMovements = piece.PossibleMovements();
                string columnIdentifier = "abcdefgh";

                for (int i = possibleMovements.GetLength(0) - 1; i >= 0; i--)
                {
                    for (int j = 0; j < possibleMovements.GetLength(1); j++)
                    {
                        if (possibleMovements[i, j])
                        {
                            position.Line = (byte)(possibleMovements.GetLength(0) - i);
                            position.Column = columnIdentifier[j];

                            if (ValidPositionToGetTheKingOutOfCheck(piece, position))
                                return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool ValidPositionToGetTheKingOutOfCheck(Piece piece, ChessPosition chessPositionDestiny)
        {
            bool canGetTheKingOutOfCheck = true;
            Position positionDestiny = GetPosition(chessPositionDestiny);

            Position currentPosition = piece.Position;
            Piece capturedPiece = Board.RemovePiece(positionDestiny);

            Board.RemovePiece(currentPosition);
            Board.AddPiece(piece, positionDestiny);

            if (KingIsInCheck(CurrentPlayer))
                canGetTheKingOutOfCheck = false;

            Board.RemovePiece(positionDestiny);
            Board.AddPiece(piece, currentPosition);

            if (capturedPiece != null)
                Board.AddPiece(capturedPiece, positionDestiny);

            return canGetTheKingOutOfCheck;
        }

        #endregion
    }
}
