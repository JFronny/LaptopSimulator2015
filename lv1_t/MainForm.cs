using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Base;

namespace SIT
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
        List<Vector2> bullets;
        Vector2 player;
        double speedMod;
        bool enemiesCanShoot;
        private void initGame()
        {
            enemies = new List<Vector2>();
            bullets = new List<Vector2>();
            speedMod = 5;
            enemiesCanShoot = true;
            player = new Vector2(minigamePanel.Width / 4, minigamePanel.Height / 2);
            player.bounds_wrap = true;
            player.bounds = new Rectangle(-10, -10, minigamePanel.Width + 10, minigamePanel.Height + 10);
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            BufferedGraphics buffer = BufferedGraphicsManager.Current.Allocate(e.Graphics, new Rectangle(0, 0, minigamePanel.Width, minigamePanel.Height));
            Graphics g = buffer.Graphics;
            try
            {
                g.Clear(Color.Black);
                for (int i = 0; i < enemies.Count; i++)
                {
                    g.FillRectangle(new SolidBrush(Color.Red), new Rectangle(enemies[i].toPoint(), new Size(10, 10)));
                }
                for (int i = 0; i < bullets.Count; i++)
                {
                    g.FillRectangle(new SolidBrush(Color.White), new Rectangle(bullets[i].toPoint(), new Size(5, 5)));
                }
                g.FillRectangle(new SolidBrush(Color.Green), new Rectangle(player.toPoint(), new Size(10, 10)));
                Random random = new Random();
                if (minigameTime != minigamePrevTime)
                {
                    minigamePrevTime = minigameTime;
                    if (random.Next(0, 100000) < minigameTime + 1300)
                        enemies.Add(new Vector2(minigamePanel.Width, random.Next(minigamePanel.Height - 10)));
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        enemies[i].X -= 1.2;
                        if (player.distanceFromSquared(enemies[i]) < 100 | enemies[i].X < 0)
                        {
                            throw new Exception("The VM was shut down to prevent damage to your Machine.", new Exception("0717750f-3508-4bc2-841e-f3b077c676fe"));
                        }
                    }
                    enemiesCanShoot = enemiesCanShoot | !Input.Action;
                    List<Vector2> enemiesToRemove = new List<Vector2>();
                    List<Vector2> bulletsToRemove = new List<Vector2>();
                    for (int i = 0; i < bullets.Count; i++)
                    {
                        bullets[i].X += 4;
                        for (int j = 0; j < enemies.Count; j++)
                        {
                            if (bullets[i].distanceFromSquared(enemies[j] + new Vector2(2.5f, 2.5f)) < 56.25f)
                            {
                                enemiesToRemove.Add(enemies[j]);
                                bulletsToRemove.Add(bullets[i]);
                            }
                        }
                        if (bullets[i].X > minigamePanel.Width)
                            bulletsToRemove.Add(bullets[i]);
                    }
                    enemies = enemies.Except(enemiesToRemove.Distinct()).Distinct().ToList();
                    bullets = bullets.Except(bulletsToRemove.Distinct()).Distinct().ToList();
                    speedMod += 0.1;
                    speedMod = Math.Max(Math.Min(speedMod, 5), 1);
                    if (Input.Up)
                        player.Y -= speedMod;
                    if (Input.Left)
                        player.X -= speedMod;
                    if (Input.Down)
                        player.Y += speedMod;
                    if (Input.Right)
                        player.X += speedMod;
                    if (Input.Action & enemiesCanShoot)
                    {
                        bullets.Add(new Vector2(0, 2.5) + player);
                        enemiesCanShoot = false;
                        speedMod--;
                    }
                }
                buffer.Render();
                buffer.Dispose();
            }
            catch (Exception ex) {
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
