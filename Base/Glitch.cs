using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    public interface CaptchaGlitch
    {
        /// <summary>
        /// The chance of the Glitch being called, must be bewteen 0 and 1. Set this to zero if it should not be called (select based on currentLevel)
        /// </summary>
        double chance { get; }
        /// <summary>
        /// The current level, intended for modifying the intensity of the Glitch
        /// </summary>
        int currentLevel { get; set; }
        /// <summary>
        /// If set to true this will always try to execute the glitch (check the chance)
        /// </summary>
        bool postGlitch { get; }
        /// <summary>
        /// Called with the selected chance after the player inputs a char to the Captcha-box
        /// </summary>
        /// <param name="inputChar">The character the player typed</param>
        /// <param name="inputString">The string to be added to the Captcha-box, modified by previous CaptchaGlitch instances</param>
        /// <param name="captchaChars">The characters which can be added to the CaptchaBox</param>
        /// <param name="rnd">Random Number Generator shared between instances</param>
        void apply(char inputChar, ref string inputString, char[] captchaChars, Random rnd);
    }
}
