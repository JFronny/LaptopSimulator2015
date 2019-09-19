using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    public static class Drawing
    {
        /// <summary>
        /// Draw a string with the given size
        /// </summary>
        /// <param name="g">The graphics object to draw the string on</param>
        /// <param name="s">The string to draw</param>
        /// <param name="size">The font size of the string</param>
        /// <param name="location">The location to draw the string at</param>
        /// <param name="brush">The brush to draw the string with</param>
        /// <param name="isLocationCentered">Set to true if you want to draw the string around instead of left-down from the location</param>
        public static void DrawSizedString(Graphics g, string s, int size, PointF location, Brush brush, bool isLocationCentered = false)
        {
            SmoothingMode tmpS = g.SmoothingMode;
            InterpolationMode tmpI = g.InterpolationMode;
            PixelOffsetMode tmpP = g.PixelOffsetMode;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            SizeF sLen = g.MeasureString(s, new Font("Tahoma", size));
            RectangleF rectf;
            if (isLocationCentered)
                rectf = new RectangleF(location.X - sLen.Width / 2, location.Y - sLen.Height / 2, sLen.Width, sLen.Height);
            else
                rectf = new RectangleF(location, sLen);
            g.DrawString(s, new Font("Tahoma", size), brush, rectf);
            g.PixelOffsetMode = tmpP;
            g.InterpolationMode = tmpI;
            g.SmoothingMode = tmpS;
        }
    }
}
