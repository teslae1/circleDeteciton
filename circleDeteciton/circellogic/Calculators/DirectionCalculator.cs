using System;
using System.ComponentModel;

namespace circleDeteciton.circellogic
{
    public enum EdgeDirection
    {
        None,
        Vertical,
        Horizontal,
        TopRightToLeftDown,
        TopLeftToRightDown
    }
    class DirectionCalculator
    {
        MathHelper mathHelper = new MathHelper();

        public EdgeDirection GetDirectionOfEdge(int xGradient, int yGradient)
        {
            
            if (xGradient == 0 && yGradient == 0)
                return EdgeDirection.None;
            var directionRadians = Math.Atan2(yGradient, xGradient);
            var directionDegrees = mathHelper.ToDegrees(directionRadians);
            return GetGradientDirection(directionDegrees);
        }

        EdgeDirection GetGradientDirection(double direction)
        {
            if (direction < 22.5 ||
                direction >= 157.5 && direction < 202.5 ||
                direction >= 337.5)
                return EdgeDirection.Vertical;
            if (direction >= 22.5 && direction < 67.5 ||
                direction >= 202.5 && direction < 247.5)
                return EdgeDirection.TopRightToLeftDown;
            if (direction >= 67.5 && direction < 112.5 ||
                direction >= 247.5 && direction < 292.5)
                return EdgeDirection.Horizontal;
            if (direction >= 112.5 && direction < 157.5 ||
                direction >= 292.5 && direction < 337.5)
                return EdgeDirection.TopLeftToRightDown;

            return EdgeDirection.None;
        }
    }
}
