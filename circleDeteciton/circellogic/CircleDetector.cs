using circleDeteciton.circellogic;
using circleDeteciton.circellogic.settings;
using ImageProcessor.Imaging.Colors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Security.Policy;
using System.Xml.Schema;

namespace circleDeteciton
{
    class CircleDetector
    {
        int minRadius;
        int maxRadius;
        int minValuePrRadiaCircleThreshold;
        DoubleThresholdFilterSettings doubleThresholdFilterSettings = new DoubleThresholdFilterSettings(20, 100);
        CannyFilter cannyFilter = new CannyFilter();
        public List<Circle> GetCircles(Image image, CircleDetectionSettings settings)
        {
            return GetCircles(new Bitmap(image), settings);
        }

        public List<Circle> GetCircles(Bitmap image, CircleDetectionSettings settings)
        {
            LoadDetectionSettings(settings);
            Image postCanny = cannyFilter.Filter(image, doubleThresholdFilterSettings);
            int[,,] grid = GetAccumulatorGrid(postCanny);
            return GetCirclesFromAcumulatorGrid(grid);
        }

        void LoadDetectionSettings(CircleDetectionSettings settings)
        {
            minRadius = settings.MinRadiusInPixels;
            maxRadius = settings.MaxRadiusInPixels;
            minValuePrRadiaCircleThreshold = settings.MinValuePrRadiaCircleThreshold;
        }

        int[,,] GetAccumulatorGrid(Image image)
        {
            return GetAccumulatorGrid(new Bitmap(image));
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
            return image.GetPixel(x, y).R >= doubleThresholdFilterSettings.HighThreshold;
        }

        void AddCircleValueToGrid(ref int[,,] grid, int radius, Position center)
        {
            List<Position> circleOutline = GetCircleOutlinePositions(radius, center);
            foreach (Position pos in circleOutline)
                if (PositionIsWithinRange(pos, grid))
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
            List<Circle> circles = new List<Circle>();
            for (int r = minRadius; r <= maxRadius; r++)
                for (int y = 0; y < grid.GetLength(1); y++)
                    for (int x = 0; x < grid.GetLength(2); x++)
                    {
                        {
                            Position pos = new Position(x, y);
                            if (IsCircleCenter(grid, pos, r))
                                circles.Add(new Circle(r, pos));
                        }
                    }
            return circles;
        }

        bool IsCircleCenter(int[,,] grid, Position position, int radius)
        {
            int value = grid[radius - minRadius, position.Y, position.X];
            return (value / radius >= minValuePrRadiaCircleThreshold);
        }

        bool PositionIsWithinRange(Position pos, int[,,] grid)
        {
            return pos.X >= 0 && pos.X < grid.GetLength(2) &&
                pos.Y >= 0 && pos.Y < grid.GetLength(1);
        }
    }
}
