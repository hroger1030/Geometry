/*
The MIT License (MIT)

Copyright (c) 2017 Roger Hill

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
    public class EllipseTests
    {
        [Test]
        [Category("Ellipse")]
        public void Ellipse_AreaPerimeterContains_Pass()
        {
            var ellipse = new Ellipse(new Point2(0f, 0f), 2f, 1f);
            Assert.That(ellipse.Area, Is.EqualTo(MathF.PI * 2f * 1f).Within(Constants.FLOAT_ERROR_MARGIN));
            Assert.That(ellipse.Contains(new Point2(1f, 0f)), Is.True);
            Assert.That(ellipse.Contains(new Point2(3f, 0f)), Is.False);
        }

        [Test]
        [Category("Ellipse")]
        public void Ellipse_InvalidRadii_Fail()
        {
            Assert.Throws<ArgumentOutOfRangeException>((Action)(() => new Ellipse(new Point2(0f, 0f), 0f, 1f)));
            Assert.Throws<ArgumentOutOfRangeException>((Action)(() => new Ellipse(new Point2(0f, 0f), 1f, 0f)));
            Assert.Throws<ArgumentOutOfRangeException>((Action)(() => new Ellipse(new Point2(0f, 0f), -1f, 1f)));
            Assert.Throws<ArgumentOutOfRangeException>((Action)(() => new Ellipse(new Point2(0f, 0f), 1f, -1f)));
        }

        [Test]
        [Category("Ellipse")]
        public void Ellipse_Perimeter_Pass()
        {
            var circleAsEllipse = new Ellipse(new Point2(0f, 0f), 2f, 2f);
            Assert.That(circleAsEllipse.Perimeter, Is.EqualTo(2f * MathF.PI * 2f).Within(Constants.FLOAT_ERROR_MARGIN));

            var ellipse = new Ellipse(new Point2(0f, 0f), 3f, 1f);
            Assert.That(ellipse.Perimeter, Is.GreaterThan(0f));
        }

        [Test]
        [Category("Ellipse")]
        public void Ellipse_Equals_Pass()
        {
            var ellipse = new Ellipse(new Point2(1f, 1f), 2f, 3f);
            var same = new Ellipse(new Point2(1f, 1f), 2f, 3f);
            var differentCenter = new Ellipse(new Point2(2f, 1f), 2f, 3f);
            var differentRadiusX = new Ellipse(new Point2(1f, 1f), 4f, 3f);
            var differentRadiusY = new Ellipse(new Point2(1f, 1f), 2f, 5f);

            Assert.That(ellipse.Equals(ellipse), Is.True);
            Assert.That(ellipse.Equals(same), Is.True);
            Assert.That(ellipse.Equals(differentCenter), Is.False);
            Assert.That(ellipse.Equals(differentRadiusX), Is.False);
            Assert.That(ellipse.Equals(differentRadiusY), Is.False);

            Assert.That(ellipse.Equals((object)same), Is.True);
            Assert.That(ellipse.Equals((object)null), Is.False);
            Assert.That(ellipse.Equals((object)"not an ellipse"), Is.False);

            Assert.That(ellipse.GetHashCode(), Is.EqualTo(same.GetHashCode()));
        }
    }
}
