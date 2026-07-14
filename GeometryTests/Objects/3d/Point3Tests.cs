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
    public class Point3Tests
    {
        [Test]
        public void Point3_OperatorsAndEquality_Pass()
        {
            var p1 = new Point3(1f, 2f, 3f);
            var p2 = new Point3(4f, 5f, 6f);
            var p3 = p1 + p2;
            var p4 = p3 - p1;

            Assert.That(p3.X == 5f && p3.Y == 7f && p3.Z == 9f, Is.True);
            Assert.That(p4.Equals(p2), Is.True);
            Assert.That(p2.Equals((object)p2), Is.True);
            Assert.That(p2.Equals((object)null), Is.False);
        }
    }
}


