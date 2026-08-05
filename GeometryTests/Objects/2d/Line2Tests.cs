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

namespace GeometryTests
{
    [TestFixture]
    public class Line2Tests
    {
        [Test]
        [Category("Line2")]
        public void Line2_LengthAndEquality_Pass()
        {
            var line = new Line2(new Point2(0f, 0f), new Point2(3f, 4f));

            Assert.That(line.Length, Is.EqualTo(5f));
            Assert.That(line.Equals(new Line2(new Point2(0f, 0f), new Point2(3f, 4f))), Is.True);
            Assert.That(line.ToString().Contains("Point1"), Is.True);
        }

        [Test]
        [Category("Line2")]
        public void Line2_FloatConstructor_Pass()
        {
            var line = new Line2(0f, 0f, 3f, 4f);

            Assert.That(line.Point1, Is.EqualTo(new Point2(0f, 0f)));
            Assert.That(line.Point2, Is.EqualTo(new Point2(3f, 4f)));
            Assert.That(line.Length, Is.EqualTo(5f));
        }

        [Test]
        [Category("Line2")]
        public void Line2_Equals_Pass()
        {
            var line = new Line2(new Point2(0f, 0f), new Point2(3f, 4f));
            var same = new Line2(new Point2(0f, 0f), new Point2(3f, 4f));
            var different = new Line2(new Point2(0f, 0f), new Point2(1f, 1f));

            Assert.That(line.Equals(line), Is.True);
            Assert.That(line.Equals(same), Is.True);
            Assert.That(line.Equals(different), Is.False);

            Assert.That(line.Equals((object)same), Is.True);
            Assert.That(line.Equals((object)null), Is.False);
            Assert.That(line.Equals((object)"not a line"), Is.False);

            Assert.That(line.GetHashCode(), Is.EqualTo(same.GetHashCode()));
        }
    }
}


