﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing.Drawing2D;
using Base;

namespace LaptopSimulator2015
{
    public partial class Tutorial : Form
    {
        public Tutorial()
        {
            InitializeComponent();
#if DEBUG
            skipButton.Visible = true;
#endif
            p1lang.Text = Settings.lang.Name.Split('-')[0];
            p1descLabel.Text = strings.winMenuText;
            cancelButton.Text = strings.cancel;
            skipButton.Text = strings.skip;
            p1continue.Text = strings._continue;
            p2continue.Text = strings._continue;
            p3continue.Text = strings.install.ToUpper();
            p5completeLabel.Text = strings.tutorialFinish;
        }

        private void skipButton_Click(object sender, EventArgs e) => tabs.SelectedIndex = 4;
        private void Tutorial_Shown(object sender, EventArgs e) => Program.splash.Hide();
        private void cancelButton_Click(object sender, EventArgs e) => Environment.Exit(0);
        private void lang_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.lang = CultureInfo.GetCultureInfo(p1lang.Text);
            Thread.CurrentThread.CurrentUICulture = Settings.lang;
            p1descLabel.Text = strings.winMenuText;
            cancelButton.Text = strings.cancel;
            skipButton.Text = strings.skip;
            p1continue.Text = strings._continue;
            p2continue.Text = strings._continue;
            p3continue.Text = strings.install.ToUpper();
            p5completeLabel.Text = strings.tutorialFinish;
        }

        private void Continue1_Click(object sender, EventArgs e)
        {
            tabs.SelectedIndex = 1;
            progressBar.Value = 25;
        }

        private void continue2_Click(object sender, EventArgs e)
        {
            tabs.SelectedIndex = 2;
            progressBar.Value = 50;
        }

        private void continue3_Click(object sender, EventArgs e)
        {
            tabs.SelectedIndex = 3;
            progressTimer.Enabled = true;
        }

        int timertime = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timertime <= 50)
            {
                progressBar.Value = 50 + timertime;
                timertime++;
                tutorialPanel.Invalidate();
            }
            else
            {
                tabs.SelectedIndex = 4;
                progressTimer.Enabled = false;
            }
        }

        private void p5reboot_Click(object sender, EventArgs e)
        {
            Settings.level = 0;
            Settings.Save();
            Program.splash.Show();
            Close();
        }

        private void tutorialPanel_Paint(object sender, PaintEventArgs e)
        {
            using (GraphicsWrapper g = new GraphicsWrapper(e.Graphics, Color.Red, new Rectangle(Point.Empty, tutorialPanel.Size), true))
            {
                for (int i = 0; i < 40; i++)
                    for (int j = 0; j < 40; j++)
                        g.DrawLine(new PointF(tutorialPanel.Width / 2, tutorialPanel.Height / 2), new PointF(i * 10 + 5, j * 10 + 5), Color.DarkGray, 1, false);
                g.DrawSizedString("This is " + ((timertime < 17) ? "bad" : (timertime <= 33) ? "neutral" : "you"), 10, new PointF(tutorialPanel.Width / 2, tutorialPanel.Height / 2), new SolidBrush((timertime < 17) ? Color.Red : (timertime <= 33) ? Color.White : Color.Green), true, true);
            }
        }
    }
}
