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
    public class VectorN : IEquatable<VectorN>
    {
        public float[] Axis { get; set; } = Array.Empty<float>();

        /// <summary>
        /// Empty vector CTOR
        /// </summary>
        public VectorN(int length)
        {
            Axis = new float[length];

            for (int i = 0; i < length; i++)
                Axis[i] = 0f;
        }

        /// <summary>
        /// Copy CTOR
        /// </summary>
        public VectorN(VectorN v)
        {
            Axis = new float[v.Axis.Length];

            for (int i = 0; i < v.Axis.Length; i++)
                Axis[i] = v.Axis[i];
        }

        public static VectorN operator +(VectorN v1, VectorN v2)
        {
            if (v1.Axis.Length != v2.Axis.Length)
                throw new InvalidOperationException($"cannot add vectors of unequal orders");

            var output = new VectorN(v1.Axis.Length);

            for (int i = 0; i < v1.Axis.Length; i++)
                output.Axis[i] = v1.Axis[i] + v2.Axis[i];

            return output;
        }

        public static VectorN operator -(VectorN v1, VectorN v2)
        {
            if (v1.Axis.Length != v2.Axis.Length)
                throw new InvalidOperationException($"cannot add vectors of unequal orders");

            var output = new VectorN(v1.Axis.Length);

            for (int i = 0; i < v1.Axis.Length; i++)
                output.Axis[i] = v1.Axis[i] - v2.Axis[i];

            return output;
        }

        public static VectorN operator *(VectorN v, float scalar)
        {
            var output = new VectorN(v.Axis.Length);

            for (int i = 0; i < v.Axis.Length; i++)
                output.Axis[i] = v.Axis[i] * scalar;

            return output;
        }

        public static VectorN operator /(VectorN v, float scalar)
        {
            var output = new VectorN(v.Axis.Length);

            for (int i = 0; i < v.Axis.Length; i++)
                output.Axis[i] = v.Axis[i] / scalar;

            return output;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            var new_obj = (VectorN)obj;
            return Equals(new_obj);
        }

        public bool Equals(VectorN v)
        {
            if (Axis.Length != v.Axis.Length)
                return false;

            for (int i = 0; i < Axis.Length; i++)
            {
                if (Axis[i] != v.Axis[i])
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return Axis.GetHashCode();
        }
    }
}
