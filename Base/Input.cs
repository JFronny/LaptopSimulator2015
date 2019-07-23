﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Base
{
    public class Input
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern short GetKeyState(int keyCode);

        public static bool IsKeyDown(Keys key)
        {
            try
            {
                int state = 0;
                short retVal = GetKeyState((int)key);
                if ((retVal & 0x8000) == 0x8000)
                    state |= 1;
                if ((retVal & 1) == 1)
                    state |= 2;
                return 1 == (state & 1);
            }
            catch (Exception e1)
            {
                Console.WriteLine("Invader: IsKeyDown failed:\r\n" + e1.ToString());
                return false;
            }
        }
    }
}
