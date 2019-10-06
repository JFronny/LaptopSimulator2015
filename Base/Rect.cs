using System;
using System.Drawing;

namespace Base
{
    public struct Rect
    {
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
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;
            if (centered)
            {
                this.X -= Width / 2;
                this.Y += Height / 2;
            }
        }

        /// <summary>
        /// Copies the Rect's data
        /// </summary>
        /// <param name="rect"></param>
        public Rect(Rect rect)
        {
            X = rect.X;
            Y = rect.Y;
            Width = rect.Width;
            Height = rect.Height;
        }
        /// <summary>
        /// Copies the Rect's data
        /// </summary>
        /// <param name="rect"></param>
        public Rect(Rectangle rect, bool transform = false) : this(rect.X, rect.Y, rect.Width, rect.Height, false) { }
        /// <summary>
        /// Copies the Rect's data
        /// </summary>
        /// <param name="rect"></param>
        public Rect(RectangleF rect, bool transform = false) : this(rect.X, rect.Y, rect.Width, rect.Height, false) { }

        public double X;
        public double Y;
        public double Width;
        public double Height;
        public Vector2 Location => new Vector2(X, Y);
        public Vector2 Size => new Vector2(Width, Height);
        public double Bottom => Y;
        public double Top => Y + Height;
        public double Left => X;
        public double Right => X + Width;
        public Vector2 bottomLeftPoint => new Vector2(Left, Bottom);
        public Vector2 topLeftPoint => new Vector2(Left, Top);
        public Vector2 bottomRightPoint => new Vector2(Right, Bottom);
        public Vector2 topRightPoint => new Vector2(Right, Top);
        public bool doOverlap(Rect other) => (Left <= other.Right && other.Left <= Right) || (Left >= other.Right && other.Left >= Right);
        public Rectangle toRectangle() => new Rectangle(Misc.d2i(X), Misc.d2i(Y), Misc.d2i(Width), Misc.d2i(Height));
        public RectangleF toRectangleF() => new RectangleF(Misc.d2f(X), Misc.d2f(Y), Misc.d2f(Width), Misc.d2f(Height));
    }
}
