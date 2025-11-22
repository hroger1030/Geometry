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
    public class Cube : I3d, IEquatable<Cube>
    {
        public readonly static Cube UnitCube = new(0f, 0f, 0f, 1f, 1f, 1f);

        public float X1 { get; set; }

        public float X2 { get; set; }

        public float Y1 { get; set; }

        public float Y2 { get; set; }

        public float Z1 { get; set; }

        public float Z2 { get; set; }

        /// <summary>
        /// Returns a C object coresponding to the 3d coordinates of a corner of the cube object
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

        public float Volume => Math.Abs(X2 - X1) * Math.Abs(Y2 - Y1) * Math.Abs(Z2 - Z1);

        public float SurfaceArea
        {
            get
            {
                float x = Math.Abs(X2 - X1);
                float y = Math.Abs(Y2 - Y1);
                float z = Math.Abs(Z2 - Z1);

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

        public bool Contains(Point3 p)
        {
            return Contains(p.X, p.Y, p.Z);
        }

        public bool Contains(float x, float y, float z)
        {
            return (X1 < x && X2 > x) && (Y1 < y && Y2 > y) && (Z1 < z && Z2 > z);
        }

        public bool Contains(Cube c)
        {
            if (!c.Contains(X1, Y1, Z1)) return false;
            if (!c.Contains(X1, Y1, Z2)) return false;
            if (!c.Contains(X1, Y2, Z2)) return false;
            if (!c.Contains(X2, Y2, Z2)) return false;
            if (!c.Contains(X2, Y2, Z1)) return false;
            if (!c.Contains(X2, Y1, Z1)) return false;
            if (!c.Contains(X2, Y1, Z2)) return false;
            if (!c.Contains(X1, Y2, Z1)) return false;

            return false;
        }

        public bool Intersects(Cube c)
        {
            if (!c.Contains(X1, Y1, Z1)) return true;
            if (!c.Contains(X1, Y1, Z2)) return true;
            if (!c.Contains(X1, Y2, Z2)) return true;
            if (!c.Contains(X2, Y2, Z2)) return true;
            if (!c.Contains(X2, Y2, Z1)) return true;
            if (!c.Contains(X2, Y1, Z1)) return true;
            if (!c.Contains(X2, Y1, Z2)) return true;
            if (!c.Contains(X1, Y2, Z1)) return true;

            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            var new_obj = (Cube)obj;
            return Equals(new_obj);
        }

        public bool Equals(Cube c)
        {
            if (X1 != c.X1) return false;
            if (X2 != c.X2) return false;
            if (Y1 != c.Y1) return false;
            if (Y2 != c.Y2) return false;
            if (Z1 != c.Z1) return false;
            if (Z2 != c.Z2) return false;

            return true;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X1, X2, Y1, Y2, Z1, Z2);
        }
    }
}