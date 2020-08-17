using circleDeteciton.circellogic.Calculators;
using System.Collections.Generic;
using System.Drawing;

namespace circleDeteciton.circellogic
{
    class EdgeTracker
    {
        PixelOutOfRangeCalculator pixelOutOfRangeCalc = new PixelOutOfRangeCalculator();
        double strongEdgeMinimum;
        double weakEdgeMinimum;
        public void EdgeTrack(int x, int y, ref Bitmap image, double strongEdgeMinimum, double weakEdgeMinimum)
        {
            this.strongEdgeMinimum = strongEdgeMinimum;
            this.weakEdgeMinimum = weakEdgeMinimum;
            EdgeTrack(x, y, ref image);
        }

        void EdgeTrack(int x, int y, ref Bitmap image)
        {
            //look at any adjacent pixels
            //if is weak edge: color edge and track
            //get the neighbouring pixels
            List<Position> neighbourPixelPositions = GetNeighbouringPixelPositions(x, y);
            foreach (Position pos in neighbourPixelPositions)
                if (IsWeakEdge(pos, image))
                {
                    image.SetPixel(pos.X, pos.Y, Color.White);
                    EdgeTrack(pos.X, pos.Y, ref image);
                }

        }

        List<Position> GetNeighbouringPixelPositions(int x, int y)
        {
            List<Position> positions = new List<Position>();
            for (int rY = y - 1; rY <= y + 1; rY++)
                for (int rX = x - 1; rX <= x + 1; rX++)
                    positions.Add(new Position(rX, rY));
            return positions;
        }

        bool IsWeakEdge(Position pos, Bitmap image)
        {
            if (PositionOutOfRange(pos, image))
                return false;
            Color currentPixel = image.GetPixel(pos.X, pos.Y);
            return currentPixel.R < strongEdgeMinimum && currentPixel.R > weakEdgeMinimum;
        }

        bool PositionOutOfRange(Position pos, Bitmap image)
        {
            return pixelOutOfRangeCalc.IndexIsOutOfRange(pos.X, pos.Y, image);
        }

        Color GetClosestPixel(Position pos, Bitmap image)
        {
            return pixelOutOfRangeCalc.GetClosestPixel(pos.X, pos.Y, image);
        }
    }
}
