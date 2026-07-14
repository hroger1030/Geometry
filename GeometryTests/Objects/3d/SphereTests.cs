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

using System;
using Geometry;
using NUnit.Framework;

namespace GeometryTests
{
    [TestFixture]
    public class SphereTests
    {
        [Test]
        public void Sphere_AreaVolumeContainsIntersects_Pass()
        {
            var sphere = new Sphere(new Point3(0f, 0f, 0f), 2f);
            Assert.That(sphere.Volume, Is.EqualTo((4f / 3f) * MathF.PI * 8f).Within(Constants.FLOAT_ERROR_MARGIN));
            Assert.That(sphere.SurfaceArea, Is.EqualTo(4f * MathF.PI * 4f).Within(Constants.FLOAT_ERROR_MARGIN));
            Assert.That(sphere.Contains(new Point3(1f, 0f, 0f)), Is.True);
            Assert.That(sphere.Contains(new Point3(3f, 0f, 0f)), Is.False);

            var nearby = new Sphere(new Point3(3f, 0f, 0f), 1.5f);
            Assert.That(sphere.Intersects(nearby), Is.True);
            Assert.That(sphere.Intersects(new Sphere(new Point3(5f, 0f, 0f), 1f)), Is.False);
        }

        [Test]
        public void Sphere_InvalidRadius_Fail()
        {
            Assert.Throws<ArgumentOutOfRangeException>((Action)(() => new Sphere(new Point3(0f, 0f, 0f), 0f)));
            Assert.Throws<ArgumentOutOfRangeException>((Action)(() => new Sphere(new Point3(0f, 0f, 0f), -1f)));
        }
    }
}



