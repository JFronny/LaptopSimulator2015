using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Base;

namespace SIT
{
    public partial class MainForm : Form
    {
        List<Vector2> invadersAliens = new List<Vector2>();
        List<Vector2> invadersBullets = new List<Vector2>();
        Vector2 invadersPlayer;
        uint minigameTime = 0;
        uint minigamePrevTime = 0;
        double speedMod = 5;
        bool invadersCanShoot = true;
        public MainForm()
        {
            InitializeComponent();
            invadersPlayer = new Vector2(minigamePanel.Width / 4, minigamePanel.Height / 2);
            invadersPlayer.bounds_wrap = true;
            invadersPlayer.bounds = new Rectangle(-10, -10, minigamePanel.Width + 10, minigamePanel.Height + 10);
        }


        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            BufferedGraphics buffer = BufferedGraphicsManager.Current.Allocate(e.Graphics, new Rectangle(0, 0, minigamePanel.Width, minigamePanel.Height));
            Graphics g = buffer.Graphics;
            try
            {
                g.Clear(Color.Black);
                for (int i = 0; i < invadersAliens.Count; i++)
                {
                    g.FillRectangle(new SolidBrush(Color.Red), new Rectangle(invadersAliens[i].toPoint(), new Size(10, 10)));
                }
                for (int i = 0; i < invadersBullets.Count; i++)
                {
                    g.FillRectangle(new SolidBrush(Color.White), new Rectangle(invadersBullets[i].toPoint(), new Size(5, 5)));
                }
                g.FillRectangle(new SolidBrush(Color.Green), new Rectangle(invadersPlayer.toPoint(), new Size(10, 10)));
                Random random = new Random();
                if (minigameTime != minigamePrevTime)
                {
                    minigamePrevTime = minigameTime;
                    if (random.Next(0, 100000) < minigameTime + 1300)
                        invadersAliens.Add(new Vector2(minigamePanel.Width, random.Next(minigamePanel.Height - 10)));
                    for (int i = 0; i < invadersAliens.Count; i++)
                    {
                        invadersAliens[i].X -= 1.2;
                        if (invadersPlayer.distanceFromSquared(invadersAliens[i]) < 100 | invadersAliens[i].X < 0)
                        {
                            throw new Exception("The VM was shut down to prevent damage to your Machine.", new Exception("0717750f-3508-4bc2-841e-f3b077c676fe"));
                        }
                    }
                    invadersCanShoot = invadersCanShoot | !Input.Action;
                    List<Vector2> aliensToRemove = new List<Vector2>();
                    List<Vector2> bulletsToRemove = new List<Vector2>();
                    for (int i = 0; i < invadersBullets.Count; i++)
                    {
                        invadersBullets[i].X += 4;
                        for (int j = 0; j < invadersAliens.Count; j++)
                        {
                            if (invadersBullets[i].distanceFromSquared(invadersAliens[j] + new Vector2(2.5f, 2.5f)) < 56.25f)
                            {
                                aliensToRemove.Add(invadersAliens[j]);
                                bulletsToRemove.Add(invadersBullets[i]);
                            }
                        }
                        if (invadersBullets[i].X > minigamePanel.Width)
                            bulletsToRemove.Add(invadersBullets[i]);
                    }
                    invadersAliens = invadersAliens.Except(aliensToRemove.Distinct()).Distinct().ToList();
                    invadersBullets = invadersBullets.Except(bulletsToRemove.Distinct()).Distinct().ToList();
                    speedMod += 0.1;
                    speedMod = Math.Max(Math.Min(speedMod, 5), 1);
                    if (Input.Up)
                        invadersPlayer.Y -= speedMod;
                    if (Input.Left)
                        invadersPlayer.X -= speedMod;
                    if (Input.Down)
                        invadersPlayer.Y += speedMod;
                    if (Input.Right)
                        invadersPlayer.X += speedMod;
                    if (Input.Action & invadersCanShoot)
                    {
                        invadersBullets.Add(new Vector2(0, 2.5) + invadersPlayer);
                        invadersCanShoot = false;
                        speedMod--;
                    }
                }
                buffer.Render();
                buffer.Dispose();
            }
            catch (Exception ex) {
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
                    buffer.Dispose();
                }
                else
#if DEBUG
                    throw;
#else
                    Console.WriteLine(ex.ToString());
#endif
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            minigameTime++;
            minigamePanel.Invalidate();
        }

        private void Button1_Click(object sender, EventArgs e) => Application.Exit();
    }
}
