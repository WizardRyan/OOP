using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesApp
{
    public class Triangle : IShape
    {
        private double _sideA;
        private double _sideB;
        private double _sideC;

        public Triangle(double sideA, double sideB, double sideC)
        {
            _sideA = sideA < 0 ? 0 : sideA;
            _sideB = sideB < 0 ? 0 : sideB;
            _sideC = sideC < 0 ? 0 : sideC;
        }

        public double GetArea()
        {
            if(_sideA == 0 || _sideB == 0 || _sideC == 0) return 0;
            double p = (_sideA + _sideB + _sideC) / 2.0;
            return Math.Sqrt(p * (p - _sideA) * (p - _sideB) * (p - _sideC));
        }
    }
}
