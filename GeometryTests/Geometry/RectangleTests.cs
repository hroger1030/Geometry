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

using Geometry;
using NUnit.Framework;

namespace GeometryTests
{
    [TestFixture]
    public class RectangleTests
    {
        [Test]
        [Category("Rectangle")]
        public void TestBasicProperties()
        {
            var r = new Rectangle(0, 0, 2, 3);

            Assert.IsTrue(r.Top == 0f, "Failed top check");
            Assert.IsTrue(r.Left == 0f, "Failed left check");

            Assert.IsTrue(r.Width == 2f, "Failed width check");
            Assert.IsTrue(r.Height == 3f, "Failed height check");

            Assert.IsTrue(r.Perimeter == 10f, "Failed perimeter check");
            Assert.IsTrue(r.Area == 6f, "Failed area check");

            Assert.IsTrue(r.Right == 2f, "Failed right side check");
            Assert.IsTrue(r.Bottom == 3f, "Failed bottom side check");
            Assert.IsTrue(r.Top == 0f, "Failed top side check");
            Assert.IsTrue(r.Left == 0f, "Failed left side check");

            Assert.IsTrue(r.Center.X == 1f, "Failed center X check");
            Assert.IsTrue(r.Center.Y == 1.5f, "Failed center X check");
        }

        [Test]
        [Category("Rectangle")]
        public void TestContainsGeometry()
        {
            var r = new Rectangle(0, 0, 10, 10);

            // point inside
            Assert.IsTrue(r.Contains(new Point2(3, 3)), "Failed contained check 1");

            // point outside
            Assert.IsFalse(r.Contains(new Point2(12, 12)), "Failed contained check 2");

            // points on border
            Assert.IsTrue(r.Contains(new Point2(0, 0)), "Failed contained check 3");
            Assert.IsTrue(r.Contains(new Point2(10, 10)), "Failed contained check 4");

            // rectangle inside
            Assert.IsTrue(r.Contains(new Rectangle(2, 2, 2, 2)), "Failed contained check 5");

            // rectangle outside
            Assert.IsFalse(r.Contains(new Rectangle(-5, -5, 2, 2)), "Failed contained check 6");

            // rectangle outside & surrounds
            Assert.IsFalse(r.Contains(new Rectangle(-50, -50, 200, 200)), "Failed contained check 7");

            // rectangle inside & tangent
            Assert.IsTrue(r.Contains(new Rectangle(0, 0, 2, 2)), "Failed contained check 8");

            // rectangle outside & tangent
            Assert.IsFalse(r.Contains(new Rectangle(-2, -2, 2, 2)), "Failed contained check 9");

            // rectangle intersects
            Assert.IsFalse(r.Contains(new Rectangle(-2, -2, 10, 10)), "Failed contained check 10");
        }

        [Test]
        [Category("Rectangle")]
        public void TestRectangleIntersectsGeometry()
        {
            var r = new Rectangle(0, 0, 10, 10);

            // rectangle intersects
            Assert.IsTrue(r.Intersects(new Rectangle(-2, -2, 10, 10)), "Failed intersect check 1");

            // rectangle inside
            Assert.IsTrue(r.Intersects(new Rectangle(2, 2, 2, 2)), "Failed intersect check 2");

            // rectangle outside
            Assert.IsFalse(r.Intersects(new Rectangle(-5, -5, 2, 2)), "Failed intersect check 3");

            // rectangle outside &surrounds
            Assert.IsTrue(r.Intersects(new Rectangle(-50, -50, 200, 200)), "Failed intersect check 4");

            // rectangle inside &tangent
            Assert.IsTrue(r.Intersects(new Rectangle(0, 0, 2, 2)), "Failed intersect check 5");

            // rectangle outside &tangent
            Assert.IsFalse(r.Intersects(new Rectangle(-2, -2, 2, 2)), "Failed intersect check 6");
        }

        [Test]
        [Category("Rectangle")]
        [Category("Geometry")]
        [TestCase(0f, 0f, 6f)] // overlapping
        [TestCase(0f, 0f, 2f)] // offset
        [TestCase(5f, 5f, 1f)] // contained
        [TestCase(5f, 5f, 25f)] // containing
        [TestCase(-1f, 5f, 1f)] // tangent
        public void Rectangle_RectangleCircleIntersection_Pass(float x, float y, float radius)
        {
            var r = new Rectangle(0, 0, 10, 10);
            var c = new Circle(x, y, radius);

            Assert.IsTrue(r.Intersects(c), "Failed intersect check 1");
        }

        [Test]
        [Category("Rectangle")]
        [Category("Math")]
        [TestCase(1f, 1f)] 
        [TestCase(0.5f, 0.5f)] 
        public void Rectangle_Scale_Pass(float x, float y)
        {
            var r = new Rectangle(0f, 0f, 2f, 2f);
            r.Scale(x, y);

            Assert.IsTrue(r.Width == (2f * x), "Failed width check");
            Assert.IsTrue(r.Height == (2f * y), "Failed height check");
        }

        [Test]
        [Category("Rectangle")]
        public void TestUnion()
        {
            var r1 = new Rectangle(0, 0, 2, 2);
            var r2 = new Rectangle(1, 1, 2, 2);
            var r3 = Rectangle.Union(r1, r2);

            Assert.IsTrue(r3.X == 0f, "Failed X check");
            Assert.IsTrue(r3.Y == 0f, "Failed Y check");
            Assert.IsTrue(r3.Width == 3f, "Failed width check");
            Assert.IsTrue(r3.Height == 3f, "Failed height check");
        }

        [Test]
        [Category("Rectangle")]
        public void TestOperatorOverloads()
        {
            var r1 = new Rectangle(0, 0, 2, 3);
            var v1 = new Vector2(1f, 1f);

            var r2 = r1 + v1;
            Assert.IsTrue(r2.Top == 1f && r2.Top == 1f && r2.Height == 3f && r2.Width == 2f, "Failed add check");

            r2 = r1 - v1;
            Assert.IsTrue(r2.Top == -1f && r2.Top == -1f && r2.Height == 3f && r2.Width == 2f, "Failed subtract check");

            r2 = r1 * 3f;
            Assert.IsTrue(r2.Top == 0f && r2.Top == 0f && r2.Height == 9f && r2.Width == 6f, "Failed multiply check");

            r2 = r1 / 2f;
            Assert.IsTrue(r2.Top == 0f && r2.Top == 0f && r2.Height == 1.5f && r2.Width == 1f, "Failed divide check");

            r2 = new Rectangle(0, 0, 2, 3);
            Assert.IsTrue(r1 == r2, "Failed equals check");

            r2 = new Rectangle(1, 2, 4, 5);
            Assert.IsTrue(r1 != r2, "Failed not equals check");
        }

        [Test]
        [Category("Rectangle")]
        public void TestIntersectsRectangle1()
        {
            // overlap
            var r1 = new Rectangle(0, 0, 1, 1);
            var r2 = new Rectangle(0, 0, 1, 1);

            Assert.IsTrue(r1.Intersects(r2));
        }

        [Test]
        [Category("Rectangle")]
        public void TestIntersectsRectangle2()
        {
            // doesn't intersect
            var rectangle1 = new Rectangle(0, 0, 1, 1) + Vector2.One;
            var rectangle2 = new Rectangle(0, 0, 1, 1) - Vector2.One;

            Assert.IsFalse(rectangle1.Intersects(rectangle2));
        }
    }
}
