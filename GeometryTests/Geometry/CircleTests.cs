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

            Assert.That(c.Right == 2f, Is.True, "Failed right side check");
            Assert.That(c.Bottom == 2f, Is.True, "Failed bottom side check");
            Assert.That(c.Top == -2f, Is.True, "Failed top side check");
            Assert.That(c.Left == -2f, Is.True, "Failed left side check");

            Assert.That(c.Radius == 2f, Is.True, "Failed radius check");
            Assert.That(c.Center.X == 0f, Is.True, "Failed center X check");
            Assert.That(c.Center.Y == 0f, Is.True, "Failed center Y check");
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

            Assert.That(c2.Center.X == (0f + x), Is.True, "Failed add check");
            Assert.That(c2.Center.Y == (0f + y), Is.True, "Failed add check");
            Assert.That(c2.Radius == 2f, Is.True, "Failed add check");
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

            Assert.That(c2.Center.X == (0f - x), Is.True, "Failed add check");
            Assert.That(c2.Center.Y == (0f - y), Is.True, "Failed add check");
            Assert.That(c2.Radius == 2f, Is.True, "Failed add check");
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

            Assert.That(c2.Center.X == x, Is.True, "Failed multiply check");
            Assert.That(c2.Center.Y == y, Is.True, "Failed multiply check");
            Assert.That(c2.Radius == (2f * scale), Is.True, "Failed multiply check");
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

            Assert.That(c2.Center.X == 0f && c2.Center.Y == 0f && c2.Radius == 1f, Is.True, "Failed scale check");
        }

        [Test]
        [Category("Circle")]
        [Category("Math")]
        public void Circle_CircleEqualsTest_Pass()
        {
            var c1 = new Circle(0, 0, 2f);
            var c2 = new Circle(0, 0, 2f);

            Assert.That(c1.Equals(c2), Is.True, "Failed equals check");
        }

        [Test]
        [Category("Circle")]
        [Category("Math")]
        public void Circle_CircleNotEqualsTest_Fail()
        {
            var c1 = new Circle(0, 0, 2f);
            var c2 = new Circle(1, 2, 4f);

            Assert.That(c1.Equals(c2), Is.False, "Failed not equals check");
        }

        [Test]
        [Category("Circle")]
        [Category("Math")]
        public void TestArea()
        {
            var c = Circle.UnitCircle;

            Assert.That(c.Area == (float)Math.PI * c.Radius * c.Radius, Is.True);
        }

        [Test]
        [Category("Circle")]
        [Category("Math")]
        public void TestPerimeter()
        {
            var c = Circle.UnitCircle;

            Assert.That(c.Circumfrence == (float)Math.PI * c.Radius * 2, Is.True);
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

            Assert.That(c1.Intersects(c2), Is.True);
        }

        [Test]
        [Category("Circle")]
        [Category("Geometry")]
        [TestCase(0f, 0f, true)] // center
        [TestCase(0.5f, 0.5f, true)] // inside
        [TestCase(1f, 0f, true)] // tangent
        [TestCase(-1f, 0f, true)] // tangent
        [TestCase(1f, 1f, false)] // outside corner
        [TestCase(100f, 100f, false)] // far distant
        public void Circle_ContainsPoints_Pass(float x, float y, bool expectedResult)
        {
            var c1 = new Circle(0, 0, 1);
            var p1 = new Point2(x, y);

            Assert.That(c1.Contains(p1) == expectedResult, Is.True);
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

            Assert.That(c1.Intersects(c2), Is.False);
        }
    }
}
