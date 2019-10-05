using System;
using System.Drawing;

namespace Base
{
    public class Rect
    {
        public Vector2 Location;
        public Vector2 Size;
        bool centered;

        /// <summary>
        /// Create a rect from the provided data
        /// </summary>
        /// <param name="Location">Position</param>
        /// <param name="Size">Rect's size</param>
        /// <param name="centered">Whether the Rect should be created top-right or around the Location</param>
        public Rect(Vector2 Location, Vector2 Size, bool centered = false) : this(Location.X, Location.Y, Size.X, Size.Y, centered) { }

        /// <summary>
        /// Create a rect from the provided data
        /// </summary>
        /// <param name="X">X in world-coordinates</param>
        /// <param name="Y">Y in world-coordinates</param>
        /// <param name="Width">Width</param>
        /// <param name="Height">Height</param>
        /// <param name="centered">Whether the Rect should be created top-right or around the Location</param>
        public Rect(double X, double Y, double Width, double Height, bool centered = false)
        {
            Location = new Vector2(X, Y);
            Size = new Vector2(Width, Height);
            this.centered = centered;
            if (this.centered)
            {
                //Location -= Size / 2;
                Location.X -= Size.X / 2;
                Location.Y += Size.Y / 2;
            }
        }

        /// <summary>
        /// Copies the Rect's data
        /// </summary>
        /// <param name="rect"></param>
        public Rect(Rect rect)
        {
            Location = rect.Location;
            Size = rect.Size;
            centered = rect.centered;
        }
        /// <summary>
        /// Copies the Rect's data
        /// </summary>
        /// <param name="rect"></param>
        public Rect(Rectangle rect) : this(rect.X, rect.Y, rect.Width, rect.Height, false) { }
        /// <summary>
        /// Copies the Rect's data
        /// </summary>
        /// <param name="rect"></param>
        public Rect(RectangleF rect) : this(rect.X, rect.Y, rect.Width, rect.Height, false) { }

        public double X
        {
            get => Location.X;
            set => Location.X = value;
        }

        public double Y
        {
            get => Location.Y;
            set => Location.Y = value;
        }

        public double Width
        {
            get => Size.X;
            set {
                if (centered)
                {
                    double tmp = Size.X - value;
                    Size.X = value;
                    Location.X += tmp / 2;
                }
                else
                    Size.X = value;
            }
        }

        public double Height
        {
            get => Size.Y;
            set {
                if (centered)
                {
                    double tmp = Size.Y - value;
                    Size.Y = value;
                    Location.Y += tmp / 2;
                }
                else
                    Size.Y = value;
            }
        }

        public double Bottom => Location.Y;
        public double Top => Location.Y + Size.Y;
        public double Left => Location.X;
        public double Right => Location.X + Size.X;
        public Vector2 bottomLeftPoint => new Vector2(Location);
        public Vector2 bottomRightPoint => new Vector2(Location.X, Location.Y + Size.Y);
        public Vector2 topLeftPoint => new Vector2(Location.X + Size.X, Location.Y);
        public Vector2 topRightPoint => Location + Size;
        public bool doOverlap(Rect other) => (Left <= other.Right && other.Left <= Right) || (Left >= other.Right && other.Left >= Right);
        public Rectangle toRectangle() => new Rectangle(Misc.d2i(X), Misc.d2i(Y), Misc.d2i(Width), Misc.d2i(Height));
        public RectangleF toRectangleF() => new RectangleF(Misc.d2f(X), Misc.d2f(Y), Misc.d2f(Width), Misc.d2f(Height));
    }
}
