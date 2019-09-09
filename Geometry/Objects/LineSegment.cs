using System;
using System.Diagnostics;

namespace Geometry
{
    public class LineSegment
    {
        public Point2 Point1 { get; set; }
        public Point2 Point2 { get; set; }

        public float Length
        {
            get { return Point1.DistanceTo(Point2); }
        }

        public LineSegment(float p1x, float p1y, float p2x, float p2y) : this(new Point2(p1x, p1y), new Point2(p2x, p2y)) { }

        public LineSegment(Point2 point1, Point2 point2)
        {
            if (point1 == null)
                throw new ArgumentException("Point1 object cannot be null");

            if (point2 == null)
                throw new ArgumentException("Point2 object cannot be null");

            Point1 = point1;
            Point2 = point2;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Point1.GetHashCode() * 37) ^ (Point2.GetHashCode() * 691);
            }
        }

        public override string ToString()
        {
            return $"Point1: {Point1}, Point2: {Point2}";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            var new_obj = (LineSegment)obj;
            return Equals(new_obj);
        }

        public bool Equals(LineSegment other)
        {
            return (Point1 == other.Point1 && Point2 == other.Point2);
        }
    }
}
