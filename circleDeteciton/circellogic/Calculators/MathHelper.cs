using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace circleDeteciton.circellogic
{
    class MathHelper
    {
        public double ToDegrees(double radians)
        {
            return Math.Abs((180 / Math.PI) * radians);
        }
    }
}
