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

namespace Geometry
{
    public class Triangle : I2d, IEquatable<Triangle>
    {
        public enum Type
        {
            Equilateral,
            Isosceles,
            Scalene,
        }

        public Point2 A { get; set; }

        public Point2 B { get; set; }

        public Point2 C { get; set; }

        public float Perimeter
        {
            get { return A.DistanceTo(B) + B.DistanceTo(C) + C.DistanceTo(A); }
        }

        public float Area
        {
            get
            {
                float buffer;

                // using heron's formula
                var a = A.DistanceTo(B);
                var b = B.DistanceTo(C);
                var c = C.DistanceTo(A);

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

        public Type TriangleType
        {
            get
            {
                var a = A.DistanceTo(B);
                var b = B.DistanceTo(C);
                var c = C.DistanceTo(A);

                if (a == b && b == c)
                    return Type.Equilateral;

                if (a == b || b == c || c == a)
                    return Type.Isosceles;

                return Type.Scalene;
            }
        }

        public Triangle(Point2 p1, Point2 p2, Point2 p3)
        {
            if (p1 == null) throw new ArgumentNullException(nameof(p1));
            if (p2 == null) throw new ArgumentNullException(nameof(p2));
            if (p3 == null) throw new ArgumentNullException(nameof(p3));

            if (p1 == (p2) || p2 == (p3) || p3 == (p1))
                throw new ArgumentException("All points must be distinct points with seperate locations");

            A = p1;
            B = p2;
            C = p3;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            var new_obj = (Triangle)obj;
            return Equals(new_obj);
        }

        public bool Equals(Triangle t)
        {
            return (A.Equals(t.A) && B.Equals(t.B) && C.Equals(t.C));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (A.GetHashCode() * 31) ^ (B.GetHashCode() * 691 ^ (C.GetHashCode() * 17));
            }
        }
    }
}
