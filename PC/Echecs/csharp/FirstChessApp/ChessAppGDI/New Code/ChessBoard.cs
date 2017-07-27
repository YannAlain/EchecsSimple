﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAppGDI.New_Code
{
    public class ChessBoard
    {
        Square[,] board;

        public ChessBoard()
        {
            board = new Square[8, 8];
            bool Dark = false;
            for (int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    board[i,j] = new Square(Dark);
                    Dark = !Dark;
                }
                Dark = !Dark;
            }
        }

        public void Restart()
        {
            // Player 1 is at the bottom
            for (int i = 0; i < 8; i++)
            {
                board[i, 6].SetPiece( new Piece(Piece.Types.PAWN, Piece.Colors.WHITE));
            }
            board[0, 7].SetPiece(new Piece(Piece.Types.ROOK, Piece.Colors.WHITE));
            board[7, 7].SetPiece(new Piece(Piece.Types.ROOK, Piece.Colors.WHITE));
            board[1, 7].SetPiece(new Piece(Piece.Types.KNIGHT, Piece.Colors.WHITE));
            board[6, 7].SetPiece(new Piece(Piece.Types.KNIGHT, Piece.Colors.WHITE));
            board[2, 7].SetPiece(new Piece(Piece.Types.BISHOP, Piece.Colors.WHITE));
            board[5, 7].SetPiece(new Piece(Piece.Types.BISHOP, Piece.Colors.WHITE));
            board[3, 7].SetPiece(new Piece(Piece.Types.QUEEN, Piece.Colors.WHITE));
            board[4, 7].SetPiece(new Piece(Piece.Types.KING, Piece.Colors.WHITE));
            // Player 2 is at the top
            for (int i = 0; i < 8; i++)
            {
                board[i, 1].SetPiece(new Piece(Piece.Types.PAWN, Piece.Colors.BLACK));
            }
            board[0, 0].SetPiece(new Piece(Piece.Types.ROOK, Piece.Colors.BLACK));
            board[7, 0].SetPiece(new Piece(Piece.Types.ROOK, Piece.Colors.BLACK));
            board[1, 0].SetPiece(new Piece(Piece.Types.KNIGHT, Piece.Colors.BLACK));
            board[6, 0].SetPiece(new Piece(Piece.Types.KNIGHT, Piece.Colors.BLACK));
            board[2, 0].SetPiece(new Piece(Piece.Types.BISHOP, Piece.Colors.BLACK));
            board[5, 0].SetPiece(new Piece(Piece.Types.BISHOP, Piece.Colors.BLACK));
            board[3, 0].SetPiece(new Piece(Piece.Types.QUEEN, Piece.Colors.BLACK));
            board[4, 0].SetPiece(new Piece(Piece.Types.KING, Piece.Colors.BLACK));
        }

        public Square[,] GetBoard()
        {
            return board;
        }
        
        public static Piece.Colors OtherColor(Piece.Colors pColor)
        {
            if (pColor.Equals(Piece.Colors.WHITE))
            {
                return Piece.Colors.BLACK;
            }
            else
            {
                return Piece.Colors.WHITE;
            }
        }

        private void removePiece(BoardPosition pPosition)
        {
            board[pPosition.X, pPosition.Y].RemovePiece();
        }

        private void setPiece(BoardPosition pPosition, Piece pPiece)
        {
            board[pPosition.X, pPosition.Y].SetPiece(pPiece);
        }

        public void movePiece(BoardPosition pStart, BoardPosition pEnd)
        {
            
            if (board[pStart.X, pStart.Y].HasPiece())
            {
                Piece transferPiece = board[pStart.X, pStart.Y].GetPiece();
                board[pEnd.X, pEnd.Y].SetPiece(transferPiece);
                board[pStart.X, pStart.Y].RemovePiece();
                
            }
        }



    }
}
