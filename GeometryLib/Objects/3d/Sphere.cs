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
    public readonly struct Sphere : I3d, IEquatable<Sphere>
    {
        public Point3 Center { get; init; }

        public float Radius { get; init; }

        /// <summary>The volume of the sphere (4/3 * PI * r^3).</summary>
        public float Volume => (4f / 3f) * MathF.PI * Radius * Radius * Radius;

        /// <summary>The surface area of the sphere (4 * PI * r^2).</summary>
        public float SurfaceArea => 4f * MathF.PI * Radius * Radius;

        /// <summary>
        /// Creates a sphere from a center and radius.
        /// Throws <see cref="ArgumentOutOfRangeException"/> if <paramref name="radius"/> is zero or negative.
        /// </summary>
        public Sphere(Point3 center, float radius)
        {
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radius, 0f);

            Center = center;
            Radius = radius;
        }

        /// <summary>
        /// Returns true if <paramref name="point"/> lies inside or on this sphere.
        /// </summary>
        public bool Contains(Point3 point)
        {
            float dx = point.X - Center.X;
            float dy = point.Y - Center.Y;
            float dz = point.Z - Center.Z;

            return (dx * dx + dy * dy + dz * dz) <= (Radius * Radius);
        }

        /// <summary>
        /// Returns true if this sphere overlaps or touches <paramref name="other"/> (distance between centers &lt;= sum of radii).
        /// </summary>
        public bool Intersects(Sphere other)
        {
            float dx = other.Center.X - Center.X;
            float dy = other.Center.Y - Center.Y;
            float dz = other.Center.Z - Center.Z;
            float radiusSum = Radius + other.Radius;

            return (dx * dx + dy * dy + dz * dz) <= (radiusSum * radiusSum);
        }

        /// <summary>
        /// Gets whether or not a specified <see cref="Cube"/> intersects with this <see cref="Sphere"/>.
        /// Delegates to <see cref="Cube.Intersects(Sphere)"/> so both directions share one algorithm.
        /// </summary>
        public bool Intersects(Cube c)
        {
            return c.Intersects(this);
        }

        /// <summary>
        /// Returns true if all eight corners of <paramref name="c"/> lie inside or on this sphere (i.e. the cube is fully enclosed).
        /// </summary>
        public bool Contains(Cube c)
        {
            for (int i = 0; i < 8; i++)
            {
                if (!Contains(c[i]))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Returns true if <paramref name="obj"/> is a <see cref="Sphere"/> with the same center and radius.
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is Sphere other && Equals(other);
        }

        /// <summary>
        /// Returns true if the other sphere has the same center and radius (no tolerance).
        /// </summary>
        public bool Equals(Sphere other)
        {
            return Center.Equals(other.Center) && Radius.Equals(other.Radius);
        }

        /// <summary>
        /// Returns true if both spheres have the same center and radius.
        /// </summary>
        public static bool operator ==(Sphere a, Sphere b) => a.Equals(b);

        /// <summary>
        /// Returns true if the spheres differ in center or radius.
        /// </summary>
        public static bool operator !=(Sphere a, Sphere b) => !a.Equals(b);

        /// <summary>
        /// Returns a hash code derived from the center and radius.
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(Center, Radius);
        }
    }
}
