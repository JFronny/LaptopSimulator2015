using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaptopSimulator2015
{
    public interface Level
    {
        string installerHeader { get; }
        string installerText { get; }
        Image installerIcon { get; }
        int LevelNumber { get; }
        int gameClock { get; }
        Panel desktopIcon { get; set; }
        int installerProgressSteps { get; }
        void initGame(Graphics g, Panel invadersPanel, Timer invadersTimer);
        void gameTick(Graphics g, Panel invadersPanel, Timer invadersTimer, uint invadersTime);
    }
}
