//////////////////////////////////////////////////////////////////////////////////
// Copyright (C) Global Conquest Games, LLC - All Rights Reserved               //
// Unauthorized copying of this file, via any medium is strictly prohibited     //
// Proprietary and confidential                                                 //
//////////////////////////////////////////////////////////////////////////////////

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Geometry;
using Newtonsoft.Json;

namespace GeometryTests
{
    [TestClass]
    public class CircleTests
    {
        [TestMethod]
        [TestCategory("Circle")]
        public void TestBasicProperties()
        {
            var c = new Circle(0,0,2);

            Assert.IsTrue(c.Right == 2f, "Failed right side check");
            Assert.IsTrue(c.Bottom == 2f, "Failed bottom side check");
            Assert.IsTrue(c.Top == -2f, "Failed top side check");
            Assert.IsTrue(c.Left == -2f, "Failed left side check");

            Assert.IsTrue(c.Radius == 2f, "Failed radius check");
            Assert.IsTrue(c.Center.X == 0f, "Failed center X check");
            Assert.IsTrue(c.Center.Y == 0f, "Failed center Y check");

            //Assert.IsTrue(c.Circumfrence == 4f, "Failed circumfrence check");
            //Assert.IsTrue(c.Area == 6f, "Failed area check");
        }

        [TestMethod]
        [TestCategory("Circle")]
        public void TestOperatorOverloads()
        {
            var c1 = new Circle(0,0,2);
            var v1 = new Vector2(1f,1f);
            Circle c2;

            c2 = c1 + v1;
            Assert.IsTrue(c2.Center.X == 1f && c2.Center.Y == 1f && c2.Radius == 2f, "Failed add check");

            c2 = c1 - v1;
            Assert.IsTrue(c2.Center.X == -1f && c2.Center.Y == -1f && c2.Radius == 2f, "Failed subtract check");

            c2 = c1 * 2f;
            Assert.IsTrue(c2.Center.X == 0f && c2.Center.Y == 0f && c2.Radius == 4f, "Failed multiply check");

            c2 = c1 / 2f;
            Assert.IsTrue(c2.Center.X == 0f && c2.Center.Y == 0f && c2.Radius == 1f, "Failed divide check");

            c2 = new Circle(0,0,2);
            Assert.IsTrue(c1 == c2, "Failed equals check");

            c2 = new Circle(1,2,4);
            Assert.IsTrue(c1 != c2, "Failed not equals check");
        }

        [TestMethod]
        [TestCategory("Circle")]
        public void TestSerialization()
        {
            var c1 = new Circle(0,0,2);

            string output = JsonConvert.SerializeObject(c1);  
            Assert.IsTrue(!string.IsNullOrWhiteSpace(output), "Failed to serialize");

            Circle c2 = JsonConvert.DeserializeObject<Circle>(output);
            Assert.IsTrue(c1 == c2, "Failed to deserialize");
        }
    }
}
