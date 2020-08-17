using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace circleDeteciton.circellogic
{
    class AcumulationGridGetter
    {
        object semaphore = new object();
        object pulseOnThreadsFinished = new object();
        int strongEdgeMinimum;
        Bitmap image;
        int minRadius;
        int maxRadius;
        Dictionary<int, int[,]> radiiLayerDict = new Dictionary<int, int[,]>();
        public int[][,] GetAcumulationGrid(Bitmap image, int minRadius, int maxRadius, int strongEdgeMinimum)
        {
            this.strongEdgeMinimum = strongEdgeMinimum;
            this.minRadius = minRadius;
            this.maxRadius = maxRadius;
            this.image = image;
            StartThreads();
            lock (pulseOnThreadsFinished)
                Monitor.Wait(pulseOnThreadsFinished);
            return ToGrid(radiiLayerDict);
        }

        void StartThreads()
        {
            for (int r = minRadius; r <= maxRadius; r++)
            {
                int n = r;
                Thread t = new Thread(() =>
                {
                    Dofo(n);
                });
                t.Priority = ThreadPriority.Highest;
                t.Start();
            }
        }


        void Dofo(int radius)
        {
            var bm = new Bitmap(1, 1);
            lock (image)
            {
                bm = new Bitmap(image);
            }

            int[,] layer = new int[bm.Height, bm.Width];
            for (int y = 0; y < bm.Height; y++)
                for (int x = 0; x < bm.Width; x++)
                {
                    if (PixelIsStrongEdge(bm.GetPixel(x, y)))
                    {
                        List<Position> circleEdge = GetCircleEdgePositions(radius, new Position(x, y));
                        foreach (Position pos in circleEdge)
                            if (PositionWithinRange(pos, bm))
                                layer[pos.Y, pos.X]++;
                    }
                }

            //save result
            lock (semaphore)
            {
                radiiLayerDict[radius] = layer;
                if (AllThreadsAreDone())
                {
                    lock (pulseOnThreadsFinished)
                        Monitor.Pulse(pulseOnThreadsFinished);
                }
            }
        }


        bool PixelIsStrongEdge(Color pixel)
        {
            return pixel.R >= strongEdgeMinimum;
        }

        List<Position> GetCircleEdgePositions(int radius, Position center)
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

        bool PositionWithinRange(Position pos, Bitmap image)
        {
            return pos.X >= 0 && pos.X < image.Width &&
                pos.Y >= 0 && pos.Y < image.Height;
        }

        bool AllThreadsAreDone()
        {
            return radiiLayerDict.Count >= maxRadius - minRadius + 1;
        }

        int[][,] ToGrid(Dictionary<int, int[,]> dict)
        {
            int[][,] grid = new int[maxRadius - minRadius + 1][,];
            for (int r = 0; r < dict.Count; r++)
                grid[r] = dict[r + minRadius];
            return grid;
        }
    }
}
