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
    public readonly struct Rectangle : I2d, IEquatable<Rectangle>
    {
        /// <summary>A 1x1 rectangle with its top-left corner at the origin.</summary>
        public readonly static Rectangle UnitRectangle = new(0f, 0f, 1f, 1f);

        /// <summary>
        /// Returns the x coordinate of the left edge of this <see cref="Rectangle"/>.
        /// </summary>
        public float Left { get; init; }

        /// <summary>
        /// Returns the x coordinate of the right edge of this <see cref="Rectangle"/>.
        /// </summary>
        public float Right { get; init; }

        /// <summary>
        /// Returns the y coordinate of the top edge of this <see cref="Rectangle"/>.
        /// </summary>
        public float Top { get; init; }

        /// <summary>
        /// Returns the y coordinate of the bottom edge of this <see cref="Rectangle"/>.
        /// </summary>
        public float Bottom { get; init; }

        public float X => Left;

        public float Y => Top;

        public float Width => Right - Left;

        public float Height => Bottom - Top;

        public Point2 TopLeftCorner => new(Left, Top);

        public Point2 TopRightCorner => new(Right, Top);

        public Point2 BottomLeftCorner => new(Left, Bottom);

        public Point2 BottomRightCorner => new(Right, Bottom);

        /// <summary>
        /// The top-left coordinates of this <see cref="Rectangle"/>.
        /// </summary>
        public Point2 Location => TopLeftCorner;

        /// <summary>
        /// The width-height coordinates of this <see cref="Rectangle"/>.
        /// </summary>
        public Point2 Size => new(Width, Height);

        /// <summary>
        /// A <see cref="Point2"/> located in the center of this <see cref="Rectangle"/>.
        /// </summary>
        public Point2 Center => new((Left + Right) / 2, (Top + Bottom) / 2);

        public float Area => Width * Height;

        public float Perimeter => (Width + Height) * 2f;

        /// <summary>
        /// Creates a 1x1 rectangle with its top-left corner at the origin.
        /// </summary>
        public Rectangle() : this(0f, 0f, 1f, 1f) { }

        /// <summary>
        /// Creates a rectangle of the given size with its top-left corner at the origin.
        /// </summary>
        public Rectangle(float width, float height) : this(0f, 0f, width, height) { }

        /// <summary>
        /// Creates a rectangle from its top-left corner and size.
        /// Throws <see cref="ArgumentOutOfRangeException"/> if <paramref name="width"/> or <paramref name="height"/> is zero or negative.
        /// </summary>
        public Rectangle(float left, float top, float width, float height)
        {
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(width, 0f);
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(height, 0f);

            Left = left;
            Top = top;
            Right = left + width;
            Bottom = top + height;
        }

        /// <summary>
        /// Creates a rectangle of the given size centered on <paramref name="center"/>.
        /// Throws <see cref="ArgumentOutOfRangeException"/> if <paramref name="width"/> or <paramref name="height"/> is zero or negative.
        /// </summary>
        public Rectangle(Point2 center, float width, float height)
        {
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(width, 0f);
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(height, 0f);

            Left = center.X - width / 2;
            Top = center.Y - height / 2;
            Right = center.X + width / 2;
            Bottom = center.Y + height / 2;
        }

        /// <summary>
        /// Creates a copy of an existing rectangle.
        /// </summary>
        public Rectangle(Rectangle rectangle)
        {
            Left = rectangle.Left;
            Top = rectangle.Top;
            Right = rectangle.Right;
            Bottom = rectangle.Bottom;
        }

        /// <summary>
        /// Returns true if <paramref name="point"/> lies inside or on the edges of this rectangle.
        /// </summary>
        public bool Contains(Point2 point)
        {
            return Contains(point.X, point.Y);
        }

        /// <summary>
        /// Returns true if (<paramref name="x"/>, <paramref name="y"/>) lies inside or on the edges of this rectangle.
        /// </summary>
        public bool Contains(float x, float y)
        {
            return x >= Left && x <= Right && y >= Top && y <= Bottom;
        }

        /// <summary>
        /// Returns true if <paramref name="value"/> lies entirely inside or on the edges of this rectangle.
        /// </summary>
        public bool Contains(Rectangle value)
        {
            return value.Left >= Left && value.Right <= Right && value.Top >= Top && value.Bottom <= Bottom;
        }

        /// <summary>
        /// Adjusts the edges of this <see cref="Rectangle"/> by specified horizontal and vertical amounts. 
        /// Rectangle top left will remain in place, and values don't have to be symmetrical.
        /// </summary>
        public Rectangle Scale(float widthScale, float heightScale)
        {
            return new Rectangle
            {
                Left = Left,
                Top = Top,
                Right = Left + (Width * widthScale),
                Bottom = Top + (Height * heightScale),
            };
        }

        /// <summary>
        /// Gets whether or not a specified <see cref="Rectangle"/> intersects with this <see cref="Rectangle"/>.
        /// </summary>
        public bool Intersects(Rectangle r)
        {
            // Check if the rectangles are intersecting or tangent
            bool intersectingOrTangent = this.Right >= r.Left && // rect1's right side is to the right of or touching rect2's left side
                                         this.Left <= r.Right && // rect1's left side is to the left of or touching rect2's right side
                                         this.Bottom >= r.Top && // rect1's bottom side is below or touching rect2's top side
                                         this.Top <= r.Bottom;   // rect1's top side is above or touching rect2's bottom side

            return intersectingOrTangent;
        }

        /// <summary>
        /// Gets whether or not a specified <see cref="Circle"/> intersects with this <see cref="Rectangle"/>.
        /// </summary>
        public bool Intersects(Circle c)
        {
            // closest point on (or in) the rectangle to the circle centre
            float closestX = Math.Clamp(c.Center.X, Left, Right);
            float closestY = Math.Clamp(c.Center.Y, Top, Bottom);

            float dx = c.Center.X - closestX;
            float dy = c.Center.Y - closestY;

            return (dx * dx + dy * dy) <= (c.Radius * c.Radius);
        }

        /// <summary>
        /// Creates a new <see cref="Rectangle"/> that completely contains two r rectangles.
        /// </summary>
        public static Rectangle Union(Rectangle r1, Rectangle r2)
        {
            return new Rectangle()
            {
                Left = MathF.Min(r1.Left, r2.Left),
                Top = MathF.Min(r1.Top, r2.Top),
                Right = MathF.Max(r1.Right, r2.Right),
                Bottom = MathF.Max(r1.Bottom, r2.Bottom),
            };
        }

        /// <summary>
        /// Creates a new <see cref="Rectangle"/> that is shifted by a vector.
        /// </summary>
        /// <param name="r">The rectangle to shift.</param>
        /// <param name="v">The translation to apply.</param>
        public static Rectangle operator +(Rectangle r, Vector2 v)
        {
            return new Rectangle()
            {
                Left = r.X + v.X,
                Top = r.Y + v.Y,
                Right = r.Right + v.X,
                Bottom = r.Bottom + v.Y,
            };
        }

        /// <summary>
        /// Creates a new <see cref="Rectangle"/> that is shifted by the negation of a vector.
        /// </summary>
        public static Rectangle operator -(Rectangle r, Vector2 v)
        {
            return new Rectangle()
            {
                Left = r.X - v.X,
                Top = r.Y - v.Y,
                Right = r.Right - v.X,
                Bottom = r.Bottom - v.Y,
            };
        }

        /// <summary>
        /// Returns a copy of the rectangle scaled about its top-left corner (width and height multiplied by <paramref name="scale"/>).
        /// Throws <see cref="ArgumentException"/> if <paramref name="scale"/> is negative.
        /// </summary>
        public static Rectangle operator *(Rectangle r, float scale)
        {
            if (scale < 0)
                throw new ArgumentException("Scale cannot be less than 0");

            return new Rectangle()
            {
                Left = r.Left,
                Top = r.Top,
                Right = r.Left + (r.Width * scale),
                Bottom = r.Top + (r.Height * scale),
            };
        }

        /// <summary>
        /// Returns a copy of the rectangle scaled about its top-left corner by 1/<paramref name="scale"/>.
        /// Throws <see cref="DivideByZeroException"/> if <paramref name="scale"/> is zero.
        /// </summary>
        public static Rectangle operator /(Rectangle r, float scale)
        {
            if (scale == 0)
                throw new DivideByZeroException("Scale cannot be 0");

            return r * (1 / scale);
        }

        /// <summary>
        /// Returns true if <paramref name="obj"/> is a <see cref="Rectangle"/> with the same edges.
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is Rectangle other && Equals(other);
        }

        /// <summary>
        /// Returns true if the other rectangle has the same left, top, right and bottom edges (no tolerance).
        /// </summary>
        public bool Equals(Rectangle r)
        {
            return Left == r.Left && Top == r.Top && Right == r.Right && Bottom == r.Bottom;
        }

        /// <summary>
        /// Returns true if both rectangles have the same edges.
        /// </summary>
        public static bool operator ==(Rectangle a, Rectangle b) => a.Equals(b);

        /// <summary>
        /// Returns true if the rectangles differ in any edge.
        /// </summary>
        public static bool operator !=(Rectangle a, Rectangle b) => !a.Equals(b);

        /// <summary>
        /// Returns a hash code derived from the four edge coordinates.
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(Left, Right, Top, Bottom);
        }
    }
}
