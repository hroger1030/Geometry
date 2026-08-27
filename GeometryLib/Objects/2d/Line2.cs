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
    public readonly struct Line2 : I1d, IEquatable<Line2>
    {
        public Point2 Point1 { get; init; }

        public Point2 Point2 { get; init; }

        public float Length => Point1.DistanceTo(Point2);

        public Line2(float p1x, float p1y, float p2x, float p2y) : this(new Point2(p1x, p1y), new Point2(p2x, p2y)) { }

        public Line2(Point2 p1, Point2 p2)
        {
            Point1 = p1;
            Point2 = p2;
        }

        public override bool Equals(object obj)
        {
            return obj is Line2 other && Equals(other);
        }

        public bool Equals(Line2 l)
        {
            return Point1.Equals(l.Point1) && Point2.Equals(l.Point2);
        }

        public static bool operator ==(Line2 a, Line2 b) => a.Equals(b);

        public static bool operator !=(Line2 a, Line2 b) => !a.Equals(b);

        public override int GetHashCode()
        {
            return HashCode.Combine(Point1, Point2);
        }

        public override string ToString()
        {
            return $"Point1: {Point1}, Point2: {Point2}";
        }
    }
}
