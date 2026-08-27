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
    public readonly struct Point3 : IEquatable<Point3>
    {
        public float X { get; init; }

        public float Y { get; init; }

        public float Z { get; init; }

        public Point3() : this(0f, 0f, 0f) { }

        public Point3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point3(Point3 p)
        {
            X = p.X;
            Y = p.Y;
            Z = p.Z;
        }

        public Point3(Point2 p)
        {
            X = p.X;
            Y = p.Y;
            Z = 0;
        }

        public static Point3 operator +(Point3 p1, Point3 p2)
        {
            return new Point3(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
        }

        public static Point3 operator -(Point3 p1, Point3 p2)
        {
            return new Point3(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
        }

        public override bool Equals(object obj)
        {
            return obj is Point3 other && Equals(other);
        }

        public bool Equals(Point3 p)
        {
            return X == p.X && Y == p.Y && Z == p.Z;
        }

        public static bool operator ==(Point3 a, Point3 b) => a.Equals(b);

        public static bool operator !=(Point3 a, Point3 b) => !a.Equals(b);

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }
    }
}
