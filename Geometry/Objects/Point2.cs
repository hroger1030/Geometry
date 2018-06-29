using System;
using System.Diagnostics;

namespace Geometry
{
    [DebuggerDisplay("Point2 ({_X},{_Y})")]
    public class Point2
    {
        public static readonly Point2 ZERO = new Point2(0, 0);
        public static readonly Point2 ONE = new Point2(1, 1);

        protected float _X;
        protected float _Y;

        public float X
        {
            get { return _X; }
            set { _X = value; }
        }
        public float Y
        {
            get { return _Y; }
            set { _Y = value; }
        }

        public Point2() : this(0f, 0f) { }

        public Point2(double x, double y) : this((float)x, (float)y) { }

        public Point2(int x, int y) : this((float)x, (float)y) { }

        public Point2(short x, short y) : this((float)x, (float)y) { }

        public Point2(float x, float y)
        {
            _X = x;
            _Y = y;
        }

        public float DistanceTo(Point2 other)
        {
            return Point2.DistanceTo(this, other);
        }

        public static float DistanceTo(Point2 p1, Point2 p2)
        {
            return (float)Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }

        public static Point2 operator +(Point2 p1, Vector2 v1)
        {
            var output = new Point2();

            output._X = p1._X + v1.X;
            output._Y = p1._Y + v1.Y;

            return output;
        }

        public static Point2 operator -(Point2 p1, Vector2 v1)
        {
            var output = new Point2();

            output._X = p1._X - v1.X;
            output._Y = p1._Y - v1.Y;

            return output;
        }

        public static bool operator ==(Point2 p1, Point2 p2)
        {
            if (ReferenceEquals(p1, p2))
                return true;

            // If one is null, but not both, return false.
            if (((object)p1 == null) || ((object)p2 == null))
                return false;

            return ((p1._X == p2._X) && (p1._Y == p2._Y));
        }

        public static bool operator !=(Point2 p1, Point2 p2)
        {
            return !(p1 == p2);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            var new_obj = (Point2)obj;
            return Equals(new_obj);
        }

        public bool Equals(Point2 obj)
        {
            return ((_X == obj._X) && (_Y == obj._Y));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_X.GetHashCode() * 17) ^ (_X.GetHashCode() * 2011);
            }
        }

        public override string ToString()
        {
            return $"Point2({_X},{_Y})";
        }
    }
}
