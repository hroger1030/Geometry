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
    public class Vector3Tests
    {
        [Test]
        [Category("Vector3")]
        public void TestDistanceOperations()
        {
            Vector3 v1;
            float distance;

            v1 = new Vector3(0, 0, 3);
            distance = Vector3.Zero.DistanceTo(v1);
            Assert.IsTrue(distance == 3f, "Expected distance of 3");

            v1 = new Vector3(3, 4, 0);
            distance = Vector3.Zero.DistanceTo(v1);
            Assert.IsTrue(distance == 5f, "Expected distance of 5");

            v1 = new Vector3(-1, 0, 0);
            distance = Vector3.Zero.DistanceTo(v1);
            Assert.IsTrue(distance == 1f, "Expected distance of 1");

            v1 = Vector3.Zero;
            distance = Vector3.Zero.DistanceTo(v1);
            Assert.IsTrue(distance == 0f, "Expected distance of 0");

            v1 = new Vector3(float.NaN, 0, 0);
            distance = Vector3.Zero.DistanceTo(v1);
            Assert.IsTrue(float.IsNaN(distance), "Expected distance of float.NaN");

            v1 = new Vector3(float.PositiveInfinity, 0, 0);
            distance = Vector3.Zero.DistanceTo(v1);
            Assert.IsTrue(float.IsPositiveInfinity(distance), "Expected distance of float.PositiveInfinity");

            v1 = new Vector3(float.NegativeInfinity, 0, 0);
            distance = Vector3.Zero.DistanceTo(v1);
            Assert.IsTrue(float.IsPositiveInfinity(distance), "Expected distance of float.PositiveInfinity");
        }

        [Test]
        [Category("Vector3")]
        public void TestOperatorOverloads()
        {
            var v1 = new Vector3(3, 3, 3);
            var v2 = new Vector3(1, 2, 3);

            var v3 = v1 + v2;
            Assert.IsTrue(v3.X == 4f && v3.Y == 5 && v3.Z == 6, "Failed addition");

            v3 = v1 - v2;
            Assert.IsTrue(v3.X == 2f && v3.Y == 1 && v3.Z == 0, "Failed subtraction");

            v3 = v1 * 2;
            Assert.IsTrue(v3.X == 6f && v3.Y == 6 && v3.Z == 6, "Failed multiplication");

            v3 = v1 / 2;
            Assert.IsTrue(v3.X == 1.5f && v3.Y == 1.5 && v3.Z == 1.5, "Failed division");
        }
    }
}
