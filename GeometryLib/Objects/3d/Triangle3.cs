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
    public class Triangle3 : IEquatable<Triangle3>
    {
        public Point3 A { get; set; }

        public Point3 B { get; set; }

        public Point3 C { get; set; }

        public float Perimeter =>
            Vector3.DistanceTo(new Vector3(A), new Vector3(B)) +
            Vector3.DistanceTo(new Vector3(B), new Vector3(C)) +
            Vector3.DistanceTo(new Vector3(C), new Vector3(A));

        public float Area
        {
            get
            {
                var ab = new Vector3(A, B);
                var ac = new Vector3(A, C);
                var cross = Vector3.Cross(ab, ac);
                return 0.5f * cross.Length();
            }
        }

        public Triangle3(Point3 a, Point3 b, Point3 c)
        {
            ArgumentNullException.ThrowIfNull(a);
            ArgumentNullException.ThrowIfNull(b);
            ArgumentNullException.ThrowIfNull(c);

            if (a.Equals(b) || b.Equals(c) || c.Equals(a))
                throw new ArgumentException("All points must be distinct points with separate locations");

            A = a;
            B = b;
            C = c;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            return Equals((Triangle3)obj);
        }

        public bool Equals(Triangle3 other)
        {
            return other is not null && A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(A, B, C);
        }
    }
}
