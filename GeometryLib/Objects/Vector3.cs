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
    public class Vector3 : IEquatable<Vector3>
    {
        public static readonly Vector3 Zero = new(0, 0, 0);
        public static readonly Vector3 One = new(1, 1, 1);

        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public Vector3() : this(0, 0, 0) { }

        public Vector3(Vector3 v1) : this(v1.X, v1.Y, v1.Z) { }

        public Vector3(Point3 p1) : this(p1.X, p1.Y, p1.Z) { }

        public Vector3(Point3 head, Point3 destination) : this(destination.X - head.X, destination.Y - head.Y, destination.Z - head.Z) { }

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public static Vector3 operator *(Vector3 v1, float s2)
        {
            return new Vector3(v1.X * s2, v1.Y * s2, v1.Z * s2);
        }

        public static Vector3 operator /(Vector3 v1, float s2)
        {
            return new Vector3(v1.X / s2, v1.Y / s2, v1.Z / s2);
        }

        public static Vector3 Normalize(Vector3 v1)
        {
            var length = v1.Length();

            if (length == 0)
                throw new DivideByZeroException("Cannot normalize a vector when it's magnitude is zero");

            float inverse = 1f / length;

            return new Vector3
            (
                v1.X * inverse,
                v1.Y * inverse,
                v1.Z * inverse
            );
        }

        public static float DistanceTo(Vector3 v1, Vector3 v2)
        {
            float delta_x = v1.X - v2.X;
            float delta_y = v1.Y - v2.Y;
            float delta_z = v1.Z - v2.Z;

            return (float)Math.Sqrt((delta_x * delta_x) + (delta_y * delta_y) + (delta_z * delta_z));
        }

        public float DistanceTo(Vector3 other)
        {
            return DistanceTo(this, other);
        }

        public float Length()
        {
            return (float)Math.Sqrt((X * X) + (Y * Y) + (Z * Z));
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            var v = (Vector3)obj;
            return Equals(v);
        }

        public bool Equals(Vector3 v)
        {
            return v.X == X && v.Y == Y && v.Z == Z;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 1361) ^ (Y.GetHashCode() * 3449) ^ (Z.GetHashCode() * 47);
            }
        }
    }
}