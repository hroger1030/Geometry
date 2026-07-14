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
    public class VectorNTests
    {
        [Test]
        public void VectorN_OperatorAndEquality_Pass()
        {
            var v1 = new VectorN(3);
            v1.Axis[0] = 1f;
            v1.Axis[1] = 2f;
            v1.Axis[2] = 3f;

            var v2 = new VectorN(3);
            v2.Axis[0] = 3f;
            v2.Axis[1] = 2f;
            v2.Axis[2] = 1f;

            var sum = v1 + v2;
            Assert.That(sum.Axis[0], Is.EqualTo(4f));
            Assert.That(sum.Axis[1], Is.EqualTo(4f));
            Assert.That(sum.Axis[2], Is.EqualTo(4f));

            var diff = v1 - v2;
            Assert.That(diff.Axis[0], Is.EqualTo(-2f));
            Assert.That(diff.Axis[2], Is.EqualTo(2f));

            Assert.That((v1 * 2f).Axis[0], Is.EqualTo(2f));
            Assert.That((v1 / 2f).Axis[0], Is.EqualTo(0.5f));
            Assert.That(v1.Equals(new VectorN(v1)), Is.True);
        }

        [Test]
        public void VectorN_DifferentOrder_Fail()
        {
            var v1 = new VectorN(2);
            var v2 = new VectorN(3);

            Assert.Throws<InvalidOperationException>((Action)(() => { var _ = v1 + v2; }));
            Assert.Throws<InvalidOperationException>((Action)(() => { var _ = v1 - v2; }));
        }
    }
}



