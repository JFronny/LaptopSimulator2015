using Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopSimulator2015.Glitches
{
    public class Rand : CaptchaGlitch
    {
        public double chance { get { if (currentLevel < 2) return 0; return Math.Min((currentLevel - 1) / 16d, 0.4); } }
        public int currentLevel { get; set; }
        public bool postGlitch => false;
        public void apply(char inputChar, ref string inputString, char[] captchaChars, Random rnd) => inputString += captchaChars[rnd.Next(captchaChars.Length - 1)];
    }
}
