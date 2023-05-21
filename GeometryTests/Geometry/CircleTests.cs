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
using System;
using System.Security.Cryptography.X509Certificates;

namespace GeometryTests
{
    [TestFixture]
    public class CircleTests
    {
        [Test]
        [Category("Circle")]
        [Category("CTOR")]
        [TestCase(-2f)]
        [TestCase(0f)]
        [TestCase(-3.14159f)]
        public void Circle_TestNegativeRadius_Fail(float radius)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Circle(0, 0, radius));
        }

        [Test]
        [Category("Circle")]
        [Category("CTOR")]
        public void Circle_TestBasicProperties_Pass()
        {
            var c = new Circle(0, 0, 2);

            Assert.IsTrue(c.Right == 2f, "Failed right side check");
            Assert.IsTrue(c.Bottom == 2f, "Failed bottom side check");
            Assert.IsTrue(c.Top == -2f, "Failed top side check");
            Assert.IsTrue(c.Left == -2f, "Failed left side check");

            Assert.IsTrue(c.Radius == 2f, "Failed radius check");
            Assert.IsTrue(c.Center.X == 0f, "Failed center X check");
            Assert.IsTrue(c.Center.Y == 0f, "Failed center Y check");
        }

        [Test]
        [Category("Circle")]
        [Category("Math")]
        [TestCase(1f, 1f)]
        [TestCase(0f, 0f)]
        [TestCase(-1.5f, -1.9f)]
        public void Circle_CirclePositiveTranslation_Pass(float x, float y)
        {
            var c1 = new Circle(0, 0, 2);
            var v1 = new Vector2(x, y);

            Circle c2 = c1 + v1;

            Assert.IsTrue(c2.Center.X == (0f + x), "Failed add check");
            Assert.IsTrue(c2.Center.Y == (0f + y), "Failed add check");
            Assert.IsTrue(c2.Radius == 2f, "Failed add check");
        }

        [Test]
        [Category("Circle")]
        [Category("Math")]
        [TestCase(1f, 1f)]
        [TestCase(0f, 0f)]
        [TestCase(-1.5f, -1.9f)]
        public void Circle_CircleNegativeTranslation_Pass(float x, float y)
        {
            var c1 = new Circle(0, 0, 2);
            var v1 = new Vector2(x, y);

            Circle c2 = c1 - v1;

            Assert.IsTrue(c2.Center.X == (0f - x), "Failed add check");
            Assert.IsTrue(c2.Center.Y == (0f - y), "Failed add check");
            Assert.IsTrue(c2.Radius == 2f, "Failed add check");
        }


        [Test]
        [Category("Circle")]
        [Category("Math")]
        [TestCase(0f, 0f, 2f)]
        [TestCase(-1.5f, -1.9f, 3f)]
        [TestCase(1f, 1f, 0.001f)]
        public void Circle_CircleTimesVector_Pass(float x, float y, float scale)
        {
            var c1 = new Circle(x, y, 2f);

            Circle c2 = c1 * scale;

            Assert.IsTrue(c2.Center.X == x, "Failed multiply check");
            Assert.IsTrue(c2.Center.Y == y, "Failed multiply check");
            Assert.IsTrue(c2.Radius == (2f * scale), "Failed multiply check");
        }

        [Test]
        [Category("Circle")]
        [Category("Math")]
        [TestCase(0f)] // sets radius to 0
        [TestCase(-10f)] // sets radius to negative
        public void Circle_CircleTimesVector_Fail(float scale)
        {
            var c1 = new Circle(0, 0, 2f);

            Assert.Throws<ArgumentOutOfRangeException>(() => c1 *= scale);
        }

        [Test]
        [Category("Circle")]
        [Category("Math")]
        public void Circle_CircleScaled_Pass()
        {
            var c1 = new Circle(0, 0, 2f);
            var c2 = c1 / 2f;

            Assert.IsTrue(c2.Center.X == 0f && c2.Center.Y == 0f && c2.Radius == 1f, "Failed scale check");
        }

        [Test]
        [Category("Circle")]
        [Category("Math")]
        public void Circle_CircleEqualsTest_Pass()
        {
            var c1 = new Circle(0, 0, 2f);
            var c2 = new Circle(0, 0, 2f);

            Assert.IsTrue(c1.Equals(c2), "Failed equals check");
        }

        [Test]
        [Category("Circle")]
        [Category("Math")]
        public void Circle_CircleNotEqualsTest_Fail()
        {
            var c1 = new Circle(0, 0, 2f);
            var c2 = new Circle(1, 2, 4f);

            Assert.IsFalse(c1.Equals(c2), "Failed not equals check");
        }

        [Test]
        [Category("Circle")]
        [Category("Math")]
        public void TestArea()
        {
            var c = Circle.UnitCircle;

            Assert.IsTrue(c.Area == (float)Math.PI * c.Radius * c.Radius);
        }

        [Test]
        [Category("Circle")]
        [Category("Math")]
        public void TestPerimeter()
        {
            var c = Circle.UnitCircle;

            Assert.IsTrue(c.Circumfrence == (float)Math.PI * c.Radius * 2);
        }

        [Test]
        [Category("Circle")]
        [Category("Geometry")]
        [TestCase(0f, 0f, 2f, 0f, 0f, 2f)] // overlapping
        [TestCase(0f, 0f, 2f, 1f, 1f, 2f)] // offset
        [TestCase(0f, 0f, 1f, 0f, 0f, 2f)] // contained
        [TestCase(0f, 0f, 2f, 0f, 0f, 1f)] // containing
        [TestCase(0f, 0f, 1f, 0f, 2f, 1f)] // tangent
        public void Circle_IntersectsCircle_Pass(float x1, float y1, float r1, float x2, float y2, float r2)
        {
            var c1 = new Circle(x1, y1, r1);
            var c2 = new Circle(x2, y2, r2);

            Assert.IsTrue(c1.Intersects(c2));
        }

        [Test]
        [Category("Circle")]
        [Category("Geometry")]
        [TestCase(0f, 0f, 2f, 9f, 9f, 2f)] // remote
        [TestCase(0f, 0f, 1f, 2.01f, 0f, 1f)] // close
        public void Circle_IntersectsAjacentCircle_Fail(float x1, float y1, float r1, float x2, float y2, float r2)
        {
            var c1 = new Circle(x1, y1, r1);
            var c2 = new Circle(x2, y2, r2);

            Assert.IsFalse(c1.Intersects(c2));
        }
    }
}
