using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ShapesApp;

namespace ShapesUnitTests
{
    [TestClass]
    public class AllShapesTests
    {
        private const double _ACCEPTABLE_DELTA = 0.001;
        [TestMethod]
        public void Ellipse_Area_Calculation()
        {
            var shape = new Ellipse(5.3, 2.0);
            Assert.AreEqual(33.30088, shape.GetArea(), _ACCEPTABLE_DELTA);

            shape = new Ellipse(-5.0, 2.0);
            Assert.AreEqual(0, shape.GetArea(), 0);

            shape = new Circle(6.3);
            Assert.AreEqual(124.68981, shape.GetArea(), _ACCEPTABLE_DELTA);

            shape = new Circle(-2);
            Assert.AreEqual(0, shape.GetArea(), 0);
        }

        [TestMethod]
        public void Triangle_Area_Calculation()
        {
            Triangle shape = new EquilateralTriangle(4.2);
            Assert.AreEqual(7.638, shape.GetArea(), _ACCEPTABLE_DELTA);

            shape = new IsoscelesTriangle(4, 5.6);
            Assert.AreEqual(7.998, shape.GetArea(), _ACCEPTABLE_DELTA);

            shape = new ScaleneTriangle(4, 5.6, 8.2);
            Assert.AreEqual(10.037, shape.GetArea(), _ACCEPTABLE_DELTA);

            shape = new ScaleneTriangle(-5, -2.3, 5);
            Assert.AreEqual(0, shape.GetArea(), _ACCEPTABLE_DELTA);
        }

        [TestMethod]
        public void Rectangle_Area_Calculation()
        {
            Rectangle shape = new Rectangle(4.2, 5.0);
            Assert.AreEqual(21, shape.GetArea(), _ACCEPTABLE_DELTA);

            shape = new Square(4);
            Assert.AreEqual(16, shape.GetArea(), _ACCEPTABLE_DELTA);

            shape = new Square(-5.2);
            Assert.AreEqual(0, shape.GetArea(), _ACCEPTABLE_DELTA);
        }
    }
}
