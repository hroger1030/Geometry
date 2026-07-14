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
    public class CubeTests
    {
        [Test]
        public void Cube_ContainsAndIntersects_Pass()
        {
            var inner = new Cube(new Point3(0f, 0f, 0f), new Point3(1f, 1f, 1f));
            var outer = new Cube(new Point3(-1f, -1f, -1f), new Point3(2f, 2f, 2f));

            Assert.That(outer.Contains(inner), Is.True);
            Assert.That(inner.Intersects(outer), Is.True);
            Assert.That(outer.Intersects(new Cube(new Point3(3f, 3f, 3f), new Point3(4f, 4f, 4f))), Is.False);
        }

        [Test]
        public void Cube_Indexer_OutOfRange_Fail()
        {
            var cube = new Cube(0f, 0f, 0f, 1f, 1f, 1f);
            Assert.Throws<IndexOutOfRangeException>((Action)(() => { var point = cube[8]; }));
        }
    }
}



