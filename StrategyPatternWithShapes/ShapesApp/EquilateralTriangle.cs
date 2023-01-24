using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesApp
{
    public class EquilateralTriangle : Triangle
    {
        public EquilateralTriangle(double sideLength) : base(sideLength, sideLength, sideLength) { }
    }
}
