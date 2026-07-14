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
    public class RayTests
    {
        [Test]
        public void Ray_PointAtAndSphereIntersection_Pass()
        {
            var ray = new Ray(new Point3(0f, 0f, -5f), new Vector3(0f, 0f, 1f));
            Assert.That(ray.Direction.Length(), Is.EqualTo(1f).Within(Constants.FLOAT_ERROR_MARGIN));
            Assert.That(ray.PointAt(5f), Is.EqualTo(new Point3(0f, 0f, 0f)));

            var sphere = new Sphere(new Point3(0f, 0f, 3f), 1f);
            Assert.That(ray.Intersects(sphere, out float distance), Is.True);
            Assert.That(distance, Is.EqualTo(7f).Within(Constants.FLOAT_ERROR_MARGIN));
        }

        [Test]
        public void Ray_IntersectsAABB_ReturnsExpectedDistance()
        {
            var ray = new Ray(new Point3(0f, 0f, -5f), new Vector3(0f, 0f, 1f));
            var box = new AABB(new Point3(-1f, -1f, -1f), new Point3(1f, 1f, 1f));

            Assert.That(ray.Intersects(box, out float distance), Is.True);
            Assert.That(distance, Is.EqualTo(4f).Within(Constants.FLOAT_ERROR_MARGIN));
        }

        [Test]
        public void Ray_IntersectsCube_ReturnsExpectedDistance()
        {
            var ray = new Ray(new Point3(0f, 0f, -5f), new Vector3(0f, 0f, 1f));
            var cube = new Cube(new Point3(-1f, -1f, -1f), new Point3(1f, 1f, 1f));

            Assert.That(ray.Intersects(cube, out float distance), Is.True);
            Assert.That(distance, Is.EqualTo(4f).Within(Constants.FLOAT_ERROR_MARGIN));
        }

        [Test]
        public void Ray_IntersectsPlane_ReturnsExpectedDistance()
        {
            var ray = new Ray(new Point3(0f, 0f, -5f), new Vector3(0f, 0f, 1f));
            var plane = new Plane3(new Vector3(0f, 0f, 1f), 0f);

            Assert.That(ray.Intersects(plane, out float distance), Is.True);
            Assert.That(distance, Is.EqualTo(5f).Within(Constants.FLOAT_ERROR_MARGIN));
        }
    }
}


