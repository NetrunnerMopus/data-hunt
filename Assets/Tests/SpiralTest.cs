using NUnit.Framework;
using UnityEngine;
using view.gui;
using static view.gui.Spiral;

namespace tests
{
    public class SpiralTest
    {

        [Test]
        public void ShouldPickVector()
        {
            ShouldPickVector(1, Vector2.zero);
            ShouldPickVector(2, R);
            ShouldPickVector(3, R + LD);
            ShouldPickVector(4, R + LD + L);
            ShouldPickVector(5, R + LD + L + LU);
            ShouldPickVector(6, R + LD + L + LU + RU);
            ShouldPickVector(19, 2 * RU);
        }

        private void ShouldPickVector(int hexIndex, Vector2 expected)
        {
            var spiral = new Spiral();
            Assert.AreEqual(expected, spiral.PickVector(hexIndex));
        }
    }
}
