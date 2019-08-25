using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    public class Vector2
    {

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
                    if (bounds.X != 0 & bounds.Width < 0)
                        throw new ArgumentException("bounds.Width must be greater than or equal to 0");
                    while (bounds.X != 0 & x_unchecked > bounds.X + bounds.Width)
                        x_unchecked -= bounds.Width;
                    while (bounds.X != 0 & x_unchecked < bounds.X)
                        x_unchecked += bounds.Width;
                    if (bounds.Y != 0 & bounds.Height < 0)
                        throw new ArgumentException("bounds.Height must be greater than or equal to 0");
                    while (bounds.Y != 0 & y_unchecked > bounds.Y + bounds.Height)
                        y_unchecked -= bounds.Height;
                    while (bounds.Y != 0 & y_unchecked < bounds.Y)
                        y_unchecked += bounds.Height;
                }
                else
                {
                    x_unchecked = Math.Min(Math.Max(x_unchecked, bounds.X), bounds.X + bounds.Width);
                    y_unchecked = Math.Min(Math.Max(y_unchecked, bounds.Y), bounds.Y + bounds.Height);
                }
            }
        }
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
        public Rectangle bounds;
        public bool bounds_wrap = false;
        public Vector2(double x = 0, double y = 0)
        {
            X = x;
            Y = y;
        }

        public Vector2(Point from)
        {
            X = from.X;
            Y = from.Y;
        }

        public Vector2(PointF from)
        {
            X = from.X;
            Y = from.Y;
        }

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

        public Point toPoint() => new Point((int)Math.Round(X), (int)Math.Round(Y));
        public PointF toPointF() => new PointF(Misc.d2f(X), Misc.d2f(Y));
        public double distanceFromSquared(Vector2 other) => Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2);
        public double distanceFrom(Vector2 other) => Math.Sqrt(distanceFromSquared(other));
        public double magnitude { get { return distanceFrom(Zero); } }
        public double sqrMagnitude { get { return distanceFromSquared(Zero); } }
        public void moveInDirection(double radians = 0, double distance = 1)
        {
            X += Math.Cos(radians) * distance;
            Y += Math.Sin(radians) * distance;
        }

        public double getDirection(Vector2 other) => Math.Atan((other.X - X) / (other.Y - Y));

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

        public Vector2 addTag(object Tag) { this.Tag = Tag; return this; }
        public Vector2 addBounds(Rectangle bounds) { this.bounds = bounds; return this; }
        public Vector2 addBoundsW(bool bounds_wrap) { this.bounds_wrap = bounds_wrap; return this; }

        public override string ToString() => "{X=" + X.ToString() + ", Y=" + Y.ToString() + "}";
        public static Vector2 operator +(Vector2 left, Vector2 right) => new Vector2(left.X + right.X, left.Y + right.Y).addTag(left.Tag).addBounds(left.bounds).addBoundsW(left.bounds_wrap);
        public static Vector2 operator +(Vector2 left, Point right) => new Vector2(left.X + right.X, left.Y + right.Y).addTag(left.Tag).addBounds(left.bounds).addBoundsW(left.bounds_wrap);
        public static Vector2 operator +(Vector2 left, PointF right) => new Vector2(left.X + right.X, left.Y + right.Y).addTag(left.Tag).addBounds(left.bounds).addBoundsW(left.bounds_wrap);
        public static Vector2 operator -(Vector2 left, Vector2 right) => new Vector2(left.X - right.X, left.Y - right.Y).addTag(left.Tag).addBounds(left.bounds).addBoundsW(left.bounds_wrap);
        public static Vector2 operator -(Vector2 left, Point right) => new Vector2(left.X - right.X, left.Y - right.Y).addTag(left.Tag).addBounds(left.bounds).addBoundsW(left.bounds_wrap);
        public static Vector2 operator -(Vector2 left, PointF right) => new Vector2(left.X - right.X, left.Y - right.Y).addTag(left.Tag).addBounds(left.bounds).addBoundsW(left.bounds_wrap);
        public static Vector2 operator *(Vector2 left, Vector2 right) => new Vector2(left.X * right.X, left.Y * right.Y).addTag(left.Tag).addBounds(left.bounds).addBoundsW(left.bounds_wrap);
        public static Vector2 operator *(Vector2 left, Point right) => new Vector2(left.X * right.X, left.Y * right.Y);
        public static Vector2 operator *(Vector2 left, PointF right) => new Vector2(left.X * right.X, left.Y * right.Y);
        public static Vector2 operator /(Vector2 left, Vector2 right) => new Vector2(left.X / right.X, left.Y / right.Y);
        public static Vector2 operator /(Vector2 left, Point right) => new Vector2(left.X / right.X, left.Y / right.Y);
        public static Vector2 operator /(Vector2 left, PointF right) => new Vector2(left.X / right.X, left.Y / right.Y);
        public static Vector2 operator ^(Vector2 left, double right) => new Vector2(Math.Pow(left.X, right), Math.Pow(left.Y, right));
    }
}
