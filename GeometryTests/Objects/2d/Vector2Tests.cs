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
    public class Vector2Tests
    {
        [Test]
        [Category("Vector2")]
        public void TestOperatorOverloads()
        {
            var v1 = new Vector2(3, 3);
            var v2 = new Vector2(1, 2);

            var v3 = v1 + v2;
            Assert.That(v3.X == 4f && v3.Y == 5, Is.True, "Failed addition");  

            v3 = v1 - v2;
            Assert.That(v3.X == 2f && v3.Y == 1, Is.True, "Failed subtraction");

            v3 = v1 * 2;
            Assert.That(v3.X == 6f && v3.Y == 6, Is.True, "Failed multiplication");

            v3 = v1 / 2;
            Assert.That(v3.X == 1.5f && v3.Y == 1.5, Is.True, "Failed division");
        }

        [Test]
        [Category("Vector2")]
        public void TestNormalize_Pass()
        {
            var v = new Vector2(3f, 4f);
            var normalized = Vector2.Normalize(v);

            Assert.That(normalized.Length(), Is.EqualTo(1f).Within(Constants.FLOAT_ERROR_MARGIN));
            Assert.That(v.Length(), Is.EqualTo(5f));
        }

        [Test]
        [Category("Vector2")]
        public void TestNormalize_ZeroVector_Fail()
        {
            var v = new Vector2(0f, 0f);

            Assert.Throws<DivideByZeroException>((Action)(() => Vector2.Normalize(v)));
            Assert.Throws<DivideByZeroException>((Action)(() => v.Normalize()));
        }

        [Test]
        [Category("Vector2")]
        public void TestDivideByZero_Fail()
        {
            var v = new Vector2(1f, 1f);

            Assert.Throws<DivideByZeroException>((Action)(() => { var result = v / 0f; }));
        }

        [Test]
        [Category("Vector2")]
        public void TestConstructors_Pass()
        {
            var defaultVector = new Vector2();
            Assert.That(defaultVector.X, Is.EqualTo(0f));
            Assert.That(defaultVector.Y, Is.EqualTo(0f));

            var copy = new Vector2(new Vector2(2f, 3f));
            Assert.That(copy.X, Is.EqualTo(2f));
            Assert.That(copy.Y, Is.EqualTo(3f));

            var fromPoint = new Vector2(new Point2(4f, 5f));
            Assert.That(fromPoint.X, Is.EqualTo(4f));
            Assert.That(fromPoint.Y, Is.EqualTo(5f));

            var fromRotation = new Vector2(0f);
            Assert.That(fromRotation.X, Is.EqualTo(1f).Within(Constants.FLOAT_ERROR_MARGIN));
            Assert.That(fromRotation.Y, Is.EqualTo(0f).Within(Constants.FLOAT_ERROR_MARGIN));
        }

        [Test]
        [Category("Vector2")]
        public void TestStaticFields_Pass()
        {
            Assert.That(Vector2.Zero.X, Is.EqualTo(0f));
            Assert.That(Vector2.Zero.Y, Is.EqualTo(0f));
            Assert.That(Vector2.One.X, Is.EqualTo(1f));
            Assert.That(Vector2.One.Y, Is.EqualTo(1f));
        }

        [Test]
        [Category("Vector2")]
        public void TestCross_Pass()
        {
            var right = new Vector2(1f, 0f);
            var up = new Vector2(0f, 1f);

            Assert.That(Vector2.Cross(right, up), Is.EqualTo(1f));
            Assert.That(Vector2.Cross(up, right), Is.EqualTo(-1f));
            Assert.That(Vector2.Cross(right, right), Is.EqualTo(0f));
        }

        [Test]
        [Category("Vector2")]
        public void TestNormalize_Instance_Pass()
        {
            var v = new Vector2(3f, 4f);
            var unit = v.Normalize();

            Assert.That(unit.Length(), Is.EqualTo(1f).Within(Constants.FLOAT_ERROR_MARGIN));
        }

        [Test]
        [Category("Vector2")]
        public void TestVectorToRotation_Pass()
        {
            var v = new Vector2(1f, 0f);
            Assert.That(v.VectorToRotation(), Is.EqualTo(0f).Within(Constants.FLOAT_ERROR_MARGIN));
        }

        [Test]
        [Category("Vector2")]
        public void TestEquals_Pass()
        {
            var v = new Vector2(1f, 2f);
            var same = new Vector2(1f, 2f);
            var different = new Vector2(3f, 2f);

            Assert.That(v.Equals(v), Is.True);
            Assert.That(v.Equals(same), Is.True);
            Assert.That(v.Equals(different), Is.False);

            Assert.That(v.Equals((object)same), Is.True);
            Assert.That(v.Equals((object)null), Is.False);
            Assert.That(v.Equals((object)"not a vector"), Is.False);

            Assert.That(v.GetHashCode(), Is.EqualTo(same.GetHashCode()));
        }
    }
}



