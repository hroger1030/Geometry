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
    public class PlaneTests
    {
        [Test]
        public void Plane_DistanceAndNormalize_Pass()
        {
            var plane = new Plane3(new Vector3(0f, 0f, 1f), -5f);
            Assert.That(plane.DistanceTo(new Point3(0f, 0f, 10f)), Is.EqualTo(5f).Within(Constants.FLOAT_ERROR_MARGIN));
            var normalized = plane.Normalize();

            Assert.That(MathF.Abs(normalized.Normal.X - 0f), Is.LessThan(Constants.FLOAT_ERROR_MARGIN));
            Assert.That(MathF.Abs(normalized.Normal.Z - 1f), Is.LessThan(Constants.FLOAT_ERROR_MARGIN));
        }

        [Test]
        public void Plane_ZeroNormal_Fail()
        {
            Assert.Throws<ArgumentException>((Action)(() => new Plane3(new Vector3(0f, 0f, 0f), 0f)));
        }
    }
}



