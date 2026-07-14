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
    public class Triangle3Tests
    {
        [Test]
        [Category("Triangle3")]
        public void Triangle3_TestBasicProperties_Pass()
        {
            var triangle = new Triangle3(
                new Point3(0f, 0f, 0f),
                new Point3(3f, 0f, 0f),
                new Point3(0f, 4f, 0f));

            Assert.That(triangle.A.X, Is.EqualTo(0f));
            Assert.That(triangle.B.X, Is.EqualTo(3f));
            Assert.That(triangle.C.Y, Is.EqualTo(4f));
            Assert.That(triangle.Perimeter, Is.EqualTo(12f));
            Assert.That(triangle.Area, Is.EqualTo(6f));
        }

        [Test]
        [Category("Triangle3")]
        public void Triangle3_TestDuplicatePoints_Fail()
        {
            Assert.Throws<ArgumentException>((Action)(() => new Triangle3(
                new Point3(0f, 0f, 0f),
                new Point3(0f, 0f, 0f),
                new Point3(1f, 1f, 1f))));
        }
    }
}
