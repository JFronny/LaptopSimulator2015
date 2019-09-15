using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Base;

namespace lv2_t
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
        List<Vector2> enemies;
        Vector2 player;
        int lives;
        private void initGame()
        {
            enemies = new List<Vector2>();
            player = new Vector2(minigamePanel.Width / 2, minigamePanel.Height / 2);
            player.bounds_wrap = true;
            player.bounds = new Rectangle(-10, -10, minigamePanel.Width + 10, minigamePanel.Height + 10);
            lives = 3;
        }

        private void MinigamePanel_Paint(object sender, PaintEventArgs e)
        {
            BufferedGraphics buffer = BufferedGraphicsManager.Current.Allocate(e.Graphics, new Rectangle(0, 0, minigamePanel.Width, minigamePanel.Height));
            Graphics g = buffer.Graphics;
            try
            {
                for (int i = 0; i < enemies.Count; i++)
                    g.FillRectangle(new SolidBrush(Color.Red), new Rectangle(enemies[i].toPoint(), new Size(10, 10)));
                g.FillRectangle(new SolidBrush(Color.Green), new Rectangle(player.toPoint(), new Size(10, 10)));
                Drawing.DrawSizedString(g, lives.ToString(), 7, (player + new PointF(5, 5)).toPointF(), Brushes.White, true);
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
                    if (Input.Up)
                        player.Y -= 5;
                    if (Input.Left)
                        player.X -= 5;
                    if (Input.Down)
                        player.Y += 5;
                    if (Input.Right)
                        player.X += 5;
                    List<Vector2> enemiesToRemove = new List<Vector2>();
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        enemies[i].moveTowards(player, Math.Max(6, Math.Sqrt(minigameTime / 100 + 1)));
                        for (int j = 0; j < enemies.Count; j++)
                        {
                            if (i != j && enemies[i].distanceFromSquared(enemies[j]) < 25 && !enemiesToRemove.Contains(enemies[j]))
                                enemiesToRemove.Add(enemies[i]);
                        }
                        if (player.distanceFromSquared(enemies[i]) < 100 && !enemiesToRemove.Contains(enemies[i]))
                        {
                            lives--;
                            enemiesToRemove.Add(enemies[i]);
                            if (lives <= 0)
                                throw new Exception("The VM was shut down to prevent damage to your Machine.", new Exception("0717750f-3508-4bc2-841e-f3b077c676fe"));
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
