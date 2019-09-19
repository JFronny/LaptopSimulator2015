using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    public static class Misc
    {
        /// <summary>
        /// Call to signal that the player failed
        /// </summary>
        public static Action closeGameWindow;
        /// <summary>
        /// Convert a double to a float
        /// </summary>
        /// <param name="input">Value to be converted</param>
        /// <returns>Converted value</returns>
        public static float d2f(double input)
        {
            float result = Convert.ToSingle(input);
            if (float.IsPositiveInfinity(result))
                result = float.MaxValue;
            else if (float.IsNegativeInfinity(result))
                result = float.MinValue;
            return result;
        }

        /// <summary>
        /// Convert a float to an int
        /// </summary>
        /// <param name="input">Value to be converted</param>
        /// <returns>Converted value</returns>
        public static int f2i(float input) => (int)Math.Round(input);
        /// <summary>
        /// Convert a double to an int
        /// </summary>
        /// <param name="input">Value to be converted</param>
        /// <returns>Converted value</returns>
        public static int d2i(double input) => f2i(d2f(input));
        /// <summary>
        /// Convert an int to a float
        /// </summary>
        /// <param name="input">Value to be converted</param>
        /// <returns>Converted value</returns>
        public static float i2f(int input) => input;
        /// <summary>
        /// Convert an int to a double
        /// </summary>
        /// <param name="input">Value to be converted</param>
        /// <returns>Converted value</returns>
        public static double i2d(int input) => input;
        /// <summary>
        /// Convert a float to a double
        /// </summary>
        /// <param name="input">Value to be converted</param>
        /// <returns>Converted value</returns>
        public static double f2d(float input) => input;
        /// <summary>
        /// Convert a value in radians to a value in degrees
        /// </summary>
        /// <param name="input">Value to be converted</param>
        /// <returns>Converted value</returns>
        public static double rad2deg(double input) => (360 * input) / (2 * Math.PI);
        /// <summary>
        /// Convert a value in degrees to a value in radians
        /// </summary>
        /// <param name="input">Value to be converted</param>
        /// <returns>Converted value</returns>
        public static double deg2rad(double input) => ((2 * Math.PI) * input) / 360;
        /// <summary>
        /// Maps a number from one range of numbers to another
        /// </summary>
        /// <param name="originalStart">Start of the range the original number is in</param>
        /// <param name="originalEnd">End of the range the original number is in</param>
        /// <param name="newStart">Start of the range the new number is in</param>
        /// <param name="newEnd">End of the range the new number is in</param>
        /// <param name="value">Value to be mapped</param>
        /// <returns>Mapped value</returns>
        public static double map(double originalStart, double originalEnd, double newStart, double newEnd, double value)
        {
            double scale = (newEnd - newStart) / (originalEnd - originalStart);
            return newStart + ((value - originalStart) * scale);
        }
    }
}
