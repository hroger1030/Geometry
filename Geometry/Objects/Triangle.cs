using System;

namespace Geometry
{
    public class Triangle
    {
        public enum Type
        {
            Equilateral,
            Isosceles,
            Scalene,
        }

        public Point2 Point1 { get; set; }
        public Point2 Point2 { get; set; }
        public Point2 Point3 { get; set; }

        public float Perimeter
        {
            get { return Point1.DistanceTo(Point2) + Point2.DistanceTo(Point3) + Point3.DistanceTo(Point1); }
        }

        public float Area
        {
            get
            {
                float buffer;

                // using heron's formula
                var a = Point1.DistanceTo(Point2);
                var b = Point2.DistanceTo(Point3);
                var c = Point3.DistanceTo(Point1);

                // arrange a > b > c
                if (b > a)
                {
                    buffer = a;
                    a = b;
                    b = buffer;
                }

                if (c > a)
                {
                    buffer = a;
                    a = c;
                    c = buffer;
                }

                if (c > b)
                {
                    buffer = b;
                    b = c;
                    c = buffer;
                }

                buffer = (a + (b + c)) * (c - (a - b)) * (c + (a - b)) * (a + (b - c));
                return (float)Math.Pow(buffer, 0.5) * 0.25f;
            }
        }

        public Triangle(Point2 point1, Point2 point2, Point2 point3)
        {
            if (point1 == null)
                throw new ArgumentException("Point1 object cannot be null");

            if (point2 == null)
                throw new ArgumentException("Point2 object cannot be null");

            if (point3 == null)
                throw new ArgumentException("Point3 object cannot be null");

            if (point1.Equals(point2) || point2.Equals(point3) || point3.Equals(point1))
                throw new ArgumentException("All points must be distinct points with seperate locations");

            Point1 = point1;
            Point2 = point2;
            Point3 = point3;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Point1.GetHashCode() * 31) ^ (Point2.GetHashCode() * 691 ^ (Point2.GetHashCode() * 17));
            }
        }

        public override string ToString()
        {
            return $"Point1: {Point1}, Point2: {Point2}, Point3: {Point3}";
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            var new_obj = (Triangle)obj;
            return Equals(new_obj);
        }

        public bool Equals(Triangle other)
        {
            return (Point1.Equals(other.Point1) && Point2.Equals(other.Point2) && Point3.Equals(other.Point3));
        }
    }
}
