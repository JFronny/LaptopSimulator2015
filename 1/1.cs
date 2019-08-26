using Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;

namespace LaptopSimulator2015.Levels
{
    class Lvl1 : Level
    {
        static Image _installer;
        public string installerHeader
        {
            get {
                switch (CultureInfo.CurrentUICulture.Name.Split('-')[0])
                {
                    case "de":
                        return "Speichererweiterer 2.0";
                    default:
                        return "RAM Installer 2.0";
                }
            }
        }

        public string installerText
        {
            get {
                switch (CultureInfo.CurrentUICulture.Name.Split('-')[0])
                {
                    case "de":
                        return "Vielen Dank dass sie sich entschieden haben, mit unserem super sicherem Speichererweiterer ihren RAM zu erweitern.\r\n\r\nIhr Computer wird es ihnen danken!";
                    default:
                        return "Thank you for deciding to download RAM Installer 2.0 from our secure download servers. Please wait a second while we install your RAM.\r\n\r\nYour Computer is about to run a whole lot smoother!";
                }
            }
        }

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

        public int LevelNumber => 1;

        public int gameClock => 17;

        public Panel desktopIcon { get; set; }

        public int installerProgressSteps => 500;

        List<Vector2> invadersAliens = new List<Vector2>();
        List<Vector2> invadersBullets = new List<Vector2>();
        Vector2 invadersPlayer;
        uint minigamePrevTime = 0;
        bool invadersCanShoot = true;
        double speedMod = 5;

        public void gameTick(Graphics e, Panel minigamePanel, Timer minigameTimer, uint minigameTime)
        {
            BufferedGraphics buffer = BufferedGraphicsManager.Current.Allocate(e, new Rectangle(0, 0, minigamePanel.Width, minigamePanel.Height));
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
            catch (Exception ex) { if (ex.InnerException?.Message == "0717750f-3508-4bc2-841e-f3b077c676fe") throw new Exception(ex.Message); else Console.WriteLine(ex.ToString()); }
        }

        public void initGame(Graphics g, Panel minigamePanel, Timer minigameTimer)
        {
            invadersPlayer = new Vector2(minigamePanel.Width / 4, minigamePanel.Height / 2);
            invadersPlayer.bounds_wrap = true;
            invadersPlayer.bounds = new Rectangle(-10, -10, minigamePanel.Width + 10, minigamePanel.Height + 10);
            invadersAliens = new List<Vector2>();
            invadersBullets = new List<Vector2>();
            minigamePrevTime = 0;
            invadersCanShoot = true;
            speedMod = 5;
        }
    }
}