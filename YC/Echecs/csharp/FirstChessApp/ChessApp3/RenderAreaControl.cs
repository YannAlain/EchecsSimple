﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessApp3
{
    public partial class RenderAreaControl : UserControl
    {
        private BufferedGraphicsContext context;
        private BufferedGraphics grafx = null;

        //private byte bufferingMode;
        //private string[] bufferingModeStrings =
        //    { "Draw to Form without OptimizedDoubleBufferring control style",
        //  "Draw to Form using OptimizedDoubleBuffering control style",
        //  "Draw to HDC for form" };

        private ChessGameView GameView_;
        private PositionPixel PosPix_;

        public ChessGameView GameView
        {
            get => GameView_;
            set
            {
                GameView_ = value;
                if (grafx != null)
                DrawToBuffer(grafx.Graphics, true);
            }
        }

        public PositionPixel PosPix { get => PosPix_; set => PosPix_ = value; }

        public RenderAreaControl()
        {
            InitializeComponent();

            InitializeRendering();
        }

        void InitializeRendering()
        {
            GameView = null;
            PosPix_ = new PositionPixel();
            PosPix.PixelDimension = Math.Min(this.Width, this.Height);

            // 
            // DoubleBufferedForm
            // 
            this.Text = "User double buffering";
            this.MouseDown += new MouseEventHandler(this.MouseDownHandler);
            this.Resize += new EventHandler(this.OnResize);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);


            //bufferingMode = 2;

            // Retrieves the BufferedGraphicsContext for the 
            // current application domain.
            context = BufferedGraphicsManager.Current;

            // Sets the maximum size for the primary graphics buffer
            // of the buffered graphics context for the application
            // domain.  Any allocation requests for a buffer larger 
            // than this will create a temporary buffered graphics 
            // context to host the graphics buffer.
            context.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);

            // Allocates a graphics buffer the size of this form
            // using the pixel format of the Graphics created by 
            // the Form.CreateGraphics() method, which returns a 
            // Graphics object that matches the pixel format of the form.
            grafx = context.Allocate(this.CreateGraphics(),
                 new Rectangle(0, 0, this.Width, this.Height));

            // Draw the first frame to the buffer.
            DrawToBuffer(grafx.Graphics, true);
        }

        private void MouseDownHandler(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right)
            //{

            //}
        }

        private void OnResize(object sender, EventArgs e)
        {
            // Re-create the graphics buffer for a new window size.
            context.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);
            if (grafx != null)
            {
                grafx.Dispose();
                grafx = null;
            }
            grafx = context.Allocate(this.CreateGraphics(),
                new Rectangle(0, 0, this.Width, this.Height));

            PosPix.PixelDimension = Math.Min(this.Width, this.Height);

            // Cause the background to be cleared and redraw.
            DrawToBuffer(grafx.Graphics, true);
            this.Refresh();
        }

        private void DrawToBuffer(Graphics g, bool drawBackground)
        {
            // Clear the graphics buffer every five updates.
            if (drawBackground)
            {
                grafx.Graphics.FillRectangle(Brushes.White, 0, 0, this.Width, this.Height);
            }

            if (GameView != null)
                GameView.DrawGame(g, PosPix);
            else
            {
                //Crerate a temporary Game and Gameview to get some feedback in the Designer
                ChessGame tempGame = new ChessGame();
                ChessGameView tempView = new ChessGameView(tempGame);
                grafx.Graphics.FillRectangle(Brushes.White, 0, 0, this.Width, this.Height);
                tempView.DrawGame(g, PosPix);
                // then tempGame and tempView should be garbage collected
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            grafx.Render(e.Graphics);
        }
    }
}
