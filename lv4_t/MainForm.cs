using Base;
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

namespace lv4_t
{
    public partial class MainForm : Form
    {
        #region FRMBD
        uint minigameTime = 0;
        uint minigamePrevTime = 0;
        public MainForm()
        {
            InitializeComponent();
            player = new Vector2(minigamePanel.Width / 2, 0);
            player.bounds = new Rectangle(-5, 0, minigamePanel.Width + 10, 0);
            player.bounds_wrap = true;
            playerV = new Vector2();
            playerV.bounds = new Rectangle(-5, -20, 10, 40);
            playerV.bounds_wrap = false;
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 2; j++)
                    platforms.Add(new Vector2(rnd.Next(minigamePanel.Width), i * (minigamePanel.Height / 5)));
        }

        private void Button1_Click(object sender, EventArgs e) => Application.Exit();
        private void MinigameClockT_Tick(object sender, EventArgs e)
        {
            minigameTime++;
            minigamePanel.Invalidate();
        }
        #endregion
        Random rnd = new Random();
        Vector2 player = new Vector2();
        Vector2 playerV = new Vector2();
        int jmpj;
        List<Vector2> platforms = new List<Vector2>();
        private void MinigamePanel_Paint(object sender, PaintEventArgs e)
        {
            BufferedGraphics buffer = BufferedGraphicsManager.Current.Allocate(e.Graphics, new Rectangle(0, 0, minigamePanel.Width, minigamePanel.Height));
            Graphics g = buffer.Graphics;
            try
            {
                g.Clear(Color.Black);
                g.FillRectangle(new SolidBrush(Color.Green), player2rect());
                bool onPlatform = false;
                for (int i = 0; i < platforms.Count; i++)
                {
                    g.FillRectangle(new SolidBrush(Color.White), plat2rect(i));
                    onPlatform |= isOnPlatform(i);
                }
                Random random = new Random();
                if (minigameTime != minigamePrevTime)
                {
                    minigamePrevTime = minigameTime;
                    if (onPlatform)
                        playerV.Y = Math.Min(playerV.Y, 0);
                    else
                        playerV.Y += 0.05f + Math.Pow(0.9, Math.Max(playerV.Y + 1, 0));
                    playerV.X /= 1.2f;
                    if (onPlatform)
                        jmpj = 10;
                    else
                        if (!Input.Up)
                        jmpj = 0;
                    if ((onPlatform || jmpj > 0) && Input.Up)
                    {
                        playerV.Y -= Math.Sqrt(jmpj);
                        jmpj--;
                    }
                    if (Input.Left)
                        playerV.X -= 5;
                    if (Input.Right)
                        playerV.X += 5;
                    player.X += playerV.X;
                    onPlatform = false;
                    if (playerV.Y < 0)
                        player.Y += playerV.Y;
                    else
                        for (int i = 0; i < playerV.Y / 2; i++)
                        {
                            for (int j = 0; j < platforms.Count; j++)
                                onPlatform |= isOnPlatform(j);
                            if (!onPlatform)
                                player.Y += 2;
                        }
                    List<Vector2> platformsToRemove = new List<Vector2>();
                    for (int i = 0; i < platforms.Count; i++)
                    {
                        platforms[i].Y++;
                        if (platforms[i].Y > minigamePanel.Height)
                        {
                            platforms[i].Y = 0;
                            platforms[i].X = rnd.Next(minigamePanel.Width);
                        }
                    }
                    if (player.Y > minigamePanel.Height)
                        throw new Exception("The VM was shut down to prevent damage to your Machine.", new Exception("0717750f-3508-4bc2-841e-f3b077c676fe"));
                }
                buffer.Render();
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

        RectangleF plat2rect(int platform) => new RectangleF((platforms[platform] - new Vector2(50, 5)).toPointF(), new SizeF(100, 10));
        RectangleF player2rect() => new RectangleF((player - new Vector2(5, 5)).toPointF(), new SizeF(10, 10));

        bool isOnPlatform(int platform)
        {
            calcDist(platform);
            return ((double)platforms[platform].Tag) <= 20 && RectangleF.Intersect(player2rect(), plat2rect(platform)) != RectangleF.Empty && player.Y < platforms[platform].Y - 8;
        }

        void calcDist(int platform)
        {
            RectangleF rect = plat2rect(platform);
            if (player.X < rect.X)
            {
                if (player.Y < rect.Y)
                {
                    Vector2 diff = player - new Vector2(rect.X, rect.Y);
                    platforms[platform].Tag = diff.magnitude;
                }
                else if (player.Y > rect.Y + rect.Height)
                {
                    Vector2 diff = player - new Vector2(rect.X, rect.Y + rect.Height);
                    platforms[platform].Tag = diff.magnitude;
                }
                else
                {
                    platforms[platform].Tag = rect.X - player.X;
                }
            }
            else if (player.X > rect.X + rect.Width)
            {
                if (player.Y < rect.Y)
                {
                    Vector2 diff = player - new Vector2(rect.X + rect.Width, rect.Y);
                    platforms[platform].Tag = diff.magnitude;
                }
                else if (player.Y > rect.Y + rect.Height)
                {
                    Vector2 diff = player - new Vector2(rect.X + rect.Width, rect.Y + rect.Height);
                    platforms[platform].Tag = diff.magnitude;
                }
                else
                {
                    platforms[platform].Tag = player.X - rect.X + rect.Width;
                }
            }
            else
            {
                if (player.Y < rect.Y)
                {
                    platforms[platform].Tag = rect.Y - player.Y;
                }
                else if (player.Y > rect.Y + rect.Height)
                {
                    platforms[platform].Tag = player.Y - (rect.Y + rect.Height);
                }
                else
                {
                    platforms[platform].Tag = 0d;
                }
            }
        }
    }
}
