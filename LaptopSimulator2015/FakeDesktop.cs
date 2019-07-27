using LaptopSimulator2015.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Media;
using System.Runtime.ExceptionServices;
using System.Reflection;
using System.Diagnostics;
using System.IO;

namespace LaptopSimulator2015
{
    public partial class FakeDesktop : Form
    {
        #region Base
        List<Level> levels = new List<Level>();
        bool winShouldClose = false;
        class TabControlH : TabControl
        {
            protected override void WndProc(ref Message m)
            {
                if (m.Msg == 0x1328 & !DesignMode)
                    m.Result = (IntPtr)1;
                else
                    base.WndProc(ref m);
            }
        }

        enum Mode
        {
            mainMenu,
            game
        }
        Mode _mode;
        Mode mode
        {
            get { return _mode; }
            set {
                _mode = value;
                winMenuStart.Enabled = _mode == Mode.mainMenu;
                winMenuStart.Visible = _mode == Mode.mainMenu;
                winMenuExit.Text = strings.winMenuExit1;
                winMenuPanel.Visible = false | _mode == Mode.mainMenu;
                winDesktop.Enabled = _mode == Mode.game;
                levelWindowText1.Text = "";
                levelWindowText2.Text = strings.ramInstallerWindowText2;
                levelWindowText3.Text = strings.ramInstallerWindowText3;
                levelWindowC1.Text = strings._continue;
                optionsWindowMLG.Text = strings.optionsWindowMLG;
                optionsWindowTitle.Text = strings.optionsWindowTitle;
                optionsWindowWamLabel.Text = strings.optionsWindowWam;
                levelWindow.Visible = false;
                minigamePanel.Visible = false;
                optionsWindow.Visible = false;
                minigamePanel.Enabled = false;
                minigameClockT.Enabled = false;
                levelWindowC1.Enabled = true;
                levelWindowProgressT.Enabled = false;
                levelWindowProgress.Value = 0;
                levelWindowContents.SelectedIndex = 0;
                subsLabel.Visible = optionsWindowSubs.Checked;
                if (_mode == Mode.mainMenu)
                    winMenuStart.Select();
            }
        }

        // Structure contain information about low-level keyboard input event
        [StructLayout(LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT
        {
            public Keys key;
            public int scanCode;
            public int flags;
            public int time;
            public IntPtr extra;
        }

        //System level functions to be used for hook and unhook keyboard input
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int id, LowLevelKeyboardProc callback, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hook);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hook, int nCode, IntPtr wp, IntPtr lp);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string name);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern short GetAsyncKeyState(Keys key);


        //Declaring Global objects
        private IntPtr ptrHook;

        private LowLevelKeyboardProc objKeyboardProcess;

        private IntPtr captureKey(int nCode, IntPtr wp, IntPtr lp)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT objKeyInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lp, typeof(KBDLLHOOKSTRUCT));
                if (objKeyInfo.key == Keys.RWin | objKeyInfo.key == Keys.LWin)
                {
                    if (wp == (IntPtr)0x0101)
                        WinKey_Click(null, null);
                    return (IntPtr)1;
                }
            }
            return CallNextHookEx(ptrHook, nCode, wp, lp);
        }

        public FakeDesktop()
        {
            Directory.SetCurrentDirectory(Path.GetDirectoryName(Application.ExecutablePath));
            if (!Directory.Exists("Levels"))
                Directory.CreateDirectory("Levels");
            objKeyboardProcess = new LowLevelKeyboardProc(captureKey);
            ptrHook = SetWindowsHookEx(13, objKeyboardProcess, GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName), 0);
            InitializeComponent();
            optionsWindowLang.Text = Settings.Default.lang.Name;
            Thread.CurrentThread.CurrentUICulture = Settings.Default.lang;
            optionsWindowWam.Value = Settings.Default.wam;
            tmpoptionsmlgcanchange = Settings.Default.mlg;
            optionsWindowMLG.Checked = Settings.Default.mlg;
            optionsWindowSubs.Checked = Settings.Default.subs;
            Text = strings.fakeDesktopTitle;
            winMenuExit.Text = strings.winMenuExit1;
            winMenuStart.Text = strings.winMenuStart;
            winMenuText.Text = strings.winMenuText;
            levelWindowTitle.Text = "";
            winTimeLabel.Text = DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00");
            new SoundPlayer(Resources.fans).PlayLooping();
            Control[] controls = getControls(ignore: new List<Control> { minigamePanel }).ToArray();
            for (int i = 0; i < controls.Length; i++)
            {
                controls[i].Paint += Control_Paint;
            }
            levels = new List<Level>();
            AppDomain ad = AppDomain.CurrentDomain;
            ad.AssemblyResolve += AssemblyResolveHandler;
            foreach (string s in Directory.GetFiles("Levels"))
                if (Path.GetExtension(s) == ".dll")
                    ad.Load(s);
            List<Type> tmp = ad.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => typeof(Level).IsAssignableFrom(p)).ToList();
            tmp.Remove(typeof(Level));
            for (int i = 0; i < tmp.Count; i++)
            {
                levels.Add((Level)Activator.CreateInstance(tmp[i]));
                Panel tmp1 = new Panel();
                Panel tmp2 = new Panel();

                tmp1.Size = new Size(50, 50);
                tmp1.BackColor = Color.FromArgb(128, 128, 255);
                tmp1.Name = "lvl" + i.ToString() + "_1";
                tmp1.Visible = i >= Settings.Default.level;

                tmp2.BackColor = Color.Blue;
                tmp2.BackgroundImageLayout = ImageLayout.Stretch;
                tmp2.BackgroundImage = levels[i].installerIcon;
                tmp2.Anchor = (AnchorStyles)15;
                tmp2.Name = "lvl" + i.ToString() + "_2";
                tmp2.Location = new Point(2, 2);
                tmp2.Size = new Size(46, 46);
                tmp2.Tag = i;
                tmp2.DoubleClick += (sender, e) => { level_Start((int)((Panel)sender).Tag); };

                tmp1.Controls.Add(tmp2);
                winDesktop.Controls.Add(tmp1);
                levels[i].desktopIcon = tmp1;
            }
            levels = levels.OrderBy(lv => lv.LevelNumber).ToList();
            mode = Mode.mainMenu;
        }
        static Assembly AssemblyResolveHandler(object source, ResolveEventArgs e) => Assembly.LoadFrom(e.Name);

        public List<Control> getControls(Control parent = null, List<Control> ignore = null)
        {
            if (parent == null) parent = this;
            if (ignore == null) ignore = new List<Control>();
            List<Control> controls = new List<Control> { parent };
            int i = 0;
            while (i < controls.Count)
            {
                if (controls[i].Controls.Count > 0)
                {
                    Control[] tmp = new Control[controls[i].Controls.Count];
                    controls[i].Controls.CopyTo(tmp, 0);
                    controls.AddRange(tmp);
                }
                i++;
                controls = controls.Except(ignore).ToList();
            }
            return controls;
        }

        private void FakeDesktop_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !winShouldClose;
            if (winDesktop.Enabled | mode == Mode.mainMenu)
            {
                if (winMenuPanel.Visible)
                    WinMenuExit_Click(sender, new EventArgs());
                else
                    WinKey_Click(sender, new EventArgs());
            }
            else
            {
                if (optionsWindow.Visible)
                    OptionsWindowExit_Click(sender, new EventArgs());
                else
                {
                    if (levelWindow.Visible)
                        LevelWindowHeaderExit_Click(sender, new EventArgs());
                }
            }
        }
        #endregion
        #region FakeDesktopFunctionality

        private void WinKey_Click(object sender, EventArgs e)
        {
            winMenuPanel.Visible = mode == Mode.mainMenu | !winMenuPanel.Visible;
            winMenuExit.Text = strings.winMenuExit1;
        }

        private void WinMenuExit_Click(object sender, EventArgs e)
        {
            if (winMenuExit.Text == strings.winMenuExit2)
            {
                if (mode == Mode.mainMenu)
                {
                    winShouldClose = true;
                    Application.Exit();
                }
                else
                    mode = Mode.mainMenu;
            }
            else
                winMenuExit.Text = strings.winMenuExit2;
        }

        private void WinMenuStart_Click(object sender, EventArgs e) => mode = Mode.game;

        private void WinTimeTimer_Tick(object sender, EventArgs e) => winTimeLabel.Text = DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00");
        #endregion
        #region Level
        int levelInd = 0;
        private void level_Start(int level)
        {
            levelInd = level;
            levelWindowIcon.BackgroundImage = levels[level].installerIcon;
            levelWindowTitle.Text = levels[level].installerHeader;
            levelWindowText1.Text = levels[level].installerText;
            minigameClockT.Interval = levels[level].gameClock;
            levelWindowProgress.Maximum = levels[level].installerProgressSteps;
            winDesktop.Enabled = false;
            levelWindow.Visible = true;
        }

        bool levelWindowMoving = false;
        Point levelWindowDiff = Point.Empty;
        private void LevelWindowHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (levelWindowMoving)
                levelWindow.Location = new Point(Cursor.Position.X + levelWindowDiff.X, Cursor.Position.Y + levelWindowDiff.Y);
        }

        private void LevelWindowHeader_MouseDown(object sender, MouseEventArgs e)
        {
            levelWindowMoving = true;
            levelWindowDiff = new Point(levelWindow.Location.X - Cursor.Position.X, levelWindow.Location.Y - Cursor.Position.Y);
        }

        private void LevelWindowHeader_MouseUp(object sender, MouseEventArgs e) => levelWindowMoving = false;

        private void LevelWindowC1_Click(object sender, EventArgs e)
        {
            switch (levelWindowContents.SelectedIndex)
            {
                case 0:
                    CaptchaPanel_Click(sender, e);
                    levelWindowContents.SelectedIndex = 1;
                    break;
                case 1:
                    if (captchaBox.Text == (string)captchaBox.Tag)
                    {
                        levelWindowContents.SelectedIndex = 2;
                        levelWindowProgressT.Enabled = true;
                        minigameTime = 0;
                        Graphics g = minigamePanel.CreateGraphics();
                        levels[levelInd].initGame(g, minigamePanel, minigameClockT);
                        minigamePanel.Visible = true;
                        minigamePanel.Enabled = true;
                        minigameClockT.Enabled = true;
                        levelWindowC1.Enabled = false;
                        g.Clear(Color.Red);
                        g.DrawString("DANGER!", new Font("Microsoft Sans Serif", 100f), new SolidBrush(Color.White), 100, 150);
                        g.DrawString("VIRUS DETECTED", new Font("Microsoft Sans Serif", 20f), new SolidBrush(Color.White), 0, 300);
                        Thread.Sleep(1000);
                        g.Flush();
                    }
                    else
                    {
                        captchaBox.Text = "";
                        captchaBox.Select();
                    }
                    break;
                case 2:
                    LevelWindowHeaderExit_Click(sender, e);
                    if (levelInd == Settings.Default.level)
                    {
                        Settings.Default.level = Math.Min(levels.Count - 1, levelInd);
                        Settings.Default.Save();
                        for (int i = 0; i < levels.Count; i++)
                        {
                            levels[i].desktopIcon.Visible = i >= Settings.Default.level;
                        }
                    }
                    break;
            }
        }

        private void CaptchaBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)8:
                    if (captchaBox.Text.Length > 0)
                        captchaBox.Text = captchaBox.Text.Remove(captchaBox.Text.Length - 1);
                    break;
                case (char)13:
                    levelWindowC1.Select();
                    LevelWindowC1_Click(sender, new EventArgs());
                    break;
                default:
                    if (strings.captchaLetters.ToCharArray().Contains(char.Parse(e.KeyChar.ToString().ToUpper())))
                        captchaBox.Text += e.KeyChar.ToString().ToUpper();
                    break;
            }
        }

        private void CaptchaPanel_Click(object sender, EventArgs e)
        {
            CaptchaGenerator cap = new CaptchaGenerator(6, strings.captchaLetters);
            cap.Generate();
            captchaPanel.BackgroundImage = cap.Captcha;
            captchaBox.Tag = cap.Text;
            captchaBox.Text = "";
        }

        private void LevelWindowProgressT_Tick(object sender, EventArgs e)
        {
            levelWindowProgress.Value = Math.Min(levelWindowProgress.Maximum, levelWindowProgress.Value + 1);
            if (levelWindowProgress.Value == levelWindowProgress.Maximum)
            {
                levelWindowProgressT.Enabled = false;
                levelWindowC1.Enabled = true;
            }
        }

        private void LevelWindowHeaderExit_Click(object sender, EventArgs e)
        {
            levelWindow.Visible = false;
            minigamePanel.Visible = false;
            minigamePanel.Enabled = false;
            minigameClockT.Enabled = false;
            winDesktop.Enabled = true;
            levelWindowContents.SelectedIndex = 0;
            levelWindowProgress.Value = 0;
            levelWindowProgressT.Enabled = false;
            levelWindowC1.Enabled = true;
        }

        #region Minigame
        uint minigameTime = 0;
        private void InvadersPanel_Paint(object sender, PaintEventArgs e) => levels[levelInd].gameTick(e.Graphics, minigamePanel, minigameClockT, minigameTime);

        private void InvadersTimer_Tick(object sender, EventArgs e)
        {
            minigameTime++;
            minigamePanel.Invalidate();
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern short GetKeyState(int keyCode);

        public static bool IsKeyDown(Keys key)
        {
            try
            {
                int state = 0;
                short retVal = GetKeyState((int)key);
                if ((retVal & 0x8000) == 0x8000)
                    state |= 1;
                if ((retVal & 1) == 1)
                    state |= 2;
                return 1 == (state & 1);
            }
            catch (Exception e1)
            {
                Console.WriteLine("Invader: IsKeyDown failed:\r\n" + e1.ToString());
                return false;
            }
        }
        #endregion

        #endregion
        #region Options
        private void Options_2_DoubleClick(object sender, EventArgs e)
        {
            winDesktop.Enabled = false;
            optionsWindow.Visible = true;
        }

        bool optionsWindowMoving = false;
        Point optionsWindowDiff = Point.Empty;

        private void OptionsWindowHeader_MouseDown(object sender, MouseEventArgs e)
        {
            optionsWindowMoving = true;
            optionsWindowDiff = new Point(optionsWindow.Location.X - Cursor.Position.X, optionsWindow.Location.Y - Cursor.Position.Y);
        }

        private void OptionsWindowHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (optionsWindowMoving)
                optionsWindow.Location = new Point(Cursor.Position.X + optionsWindowDiff.X, Cursor.Position.Y + optionsWindowDiff.Y);
        }

        private void OptionsWindowHeader_MouseUp(object sender, MouseEventArgs e) => optionsWindowMoving = false;

        private void OptionsWindowExit_Click(object sender, EventArgs e)
        {
            Settings.Default.wam = optionsWindowWam.Value;
            Settings.Default.mlg = optionsWindowMLG.Checked;
            Settings.Default.subs = optionsWindowSubs.Checked;
            bool tmp = false;
            if (Settings.Default.lang.Name != optionsWindowLang.Text)
            {
                Settings.Default.lang = System.Globalization.CultureInfo.GetCultureInfo(optionsWindowLang.Text);
                tmp = true;
            }
            winDesktop.Enabled = true;
            optionsWindow.Visible = false;
            subsLabel.Visible = optionsWindowSubs.Checked;
            Settings.Default.Save();
            if (tmp && MessageBox.Show(strings.langWarning, "LaptopSimulator2015", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                winShouldClose = true;
                string ex = "";
                if (Application.ExecutablePath.Contains(" "))
                    ex = "\"\" \"" + Application.ExecutablePath + "\"";
                else
                    ex = Application.ExecutablePath;
                Process.Start(new ProcessStartInfo
                {
                    Arguments = "/C timeout /t 2 /nobreak >nul && start " + ex,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    FileName = "cmd.exe"
                });
                Application.Exit();
            }
        }

        bool tmpoptionsmlgcanchange = false;
        private void OptionsWindowMLG_CheckedChanged(object sender, EventArgs e)
        {
            if (optionsWindowMLG.Checked)
            {
                if (tmpoptionsmlgcanchange)
                {
                    optionsWindowMLG.Checked = true;
                    tmpoptionsmlgcanchange = false;
                }
                else
                {
                    optionsWindowMLG.Checked = false;
                    try
                    {
                        if (MessageBox.Show("Are you SURE?\r\n(This will break EVERYTHING!)", "WARNING", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            tmpoptionsmlgcanchange = true;
                            optionsWindowMLG.Checked = true;
                        }
                    }
                    catch (Exception e1)
                    {
                        Console.WriteLine("MLGThreadException:\r\n" + e1.ToString());
                    }
                }
            }
        }

        private void MlgTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                Random rndg = new Random();
                rndCol = Color.FromArgb(rndg.Next(255), rndg.Next(255), rndg.Next(255));
                optionsWindowMLG.ForeColor = rndCol;
                if (Settings.Default.mlg)
                {
                    ForeColor = rndCol;
                    Font = new Font("Comic Sans MS", 7f);
                }
                else
                {
                    ForeColor = Color.FromKnownColor(KnownColor.ControlText);
                    Font = new Font("Microsoft Sans Serif", 8.25f);
                }
            }
            catch (Exception e1)
            {
                Console.WriteLine("MLGTimer Failed: \r\n" + e1.ToString());
            }
        }

        Color rndCol = Color.White;

        private void Control_Paint(object sender, PaintEventArgs e)
        {
            if (Settings.Default.mlg)
            {
                rndCol = Color.FromArgb(128, rndCol.R, rndCol.G, rndCol.B);
                e.Graphics.FillRectangle(new SolidBrush(rndCol), new RectangleF(Point.Empty, ((Control)sender).Size));
            }
        }
        #endregion
    }
}