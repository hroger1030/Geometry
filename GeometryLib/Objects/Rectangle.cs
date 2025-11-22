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
using System.Diagnostics;

namespace Geometry
{
    [DebuggerDisplay("({Top},{Left},{Bottom},{Right})")]
    public class Rectangle : I2d, IEquatable<Rectangle>
    {
        /// <summary>
        /// Returns the x coordinate of the left edge of this <see cref="Rectangle"/>.
        /// </summary>
        public float Left { get; set; }

        /// <summary>
        /// Returns the x coordinate of the right edge of this <see cref="Rectangle"/>.
        /// </summary>
        public float Right { get; set; }

        /// <summary>
        /// Returns the y coordinate of the top edge of this <see cref="Rectangle"/>.
        /// </summary>
        public float Top { get; set; }

        /// <summary>
        /// Returns the y coordinate of the bottom edge of this <see cref="Rectangle"/>.
        /// </summary>
        public float Bottom { get; set; }

        public float X => Left;

        public float Y => Top;

        public float Width => Right - Left;

        public float Height => Bottom - Top;

        public Point2 TopLeftConrner => new(Left, Top);

        public Point2 TopRightConrner => new(Right, Top);

        public Point2 BottomLeftConrner => new(Left, Bottom);

        public Point2 BottomRightCorner => new(Right, Bottom);

        /// <summary>
        /// The top-left coordinates of this <see cref="Rectangle"/>.
        /// </summary>
        public Point2 Location => TopLeftConrner;

        /// <summary>
        /// The width-height coordinates of this <see cref="Rectangle"/>.
        /// </summary>
        public Point2 Size => new(Width, Height);

        /// <summary>
        /// A <see cref="Point2"/> located in the center of this <see cref="Rectangle"/>.
        /// </summary>
        public Point2 Center => new((Right - Left) / 2, (Bottom - Top) / 2);

        public float Area => Width * Height;

        public float Perimeter => (Width + Height) * 2f;

        public Rectangle() : this(0f, 0f, 1f, 1f) { }

        public Rectangle(float width, float height) : this(0f, 0f, width, height) { }

        public Rectangle(float left, float top, float width, float height)
        {
            if (width < float.Epsilon)
                throw new ArgumentException("Width must be greater than 0");

            if (height < float.Epsilon)
                throw new ArgumentException("Height must be greater than 0");

            Left = left;
            Top = top;
            Right = left + width;
            Bottom = top + height;
        }

        public Rectangle(Point2 center, float width, float height)
        {
            if (center == null)
                throw new ArgumentNullException(nameof(center), "Center point cannot be null");

            if (width < float.Epsilon)
                throw new ArgumentException("Width must be greater than 0");

            if (height < float.Epsilon)
                throw new ArgumentException("Height must be greater than 0");

            Left = center.X - width / 2;
            Top = center.Y - height / 2;
            Right = center.X + width / 2;
            Bottom = center.Y + height / 2;
        }

        public Rectangle(Rectangle rectangle)
        {
            Left = rectangle.Left;
            Top = rectangle.Top;
            Right = rectangle.Right;
            Bottom = rectangle.Bottom;
        }

        public bool Contains(Point2 point)
        {
            return Contains(point.X, point.Y);
        }

        public bool Contains(float x, float y)
        {
            return x >= Left && x <= Right && y >= Top && y <= Bottom;
        }

        public bool Contains(Rectangle value)
        {
            return value.Left >= Left && value.Right <= Right && value.Top >= Top && value.Bottom <= Bottom;
        }

        /// <summary>
        /// Adjusts the edges of this <see cref="Rectangle"/> by specified horizontal and vertical amounts. 
        /// Rectangle top left will remain in place, and values don't have to be symetrical.
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

            //var topLeft = Contains(r.TopLeftConrner);
            //var topRight = Contains(r.TopRightConrner);
            //var bottomLeft = Contains(r.BottomLeftConrner);
            //var bottomRight = Contains(r.BottomRightCorner);

            //// retrun true if any one of these are true
            //return topLeft || topRight || bottomLeft || bottomRight;
        }

        /// <summary>
        /// Gets whether or not a specified <see cref="Circle"/> intersects with this <see cref="Rectangle"/>.
        /// </summary>
        public bool Intersects(Circle c)
        {
            // return true if circle center is in rectangle
            if (Contains(c.Center))
                return true;

            var topLeft = c.Contains(TopLeftConrner);
            var topRight = c.Contains(TopRightConrner);
            var bottomLeft = c.Contains(BottomLeftConrner);
            var bottomRight = c.Contains(BottomRightCorner);

            // return true if circle contains any cormers
            return topLeft || topRight || bottomLeft || bottomRight;
        }

        /// <summary>
        /// Creates a new <see cref="Rectangle"/> that completely contains two r rectangles.
        /// </summary>
        public static Rectangle Union(Rectangle r1, Rectangle r2)
        {
            return new Rectangle()
            {
                Left = Math.Min(r1.Left, r2.Left),
                Top = Math.Min(r1.Left, r2.Left),
                Right = Math.Max(r1.Right, r2.Right),
                Bottom = Math.Max(r1.Bottom, r2.Bottom),
            };
        }

        /// <summary>
        /// Creates a new <see cref="Rectangle"/> that is shifted by a vector.
        /// </summary>
        public static Rectangle operator +(Rectangle r, Vector2 v)
        {
            if (r == null)
                throw new ArgumentNullException(nameof(r));

            if (v == null)
                throw new ArgumentNullException(nameof(v));

            return new Rectangle()
            {
                Left = r.X + v.X,
                Top = r.Y + v.Y,
                Right = r.Right + v.X,
                Bottom = r.Bottom + v.Y,
            };
        }

        public static Rectangle operator -(Rectangle r, Vector2 v)
        {
            if (r == null)
                throw new ArgumentNullException(nameof(r));

            if (v == null)
                throw new ArgumentNullException(nameof(v));

            return new Rectangle()
            {
                Left = r.X - v.X,
                Top = r.Y - v.Y,
                Right = r.Right - v.X,
                Bottom = r.Bottom - v.Y,
            };
        }

        public static Rectangle operator *(Rectangle r, float scale)
        {
            if (r == null)
                throw new ArgumentNullException(nameof(r));

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

        public static Rectangle operator /(Rectangle r, float scale)
        {
            if (scale == 0)
                throw new DivideByZeroException("Scale cannot be 0");

            return r * (1 / scale);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            var newObj = (Rectangle)obj;
            return Equals(newObj);
        }

        public bool Equals(Rectangle r)
        {
            return Left == r.Left && Top == r.Top && Right == r.Right && Bottom == r.Bottom;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Left, Right, Top, Bottom);
        }
    }
}
