using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace circleDeteciton.circellogic
{
    class CircleDetectionSettings
    {
        public int MinRadiusInPixels;
        public int MaxRadiusInPixels;
        public int MinValuePrRadiaCircleThreshold;

        public CircleDetectionSettings(int minRadiusInPixels, int maxRadiusInPixels, int minValuePrRadiaCircleThreshold)
        {
            if(ParamsAreValid(minRadiusInPixels, maxRadiusInPixels, minValuePrRadiaCircleThreshold))
            {
            MinRadiusInPixels = minRadiusInPixels;
            MaxRadiusInPixels = maxRadiusInPixels;
            MinValuePrRadiaCircleThreshold = minValuePrRadiaCircleThreshold;
            }
        }

        bool ParamsAreValid(int minRadius, int maxRadius, int minValuePrRadiaCircleThreshold)
        {
            if (!ValidMinValuePrRadia(minValuePrRadiaCircleThreshold))
                throw new Exception("Minimum value pr. radia must be more than zero.");
            if (!ValidRadiusRange(minRadius, maxRadius))
                throw new Exception("radius must be positive integer, and maximum radius must be bigger than or equals to minimum radius");
            return true;
        }

        bool ValidMinValuePrRadia(int minValue)
        {
            return minValue > 0;
        }

        bool ValidRadiusRange(int minRadius, int maxRadius)
        {
            return minRadius >= 0 && maxRadius >= 0 &&
                minRadius <= maxRadius;
        }
    }
}
