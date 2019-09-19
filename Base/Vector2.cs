using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    /// <summary>
    /// Class for a 2-Dimensional Vector
    /// </summary>
    public class Vector2
    {
        /// <summary>
        /// A new Vector with a value of zero
        /// </summary>
        public static readonly Vector2 Zero = new Vector2(Point.Empty);
        double x_unchecked = 0;
        double y_unchecked = 0;
        public object Tag;
        void check()
        {
            if (!bounds.IsEmpty)
            {
                if (bounds_wrap)
                {
                    if (!(bounds.X == 0 && bounds.Width == 0))
                    {
                        if (bounds.Width == 0)
                            x_unchecked = bounds.X;
                        else
                        {
                            if (bounds.Width < 0)
                                throw new ArgumentException("bounds.Width must be greater than or equal to 0");
                            while (x_unchecked > bounds.X + bounds.Width)
                                x_unchecked -= bounds.Width;
                            while (x_unchecked < bounds.X)
                                x_unchecked += bounds.Width;
                        }
                    }
                    if (!(bounds.Y == 0 && bounds.Height == 0))
                    {
                        if (bounds.Height == 0)
                            y_unchecked = bounds.Y;
                        else
                        {
                            if (bounds.Height < 0)
                                throw new ArgumentException("bounds.Height must be greater than or equal to 0");
                            while (y_unchecked > bounds.Y + bounds.Height)
                                y_unchecked -= bounds.Height;
                            while (y_unchecked < bounds.Y)
                                y_unchecked += bounds.Height;
                        }
                    }
                }
                else
                {
                    x_unchecked = Math.Min(Math.Max(x_unchecked, bounds.X), bounds.X + bounds.Width);
                    y_unchecked = Math.Min(Math.Max(y_unchecked, bounds.Y), bounds.Y + bounds.Height);
                }
            }
        }
        /// <summary>
        /// X-Coordinate of the Vector
        /// </summary>
        public double X
        {
            get {
                check();
                return x_unchecked;
            }
            set {
                x_unchecked = value;
                check();
            }
        }
        /// <summary>
        /// Y-Coordinate of the Vector
        /// </summary>
        public double Y
        {
            get {
                check();
                return y_unchecked;
            }
            set {
                y_unchecked = value;
                check();
            }
        }
        /// <summary>
        /// Bounds of the Vector, set both values for a axis to zero to ignore it
        /// </summary>
        public Rectangle bounds;
        /// <summary>
        /// Set to true if you want the Vector to wrap instead of 'cutting' when reaching the bound 
        /// </summary>
        public bool bounds_wrap = false;
        /// <summary>
        /// Create a new Vector
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public Vector2(double x = 0, double y = 0)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Create a new Vector
        /// </summary>
        /// <param name="from">Point to copy data from</param>
        public Vector2(Point from)
        {
            X = from.X;
            Y = from.Y;
        }

        /// <summary>
        /// Create a new Vector
        /// </summary>
        /// <param name="from">Point to copy data from</param>
        public Vector2(PointF from)
        {
            X = from.X;
            Y = from.Y;
        }

        /// <summary>
        /// Copy data from the Vector to a new one
        /// </summary>
        /// <param name="from">Vector to copy data from</param>
        /// <param name="useProperties">Set to true to copy bounds etc</param>
        public Vector2(Vector2 from, bool useProperties = false)
        {
            X = from.X;
            Y = from.Y;
            if (useProperties)
            {
                Tag = from.Tag;
                bounds = from.bounds;
                bounds_wrap = from.bounds_wrap;
            }
        }

        /// <summary>
        /// Copy the Vectors axis to a point
        /// </summary>
        /// <returns>The new Point</returns>
        public Point toPoint() => new Point(Misc.d2i(X), Misc.d2i(Y));
        /// <summary>
        /// Copy the Vectors axis to a point
        /// </summary>
        /// <returns>The new Point</returns>
        public PointF toPointF() => new PointF(Misc.d2f(X), Misc.d2f(Y));
        /// <summary>
        /// Get the squared distance between this Vector and the other
        /// </summary>
        /// <param name="other">The other Vector</param>
        /// <returns>Distance</returns>
        public double distanceFromSquared(Vector2 other) => Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2);
        /// <summary>
        /// Get the distance between this Vector and the other
        /// </summary>
        /// <param name="other">The other Vector</param>
        /// <returns>Distance</returns>
        public double distanceFrom(Vector2 other) => Math.Sqrt(distanceFromSquared(other));
        /// <summary>
        /// Provided for compatibility with some methods for other Vector implementations
        /// </summary>
        public double magnitude { get { return distanceFrom(Zero); } }
        /// <summary>
        /// Provided for compatibility with some methods for other Vector implementations
        /// </summary>
        public double sqrMagnitude { get { return distanceFromSquared(Zero); } }
        /// <summary>
        /// Move the Vector in the direction
        /// </summary>
        /// <param name="radians">The angle in radians</param>
        /// <param name="distance">Distance to move the Vector</param>
        public void moveInDirection(double radians = 0, double distance = 1)
        {
            X += Math.Cos(radians) * distance;
            Y += Math.Sin(radians) * distance;
        }

        /// <summary>
        /// Get the angle inbetween the X-Axis and a line between two poins
        /// </summary>
        /// <param name="other">The other point for the line</param>
        /// <returns>Angle in Radians</returns>
        public double getDirection(Vector2 other) => Math.Atan((other.X - X) / (other.Y - Y));

        /// <summary>
        /// Move the Vector towards the other Vector
        /// </summary>
        /// <param name="other">The other Vector</param>
        /// <param name="distance">The distance to move</param>
        /// <param name="stopAtTarget">Whether to stop at the target or to go through it</param>
        public void moveTowards(Vector2 other, double distance = 1, bool stopAtTarget = true)
        {
            double dist = distanceFrom(other);
            if (stopAtTarget & distance >= dist)
            {
                X = other.X;
                Y = other.Y;
            }
            else
            {
                double k = distance / dist;
                double localX = other.X - X;
                double localY = other.Y - Y;
                X = localX * k + X;
                Y = localY * k + Y;
            }
        }

        Vector2 addTag(object Tag) { this.Tag = Tag; return this; }
        Vector2 addBounds(Rectangle bounds) { this.bounds = bounds; return this; }
        Vector2 addBoundsW(bool bounds_wrap) { this.bounds_wrap = bounds_wrap; return this; }
        Vector2 addData(object Tag, Rectangle bounds, bool bounds_wrap) => addTag(Tag).addBounds(bounds).addBoundsW(bounds_wrap);

        public override string ToString() => "{X=" + X.ToString() + ", Y=" + Y.ToString() + "}";
        public static Vector2 operator +(Vector2 left, Vector2 right) => new Vector2(left.X + right.X, left.Y + right.Y).addData(left.Tag, left.bounds, left.bounds_wrap);
        public static Vector2 operator +(Vector2 left, Point right) => new Vector2(left.X + right.X, left.Y + right.Y).addData(left.Tag, left.bounds, left.bounds_wrap);
        public static Vector2 operator +(Vector2 left, PointF right) => new Vector2(left.X + right.X, left.Y + right.Y).addData(left.Tag, left.bounds, left.bounds_wrap);
        public static Vector2 operator -(Vector2 left, Vector2 right) => new Vector2(left.X - right.X, left.Y - right.Y).addData(left.Tag, left.bounds, left.bounds_wrap);
        public static Vector2 operator -(Vector2 left, Point right) => new Vector2(left.X - right.X, left.Y - right.Y).addData(left.Tag, left.bounds, left.bounds_wrap);
        public static Vector2 operator -(Vector2 left, PointF right) => new Vector2(left.X - right.X, left.Y - right.Y).addData(left.Tag, left.bounds, left.bounds_wrap);
        public static Vector2 operator *(Vector2 left, Vector2 right) => new Vector2(left.X * right.X, left.Y * right.Y).addData(left.Tag, left.bounds, left.bounds_wrap);
        public static Vector2 operator *(Vector2 left, Point right) => new Vector2(left.X * right.X, left.Y * right.Y);
        public static Vector2 operator *(Vector2 left, PointF right) => new Vector2(left.X * right.X, left.Y * right.Y);
        public static Vector2 operator /(Vector2 left, Vector2 right) => new Vector2(left.X / right.X, left.Y / right.Y);
        public static Vector2 operator /(Vector2 left, Point right) => new Vector2(left.X / right.X, left.Y / right.Y);
        public static Vector2 operator /(Vector2 left, PointF right) => new Vector2(left.X / right.X, left.Y / right.Y);
        public static Vector2 operator ^(Vector2 left, double right) => new Vector2(Math.Pow(left.X, right), Math.Pow(left.Y, right));
    }
}
