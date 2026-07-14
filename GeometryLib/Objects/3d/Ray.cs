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
    public class Ray : IEquatable<Ray>
    {
        public static readonly Plane3 ZeroPlane = new(new Vector3(0f, 0f, 1f), 0f);

        public Point3 Origin { get; set; }

        public Vector3 Direction { get; set; }

        public Ray(Point3 origin, Vector3 direction)
        {
            ArgumentNullException.ThrowIfNull(origin);
            ArgumentNullException.ThrowIfNull(direction);

            if (direction.Length() == 0f)
                throw new ArgumentException("Direction vector must be non-zero.", nameof(direction));

            Origin = origin;
            Direction = Vector3.Normalize(direction);
        }

        public Point3 PointAt(float distance)
        {
            return new Point3(
                Origin.X + Direction.X * distance,
                Origin.Y + Direction.Y * distance,
                Origin.Z + Direction.Z * distance);
        }

        public bool Intersects(Sphere sphere, out float distance)
        {
            ArgumentNullException.ThrowIfNull(sphere);

            float dx = Origin.X - sphere.Center.X;
            float dy = Origin.Y - sphere.Center.Y;
            float dz = Origin.Z - sphere.Center.Z;

            float b = dx * Direction.X + dy * Direction.Y + dz * Direction.Z;
            float c = dx * dx + dy * dy + dz * dz - sphere.Radius * sphere.Radius;
            float discriminant = b * b - c;

            if (discriminant < 0f)
            {
                distance = 0f;
                return false;
            }

            float sqrt = MathF.Sqrt(discriminant);
            float t0 = -b - sqrt;
            float t1 = -b + sqrt;

            if (t0 >= 0f)
            {
                distance = t0;
                return true;
            }

            if (t1 >= 0f)
            {
                distance = t1;
                return true;
            }

            distance = 0f;
            return false;
        }

        public bool Intersects(AABB aabb, out float distance)
        {
            ArgumentNullException.ThrowIfNull(aabb);

            float tMin = float.NegativeInfinity;
            float tMax = float.PositiveInfinity;

            if (MathF.Abs(Direction.X) < Constants.FLOAT_ERROR_MARGIN)
            {
                if (Origin.X < aabb.Min.X || Origin.X > aabb.Max.X)
                {
                    distance = 0f;
                    return false;
                }
            }
            else
            {
                float t1 = (aabb.Min.X - Origin.X) / Direction.X;
                float t2 = (aabb.Max.X - Origin.X) / Direction.X;

                if (t1 > t2)
                {
                    float temp = t1;
                    t1 = t2;
                    t2 = temp;
                }

                tMin = MathF.Max(tMin, t1);
                tMax = MathF.Min(tMax, t2);

                if (tMin > tMax)
                {
                    distance = 0f;
                    return false;
                }
            }

            if (MathF.Abs(Direction.Y) < Constants.FLOAT_ERROR_MARGIN)
            {
                if (Origin.Y < aabb.Min.Y || Origin.Y > aabb.Max.Y)
                {
                    distance = 0f;
                    return false;
                }
            }
            else
            {
                float t1 = (aabb.Min.Y - Origin.Y) / Direction.Y;
                float t2 = (aabb.Max.Y - Origin.Y) / Direction.Y;

                if (t1 > t2)
                {
                    float temp = t1;
                    t1 = t2;
                    t2 = temp;
                }

                tMin = MathF.Max(tMin, t1);
                tMax = MathF.Min(tMax, t2);

                if (tMin > tMax)
                {
                    distance = 0f;
                    return false;
                }
            }

            if (MathF.Abs(Direction.Z) < Constants.FLOAT_ERROR_MARGIN)
            {
                if (Origin.Z < aabb.Min.Z || Origin.Z > aabb.Max.Z)
                {
                    distance = 0f;
                    return false;
                }
            }
            else
            {
                float t1 = (aabb.Min.Z - Origin.Z) / Direction.Z;
                float t2 = (aabb.Max.Z - Origin.Z) / Direction.Z;

                if (t1 > t2)
                {
                    float temp = t1;
                    t1 = t2;
                    t2 = temp;
                }

                tMin = MathF.Max(tMin, t1);
                tMax = MathF.Min(tMax, t2);

                if (tMin > tMax)
                {
                    distance = 0f;
                    return false;
                }
            }

            distance = MathF.Max(0f, tMin);
            return true;
        }

        public bool Intersects(Cube cube, out float distance)
        {
            ArgumentNullException.ThrowIfNull(cube);

            return Intersects(
                new AABB(new Point3(cube.X1, cube.Y1, cube.Z1), new Point3(cube.X2, cube.Y2, cube.Z2)),
                out distance);
        }

        public bool Intersects(Plane3 plane, out float distance)
        {
            ArgumentNullException.ThrowIfNull(plane);

            float denominator = plane.Normal.X * Direction.X + plane.Normal.Y * Direction.Y + plane.Normal.Z * Direction.Z;

            if (MathF.Abs(denominator) < Constants.FLOAT_ERROR_MARGIN)
            {
                distance = 0f;
                return false;
            }

            float numerator = -(plane.Normal.X * Origin.X + plane.Normal.Y * Origin.Y + plane.Normal.Z * Origin.Z + plane.D);
            distance = numerator / denominator;

            return distance >= 0f;
        }

        private static float Cross(Vector2 a, Vector2 b)
        {
            return a.X * b.Y - a.Y * b.X;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            return Equals((Ray)obj);
        }

        public bool Equals(Ray other)
        {
            if (other is null) return false;
            return Origin.Equals(other.Origin) && Direction.Equals(other.Direction);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Origin, Direction);
        }
    }
}
