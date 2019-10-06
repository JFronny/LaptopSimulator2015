using LaptopSimulator2015;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Base;

namespace LevelTest
{
    public partial class MainForm : Form
    {
        Minigame level;
        public MainForm(Minigame game)
        {
            Misc.closeGameWindow = () => { level.initGame(minigamePanel, minigameClockT); };
            level = game;
            InitializeComponent();
            minigameClockT.Interval = level.gameClock;
            Text = level.name;
            Misc.closeGameWindow.Invoke();
        }

        uint minigameTime;
        uint minigamePrevTime;
        private void MinigameClockT_Tick(object sender, EventArgs e)
        {
            minigameTime++;
            minigamePanel.Invalidate();
        }

        private void MinigamePanel_Paint(object sender, PaintEventArgs e)
        {
            using (GraphicsWrapper w = new GraphicsWrapper(e.Graphics, level.backColor, new Rectangle(Point.Empty, minigamePanel.Size)))
            {
                w.Clear();
                level.draw(w, minigamePanel, minigameClockT, minigameTime);
                if (minigameTime != minigamePrevTime)
                {
                    level.gameTick(w, minigamePanel, minigameClockT, minigameTime);
                    minigamePrevTime = minigameTime;
                }
            }
        }

        bool isFClose = true;
        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                isFClose = false;
                Close();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isFClose)
                Environment.Exit(0);
        }
    }
}
