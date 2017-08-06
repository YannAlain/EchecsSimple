﻿namespace ChessApp3
{
    partial class GameControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.PlayerTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PieceTextBox = new System.Windows.Forms.TextBox();
            this.WhiteMovesButton = new System.Windows.Forms.Button();
            this.BlackMovesButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Player";
            // 
            // PlayerTextBox
            // 
            this.PlayerTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlayerTextBox.Location = new System.Drawing.Point(79, 58);
            this.PlayerTextBox.Name = "PlayerTextBox";
            this.PlayerTextBox.ReadOnly = true;
            this.PlayerTextBox.Size = new System.Drawing.Size(129, 20);
            this.PlayerTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Piece";
            // 
            // PieceTextBox
            // 
            this.PieceTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PieceTextBox.Location = new System.Drawing.Point(79, 91);
            this.PieceTextBox.Name = "PieceTextBox";
            this.PieceTextBox.ReadOnly = true;
            this.PieceTextBox.Size = new System.Drawing.Size(129, 20);
            this.PieceTextBox.TabIndex = 3;
            // 
            // WhiteMovesButton
            // 
            this.WhiteMovesButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WhiteMovesButton.Location = new System.Drawing.Point(22, 127);
            this.WhiteMovesButton.Name = "WhiteMovesButton";
            this.WhiteMovesButton.Size = new System.Drawing.Size(186, 25);
            this.WhiteMovesButton.TabIndex = 4;
            this.WhiteMovesButton.Text = "Display White Moves";
            this.WhiteMovesButton.UseVisualStyleBackColor = true;
            this.WhiteMovesButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnToggleDisplayMove);
            this.WhiteMovesButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnToggleDisplayMove);
            // 
            // BlackMovesButton
            // 
            this.BlackMovesButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BlackMovesButton.Location = new System.Drawing.Point(22, 158);
            this.BlackMovesButton.Name = "BlackMovesButton";
            this.BlackMovesButton.Size = new System.Drawing.Size(186, 25);
            this.BlackMovesButton.TabIndex = 5;
            this.BlackMovesButton.Text = "Display Black Moves";
            this.BlackMovesButton.UseVisualStyleBackColor = true;
            this.BlackMovesButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnToggleDisplayMove);
            this.BlackMovesButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnToggleDisplayMove);
            // 
            // GameControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BlackMovesButton);
            this.Controls.Add(this.WhiteMovesButton);
            this.Controls.Add(this.PieceTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PlayerTextBox);
            this.Controls.Add(this.label1);
            this.Name = "GameControl";
            this.Size = new System.Drawing.Size(221, 394);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox PlayerTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox PieceTextBox;
        private System.Windows.Forms.Button WhiteMovesButton;
        private System.Windows.Forms.Button BlackMovesButton;
    }
}
