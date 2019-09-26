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
            level = game;
            InitializeComponent();
            minigameClockT.Interval = level.gameClock;
            Text = level.name;
            level.initGame(minigamePanel, minigameClockT);
        }

        private void CloseButton_Click(object sender, EventArgs e) => Application.Exit();
        uint minigameTime;
        uint minigamePrevTime;
        private void MinigameClockT_Tick(object sender, EventArgs e)
        {
            minigameTime++;
            minigamePanel.Invalidate();
        }

        private void MinigamePanel_Paint(object sender, PaintEventArgs e)
        {
            using (GraphicsWrapper w = new GraphicsWrapper(e.Graphics, new Rectangle(Point.Empty, minigamePanel.Size)))
            {
                level.draw(w, minigamePanel, minigameClockT, minigameTime);
                if (minigameTime != minigamePrevTime)
                {
                    level.gameTick(w, minigamePanel, minigameClockT, minigameTime);
                    minigamePrevTime = minigameTime;
                }
            }
        }
    }
}
