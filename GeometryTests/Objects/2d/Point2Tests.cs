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
using System;

namespace GeometryTests
{
    [TestFixture]
    public class Point2Tests
    {
        [Test]
        [Category("Point2")]
        public void TestDistance()
        {
            var p1 = new Point2();
            var p2 = new Point2(0f, 1f);
            var p3 = new Point2(0f, -1f);
            var p4 = new Point2(3f, 4f);

            Assert.That(p1.DistanceTo(p2) == 1f, Is.True);
            Assert.That(p2.DistanceTo(p3) == 2f, Is.True);
            Assert.That(p1.DistanceTo(p4) == 5f, Is.True);
        }

        [Test]
        [Category("Point2")]
        public void TestOperatorOverloads()
        {
            var p1 = new Point2(0f, 1f);
            var v1 = new Vector2(3, 3);

            var p2 = p1 + v1;

            Assert.That(p2.X == 3f && p2.Y == 4f, Is.True);
        }

        [Test]
        [Category("Point2")]
        public void TestDistance_NullInput_Fail()
        {
            var p1 = new Point2(0f, 0f);

            Assert.Throws<ArgumentNullException>((Action)(() => p1.DistanceTo(null)));
            Assert.Throws<ArgumentNullException>((Action)(() => Point2.DistanceTo(null, new Point2())));
            Assert.Throws<ArgumentNullException>((Action)(() => Point2.DistanceTo(new Point2(), null)));
        }

        [Test]
        [Category("Point2")]
        public void TestOperatorOverloads_NullVector_Fail()
        {
            var p1 = new Point2(0f, 1f);

            Assert.Throws<ArgumentNullException>((Action)(() => { var p2 = p1 + (Vector2)null; }));
            Assert.Throws<ArgumentNullException>((Action)(() => { var p2 = p1 - (Vector2)null; }));
        }

        [Test]
        [Category("Point2")]
        public void TestEqualsAndHashCode()
        {
            var p1 = new Point2(1f, 2f);
            var p2 = new Point2(1f, 2f);

            Assert.That(p1.Equals(p2), Is.True);
            Assert.That(p1.Equals((object)p2), Is.True);
            Assert.That(p1.GetHashCode(), Is.EqualTo(p2.GetHashCode()));
            Assert.That(p1.Equals((object)null), Is.False);
        }
    }
}



