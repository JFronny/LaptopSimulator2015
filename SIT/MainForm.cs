using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SIT
{
    public partial class MainForm : Form
    {
        List<Point> invadersAliens = new List<Point>();
        List<Point> invadersBullets = new List<Point>();
        Point invadersPlayer;
        uint invadersTime = 0;
        uint invadersPrevTime = 0;
        bool invadersCanShoot = true;
        public MainForm()
        {
            InitializeComponent();
            invadersPlayer = new Point(invadersPanel.Width / 4, invadersPanel.Height / 2);
        }


        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            try
            {
                g.Clear(Color.Black);
                for (int i = 0; i < invadersAliens.Count; i++)
                {
                    g.FillRectangle(new SolidBrush(Color.Red), new Rectangle(invadersAliens[i], new Size(10, 10)));
                }
                for (int i = 0; i < invadersBullets.Count; i++)
                {
                    g.FillRectangle(new SolidBrush(Color.White), new Rectangle(invadersBullets[i], new Size(5, 5)));
                }
                g.FillRectangle(new SolidBrush(Color.Green), new Rectangle(invadersPlayer, new Size(10, 10)));
                Random random = new Random();
                if (invadersTime != invadersPrevTime)
                {
                    if (random.Next(0, 100000) < invadersTime + 1300)
                        invadersAliens.Add(new Point(invadersPanel.Width, random.Next(invadersPanel.Height - 10)));
                    invadersPrevTime = invadersTime;
                    for (int i = 0; i < invadersAliens.Count; i++)
                    {
                        invadersAliens[i] = new Point(invadersAliens[i].X - 1, invadersAliens[i].Y);
                        if (Math.Pow(invadersPlayer.X - invadersAliens[i].X, 2) + Math.Pow(invadersPlayer.Y - invadersAliens[i].Y, 2) < 100 | invadersAliens[i].X < 0)
                        {
                            invadersTimer.Enabled = false;
                            g.Clear(Color.Red);
                            g.SmoothingMode = SmoothingMode.AntiAlias;
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            RectangleF rectf = new RectangleF(invadersPanel.Width / 2, invadersPanel.Height / 2, 90, 50);
                            g.DrawString("Lost.", new Font("Tahoma", 20), Brushes.Black, rectf);
                        }
                    }
                    invadersCanShoot = invadersCanShoot | !IsKeyDown(Keys.Space);
                    List<int> aliensToRemove = new List<int>();
                    List<int> bulletsToRemove = new List<int>();
                    for (int i = 0; i < invadersBullets.Count; i++)
                    {
                        invadersBullets[i] = new Point(invadersBullets[i].X + 4, invadersBullets[i].Y);
                        for (int j = 0; j < invadersAliens.Count; j++)
                        {
                            if (Math.Pow(invadersBullets[i].X - invadersAliens[j].X - 2.5f, 2) + Math.Pow(invadersBullets[i].Y - invadersAliens[j].Y - 2.5f, 2) < 56.25f)
                            {
                                if (!aliensToRemove.Contains(j))
                                    aliensToRemove.Add(j);
                                if (!bulletsToRemove.Contains(i))
                                    bulletsToRemove.Add(i);
                            }
                        }
                        if (invadersBullets[i].X > invadersPanel.Width)
                            bulletsToRemove.Add(i);
                    }
                    aliensToRemove = aliensToRemove.Distinct().ToList();
                    aliensToRemove.Sort();
                    aliensToRemove.Reverse();
                    bulletsToRemove = bulletsToRemove.Distinct().ToList();
                    bulletsToRemove.Sort();
                    bulletsToRemove.Reverse();
                    for (int i = 0; i < aliensToRemove.Count; i++)
                    {
                        try
                        {
                            invadersAliens.RemoveAt(aliensToRemove[i]);
                        }
                        catch { }
                    }
                    for (int i = 0; i < bulletsToRemove.Count; i++)
                    {
                        try
                        {
                            invadersBullets.RemoveAt(bulletsToRemove[i]);
                        }
                        catch { }
                    }
                    if (IsKeyDown(Keys.W))
                        invadersPlayer.Y -= 2;
                    if (IsKeyDown(Keys.A))
                        invadersPlayer.X -= 2;
                    if (IsKeyDown(Keys.S))
                        invadersPlayer.Y += 2;
                    if (IsKeyDown(Keys.D))
                        invadersPlayer.X += 2;
                    if (IsKeyDown(Keys.Space) & invadersCanShoot)
                    {
                        invadersBullets.Add(invadersPlayer);
                        invadersCanShoot = false;
                    }
                    if (invadersPlayer.X < 0)
                        invadersPlayer.X = invadersPanel.Width;
                    if (invadersPlayer.X > invadersPanel.Width)
                        invadersPlayer.X = 0;
                    if (invadersPlayer.Y < 0)
                        invadersPlayer.Y = invadersPanel.Height;
                    if (invadersPlayer.Y > invadersPanel.Height)
                        invadersPlayer.Y = 0;
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            invadersTime++;
            invadersPanel.Invalidate();
        }

        private void Button1_Click(object sender, EventArgs e) => Application.Exit();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern short GetKeyState(int keyCode);

        public static bool IsKeyDown(Keys key)
        {
            int state = 0;
            short retVal = GetKeyState((int)key);
            if ((retVal & 0x8000) == 0x8000)
                state |= 1;
            if ((retVal & 1) == 1)
                state |= 2;
            return 1 == (state & 1);
        }
    }
}
