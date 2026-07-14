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
    public class Capsule : IEquatable<Capsule>
    {
        public Point3 PointA { get; set; }

        public Point3 PointB { get; set; }

        public float Radius { get; set; }

        public Capsule(Point3 pointA, Point3 pointB, float radius)
        {
            ArgumentNullException.ThrowIfNull(pointA);
            ArgumentNullException.ThrowIfNull(pointB);

            if (radius < 0f)
                throw new ArgumentOutOfRangeException(nameof(radius), "Radius must be non-negative.");

            PointA = pointA;
            PointB = pointB;
            Radius = radius;
        }

        public bool Contains(Point3 point)
        {
            ArgumentNullException.ThrowIfNull(point);

            var ab = new Vector3(PointA, PointB);
            if (ab.Length() == 0f)
            {
                return new Sphere(PointA, Radius).Contains(point);
            }

            var ap = new Vector3(PointA, point);
            float t = (ab.X * ap.X + ab.Y * ap.Y + ab.Z * ap.Z) / (ab.X * ab.X + ab.Y * ab.Y + ab.Z * ab.Z);
            t = Math.Clamp(t, 0f, 1f);

            var closest = new Point3(
                PointA.X + ab.X * t,
                PointA.Y + ab.Y * t,
                PointA.Z + ab.Z * t);

            return new Sphere(closest, Radius).Contains(point);
        }

        public bool Intersects(Sphere sphere)
        {
            ArgumentNullException.ThrowIfNull(sphere);
            var ab = new Vector3(PointA, PointB);
            if (ab.Length() == 0f)
            {
                return new Sphere(PointA, Radius).Intersects(sphere);
            }

            var ac = new Vector3(PointA, sphere.Center);
            float t = (ab.X * ac.X + ab.Y * ac.Y + ab.Z * ac.Z) / (ab.X * ab.X + ab.Y * ab.Y + ab.Z * ab.Z);
            t = Math.Clamp(t, 0f, 1f);

            var closest = new Point3(
                PointA.X + ab.X * t,
                PointA.Y + ab.Y * t,
                PointA.Z + ab.Z * t);

            return new Sphere(closest, Radius).Intersects(sphere);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            return Equals((Capsule)obj);
        }

        public bool Equals(Capsule other)
        {
            if (other is null) return false;
            return PointA.Equals(other.PointA)
                && PointB.Equals(other.PointB)
                && Radius == other.Radius;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PointA, PointB, Radius);
        }
    }
}
