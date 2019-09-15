using LaptopSimulator2015.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Media;
using System.Reflection;
using System.Diagnostics;
using System.IO;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using Base;

namespace LaptopSimulator2015
{
    public partial class FakeDesktop : Form
    {
        #region Base
        List<Level> levels = new List<Level>();
        SoundPlayer fans;
        bool winShouldClose = false;

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
                optionsWindowLSD.Text = strings.optionsWindowLSD;
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
                for (int i = 0; i < levels.Count; i++)
                    levels[i].desktopIcon.Visible = levels[i].LevelNumber <= Settings.level;
            }
        }

        [DllImport("winmm.dll")]
        public static extern int waveOutGetVolume(IntPtr hwo, out uint dwVolume);

        [DllImport("winmm.dll")]
        public static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);

        public FakeDesktop()
        {
            Directory.SetCurrentDirectory(Path.GetDirectoryName(Application.ExecutablePath));
            if (!Directory.Exists("Levels"))
                Directory.CreateDirectory("Levels");
            InitializeComponent();
            toolTip.SetToolTip(options_2, strings.optionsWindowTitle);
            levelWindowContents.ItemSize = new Size(0, 1);
            optionsWindowLang.Text = Settings.lang.Name;
            Thread.CurrentThread.CurrentUICulture = Settings.lang;
            optionsWindowWam.Value = Settings.wam;
            int NewVolume = ((ushort.MaxValue / 10) * Settings.wam);
            uint NewVolumeAllChannels = ((uint)NewVolume & 0x0000ffff) | ((uint)NewVolume << 16);
            waveOutSetVolume(IntPtr.Zero, NewVolumeAllChannels);
            tmpoptionslsdcanchange = Settings.lsd;
            optionsWindowLSD.Checked = Settings.lsd;
            optionsWindowSubs.Checked = Settings.subs;
            Text = strings.fakeDesktopTitle;
            winMenuExit.Text = strings.winMenuExit1;
            winMenuStart.Text = strings.winMenuStart;
            winMenuText.Text = strings.winMenuText;
            levelWindowTitle.Text = "";
            winTimeLabel.Text = DateTime.Now.ToString("hh:mm:ss", Settings.lang);
            fans = new SoundPlayer(Resources.fans);
            fans.PlayLooping();
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
                levels[i].desktopIcon = new Panel();
                Panel tmp1 = new Panel();

                levels[i].desktopIcon.Size = new Size(50, 50);
                levels[i].desktopIcon.BackColor = Color.FromArgb(128, 128, 255);
                levels[i].desktopIcon.Name = "lvl" + i.ToString() + "_1";
                levels[i].desktopIcon.Visible = levels[i].LevelNumber <= Settings.level;

                tmp1.BackColor = Color.Blue;
                tmp1.BackgroundImageLayout = ImageLayout.Stretch;
                tmp1.BackgroundImage = levels[i].installerIcon;
                tmp1.Anchor = (AnchorStyles)15;
                tmp1.Name = "lvl" + i.ToString() + "_2";
                tmp1.Location = new Point(2, 2);
                tmp1.Size = new Size(46, 46);
                tmp1.Tag = i;
                tmp1.DoubleClick += (sender, e) => { level_Start((int)((Panel)sender).Tag); };
                toolTip.SetToolTip(tmp1, strings.lvPref + " " + (i + 1).ToString() + ": " + levels[i].installerHeader);

                levels[i].desktopIcon.Controls.Add(tmp1);
                winDesktop.Controls.Add(levels[i].desktopIcon);
            }
            levels = levels.OrderBy(lv => lv.LevelNumber).ToList();
            mode = Mode.mainMenu;
            Program.splash.Close();
            Misc.closeGameWindow = new Action(closeLevelWindow);
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

        private void WinTimeTimer_Tick(object sender, EventArgs e) => winTimeLabel.Text = DateTime.Now.ToString("hh:mm:ss", Settings.lang);
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
#if !DEBUG
                        levelWindowC1.Enabled = false;
#endif
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
                    if (levels[levelInd].LevelNumber >= Settings.level)
                    {
                        int closest = int.MaxValue;
                        for (int i = 0; i < levels.Count; i++)
                            if (levels[i].LevelNumber < closest & levels[i].LevelNumber > levels[levelInd].LevelNumber)
                                closest = levels[i].LevelNumber;
                        if (closest != int.MaxValue)
                            Settings.level = closest;
                        Settings.Save();
                        for (int i = 0; i < levels.Count; i++)
                            levels[i].desktopIcon.Visible = levels[i].LevelNumber <= Settings.level;
                        mode = Mode.game;
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
            Random rnd = new Random();
            string Chars = strings.captchaLetters;
            captchaBox.Tag = "";
            captchaPanel.BackgroundImage = new Bitmap(175, 60, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(captchaPanel.BackgroundImage))
            {
                g.FillRectangle(new LinearGradientBrush(new Point(0, 0), new Point(175, 60), Color.FromArgb(rnd.Next(180, 255), rnd.Next(180, 255), rnd.Next(180, 255)), Color.FromArgb(rnd.Next(180, 255), rnd.Next(180, 255), rnd.Next(180, 255))), new Rectangle(0, 0, 175, 60));
                g.FillRectangle(new HatchBrush((HatchStyle)rnd.Next(53), Color.FromArgb(rnd.Next(180, 255), rnd.Next(180, 255), rnd.Next(180, 255)), Color.Transparent), new Rectangle(0, 0, 175, 60));
                for (int i = 0; i < 6; i++)
                {
                    int y = rnd.Next(8, 13);
                    int fontSize = rnd.Next(12, 18);
                    g.TranslateTransform(25 * (i) + 10, (30 - fontSize) / 2);
                    g.RotateTransform(rnd.Next(-20, 20));
                    int tmpR = rnd.Next(0, 200);
                    int tmpG = rnd.Next(0, (200 - tmpR) / 2);
                    string s = Chars[rnd.Next(Chars.Length)].ToString();
                    captchaBox.Tag = (string)captchaBox.Tag + s;
                    g.DrawString(s, new Font(new string[] { "Arial", "Consolas", "Verdena" }[rnd.Next(3)], fontSize), new SolidBrush(Color.FromArgb(tmpR, tmpG, Math.Max(0, 200 - tmpR - tmpG))), new PointF(5, y));
                    g.ResetTransform();
                }
                g.FillRectangle(new HatchBrush((HatchStyle)rnd.Next(53), Color.FromArgb(rnd.Next(10, 50), rnd.Next(180, 255), rnd.Next(180, 255), rnd.Next(180, 255)), Color.FromArgb(rnd.Next(10, 50), rnd.Next(180, 255), rnd.Next(180, 255), rnd.Next(180, 255))), new Rectangle(0, 0, 175, 60));
            }
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

        private void LevelWindowHeaderExit_Click(object sender, EventArgs e) => closeLevelWindow();

        private void closeLevelWindow()
        {
            BackColor = Color.FromArgb(100, 0, 255);
            levelWindow.Visible = false;
            minigamePanel.Visible = false;
            minigamePanel.Enabled = false;
            minigameClockT.Enabled = false;
            winDesktop.Enabled = true;
            levelWindowContents.SelectedIndex = 0;
            levelWindowProgress.Value = 0;
            levelWindowProgressT.Enabled = false;
            levelWindowC1.Enabled = true;
            Thread.Sleep(100);
            BackColor = Color.Blue;
        }

#region Minigame
        uint minigameTime = 0;
        private void InvadersPanel_Paint(object sender, PaintEventArgs e) => levels[levelInd].gameTick(e.Graphics, minigamePanel, minigameClockT, minigameTime);

        private void InvadersTimer_Tick(object sender, EventArgs e)
        {
            minigameTime++;
            minigamePanel.Invalidate();
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

        private void OptionsWindowWam_Scroll(object sender, EventArgs e)
        {
            int NewVolume = ((ushort.MaxValue / 10) * optionsWindowWam.Value);
            uint NewVolumeAllChannels = ((uint)NewVolume & 0x0000ffff) | ((uint)NewVolume << 16);
            waveOutSetVolume(IntPtr.Zero, NewVolumeAllChannels);
        }

        private void OptionsWindowExit_Click(object sender, EventArgs e)
        {
            Settings.wam = optionsWindowWam.Value;
            Settings.lsd = optionsWindowLSD.Checked;
            Settings.subs = optionsWindowSubs.Checked;
            int NewVolume = ((ushort.MaxValue / 10) * Settings.wam);
            uint NewVolumeAllChannels = ((uint)NewVolume & 0x0000ffff) | ((uint)NewVolume << 16);
            waveOutSetVolume(IntPtr.Zero, NewVolumeAllChannels);
            bool tmp = false;
            if (Settings.lang.Name != optionsWindowLang.Text)
            {
                Settings.lang = System.Globalization.CultureInfo.GetCultureInfo(optionsWindowLang.Text);
                tmp = true;
            }
            winDesktop.Enabled = true;
            optionsWindow.Visible = false;
            subsLabel.Visible = optionsWindowSubs.Checked;
            Settings.Save();
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
            mode = Mode.game;
        }

        bool tmpoptionslsdcanchange = false;
        private void OptionsWindowLSD_CheckedChanged(object sender, EventArgs e)
        {
            if (optionsWindowLSD.Checked)
            {
                if (tmpoptionslsdcanchange)
                {
                    optionsWindowLSD.Checked = true;
                    tmpoptionslsdcanchange = false;
                }
                else
                {
                    optionsWindowLSD.Checked = false;
                    try
                    {
                        if (MessageBox.Show(strings.optionsWindowLSDWarning, "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            tmpoptionslsdcanchange = true;
                            optionsWindowLSD.Checked = true;
                        }
                    }
                    catch (Exception e1)
                    {
                        Console.WriteLine("LSDThreadException:\r\n" + e1.ToString());
                    }
                }
            }
        }

        private void LsdTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                Random rndg = new Random();
                rndCol = Color.FromArgb(rndg.Next(255), rndg.Next(255), rndg.Next(255));
                optionsWindowLSD.ForeColor = rndCol;
                if (Settings.lsd)
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
                Console.WriteLine("LSDTimer Failed: \r\n" + e1.ToString());
            }
        }

        Color rndCol = Color.White;

        private void Control_Paint(object sender, PaintEventArgs e)
        {
            if (Settings.lsd)
            {
                rndCol = Color.FromArgb(128, rndCol.R, rndCol.G, rndCol.B);
                e.Graphics.FillRectangle(new SolidBrush(rndCol), new RectangleF(Point.Empty, ((Control)sender).Size));
            }
        }
        private void OptionsWindowReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(strings.resetWarning1, "", MessageBoxButtons.YesNo) == DialogResult.Yes && MessageBox.Show(strings.resetWarning2, "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                File.Delete(Settings._xmlfile);
                winShouldClose = true;
                string ex = "";
                if (Application.ExecutablePath.Contains(" "))
                    ex = "\"\" \"" + Application.ExecutablePath + "\"";
                else
                    ex = Application.ExecutablePath;
                Process.Start(new ProcessStartInfo
                {
                    Arguments = "/C timeout /t 2 /nobreak >nul & del \"" + Settings._xmlfile + "\" & start " + ex,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    FileName = "cmd.exe"
                });
                Application.Exit();
            }
        }
        #endregion
    }
}