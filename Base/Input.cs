using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Base
{
    public class Input
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern short GetKeyState(int keyCode);

        //public static bool IsKeyDown(Key key) => Keyboard.IsKeyDown(key);
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
            /*Enum.TryParse(key.ToString(), out Key k);
            if (k == Key.None)
                return false;
            return IsKeyDown(k);*/
        }

        public static bool Up => IsKeyDown(Keys.Up) || IsKeyDown(Keys.W);
        public static bool Left => IsKeyDown(Keys.Left) || IsKeyDown(Keys.A);
        public static bool Down => IsKeyDown(Keys.Down) || IsKeyDown(Keys.S);
        public static bool Right => IsKeyDown(Keys.Right) || IsKeyDown(Keys.D);
        public static bool Action => IsKeyDown(Keys.Space) || IsKeyDown(Keys.Q) || IsKeyDown(Keys.E);
    }
}
