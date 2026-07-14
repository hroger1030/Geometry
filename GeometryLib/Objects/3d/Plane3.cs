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
    public class Plane3 : IEquatable<Plane3>
    {
        public Vector3 Normal { get; set; }
        public float D { get; set; }

        public Plane3(Vector3 normal, float d)
        {
            ArgumentNullException.ThrowIfNull(normal);

            if (normal.X == 0f && normal.Y == 0f && normal.Z == 0f)
                throw new ArgumentException("Normal must be non-zero.", nameof(normal));

            Normal = normal;
            D = d;
        }

        public float DistanceTo(Point3 point)
        {
            ArgumentNullException.ThrowIfNull(point);

            float length = MathF.Sqrt(Normal.X * Normal.X + Normal.Y * Normal.Y + Normal.Z * Normal.Z);

            return (Normal.X * point.X + Normal.Y * point.Y + Normal.Z * point.Z + D) / length;
        }

        public Plane3 Normalize()
        {
            float length = MathF.Sqrt(Normal.X * Normal.X + Normal.Y * Normal.Y + Normal.Z * Normal.Z);
            var normalized = new Vector3(Normal.X / length, Normal.Y / length, Normal.Z / length);

            return new Plane3(normalized, D / length);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            return Equals((Plane3)obj);
        }

        public bool Equals(Plane3 other)
        {
            if (other is null) return false;

            return Normal.Equals(other.Normal) && D.Equals(other.D);
        }

        public override int GetHashCode() => HashCode.Combine(Normal, D);
    }
}
