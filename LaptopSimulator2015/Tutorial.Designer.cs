namespace LaptopSimulator2015
{
    partial class Tutorial
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
            this.skipButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // skipButton
            // 
            this.skipButton.Location = new System.Drawing.Point(761, 0);
            this.skipButton.Name = "skipButton";
            this.skipButton.Size = new System.Drawing.Size(39, 23);
            this.skipButton.TabIndex = 0;
            this.skipButton.Text = "SKIP";
            this.skipButton.UseVisualStyleBackColor = true;
            this.skipButton.Visible = false;
            this.skipButton.Click += new System.EventHandler(this.skipButton_Click);
            // 
            // Tutorial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.skipButton);
            this.Name = "Tutorial";
            this.Text = "Tutorial";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button skipButton;
    }
}