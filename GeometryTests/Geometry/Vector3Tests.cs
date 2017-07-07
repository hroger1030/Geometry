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
    public class Vector3Tests
    {
        [Test]
        [Category("Vector3")]
        public void TestOperatorOverloads()
        {
            Vector3 v1 = new Vector3(3, 3, 3);
            Vector3 v2 = new Vector3(1, 2, 3);
            Vector3 v3;

            v3 = v1 + v2;
            Assert.IsTrue(v3.X == 4f && v3.Y == 5 && v3.Z == 6, "Failed addition");

            v3 = v1 - v2;
            Assert.IsTrue(v3.X == 2f && v3.Y == 1 && v3.Z == 0, "Failed subtraction");

            v3 = v1 * 2;
            Assert.IsTrue(v3.X == 6f && v3.Y == 6 && v3.Z == 6, "Failed multiplication");

            v3 = v1 / 2;
            Assert.IsTrue(v3.X == 1.5f && v3.Y == 1.5 && v3.Z == 1.5, "Failed division");
        }

        [Test]
        [Category("Vector2")]
        public void TestSerialization()
        {
            Vector3 v1 = new Vector3(0f, 1f, 2f);

            string output = JsonConvert.SerializeObject(v1);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(output), "Failed to serialize");

            Vector3 v2 = JsonConvert.DeserializeObject<Vector3>(output);
            Assert.IsTrue(v1 == v2, "Failed to deserialize");
        }
    }
}
