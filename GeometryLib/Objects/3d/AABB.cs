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
    public class AABB : I3d, IEquatable<AABB>
    {
        public Point3 Min { get; set; }

        public Point3 Max { get; set; }

        public float Volume => MathF.Abs(Max.X - Min.X) * MathF.Abs(Max.Y - Min.Y) * MathF.Abs(Max.Z - Min.Z);

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

        public AABB(Point3 min, Point3 max)
        {
            ArgumentNullException.ThrowIfNull(min);
            ArgumentNullException.ThrowIfNull(max);

            if (max.X < min.X || max.Y < min.Y || max.Z < min.Z)
                throw new ArgumentException("Max must be greater than or equal to Min.");

            Min = min;
            Max = max;
        }

        public bool Contains(Point3 point)
        {
            ArgumentNullException.ThrowIfNull(point);

            return point.X >= Min.X && point.X <= Max.X &&
                   point.Y >= Min.Y && point.Y <= Max.Y &&
                   point.Z >= Min.Z && point.Z <= Max.Z;
        }

        public bool Intersects(AABB other)
        {
            ArgumentNullException.ThrowIfNull(other);

            return Min.X <= other.Max.X && Max.X >= other.Min.X &&
                   Min.Y <= other.Max.Y && Max.Y >= other.Min.Y &&
                   Min.Z <= other.Max.Z && Max.Z >= other.Min.Z;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            return Equals((AABB)obj);
        }

        public bool Equals(AABB other)
        {
            if (other is null) return false;
            return Min.Equals(other.Min) && Max.Equals(other.Max);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Min, Max);
        }
    }
}
