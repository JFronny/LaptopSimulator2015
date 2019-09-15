﻿using Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lv3_t
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
        Vector2 center;
        Vector2 cannon;
        Vector2 targ;
        List<Vector2> targets;
        Rectangle player => new Rectangle(center.toPoint().X - 5, center.toPoint().Y - 5, 10, 10);
        double playerRot;
        double cannonL;
        double power;
        bool firing;
        uint lastTarget;
        private void initGame()
        {
            center = new Vector2(minigamePanel.Width / 2, minigamePanel.Height / 2);
            cannon = new Vector2(center);
            targ = new Vector2(center);
            targets = new List<Vector2>();
            playerRot = 0;
            cannonL = 30;
            power = 10;
            firing = false;
            lastTarget = 0;
        }
        private void MinigamePanel_Paint(object sender, PaintEventArgs e)
        {
            BufferedGraphics buffer = BufferedGraphicsManager.Current.Allocate(e.Graphics, new Rectangle(0, 0, minigamePanel.Width, minigamePanel.Height));
            Graphics g = buffer.Graphics;
            try
            {
                g.Clear(Color.Black);
                g.FillRectangle(new SolidBrush(Color.Green), player);
                g.DrawLine(new Pen(new SolidBrush(Color.Green), 5), center.toPoint(), cannon.toPoint());
                for (int i = 0; i < targets.Count; i++)
                {
                    g.DrawEllipse(new Pen(new SolidBrush(Color.Red), 6), new RectangleF(Misc.d2f(targets[i].X - 10), Misc.d2f(targets[i].Y - 10), 20, 20));
                    g.DrawEllipse(new Pen(new SolidBrush(Color.White), 6), new RectangleF(Misc.d2f(targets[i].X - 7), Misc.d2f(targets[i].Y - 7), 14, 14));
                    g.FillEllipse(new SolidBrush(Color.Red), new RectangleF(Misc.d2f(targets[i].X - 3), Misc.d2f(targets[i].Y - 3), 6, 6));
                    g.DrawLine(new Pen(new SolidBrush(Color.Gray), 3), Misc.d2f(targets[i].X - 13), Misc.d2f(targets[i].Y - 15), Misc.d2f(targets[i].X + 13), Misc.d2f(targets[i].Y - 15));
                    g.DrawLine(new Pen(new SolidBrush(Color.Red), 3), Misc.d2f(targets[i].X - 13), Misc.d2f(targets[i].Y - 15), Misc.d2f(targets[i].X + ((((double)targets[i].Tag) * 0.2) - 12.9) + 0.1), Misc.d2f(targets[i].Y - 15));
                }
                if (firing)
                {
                    g.DrawRectangle(new Pen(new SolidBrush(Color.Green), 1), new Rectangle(Misc.d2i(targ.X - power / 2), Misc.d2i(targ.Y - power / 2), Misc.d2i(power), Misc.d2i(power)));
                    g.DrawLine(new Pen(new SolidBrush(Color.Green), 1), new PointF(Misc.d2i(targ.X), Misc.d2i(targ.Y - power / 2)), new PointF(Misc.d2i(targ.X), Misc.d2i(targ.Y + power / 2)));
                    g.DrawLine(new Pen(new SolidBrush(Color.Green), 1), new PointF(Misc.d2i(targ.X - power / 2), Misc.d2i(targ.Y)), new PointF(Misc.d2i(targ.X + power / 2), Misc.d2i(targ.Y)));
                }
                else
                {
                    g.FillRectangle(new SolidBrush(Color.Green), new RectangleF(Misc.d2f(targ.X - 2.5f), Misc.d2f(targ.Y - 2.5f), 5, 5));
                }
                Random random = new Random();
                if (minigameTime != minigamePrevTime)
                {
                    minigamePrevTime = minigameTime;
                    if (minigameTime - lastTarget > 90 + 40 / (minigameTime / 100 + 1))
                    {
                        targets.Add(new Vector2(random.Next(minigamePanel.Height + 25) + (minigamePanel.Width - minigamePanel.Height - 50) / 2, random.Next(minigamePanel.Height)));
                        targets[targets.Count - 1].Tag = (double)130;
                        lastTarget = minigameTime;
                    }
                    cannon = new Vector2(center);
                    cannon.moveInDirection(Misc.deg2rad(playerRot), 20);
                    if (Input.Action)
                    {
                        firing = true;
                        power = Math.Min(power + 5, 100);
                    }
                    else
                        if (firing)
                    {
                        firing = false;
                        List<Vector2> targetsToRemove = new List<Vector2>();
                        for (int i = 0; i < targets.Count; i++)
                        {
                            if (targets[i].distanceFromSquared(targ) <= Math.Pow(power + 10, 2))
                                targetsToRemove.Add(targets[i]);
                        }
                        targets = targets.Except(targetsToRemove.Distinct()).Distinct().ToList();
                        g.FillRectangle(new SolidBrush(Color.White), new Rectangle(Misc.d2i(targ.X - power / 2), Misc.d2i(targ.Y - power / 2), Misc.d2i(power), Misc.d2i(power)));
                        power = 10;
                    }
                    targ = new Vector2(center);
                    targ.Tag = playerRot;
                    if (Input.Up)
                        cannonL += 100 / power;
                    if (Input.Down)
                        cannonL -= 100 / power;
                    if (Input.Right)
                        playerRot += 80 / power;
                    if (Input.Left)
                        playerRot -= 80 / power;
                    while (playerRot > 360)
                        playerRot -= 360;
                    while (playerRot < 0)
                        playerRot += 360;
                    cannonL = Math.Max(Math.Min(cannonL, minigamePanel.Height / 2), 22.5f);
                    targ.moveInDirection(Misc.deg2rad((double)targ.Tag), cannonL);
                    for (int i = 0; i < targets.Count; i++)
                    {
                        targets[i].Tag = ((double)targets[i].Tag) - 1;
                        if ((double)targets[i].Tag <= 0)
                            throw new Exception("The VM was shut down to prevent damage to your Machine.", new Exception("0717750f-3508-4bc2-841e-f3b077c676fe"));
                    }
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
