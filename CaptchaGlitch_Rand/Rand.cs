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
        public double chance { get { if (currentLevel < 2) return 0; return Math.Min((currentLevel - 1) / 16d, 0.8); } }
        public int currentLevel { get; set; }
        public void apply(char inputChar, ref string inputString, char[] captchaChars) => inputString += captchaChars[new Random().Next(captchaChars.Length - 1)];
    }
}
