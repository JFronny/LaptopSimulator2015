using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Base;

namespace lv_tst_base
{
    public partial class MainForm : Form
    {
        #region FRMBD
        uint minigameTime;
        uint minigamePrevTime;
        public MainForm()
        {
            InitializeComponent();
            _initGame();
        }

        private void Button1_Click(object sender, EventArgs e) => Application.Exit();
        private void MinigameClockT_Tick(object sender, EventArgs e)
        {
            minigameTime++;
            minigamePanel.Invalidate();
        }

        private void _initGame()
        {
            minigameTime = 0;
            minigamePrevTime = 0;
            initGame();
        }
        #endregion
        private void initGame()
        {

        }

        private void MinigamePanel_Paint(object sender, PaintEventArgs e)
        {
            BufferedGraphics buffer = BufferedGraphicsManager.Current.Allocate(e.Graphics, new Rectangle(0, 0, minigamePanel.Width, minigamePanel.Height));
            Graphics g = buffer.Graphics;
            try
            {
                g.Clear(Color.Black);
                //Draw Sprites
                Random random = new Random();
                if (minigameTime != minigamePrevTime)
                {
                    minigamePrevTime = minigameTime;
                    //Game Logic
                }
                buffer.Render();
            }
            catch (Exception ex)
            {
                if (ex.InnerException?.Message == "0717750f-3508-4bc2-841e-f3b077c676fe")
                {
                    g.Clear(Color.Red);
                    Drawing.DrawSizedString(g, "Lost.", 20, new PointF(minigamePanel.Width / 2, minigamePanel.Height / 2), Brushes.Black, true);
                    buffer.Render();
                    Thread.Sleep(500);
                    _initGame();
                }
                else
#if DEBUG
                    throw;
#else
                    Console.WriteLine(ex.ToString());
#endif
            }
        }
    }
}