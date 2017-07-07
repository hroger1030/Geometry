//////////////////////////////////////////////////////////////////////////////////
// Copyright (C) Global Conquest Games, LLC - All Rights Reserved               //
// Unauthorized copying of this file, via any medium is strictly prohibited     //
// Proprietary and confidential                                                 //
//////////////////////////////////////////////////////////////////////////////////

using Geometry;
using Newtonsoft.Json;
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
            Rectangle r = new Rectangle(0, 0, 2, 3);

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
            Rectangle r = new Rectangle(0, 0, 10, 10);

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
            Rectangle r = new Rectangle(0, 0, 10, 10);

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
        public void TestCircleIntersectsGeometry()
        {
            Rectangle r = new Rectangle(0, 0, 10, 10);

            // circle inside rectangle
            Assert.IsTrue(r.Intersects(new Circle(5, 5, 1)), "Failed intersect check 1");

            // circle outside
            Assert.IsFalse(r.Intersects(new Circle(5, 15, 1)), "Failed intersect check 2.0");
            Assert.IsFalse(r.Intersects(new Circle(15, 5, 1)), "Failed intersect check 2.1");
            Assert.IsFalse(r.Intersects(new Circle(-5, 5, 1)), "Failed intersect check 2.2");
            Assert.IsFalse(r.Intersects(new Circle(5, -5, 1)), "Failed intersect check 2.3");

            // circle inside & tangent
            Assert.IsTrue(r.Intersects(new Circle(5, 5, 5)), "Failed intersect check 3");

            // circle outside & tangent
            Assert.IsTrue(r.Intersects(new Circle(5, 15, 5)), "Failed intersect check 4");

            // rectangle inside circle
            Assert.IsTrue(r.Intersects(new Circle(5, 5, 20)), "Failed intersect check 5");
        }

        [Test]
        [Category("Rectangle")]
        public void TestScale()
        {
            Rectangle r = new Rectangle(0, 0, 2, 3);
            r.Scale(1.5f, 2f);

            Assert.IsTrue(r.Width == 3f, "Failed width check");
            Assert.IsTrue(r.Height == 6f, "Failed height check");
        }

        [Test]
        [Category("Rectangle")]
        public void TestUnion()
        {
            Rectangle r1 = new Rectangle(0, 0, 2, 2);
            Rectangle r2 = new Rectangle(1, 1, 2, 2);
            Rectangle r3 = Rectangle.Union(r1, r2);

            Assert.IsTrue(r3.X == 0f, "Failed X check");
            Assert.IsTrue(r3.Y == 0f, "Failed Y check");
            Assert.IsTrue(r3.Width == 3f, "Failed width check");
            Assert.IsTrue(r3.Height == 3f, "Failed height check");
        }

        [Test]
        [Category("Rectangle")]
        public void TestOperatorOverloads()
        {
            Rectangle r1 = new Rectangle(0, 0, 2, 3);
            Vector2 v1 = new Vector2(1f, 1f);
            Rectangle r2;

            r2 = r1 + v1;
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
        public void TestSerialization()
        {
            Rectangle r1 = new Rectangle(0, 0, 2, 3);
            string output = JsonConvert.SerializeObject(r1);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(output), "Failed to serialize");

            Rectangle r2 = JsonConvert.DeserializeObject<Rectangle>(output);
            Assert.IsTrue(r1 == r2, "Failed to deserialize");
        }
    }
}
