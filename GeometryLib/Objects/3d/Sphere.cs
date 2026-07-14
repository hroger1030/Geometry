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
    public class Sphere : I3d, IEquatable<Sphere>
    {
        public Point3 Center { get; set; }

        public float Radius { get; set; }

        public float Volume => (4f / 3f) * MathF.PI * Radius * Radius * Radius;

        public float SurfaceArea => 4f * MathF.PI * Radius * Radius;

        public Sphere(Point3 center, float radius)
        {
            ArgumentNullException.ThrowIfNull(center);

            if (radius <= 0f)
                throw new ArgumentOutOfRangeException(nameof(radius), "Radius must be greater than zero.");

            Center = center;
            Radius = radius;
        }

        public bool Contains(Point3 point)
        {
            ArgumentNullException.ThrowIfNull(point);

            float dx = point.X - Center.X;
            float dy = point.Y - Center.Y;
            float dz = point.Z - Center.Z;

            return (dx * dx + dy * dy + dz * dz) <= (Radius * Radius);
        }

        public bool Intersects(Sphere other)
        {
            ArgumentNullException.ThrowIfNull(other);

            float dx = other.Center.X - Center.X;
            float dy = other.Center.Y - Center.Y;
            float dz = other.Center.Z - Center.Z;
            float radiusSum = Radius + other.Radius;

            return (dx * dx + dy * dy + dz * dz) <= (radiusSum * radiusSum);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            return Equals((Sphere)obj);
        }

        public bool Equals(Sphere other)
        {
            if (other is null) return false;
            return Center.Equals(other.Center) && Radius.Equals(other.Radius);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Center, Radius);
        }
    }
}
