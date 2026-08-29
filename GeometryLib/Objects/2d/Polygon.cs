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
    public class Polygon : I2d, IEquatable<Polygon>
    {
        public List<Point2> Vertices { get; set; } = new();

        /// <summary>
        /// The area enclosed by the polygon, computed with the shoelace formula. Result is unsigned,
        /// so winding order does not matter; self-intersecting polygons give an ill-defined value.
        /// </summary>
        public float Area
        {
            get
            {
                float area = 0;

                for (int i = 0; i < Vertices.Count; i++)
                {
                    var p1 = Vertices[i];
                    var p2 = Vertices[(i + 1) % Vertices.Count];
                    area += (p1.X * p2.Y) - (p1.Y * p2.X);
                }

                return MathF.Abs(area / 2);
            }
        }

        /// <summary>
        /// The total edge length of the polygon, including the closing edge from the last vertex back to the first.
        /// </summary>
        public float Perimeter
        {
            get
            {
                float perimeter = 0;

                for (int i = 0; i < Vertices.Count; i++)
                {
                    var p1 = Vertices[i];
                    var p2 = Vertices[(i + 1) % Vertices.Count];
                    perimeter += p1.DistanceTo(p2);
                }

                return perimeter;
            }
        }

        /// <summary>The number of vertices (equivalently, the number of edges).</summary>
        public int Sides => Vertices.Count;

        /// <summary>
        /// Creates an empty polygon with no vertices.
        /// </summary>
        public Polygon() { }

        /// <summary>
        /// Creates a polygon that wraps the given vertex list (the list is stored by reference, not copied).
        /// Throws <see cref="ArgumentNullException"/> if <paramref name="vertices"/> is null.
        /// </summary>
        public Polygon(List<Point2> vertices)
        {
            ArgumentNullException.ThrowIfNull(vertices);

            Vertices = vertices;
        }

        /// <summary>
        /// Returns true if (<paramref name="x"/>, <paramref name="y"/>) lies inside the polygon.
        /// </summary>
        public bool Contains(float x, float y)
        {
            return Contains(new Point2(x, y));
        }

        /// <summary>
        /// Returns true if <paramref name="point"/> lies inside the polygon, using a ray-casting (even-odd) test.
        /// Always returns false for polygons with fewer than three vertices; behavior on the boundary is not guaranteed.
        /// </summary>
        public bool Contains(Point2 point)
        {
            if (Sides < 3)
                return false;

            bool isInside = false;

            for (int i = 0, j = Vertices.Count - 1; i < Vertices.Count; j = i++)
            {
                if ((Vertices[i].Y > point.Y) != (Vertices[j].Y > point.Y) && point.X < (Vertices[j].X - Vertices[i].X) * (point.Y - Vertices[i].Y) / (Vertices[j].Y - Vertices[i].Y) + Vertices[i].X)
                {
                    isInside = !isInside;
                }
            }

            return isInside;
        }

        /// <summary>
        /// Creates a new <see cref="Polygon"/> that is shifted by a vector.
        /// </summary>
        public static Polygon operator +(Polygon p, Vector2 v)
        {
            ArgumentNullException.ThrowIfNull(p);

            var output = new Polygon();

            foreach (var vertex in p.Vertices)
                output.Vertices.Add(vertex + v);

            return output;
        }

        /// <summary>
        /// Creates a new <see cref="Polygon"/> with every vertex shifted by the negation of a vector.
        /// </summary>
        public static Polygon operator -(Polygon p, Vector2 v)
        {
            ArgumentNullException.ThrowIfNull(p);

            var output = new Polygon();

            foreach (var vertex in p.Vertices)
                output.Vertices.Add(vertex - v);

            return output;
        }

        /// <summary>
        /// Creates a new <see cref="Polygon"/> with every vertex scaled about the origin by <paramref name="scale"/>.
        /// </summary>
        public static Polygon operator *(Polygon p, float scale)
        {
            ArgumentNullException.ThrowIfNull(p);

            var output = new Polygon();

            foreach (var vertex in p.Vertices)
                output.Vertices.Add(new Point2(vertex.X * scale, vertex.Y * scale));

            return output;
        }

        /// <summary>
        /// Creates a new <see cref="Polygon"/> with every vertex divided about the origin by <paramref name="scale"/>.
        /// Throws <see cref="DivideByZeroException"/> if <paramref name="scale"/> is zero.
        /// </summary>
        public static Polygon operator /(Polygon p, float scale)
        {
            ArgumentNullException.ThrowIfNull(p);

            if (scale == 0f)
                throw new DivideByZeroException(nameof(scale));

            var output = new Polygon();

            foreach (var vertex in p.Vertices)
                output.Vertices.Add(new Point2(vertex.X / scale, vertex.Y / scale));

            return output;
        }

        /// <summary>
        /// Returns true if <paramref name="obj"/> is a <see cref="Polygon"/> with the same vertices in the same order.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            var new_obj = (Polygon)obj;
            return Equals(new_obj);
        }

        /// <summary>
        /// Returns true if the other polygon has the same vertex count and identical vertices in the same order.
        /// </summary>
        public bool Equals(Polygon p)
        {
            if (Vertices.Count != p.Vertices.Count)
                return false;

            for (int i = 0; i < Vertices.Count; i++)
            {
                if (!Vertices[i].Equals(p.Vertices[i]))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Returns a hash code formed by XOR-ing the hash codes of all vertices (order-independent).
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                int output = 0;

                foreach (var vertex in Vertices)
                    output ^= vertex.GetHashCode();

                return output;
            }
        }
    }
}
