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
    public readonly struct Ellipse : I2d, IEquatable<Ellipse>
    {
        public Point2 Center { get; init; }

        public float RadiusX { get; init; }

        public float RadiusY { get; init; }

        public float Area => MathF.PI * RadiusX * RadiusY;

        /// <summary>
        /// The approximate circumference of the ellipse, using Ramanujan's second approximation.
        /// </summary>
        public float Perimeter
        {
            get
            {
                float a = MathF.Max(RadiusX, RadiusY);
                float b = MathF.Min(RadiusX, RadiusY);
                float h = MathF.Pow((a - b), 2) / MathF.Pow((a + b), 2);

                return MathF.PI * (a + b) * (1 + (3f * h) / (10f + MathF.Sqrt(4f - 3f * h)));
            }
        }

        /// <summary>
        /// Creates an axis-aligned ellipse centered at <paramref name="center"/> with the given semi-axis lengths.
        /// Throws <see cref="ArgumentOutOfRangeException"/> if either radius is zero or negative.
        /// </summary>
        public Ellipse(Point2 center, float radiusX, float radiusY)
        {
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radiusX, 0f);
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radiusY, 0f);

            Center = center;
            RadiusX = radiusX;
            RadiusY = radiusY;
        }

        /// <summary>
        /// Returns true if <paramref name="point"/> lies inside or on this ellipse.
        /// </summary>
        public bool Contains(Point2 point)
        {

            float dx = point.X - Center.X;
            float dy = point.Y - Center.Y;

            return (dx * dx) / (RadiusX * RadiusX) + (dy * dy) / (RadiusY * RadiusY) <= 1f;
        }

        /// <summary>
        /// Returns true if <paramref name="obj"/> is an <see cref="Ellipse"/> with the same center and semi-axes.
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is Ellipse other && Equals(other);
        }

        /// <summary>
        /// Returns true if the other ellipse has the same center and semi-axis lengths (no tolerance).
        /// </summary>
        public bool Equals(Ellipse other)
        {
            return Center.Equals(other.Center) && RadiusX.Equals(other.RadiusX) && RadiusY.Equals(other.RadiusY);
        }

        /// <summary>
        /// Returns true if both ellipses have the same center and semi-axes.
        /// </summary>
        public static bool operator ==(Ellipse a, Ellipse b) => a.Equals(b);

        /// <summary>
        /// Returns true if the ellipses differ in center or either semi-axis.
        /// </summary>
        public static bool operator !=(Ellipse a, Ellipse b) => !a.Equals(b);

        /// <summary>
        /// Returns a hash code derived from the center and both semi-axis lengths.
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(Center, RadiusX, RadiusY);
        }
    }
}
