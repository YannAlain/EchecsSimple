﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using ChessEngine;

namespace ChessApp3
{
    public class ChessGameView
    {
        Graphics graphix_ = null;
        PositionPixel Pixel2Position_ = null;
        private ChessGame Game_;
        private Boolean DrawBlackMoves_ = false;
        private Boolean DrawWhiteMoves_ = false;
        public Color DarkColor = Color.DarkBlue;
        public Color LightColor = Color.LightGoldenrodYellow;

        public ChessGame Game { get => Game_;}
        public Graphics Graphix { get => graphix_; set => graphix_ = value; }
        public PositionPixel Pixel2Position { get => Pixel2Position_; set => Pixel2Position_ = value; }
        public bool DrawBlackMoves
        {
            get => DrawBlackMoves_;
            set
            {
                DrawBlackMoves_ = value;
                NeedsRedraw();
            }
        }

        public bool DrawWhiteMoves
        {
            get => DrawWhiteMoves_;
            set
            {
                DrawWhiteMoves_ = value;
                NeedsRedraw();
            }
        }

        public event EventHandler Redraw;


        public ChessGameView(ChessGame IGame)
        {
            Game_ = IGame;
        }

        protected virtual void NeedsRedraw()
        {
            if (Redraw != null)
                Redraw(this, new EventArgs());
        }

        public void GameChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Redraw Game From notification.");
            if (sender is ChessGame)
                DrawGame();
        }


        public void DrawGame()
        {
            if (Game_ == null)
                return;
            if (Graphix == null)
                return;
            if (Pixel2Position == null)
                return;

            DrawChessboard(Game.Board, Graphix, Pixel2Position);
            HightlightSelectedPiece(Game.SelectedPiece, Graphix, Pixel2Position);
            HightlightPossiblePositions(Game.SelectedPiece, Game.Board, Graphix, Pixel2Position);
            if (DrawBlackMoves)
            {
                HightlightPieceSetPossiblePositions(Game.BlackPieces, Game.Board, Graphix, Pixel2Position);
            }
            if (DrawWhiteMoves)
            {
                HightlightPieceSetPossiblePositions(Game.WhitePieces, Game.Board, Graphix, Pixel2Position);
            }
        }

        private void DrawChessboard(Chessboard iBoard, Graphics g, PositionPixel PP)
        {
            if (iBoard == null)
                return;

            SolidBrush DarkBrush = new SolidBrush(this.DarkColor);
            SolidBrush LightBrush = new SolidBrush(this.LightColor);
            bool Dark = false;
            for (int irow = 0; irow < 8; irow++)
            {
                for (int icol = 0; icol < 8; icol++)
                {
                    Square Cell = iBoard.GetSquare(new Position(icol, irow));// Board.Squares[icol, irow];
                    Dark = Cell.IsDark;
                    Point UpperCorner = PP.GetPositionUpperCorner(Cell.Position);
                    g.FillRectangle(Dark ? DarkBrush : LightBrush, UpperCorner.X, UpperCorner.Y, PP.PixelSquare, PP.PixelSquare);
                    if (Cell.HasPiece())
                    {
                        Piece P = Cell.Piece;
                        UpperCorner = PP.GetPositionUpperCorner(P.Position);
                        int PieceMargin = 2;
                        int PieceDim = PP.PixelSquare - 2 * PieceMargin;
                        g.DrawImage(P.Image, UpperCorner.X + PieceMargin, UpperCorner.Y + PieceMargin, PieceDim, PieceDim);
                    }
                }
            }
        }

        private void HightlightSelectedPiece(Piece selectedPiece, Graphics g, PositionPixel PP)
        {
            if (selectedPiece == null)
                return;

            SolidBrush redBrush = new SolidBrush(Color.Red);
            int Width = 4;
            Pen HighlightPen = new Pen(redBrush);
            HighlightPen.Width = Width;
            HighlightPen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
            Point UpperCorner = PP.GetPositionUpperCorner(selectedPiece.Position);
            int PieceMargin = Width/2;
            int PieceDim = PP.PixelSquare - 2 * PieceMargin;
            g.DrawRectangle(HighlightPen, UpperCorner.X + PieceMargin, UpperCorner.Y + PieceMargin, PieceDim, PieceDim);
        }

        private void HightlightPieceSetPossiblePositions(PieceSet set, Chessboard iBoard, Graphics g, PositionPixel PP)
        {
            foreach( Piece piece in set)
            {
                HightlightPossiblePositions(piece, iBoard, g, PP);
            }
        }


        private void HightlightPossiblePositions(Piece piece, Chessboard iBoard, Graphics g, PositionPixel PP)
        {
            if ((piece == null) || (piece.PossibleNewPositions == null))
                return;

            SolidBrush semiTransGreenBrush = new SolidBrush(Color.FromArgb(128, 20, 255, 20));
            SolidBrush semiTransRedBrush = new SolidBrush(Color.FromArgb(128, 255, 20, 20));

            foreach ( Position P in piece.PossibleNewPositions)
            {
                Point UpperCorner = PP.GetPositionUpperCorner(P);
                Brush brush = iBoard.GetSquare(P).HasPiece() ? semiTransRedBrush : semiTransGreenBrush;
                g.FillRectangle(brush, UpperCorner.X, UpperCorner.Y, PP.PixelSquare, PP.PixelSquare);
            }

            SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
            SolidBrush redBrush = new SolidBrush(Color.Red);

            Point current = PP.GetPositionCenter(piece.Position);
            if ((piece.PossibleNewPositions == null) || (piece.PossibleNewPositions.Count == 0))
                FillCircle(g, redBrush, current, PP.PixelSquare / 10);
            else
                FillCircle(g, yellowBrush, current, PP.PixelSquare / 10);

            int Width = 2;
            Pen yellowPen = new Pen(yellowBrush);
            yellowPen.Width = Width;
            //HighlightPen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;

            foreach (Position P in piece.PossibleNewPositions)
            {
                Point center = PP.GetPositionCenter(P);

                FillCircle(g, yellowBrush, center, PP.PixelSquare / 10);

                g.DrawLine(yellowPen, current, center);
            }
        }

        void FillCircle(Graphics g, Brush brush,
                              Point center, int radius)
        {
            g.FillEllipse(brush, center.X - radius, center.Y - radius,
                          radius + radius, radius + radius);
        }
    }
}
