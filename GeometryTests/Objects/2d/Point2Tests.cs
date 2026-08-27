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
        public void TestEqualsAndHashCode()
        {
            var p1 = new Point2(1f, 2f);
            var p2 = new Point2(1f, 2f);
            var different = new Point2(9f, 2f);

            Assert.That(p1.Equals(p1), Is.True);
            Assert.That(p1.Equals(p2), Is.True);
            Assert.That(p1.Equals(different), Is.False);

            Assert.That(p1.Equals((object)p1), Is.True);
            Assert.That(p1.Equals((object)p2), Is.True);
            Assert.That(p1.GetHashCode(), Is.EqualTo(p2.GetHashCode()));
            Assert.That(p1.Equals((object)null), Is.False);
            Assert.That(p1.Equals((object)"not a point"), Is.False);
        }

        [Test]
        [Category("Point2")]
        public void TestConstructorOverloads_Pass()
        {
            var fromDouble = new Point2(1.0, 2.0);
            Assert.That(fromDouble.X, Is.EqualTo(1f));
            Assert.That(fromDouble.Y, Is.EqualTo(2f));

            var fromInt = new Point2(3, 4);
            Assert.That(fromInt.X, Is.EqualTo(3f));
            Assert.That(fromInt.Y, Is.EqualTo(4f));

            var fromShort = new Point2((short)5, (short)6);
            Assert.That(fromShort.X, Is.EqualTo(5f));
            Assert.That(fromShort.Y, Is.EqualTo(6f));
        }

        [Test]
        [Category("Point2")]
        public void TestStaticFields_Pass()
        {
            Assert.That(Point2.ZERO.X, Is.EqualTo(0f));
            Assert.That(Point2.ZERO.Y, Is.EqualTo(0f));
            Assert.That(Point2.ONE.X, Is.EqualTo(1f));
            Assert.That(Point2.ONE.Y, Is.EqualTo(1f));
        }

        [Test]
        [Category("Point2")]
        public void TestSubtractOperator_Pass()
        {
            var p1 = new Point2(3f, 4f);
            var v1 = new Vector2(1f, 2f);

            var p2 = p1 - v1;

            Assert.That(p2.X, Is.EqualTo(2f));
            Assert.That(p2.Y, Is.EqualTo(2f));
        }
    }
}



