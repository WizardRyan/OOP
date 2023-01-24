using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesApp
{
    public class Square : Rectangle
    {
        public Square(double sideLength) : base(sideLength, sideLength) { }
    }
}
