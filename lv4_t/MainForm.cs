using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;
using Base;

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
        Random rnd;
        Vector2 player;
        Vector2 playerV;
        double lazor;
        double lazorTime;
        double speed;
        int jmpj;
        bool wasOnPlatform;
        List<Vector2> platforms;
        private void initGame()
        {
            rnd = new Random();
            playerV = new Vector2();
            playerV.bounds = new Rectangle(-10, -20, 20, 40);
            playerV.bounds_wrap = false;
            platforms = new List<Vector2>();
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 2; j++)
                {
                    platforms.Add(new Vector2(rnd.Next(minigamePanel.Width - 100) + 50, i * (minigamePanel.Height / 5)));
                }
            player = new Vector2(platforms[platforms.Count / 2].X, -10);
            player.bounds = new Rectangle(-5, 0, minigamePanel.Width + 10, 0);
            player.bounds_wrap = true;
            lazor = player.X;
            lazorTime = 100;
            speed = 1;
            wasOnPlatform = true;
        }
        private void MinigamePanel_Paint(object sender, PaintEventArgs e)
        {
            BufferedGraphics buffer = BufferedGraphicsManager.Current.Allocate(e.Graphics, new Rectangle(0, 0, minigamePanel.Width, minigamePanel.Height));
            Graphics g = buffer.Graphics;
            try
            {
                g.Clear(Color.Black);
                g.FillRectangle(new SolidBrush(Color.Green), player2rect());
                if (lazorTime >= 0 && lazorTime <= 80)
                {
                    g.FillRectangle(new SolidBrush(Color.DarkGray), new RectangleF((float)lazor - 1, 0, 2, minigamePanel.Height));
                    g.FillRectangle(new SolidBrush(Color.Red), new RectangleF((float)lazor - 1, 0, 2, minigamePanel.Height - (float)Misc.map(0, 80, 0, minigamePanel.Height, lazorTime)));
                }
                for (int i = 0; i < platforms.Count; i++)
                    g.FillRectangle(new SolidBrush(Color.White), plat2rect(i));
                Random random = new Random();
                if (minigameTime != minigamePrevTime)
                {
                    speed = Math.Min(minigameTime / 200d, 2) + 0.5;
                    lazorTime -= Math.Min(minigameTime / 800, 2.5) + 0.5;
                    minigamePrevTime = minigameTime;
                    if (lazorTime <= 0)
                    {
                        g.FillRectangle(new SolidBrush(Color.Red), new RectangleF((float)lazor - 5, 0, 10, minigamePanel.Height));
                        if (lazorTime <= -2)
                        {
                            lazorTime = 100;
                            lazor = player.X;
                        }
                        else
                        {
                            if (player.X > lazor - 10 && player.X < lazor + 10)
                                throw new Exception("The VM was shut down to prevent damage to your Machine.", new Exception("0717750f-3508-4bc2-841e-f3b077c676fe"));
                        }
                    }
                    player.Y += speed;
                    for (int i = 0; i < platforms.Count; i++)
                    {
                        platforms[i].Y += speed;
                        if (platforms[i].Y > minigamePanel.Height)
                        {
                            platforms[i].Y = 0;
                            platforms[i].X = rnd.Next(minigamePanel.Width);
                        }
                    }
                    double movementFactor;
                    if (wasOnPlatform)
                    {
                        movementFactor = 2;
                        playerV.X *= 0.7;
                        playerV.Y = Math.Min(playerV.Y, 0);
                    }
                    else
                    {
                        movementFactor = 5;
                        playerV.X *= 0.9;
                        playerV.Y += 1;
                    }
                    if (Input.Up)
                    {
                        if (wasOnPlatform || jmpj > 0)
                        {
                            playerV.Y -= jmpj / 6d + 1.5;
                            jmpj--;
                        }
                    }
                    else
                    {
                        if (wasOnPlatform)
                            jmpj = 10;
                        else
                            jmpj = 0;
                    }
                    jmpj = Math.Max(0, jmpj);
                    if (Input.Left)
                        playerV.X -= movementFactor;
                    if (Input.Right)
                        playerV.X += movementFactor;
                    player.X += playerV.X;
                    if (playerV.Y < 0)
                        player.Y += playerV.Y;
                    else
                        for (int i = 0; i < playerV.Y / 2; i++)
                        {
                            if (onPlatform)
                                break;
                            player.Y += 2;
                        }
                    if (player.Y > minigamePanel.Height)
                        throw new Exception("The VM was shut down to prevent damage to your Machine.", new Exception("0717750f-3508-4bc2-841e-f3b077c676fe"));
                    wasOnPlatform = onPlatform;
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
        bool onPlatform
        {
            get {
                for (int i = 0; i < platforms.Count; i++)
                {
                    RectangleF rect = plat2rect(i);
                    if (player.X < rect.X)
                    {
                        if (player.Y < rect.Y)
                            platforms[i].Tag = (player - new PointF(rect.X, rect.Y)).magnitude;
                        else if (player.Y > rect.Y + rect.Height)
                            platforms[i].Tag = (player - new PointF(rect.X, rect.Y + rect.Height)).magnitude;
                        else
                            platforms[i].Tag = rect.X - player.X;
                    }
                    else if (player.X > rect.X + rect.Width)
                    {
                        if (player.Y < rect.Y)
                            platforms[i].Tag = (player - new PointF(rect.X + rect.Width, rect.Y)).magnitude;
                        else if (player.Y > rect.Y + rect.Height)
                            platforms[i].Tag = (player - new PointF(rect.X + rect.Width, rect.Y + rect.Height)).magnitude;
                        else
                            platforms[i].Tag = player.X - rect.X + rect.Width;
                    }
                    else
                    {
                        if (player.Y < rect.Y)
                            platforms[i].Tag = rect.Y - player.Y;
                        else if (player.Y > rect.Y + rect.Height)
                            platforms[i].Tag = player.Y - (rect.Y + rect.Height);
                        else
                            platforms[i].Tag = 0d;
                    }
                    if (((double)platforms[i].Tag) <= 20 && RectangleF.Intersect(player2rect(), rect) != RectangleF.Empty && player.Y < platforms[i].Y - 8)
                        return true;
                }
                return false;
            }
        }
        RectangleF plat2rect(int platform) => new RectangleF((platforms[platform] - new Vector2(50, 5)).toPointF(), new SizeF(100, 10));
        RectangleF player2rect() => new RectangleF((player - new Vector2(5, 5)).toPointF(), new SizeF(10, 10));
    }
}
