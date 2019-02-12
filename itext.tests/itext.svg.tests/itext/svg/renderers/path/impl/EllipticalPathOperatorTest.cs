using System;
using iText.Kernel.Geom;

namespace iText.Svg.Renderers.Path.Impl {
    public class EllipticalPathOperatorTest {
        // tests for coordinates
        [NUnit.Framework.Test]
        public virtual void TestBasicParameterSet() {
            EllipticalCurveTo absoluteElliptic = new EllipticalCurveTo();
            // String array length = 7
            absoluteElliptic.SetCoordinates(new String[] { "40", "40", "0", "0", "0", "20", "20" }, new Point());
            String[][] result = absoluteElliptic.GetCoordinates();
            NUnit.Framework.Assert.AreEqual(1, result.Length);
            NUnit.Framework.Assert.AreEqual(7, result[0].Length);
        }

        [NUnit.Framework.Test]
        public virtual void TestTooManyParameterSet() {
            EllipticalCurveTo absoluteElliptic = new EllipticalCurveTo();
            // String array length = 8
            absoluteElliptic.SetCoordinates(new String[] { "40", "40", "0", "0", "0", "20", "20", "1" }, new Point());
            String[][] result = absoluteElliptic.GetCoordinates();
            NUnit.Framework.Assert.AreEqual(1, result.Length);
            NUnit.Framework.Assert.AreEqual(7, result[0].Length);
        }

        [NUnit.Framework.Test]
        public virtual void TestIncorrectMultipleParameterSets() {
            EllipticalCurveTo absoluteElliptic = new EllipticalCurveTo();
            // String array length = 13
            absoluteElliptic.SetCoordinates(new String[] { "40", "40", "0", "0", "0", "20", "20", "40", "40", "0", "0"
                , "0", "20" }, new Point());
            String[][] result = absoluteElliptic.GetCoordinates();
            NUnit.Framework.Assert.AreEqual(1, result.Length);
            NUnit.Framework.Assert.AreEqual(7, result[0].Length);
        }

        [NUnit.Framework.Test]
        public virtual void TestMultipleParameterSet() {
            EllipticalCurveTo absoluteElliptic = new EllipticalCurveTo();
            // String array length = 14
            absoluteElliptic.SetCoordinates(new String[] { "40", "40", "0", "0", "0", "20", "20", "40", "40", "0", "0"
                , "0", "20", "20" }, new Point());
            String[][] result = absoluteElliptic.GetCoordinates();
            NUnit.Framework.Assert.AreEqual(2, result.Length);
            NUnit.Framework.Assert.AreEqual(7, result[0].Length);
            NUnit.Framework.Assert.AreEqual(7, result[1].Length);
        }

        [NUnit.Framework.Test]
        public virtual void TestRandomParameterAmountSet() {
            EllipticalCurveTo absoluteElliptic = new EllipticalCurveTo();
            // String array length = 17
            absoluteElliptic.SetCoordinates(new String[] { "40", "40", "0", "0", "0", "20", "20", "40", "40", "0", "0"
                , "0", "20", "20", "0", "1", "2" }, new Point());
            String[][] result = absoluteElliptic.GetCoordinates();
            NUnit.Framework.Assert.AreEqual(2, result.Length);
            NUnit.Framework.Assert.AreEqual(7, result[0].Length);
            NUnit.Framework.Assert.AreEqual(7, result[1].Length);
        }

        [NUnit.Framework.Test]
        public virtual void TestNotEnoughParameterSet() {
            EllipticalCurveTo absoluteElliptic = new EllipticalCurveTo();
            // String array length = 6
            absoluteElliptic.SetCoordinates(new String[] { "40", "0", "0", "0", "20", "20" }, new Point());
            String[][] result = absoluteElliptic.GetCoordinates();
            NUnit.Framework.Assert.AreEqual(0, result.Length);
        }

        [NUnit.Framework.Test]
        public virtual void TestNoParameterSet() {
            EllipticalCurveTo absoluteElliptic = new EllipticalCurveTo();
            // String array length = 0
            absoluteElliptic.SetCoordinates(new String[] {  }, new Point());
            String[][] result = absoluteElliptic.GetCoordinates();
            NUnit.Framework.Assert.AreEqual(0, result.Length);
        }

        // rotate tests
        private void AssertPointArrayArrayEquals(Point[][] expected, Point[][] actual) {
            NUnit.Framework.Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++) {
                AssertPointArrayEquals(expected[i], actual[i]);
            }
        }

        private void AssertPointArrayEquals(Point[] expected, Point[] actual) {
            NUnit.Framework.Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++) {
                NUnit.Framework.Assert.AreEqual(expected[i].x, actual[i].x, 0.00001);
                NUnit.Framework.Assert.AreEqual(expected[i].y, actual[i].y, 0.00001);
            }
        }

        [NUnit.Framework.Test]
        public virtual void ZeroRotationOriginTest() {
            Point[][] input = new Point[][] { new Point[] { new Point(50, 30) } };
            Point[][] actual = EllipticalCurveTo.Rotate(input, 0.0, new Point(0, 0));
            AssertPointArrayArrayEquals(actual, input);
        }

        [NUnit.Framework.Test]
        public virtual void FullCircleRotationOriginTest() {
            Point[][] input = new Point[][] { new Point[] { new Point(50, 30) } };
            Point[][] actual = EllipticalCurveTo.Rotate(input, 2 * Math.PI, new Point(0, 0));
            AssertPointArrayArrayEquals(actual, input);
        }

        [NUnit.Framework.Test]
        public virtual void HalfCircleRotationOriginTest() {
            Point[][] input = new Point[][] { new Point[] { new Point(50, 30) } };
            Point[][] actual = EllipticalCurveTo.Rotate(input, Math.PI, new Point(0, 0));
            Point[][] expected = new Point[][] { new Point[] { new Point(-50, -30) } };
            AssertPointArrayArrayEquals(expected, actual);
        }

        [NUnit.Framework.Test]
        public virtual void ThirtyDegreesRotationOriginTest() {
            Point[][] input = new Point[][] { new Point[] { new Point(0, 30) } };
            Point[][] actual = EllipticalCurveTo.Rotate(input, -Math.PI / 6, new Point(0, 0));
            Point[][] expected = new Point[][] { new Point[] { new Point(15, Math.Cos(Math.PI / 6) * 30) } };
            AssertPointArrayArrayEquals(expected, actual);
        }

        [NUnit.Framework.Test]
        public virtual void FortyFiveDegreesRotationOriginTest() {
            Point[][] input = new Point[][] { new Point[] { new Point(0, 30) } };
            Point[][] actual = EllipticalCurveTo.Rotate(input, -Math.PI / 4, new Point(0, 0));
            Point[][] expected = new Point[][] { new Point[] { new Point(Math.Sin(Math.PI / 4) * 30, Math.Sin(Math.PI 
                / 4) * 30) } };
            AssertPointArrayArrayEquals(expected, actual);
        }

        [NUnit.Framework.Test]
        public virtual void SixtyDegreesRotationOriginTest() {
            Point[][] input = new Point[][] { new Point[] { new Point(0, 30) } };
            Point[][] actual = EllipticalCurveTo.Rotate(input, -Math.PI / 3, new Point(0, 0));
            Point[][] expected = new Point[][] { new Point[] { new Point(Math.Sin(Math.PI / 3) * 30, 15) } };
            AssertPointArrayArrayEquals(expected, actual);
        }

        [NUnit.Framework.Test]
        public virtual void NinetyDegreesRotationOriginTest() {
            Point[][] input = new Point[][] { new Point[] { new Point(0, 30) } };
            Point[][] actual = EllipticalCurveTo.Rotate(input, -Math.PI / 2, new Point(0, 0));
            Point[][] expected = new Point[][] { new Point[] { new Point(30, 0) } };
            AssertPointArrayArrayEquals(expected, actual);
        }

        [NUnit.Framework.Test]
        public virtual void ZeroRotationRandomPointTest() {
            Point[][] input = new Point[][] { new Point[] { new Point(50, 30) } };
            Point[][] actual = EllipticalCurveTo.Rotate(input, 0.0, new Point(40, 90));
            AssertPointArrayArrayEquals(actual, input);
        }

        [NUnit.Framework.Test]
        public virtual void FullCircleRotationRandomPointTest() {
            Point[][] input = new Point[][] { new Point[] { new Point(50, 30) } };
            Point[][] actual = EllipticalCurveTo.Rotate(input, 2 * Math.PI, new Point(-200, 50));
            AssertPointArrayArrayEquals(actual, input);
        }

        [NUnit.Framework.Test]
        public virtual void HalfCircleRotationRandomPointTest() {
            Point[][] input = new Point[][] { new Point[] { new Point(50, 30) } };
            Point[][] actual = EllipticalCurveTo.Rotate(input, Math.PI, new Point(-20, -20));
            Point[][] expected = new Point[][] { new Point[] { new Point(-90, -70) } };
            AssertPointArrayArrayEquals(expected, actual);
        }

        [NUnit.Framework.Test]
        public virtual void ThirtyDegreesRotationRandomPointTest() {
            Point[][] input = new Point[][] { new Point[] { new Point(0, 30) } };
            Point[][] actual = EllipticalCurveTo.Rotate(input, -Math.PI / 6, new Point(100, 100));
            Point[][] expected = new Point[][] { new Point[] { new Point(-21.60253882, 89.37822282) } };
            AssertPointArrayArrayEquals(expected, actual);
        }

        [NUnit.Framework.Test]
        public virtual void FortyFiveDegreesRotationRandomPointTest() {
            Point[][] input = new Point[][] { new Point[] { new Point(0, 30) } };
            Point[][] actual = EllipticalCurveTo.Rotate(input, -Math.PI / 4, new Point(20, 0));
            Point[][] expected = new Point[][] { new Point[] { new Point(27.07106769, 35.35533845) } };
            AssertPointArrayArrayEquals(expected, actual);
        }

        [NUnit.Framework.Test]
        public virtual void SixtyDegreesRotationRandomPointTest() {
            Point[][] input = new Point[][] { new Point[] { new Point(0, 30) } };
            Point[][] actual = EllipticalCurveTo.Rotate(input, -Math.PI / 3, new Point(0, -50));
            Point[][] expected = new Point[][] { new Point[] { new Point(69.28203105, -10) } };
            AssertPointArrayArrayEquals(expected, actual);
        }

        [NUnit.Framework.Test]
        public virtual void NinetyDegreesRotationRandomPointTest() {
            Point[][] input = new Point[][] { new Point[] { new Point(0, 30) } };
            Point[][] actual = EllipticalCurveTo.Rotate(input, -Math.PI / 2, new Point(-0, 20));
            Point[][] expected = new Point[][] { new Point[] { new Point(10, 20) } };
            AssertPointArrayArrayEquals(expected, actual);
        }
    }
}
