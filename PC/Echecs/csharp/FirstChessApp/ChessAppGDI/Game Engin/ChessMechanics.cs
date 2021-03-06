﻿using ChessAppGDI.Game_Engin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAppGDI.New_Code
{
    public static class ChessMechanics
    {
        public static List<BoardPosition> AvaiableMoves(BoardPosition bp, ChessBoardView pChessBoard)
        {
            List<BoardPosition> boardPositionList = new List<BoardPosition>();
            Piece aPiece;
            if (pChessBoard.GetChessBoard().GetBoard()[bp.X, bp.Y].HasPiece())
            {
                aPiece = pChessBoard.GetChessBoard().GetBoard()[bp.X, bp.Y].GetPiece();

            }
            else
            {
                return boardPositionList;
            }

            if( aPiece.pieceType.type == PieceType.Types.ROOK)
            {
                boardPositionList.AddRange(GetRookMoves(bp, pChessBoard));
            }
            else if(aPiece.pieceType.type == PieceType.Types.KNIGHT)
            {
                boardPositionList.AddRange(GetKnightMoves(bp, pChessBoard));
            }
            else if (aPiece.pieceType.type == PieceType.Types.BISHOP)
            {
                boardPositionList.AddRange(GetBishopMoves(bp, pChessBoard));
            }
            else if (aPiece.pieceType.type == PieceType.Types.KING)
            {
                boardPositionList.AddRange(GetKingMoves(bp, pChessBoard));
            }
            else if (aPiece.pieceType.type == PieceType.Types.QUEEN)
            {
                boardPositionList.AddRange(GetQueenMoves(bp, pChessBoard));
            }
            else if (aPiece.pieceType.type == PieceType.Types.PAWN)
            {
                boardPositionList.AddRange(GetPawnMoves(bp, pChessBoard));
            }

            return boardPositionList;
        }
        
        private static List<BoardPosition> GetPawnMoves(BoardPosition bp, ChessBoardView pChessBoard)
        {
            List<BoardPosition> boardPositionList = new List<BoardPosition>();
            boardPositionList.Add(bp);
            Piece p = pChessBoard.GetChessBoard().GetBoard()[bp.X, bp.Y].GetPiece();

            
            int i = bp.X;
            int j = bp.Y;
            // Right now white is assumed to be at the bottom of the board 
            // TODO: find a way to make sure the color is not the factor
            // TODO: add the ability to double jump at the begining and the ability 
            // to change pawn into other piece when it reaches the other side of the board
            if ( p.color == Piece.Colors.WHITE)
            {
                if(j > 0)
                {
                    if (j > 1)
                    {
                        if (!(pChessBoard.GetViewBoard()[i, j]).GetHasMoved()
                            && !pChessBoard.GetChessBoard().GetBoard()[i, j - 2].HasPiece()
                            && !pChessBoard.GetChessBoard().GetBoard()[i, j - 1].HasPiece())
                        {
                            boardPositionList.Add(new BoardPosition(i, j - 2));
                        }
                    }
                    j--;
                    if (!pChessBoard.GetChessBoard().GetBoard()[i, j].HasPiece())
                    {
                        boardPositionList.Add(new BoardPosition(i, j));
                    }
                    if (i > 0)
                    {
                        i--;
                        if (pChessBoard.GetChessBoard().GetBoard()[i, j].HasPiece())
                        {
                            if (!p.IsSameColor(pChessBoard.GetChessBoard().GetBoard()[i, j].GetPiece()))
                            {
                                boardPositionList.Add(new BoardPosition(i, j));
                            }
                        }
                    }
                    i = bp.X;
                    if (i < 7)
                    {
                        i++;
                        if (pChessBoard.GetChessBoard().GetBoard()[i, j].HasPiece())
                        {
                            if (!p.IsSameColor(pChessBoard.GetChessBoard().GetBoard()[i, j].GetPiece()))
                            {
                                boardPositionList.Add(new BoardPosition(i, j));
                            }
                        }
                    }
                }
            }
            else
            {
                if (j < 7)
                {
                    if (j < 7)
                    {
                        if (!(pChessBoard.GetViewBoard()[i, j]).GetHasMoved()
                            && !pChessBoard.GetChessBoard().GetBoard()[i, j + 2].HasPiece())
                        {
                            boardPositionList.Add(new BoardPosition(i, j + 2));
                        }
                    }

                    j++;
                    if (!pChessBoard.GetChessBoard().GetBoard()[i, j].HasPiece())
                    {
                        boardPositionList.Add(new BoardPosition(i, j));
                    }
                    i--;
                    if (i > 0)
                    {
                        if (pChessBoard.GetChessBoard().GetBoard()[i, j].HasPiece())
                        {
                            if (!p.IsSameColor(pChessBoard.GetChessBoard().GetBoard()[i, j].GetPiece()))
                            {
                                boardPositionList.Add(new BoardPosition(i, j));
                            }
                        }
                    }
                    i = bp.X;
                    if (i < 7)
                    {
                        i++;
                        if (pChessBoard.GetChessBoard().GetBoard()[i, j].HasPiece())
                        {
                            if (!p.IsSameColor(pChessBoard.GetChessBoard().GetBoard()[i, j].GetPiece()))
                            {
                                boardPositionList.Add(new BoardPosition(i, j));
                            }
                        }
                    }
                }
            }


            return boardPositionList;
        }

        private static List<BoardPosition> GetKingMoves(BoardPosition bp, ChessBoardView pChessBoard)
        {
            List<BoardPosition> boardPositionList = new List<BoardPosition>();
            boardPositionList.Add(bp);
            Piece p = pChessBoard.GetChessBoard().GetBoard()[bp.X, bp.Y].GetPiece();
            PieceView pv = pChessBoard.GetViewBoard()[bp.X, bp.Y];

            int i = bp.X;
            int j = bp.Y;

            boardPositionList.AddRange(CheckCastling(bp, pChessBoard));
            ///////////////
            if (i >= 1)
            {
                if (j >= 1)
                {
                    if (!pChessBoard.GetChessBoard().GetBoard()[i - 1, j - 1].HasPiece())
                    {
                        boardPositionList.Add(new BoardPosition(i - 1, j - 1));
                    }
                    else
                    {
                        if (!p.IsSameColor(pChessBoard.GetChessBoard().GetBoard()[i - 1, j - 1].GetPiece()))
                        {
                            boardPositionList.Add(new BoardPosition(i - 1, j - 1));
                        }

                    }
                }

                if (!pChessBoard.GetChessBoard().GetBoard()[i - 1 , j].HasPiece())
                {
                    boardPositionList.Add(new BoardPosition(i - 1, j));
                }
                else
                {
                    if(j < 7)
                    {
                        if (!p.IsSameColor(pChessBoard.GetChessBoard().GetBoard()[i - 1, j + 1].GetPiece()))
                        {
                            boardPositionList.Add(new BoardPosition(i - 1, j + 1));
                        }

                    }

                }

                if (j <= 6)
                {
                    if (!pChessBoard.GetChessBoard().GetBoard()[i - 1, j + 1].HasPiece())
                    {
                        boardPositionList.Add(new BoardPosition(i - 1, j + 1));
                    }
                    else
                    {
                        if (!p.IsSameColor(pChessBoard.GetChessBoard().GetBoard()[i - 1, j + 1].GetPiece()))
                        {
                            boardPositionList.Add(new BoardPosition(i - 1, j + 1));
                        }

                    }
                }
            }
            if (j >= 1)
            {
                if (!pChessBoard.GetChessBoard().GetBoard()[i, j - 1].HasPiece())
                {
                    boardPositionList.Add(new BoardPosition(i, j - 1));
                }
                else
                {
                    if (!p.IsSameColor(pChessBoard.GetChessBoard().GetBoard()[i, j - 1].GetPiece()))
                    {
                        boardPositionList.Add(new BoardPosition(i, j - 1));
                    }

                }
            }
            if (j <= 6)
            {
                if (!pChessBoard.GetChessBoard().GetBoard()[i, j + 1].HasPiece())
                {
                    boardPositionList.Add(new BoardPosition(i, j + 1));
                }
                else
                {
                    if (!p.IsSameColor(pChessBoard.GetChessBoard().GetBoard()[i, j + 1].GetPiece()))
                    {
                        boardPositionList.Add(new BoardPosition(i, j + 1));
                    }

                }
            }
            if (i <= 6)
            {
                if (j >= 1)
                {
                    if (!pChessBoard.GetChessBoard().GetBoard()[i + 1, j - 1].HasPiece())
                    {
                        boardPositionList.Add(new BoardPosition(i + 1, j - 1));
                    }
                    else
                    {
                        if (!p.IsSameColor(pChessBoard.GetChessBoard().GetBoard()[i + 1, j - 1].GetPiece()))
                        {
                            boardPositionList.Add(new BoardPosition(i + 1, j - 1));
                        }

                    }
                }

                if (!pChessBoard.GetChessBoard().GetBoard()[i + 1, j].HasPiece())
                {
                    boardPositionList.Add(new BoardPosition(i + 1, j));
                }
                else
                {
                    if(j < 7)
                    {
                        if (!p.IsSameColor(pChessBoard.GetChessBoard().GetBoard()[i + 1, j + 1].GetPiece()))
                        {
                            boardPositionList.Add(new BoardPosition(i + 1, j + 1));
                        }

                    }

                }

                if (j <= 6)
                {
                    if (!pChessBoard.GetChessBoard().GetBoard()[i + 1, j + 1].HasPiece())
                    {
                        boardPositionList.Add(new BoardPosition(i + 1, j + 1));
                    }
                    else
                    {
                        if (!p.IsSameColor(pChessBoard.GetChessBoard().GetBoard()[i + 1, j + 1].GetPiece()))
                        {
                            boardPositionList.Add(new BoardPosition(i + 1, j + 1));
                        }

                    }
                }
            }
            return boardPositionList;
        }

        private static List<BoardPosition> GetQueenMoves(BoardPosition bp, ChessBoardView pChessBoard)
        {
            List<BoardPosition> boardPositionList = new List<BoardPosition>();
            boardPositionList.Add(bp);
            Piece p = pChessBoard.GetChessBoard().GetBoard()[bp.X, bp.Y].GetPiece();
            boardPositionList.AddRange(GetRookMoves(bp, pChessBoard));
            for (int i = 0; i < boardPositionList.Count; i++)
            {
             //   Console.WriteLine("Rook moves:" + boardPositionList[i].ToString());
            }
            boardPositionList.AddRange(GetBishopMoves(bp, pChessBoard));
            for(int i = 0; i < boardPositionList.Count ; i++)
            {
              //  Console.WriteLine(boardPositionList[i].ToString());
            }
            return boardPositionList;
        }

        private static List<BoardPosition> GetBishopMoves(BoardPosition bp, ChessBoardView pChessBoard)
        {
            List<BoardPosition> boardPositionList = new List<BoardPosition>();
            boardPositionList.Add(bp);
            Piece p = pChessBoard.GetChessBoard().GetBoard()[bp.X, bp.Y].GetPiece();

            int i = bp.X;
            int j = bp.Y;
            while( i > 0 && j > 0)
            {
                i--;
                j--;
                if (pChessBoard.GetChessBoard().GetBoard()[i, j].HasPiece())
                {
                    if (!p.IsSameColor(pChessBoard.GetChessBoard().GetBoard()[i, j].GetPiece()))
                    {
                        boardPositionList.Add(new BoardPosition(i, j));
                    }
                    break;
                }
                else
                {
                    boardPositionList.Add(new BoardPosition(i, j));
                }
                
            }
            i = bp.X;
            j = bp.Y;
            while (i < 7 && j > 0)
            {
                i++;
                j--;
                if (pChessBoard.GetChessBoard().GetBoard()[i, j].HasPiece())
                {
                    if (!p.IsSameColor(pChessBoard.GetChessBoard().GetBoard()[i, j].GetPiece()))
                    {
                        boardPositionList.Add(new BoardPosition(i, j));
                    }
                    break;
                }
                else
                {
                    boardPositionList.Add(new BoardPosition(i, j));
                }
            }
            i = bp.X;
            j = bp.Y;
            while (i > 0 && j < 7)
            {
                i--;
                j++;
                if (pChessBoard.GetChessBoard().GetBoard()[i, j].HasPiece())
                {
                    if (!p.IsSameColor(pChessBoard.GetChessBoard().GetBoard()[i, j].GetPiece()))
                    {
                        boardPositionList.Add(new BoardPosition(i, j));
                    }
                    break;
                }
                else
                {
                    boardPositionList.Add(new BoardPosition(i, j));
                }
            }
            i = bp.X;
            j = bp.Y;
            while (i < 7 && j < 7)
            {
                i++;
                j++;
                if (pChessBoard.GetChessBoard().GetBoard()[i, j].HasPiece())
                {
                    if (!p.IsSameColor(pChessBoard.GetChessBoard().GetBoard()[i, j].GetPiece()))
                    {
                        boardPositionList.Add(new BoardPosition(i, j));
                    }
                    break;
                }
                else
                {
                    boardPositionList.Add(new BoardPosition(i, j));
                }
            }

            return boardPositionList;
        }
        
        private static List<BoardPosition> GetKnightMoves(BoardPosition bp, ChessBoardView pChessBoard)
        {
            List<BoardPosition> boardPositionList = new List<BoardPosition>();
            boardPositionList.Add(bp);
            Piece p = pChessBoard.GetChessBoard().GetBoard()[bp.X, bp.Y].GetPiece();

            int i = bp.X;
            int j = bp.Y;
            /////////////////////////////////////////////////
            //
            //             8 o 1
            //           7   o   2 
            //           o o x o o
            //           6   o   3   
            //             5 o 4 
            //
            ///////////////////// 1 /////////////////////////
            if (i >= 2 && j <= 6)
            {
                if (pChessBoard.GetChessBoard().GetBoard()[i - 2, j + 1].HasPiece())
                {
                    if (!p.IsSameColor(pChessBoard.GetChessBoard().GetBoard()[i - 2, j + 1].GetPiece()))
                    {
                        boardPositionList.Add(new BoardPosition(i - 2, j + 1));
                    }
                }
                else
                {
                    boardPositionList.Add(new BoardPosition(i - 2, j + 1));
                }
            }
            ///////////////////// 2 /////////////////////////
            if (i >= 1 && j <= 5)
            {
                if (pChessBoard.GetChessBoard().GetBoard()[i - 1, j + 2].HasPiece())
                {
                    if (!p.IsSameColor(pChessBoard.GetChessBoard().GetBoard()[i - 1, j + 2].GetPiece()))
                    {
                        boardPositionList.Add(new BoardPosition(i - 1, j + 2));
                    }
                }
                else
                {
                    boardPositionList.Add(new BoardPosition(i - 1, j + 2));
                }
            }
            ///////////////////// 3 /////////////////////////
            if (i <= 6 && j <= 5)
            {
                if (pChessBoard.GetChessBoard().GetBoard()[i + 1, j + 2].HasPiece())
                {
                    if (!p.IsSameColor(pChessBoard.GetChessBoard().GetBoard()[i + 1, j + 2].GetPiece()))
                    {
                        boardPositionList.Add(new BoardPosition(i + 1, j + 2));
                    }
                }
                else
                {
                    boardPositionList.Add(new BoardPosition(i + 1, j + 2));
                }
            }
            ///////////////////// 4 /////////////////////////
            if (i <= 5 && j <= 6)
            {
                if (pChessBoard.GetChessBoard().GetBoard()[i + 2, j + 1].HasPiece())
                {
                    if (!p.IsSameColor(pChessBoard.GetChessBoard().GetBoard()[i + 2, j + 1].GetPiece()))
                    {
                        boardPositionList.Add(new BoardPosition(i + 2, j + 1));
                    }
                }
                else
                {
                    boardPositionList.Add(new BoardPosition(i + 2, j + 1));
                }
            }
            ///////////////////// 5 /////////////////////////
            if (i <= 5 && j >= 1)
            {
                if (pChessBoard.GetChessBoard().GetBoard()[i + 2, j - 1].HasPiece())
                {
                    if (!p.IsSameColor(pChessBoard.GetChessBoard().GetBoard()[i + 2, j - 1].GetPiece()))
                    {
                        boardPositionList.Add(new BoardPosition(i + 2, j - 1));
                    }
                }
                else
                {
                    boardPositionList.Add(new BoardPosition(i + 2, j - 1));
                }
            }
            ///////////////////// 6 /////////////////////////
            if (i <= 6 && j >= 2)
            {
                if (pChessBoard.GetChessBoard().GetBoard()[i + 1, j - 2].HasPiece())
                {
                    if (!p.IsSameColor(pChessBoard.GetChessBoard().GetBoard()[i + 1, j - 2].GetPiece()))
                    {
                        boardPositionList.Add(new BoardPosition(i + 1, j - 2));
                    }
                }
                else
                {
                    boardPositionList.Add(new BoardPosition(i + 1, j - 2));
                }
            }
            ///////////////////// 7 /////////////////////////
            if (i >= 1 && j >= 2)
            {
                if (pChessBoard.GetChessBoard().GetBoard()[i - 1, j - 2].HasPiece())
                {
                    if (!p.IsSameColor(pChessBoard.GetChessBoard().GetBoard()[i - 1, j - 2].GetPiece()))
                    {
                        boardPositionList.Add(new BoardPosition(i - 1, j - 2));
                    }
                }
                else
                {
                    boardPositionList.Add(new BoardPosition(i - 1, j - 2));
                }
            }
            ///////////////////// 8 /////////////////////////
            if (i >= 2 && j >= 1)
            {
                if (pChessBoard.GetChessBoard().GetBoard()[i - 2, j - 1].HasPiece())
                {
                    if (!p.IsSameColor(pChessBoard.GetChessBoard().GetBoard()[i - 2, j - 1].GetPiece()))
                    {
                        boardPositionList.Add(new BoardPosition(i - 2, j - 1));
                    }
                }
                else
                {
                    boardPositionList.Add(new BoardPosition(i - 2, j - 1));
                }
            }
            return boardPositionList;
        }
        
        private static List<BoardPosition> GetRookMoves(BoardPosition bp, ChessBoardView pChessBoard)
        {

            List<BoardPosition> boardPositionList = new List<BoardPosition>();
            boardPositionList.Add(bp);
            Piece p = pChessBoard.GetChessBoard().GetBoard()[bp.X, bp.Y].GetPiece();
            int index = 0;
            int i = bp.X;
            int j = bp.Y;
            // Up
            while (j < 7 )
            {
                j++;
                if(pChessBoard.GetChessBoard().GetBoard()[i, j].HasPiece() )
                {
                    if(!p.IsSameColor(pChessBoard.GetChessBoard().GetBoard()[i, j].GetPiece()))
                    {
                        boardPositionList.Add(new BoardPosition(i, j));
                    }
                    break;
                }
                else
                {
                    boardPositionList.Add(new BoardPosition(i, j));
                }
            }
            for (int m = 0; m < boardPositionList.Count; m++)
            {
             //   Console.WriteLine("Rook moves 1:" + boardPositionList[m].ToString());
                index = m;
            }
            i = bp.X;
            j = bp.Y;
            while( j > 0)
            {
                j--;
                if (pChessBoard.GetChessBoard().GetBoard()[i, j].HasPiece())
                {
                    if (!p.IsSameColor(pChessBoard.GetChessBoard().GetBoard()[i, j].GetPiece()))
                    {
                        boardPositionList.Add(new BoardPosition(i, j));
                    }
                    break;
                }
                else
                {
                    boardPositionList.Add(new BoardPosition(i, j));
                }
            }
            for (int m = index; m < boardPositionList.Count; m++)
            {
              //  Console.WriteLine("Rook moves 2:" + boardPositionList[m].ToString());
                index = m;
            }
            i = bp.X;
            j = bp.Y;
            while (i < 7)
            {
                i++;
                if (pChessBoard.GetChessBoard().GetBoard()[i, j].HasPiece())
                {
                    if (!p.IsSameColor(pChessBoard.GetChessBoard().GetBoard()[i, j].GetPiece()))
                    {
                        boardPositionList.Add(new BoardPosition(i, j));
                    }
                    break;
                }
                else
                {
                    boardPositionList.Add(new BoardPosition(i, j));
                }
            }
            for (int m = index; m < boardPositionList.Count; m++)
            {
              //  Console.WriteLine("Rook moves 3:" + boardPositionList[m].ToString());
                index = m;
            }
            i = bp.X;
            j = bp.Y;
            while (i > 0)
            {
                i--;
                if (pChessBoard.GetChessBoard().GetBoard()[i, j].HasPiece())
                {
                    if (!p.IsSameColor(pChessBoard.GetChessBoard().GetBoard()[i, j].GetPiece()))
                    {
                        boardPositionList.Add(new BoardPosition(i, j));
                    }
                    break;
                }
                else
                {
                    boardPositionList.Add(new BoardPosition(i, j));
                }
            }
            
            return boardPositionList;
        }



        
        public static void PrintBoardView(ChessBoardView pBoard)
        {
            PieceView[,] view = pBoard.GetViewBoard();
            Square[,] view2 = pBoard.GetChessBoard().GetBoard();
            for (int i = 0; i < view2.GetLength(0); i++)
            {
                for(int j = 0; j < view2.GetLength(1); j++)
                {
                    Console.Write(view2[i, j].HasPiece() + " ");
                }
                Console.WriteLine();
            }
        }

        private static List<BoardPosition> CheckCastling(BoardPosition bp, ChessBoardView pChessBoard)
        {
            List<BoardPosition> boardPositionList = new List<BoardPosition>();
            boardPositionList.Add(bp);
            Piece p = pChessBoard.GetChessBoard().GetBoard()[bp.X, bp.Y].GetPiece();
            PieceView pv = pChessBoard.GetViewBoard()[bp.X, bp.Y];

            int i = bp.X;
            int j = bp.Y;
            if (p.color == Piece.Colors.WHITE)
            {
                if (!pv.GetHasMoved())
                {
                    if (pChessBoard.GetChessBoard().GetBoard()[0, 7].GetPiece() != null)
                    {
                        if (pChessBoard.GetChessBoard().GetBoard()[0, 7].GetPiece().pieceType.type == PieceType.Types.ROOK)
                        {
                            if (!pChessBoard.GetViewBoard()[0, 7].GetHasMoved())
                            {
                                if (!pChessBoard.GetChessBoard().GetBoard()[1, 7].HasPiece() &&
                                    !pChessBoard.GetChessBoard().GetBoard()[2, 7].HasPiece() &&
                                    !pChessBoard.GetChessBoard().GetBoard()[3, 7].HasPiece())
                                {
                                    if (!SquareIsThreatened(pChessBoard.GetChessBoard().GetBoard()[4, 7].GetPiece(), new BoardPosition(0, 7), pChessBoard)
                                        && !SquareIsThreatened(pChessBoard.GetChessBoard().GetBoard()[4, 7].GetPiece(), new BoardPosition(1, 7), pChessBoard)
                                        && !SquareIsThreatened(pChessBoard.GetChessBoard().GetBoard()[4, 7].GetPiece(), new BoardPosition(2, 7), pChessBoard)
                                        && !SquareIsThreatened(pChessBoard.GetChessBoard().GetBoard()[4, 7].GetPiece(), new BoardPosition(3, 7), pChessBoard)
                                        && !SquareIsThreatened(pChessBoard.GetChessBoard().GetBoard()[4, 7].GetPiece(), new BoardPosition(5, 7), pChessBoard))
                                    {
                                        boardPositionList.Add(new BoardPosition(2, 7));

                                    }
                                }
                            }
                        }
                    }
                    if (pChessBoard.GetChessBoard().GetBoard()[7, 7].GetPiece() != null)
                    {
                        if (pChessBoard.GetChessBoard().GetBoard()[7, 7].GetPiece().pieceType.type == PieceType.Types.ROOK)
                        {
                            if (!pChessBoard.GetViewBoard()[7, 7].GetHasMoved())
                            {
                                if (!pChessBoard.GetChessBoard().GetBoard()[6, 7].HasPiece() &&
                                    !pChessBoard.GetChessBoard().GetBoard()[5, 7].HasPiece())
                                {
                                    if (!SquareIsThreatened(pChessBoard.GetChessBoard().GetBoard()[4, 7].GetPiece(), new BoardPosition(4, 7), pChessBoard)
                                        && !SquareIsThreatened(pChessBoard.GetChessBoard().GetBoard()[4, 7].GetPiece(), new BoardPosition(5, 7), pChessBoard)
                                        && !SquareIsThreatened(pChessBoard.GetChessBoard().GetBoard()[4, 7].GetPiece(), new BoardPosition(6, 7), pChessBoard)
                                        && !SquareIsThreatened(pChessBoard.GetChessBoard().GetBoard()[4, 7].GetPiece(), new BoardPosition(7, 7), pChessBoard))
                                    {
                                        boardPositionList.Add(new BoardPosition(6, 7));
                                    }
                                }
                            }
                        }
                    }
                }

            }
            else if (p.color == Piece.Colors.BLACK)
            {
                if (!pv.GetHasMoved())
                {
                    if (pChessBoard.GetChessBoard().GetBoard()[0, 0].GetPiece() != null)
                    {
                        if (pChessBoard.GetChessBoard().GetBoard()[0, 0].GetPiece().pieceType.type == PieceType.Types.ROOK)
                        {
                            if (!pChessBoard.GetViewBoard()[0, 0].GetHasMoved())
                            {
                                if (!pChessBoard.GetChessBoard().GetBoard()[1, 0].HasPiece() &&
                                    !pChessBoard.GetChessBoard().GetBoard()[2, 0].HasPiece() &&
                                    !pChessBoard.GetChessBoard().GetBoard()[3, 0].HasPiece())
                                {
                                    if (!SquareIsThreatened(pChessBoard.GetChessBoard().GetBoard()[4, 0].GetPiece(), new BoardPosition(1, 0), pChessBoard)
                                        && !SquareIsThreatened(pChessBoard.GetChessBoard().GetBoard()[4, 0].GetPiece(), new BoardPosition(2, 0), pChessBoard)
                                        && !SquareIsThreatened(pChessBoard.GetChessBoard().GetBoard()[4, 0].GetPiece(), new BoardPosition(3, 0), pChessBoard))
                                    {

                                        boardPositionList.Add(new BoardPosition(2, 0));
                                    }
                                }
                            }
                        }
                    }
                    if (pChessBoard.GetChessBoard().GetBoard()[7, 0].GetPiece() != null)
                    {
                        if (pChessBoard.GetChessBoard().GetBoard()[7, 0].GetPiece().pieceType.type == PieceType.Types.ROOK)
                        {
                            if (!pChessBoard.GetViewBoard()[7, 0].GetHasMoved())
                            {
                                if (!pChessBoard.GetChessBoard().GetBoard()[6, 0].HasPiece() &&
                                    !pChessBoard.GetChessBoard().GetBoard()[5, 0].HasPiece())
                                {
                                    if (!SquareIsThreatened(pChessBoard.GetChessBoard().GetBoard()[4, 7].GetPiece(), new BoardPosition(5, 0), pChessBoard)
                                        && !SquareIsThreatened(pChessBoard.GetChessBoard().GetBoard()[4, 7].GetPiece(), new BoardPosition(6, 0), pChessBoard))
                                    {
                                        boardPositionList.Add(new BoardPosition(6, 0));
                                    }
                                }
                            }
                        }
                    }
                }

            }

            return boardPositionList;
        }

        public static bool SquareIsThreatened(Piece pPiece, BoardPosition bp, ChessBoardView pChessBoard)
        {

            Piece.Colors pieceColor = pPiece.color;
            List<BoardPosition> moveList;
            for (int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    if(pChessBoard.GetChessBoard().GetBoard()[i, j].HasPiece())
                    {
                        if(!(pPiece.IsSameColor(pChessBoard.GetChessBoard().GetBoard()[i, j].GetPiece())))
                        {
                            if(pChessBoard.GetChessBoard().GetBoard()[i,j].GetPiece().pieceType.type == PieceType.Types.QUEEN)
                            {
                            moveList = AvaiableMoves(new BoardPosition(i, j), pChessBoard);
                            Console.WriteLine("Position: " + i + "," + j);
                                foreach (BoardPosition b in moveList)
                                {                                    Console.Write(b.ToString() + " ");
                                    if (b.IsSamePosition(bp))
                                    {
                                        return true;
                                    }

                                }

                            }
                        }
                    }

                }
            }

            return false;
        }



    }
}
