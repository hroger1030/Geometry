/*
The MIT License (MIT)

Copyright (c) 2007 Roger Hill

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files 
(the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, 
publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do 
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN 
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Diagnostics;

namespace Geometry
{
    [DebuggerDisplay("({Center.X},{Center.Y},{Radius}r)")]
    public class Circle : I2d, IEquatable<Circle>
    {
        public static readonly Circle UnitCircle = new();

        public Point2 Center { get; set; }

        public float Radius { get; set; }

        public float Left => Center.X - Radius;

        public float Right => Center.X + Radius;

        public float Top => Center.Y - Radius;

        public float Bottom => Center.Y + Radius;

        public float Area => MathF.PI * Radius * Radius;

        public float Circumference => MathF.PI * 2 * Radius;

        public float Diameter => Radius * 2;

        public float Perimeter => Circumference;

        public Circle() : this(0f, 0f, 1f) { }

        public Circle(Point2 position) : this(position.X, position.Y, 1f) { }

        public Circle(float radius) : this(0f, 0f, radius) { }

        public Circle(Circle circle) : this(circle.Center.X, circle.Center.Y, circle.Radius) { }

        public Circle(float x, float y, float radius)
        {
            if (radius <= 0f)
                throw new ArgumentOutOfRangeException(nameof(radius), "Radius must be greater than zero.");

            Center = new Point2(x, y);
            Radius = radius;
        }

        /// <summary>
        /// Checks to see if circles are intersecting. 
        /// Tangent circles will return true.
        /// </summary>
        public bool Intersects(Circle c)
        {
            if (c.Center == Center)
                return true;

            float distance_x = c.Center.X - Center.X;
            float distance_y = c.Center.Y - Center.Y;
            float sum_radius = Radius + c.Radius;

            return ((sum_radius * sum_radius) >= (distance_x * distance_x + distance_y * distance_y));
        }

        public bool Intersects(Rectangle r)
        {
            ArgumentNullException.ThrowIfNull(r);

            float closestX = Math.Clamp(Center.X, r.Left, r.Right);
            float closestY = Math.Clamp(Center.Y, r.Top, r.Bottom);

            float distanceX = Center.X - closestX;
            float distanceY = Center.Y - closestY;

            return (distanceX * distanceX + distanceY * distanceY) <= (Radius * Radius);
        }

        public bool Contains(Point2 p)
        {
            float distance_x = p.X - Center.X;
            float distance_y = p.Y - Center.Y;

            return ((Radius * Radius) >= MathF.Abs(distance_x * distance_x + distance_y * distance_y));
        }

        public bool Contains(Rectangle r)
        {
            if (!Contains(r.TopLeftCorner)) return false;
            if (!Contains(r.TopRightCorner)) return false;
            if (!Contains(r.BottomRightCorner)) return false;
            if (!Contains(r.BottomLeftCorner)) return false;

            return true;
        }

        public static Circle operator +(Circle c, Vector2 v)
        {
            return new Circle(c.Center.X + v.X, c.Center.Y + v.Y, c.Radius);
        }

        public static Circle operator -(Circle c, Vector2 v)
        {
            return new Circle(c.Center.X - v.X, c.Center.Y - v.Y, c.Radius);
        }

        public static Circle operator *(Circle c, float scale)
        {
            if (scale <= 0f)
                throw new ArgumentOutOfRangeException(nameof(scale), "Scale must be greater than zero.");

            return new Circle(c.Center.X, c.Center.Y, c.Radius * scale);
        }

        public static Circle operator /(Circle c, float scale)
        {
            if (scale <= 0f)
                throw new ArgumentOutOfRangeException(nameof(scale), "Scale must be greater than zero.");

            return new Circle(c.Center.X, c.Center.Y, c.Radius / scale);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            var new_obj = (Circle)obj;
            return Equals(new_obj);
        }

        public bool Equals(Circle c)
        {
            return (Center.Equals(c.Center) && Radius.Equals(c.Radius));
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Center, Radius);
        }
    }
}
