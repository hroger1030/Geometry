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
    public class ConstantsTests
    {
        [Test]
        [Category("Constants")]
        public void Constants_PiFamily_Pass()
        {
            Assert.That(Constants.PI, Is.EqualTo(MathF.PI));
            Assert.That(Constants.TWO_PI, Is.EqualTo(MathF.PI * 2f).Within(Constants.FLOAT_ERROR_MARGIN));
            Assert.That(Constants.HALF_PI, Is.EqualTo(MathF.PI / 2f).Within(Constants.FLOAT_ERROR_MARGIN));
            Assert.That(Constants.QUARTER_PI, Is.EqualTo(MathF.PI / 4f).Within(Constants.FLOAT_ERROR_MARGIN));
        }

        [Test]
        [Category("Constants")]
        public void Constants_DegRadConversion_RoundTrips_Pass()
        {
            const float degrees = 180f;

            var radians = degrees * Constants.DEG_TO_RAD;
            Assert.That(radians, Is.EqualTo(MathF.PI).Within(Constants.FLOAT_ERROR_MARGIN));

            var backToDegrees = radians * Constants.RAD_TO_DEG;
            Assert.That(backToDegrees, Is.EqualTo(degrees).Within(Constants.FLOAT_ERROR_MARGIN));
        }

        [Test]
        [Category("Constants")]
        public void Constants_SquareRoots_Pass()
        {
            Assert.That(Constants.SQRT_2, Is.EqualTo(MathF.Sqrt(2f)).Within(Constants.FLOAT_ERROR_MARGIN));
            Assert.That(Constants.SQRT_3, Is.EqualTo(MathF.Sqrt(3f)).Within(Constants.FLOAT_ERROR_MARGIN));
        }
    }
}
