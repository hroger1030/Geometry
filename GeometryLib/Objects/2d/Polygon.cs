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
using System.Collections.Generic;

namespace Geometry
{
    public class Polygon : I2d, IEquatable<Polygon>
    {
        public static readonly Polygon UnitPolygon = new(new List<Point2>() 
        { 
            new Point2(0, 0), 
            new Point2(0, 1), 
            new Point2(0, 1) 
        });

        public List<Point2> Vertices { get; set; } = new List<Point2>();

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

        public int Sides => Vertices.Count;

        public Polygon() { }

        public Polygon(List<Point2> vertices)
        {
            ArgumentNullException.ThrowIfNull(vertices);

            Vertices = vertices;
        }

        public bool Contains(float x, float y)
        {
            return Contains(new Point2(x, y));
        }

        public bool Contains(Point2 point)
        {
            ArgumentNullException.ThrowIfNull(point);

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

        // contains
        // intersects

        /// <summary>
        /// Creates a new <see cref="Polygon"/> that is shifted by a vector.
        /// </summary>
        public static Polygon operator +(Polygon p, Vector2 v)
        {
            ArgumentNullException.ThrowIfNull(p);
            ArgumentNullException.ThrowIfNull(v);

            var output = new Polygon();

            foreach (var vertex in p.Vertices)
                output.Vertices.Add(vertex + v);

            return output;
        }

        public static Polygon operator -(Polygon p, Vector2 v)
        {
            ArgumentNullException.ThrowIfNull(p);
            ArgumentNullException.ThrowIfNull(v);

            var output = new Polygon();

            foreach (var vertex in p.Vertices)
                output.Vertices.Add(vertex - v);

            return output;
        }

        public static Polygon operator *(Polygon p, float scale)
        {
            ArgumentNullException.ThrowIfNull(p);

            var output = new Polygon();

            foreach (var vertex in p.Vertices)
                output.Vertices.Add(new Point2(vertex.X * scale, vertex.Y * scale));

            return output;
        }

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

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            var new_obj = (Polygon)obj;
            return Equals(new_obj);
        }

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
