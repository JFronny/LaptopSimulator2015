using CC_Functions.W32;
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
    public static class Input
    {
        /// <summary>
        /// Check whether the Key is pressed
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns>Whether the key is pressed</returns>
        public static bool IsKeyDown(Keys key)
        {
            try
            {
                return KeyboardReader.IsKeyDown(key);
            }
            catch (Exception e)
            {
                Console.WriteLine("Invader: IsKeyDown failed:\r\n" + e.ToString());
                return false;
            }
        }

        /// <summary>
        /// Unified input for going up
        /// </summary>
        public static bool Up => IsKeyDown(Keys.Up) || IsKeyDown(Keys.W);
        /// <summary>
        /// Unified input for going left
        /// </summary>
        public static bool Left => IsKeyDown(Keys.Left) || IsKeyDown(Keys.A);
        /// <summary>
        /// Unified input for going down
        /// </summary>
        public static bool Down => IsKeyDown(Keys.Down) || IsKeyDown(Keys.S);
        /// <summary>
        /// Unified input for going right
        /// </summary>
        public static bool Right => IsKeyDown(Keys.Right) || IsKeyDown(Keys.D);
        /// <summary>
        /// Unified input for doing something
        /// </summary>
        public static bool Action => IsKeyDown(Keys.Space) || IsKeyDown(Keys.Q) || IsKeyDown(Keys.E);
    }
}
