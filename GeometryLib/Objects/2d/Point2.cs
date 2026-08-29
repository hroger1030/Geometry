/*
The MIT License (MIT)

Copyright (c) 2017 Roger Hill

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

namespace Geometry
{
    public readonly struct Point2 : IEquatable<Point2>
    {
        /// <summary>The origin point (0, 0).</summary>
        public static readonly Point2 ZERO = new(0, 0);
        /// <summary>The point (1, 1).</summary>
        public static readonly Point2 ONE = new(1, 1);

        public float X { get; init; }

        public float Y { get; init; }

        /// <summary>
        /// Creates the origin point (0, 0).
        /// </summary>
        public Point2() : this(0f, 0f) { }

        /// <summary>
        /// Creates a point from double coordinates, narrowed to <see cref="float"/>.
        /// </summary>
        public Point2(double x, double y) : this((float)x, (float)y) { }

        /// <summary>
        /// Creates a point from integer coordinates.
        /// </summary>
        public Point2(int x, int y) : this((float)x, (float)y) { }

        /// <summary>
        /// Creates a point from short coordinates.
        /// </summary>
        public Point2(short x, short y) : this((float)x, (float)y) { }

        /// <summary>
        /// Creates a point from explicit X and Y coordinates.
        /// </summary>
        public Point2(float x, float y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Returns the Euclidean distance from this point to <paramref name="p"/>.
        /// </summary>
        public float DistanceTo(Point2 p)
        {
            return DistanceTo(this, p);
        }

        /// <summary>
        /// Returns the Euclidean distance between two points.
        /// </summary>
        public static float DistanceTo(Point2 p1, Point2 p2)
        {
            return MathF.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }

        /// <summary>
        /// Translates a point by a vector, returning the displaced point.
        /// </summary>
        public static Point2 operator +(Point2 p1, Vector2 v1)
        {

            return new Point2(p1.X + v1.X, p1.Y + v1.Y);
        }

        /// <summary>
        /// Translates a point by the negation of a vector, returning the displaced point.
        /// </summary>
        public static Point2 operator -(Point2 p1, Vector2 v1)
        {

            return new Point2(p1.X - v1.X, p1.Y - v1.Y);
        }

        /// <summary>
        /// Returns true if <paramref name="obj"/> is a <see cref="Point2"/> at the same coordinates.
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is Point2 other && Equals(other);
        }

        /// <summary>
        /// Returns true if the other point has exactly equal X and Y coordinates (no tolerance).
        /// </summary>
        public bool Equals(Point2 p)
        {
            return X == p.X && Y == p.Y;
        }

        /// <summary>
        /// Returns true if both points share the same coordinates.
        /// </summary>
        public static bool operator ==(Point2 a, Point2 b) => a.Equals(b);

        /// <summary>
        /// Returns true if the points differ in either coordinate.
        /// </summary>
        public static bool operator !=(Point2 a, Point2 b) => !a.Equals(b);

        /// <summary>
        /// Returns a hash code derived from the X and Y coordinates.
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}
