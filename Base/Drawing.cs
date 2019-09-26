using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    public class GraphicsWrapper : IDisposable
    {
        BufferedGraphics _g;
        Color backColor;
        Rectangle targetSize;
        public readonly Graphics g;
        /// <summary>
        /// Wrap the Graphics object with these excellent High-Quality functions
        /// </summary>
        /// <param name="g">The Graphics-object to wrap</param>
        /// <param name="targetSize">The size of the device the Graphics are drawn to</param>
        public GraphicsWrapper(Graphics g, Color backColor, Rectangle targetSize)
        {
            _g = BufferedGraphicsManager.Current.Allocate(g ?? throw new ArgumentNullException(nameof(g)), targetSize);
            this.g = _g.Graphics;
            this.backColor = backColor;
            this.targetSize = targetSize;
        }

        /// <summary>
        /// Draw a string with the given size
        /// </summary>
        /// <param name="s">The string to draw</param>
        /// <param name="size">The font size of the string</param>
        /// <param name="location">The location to draw the string at</param>
        /// <param name="brush">The brush to draw the string with</param>
        /// <param name="isLocationCentered">Set to true if you want to draw the string around instead of left-down from the location</param>
        public void DrawSizedString(string s, int size, PointF location, Brush brush, bool transform = true, bool isLocationCentered = false)
        {
            SmoothingMode tmpS = g.SmoothingMode;
            InterpolationMode tmpI = g.InterpolationMode;
            PixelOffsetMode tmpP = g.PixelOffsetMode;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            SizeF sLen = g.MeasureString(s, new Font("Tahoma", size));
            RectangleF rectf = new RectangleF(location, sLen);
            if (isLocationCentered)
                rectf = new RectangleF(rectf.X - rectf.Width / 2, rectf.Y - rectf.Height / 2, rectf.Width, rectf.Height);
            if (transform)
                rectf = w2s(rectf);
            g.DrawString(s, new Font("Tahoma", size), brush, rectf);
            g.PixelOffsetMode = tmpP;
            g.InterpolationMode = tmpI;
            g.SmoothingMode = tmpS;
        }

        /// <summary>
        /// Draws a rectangle
        /// </summary>
        /// <param name="rectangle">Use the PointF/SizeF Constructor as it is much more logical</param>
        /// <param name="color">The color of the rectangle</param>
        /// <param name="centered">Whether the rectangle should be drawn centered rather than down-left</param>
        /// <param name="filled">Whether the rectangle should be filled</param>
        /// <param name="unfilledLineSize">The size of the lines used when not filling</param>
        public void DrawRectangle(RectangleF rectangle, Color color, bool centered = true, bool transform = true, bool filled = true, int unfilledLineSize = 1)
        {
            RectangleF r = rectangle;
            if (transform)
                r = w2s(r);
            Brush b = new SolidBrush(color);
            if (centered)
            {
                r = new RectangleF(new PointF(r.X - r.Width / 2, r.Y - r.Height / 2), r.Size);
            }
            if (filled)
                g.FillRectangle(b, r);
            else
                g.DrawRectangle(new Pen(b, unfilledLineSize), new Rectangle(Misc.f2i(r.X), Misc.f2i(r.Y), Misc.f2i(r.Width), Misc.f2i(r.Height)));
        }

        public void Clear() => g.Clear(backColor);

        public void Dispose()
        {
            g.Flush();
            _g.Render();
            g.Dispose();
            _g.Dispose();
        }

        public RectangleF w2s(RectangleF from) => new RectangleF(w2s(from.Location), from.Size);
        public PointF w2s(PointF from) => new PointF(from.X, targetSize.Height - from.Y);
    }
}
