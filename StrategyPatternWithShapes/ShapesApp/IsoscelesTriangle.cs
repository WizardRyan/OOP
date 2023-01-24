using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesApp
{
    public class IsoscelesTriangle : Triangle
    {

        public IsoscelesTriangle(double legsLength, double baseLength) : base(legsLength, legsLength, baseLength) { }
    }
}
