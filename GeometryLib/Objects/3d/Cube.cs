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
    public readonly struct Cube : I3d, IEquatable<Cube>
    {
        /// <summary>A 1x1x1 cube with one corner at the origin and the opposite corner at (1, 1, 1).</summary>
        public readonly static Cube UnitCube = new(0f, 0f, 0f, 1f, 1f, 1f);

        public float X1 { get; init; }

        public float X2 { get; init; }

        public float Y1 { get; init; }

        public float Y2 { get; init; }

        public float Z1 { get; init; }

        public float Z2 { get; init; }

        /// <summary>
        /// Returns the <see cref="Point3"/> for one of the cube's eight corners. Indices 0-3 are the Z1 face,
        /// indices 4-7 the Z2 face. Throws <see cref="IndexOutOfRangeException"/> for indices outside 0-7.
        /// </summary>
        public Point3 this[int i]
        {
            get
            {
                switch (i)
                {
                    // top face
                    case 0: return new Point3(X1, Y1, Z1);
                    case 1: return new Point3(X1, Y2, Z1);
                    case 2: return new Point3(X2, Y1, Z1);
                    case 3: return new Point3(X2, Y2, Z1);

                    // bottom face
                    case 4: return new Point3(X1, Y1, Z2);
                    case 5: return new Point3(X1, Y2, Z2);
                    case 6: return new Point3(X2, Y1, Z2);
                    case 7: return new Point3(X2, Y2, Z2);

                    default:
                        throw new IndexOutOfRangeException($"Unknown index {i}");
                }
            }
        }

        /// <summary>The extent along X (X2 - X1); may be negative if the corners are not min/max ordered.</summary>
        public float Width => X2 - X1;

        /// <summary>The extent along Y (Y2 - Y1); may be negative if the corners are not min/max ordered.</summary>
        public float Height => Y2 - Y1;

        /// <summary>The extent along Z (Z2 - Z1); may be negative if the corners are not min/max ordered.</summary>
        public float Depth => Z2 - Z1;

        /// <summary>
        /// A <see cref="Point3"/> located in the center of this <see cref="Cube"/>.
        /// </summary>
        public Point3 Center => new((X1 + X2) / 2, (Y1 + Y2) / 2, (Z1 + Z2) / 2);

        /// <summary>The volume of the cube (absolute width * height * depth).</summary>
        public float Volume => MathF.Abs(X2 - X1) * MathF.Abs(Y2 - Y1) * MathF.Abs(Z2 - Z1);

        /// <summary>The total surface area of the cube's six faces.</summary>
        public float SurfaceArea
        {
            get
            {
                float x = MathF.Abs(X2 - X1);
                float y = MathF.Abs(Y2 - Y1);
                float z = MathF.Abs(Z2 - Z1);

                return (x * y * 2) + (y * z * 2) + (z * x * 2);
            }
        }

        /// <summary>
        /// Creates a cube spanning two opposite corners.
        /// </summary>
        public Cube(Point3 p1, Point3 p2) : this(p1.X, p1.Y, p1.Z, p2.X, p2.Y, p2.Z) { }

        /// <summary>
        /// Creates a cube from the raw coordinates of two opposite corners. The Contains/Intersects
        /// tests assume X1 &lt;= X2, Y1 &lt;= Y2 and Z1 &lt;= Z2.
        /// </summary>
        public Cube(float x1, float y1, float z1, float x2, float y2, float z2)
        {
            X1 = x1;
            Y1 = y1;
            Z1 = z1;
            X2 = x2;
            Y2 = y2;
            Z2 = z2;
        }

        /// <summary>
        /// Creates a copy of an existing cube.
        /// </summary>
        public Cube(Cube cube)
        {
            X1 = cube.X1;
            Y1 = cube.Y1;
            Z1 = cube.Z1;
            X2 = cube.X2;
            Y2 = cube.Y2;
            Z2 = cube.Z2;
        }

        /// <summary>
        /// Returns true if point <paramref name="p"/> lies inside or on the faces of this cube.
        /// </summary>
        public bool Contains(Point3 p)
        {
            return Contains(p.X, p.Y, p.Z);
        }

        /// <summary>
        /// Returns true if (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>) lies inside or on the faces of this cube.
        /// </summary>
        public bool Contains(float x, float y, float z)
        {
            return (X1 <= x && X2 >= x) && (Y1 <= y && Y2 >= y) && (Z1 <= z && Z2 >= z);
        }

        /// <summary>
        /// Returns true if every corner of <paramref name="c"/> lies inside or on this cube (i.e. the cube is fully enclosed).
        /// </summary>
        public bool Contains(Cube c)
        {
            return Contains(c.X1, c.Y1, c.Z1) &&
                   Contains(c.X1, c.Y1, c.Z2) &&
                   Contains(c.X1, c.Y2, c.Z2) &&
                   Contains(c.X2, c.Y2, c.Z2) &&
                   Contains(c.X2, c.Y2, c.Z1) &&
                   Contains(c.X2, c.Y1, c.Z1) &&
                   Contains(c.X2, c.Y1, c.Z2) &&
                   Contains(c.X1, c.Y2, c.Z1);
        }

        /// <summary>
        /// Returns true if this cube overlaps or touches <paramref name="c"/> on all three axes.
        /// </summary>
        public bool Intersects(Cube c)
        {
            return X1 <= c.X2 && X2 >= c.X1 &&
                   Y1 <= c.Y2 && Y2 >= c.Y1 &&
                   Z1 <= c.Z2 && Z2 >= c.Z1;
        }

        /// <summary>
        /// Gets whether or not a specified <see cref="Sphere"/> intersects with this <see cref="Cube"/>.
        /// </summary>
        public bool Intersects(Sphere s)
        {
            float closestX = Math.Clamp(s.Center.X, X1, X2);
            float closestY = Math.Clamp(s.Center.Y, Y1, Y2);
            float closestZ = Math.Clamp(s.Center.Z, Z1, Z2);

            float distanceX = s.Center.X - closestX;
            float distanceY = s.Center.Y - closestY;
            float distanceZ = s.Center.Z - closestZ;

            return (distanceX * distanceX + distanceY * distanceY + distanceZ * distanceZ) <= (s.Radius * s.Radius);
        }

        /// <summary>
        /// Creates a new <see cref="Cube"/> that is scaled up from the X1,Y1,Z1 corner. Calling this with a scale of
        /// 2 will double the width, height and depth while keeping X1,Y1,Z1 fixed.
        /// </summary>
        public static Cube operator *(Cube c, float scale)
        {
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(scale, 0f);

            return new Cube(c.X1, c.Y1, c.Z1, c.X1 + (c.Width * scale), c.Y1 + (c.Height * scale), c.Z1 + (c.Depth * scale));
        }

        /// <summary>
        /// Returns true if <paramref name="obj"/> is a <see cref="Cube"/> with the same six coordinates.
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is Cube other && Equals(other);
        }

        /// <summary>
        /// Returns true if the other cube has the same six corner coordinates (no tolerance).
        /// </summary>
        public bool Equals(Cube c)
        {
            return X1 == c.X1 && X2 == c.X2 && Y1 == c.Y1 && Y2 == c.Y2 && Z1 == c.Z1 && Z2 == c.Z2;
        }

        /// <summary>
        /// Returns true if both cubes have the same six coordinates.
        /// </summary>
        public static bool operator ==(Cube a, Cube b) => a.Equals(b);

        /// <summary>
        /// Returns true if the cubes differ in any coordinate.
        /// </summary>
        public static bool operator !=(Cube a, Cube b) => !a.Equals(b);

        /// <summary>
        /// Returns a hash code derived from the six corner coordinates.
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(X1, X2, Y1, Y2, Z1, Z2);
        }
    }
}