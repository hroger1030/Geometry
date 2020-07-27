using System;
using System.Diagnostics;

namespace Geometry
{
    [DebuggerDisplay("VectorN {_Axis.Length}")]
    public class VectorN
    {
        protected double[] _Axis;

        public double[] Axis
        {
            get { return _Axis; }
            set { _Axis = value; }
        }

        public double this[int i]
        {
            get
            {
                if (i < 0 || i >= _Axis.Length)
                    throw new IndexOutOfRangeException($"VectorN element '{i}' does not exist.");
                else
                    return _Axis[i];
            }
            set
            {
                if (i < 0 || i >= _Axis.Length)
                    throw new IndexOutOfRangeException($"VectorN element '{i}' does not exist.");
                else
                    _Axis[i] = value;
            }
        }

        public double Length
        {
            get { return GetLength(); }
        }

        public bool IsNormalized
        {
            get { return (GetLength() == 1.0); }
        }

        public VectorN(VectorN other)
        {
            Copy(other);
        }

        public VectorN(int dimensions) : this(dimensions, 0) { }

        public VectorN(int dimensions, double default_value)
        {
            if (dimensions < 1)
                throw new InvalidOperationException("Vectors with less that 1 dimension aren't supported.");

            _Axis = new double[dimensions];

            for (int i = 0; i < dimensions; i++)
                _Axis[i] = default_value;
        }

        public static bool operator <(VectorN v1, VectorN v2)
        {
            return v1.Length < v2.Length;
        }

        public static bool operator >(VectorN v1, VectorN v2)
        {
            return v1.Length > v2.Length;
        }

        public static bool operator ==(VectorN v1, VectorN v2)
        {
            if (v1.Axis.Length != v2.Axis.Length)
                throw new InvalidOperationException($"Performing operations on vectors of differing dimensions ('{v1.Length}' and '{v2.Length}') can produce odd results.");

            for (int i = 0; i < v1.Axis.Length; i++)
            {
                if (v1.Axis[i] != v2.Axis[i])
                    return false;
            }

            return true;
        }

        public static bool operator !=(VectorN v1, VectorN v2)
        {
            if (v1.Axis.Length != v2.Axis.Length)
                throw new InvalidOperationException($"Performing operations on vectors of differing dimensions ('{v1.Length}' and '{v2.Length}') can produce odd results.");

            for (int i = 0; i < v1.Axis.Length; i++)
            {
                if (v1.Axis[i] == v2.Axis[i])
                    return false;
            }

            return true;
        }

        public static VectorN operator -(VectorN v1)
        {
            VectorN output = new VectorN(v1.Axis.Length);

            for (int i = 0; i < v1.Axis.Length; i++)
                output.Axis[i] = -v1.Axis[i];

            return output;
        }

        public static VectorN operator +(VectorN v1, VectorN v2)
        {
            if (v1.Axis.Length != v2.Axis.Length)
                throw new InvalidOperationException($"Performing operations on vectors of differing dimensions ('{v1.Length}' and '{v2.Length}') can produce odd results.");

            VectorN output = new VectorN(v1.Axis.Length);

            for (int i = 0; i < output.Axis.Length; i++)
                output.Axis[i] = v1.Axis[i] + v2.Axis[i];

            return output;
        }

        public static VectorN operator -(VectorN v1, VectorN v2)
        {
            if (v1.Axis.Length != v2.Axis.Length)
                throw new InvalidOperationException($"Performing operations on vectors of differing dimensions ('{v1.Length}' and '{v2.Length}') can produce odd results.");

            VectorN output = new VectorN(v1.Axis.Length);

            for (int i = 0; i < output.Axis.Length; i++)
                output.Axis[i] = v1.Axis[i] - v2.Axis[i];

            return output;
        }

        public static VectorN operator *(VectorN v1, double scalar)
        {
            VectorN output = new VectorN(v1);
            output.Scale(scalar);

            return output;
        }

        public static VectorN operator /(VectorN v1, double scalar)
        {
            VectorN output = new VectorN(v1);
            output.Scale(1 / scalar);

            return output;
        }

        /// <summary>
        /// Returns the dot product scalar of the two vectors.
        /// </summary>
        public static double operator *(VectorN v1, VectorN v2)
        {
            if (v1.Axis.Length != v2.Axis.Length)
                throw new InvalidOperationException($"Performing operations on vectors of differing dimensions ('{v1.Length}' and '{v2.Length}') can produce odd results.");

            double output = 0;

            for (int i = 0; i < v1.Axis.Length; i++)
                output += v1.Axis[i] * v2.Axis[i];

            return output;
        }

        public VectorN CreateVectorToTarget(VectorN target)
        {
            VectorN output = new VectorN(_Axis.Length);

            for (int i = 0; i < _Axis.Length; i++)
                output[i] = target[i] - _Axis[i];

            if (!IsNormalized)
                output.Normalize();

            return output;
        }

        public static VectorN Interpolate(VectorN v1, VectorN v2, double interpolate_range)
        {
            if (v1.Axis.Length != v2.Axis.Length)
                throw new InvalidOperationException($"Performing operations on vectors of differing dimensions ('{v1.Length}' and '{v2.Length}') can produce odd results.");

            if (interpolate_range > 1 || interpolate_range < 0)
                throw new ArgumentOutOfRangeException("Interpolate range", interpolate_range, "interpolate range must be between 0 and 1.");

            VectorN output = new VectorN(v1.Axis.Length);

            for (int i = 0; i < output.Axis.Length; i++)
                output.Axis[i] = v1.Axis[i] * (1 - interpolate_range) + v2.Axis[i] * interpolate_range;

            return output;
        }

        public double DistanceTo(VectorN target)
        {
            double distance = 0;

            for (int i = 0; i < _Axis.Length; i++)
                distance += (target._Axis[i] - this._Axis[i]) * (target._Axis[i] - this._Axis[i]);

            return Math.Sqrt(distance);
        }

        public void Copy(VectorN other)
        {
            _Axis = new double[other.Axis.Length];

            for (int i = 0; i < _Axis.Length; i++)
                _Axis[i] = other[i];
        }

        public void Scale(double scalar)
        {
            for (int i = 0; i < _Axis.Length; i++)
                _Axis[i] *= scalar;
        }

        public void Normalize()
        {
            double length = GetLength();

            if (length != 0)
            {
                for (int i = 0; i < _Axis.Length; i++)
                    _Axis[i] /= length;
            }
        }

        public void Reset()
        {
            for (int i = 0; i < _Axis.Length; i++)
                _Axis[i] = 0;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            var new_obj = (VectorN)obj;
            return Equals(new_obj);
        }

        public bool Equals(VectorN other)
        {
            if (_Axis.Length != other._Axis.Length)
                return false;

            for (int i = 0; i < _Axis.Length; i++)
            {
                if (_Axis[i] != other._Axis[i])
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return _Axis.GetHashCode();
        }

        public override string ToString()
        {
            return $"VectorN({ _Axis.Length})";
        }

        protected double GetLength()
        {
            double total = 0;

            for (int i = 0; i < _Axis.Length; i++)
                total += _Axis[i] * _Axis[i];

            return Math.Sqrt(total);
        }
    }
}
