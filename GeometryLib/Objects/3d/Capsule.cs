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
    public readonly struct Capsule : IEquatable<Capsule>
    {
        public Point3 PointA { get; init; }

        public Point3 PointB { get; init; }

        public float Radius { get; init; }

        public Capsule(Point3 pointA, Point3 pointB, float radius)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(radius);

            PointA = pointA;
            PointB = pointB;
            Radius = radius;
        }

        public bool Contains(Point3 point)
        {

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

            var ab = new Vector3(PointA, PointB);

            if (ab.Length() == 0f)
                return new Sphere(PointA, Radius).Intersects(sphere);

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
            return obj is Capsule other && Equals(other);
        }

        public bool Equals(Capsule other)
        {
            return PointA.Equals(other.PointA)
                && PointB.Equals(other.PointB)
                && Radius == other.Radius;
        }

        public static bool operator ==(Capsule a, Capsule b) => a.Equals(b);

        public static bool operator !=(Capsule a, Capsule b) => !a.Equals(b);

        public override int GetHashCode()
        {
            return HashCode.Combine(PointA, PointB, Radius);
        }
    }
}
