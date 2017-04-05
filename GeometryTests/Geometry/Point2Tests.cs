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
    public class Point2Tests
    {
        [TestMethod]
        [TestCategory("Point2")]
        public void TestDistance()
        {
            Point2 p1 = new Point2();
            Point2 p2 = new Point2(0f,1f);
            Point2 p3 = new Point2(0f,-1f);
            Point2 p4 = new Point2(3f,4f);

            Assert.IsTrue(p1.DistanceTo(p2) == 1f);
            Assert.IsTrue(p2.DistanceTo(p3) == 2f);
            Assert.IsTrue(p1.DistanceTo(p4) == 5f);
        }

        [TestMethod]
        [TestCategory("Point2")]
        public void TestOperatorOverloads()
        {
            Point2 p1 = new Point2(0f,1f);
            Vector2 v1 = new Vector2(3,3);

            Point2 p2 = p1 + v1;

            Assert.IsTrue(p2.X == 3f && p2.Y == 4f);
        }

        [TestMethod]
        [TestCategory("Point2")]
        public void TestSerialization()
        {
            Point2 p1 = new Point2(0f,1f);

            string output = JsonConvert.SerializeObject(p1);  
            Assert.IsTrue(!string.IsNullOrWhiteSpace(output), "Failed to serialize");

            Point2 p2 = JsonConvert.DeserializeObject<Point2>(output);
            Assert.IsTrue(p1 == p2, "Failed to deserialize");
        }
    }
}
