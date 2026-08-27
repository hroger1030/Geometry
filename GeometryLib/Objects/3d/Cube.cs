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
        public readonly static Cube UnitCube = new(0f, 0f, 0f, 1f, 1f, 1f);

        public float X1 { get; init; }

        public float X2 { get; init; }

        public float Y1 { get; init; }

        public float Y2 { get; init; }

        public float Z1 { get; init; }

        public float Z2 { get; init; }

        /// <summary>
        /// Returns a C object corresponding to the 3d coordinates of a corner of the cube object
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

        public float Width => X2 - X1;

        public float Height => Y2 - Y1;

        public float Depth => Z2 - Z1;

        /// <summary>
        /// A <see cref="Point3"/> located in the center of this <see cref="Cube"/>.
        /// </summary>
        public Point3 Center => new((X1 + X2) / 2, (Y1 + Y2) / 2, (Z1 + Z2) / 2);

        public float Volume => MathF.Abs(X2 - X1) * MathF.Abs(Y2 - Y1) * MathF.Abs(Z2 - Z1);

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

        public Cube(Point3 p1, Point3 p2) : this(p1.X, p1.Y, p1.Z, p2.X, p2.Y, p2.Z) { }

        public Cube(float x1, float y1, float z1, float x2, float y2, float z2)
        {
            X1 = x1;
            Y1 = y1;
            Z1 = z1;
            X2 = x2;
            Y2 = y2;
            Z2 = z2;
        }

        public Cube(Cube cube)
        {
            X1 = cube.X1;
            Y1 = cube.Y1;
            Z1 = cube.Z1;
            X2 = cube.X2;
            Y2 = cube.Y2;
            Z2 = cube.Z2;
        }

        public bool Contains(Point3 p)
        {
            return Contains(p.X, p.Y, p.Z);
        }

        public bool Contains(float x, float y, float z)
        {
            return (X1 <= x && X2 >= x) && (Y1 <= y && Y2 >= y) && (Z1 <= z && Z2 >= z);
        }

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

        public override bool Equals(object obj)
        {
            return obj is Cube other && Equals(other);
        }

        public bool Equals(Cube c)
        {
            return X1 == c.X1 && X2 == c.X2 && Y1 == c.Y1 && Y2 == c.Y2 && Z1 == c.Z1 && Z2 == c.Z2;
        }

        public static bool operator ==(Cube a, Cube b) => a.Equals(b);

        public static bool operator !=(Cube a, Cube b) => !a.Equals(b);

        public override int GetHashCode()
        {
            return HashCode.Combine(X1, X2, Y1, Y2, Z1, Z2);
        }
    }
}