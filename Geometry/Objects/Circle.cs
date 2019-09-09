using System;
using System.Diagnostics;

namespace Geometry
{
    [DebuggerDisplay("Circle {_Center}, r{_Radius}")]
    public class Circle
    {
        public static readonly Circle UnitCircle = new Circle();

        public Point2 Center { get; set; }
        public float Radius { get; set; }

        public float Left
        {
            get { return Center.X - Radius; }
        }
        public float Right
        {
            get { return Center.X + Radius; }
        }
        public float Top
        {
            get { return Center.Y - Radius; }
        }
        public float Bottom
        {
            get { return Center.Y + Radius; }
        }
        public float Area
        {
            get { return (float)(Math.PI * Radius * Radius); }
        }
        public float Circumfrence
        {
            get { return (float)(Math.PI * 2 * Radius); }
        }

        public Circle() : this(0f, 0f, 1f) { }

        public Circle(Point2 position) : this(position, 1f) { }

        public Circle(float radius) : this(0f, 0f, radius) { }

        public Circle(float x, float y, float radius) : this(new Point2(x, y), radius) { }

        public Circle(Circle circle) : this(new Point2(circle.Center.X, circle.Center.Y), circle.Radius) { }

        public Circle(Point2 position, float radius)
        {
            Center = position;
            Radius = radius;
        }

        /// <summary>
        /// Checks to see if circles are intersecting. 
        /// Tangent circles will return true.
        /// </summary>
        public bool Intersects(Circle circle)
        {
            float distance_x = circle.Center.X - Center.X;
            float distance_y = circle.Center.Y - Center.Y;
            float sum_radius = Radius + circle.Radius;

            if ((sum_radius * sum_radius) < (distance_x * distance_x + distance_y * distance_y))
                return false;
            else
                return true;
        }

        public bool Intersects(Rectangle rectangle)
        {
            if (rectangle.Contains(Center))
                return true;

            if (this.Contains(rectangle.TopLeftConrner) || this.Contains(rectangle.TopRightConrner) ||
                this.Contains(rectangle.BottomLeftConrner) || this.Contains(rectangle.BottomRightCorner))
                return true;

            return false;
        }

        public bool Contains(Point2 point)
        {
            float distance_x = point.X - this.Center.X;
            float distance_y = point.Y - this.Center.Y;

            if ((Radius * Radius) < Math.Abs(distance_x * distance_x + distance_y * distance_y))
                return false;
            else
                return true;
        }

        public bool Contains(Rectangle rectangle)
        {
            // A circle contains a rectangle if it contains all of the rectangle's corners.
            return this.Contains(rectangle.TopLeftConrner) &&
            this.Contains(rectangle.TopRightConrner) &&
            this.Contains(rectangle.BottomRightCorner) &&
            this.Contains(rectangle.BottomLeftConrner);
        }

        public static Circle operator +(Circle circle, Vector2 vector)
        {
            return new Circle(circle.Center.X + vector.X, circle.Center.Y + vector.Y, circle.Radius);
        }

        public static Circle operator -(Circle circle, Vector2 vector)
        {
            return new Circle(circle.Center.X - vector.X, circle.Center.Y - vector.Y, circle.Radius);
        }

        public static Circle operator *(Circle circle, float scalar)
        {
            return new Circle(circle.Center.X, circle.Center.Y, circle.Radius * scalar);
        }

        public static Circle operator /(Circle circle, float scalar)
        {
            return new Circle(circle.Center.X, circle.Center.Y, circle.Radius / scalar);
        }

        public static bool operator ==(Circle c1, Circle c2)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(c1, c2))
                return true;

            // If one is null, but not both, return false.
            if (((object)c1 == null) || ((object)c2 == null))
                return false;

            return (c1.Center.X == c2.Center.X) && (c1.Center.Y == c2.Center.Y) && (c1.Radius == c2.Radius);
        }

        public static bool operator !=(Circle a, Circle b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Center.GetHashCode() * 19) ^ (Radius.GetHashCode() * 691);
            }
        }

        public override string ToString()
        {
            return $"CenterPoint {Center}, Radius: {Radius}";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            var new_obj = (Circle)obj;
            return Equals(new_obj);
        }

        public bool Equals(Circle other)
        {
            return (Center == other.Center && Radius == other.Radius);
        }
    }
}
