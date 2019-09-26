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
        List<Minigame> levels = new List<Minigame>();
        List<CaptchaGlitch> captchaGlitches = new List<CaptchaGlitch>();
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
                optionsWindowReset.Text = strings.optionsWindowReset;
                optionsWindowSubs.Text = strings.optionsWindowSubs;
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
                if (tmp__mode_uiv)
                    updateIconVisibility();
            }
        }
        bool tmp__mode_uiv = false;

        [DllImport("winmm.dll")]
        public static extern int waveOutGetVolume(IntPtr hwo, out uint dwVolume);

        [DllImport("winmm.dll")]
        public static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);

        public FakeDesktop()
        {
            Directory.SetCurrentDirectory(Path.GetDirectoryName(Application.ExecutablePath));
            if (!Directory.Exists("Content"))
                Directory.CreateDirectory("Content");
            InitializeComponent();
            toolTip.SetToolTip(options_2, strings.optionsWindowTitle);
#if DEBUG
            devWindowOpen.Visible = true;
            optionsWindowLang.Size = new Size(88, 21);
#endif
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
                controls[i].Paint += Control_Paint;
            IEnumerable<string> tmpdirs = Directory.EnumerateFiles("Content", "*.dll", SearchOption.AllDirectories);
            IEnumerable<Type> tmp_types = tmpdirs.Select(s => Assembly.LoadFrom(s)).SelectMany(s => s.GetTypes()).Distinct();
            levels = tmp_types.Where(p => typeof(Minigame).IsAssignableFrom(p)).Distinct().Except(new Type[] { typeof(Minigame), typeof(Level), typeof(Goal) })
                .Select(s => (Minigame)Activator.CreateInstance(s)).OrderBy(lv => lv.availableAfter).ToList();
#if DEBUG
            devWindowDllList.Items.AddRange(tmpdirs.ToArray());
            devWindowLevelList.Items.AddRange(levels.Select(s => s.availableAfter.ToString() + " - " + s.name + (typeof(Goal).IsAssignableFrom(s.GetType()) ? (" - " + ((Goal)s).playableAfter.ToString() + " (Goal)") : " (Level)")).ToArray());
#endif
            captchaGlitches = tmp_types.Where(p => typeof(CaptchaGlitch).IsAssignableFrom(p)).Distinct().Except(new Type[] { typeof(CaptchaGlitch) })
                .Select(s => (CaptchaGlitch)Activator.CreateInstance(s)).ToList();
            for (int i = 0; i < levels.Count; i++)
            {
                levels[i].desktopIcon = new Panel
                {
                    Size = new Size(50, 50),
                    BackColor = Color.FromArgb(128, 128, 255),
                    Name = "lvl" + i.ToString() + "_1",
                    Visible = levels[i].availableAfter <= Settings.level
                };
                Panel tmp1 = new Panel
                {
                    BackColor = Color.Blue,
                    BackgroundImageLayout = ImageLayout.Stretch,
                    BackgroundImage = levels[i].icon,
                    Anchor = (AnchorStyles)15,
                    Name = "lvl" + i.ToString() + "_2",
                    Location = new Point(2, 2),
                    Size = new Size(46, 46),
                    Tag = i
                };
                tmp1.DoubleClick += (sender, e) =>
                {
                    levelInd = (int)((Panel)sender).Tag;
                    levelWindowIcon.BackgroundImage = levels[levelInd].icon;
                    levelWindowTitle.Text = levels[levelInd].name;
                    minigameClockT.Interval = levels[levelInd].gameClock;
                    winDesktop.Enabled = false;
                    if (typeof(Level).IsAssignableFrom(levels[levelInd].GetType()))
                    {
                        Misc.closeGameWindow = new Action(() => {
                            base.BackColor = Color.FromArgb(100, 0, 255);
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
                            base.BackColor = Color.Blue;
                        });
                        levelWindowProgress.Maximum = ((Level)levels[levelInd]).installerProgressSteps;
                        levelWindowText1.Text = ((Level)levels[levelInd]).installerText;
                        levelWindow.Visible = true;
                    }
                    else
                    {
                        Goal goal = ((Goal)levels[levelInd]);
                        if (goal.playableAfter <= Settings.level)
                        {
                            levels[levelInd].initGame(minigamePanel, minigameClockT);
                            minigamePanel.Visible = true;
                            minigamePanel.Enabled = true;
                            minigameClockT.Enabled = true;
                            minigameClose.Visible = true;
                            Misc.closeGameWindow = new Action(() => {
                                base.BackColor = Color.FromArgb(100, 0, 255);
                                minigamePanel.Visible = false;
                                minigamePanel.Enabled = false;
                                minigameClockT.Enabled = false;
                                winDesktop.Enabled = true;
                                minigameClose.Visible = false;
                                Thread.Sleep(100);
                                base.BackColor = Color.Blue;
                            });
                            minigamePanel.Show();
                        }
                        else
                        {
                            base.BackColor = Color.FromArgb(100, 0, 255);
                            if (levels[levelInd].availableAfter == Settings.level)
                            {
                                playDialog(goal.incompleteText);
                                incrementLevel();
                            }
                            Thread.Sleep(100);
                            base.BackColor = Color.Blue;
                            winDesktop.Enabled = true;
                        }
                    }
                };
                toolTip.SetToolTip(tmp1, strings.lvPref + " " + (i + 1).ToString() + ": " + levels[i].name);
                levels[i].desktopIcon.Controls.Add(tmp1);
                winDesktop.Controls.Add(levels[i].desktopIcon);
            }
        }

        private void FakeDesktop_Load(object sender, EventArgs e)
        {
            mode = Mode.mainMenu;
            Program.splash.Close();
            GC.Collect();
            tmp__mode_uiv = true;
        }

        void updateIconVisibility(bool ignoreSub = false)
        {
            for (int i = 0; i < levels.Count; i++)
            {
                if ((!ignoreSub) && ((typeof(Goal).IsAssignableFrom(levels[i].GetType()) && levels[i].desktopIcon.Visible != levels[i].availableAfter <= Settings.level) || (levels[i].availableAfter == 0 && Settings.level == 0)))
                {
                    string[] at = ((Goal)levels[i]).availableText;
                    new Thread(() =>
                    {
                        Invoke((MethodInvoker)delegate () { winDesktop.Enabled = false; });
                        playDialog(at).Join();
                        Invoke((MethodInvoker)delegate ()
                        {
                            winDesktop.Enabled = true;
                            updateIconVisibility(true);
                        });
                    }).Start();
                }
                levels[i].desktopIcon.Visible = levels[i].availableAfter <= Settings.level;
            }
#if DEBUG
            devWindowLevelList.SelectedIndex = levels.FindIndex(s => s.availableAfter == Settings.level);
#endif
        }

        Thread playDialog(string[] lines)
        {
            var tmp = new Thread(() =>
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    Invoke((MethodInvoker)delegate () { subsLabel.Text = lines[i]; });
                    Thread.Sleep(2000);
                }
                Invoke((MethodInvoker)delegate () { subsLabel.Text = ""; });
            });
            tmp.Start();
            return tmp;
        }

        void incrementLevel()
        {
            int closest = int.MaxValue;
            for (int i = 0; i < levels.Count; i++)
                if (levels[i].availableAfter < closest & levels[i].availableAfter > levels[levelInd].availableAfter)
                    closest = levels[i].availableAfter;
            if (closest != int.MaxValue)
                Settings.level = closest;
            Settings.Save();
            updateIconVisibility();
            for (int i = 0; i < levels.Count; i++)
                if (typeof(Goal).IsAssignableFrom(levels[i].GetType()) && ((Goal)levels[i]).playableAfter == Settings.level)
                    playDialog(((Goal)levels[i]).completeText);
        }

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
                        levels[levelInd].initGame(minigamePanel, minigameClockT);
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
                    if (levels[levelInd].availableAfter >= Settings.level)
                    {
                        incrementLevel();
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
                    char tmp1 = char.ToUpper(e.KeyChar);
                    if (strings.captchaLetters.ToUpper().ToCharArray().Contains(tmp1))
                    {
                        string tmp2 = tmp1.ToString().ToUpper();
                        captchaGlitches.ForEach(s => s.currentLevel = Settings.level);
                        Random rnd = new Random();
                        CaptchaGlitch[] capGlN = captchaGlitches.Where(s => !s.postGlitch).ToArray();
                        List<CaptchaGlitch> capGlP = captchaGlitches.Where(s => s.postGlitch).ToList();
                        for (int i = 0; i < capGlN.Length;  i++)
                            if (rnd.NextDouble() < capGlN[i].chance)
                            {
                                capGlN[i].apply(tmp1, ref tmp2, strings.captchaLetters.ToUpper().ToCharArray(), rnd);
                                break;
                            }
                        capGlP.ForEach(s => s.apply(tmp1, ref tmp2, strings.captchaLetters.ToUpper().ToCharArray(), rnd));
                        captchaBox.Text += tmp2;
                    }
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
                    int fontSize = rnd.Next(12, 18);
                    g.TranslateTransform(25 * (i) + 10, (30 - fontSize) / 2);
                    g.RotateTransform(rnd.Next(-20, 20));
                    int tmpR = rnd.Next(0, 200);
                    int tmpG = rnd.Next(0, (200 - tmpR) / 2);
                    string s = Chars[rnd.Next(Chars.Length)].ToString();
                    captchaBox.Tag = (string)captchaBox.Tag + s;
                    g.DrawString(s, new Font(new string[] { "Arial", "Consolas", "Verdena" }[rnd.Next(3)], fontSize), new SolidBrush(Color.FromArgb(tmpR, tmpG, Math.Max(0, 200 - tmpR - tmpG))), new PointF(5, rnd.Next(8, 13)));
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

        private void LevelWindowHeaderExit_Click(object sender, EventArgs e) => Misc.closeGameWindow.Invoke();
        private void MinigameClose_Click(object sender, EventArgs e) => Misc.closeGameWindow.Invoke();

        #region Minigame
        uint minigameTime = 0;
        uint minigamePrevTime = 0;
        private void InvadersPanel_Paint(object sender, PaintEventArgs e)
        {
            using (GraphicsWrapper w = new GraphicsWrapper(e.Graphics, new Rectangle(Point.Empty, minigamePanel.Size)))
            {
                levels[levelInd].draw(w, minigamePanel, minigameClockT, minigameTime);
                if (minigameTime != minigamePrevTime)
                {
                    levels[levelInd].gameTick(w, minigamePanel, minigameClockT, minigameTime);
                    minigamePrevTime = minigameTime;
                }
            }
        }

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

        bool devWindowMoving = false;
        Point devWindowDiff = Point.Empty;

        private void DevWindowHeader_MouseDown(object sender, MouseEventArgs e)
        {
            devWindowMoving = true;
            devWindowDiff = new Point(devWindow.Location.X - Cursor.Position.X, devWindow.Location.Y - Cursor.Position.Y);
        }

        private void DevWindowHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (devWindowMoving)
                devWindow.Location = new Point(Cursor.Position.X + devWindowDiff.X, Cursor.Position.Y + devWindowDiff.Y);
        }

        private void DevWindowHeader_MouseUp(object sender, MouseEventArgs e) => devWindowMoving = false;
        private void DevWindowHeaderExit_Click(object sender, EventArgs e) => devWindow.Visible = false;
        private void DevWindowOpen_Click(object sender, EventArgs e) => devWindow.Visible = true;
        private void DevWindowDllList_SelectedIndexChanged(object sender, EventArgs e) => _ = Process.Start("explorer", "/select," + (((string)devWindowDllList.SelectedItem).Contains(" ") ? "\"" + (string)devWindowDllList.SelectedItem + "\"" : (string)devWindowDllList.SelectedItem));
        #endregion

        private void DevWindowLevelList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.level = levels[devWindowLevelList.SelectedIndex].availableAfter;
            updateIconVisibility();
        }

        private void DevWindowSkip_Click(object sender, EventArgs e)
        {
            if (levelWindow.Visible)
            {
                if (levelWindowContents.SelectedIndex == 1)
                    captchaBox.Text = (string)captchaBox.Tag;
                LevelWindowC1_Click(sender, e);
            }
        }
    }
}