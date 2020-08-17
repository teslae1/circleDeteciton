using circleDeteciton.circellogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Security.Policy;

namespace circleDeteciton
{
    class CircleDetector
    {
        int minRadius = 100;
        int maxRadius = 200;
        int strongEdgeMinimum = 100;
        int minValuePrRadiaCircleCenter = 32;
        AcumulationGridGetter acumulatorGridGetter = new AcumulationGridGetter();
        public List<Circle> GetCircles(Image image)
        {
            return GetCircles(new Bitmap(image));
        }

        public List<Circle> GetCircles(Bitmap image)
        {
            int[,,] grid = GetAccumulatorGrid(image);
            return GetCirclesFromAcumulatorGrid(grid);
        }

        int[,,] GetAccumulatorGrid(Bitmap image)
        {
            int[,,] grid = new int[maxRadius - minRadius + 1, image.Height, image.Width];
            for (int y = 0; y < image.Height; y++)
                for (int x = 0; x < image.Width; x++)
                    if (IsStrongEdge(x, y, image))
                        for (int r = minRadius; r <= maxRadius; r++)
                            AddCircleValueToGrid(ref grid, r, new Position(x, y));
            return grid;
        }

        bool IsStrongEdge(int x, int y, Bitmap image)
        {
            return image.GetPixel(x, y).R >= strongEdgeMinimum;
        }

        void AddCircleValueToGrid(ref int[,,] grid, int radius, Position center)
        {
            List<Position> circleOutline = GetCircleOutlinePositions(radius, center);
            foreach (Position pos in circleOutline)
                grid[radius - minRadius, pos.Y, pos.X]++;
        }

        List<Position> GetCircleOutlinePositions(int radius, Position center)
        {
            List<Position> circleEdges = new List<Position>();
            for (double i = 0.0; i <= 360; i += 0.1)
            {
                double angle = i * (Math.PI / 180);
                int x = (int)(center.X + radius * Math.Cos(angle));
                int y = (int)(center.Y + radius * Math.Sin(angle));
                circleEdges.Add(new Position(x, y));
            }

            return circleEdges;
        }



        List<Circle> GetCirclesFromAcumulatorGrid(int[,,] grid)
        {
            return null;
        }

        bool IsCircleCenter(int value, int circleRadius)
        {
            return value >= (circleRadius * minValuePrRadiaCircleCenter);
        }
    }
}
