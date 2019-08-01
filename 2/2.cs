﻿using Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaptopSimulator2015.Levels
{
    class Lvl2 : Level
    {
        public string installerHeader
        {
            get {
                switch (CultureInfo.CurrentUICulture.Name.Split('-')[0])
                {
                    case "de":
                        return "Pentium 2 Download (gratis) (Kein Virus!)";
                    default:
                        return "Pentium 2 free download (No virus!)";
                }
            }
        }

        public string installerText
        {
            get {
                switch (CultureInfo.CurrentUICulture.Name.Split('-')[0])
                {
                    case "de":
                        return "Vielen Dank für diesen garantiert sicheren Download.\r\nBitte klicken sie \"Fortfahren\" um mit der Installation ihres Pentium-Prozessors fortzufahren.";
                    default:
                        return "Thank you for downloading this very secure Installer.\r\nPlease click \"Continue\" to continue the Installation of your Pentium 2.";
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

        public int LevelNumber => 2;

        public int gameClock => 17;

        public Panel desktopIcon { get; set; }

        public int installerProgressSteps => 500;
        List<Vector2> enemies = new List<Vector2>();
        Vector2 player;
        uint minigamePrevTime = 0;
        uint lives = 3;

        public void gameTick(Graphics e, Panel minigamePanel, Timer minigameTimer, uint minigameTime)
        {
            BufferedGraphics buffer = BufferedGraphicsManager.Current.Allocate(e, new Rectangle(0, 0, minigamePanel.Width, minigamePanel.Height));
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
            catch (Exception ex) { if (ex.InnerException?.Message == "0717750f-3508-4bc2-841e-f3b077c676fe") throw new Exception(ex.Message); else Console.WriteLine(ex.ToString()); }
        }

        public void initGame(Graphics g, Panel minigamePanel, Timer minigameTimer)
        {
            player = new Vector2(minigamePanel.Width / 2, minigamePanel.Height / 2);
            player.bounds_wrap = true;
            player.bounds = new Rectangle(-10, -10, minigamePanel.Width + 10, minigamePanel.Height + 10);
            enemies = new List<Vector2>();
            lives = 3;
        }
    }
}
