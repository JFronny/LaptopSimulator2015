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
            this.components = new System.ComponentModel.Container();
            this.skipButton = new System.Windows.Forms.Button();
            this.dialog = new System.Windows.Forms.Panel();
            this.tabs = new LaptopSimulator2015.WizardTab();
            this.p1 = new System.Windows.Forms.TabPage();
            this.p1descLabel = new System.Windows.Forms.Label();
            this.p1controlPanel = new System.Windows.Forms.Panel();
            this.p1continue = new System.Windows.Forms.Button();
            this.p1lang = new System.Windows.Forms.ComboBox();
            this.p1titleLabel = new System.Windows.Forms.Label();
            this.p2 = new System.Windows.Forms.TabPage();
            this.p2continue = new System.Windows.Forms.Button();
            this.p2privacyLabel = new System.Windows.Forms.Label();
            this.p3 = new System.Windows.Forms.TabPage();
            this.p3spacingPanel1 = new System.Windows.Forms.Panel();
            this.p3continue = new System.Windows.Forms.Button();
            this.p3spacingPanel2 = new System.Windows.Forms.Panel();
            this.p4 = new System.Windows.Forms.TabPage();
            this.tutorialPanel = new System.Windows.Forms.Panel();
            this.p5 = new System.Windows.Forms.TabPage();
            this.p5completeLabel = new System.Windows.Forms.Label();
            this.p5controlPanel = new System.Windows.Forms.Panel();
            this.p5reboot = new System.Windows.Forms.Button();
            this.p5title = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.cancelButton = new System.Windows.Forms.Button();
            this.progressTimer = new System.Windows.Forms.Timer(this.components);
            this.dialog.SuspendLayout();
            this.tabs.SuspendLayout();
            this.p1.SuspendLayout();
            this.p1controlPanel.SuspendLayout();
            this.p2.SuspendLayout();
            this.p3.SuspendLayout();
            this.p4.SuspendLayout();
            this.p5.SuspendLayout();
            this.p5controlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // skipButton
            // 
            this.skipButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.skipButton.Location = new System.Drawing.Point(1220, 0);
            this.skipButton.Name = "skipButton";
            this.skipButton.Size = new System.Drawing.Size(80, 23);
            this.skipButton.TabIndex = 0;
            this.skipButton.Text = "SKIP";
            this.skipButton.UseVisualStyleBackColor = true;
            this.skipButton.Visible = false;
            this.skipButton.Click += new System.EventHandler(this.skipButton_Click);
            // 
            // dialog
            // 
            this.dialog.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dialog.BackColor = System.Drawing.Color.Silver;
            this.dialog.Controls.Add(this.tabs);
            this.dialog.Location = new System.Drawing.Point(450, 150);
            this.dialog.Name = "dialog";
            this.dialog.Size = new System.Drawing.Size(400, 400);
            this.dialog.TabIndex = 1;
            // 
            // tabs
            // 
            this.tabs.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabs.Controls.Add(this.p1);
            this.tabs.Controls.Add(this.p2);
            this.tabs.Controls.Add(this.p3);
            this.tabs.Controls.Add(this.p4);
            this.tabs.Controls.Add(this.p5);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.ItemSize = new System.Drawing.Size(10, 21);
            this.tabs.Location = new System.Drawing.Point(0, 0);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(400, 400);
            this.tabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabs.TabIndex = 0;
            this.tabs.TabStop = false;
            // 
            // p1
            // 
            this.p1.BackColor = System.Drawing.Color.White;
            this.p1.Controls.Add(this.p1descLabel);
            this.p1.Controls.Add(this.p1controlPanel);
            this.p1.Controls.Add(this.p1titleLabel);
            this.p1.Location = new System.Drawing.Point(4, 25);
            this.p1.Name = "p1";
            this.p1.Padding = new System.Windows.Forms.Padding(3);
            this.p1.Size = new System.Drawing.Size(392, 371);
            this.p1.TabIndex = 0;
            // 
            // p1descLabel
            // 
            this.p1descLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p1descLabel.Location = new System.Drawing.Point(3, 65);
            this.p1descLabel.Name = "p1descLabel";
            this.p1descLabel.Size = new System.Drawing.Size(386, 258);
            this.p1descLabel.TabIndex = 0;
            this.p1descLabel.Text = "label2";
            this.p1descLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // p1controlPanel
            // 
            this.p1controlPanel.BackColor = System.Drawing.Color.Silver;
            this.p1controlPanel.Controls.Add(this.p1continue);
            this.p1controlPanel.Controls.Add(this.p1lang);
            this.p1controlPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.p1controlPanel.Location = new System.Drawing.Point(3, 323);
            this.p1controlPanel.Name = "p1controlPanel";
            this.p1controlPanel.Size = new System.Drawing.Size(386, 45);
            this.p1controlPanel.TabIndex = 1;
            // 
            // p1continue
            // 
            this.p1continue.Location = new System.Drawing.Point(304, 10);
            this.p1continue.Name = "p1continue";
            this.p1continue.Size = new System.Drawing.Size(75, 23);
            this.p1continue.TabIndex = 1;
            this.p1continue.Text = "Continue";
            this.p1continue.UseVisualStyleBackColor = true;
            this.p1continue.Click += new System.EventHandler(this.Continue1_Click);
            // 
            // p1lang
            // 
            this.p1lang.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.p1lang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.p1lang.FormattingEnabled = true;
            this.p1lang.Items.AddRange(new object[] {
            "de",
            "en"});
            this.p1lang.Location = new System.Drawing.Point(12, 12);
            this.p1lang.Name = "p1lang";
            this.p1lang.Size = new System.Drawing.Size(121, 21);
            this.p1lang.TabIndex = 0;
            this.p1lang.SelectedIndexChanged += new System.EventHandler(this.lang_SelectedIndexChanged);
            // 
            // p1titleLabel
            // 
            this.p1titleLabel.BackColor = System.Drawing.Color.Silver;
            this.p1titleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.p1titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.p1titleLabel.Location = new System.Drawing.Point(3, 3);
            this.p1titleLabel.Name = "p1titleLabel";
            this.p1titleLabel.Size = new System.Drawing.Size(386, 62);
            this.p1titleLabel.TabIndex = 0;
            this.p1titleLabel.Text = "LaptopSimulator2015";
            this.p1titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // p2
            // 
            this.p2.Controls.Add(this.p2continue);
            this.p2.Controls.Add(this.p2privacyLabel);
            this.p2.Location = new System.Drawing.Point(4, 25);
            this.p2.Name = "p2";
            this.p2.Padding = new System.Windows.Forms.Padding(3);
            this.p2.Size = new System.Drawing.Size(392, 371);
            this.p2.TabIndex = 1;
            // 
            // p2continue
            // 
            this.p2continue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.p2continue.Location = new System.Drawing.Point(311, 345);
            this.p2continue.Name = "p2continue";
            this.p2continue.Size = new System.Drawing.Size(75, 23);
            this.p2continue.TabIndex = 1;
            this.p2continue.Text = "Continue";
            this.p2continue.UseVisualStyleBackColor = true;
            this.p2continue.Click += new System.EventHandler(this.continue2_Click);
            // 
            // p2privacyLabel
            // 
            this.p2privacyLabel.AutoSize = true;
            this.p2privacyLabel.Location = new System.Drawing.Point(6, 3);
            this.p2privacyLabel.Name = "p2privacyLabel";
            this.p2privacyLabel.Size = new System.Drawing.Size(209, 13);
            this.p2privacyLabel.TabIndex = 0;
            this.p2privacyLabel.Text = "Alan, please add random \"Privacy\"-notices";
            // 
            // p3
            // 
            this.p3.BackColor = System.Drawing.Color.White;
            this.p3.Controls.Add(this.p3spacingPanel1);
            this.p3.Controls.Add(this.p3continue);
            this.p3.Controls.Add(this.p3spacingPanel2);
            this.p3.Location = new System.Drawing.Point(4, 25);
            this.p3.Name = "p3";
            this.p3.Size = new System.Drawing.Size(392, 371);
            this.p3.TabIndex = 2;
            // 
            // p3spacingPanel1
            // 
            this.p3spacingPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.p3spacingPanel1.Location = new System.Drawing.Point(0, 0);
            this.p3spacingPanel1.Name = "p3spacingPanel1";
            this.p3spacingPanel1.Size = new System.Drawing.Size(392, 100);
            this.p3spacingPanel1.TabIndex = 2;
            // 
            // p3continue
            // 
            this.p3continue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p3continue.Font = new System.Drawing.Font("Microsoft Sans Serif", 37F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.p3continue.Location = new System.Drawing.Point(0, 0);
            this.p3continue.Name = "p3continue";
            this.p3continue.Size = new System.Drawing.Size(392, 271);
            this.p3continue.TabIndex = 0;
            this.p3continue.Text = "INSTALL";
            this.p3continue.UseVisualStyleBackColor = true;
            this.p3continue.Click += new System.EventHandler(this.continue3_Click);
            // 
            // p3spacingPanel2
            // 
            this.p3spacingPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.p3spacingPanel2.Location = new System.Drawing.Point(0, 271);
            this.p3spacingPanel2.Name = "p3spacingPanel2";
            this.p3spacingPanel2.Size = new System.Drawing.Size(392, 100);
            this.p3spacingPanel2.TabIndex = 1;
            // 
            // p4
            // 
            this.p4.BackColor = System.Drawing.Color.White;
            this.p4.Controls.Add(this.tutorialPanel);
            this.p4.Location = new System.Drawing.Point(4, 25);
            this.p4.Name = "p4";
            this.p4.Size = new System.Drawing.Size(392, 371);
            this.p4.TabIndex = 3;
            // 
            // tutorialPanel
            // 
            this.tutorialPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tutorialPanel.Location = new System.Drawing.Point(0, 0);
            this.tutorialPanel.Name = "tutorialPanel";
            this.tutorialPanel.Size = new System.Drawing.Size(392, 371);
            this.tutorialPanel.TabIndex = 0;
            this.tutorialPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.tutorialPanel_Paint);
            // 
            // p5
            // 
            this.p5.BackColor = System.Drawing.Color.White;
            this.p5.Controls.Add(this.p5completeLabel);
            this.p5.Controls.Add(this.p5controlPanel);
            this.p5.Controls.Add(this.p5title);
            this.p5.Location = new System.Drawing.Point(4, 25);
            this.p5.Name = "p5";
            this.p5.Size = new System.Drawing.Size(392, 371);
            this.p5.TabIndex = 4;
            // 
            // p5completeLabel
            // 
            this.p5completeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p5completeLabel.Location = new System.Drawing.Point(0, 62);
            this.p5completeLabel.Name = "p5completeLabel";
            this.p5completeLabel.Size = new System.Drawing.Size(392, 264);
            this.p5completeLabel.TabIndex = 2;
            this.p5completeLabel.Text = "label2";
            this.p5completeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // p5controlPanel
            // 
            this.p5controlPanel.Controls.Add(this.p5reboot);
            this.p5controlPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.p5controlPanel.Location = new System.Drawing.Point(0, 326);
            this.p5controlPanel.Name = "p5controlPanel";
            this.p5controlPanel.Size = new System.Drawing.Size(392, 45);
            this.p5controlPanel.TabIndex = 4;
            // 
            // p5reboot
            // 
            this.p5reboot.Location = new System.Drawing.Point(304, 10);
            this.p5reboot.Name = "p5reboot";
            this.p5reboot.Size = new System.Drawing.Size(75, 23);
            this.p5reboot.TabIndex = 1;
            this.p5reboot.Text = "Reboot";
            this.p5reboot.UseVisualStyleBackColor = true;
            this.p5reboot.Click += new System.EventHandler(this.p5reboot_Click);
            // 
            // p5title
            // 
            this.p5title.BackColor = System.Drawing.Color.Silver;
            this.p5title.Dock = System.Windows.Forms.DockStyle.Top;
            this.p5title.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.p5title.Location = new System.Drawing.Point(0, 0);
            this.p5title.Name = "p5title";
            this.p5title.Size = new System.Drawing.Size(392, 62);
            this.p5title.TabIndex = 3;
            this.p5title.Text = "LaptopSimulator2015";
            this.p5title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.Location = new System.Drawing.Point(0, 677);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1300, 23);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 2;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(0, 0);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(80, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // progressTimer
            // 
            this.progressTimer.Interval = 120;
            this.progressTimer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Tutorial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1300, 700);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.dialog);
            this.Controls.Add(this.skipButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Tutorial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tutorial";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.Tutorial_Shown);
            this.dialog.ResumeLayout(false);
            this.tabs.ResumeLayout(false);
            this.p1.ResumeLayout(false);
            this.p1controlPanel.ResumeLayout(false);
            this.p2.ResumeLayout(false);
            this.p2.PerformLayout();
            this.p3.ResumeLayout(false);
            this.p4.ResumeLayout(false);
            this.p5.ResumeLayout(false);
            this.p5controlPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button skipButton;
        private System.Windows.Forms.Panel dialog;
        private System.Windows.Forms.ProgressBar progressBar;
        private WizardTab tabs;
        private System.Windows.Forms.TabPage p1;
        private System.Windows.Forms.TabPage p2;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label p1titleLabel;
        private System.Windows.Forms.Label p1descLabel;
        private System.Windows.Forms.Panel p1controlPanel;
        private System.Windows.Forms.ComboBox p1lang;
        private System.Windows.Forms.Button p1continue;
        private System.Windows.Forms.Label p2privacyLabel;
        private System.Windows.Forms.Button p2continue;
        private System.Windows.Forms.TabPage p3;
        private System.Windows.Forms.Panel p3spacingPanel1;
        private System.Windows.Forms.Panel p3spacingPanel2;
        private System.Windows.Forms.Button p3continue;
        private System.Windows.Forms.Timer progressTimer;
        private System.Windows.Forms.TabPage p4;
        private System.Windows.Forms.TabPage p5;
        private System.Windows.Forms.Label p5completeLabel;
        private System.Windows.Forms.Panel p5controlPanel;
        private System.Windows.Forms.Button p5reboot;
        private System.Windows.Forms.Label p5title;
        private System.Windows.Forms.Panel tutorialPanel;
    }
}