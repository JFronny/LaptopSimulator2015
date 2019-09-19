using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base;

namespace LaptopSimulator2015.Glitches
{
    public class ShuffleChars : CaptchaGlitch
    {
        public double chance { get { if (currentLevel < 1) return 0; return Math.Min(currentLevel / 8d, 0.8); } }
        public int currentLevel { get; set; }
        Random rnd = new Random();
        public void apply(char inputChar, ref string inputString, char[] captchaChars) => inputString = string.Join("", inputString.ToCharArray().OrderBy(x => rnd.Next()));
    }
}
