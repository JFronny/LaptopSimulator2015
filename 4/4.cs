using Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace LaptopSimulator2015.Levels
{
    class Lvl4 : Level
    {
        public string installerHeader
        {
            get {
                switch (CultureInfo.CurrentUICulture.Name.Split('-')[0])
                {
                    case "de":
                        return "Flux-Kernel Installationswerkzeug";
                    default:
                        return "FluxKernel installer";
                }
            }
        }

        public string installerText
        {
            get {
                switch (CultureInfo.CurrentUICulture.Name.Split('-')[0])
                {
                    case "de":
                        return "Flux-Kernel ist der beste und schnellste Ersatz-Kernel für Macrohard Doors 98 (tm). Flux erweitert das RAM-Management und Speicher-Vorgänge mit fortschrittlichen APIs, um in unterstützten Anwendungen die Prozessornutzung zu verringern.";
                    default:
                        return "Flux-Kernel is the best and fastest custom kernel for Macrohard Doors 98 (tm). Flux extends RAM-Management and Storage operations with advanced APIs to allow supported Applications to reduce their CPU-Usage.";
                }
            }
        }

        static Image _installer;
        public Image installerIcon
        {
            get {
                if (_installer == null)
                {
                    string __installer = "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";
                    Stream s = new MemoryStream(Convert.FromBase64String(__installer));
                    _installer = Image.FromStream(s);
                    s.Close();
                }
                return _installer;
            }
        }

        public int LevelNumber => 4;
        public int gameClock => 17;
        public Panel desktopIcon { get; set; }
        public int installerProgressSteps => 500;
        uint minigamePrevTime = 0;
        Random rnd = new Random();
        Vector2 player = new Vector2();
        Vector2 playerV = new Vector2();
        double lazor;
        double lazorTime;
        int jmpj;
        List<Vector2> platforms = new List<Vector2>();
        public void gameTick(Graphics e, Panel minigamePanel, Timer minigameTimer, uint minigameTime)
        {
            BufferedGraphics buffer = BufferedGraphicsManager.Current.Allocate(e, new Rectangle(0, 0, minigamePanel.Width, minigamePanel.Height));
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
                if (lazorTime >= 0 && lazorTime <= 30)
                {
                    g.FillRectangle(new SolidBrush(Color.DarkGray), new RectangleF((float)lazor - 1, 0, 2, minigamePanel.Height));
                    g.FillRectangle(new SolidBrush(Color.Red), new RectangleF((float)lazor - 1, 0, 2, minigamePanel.Height - (float)Misc.map(0, 30, 0, minigamePanel.Height, lazorTime)));
                }
                Random random = new Random();
                if (minigameTime != minigamePrevTime)
                {
                    lazorTime -= minigameTime - minigamePrevTime;
                    minigamePrevTime = minigameTime;
                    if (lazorTime <= 0)
                    {
                        g.FillRectangle(new SolidBrush(Color.Red), new RectangleF((float)lazor - 5, 0, 10, minigamePanel.Height));
                        if (lazorTime <= -2)
                        {
                            lazorTime = 40;
                            lazor = player.X;
                        }
                        else
                        {
                            if (player.X >= lazor - 5 && player.X <= lazor + 5)
                                throw new Exception("The VM was shut down to prevent damage to your Machine.", new Exception("0717750f-3508-4bc2-841e-f3b077c676fe"));
                        }
                    }
                    if (onPlatform)
                        playerV.Y = Math.Min(playerV.Y, 0);
                    else
                        playerV.Y += 1;
                    playerV.X /= 1.2f;
                    if (onPlatform)
                        jmpj = 10;
                    else
                        if (!Input.Up)
                        jmpj = 0;
                    if ((onPlatform || jmpj > 0) && Input.Up)
                    {
                        playerV.Y -= jmpj / 6d + 1.5;
                        jmpj--;
                    }
                    double movementFactor = 15;
                    if (onPlatform)
                        movementFactor /= 4;
                    if (Input.Left)
                        playerV.X -= movementFactor;
                    if (Input.Right)
                        playerV.X += movementFactor;
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
                        platforms[i].Y += 1.7;
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
                buffer.Dispose();
            }
            catch (Exception ex) { if (ex.InnerException?.Message == "0717750f-3508-4bc2-841e-f3b077c676fe") Misc.closeGameWindow.Invoke(); else Console.WriteLine(ex.ToString()); }
        }

        public void initGame(Graphics g, Panel minigamePanel, Timer minigameTimer)
        {
            playerV = new Vector2();
            playerV.bounds = new Rectangle(-5, -20, 10, 40);
            playerV.bounds_wrap = false;
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 2; j++)
                    platforms.Add(new Vector2(rnd.Next(minigamePanel.Width), i * (minigamePanel.Height / 5)));
            player = new Vector2(platforms[0].X, -10);
            player.bounds = new Rectangle(-5, 0, minigamePanel.Width + 10, 0);
            player.bounds_wrap = true;
            lazor = player.X;
            lazorTime = 50;
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
