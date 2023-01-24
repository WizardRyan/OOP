using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesApp
{
    public class Ellipse : IShape
    {
        private double _majorAxis;
        private double _minorAxis;

        public Ellipse(double majorAxis, double minorAxis)
        {
            _majorAxis = majorAxis < 0 ? 0 : majorAxis;
            _minorAxis = minorAxis < 0 ? 0 : minorAxis;
        }

        public double GetArea()
        {
            return Math.PI * _majorAxis * _minorAxis;
        }
    }
}
