using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaptopSimulator2015
{
    public interface Minigame
    {
        string name { get; }
        Image icon { get; }
        int levelNumber { get; }
        int gameClock { get; }
        Panel desktopIcon { get; set; }
        void initGame(Graphics g, Panel minigamePanel, Timer minigameTimer);
        void gameTick(Graphics g, Panel minigamePanel, Timer minigameTimer, uint minigameTime);
    }
    public interface Level : Minigame
    {
        string installerText { get; }
        int installerProgressSteps { get; }
    }
    public interface Goal : Minigame
    {
        int playableAfter { get; }
        string[] availableText { get; }
        string[] incompleteText { get; }
        string[] completeText { get; }
    }
}
