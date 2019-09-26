using Base;
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
        /// <summary>
        /// The minigames displayed name, found in installers title and tooltips
        /// </summary>
        string name { get; }
        /// <summary>
        /// The minigames icon, found in installers and on desktop
        /// </summary>
        Image icon { get; }
        /// <summary>
        /// Level on which the Minigame becomes visible
        /// </summary>
        int availableAfter { get; }
        /// <summary>
        /// Defines the delay between frames
        /// </summary>
        int gameClock { get; }
        /// <summary>
        /// DO NOT CHANGE! INTERNAL USE ONLY!
        /// </summary>
        Panel desktopIcon { get; set; }
        /// <summary>
        /// Called before each time before gameTick, to be used for resetting/initializing variables
        /// </summary>
        /// <param name="g">A temporary Graphics object, should not be used</param>
        /// <param name="minigamePanel">The panel on which the minigame is displayed</param>
        /// <param name="minigameTimer">The timer used for scheduling frames</param>
        void initGame(Panel minigamePanel, Timer minigameTimer);
        /// <summary>
        /// Called physics frame
        /// </summary>
        /// <param name="g">Graphics object, to be used for drawing the scene</param>
        /// <param name="minigamePanel">The panel on which the minigame is displayed</param>
        /// <param name="minigameTimer">The timer used for scheduling frames</param>
        /// <param name="minigameTime">The amount of total displayed frames</param>
        void gameTick(GraphicsWrapper g, Panel minigamePanel, Timer minigameTimer, uint minigameTime);
        /// <summary>
        /// Called graphics frame
        /// </summary>
        /// <param name="g">Graphics object, to be used for drawing the scene</param>
        /// <param name="minigamePanel">The panel on which the minigame is displayed</param>
        /// <param name="minigameTimer">The timer used for scheduling frames</param>
        /// <param name="minigameTime">The amount of total displayed frames</param>
        void draw(GraphicsWrapper g, Panel minigamePanel, Timer minigameTimer, uint minigameTime);
    }
    public interface Level : Minigame
    {
        /// <summary>
        /// Description shown on the installers first page
        /// </summary>
        string installerText { get; }
        /// <summary>
        /// Amount of seconds the minigame is to be played times ten
        /// </summary>
        int installerProgressSteps { get; }
    }
    public interface Goal : Minigame
    {
        /// <summary>
        /// The level on which the Goal is reached
        /// </summary>
        int playableAfter { get; }
        /// <summary>
        /// The text displayed after the Minigame becomes visible
        /// </summary>
        string[] availableText { get; }
        /// <summary>
        /// The text displayed after finding out about the fact the Goal is not reached
        /// </summary>
        string[] incompleteText { get; }
        /// <summary>
        /// The text displayed after Goal is reached (NOT after the goals minigame is played)
        /// </summary>
        string[] completeText { get; }
    }
}
