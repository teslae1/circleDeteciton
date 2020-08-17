using circleDeteciton.circellogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace circleDeteciton
{
    class Circle
    {
        // måle størrelse på emner:
        // 
        public Position Center { get; private set; }
        public int Radius { get; private set; }
        public int Diameter { get; private set; }
        public Circle(int radius, Position center)
        {
            Radius = radius;
            Center = center;
            Diameter = radius * 2;
        }
    }
}
