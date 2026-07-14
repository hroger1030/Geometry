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
using System.Collections.Generic;
using Geometry;
using NUnit.Framework;

namespace GeometryTests
{
    [TestFixture]
    public class PolygonTests
    {
        [Test]
        public void Polygon_AreaPerimeterAndContainment_Pass()
        {
            var polygon = new Polygon(new List<Point2>
            {
                new Point2(0f, 0f),
                new Point2(2f, 0f),
                new Point2(2f, 2f),
                new Point2(0f, 2f),
            });

            Assert.That(polygon.Sides, Is.EqualTo(4));
            Assert.That(polygon.Area, Is.EqualTo(4f));
            Assert.That(polygon.Perimeter, Is.EqualTo(8f));
            Assert.That(polygon.Contains(new Point2(1f, 1f)), Is.True);
            Assert.That(polygon.Contains(new Point2(3f, 3f)), Is.False);
        }

        [Test]
        public void Polygon_OperatorTranslation_Pass()
        {
            var polygon = new Polygon(new List<Point2>
            {
                new Point2(0f, 0f),
                new Point2(1f, 0f),
                new Point2(1f, 1f),
            });

            var translated = polygon + new Vector2(1f, 1f);
            Assert.That(translated.Contains(new Point2(1.1f, 1.1f)), Is.True);

            var shiftedBack = translated - new Vector2(1f, 1f);
            Assert.That(shiftedBack.Equals(polygon), Is.True);
        }

        [Test]
        public void Polygon_ScalingOperators_Pass()
        {
            var polygon = new Polygon(new List<Point2>
            {
                new Point2(1f, 1f),
                new Point2(2f, 1f),
                new Point2(2f, 2f),
            });

            var scaled = polygon * 2f;
            Assert.That(scaled.Vertices[0].X, Is.EqualTo(2f));
            Assert.That(scaled.Vertices[0].Y, Is.EqualTo(2f));
            Assert.That(scaled.Vertices[2].X, Is.EqualTo(4f));

            var half = scaled / 2f;
            Assert.That(half.Equals(polygon), Is.True);
        }

        [Test]
        public void Polygon_ScalingOperatorDivideByZero_Fail()
        {
            var polygon = new Polygon(new List<Point2>
            {
                new Point2(1f, 1f),
                new Point2(2f, 1f),
                new Point2(2f, 2f),
            });

            Assert.Throws<DivideByZeroException>((Action)(() => { var _ = polygon / 0f; }));
        }

        [Test]
        public void Polygon_NullConstructor_Fail()
        {
            Assert.Throws<ArgumentNullException>((Action)(() => new Polygon(null)));
        }
    }
}



