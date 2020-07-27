using System.Diagnostics;

namespace Geometry
{
    [DebuggerDisplay("Point3 {X},{Y},{Z}")]
    public class Point3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Point3() : this(0f, 0f, 0f) { }

        public Point3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Point3 operator +(Point3 p1, Point3 p2)
        {
            Point3 output = new Point3
            {
                X = p1.X + p2.X,
                Y = p1.Y + p2.Y,
                Z = p1.Z + p2.Z
            };

            return output;
        }

        public static Point3 operator -(Point3 p1, Point3 p2)
        {
            Point3 output = new Point3
            {
                X = p1.X - p2.X,
                Y = p1.Y - p2.Y,
                Z = p1.Z - p2.Z
            };

            return output;
        }

        public static Point3 operator *(Point3 p1, Point3 p2)
        {
            Point3 output = new Point3
            {
                X = p1.X * p2.X,
                Y = p1.Y * p2.Y,
                Z = p1.Z * p2.Z
            };

            return output;
        }

        public static Point3 operator /(Point3 p1, Point3 p2)
        {
            Point3 output = new Point3
            {
                X = p1.X / p2.X,
                Y = p1.Y / p2.Y,
                Z = p1.Z / p2.Z
            };

            return output;
        }

        public static bool operator ==(Point3 p1, Point3 p2)
        {
            if (ReferenceEquals(p1, p2))
                return true;

            // If one is null, but not both, return false.
            if ((p1 is null) || (p2 is null))
                return false;

            return ((p1.X == p2.X) && (p1.Y == p2.Y) && (p1.Z == p2.Z));
        }

        public static bool operator !=(Point3 v1, Point3 v2)
        {
            return !(v1 == v2);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            var new_obj = (Point3)obj;
            return Equals(new_obj);
        }

        public bool Equals(Point3 obj)
        {
            if (X != obj.X)
                return false;

            if (Y != obj.Y)
                return false;

            if (Z != obj.Z)
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 1607) ^ (Y.GetHashCode() * 1033) ^ (Z.GetHashCode() * 59);
            }
        }

        public override string ToString()
        {
            return $"Point3({X},{Y},{Z})";
        }
    }
}
