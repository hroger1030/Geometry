using System;
using System.Diagnostics;

namespace Geometry
{
    [DebuggerDisplay("Rectangle {_Left},{_Top} - {_Height}x{_Width}")]
    public class Rectangle : IEquatable<Rectangle>
    {
        protected float _Left;
        protected float _Top;
        protected float _Width;
        protected float _Height;

        public float X
        {
            get { return _Left; }
            set { _Left = value; }
        }
        public float Y
        {
            get { return _Top; }
            set { _Top = value; }
        }
        public float Width
        {
            get { return _Width; }
            set
            {
                if (value < float.Epsilon)
                    throw new ArgumentException("width must be greater than 0");

                _Width = value;
            }
        }
        public float Height
        {
            get { return _Height; }
            set
            {
                if (value < float.Epsilon)
                    throw new ArgumentException("height must be greater than 0");

                _Height = value;
            }
        }

        public Point2 TopLeftConrner
        {
            get { return new Point2(_Left, _Top); }
        }
        public Point2 TopRightConrner
        {
            get { return new Point2(_Left + _Width, _Top); }
        }
        public Point2 BottomLeftConrner
        {
            get { return new Point2(_Left, _Top + _Height); }
        }
        public Point2 BottomRightCorner
        {
            get { return new Point2(_Left + _Width, _Top + _Height); }
        }

        /// <summary>
        /// Returns the x coordinate of the left edge of this <see cref="Rectangle"/>.
        /// </summary>
        public float Left
        {
            get { return _Left; }
            set { _Left = value; }
        }
        /// <summary>
        /// Returns the x coordinate of the right edge of this <see cref="Rectangle"/>.
        /// </summary>
        public float Right
        {
            get { return _Left + _Width; }
            set { _Left = value - _Width; }
        }
        /// <summary>
        /// Returns the y coordinate of the top edge of this <see cref="Rectangle"/>.
        /// </summary>
        public float Top
        {
            get { return _Top; }
            set { _Top = value; }
        }
        /// <summary>
        /// Returns the y coordinate of the bottom edge of this <see cref="Rectangle"/>.
        /// </summary>
        public float Bottom
        {
            get { return (_Top + _Height); }
            set { _Top = value - _Height; }
        }
        /// <summary>
        /// The top-left coordinates of this <see cref="Rectangle"/>.
        /// </summary>
        public Point2 Location
        {
            get { return new Point2(_Left, _Top); }
            set
            {
                _Left = value.X;
                _Top = value.Y;
            }
        }
        /// <summary>
        /// The width-height coordinates of this <see cref="Rectangle"/>.
        /// </summary>
        public Point2 Size
        {
            get
            {
                return new Point2(_Width, _Height);
            }
            set
            {
                _Width = value.X;
                _Height = value.Y;
            }
        }

        /// <summary>
        /// A <see cref="Point2"/> located in the center of this <see cref="Rectangle"/>.
        /// </summary>
        public Point2 Center
        {
            get { return new Point2(_Left + (_Width / 2), _Top + (_Height / 2)); }
            set
            {
                _Left = value.X - (_Width / 2);
                _Top = value.Y - (_Height / 2);
            }
        }
        public float Area
        {
            get { return _Height * _Width; }
        }
        public float Perimeter
        {
            get { return (_Height + _Width) * 2f; }
        }

        public Rectangle() : this(0f, 0f, 1f, 1f) { }

        public Rectangle(float width, float height) : this(0f, 0f, width, height) { }

        public Rectangle(float x, float y, float width, float height)
        {
            if (width < float.Epsilon)
                throw new ArgumentException("Width must be greater than 0");

            if (height < float.Epsilon)
                throw new ArgumentException("Height must be greater than 0");

            _Left = x;
            _Top = y;
            _Width = width;
            _Height = height;
        }

        public Rectangle(Point2 center, float width, float height)
        {
            if (center == null)
                throw new ArgumentNullException("Center point cannot be null");

            if (width < float.Epsilon)
                throw new ArgumentException("Width must be greater than 0");

            if (height < float.Epsilon)
                throw new ArgumentException("Height must be greater than 0");

            _Left = center.X - width/2;
            _Top = center.Y - height/2;
            _Width = width;
            _Height = height;
        }

        public Rectangle(Rectangle rectangle)
        {
            _Left = rectangle._Left;
            _Top = rectangle._Top;
            _Width = rectangle._Width;
            _Height = rectangle._Height;
        }

        public bool Contains(int x, int y)
        {
            return ((((_Left <= x) && (x < (_Left + _Width))) && (_Top <= y)) && (y < (_Top + _Height)));
        }

        public bool Contains(float x, float y)
        {
            return (_Left <= x) && (_Top <= y) && (x <= (_Left + _Width)) && (y <= (_Top + _Height));
        }

        public bool Contains(Point2 point)
        {
            return (_Left <= point.X) && (_Top <= point.Y) && (point.X <= (_Left + _Width)) && (point.Y <= (_Top + _Height));
        }

        public bool Contains(Rectangle value)
        {
            return (_Left <= value._Left) && (value._Left + value._Width) <= (_Left + _Width) && (_Top <= value._Top) && (value._Top + value._Height) <= (_Top + _Height);
        }

        /// <summary>
        /// Adjusts the edges of this <see cref="Rectangle"/> by specified horizontal and vertical amounts. 
        /// Rectangle top left will remain in place, and values don't have to be symetrical.
        /// </summary>
        public void Scale(float width_scale, float height_scale)
        {
            if (height_scale < float.Epsilon)
                throw new ArgumentException("Height scale must be greater than 0");

            if (width_scale < float.Epsilon)
                throw new ArgumentException("Width scale must be greater than 0");

            _Width *= width_scale;
            _Height *= height_scale;
        }

        /// <summary>
        /// Gets whether or not a specified <see cref="Rectangle"/> intersects with this <see cref="Rectangle"/>.
        /// </summary>
        public bool Intersects(Rectangle other)
        {
            return (other.Left < Right) && (other.Right > Left) && (other.Top < Bottom) && (other.Bottom > Top);
        }

        /// <summary>
        /// Gets whether or not a specified <see cref="Circle"/> intersects with this <see cref="Rectangle"/>.
        /// </summary>
        public bool Intersects(Circle circle)
        {
            Point2 rectangle_center = Center;

            float circleDistance_x = Math.Abs(circle.Center.X - rectangle_center.X);
            float circleDistance_y = Math.Abs(circle.Center.Y - rectangle_center.Y);

            if (circleDistance_x > (_Width / 2f + circle.Radius))
                return false;

            if (circleDistance_y > (_Height / 2f + circle.Radius))
                return false;

            if (circleDistance_x <= (_Width / 2f))
                return true;

            if (circleDistance_y <= (_Height / 2f))
                return true;

            float cornerDistance_sq = (circleDistance_x - _Width / 2f) * (circleDistance_x - _Width / 2f) + (circleDistance_y - _Height / 2f) * (circleDistance_y - _Height / 2f);

            return (cornerDistance_sq <= (circle.Radius * circle.Radius));
        }

        /// <summary>
        /// Creates a new <see cref="Rectangle"/> that completely contains two other rectangles.
        /// </summary>
        public static Rectangle Union(Rectangle value1, Rectangle value2)
        {
            float x = Math.Min(value1._Left, value2._Left);
            float y = Math.Min(value1._Top, value2._Top);
            return new Rectangle(x, y, Math.Max(value1.Right, value2.Right) - x, Math.Max(value1.Bottom, value2.Bottom) - y);
        }

        public static Rectangle operator +(Rectangle rectangle, Vector2 vector)
        {
            return new Rectangle(rectangle.X + vector.X, rectangle.Y + vector.Y, rectangle.Width, rectangle.Height);
        }

        public static Rectangle operator -(Rectangle rectangle, Vector2 vector)
        {
            return new Rectangle(rectangle.X - vector.X, rectangle.Y - vector.Y, rectangle.Width, rectangle.Height);
        }

        public static Rectangle operator *(Rectangle rectangle, float scale)
        {
            if (scale < 0)
                throw new ArgumentException("Scale cannot be less than 0");

            return new Rectangle(rectangle.X, rectangle.Y, rectangle.Width * scale, rectangle.Height * scale);
        }

        public static Rectangle operator /(Rectangle rectangle, float scale)
        {
            if (scale == 0f)
                throw new ArgumentException("Scale cannot be zero, divide by zero errors will occur");

            return new Rectangle(rectangle.X, rectangle.Y, rectangle.Width / scale, rectangle.Height / scale);
        }

        public static bool operator ==(Rectangle r1, Rectangle r2)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(r1, r2))
                return true;

            // If one is null, but not both, return false.
            if (((object)r1 == null) || ((object)r2 == null))
                return false;

            return (r1._Left == r2._Left) && (r1._Top == r2._Top) && (r1._Width == r2._Width) && (r1._Height == r2._Height);
        }

        public static bool operator !=(Rectangle a, Rectangle b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            var new_obj = (Rectangle)obj;
            return Equals(new_obj);
        }

        public bool Equals(Rectangle other)
        {
            return _Left == other._Left && _Top == other._Top && _Width == other._Width && _Height == other._Height;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return _Left.GetHashCode() ^ (_Top.GetHashCode() << 8) ^ (_Width.GetHashCode() << 16) ^ (_Height.GetHashCode() << 24);
            }
        }

        public override string ToString()
        {
            return $"Rectangle(X:{_Left}, Y:{_Top}, Width:{_Width}, Height:{_Height}";
        }
    }
}
