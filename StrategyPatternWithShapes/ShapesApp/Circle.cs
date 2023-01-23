using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesApp
{
    internal class Circle : Ellipse
    {
        public Circle(double radius) : base(radius, radius) { }
    }
}
