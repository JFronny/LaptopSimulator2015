using System;

namespace LaptopSimulator2015
{
    partial class FakeDesktop
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
            /*if (ptrHook != IntPtr.Zero)
            {
                UnhookWindowsHookEx(ptrHook);
                ptrHook = IntPtr.Zero;
            }*/
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FakeDesktop));
            this.winKey = new System.Windows.Forms.Button();
            this.winMenuPanel = new System.Windows.Forms.Panel();
            this.winMenuStart = new System.Windows.Forms.Button();
            this.winMenuTitle = new System.Windows.Forms.Label();
            this.winMenuText = new System.Windows.Forms.Label();
            this.winMenuExit = new System.Windows.Forms.Button();
            this.winTaskbar = new System.Windows.Forms.Panel();
            this.subsLabel = new System.Windows.Forms.Label();
            this.winDesktop = new System.Windows.Forms.FlowLayoutPanel();
            this.options_1 = new System.Windows.Forms.Panel();
            this.options_2 = new System.Windows.Forms.Panel();
            this.levelWindow = new System.Windows.Forms.Panel();
            this.levelWindowC1 = new System.Windows.Forms.Button();
            this.levelWindowContents = new System.Windows.Forms.TabControl();
            this.levelWindow1 = new System.Windows.Forms.TabPage();
            this.levelWindowText1 = new System.Windows.Forms.Label();
            this.levelWindow2 = new System.Windows.Forms.TabPage();
            this.captchaBox = new System.Windows.Forms.TextBox();
            this.captchaPanel = new System.Windows.Forms.Panel();
            this.levelWindowText2 = new System.Windows.Forms.Label();
            this.levelWindow3 = new System.Windows.Forms.TabPage();
            this.levelWindowProgress = new System.Windows.Forms.ProgressBar();
            this.levelWindowText3 = new System.Windows.Forms.Label();
            this.levelWindowHeader = new System.Windows.Forms.Panel();
            this.levelWindowHeaderExit = new System.Windows.Forms.Label();
            this.levelWindowIcon = new System.Windows.Forms.Panel();
            this.levelWindowTitle = new System.Windows.Forms.Label();
            this.levelWindowProgressT = new System.Windows.Forms.Timer(this.components);
            this.winTimeLabel = new System.Windows.Forms.Label();
            this.winTimeTimer = new System.Windows.Forms.Timer(this.components);
            this.minigamePanel = new System.Windows.Forms.Panel();
            this.minigameClockT = new System.Windows.Forms.Timer(this.components);
            this.optionsWindow = new System.Windows.Forms.Panel();
            this.optionsWindowReset = new System.Windows.Forms.Button();
            this.optionsWindowLang = new System.Windows.Forms.ComboBox();
            this.optionsWindowSubs = new System.Windows.Forms.CheckBox();
            this.optionsWindowExit = new System.Windows.Forms.Button();
            this.optionsWindowLSD = new System.Windows.Forms.CheckBox();
            this.optionsWindowWamLabel = new System.Windows.Forms.Label();
            this.optionsWindowWam = new System.Windows.Forms.TrackBar();
            this.optionsWindowHeader = new System.Windows.Forms.Panel();
            this.optionsWindowHeaderExit = new System.Windows.Forms.Label();
            this.optionsWindowIcon = new System.Windows.Forms.Panel();
            this.optionsWindowTitle = new System.Windows.Forms.Label();
            this.lsdEffectT = new System.Windows.Forms.Timer(this.components);
            this.winMenuPanel.SuspendLayout();
            this.winTaskbar.SuspendLayout();
            this.winDesktop.SuspendLayout();
            this.options_1.SuspendLayout();
            this.levelWindow.SuspendLayout();
            this.levelWindowContents.SuspendLayout();
            this.levelWindow1.SuspendLayout();
            this.levelWindow2.SuspendLayout();
            this.levelWindow3.SuspendLayout();
            this.levelWindowHeader.SuspendLayout();
            this.optionsWindow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.optionsWindowWam)).BeginInit();
            this.optionsWindowHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // winKey
            // 
            this.winKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.winKey.BackColor = System.Drawing.Color.Blue;
            this.winKey.Font = new System.Drawing.Font("Impact", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.winKey.ForeColor = System.Drawing.Color.White;
            this.winKey.Location = new System.Drawing.Point(0, 889);
            this.winKey.Name = "winKey";
            this.winKey.Size = new System.Drawing.Size(30, 30);
            this.winKey.TabIndex = 0;
            this.winKey.Text = "*";
            this.winKey.UseVisualStyleBackColor = false;
            this.winKey.Click += new System.EventHandler(this.WinKey_Click);
            // 
            // winMenuPanel
            // 
            this.winMenuPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.winMenuPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.winMenuPanel.Controls.Add(this.winMenuStart);
            this.winMenuPanel.Controls.Add(this.winMenuTitle);
            this.winMenuPanel.Controls.Add(this.winMenuText);
            this.winMenuPanel.Controls.Add(this.winMenuExit);
            this.winMenuPanel.Location = new System.Drawing.Point(1, 671);
            this.winMenuPanel.Name = "winMenuPanel";
            this.winMenuPanel.Size = new System.Drawing.Size(200, 220);
            this.winMenuPanel.TabIndex = 1;
            // 
            // winMenuStart
            // 
            this.winMenuStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.winMenuStart.Location = new System.Drawing.Point(0, 171);
            this.winMenuStart.Name = "winMenuStart";
            this.winMenuStart.Size = new System.Drawing.Size(200, 25);
            this.winMenuStart.TabIndex = 3;
            this.winMenuStart.Text = "Start";
            this.winMenuStart.UseVisualStyleBackColor = false;
            this.winMenuStart.Click += new System.EventHandler(this.WinMenuStart_Click);
            // 
            // winMenuTitle
            // 
            this.winMenuTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.winMenuTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.winMenuTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.winMenuTitle.Location = new System.Drawing.Point(3, 0);
            this.winMenuTitle.Name = "winMenuTitle";
            this.winMenuTitle.Size = new System.Drawing.Size(194, 32);
            this.winMenuTitle.TabIndex = 2;
            this.winMenuTitle.Text = "Laptop Simulator 2015";
            this.winMenuTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // winMenuText
            // 
            this.winMenuText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.winMenuText.Location = new System.Drawing.Point(3, 32);
            this.winMenuText.Name = "winMenuText";
            this.winMenuText.Size = new System.Drawing.Size(194, 136);
            this.winMenuText.TabIndex = 1;
            this.winMenuText.Text = "Hello and welcome to my game, I hope you\'ll like it. My name is CreepyCrafter24, " +
    "btw. (FunFact: This game was originally created as the result of a conversation " +
    "with a classmate)";
            // 
            // winMenuExit
            // 
            this.winMenuExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.winMenuExit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.winMenuExit.Location = new System.Drawing.Point(0, 195);
            this.winMenuExit.Name = "winMenuExit";
            this.winMenuExit.Size = new System.Drawing.Size(200, 25);
            this.winMenuExit.TabIndex = 0;
            this.winMenuExit.Text = "Exit";
            this.winMenuExit.UseVisualStyleBackColor = false;
            this.winMenuExit.Click += new System.EventHandler(this.WinMenuExit_Click);
            // 
            // winTaskbar
            // 
            this.winTaskbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.winTaskbar.BackColor = System.Drawing.Color.Navy;
            this.winTaskbar.Controls.Add(this.subsLabel);
            this.winTaskbar.Location = new System.Drawing.Point(30, 889);
            this.winTaskbar.Name = "winTaskbar";
            this.winTaskbar.Size = new System.Drawing.Size(1357, 30);
            this.winTaskbar.TabIndex = 3;
            // 
            // subsLabel
            // 
            this.subsLabel.AutoSize = true;
            this.subsLabel.BackColor = System.Drawing.Color.Navy;
            this.subsLabel.Location = new System.Drawing.Point(6, 9);
            this.subsLabel.Name = "subsLabel";
            this.subsLabel.Size = new System.Drawing.Size(0, 13);
            this.subsLabel.TabIndex = 0;
            // 
            // winDesktop
            // 
            this.winDesktop.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.winDesktop.Controls.Add(this.options_1);
            this.winDesktop.Enabled = false;
            this.winDesktop.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.winDesktop.Location = new System.Drawing.Point(12, 12);
            this.winDesktop.Name = "winDesktop";
            this.winDesktop.Size = new System.Drawing.Size(1363, 871);
            this.winDesktop.TabIndex = 5;
            // 
            // options_1
            // 
            this.options_1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.options_1.Controls.Add(this.options_2);
            this.options_1.Location = new System.Drawing.Point(3, 3);
            this.options_1.Name = "options_1";
            this.options_1.Size = new System.Drawing.Size(50, 50);
            this.options_1.TabIndex = 5;
            // 
            // options_2
            // 
            this.options_2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.options_2.BackColor = System.Drawing.Color.Blue;
            this.options_2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("options_2.BackgroundImage")));
            this.options_2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.options_2.Location = new System.Drawing.Point(2, 2);
            this.options_2.Name = "options_2";
            this.options_2.Size = new System.Drawing.Size(46, 46);
            this.options_2.TabIndex = 0;
            this.options_2.DoubleClick += new System.EventHandler(this.Options_2_DoubleClick);
            // 
            // levelWindow
            // 
            this.levelWindow.BackColor = System.Drawing.SystemColors.Window;
            this.levelWindow.Controls.Add(this.levelWindowC1);
            this.levelWindow.Controls.Add(this.levelWindowContents);
            this.levelWindow.Controls.Add(this.levelWindowHeader);
            this.levelWindow.Location = new System.Drawing.Point(10, 100);
            this.levelWindow.Name = "levelWindow";
            this.levelWindow.Size = new System.Drawing.Size(502, 268);
            this.levelWindow.TabIndex = 5;
            this.levelWindow.Visible = false;
            // 
            // levelWindowC1
            // 
            this.levelWindowC1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.levelWindowC1.Location = new System.Drawing.Point(415, 234);
            this.levelWindowC1.Name = "levelWindowC1";
            this.levelWindowC1.Size = new System.Drawing.Size(75, 23);
            this.levelWindowC1.TabIndex = 2;
            this.levelWindowC1.Text = "Continue";
            this.levelWindowC1.UseVisualStyleBackColor = true;
            this.levelWindowC1.Click += new System.EventHandler(this.LevelWindowC1_Click);
            // 
            // levelWindowContents
            // 
            this.levelWindowContents.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.levelWindowContents.Controls.Add(this.levelWindow1);
            this.levelWindowContents.Controls.Add(this.levelWindow2);
            this.levelWindowContents.Controls.Add(this.levelWindow3);
            this.levelWindowContents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.levelWindowContents.ItemSize = new System.Drawing.Size(20, 15);
            this.levelWindowContents.Location = new System.Drawing.Point(0, 20);
            this.levelWindowContents.Name = "levelWindowContents";
            this.levelWindowContents.SelectedIndex = 0;
            this.levelWindowContents.Size = new System.Drawing.Size(502, 248);
            this.levelWindowContents.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.levelWindowContents.TabIndex = 2;
            this.levelWindowContents.TabStop = false;
            // 
            // levelWindow1
            // 
            this.levelWindow1.Controls.Add(this.levelWindowText1);
            this.levelWindow1.Location = new System.Drawing.Point(4, 19);
            this.levelWindow1.Name = "levelWindow1";
            this.levelWindow1.Padding = new System.Windows.Forms.Padding(3);
            this.levelWindow1.Size = new System.Drawing.Size(494, 225);
            this.levelWindow1.TabIndex = 0;
            this.levelWindow1.UseVisualStyleBackColor = true;
            // 
            // levelWindowText1
            // 
            this.levelWindowText1.Location = new System.Drawing.Point(3, 3);
            this.levelWindowText1.Name = "levelWindowText1";
            this.levelWindowText1.Size = new System.Drawing.Size(488, 216);
            this.levelWindowText1.TabIndex = 1;
            this.levelWindowText1.Text = "Thank you for deciding to download RAM Installer 2.0 from our secure download ser" +
    "vers. Please wait a second while we install your RAM.\r\n\r\nYour Computer is about " +
    "to run a whole lot smoother!";
            // 
            // levelWindow2
            // 
            this.levelWindow2.Controls.Add(this.captchaBox);
            this.levelWindow2.Controls.Add(this.captchaPanel);
            this.levelWindow2.Controls.Add(this.levelWindowText2);
            this.levelWindow2.Location = new System.Drawing.Point(4, 19);
            this.levelWindow2.Name = "levelWindow2";
            this.levelWindow2.Padding = new System.Windows.Forms.Padding(3);
            this.levelWindow2.Size = new System.Drawing.Size(494, 225);
            this.levelWindow2.TabIndex = 1;
            this.levelWindow2.UseVisualStyleBackColor = true;
            // 
            // captchaBox
            // 
            this.captchaBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.captchaBox.Location = new System.Drawing.Point(212, 71);
            this.captchaBox.Name = "captchaBox";
            this.captchaBox.ReadOnly = true;
            this.captchaBox.Size = new System.Drawing.Size(276, 80);
            this.captchaBox.TabIndex = 2;
            this.captchaBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CaptchaBox_KeyPress);
            // 
            // captchaPanel
            // 
            this.captchaPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.captchaPanel.Location = new System.Drawing.Point(6, 19);
            this.captchaPanel.Name = "captchaPanel";
            this.captchaPanel.Size = new System.Drawing.Size(200, 197);
            this.captchaPanel.TabIndex = 1;
            this.captchaPanel.Click += new System.EventHandler(this.CaptchaPanel_Click);
            // 
            // levelWindowText2
            // 
            this.levelWindowText2.AutoSize = true;
            this.levelWindowText2.Location = new System.Drawing.Point(6, 3);
            this.levelWindowText2.Name = "levelWindowText2";
            this.levelWindowText2.Size = new System.Drawing.Size(435, 13);
            this.levelWindowText2.TabIndex = 0;
            this.levelWindowText2.Text = "To verify that you are human we ask you to fill in this Captcha. This will only t" +
    "ake a second.";
            // 
            // levelWindow3
            // 
            this.levelWindow3.Controls.Add(this.levelWindowProgress);
            this.levelWindow3.Controls.Add(this.levelWindowText3);
            this.levelWindow3.Location = new System.Drawing.Point(4, 19);
            this.levelWindow3.Name = "levelWindow3";
            this.levelWindow3.Size = new System.Drawing.Size(494, 225);
            this.levelWindow3.TabIndex = 2;
            this.levelWindow3.UseVisualStyleBackColor = true;
            // 
            // levelWindowProgress
            // 
            this.levelWindowProgress.Location = new System.Drawing.Point(6, 19);
            this.levelWindowProgress.Maximum = 200;
            this.levelWindowProgress.Name = "levelWindowProgress";
            this.levelWindowProgress.Size = new System.Drawing.Size(480, 167);
            this.levelWindowProgress.TabIndex = 1;
            // 
            // levelWindowText3
            // 
            this.levelWindowText3.AutoSize = true;
            this.levelWindowText3.Location = new System.Drawing.Point(3, 3);
            this.levelWindowText3.Name = "levelWindowText3";
            this.levelWindowText3.Size = new System.Drawing.Size(194, 13);
            this.levelWindowText3.TabIndex = 0;
            this.levelWindowText3.Text = "Please wait for the installation to finish...";
            // 
            // levelWindowHeader
            // 
            this.levelWindowHeader.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.levelWindowHeader.Controls.Add(this.levelWindowHeaderExit);
            this.levelWindowHeader.Controls.Add(this.levelWindowIcon);
            this.levelWindowHeader.Controls.Add(this.levelWindowTitle);
            this.levelWindowHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.levelWindowHeader.Location = new System.Drawing.Point(0, 0);
            this.levelWindowHeader.Name = "levelWindowHeader";
            this.levelWindowHeader.Size = new System.Drawing.Size(502, 20);
            this.levelWindowHeader.TabIndex = 0;
            this.levelWindowHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LevelWindowHeader_MouseDown);
            this.levelWindowHeader.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LevelWindowHeader_MouseMove);
            this.levelWindowHeader.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LevelWindowHeader_MouseUp);
            // 
            // levelWindowHeaderExit
            // 
            this.levelWindowHeaderExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.levelWindowHeaderExit.BackColor = System.Drawing.Color.Red;
            this.levelWindowHeaderExit.Font = new System.Drawing.Font("Marlett", 12F);
            this.levelWindowHeaderExit.Location = new System.Drawing.Point(462, 0);
            this.levelWindowHeaderExit.Name = "levelWindowHeaderExit";
            this.levelWindowHeaderExit.Size = new System.Drawing.Size(40, 20);
            this.levelWindowHeaderExit.TabIndex = 2;
            this.levelWindowHeaderExit.Text = "r";
            this.levelWindowHeaderExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.levelWindowHeaderExit.Click += new System.EventHandler(this.LevelWindowHeaderExit_Click);
            // 
            // levelWindowIcon
            // 
            this.levelWindowIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.levelWindowIcon.Location = new System.Drawing.Point(0, 0);
            this.levelWindowIcon.Name = "levelWindowIcon";
            this.levelWindowIcon.Size = new System.Drawing.Size(20, 20);
            this.levelWindowIcon.TabIndex = 1;
            // 
            // levelWindowTitle
            // 
            this.levelWindowTitle.AutoSize = true;
            this.levelWindowTitle.Location = new System.Drawing.Point(19, 4);
            this.levelWindowTitle.Name = "levelWindowTitle";
            this.levelWindowTitle.Size = new System.Drawing.Size(88, 13);
            this.levelWindowTitle.TabIndex = 0;
            this.levelWindowTitle.Text = "RAM Installer 2.0";
            this.levelWindowTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LevelWindowHeader_MouseDown);
            this.levelWindowTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LevelWindowHeader_MouseMove);
            this.levelWindowTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LevelWindowHeader_MouseUp);
            // 
            // levelWindowProgressT
            // 
            this.levelWindowProgressT.Tick += new System.EventHandler(this.LevelWindowProgressT_Tick);
            // 
            // winTimeLabel
            // 
            this.winTimeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.winTimeLabel.AutoSize = true;
            this.winTimeLabel.BackColor = System.Drawing.Color.Navy;
            this.winTimeLabel.ForeColor = System.Drawing.Color.White;
            this.winTimeLabel.Location = new System.Drawing.Point(1344, 898);
            this.winTimeLabel.Name = "winTimeLabel";
            this.winTimeLabel.Size = new System.Drawing.Size(34, 13);
            this.winTimeLabel.TabIndex = 0;
            this.winTimeLabel.Text = "00:00";
            // 
            // winTimeTimer
            // 
            this.winTimeTimer.Enabled = true;
            this.winTimeTimer.Interval = 1000;
            this.winTimeTimer.Tick += new System.EventHandler(this.WinTimeTimer_Tick);
            // 
            // minigamePanel
            // 
            this.minigamePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.minigamePanel.BackColor = System.Drawing.Color.Black;
            this.minigamePanel.Location = new System.Drawing.Point(564, 421);
            this.minigamePanel.Name = "minigamePanel";
            this.minigamePanel.Size = new System.Drawing.Size(800, 450);
            this.minigamePanel.TabIndex = 6;
            this.minigamePanel.Visible = false;
            this.minigamePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.InvadersPanel_Paint);
            // 
            // minigameClockT
            // 
            this.minigameClockT.Interval = 17;
            this.minigameClockT.Tick += new System.EventHandler(this.InvadersTimer_Tick);
            // 
            // optionsWindow
            // 
            this.optionsWindow.BackColor = System.Drawing.SystemColors.Window;
            this.optionsWindow.Controls.Add(this.optionsWindowReset);
            this.optionsWindow.Controls.Add(this.optionsWindowLang);
            this.optionsWindow.Controls.Add(this.optionsWindowSubs);
            this.optionsWindow.Controls.Add(this.optionsWindowExit);
            this.optionsWindow.Controls.Add(this.optionsWindowLSD);
            this.optionsWindow.Controls.Add(this.optionsWindowWamLabel);
            this.optionsWindow.Controls.Add(this.optionsWindowWam);
            this.optionsWindow.Controls.Add(this.optionsWindowHeader);
            this.optionsWindow.Location = new System.Drawing.Point(527, 26);
            this.optionsWindow.Name = "optionsWindow";
            this.optionsWindow.Size = new System.Drawing.Size(502, 94);
            this.optionsWindow.TabIndex = 6;
            this.optionsWindow.Visible = false;
            // 
            // optionsWindowReset
            // 
            this.optionsWindowReset.BackColor = System.Drawing.Color.Red;
            this.optionsWindowReset.Location = new System.Drawing.Point(332, 64);
            this.optionsWindowReset.Name = "optionsWindowReset";
            this.optionsWindowReset.Size = new System.Drawing.Size(75, 23);
            this.optionsWindowReset.TabIndex = 7;
            this.optionsWindowReset.Text = "Reset";
            this.optionsWindowReset.UseVisualStyleBackColor = false;
            this.optionsWindowReset.Click += new System.EventHandler(this.OptionsWindowReset_Click);
            // 
            // optionsWindowLang
            // 
            this.optionsWindowLang.FormattingEnabled = true;
            this.optionsWindowLang.Items.AddRange(new object[] {
            "de",
            "en"});
            this.optionsWindowLang.Location = new System.Drawing.Point(180, 62);
            this.optionsWindowLang.Name = "optionsWindowLang";
            this.optionsWindowLang.Size = new System.Drawing.Size(121, 21);
            this.optionsWindowLang.TabIndex = 6;
            // 
            // optionsWindowSubs
            // 
            this.optionsWindowSubs.AutoSize = true;
            this.optionsWindowSubs.Location = new System.Drawing.Point(108, 64);
            this.optionsWindowSubs.Name = "optionsWindowSubs";
            this.optionsWindowSubs.Size = new System.Drawing.Size(66, 17);
            this.optionsWindowSubs.TabIndex = 5;
            this.optionsWindowSubs.Text = "Subtitles";
            this.optionsWindowSubs.UseVisualStyleBackColor = true;
            // 
            // optionsWindowExit
            // 
            this.optionsWindowExit.Location = new System.Drawing.Point(413, 64);
            this.optionsWindowExit.Name = "optionsWindowExit";
            this.optionsWindowExit.Size = new System.Drawing.Size(75, 23);
            this.optionsWindowExit.TabIndex = 4;
            this.optionsWindowExit.Text = "OK";
            this.optionsWindowExit.UseVisualStyleBackColor = true;
            this.optionsWindowExit.Click += new System.EventHandler(this.OptionsWindowExit_Click);
            // 
            // optionsWindowLSD
            // 
            this.optionsWindowLSD.AutoSize = true;
            this.optionsWindowLSD.Font = new System.Drawing.Font("Comic Sans MS", 7F);
            this.optionsWindowLSD.ForeColor = System.Drawing.Color.Red;
            this.optionsWindowLSD.Location = new System.Drawing.Point(17, 64);
            this.optionsWindowLSD.Name = "optionsWindowLSD";
            this.optionsWindowLSD.Size = new System.Drawing.Size(73, 18);
            this.optionsWindowLSD.TabIndex = 3;
            this.optionsWindowLSD.Text = "LSD Mode";
            this.optionsWindowLSD.UseVisualStyleBackColor = true;
            this.optionsWindowLSD.CheckedChanged += new System.EventHandler(this.OptionsWindowLSD_CheckedChanged);
            // 
            // optionsWindowWamLabel
            // 
            this.optionsWindowWamLabel.AutoSize = true;
            this.optionsWindowWamLabel.Location = new System.Drawing.Point(14, 33);
            this.optionsWindowWamLabel.Name = "optionsWindowWamLabel";
            this.optionsWindowWamLabel.Size = new System.Drawing.Size(88, 13);
            this.optionsWindowWamLabel.TabIndex = 2;
            this.optionsWindowWamLabel.Text = "Dedodated Wam";
            // 
            // optionsWindowWam
            // 
            this.optionsWindowWam.Location = new System.Drawing.Point(108, 26);
            this.optionsWindowWam.Name = "optionsWindowWam";
            this.optionsWindowWam.Size = new System.Drawing.Size(380, 45);
            this.optionsWindowWam.TabIndex = 1;
            this.optionsWindowWam.Scroll += new System.EventHandler(this.OptionsWindowWam_Scroll);
            // 
            // optionsWindowHeader
            // 
            this.optionsWindowHeader.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.optionsWindowHeader.Controls.Add(this.optionsWindowHeaderExit);
            this.optionsWindowHeader.Controls.Add(this.optionsWindowIcon);
            this.optionsWindowHeader.Controls.Add(this.optionsWindowTitle);
            this.optionsWindowHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.optionsWindowHeader.Location = new System.Drawing.Point(0, 0);
            this.optionsWindowHeader.Name = "optionsWindowHeader";
            this.optionsWindowHeader.Size = new System.Drawing.Size(502, 20);
            this.optionsWindowHeader.TabIndex = 0;
            this.optionsWindowHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OptionsWindowHeader_MouseDown);
            this.optionsWindowHeader.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OptionsWindowHeader_MouseMove);
            this.optionsWindowHeader.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OptionsWindowHeader_MouseUp);
            // 
            // optionsWindowHeaderExit
            // 
            this.optionsWindowHeaderExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.optionsWindowHeaderExit.BackColor = System.Drawing.Color.Red;
            this.optionsWindowHeaderExit.Font = new System.Drawing.Font("Marlett", 12F);
            this.optionsWindowHeaderExit.Location = new System.Drawing.Point(462, 0);
            this.optionsWindowHeaderExit.Name = "optionsWindowHeaderExit";
            this.optionsWindowHeaderExit.Size = new System.Drawing.Size(40, 20);
            this.optionsWindowHeaderExit.TabIndex = 3;
            this.optionsWindowHeaderExit.Text = "r";
            this.optionsWindowHeaderExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.optionsWindowHeaderExit.Click += new System.EventHandler(this.OptionsWindowExit_Click);
            // 
            // optionsWindowIcon
            // 
            this.optionsWindowIcon.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("optionsWindowIcon.BackgroundImage")));
            this.optionsWindowIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.optionsWindowIcon.Location = new System.Drawing.Point(0, 0);
            this.optionsWindowIcon.Name = "optionsWindowIcon";
            this.optionsWindowIcon.Size = new System.Drawing.Size(20, 20);
            this.optionsWindowIcon.TabIndex = 1;
            // 
            // optionsWindowTitle
            // 
            this.optionsWindowTitle.AutoSize = true;
            this.optionsWindowTitle.Location = new System.Drawing.Point(19, 4);
            this.optionsWindowTitle.Name = "optionsWindowTitle";
            this.optionsWindowTitle.Size = new System.Drawing.Size(80, 13);
            this.optionsWindowTitle.TabIndex = 0;
            this.optionsWindowTitle.Text = "PCOptimizerPro";
            // 
            // lsdEffectT
            // 
            this.lsdEffectT.Enabled = true;
            this.lsdEffectT.Tick += new System.EventHandler(this.LsdTimer_Tick);
            // 
            // FakeDesktop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Blue;
            this.ClientSize = new System.Drawing.Size(1387, 919);
            this.ControlBox = false;
            this.Controls.Add(this.optionsWindow);
            this.Controls.Add(this.minigamePanel);
            this.Controls.Add(this.levelWindow);
            this.Controls.Add(this.winTimeLabel);
            this.Controls.Add(this.winMenuPanel);
            this.Controls.Add(this.winTaskbar);
            this.Controls.Add(this.winKey);
            this.Controls.Add(this.winDesktop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FakeDesktop";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FakeDesktop";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FakeDesktop_FormClosing);
            this.winMenuPanel.ResumeLayout(false);
            this.winTaskbar.ResumeLayout(false);
            this.winTaskbar.PerformLayout();
            this.winDesktop.ResumeLayout(false);
            this.options_1.ResumeLayout(false);
            this.levelWindow.ResumeLayout(false);
            this.levelWindowContents.ResumeLayout(false);
            this.levelWindow1.ResumeLayout(false);
            this.levelWindow2.ResumeLayout(false);
            this.levelWindow2.PerformLayout();
            this.levelWindow3.ResumeLayout(false);
            this.levelWindow3.PerformLayout();
            this.levelWindowHeader.ResumeLayout(false);
            this.levelWindowHeader.PerformLayout();
            this.optionsWindow.ResumeLayout(false);
            this.optionsWindow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.optionsWindowWam)).EndInit();
            this.optionsWindowHeader.ResumeLayout(false);
            this.optionsWindowHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button winKey;
        private System.Windows.Forms.Panel winMenuPanel;
        private System.Windows.Forms.Button winMenuExit;
        private System.Windows.Forms.Label winMenuText;
        private System.Windows.Forms.Label winMenuTitle;
        private System.Windows.Forms.Button winMenuStart;
        private System.Windows.Forms.Panel winTaskbar;
        private System.Windows.Forms.FlowLayoutPanel winDesktop;
        private System.Windows.Forms.Panel levelWindow;
        private System.Windows.Forms.Panel levelWindowHeader;
        private System.Windows.Forms.Label levelWindowTitle;
        private System.Windows.Forms.Panel levelWindowIcon;
        private System.Windows.Forms.Label levelWindowText1;
        private System.Windows.Forms.TabControl levelWindowContents;
        private System.Windows.Forms.TabPage levelWindow1;
        private System.Windows.Forms.TabPage levelWindow2;
        private System.Windows.Forms.Button levelWindowC1;
        private System.Windows.Forms.Label levelWindowText2;
        private System.Windows.Forms.TextBox captchaBox;
        private System.Windows.Forms.Panel captchaPanel;
        private System.Windows.Forms.TabPage levelWindow3;
        private System.Windows.Forms.Label levelWindowText3;
        private System.Windows.Forms.ProgressBar levelWindowProgress;
        private System.Windows.Forms.Timer levelWindowProgressT;
        private System.Windows.Forms.Label winTimeLabel;
        private System.Windows.Forms.Timer winTimeTimer;
        private System.Windows.Forms.Panel minigamePanel;
        private System.Windows.Forms.Timer minigameClockT;
        private System.Windows.Forms.Panel options_1;
        private System.Windows.Forms.Panel options_2;
        private System.Windows.Forms.Panel optionsWindow;
        private System.Windows.Forms.Panel optionsWindowHeader;
        private System.Windows.Forms.Panel optionsWindowIcon;
        private System.Windows.Forms.Label optionsWindowTitle;
        private System.Windows.Forms.CheckBox optionsWindowLSD;
        private System.Windows.Forms.Label optionsWindowWamLabel;
        private System.Windows.Forms.TrackBar optionsWindowWam;
        private System.Windows.Forms.Button optionsWindowExit;
        private System.Windows.Forms.Timer lsdEffectT;
        private System.Windows.Forms.CheckBox optionsWindowSubs;
        private System.Windows.Forms.Label subsLabel;
        private System.Windows.Forms.ComboBox optionsWindowLang;
        private System.Windows.Forms.Label levelWindowHeaderExit;
        private System.Windows.Forms.Label optionsWindowHeaderExit;
        private System.Windows.Forms.Button optionsWindowReset;
    }
}