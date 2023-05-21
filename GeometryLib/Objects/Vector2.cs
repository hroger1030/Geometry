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
    public class Vector2 : IEquatable<Vector2>
    {
        public static readonly Vector2 Zero = new(0, 0);
        public static readonly Vector2 One = new(1, 1);

        public float X { get; set; }

        public float Y { get; set; }

        public Vector2() : this(0, 0) { }

        public Vector2(Vector2 v1) : this(v1.X, v1.Y) { }

        public Vector2(Point2 p1) : this(p1.X, p1.Y) { }

        public Vector2(double rotation) : this((float)Math.Cos(rotation), (float)Math.Sin(rotation)) { }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            if (v1 == null) throw new ArgumentNullException(nameof(v1));
            if (v2 == null) throw new ArgumentNullException(nameof(v2));

            return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            if (v1 == null) throw new ArgumentNullException(nameof(v1));
            if (v2 == null) throw new ArgumentNullException(nameof(v2));

            return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Vector2 operator *(Vector2 v, float scale)
        {
            if (v == null) throw new ArgumentNullException(nameof(v));

            return new Vector2(v.X * scale, v.Y * scale);
        }

        public static Vector2 operator /(Vector2 v, float scale)
        {
            if (v == null) throw new ArgumentNullException(nameof(v));
            if (scale == 0f) throw new DivideByZeroException(nameof(scale));

            return new Vector2(v.X / scale, v.Y / scale);
        }

        public static Vector2 Normalize(Vector2 v)
        {
            if (v == null) throw new ArgumentNullException(nameof(v));

            var length = v.Length();

            if (length == 0)
                throw new DivideByZeroException("Cannot normalize a vector when it's magnitude is zero");

            float inverse = 1f / length;

            return new Vector2
            (
                v.X * inverse,
                v.Y * inverse
            );
        }

        public void Normalize()
        {
            float length = Length();

            if (length == 0)
                throw new DivideByZeroException("Cannot normalize a vector when it's magnitude is zero");

            float inverse = 1f / length;

            X *= inverse;
            Y *= inverse;
        }

        public float VectorToRotation()
        {
            return (float)Math.Atan2(Y, X);
        }

        public float Length()
        {
            return (float)Math.Sqrt((X * X) + (Y * Y));
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            var new_obj = (Vector2)obj;
            return Equals(new_obj);
        }

        public bool Equals(Vector2 v)
        {
            if (v == null)
                return false;

            return v.X == X && v.Y == Y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return X.GetHashCode() ^ (Y.GetHashCode() * 17);
            }
        }
    }
}