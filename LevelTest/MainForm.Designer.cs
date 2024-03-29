﻿namespace LevelTest
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.minigamePanel = new System.Windows.Forms.Panel();
            this.minigameClockT = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // minigamePanel
            // 
            this.minigamePanel.BackColor = System.Drawing.Color.Black;
            this.minigamePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.minigamePanel.Location = new System.Drawing.Point(0, 0);
            this.minigamePanel.Name = "minigamePanel";
            this.minigamePanel.Size = new System.Drawing.Size(800, 450);
            this.minigamePanel.TabIndex = 1;
            this.minigamePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.MinigamePanel_Paint);
            // 
            // minigameClockT
            // 
            this.minigameClockT.Enabled = true;
            this.minigameClockT.Interval = 17;
            this.minigameClockT.Tick += new System.EventHandler(this.MinigameClockT_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.minigamePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "Form1";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel minigamePanel;
        private System.Windows.Forms.Timer minigameClockT;
    }
}

