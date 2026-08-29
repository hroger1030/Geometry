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
    public readonly struct Vector3 : IEquatable<Vector3>
    {
        /// <summary>A vector with all components set to zero.</summary>
        public static readonly Vector3 Zero = new(0, 0, 0);
        /// <summary>A vector with all components set to one.</summary>
        public static readonly Vector3 One = new(1, 1, 1);

        public float X { get; init; }

        public float Y { get; init; }

        public float Z { get; init; }

        /// <summary>
        /// Creates a zero vector (0, 0, 0).
        /// </summary>
        public Vector3() : this(0, 0, 0) { }

        /// <summary>
        /// Creates a copy of an existing vector.
        /// </summary>
        public Vector3(Vector3 v1) : this(v1.X, v1.Y, v1.Z) { }

        /// <summary>
        /// Creates a vector from the X, Y and Z components of a <see cref="Point3"/> (a position vector).
        /// </summary>
        public Vector3(Point3 p1) : this(p1.X, p1.Y, p1.Z) { }

        /// <summary>
        /// Creates the displacement vector pointing from <paramref name="head"/> to <paramref name="destination"/>.
        /// </summary>
        public Vector3(Point3 head, Point3 destination) : this(destination.X - head.X, destination.Y - head.Y, destination.Z - head.Z) { }

        /// <summary>
        /// Creates a vector from explicit X, Y and Z components.
        /// </summary>
        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Adds two vectors component-wise.
        /// </summary>
        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        /// <summary>
        /// Subtracts <paramref name="v2"/> from <paramref name="v1"/> component-wise.
        /// </summary>
        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        /// <summary>
        /// Multiplies each component of the vector by scalar <paramref name="s2"/>.
        /// </summary>
        public static Vector3 operator *(Vector3 v1, float s2)
        {
            return new Vector3(v1.X * s2, v1.Y * s2, v1.Z * s2);
        }

        /// <summary>
        /// Divides each component of the vector by scalar <paramref name="s2"/>.
        /// </summary>
        public static Vector3 operator /(Vector3 v1, float s2)
        {
            return new Vector3(v1.X / s2, v1.Y / s2, v1.Z / s2);
        }

        /// <summary>
        /// Returns the cross product <paramref name="v1"/> x <paramref name="v2"/>, a vector perpendicular to both inputs.
        /// </summary>
        public static Vector3 Cross(Vector3 v1, Vector3 v2)
        {
            return new Vector3(
                (v1.Y * v2.Z) - (v1.Z * v2.Y),
                (v1.Z * v2.X) - (v1.X * v2.Z),
                (v1.X * v2.Y) - (v1.Y * v2.X));
        }

        /// <summary>
        /// Returns a unit-length copy of <paramref name="v1"/>. Throws <see cref="DivideByZeroException"/> if the vector's magnitude is zero.
        /// </summary>
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

        /// <summary>
        /// Returns the Euclidean distance between the points that the two vectors represent.
        /// </summary>
        public static float DistanceTo(Vector3 v1, Vector3 v2)
        {
            float delta_x = v1.X - v2.X;
            float delta_y = v1.Y - v2.Y;
            float delta_z = v1.Z - v2.Z;

            return MathF.Sqrt((delta_x * delta_x) + (delta_y * delta_y) + (delta_z * delta_z));
        }

        /// <summary>
        /// Returns the Euclidean distance between this vector and <paramref name="other"/>.
        /// </summary>
        public float DistanceTo(Vector3 other)
        {
            return DistanceTo(this, other);
        }

        /// <summary>
        /// Returns the magnitude (Euclidean length) of this vector.
        /// </summary>
        public float Length() => MathF.Sqrt((X * X) + (Y * Y) + (Z * Z));

        /// <summary>
        /// Returns true if <paramref name="obj"/> is a <see cref="Vector3"/> with the same components.
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is Vector3 other && Equals(other);
        }

        /// <summary>
        /// Returns true if the other vector has exactly equal X, Y and Z components (no tolerance).
        /// </summary>
        public bool Equals(Vector3 v)
        {
            return X == v.X && Y == v.Y && Z == v.Z;
        }

        /// <summary>
        /// Returns true if both vectors have exactly equal components.
        /// </summary>
        public static bool operator ==(Vector3 a, Vector3 b) => a.Equals(b);

        /// <summary>
        /// Returns true if the vectors differ in any component.
        /// </summary>
        public static bool operator !=(Vector3 a, Vector3 b) => !a.Equals(b);

        /// <summary>
        /// Returns a hash code derived from the X, Y and Z components.
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }
    }
}