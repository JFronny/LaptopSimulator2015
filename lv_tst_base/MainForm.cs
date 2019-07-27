using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Base;

namespace lv_tst_base
{
    public partial class MainForm : Form
    {
        #region FRMBD
        uint minigameTime = 0;
        uint minigamePrevTime = 0;
        public MainForm() => InitializeComponent();
        private void Button1_Click(object sender, EventArgs e) => Application.Exit();
        private void MinigameClockT_Tick(object sender, EventArgs e)
        {
            minigameTime++;
            minigamePanel.Invalidate();
        }
        #endregion

        private void MinigamePanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
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
            }
            catch (Exception ex)
            {
                if (ex.InnerException?.Message == "0717750f-3508-4bc2-841e-f3b077c676fe")
                {
                    minigameClockT.Enabled = false;
                    g.Clear(Color.Red);
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    SizeF sLen = g.MeasureString("Lost.", new Font("Tahoma", 20));
                    RectangleF rectf = new RectangleF(minigamePanel.Width / 2 - sLen.Width / 2, minigamePanel.Height / 2 - sLen.Height / 2, 90, 50);
                    g.DrawString("Lost.", new Font("Tahoma", 20), Brushes.Black, rectf);
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