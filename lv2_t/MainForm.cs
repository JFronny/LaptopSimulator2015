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

namespace lv2_t
{
    public partial class MainForm : Form
    {
        #region FRMBD
        uint minigameTime = 0;
        uint minigamePrevTime = 0;
        public MainForm()
        {
            InitializeComponent();
            player = new Vector2(minigamePanel.Width / 2, minigamePanel.Height / 2);
            player.bounds_wrap = true;
            player.bounds = new Rectangle(-10, -10, minigamePanel.Width + 10, minigamePanel.Height + 10);
        }

        private void Button1_Click(object sender, EventArgs e) => Application.Exit();
        private void MinigameClockT_Tick(object sender, EventArgs e)
        {
            minigameTime++;
            minigamePanel.Invalidate();
        }
        #endregion
        List<Vector2> enemies = new List<Vector2>();
        Vector2 player;
        int lives = 3;
        private void MinigamePanel_Paint(object sender, PaintEventArgs e)
        {
            BufferedGraphics buffer = BufferedGraphicsManager.Current.Allocate(e.Graphics, new Rectangle(0, 0, minigamePanel.Width, minigamePanel.Height));
            Graphics g = buffer.Graphics;
            try
            {
                for (int i = 0; i < enemies.Count; i++)
                    g.FillRectangle(new SolidBrush(Color.Red), new Rectangle(enemies[i].toPoint(), new Size(10, 10)));
                g.FillRectangle(new SolidBrush(Color.Green), new Rectangle(player.toPoint(), new Size(10, 10)));
                g.DrawString(lives.ToString(), new Font("Tahoma", 7), Brushes.White, new Rectangle(player.toPoint(), new Size(10, 10)));
                Random random = new Random();
                if (minigameTime != minigamePrevTime)
                {
                    minigamePrevTime = minigameTime;
                    if (random.Next(0, 100000) < minigameTime + 1300)
                    {
                        int tst = random.Next(minigamePanel.Width * 2 + (minigamePanel.Height - 10) * 2);
                        if (tst <= minigamePanel.Width)
                            enemies.Add(new Vector2(tst, 0));
                        else if (tst <= minigamePanel.Width * 2)
                            enemies.Add(new Vector2(tst - minigamePanel.Width, minigamePanel.Height - 10));
                        else if (tst <= minigamePanel.Width * 2 + minigamePanel.Height - 10)
                            enemies.Add(new Vector2(0, tst - minigamePanel.Width * 2));
                        else
                            enemies.Add(new Vector2(0, tst - minigamePanel.Width * 2 - minigamePanel.Height + 10));
                    }
                    if (Input.IsKeyDown(Keys.W))
                        player.Y -= 5;
                    if (Input.IsKeyDown(Keys.A))
                        player.X -= 5;
                    if (Input.IsKeyDown(Keys.S))
                        player.Y += 5;
                    if (Input.IsKeyDown(Keys.D))
                        player.X += 5;
                    List<Vector2> enemiesToRemove = new List<Vector2>();
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        enemies[i].moveTowards(player, Math.Max(6, Math.Sqrt(minigameTime / 100 + 1)));
                        if (player.distanceFromSquared(enemies[i]) < 100)
                        {
                            lives--;
                            enemiesToRemove.Add(enemies[i]);
                            if (lives <= 0)
                                throw new Exception("The VM was shut down to prevent damage to your Machine.", new Exception("0717750f-3508-4bc2-841e-f3b077c676fe"));
                        }
                        for (int j = 0; j < enemies.Count; j++)
                        {
                            if (i != j & enemies[i].distanceFromSquared(enemies[j]) < 25)
                                enemiesToRemove.Add(enemies[i]);
                        }
                    }
                    enemies = enemies.Except(enemiesToRemove.Distinct()).Distinct().ToList();
                }
                buffer.Render();
                buffer.Dispose();
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
                    buffer.Render();
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
