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
    public readonly struct AABB : I3d, IEquatable<AABB>
    {
        public Point3 Min { get; init; }

        public Point3 Max { get; init; }

        /// <summary>The volume of the box (width * height * depth).</summary>
        public float Volume => MathF.Abs(Max.X - Min.X) * MathF.Abs(Max.Y - Min.Y) * MathF.Abs(Max.Z - Min.Z);

        /// <summary>The total surface area of the box's six faces.</summary>
        public float SurfaceArea
        {
            get
            {
                float width = MathF.Abs(Max.X - Min.X);
                float height = MathF.Abs(Max.Y - Min.Y);
                float depth = MathF.Abs(Max.Z - Min.Z);
                return 2f * (width * height + height * depth + depth * width);
            }
        }

        /// <summary>
        /// Creates an axis-aligned bounding box from its minimum and maximum corners.
        /// Throws <see cref="ArgumentException"/> if any component of <paramref name="max"/> is less than the matching component of <paramref name="min"/>.
        /// </summary>
        public AABB(Point3 min, Point3 max)
        {

            if (max.X < min.X || max.Y < min.Y || max.Z < min.Z)
                throw new ArgumentException("Max must be greater than or equal to Min.");

            Min = min;
            Max = max;
        }

        /// <summary>
        /// Returns true if <paramref name="point"/> lies inside or on the faces of this box.
        /// </summary>
        public bool Contains(Point3 point)
        {

            return point.X >= Min.X && point.X <= Max.X &&
                   point.Y >= Min.Y && point.Y <= Max.Y &&
                   point.Z >= Min.Z && point.Z <= Max.Z;
        }

        /// <summary>
        /// Returns true if this box overlaps or touches <paramref name="other"/> on all three axes.
        /// </summary>
        public bool Intersects(AABB other)
        {
            return Min.X <= other.Max.X && Max.X >= other.Min.X &&
                   Min.Y <= other.Max.Y && Max.Y >= other.Min.Y &&
                   Min.Z <= other.Max.Z && Max.Z >= other.Min.Z;
        }

        /// <summary>
        /// Returns true if <paramref name="obj"/> is an <see cref="AABB"/> with the same corners.
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is AABB other && Equals(other);
        }

        /// <summary>
        /// Returns true if the other box has the same min and max corners (no tolerance).
        /// </summary>
        public bool Equals(AABB other)
        {
            return Min.Equals(other.Min) && Max.Equals(other.Max);
        }

        /// <summary>
        /// Returns true if both boxes have the same corners.
        /// </summary>
        public static bool operator ==(AABB a, AABB b) => a.Equals(b);

        /// <summary>
        /// Returns true if the boxes differ in either corner.
        /// </summary>
        public static bool operator !=(AABB a, AABB b) => !a.Equals(b);

        /// <summary>
        /// Returns a hash code derived from the min and max corners.
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(Min, Max);
        }
    }
}
