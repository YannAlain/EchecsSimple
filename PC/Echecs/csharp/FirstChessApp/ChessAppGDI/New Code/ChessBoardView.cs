﻿using ChessAppGDI.Game_Engin;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAppGDI.New_Code
{
    public class ChessBoardView
    {
        ChessBoard board;
        PieceView[,] viewBoard;
        Boolean isSelecting;
        public ChessBoardView()
        {
            board = new ChessBoard();
            viewBoard = new PieceView[8, 8];
            isSelecting = false;
            board.Restart();
            SetViewList();
        }

        public ChessBoard GetChessBoard()
        {
            return board;
        }

        public PieceView[,] GetViewBoard()
        {
            return viewBoard;
        }

        public void movePiece(BoardPosition pStart, BoardPosition pEnd)
        {
            board.movePiece(pStart, pEnd);

            if (GetViewBoard()[pStart.X, pStart.Y].GetPiece().pieceType.type.Equals(PieceType.Types.PAWN) ||
                GetViewBoard()[pStart.X, pStart.Y].GetPiece().pieceType.type.Equals(PieceType.Types.ROOK) ||
                GetViewBoard()[pStart.X, pStart.Y].GetPiece().pieceType.type.Equals(PieceType.Types.KING))
            {
                if (GetViewBoard()[pStart.X, pStart.Y].GetPiece().pieceType.type.Equals(PieceType.Types.KING)
                    && GetViewBoard()[pStart.X, pStart.Y].GetPiece().color.Equals(Piece.Colors.WHITE))
                {
                    if (!GetViewBoard()[pStart.X, pStart.Y].GetHasMoved())
                    {
                        if (pEnd.X == 2 && pEnd.Y == 7)
                        {
                            if (GetViewBoard()[0, 7].GetPiece().pieceType.type.Equals(PieceType.Types.ROOK) &&
                                !GetViewBoard()[0, 7].GetHasMoved())
                            {
                                board.movePiece(new BoardPosition(0, 7), new BoardPosition(3, 7));
                                viewBoard[0, 7].HasMoved();
                                viewBoard[3, 7] = viewBoard[0, 7];
                                viewBoard[0, 7] = null;

                            }
                        }
                        else if (pEnd.X == 6 && pEnd.Y == 7)
                        {
                            if (GetViewBoard()[7, 7].GetPiece().pieceType.type.Equals(PieceType.Types.ROOK) &&
                                !GetViewBoard()[7, 7].GetHasMoved())
                            {
                                board.movePiece(new BoardPosition(7, 7), new BoardPosition(5, 7));
                                viewBoard[7, 7].HasMoved();
                                viewBoard[5, 7] = viewBoard[7, 7];
                                viewBoard[7, 7] = null;

                            }
                        }
                    }
                }
                if (GetViewBoard()[pStart.X, pStart.Y].GetPiece().pieceType.type.Equals(PieceType.Types.KING)
                    && GetViewBoard()[pStart.X, pStart.Y].GetPiece().color.Equals(Piece.Colors.BLACK))
                {
                    if (!GetViewBoard()[pStart.X, pStart.Y].GetHasMoved())
                    {
                        if (pEnd.X == 2 && pEnd.Y == 0)
                        {
                            if (GetViewBoard()[0, 0].GetPiece().pieceType.type.Equals(PieceType.Types.ROOK) &&
                                !GetViewBoard()[0, 0].GetHasMoved())
                            {
                                board.movePiece(new BoardPosition(0, 0), new BoardPosition(3, 0));
                                viewBoard[0, 0].HasMoved();
                                viewBoard[3, 0] = viewBoard[0, 0];
                                viewBoard[0, 0] = null;

                            }
                        }
                        else if (pEnd.X == 6 && pEnd.Y == 0)
                        {
                            if (GetViewBoard()[7, 0].GetPiece().pieceType.type.Equals(PieceType.Types.ROOK) &&
                                !GetViewBoard()[7, 0].GetHasMoved())
                            {
                                board.movePiece(new BoardPosition(7, 0), new BoardPosition(5, 0));
                                viewBoard[7, 0].HasMoved();
                                viewBoard[5, 0] = viewBoard[7, 0];
                                viewBoard[7, 0] = null;

                            }
                        }
                    }
                }
                viewBoard[pStart.X, pStart.Y].HasMoved();
            }
            viewBoard[pEnd.X, pEnd.Y] = viewBoard[pStart.X, pStart.Y];
            viewBoard[pStart.X, pStart.Y] = null;

            // Switch the pawn for the selected piece
            if (GetViewBoard()[pEnd.X, pEnd.Y].GetPiece().pieceType.type.Equals(PieceType.Types.PAWN))
            {
                if ((pEnd.Y == 0) || (pEnd.Y == 7))
                {
                    ReplacePawn(pEnd);
                }
            }
        }
        
        public void SetReplacePiece(Piece pPiece)
        {
            aReplacementPiece = pPiece;
        }

        Piece aReplacementPiece = new Piece(PieceType.Types.QUEEN, Piece.Colors.WHITE);
        Piece aRealReplacementPiece;

        private void ReplacePawn(BoardPosition bp)
        {
            if (bp.Y == 0)
            {
                aRealReplacementPiece = new Piece(aReplacementPiece.pieceType.type, Piece.Colors.WHITE);
            }
            else
            {
                aRealReplacementPiece = new Piece(aReplacementPiece.pieceType.type, Piece.Colors.BLACK);
            }
            board.GetBoard()[bp.X, bp.Y].SetPiece(aRealReplacementPiece);
            viewBoard[bp.X, bp.Y] = new PieceView(board.GetBoard()[bp.X, bp.Y].GetPiece());

            SetViewList();
        }

        private void SetViewList()
        {
            Square[,] boardPiece = board.GetBoard();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board.GetBoard()[i, j].HasPiece())
                    {
                        viewBoard[i, j] = new PieceView(board.GetBoard()[i, j].GetPiece());
                    }
                }
            }
        }


        List<BoardPosition> possibleMoves;
        Font font = new Font("Times New Roman", 16);
        int square = 0;

        public void DrawGame(Graphics g)
        {
            DrawBoard(g);
            DrawPieces(g);
        }

        private void DrawBoard(Graphics g)
        {
            square = PositionAndPixels.square;
            for (int i = 0; i < 9; i++)
            {
                // Vertical lines
                Point p1 = new Point(square / 2, square / 2 + square * i);
                Point p2 = new Point(square / 2 + square * 8, square / 2 + square * i);
                g.DrawLine(Pens.Black, p1, p2);

                // Horizontal lines
                Point p3 = new Point(square / 2 + square * i, square / 2);
                Point p4 = new Point(square / 2 + square * i, square / 2 + square * 8);
                g.DrawLine(Pens.Black, p3, p4);
            }
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int p1 = square / 2 + (2 * i * square);
                    int p2 = square / 2 + (2 * j * square);
                    g.FillRectangle(Brushes.Black, p1, p2, square, square);
                }
            }
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int p1 = square / 2 + square + (2 * i * square);
                    int p2 = square / 2 + square + (2 * j * square);
                    g.FillRectangle(Brushes.Black, p1, p2, square, square);
                }
            }
            for (int i = 1; i < 9; i++)
            {
                g.DrawString((9 - i).ToString(), font, Brushes.Black, square / 16, square * i - (square / 4));
                g.DrawString(Char.ConvertFromUtf32(65 + i - 1), font, Brushes.Black, square * i - (square / 4), square / 16);
            }
            if (isSelecting)
            {
                DrawSelectedSquares(g);
            }

        }

        private void DrawSelectedSquares(Graphics g)
        {
            foreach(BoardPosition b in possibleMoves)
            {
                Console.WriteLine(b.ToString());
                Point p = PositionAndPixels.BoardPositionToPixels(b);
                g.FillRectangle(Brushes.Red, p.X, p.Y, square, square);
            }
        }
        public void SetAvaiablemoves(List<BoardPosition> list)
        {
            possibleMoves = list;
        }

        private void DrawPieces(Graphics g)
        {
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    if(board.GetBoard()[i, j].HasPiece())
                    {
                        Point point = PositionAndPixels.BoardPositionToPixels(new BoardPosition(i, j));
                        Image image = viewBoard[i, j].GetImage();

                        g.DrawImage(image , point.X, point.Y, PositionAndPixels.square, PositionAndPixels.square);
                    }
                }
            }
        }

        public bool GetIsSelecting()
        {
            return isSelecting;
        }

        public void SetIsSelecting(bool selected)
        {
            isSelecting = selected;
        }

    }
}
