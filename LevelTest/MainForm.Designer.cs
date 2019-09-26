namespace LevelTest
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
            this.closeButton = new System.Windows.Forms.Label();
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
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.BackColor = System.Drawing.Color.Red;
            this.closeButton.Location = new System.Drawing.Point(776, 2);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(23, 23);
            this.closeButton.TabIndex = 0;
            this.closeButton.Text = "X";
            this.closeButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.closeButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.minigamePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel minigamePanel;
        private System.Windows.Forms.Timer minigameClockT;
        private System.Windows.Forms.Label closeButton;
    }
}

