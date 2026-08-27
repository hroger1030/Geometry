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
    public readonly struct Vector2 : IEquatable<Vector2>
    {
        public static readonly Vector2 Zero = new(0, 0);
        public static readonly Vector2 One = new(1, 1);

        public float X { get; init; }

        public float Y { get; init; }

        public Vector2() : this(0, 0) { }

        public Vector2(Vector2 v1) : this(v1.X, v1.Y) { }

        public Vector2(Point2 p1) : this(p1.X, p1.Y) { }

        public Vector2(float rotation) : this(MathF.Cos(rotation), MathF.Sin(rotation)) { }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Vector2 operator *(Vector2 v, float scale)
        {
            return new Vector2(v.X * scale, v.Y * scale);
        }

        public static Vector2 operator /(Vector2 v, float scale)
        {
            if (scale == 0f) throw new DivideByZeroException(nameof(scale));

            return new Vector2(v.X / scale, v.Y / scale);
        }

        public static Vector2 Normalize(Vector2 v)
        {
            return v.Normalize();
        }

        /// <summary>
        /// Returns a unit-length copy of this vector. Throws if the vector's magnitude is zero.
        /// </summary>
        public Vector2 Normalize()
        {
            float length = Length();

            if (length == 0)
                throw new DivideByZeroException("Cannot normalize a vector when it's magnitude is zero");

            float inverse = 1f / length;

            return new Vector2(X * inverse, Y * inverse);
        }

        public float VectorToRotation()
        {
            return MathF.Atan2(Y, X);
        }

        // 2D cross product has no perpendicular axis to return a vector along, so the result is the
        // scalar Z component that a 3D cross product would produce; sign indicates winding/orientation.
        public static float Cross(Vector2 v1, Vector2 v2)
        {
            return (v1.X * v2.Y) - (v1.Y * v2.X);
        }

        public float Length()
        {
            return MathF.Sqrt((X * X) + (Y * Y));
        }

        public override bool Equals(object obj)
        {
            return obj is Vector2 other && Equals(other);
        }

        public bool Equals(Vector2 v)
        {
            return X == v.X && Y == v.Y;
        }

        public static bool operator ==(Vector2 a, Vector2 b) => a.Equals(b);

        public static bool operator !=(Vector2 a, Vector2 b) => !a.Equals(b);

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}