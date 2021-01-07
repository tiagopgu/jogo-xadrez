using System;
using Xadrez.Domain.Entities.Enums;
using Xadrez.Domain.Entities.Pieces;

namespace Xadrez.Domain.Entities
{
    public static class SpecialMoves
    {
        public static bool IsSmallCastling(Piece piece, Position destiny, Board board)
        {
            Position positionRook = new Position(destiny.Line, (byte)(destiny.Column - 1));

            if (IsCastling(piece, destiny, positionRook, board) == false)
                return false;

            if (destiny.Column - piece.Position.Column != -2)
                return false;

            return FreeWayForCastling(piece, destiny, board);
        }

        public static bool IsBigCastling(Piece piece, Position destiny, Board board)
        {
            Position positionRook = new Position(destiny.Line, (byte)(destiny.Column + 2));

            if (IsCastling(piece, destiny, positionRook, board) == false)
                return false;

            if (destiny.Column - piece.Position.Column != 2)
                return false;

            return FreeWayForCastling(piece, destiny, board, true);
        }

        public static bool IsEnPassant(Piece piece, Position destiny, Board board, int currentShift)
        {
            if (piece == null || piece is Pawn == false)
                return false;

            Position OpposingPawnPosition = new Position(piece.Position.Line, destiny.Column);
            Piece OpposingPiece = board.GetPiece(OpposingPawnPosition);

            if (OpposingPiece == null || OpposingPiece is Pawn == false || OpposingPiece.Color == piece.Color || OpposingPiece.AmountMoviments > 1)
                return false;

            if ((OpposingPiece.Color == Color.White && (OpposingPiece.Position.Line - 2) != 2) || (OpposingPiece.Color == Color.Black && (OpposingPiece.Position.Line + 2) != 7))
                return false;

            Pawn OpposingPawn = OpposingPiece as Pawn;

            if (currentShift - 1 != OpposingPawn.LastMoveShift)
                return false;

            return true;
        }

        public static bool IsAPawnPromotion(Piece piece, Position destiny)
        {
            if (piece == null || piece is Pawn == false)
                return false;

            if (piece.Color == Color.White && destiny.Line != 8 || piece.Color == Color.Black && destiny.Line != 1)
                return false;

            return true;
        }

        public static bool IsSpecialMoves(Piece piece, Position destiny, Board board, int currentShift)
        {
            return IsSmallCastling(piece, destiny, board) || IsBigCastling(piece, destiny, board) || IsEnPassant(piece, destiny, board, currentShift);
        }

        #region

        private static bool IsCastling(Piece piece, Position destiny, Position positionRook, Board board)
        {
            if (piece == null || piece is King == false || piece.AmountMoviments > 0)
                return false;

            if (piece.Position.Line != destiny.Line)
                return false;

            King king = piece as King;

            if (king.IsCheckMovement(king.Position))
                return false;

            Piece rook = board.GetPiece(positionRook);

            if (rook == null || rook is Rook == false || rook.Color != piece.Color || rook.AmountMoviments > 0)
                return false;

            return true;
        }

        private static bool FreeWayForCastling(Piece piece, Position destiny, Board board, bool isBigCastling = false)
        {
            King king = piece as King;
            Position positionTest = new Position(king.Position.Line, 0);


            for (int i = king.Position.Column + (isBigCastling ? 1 : -1); isBigCastling ? i <= destiny.Column : i >= destiny.Column; i = isBigCastling ? ++i : --i)
            {
                positionTest.Column = (byte)i;

                if (board.ExistsPiece(positionTest))
                    return false;

                if (king.IsCheckMovement(positionTest))
                    return false;
            }

            return true;
        }

        #endregion
    }
}
