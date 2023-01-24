using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesApp
{
    public class Rectangle : IShape
    {
        private double _length;
        private double _width;
        public Rectangle(double length, double width)
        {
            _length = length < 0 ? 0 : length;
            _width = width < 0 ? 0 : width;
        }

        public double GetArea()
        {
            return _length * _width;
        }
    }
}
