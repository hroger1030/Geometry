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
    public class TriangleTests
    {
        [Test]
        public void Triangle_AreaPerimeterType_Pass()
        {
            var equilateral = new Triangle2(new Point2(0f, 0f), new Point2(1f, 0f), new Point2(0.5f, MathF.Sqrt(3) / 2f));

            Assert.That(equilateral.TriangleType, Is.EqualTo(Triangle2.Type.Equilateral));
            Assert.That(equilateral.Perimeter, Is.EqualTo(3f));
            Assert.That(equilateral.Area, Is.Positive);

            var isosceles = new Triangle2(new Point2(0f, 0f), new Point2(2f, 0f), new Point2(1f, 1f));
            Assert.That(isosceles.TriangleType, Is.EqualTo(Triangle2.Type.Isosceles));

            var scalene = new Triangle2(new Point2(0f, 0f), new Point2(2f, 0f), new Point2(0f, 3f));
            Assert.That(scalene.TriangleType, Is.EqualTo(Triangle2.Type.Scalene));
        }

        [Test]
        public void Triangle_NullPoint_Fail()
        {
            Assert.Throws<ArgumentNullException>((Action)(() => new Triangle2(null, new Point2(1f, 0f), new Point2(0f, 1f))));
            Assert.Throws<ArgumentNullException>((Action)(() => new Triangle2(new Point2(0f, 0f), null, new Point2(0f, 1f))));
            Assert.Throws<ArgumentNullException>((Action)(() => new Triangle2(new Point2(0f, 0f), new Point2(1f, 0f), null)));
        }

        [Test]
        public void Triangle_DuplicatePoint_Fail()
        {
            var p = new Point2(0f, 0f);
            Assert.Throws<ArgumentException>((Action)(() => new Triangle2(p, p, new Point2(1f, 0f))));
        }
    }
}



