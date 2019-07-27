using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    public static class Misc
    {
        public static float d2f(double input)
        {
            float result = Convert.ToSingle(input);
            if (float.IsPositiveInfinity(result))
                result = float.MaxValue;
            else if (float.IsNegativeInfinity(result))
                result = float.MinValue;
            return result;
        }

        public static int f2i(float input) => (int)Math.Round(input);
        public static int d2i(double input) => f2i(d2f(input));
        public static float i2f(int input) => input;
        public static double i2d(int input) => input;
        public static double f2d(float input) => input;
    }
}
