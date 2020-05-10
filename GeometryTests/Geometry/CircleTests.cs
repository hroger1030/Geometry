//////////////////////////////////////////////////////////////////////////////////
// Copyright (C) Global Conquest Games, LLC - All Rights Reserved               //
// Unauthorized copying of this file, via any medium is strictly prohibited     //
// Proprietary and confidential                                                 //
//////////////////////////////////////////////////////////////////////////////////

using System;

using Geometry;
using NUnit.Framework;

namespace GeometryTests
{
    [TestFixture]
    public class CircleTests
    {
        [Test]
        [Category("Circle")]
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
        public void Circle_CirclePlusVector_Success()
        {
            var c1 = new Circle(0, 0, 2);
            var v1 = new Vector2(1f, 1f);

            Circle c2 = c1 + v1;
            Assert.IsTrue(c2.Center.X == 1f && c2.Center.Y == 1f && c2.Radius == 2f, "Failed add check");
        }

        [Test]
        [Category("Circle")]
        public void Circle_CircleMinusVector_Success()
        {
            var c1 = new Circle(0, 0, 2);
            var v1 = new Vector2(1f, 1f);

            Circle c2 = c1 - v1;
            Assert.IsTrue(c2.Center.X == -1f && c2.Center.Y == -1f && c2.Radius == 2f, "Failed subtract check");
        }


        [Test]
        [Category("Circle")]
        public void Circle_CircleTimesVector_Success()
        {
            var c1 = new Circle(0, 0, 2);
            var v1 = new Vector2(1f, 1f);

            Circle c2 = c1 * 2f;
            Assert.IsTrue(c2.Center.X == 0f && c2.Center.Y == 0f && c2.Radius == 4f, "Failed multiply check");
        }

        [Test]
        [Category("Circle")]
        public void Circle_CircleDividedByVector_Success()
        {
            var c1 = new Circle(0, 0, 2);
            var v1 = new Vector2(1f, 1f);

            Circle c2 = c1 / 2f;
            Assert.IsTrue(c2.Center.X == 0f && c2.Center.Y == 0f && c2.Radius == 1f, "Failed divide check");
        }

        [Test]
        [Category("Circle")]
        public void Circle_CircleEqualsTest_Success()
        {
            var c1 = new Circle(0, 0, 2);
            var c2 = new Circle(0, 0, 2);

            Assert.IsTrue(c1 == c2, "Failed equals check");
        }

        [Test]
        [Category("Circle")]
        public void Circle_CircleNotEqualsTest_Success()
        {
            var c1 = new Circle(0, 0, 2);
            var c2 = new Circle(1, 2, 4);

            Assert.IsTrue(c1 != c2, "Failed not equals check");
        }

        [Test]
        [Category("Circle")]
        public void TestArea()
        {
            var circle = Circle.UnitCircle;

            Assert.IsTrue(circle.Area == (float)Math.PI * circle.Radius * circle.Radius);
        }

        [Test]
        [Category("Circle")]
        public void TestPerimeter()
        {
            var circle = Circle.UnitCircle;

            Assert.IsTrue(circle.Circumfrence == (float)Math.PI * circle.Radius * 2);
        }

        [Test]
        [Category("Circle")]
        public void TestIntersectsOverlaidCircle()
        {
            // overlap
            var circle1 = Circle.UnitCircle;
            var circle2 = Circle.UnitCircle;

            Assert.IsTrue(circle1.Intersects(circle2));
        }

        [Test]
        [Category("Circle")]
        public void TestIntersectsAjacentCircle()
        {
            // doesn't intersect
            var circle1 = Circle.UnitCircle + new Vector2(3f, 0f);
            var circle2 = Circle.UnitCircle - new Vector2(3f, 0f);

            Assert.IsFalse(circle1.Intersects(circle2));
        }

        [Test]
        [Category("Circle")]
        public void TestIntersectsOverlapingCircle()
        {
            // intersects
            var circle1 = Circle.UnitCircle;
            var circle2 = Circle.UnitCircle + new Vector2(0.5f, 0f);

            Assert.IsTrue(circle1.Intersects(circle2));
        }

        [Test]
        [Category("Circle")]
        public void TestIntersectsTangentCircle()
        {
            // tangent
            var circle1 = Circle.UnitCircle + new Vector2(1f, 0f);
            var circle2 = Circle.UnitCircle - new Vector2(1f, 0f);

            Assert.IsTrue(circle1.Intersects(circle2));
        }

        [Test]
        [Category("Circle")]
        public void TestIntersectsRectangle1()
        {
            // overlap
            var rectangle1 = new Rectangle(0, 0, 1, 1);
            var rectangle2 = new Rectangle(0, 0, 1, 1);

            Assert.IsTrue(rectangle1.Intersects(rectangle2));
        }

        [Test]
        [Category("Circle")]
        public void TestIntersectsRectangle2()
        {
            // doesn't intersect
            var rectangle1 = new Rectangle(0, 0, 1, 1) + Vector2.One;
            var rectangle2 = new Rectangle(0, 0, 1, 1) - Vector2.One;

            Assert.IsFalse(rectangle1.Intersects(rectangle2));
        }
    }
}
