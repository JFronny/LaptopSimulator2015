﻿namespace SIT
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
            if (disposing & (components != null))
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
            this.button1 = new System.Windows.Forms.Button();
            this.minigameClockT = new System.Windows.Forms.Timer(this.components);
            this.minigamePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // minigamePanel
            // 
            this.minigamePanel.BackColor = System.Drawing.Color.Black;
            this.minigamePanel.Controls.Add(this.button1);
            this.minigamePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.minigamePanel.Location = new System.Drawing.Point(0, 0);
            this.minigamePanel.Name = "minigamePanel";
            this.minigamePanel.Size = new System.Drawing.Size(800, 450);
            this.minigamePanel.TabIndex = 0;
            this.minigamePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel1_Paint);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(777, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(23, 23);
            this.button1.TabIndex = 0;
            this.button1.TabStop = false;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // minigameClockT
            // 
            this.minigameClockT.Enabled = true;
            this.minigameClockT.Interval = 17;
            this.minigameClockT.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.minigamePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.minigamePanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel minigamePanel;
        private System.Windows.Forms.Timer minigameClockT;
        private System.Windows.Forms.Button button1;
    }
}
