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
    [DebuggerDisplay("({X},{Y})")]
    public class Point2 : I1d, IEquatable<Point2>
    {
        public static readonly Point2 ZERO = new(0, 0);
        public static readonly Point2 ONE = new(1, 1);

        public float X { get; set; }

        public float Y { get; set; }

        public float Length => 0f;

        public Point2() : this(0f, 0f) { }

        public Point2(double x, double y) : this((float)x, (float)y) { }

        public Point2(int x, int y) : this((float)x, (float)y) { }

        public Point2(short x, short y) : this((float)x, (float)y) { }

        public Point2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float DistanceTo(Point2 p)
        {
            if (p == null) throw new ArgumentNullException(nameof(p));

            return DistanceTo(this, p);
        }

        public static float DistanceTo(Point2 p1, Point2 p2)
        {
            if (p1 == null) throw new ArgumentNullException(nameof(p1));
            if (p2 == null) throw new ArgumentNullException(nameof(p2));

            return (float)Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }

        public static Point2 operator +(Point2 p1, Vector2 v1)
        {
            if (p1 == null) throw new ArgumentNullException(nameof(p1));
            if (v1 == null) throw new ArgumentNullException(nameof(v1));

            var output = new Point2
            {
                X = p1.X + v1.X,
                Y = p1.Y + v1.Y
            };

            return output;
        }

        public static Point2 operator -(Point2 p1, Vector2 v1)
        {
            if (p1 == null) throw new ArgumentNullException(nameof(p1));
            if (v1 == null) throw new ArgumentNullException(nameof(v1));

            var output = new Point2
            {
                X = p1.X - v1.X,
                Y = p1.Y - v1.Y
            };

            return output;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            var new_obj = (Point2)obj;
            return Equals(new_obj);
        }

        public bool Equals(Point2 p)
        {
            if (p == null)
                return false;

            return ((X == p.X) && (Y == p.Y));
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}
