using System;
using System.Drawing;

namespace circleDeteciton.circellogic.Calculators
{
    class GradientCalculator
    {
        int[,] xKernel = {
            {-1,0,1 },
            {-2,0,2 },
            {-1,0,1 },
            };

        int[,] yKernel =
        {
            {-1,-2,-1},
            { 0, 0, 0},
            { 1, 2, 1},
        };
        PixelOutOfRangeCalculator pixelOutOfRangeHelper = new PixelOutOfRangeCalculator(); 
        public int GetXGradient(int x, int y, Bitmap image)
        {
            int gradient = 0;
            int kernelMultiplier = 0;
            Color closestPixel;
            Color pixel;
            for (int relativeY = y - 1; relativeY <= y + 1; relativeY++)
                for (int relativeX = x - 1; relativeX <= x + 1; relativeX += 2)
                {
                    kernelMultiplier = xKernel[relativeY - y + 1, relativeX - x + 1];
                    if (IndexIsOutOfRange(relativeX, relativeY, image))
                    {
                        closestPixel = GetClosestPixel(relativeX, relativeY, image);
                        gradient += closestPixel.R * kernelMultiplier;
                    }
                    else
                    {
                        pixel = image.GetPixel(relativeX, relativeY);
                        gradient += pixel.R * kernelMultiplier;
                    }
                }

            return gradient;
        }
        
        public int GetYGradient(int x, int y, Bitmap image)
        {
            int gradient = 0;
            int kernelMultiplier = 0;
            Color closestPixel;
            Color pixel;
            for(int relativeY = y - 1; relativeY <= y + 1;relativeY += 2)
                for(int relativeX = x - 1; relativeX <= x + 1; relativeX++)
                {
                    kernelMultiplier = yKernel[relativeY - y + 1, relativeX - x + 1];
                    if (IndexIsOutOfRange(relativeX, relativeY, image))
                    {
                        closestPixel = GetClosestPixel(relativeX, relativeY, image);
                        gradient += closestPixel.R * kernelMultiplier;
                    }
                    else
                    {
                        pixel = image.GetPixel(relativeX, relativeY);
                        gradient += pixel.R * kernelMultiplier;
                    }
                }

            return gradient;
        }

        bool IndexIsOutOfRange(int x, int y, Bitmap image)
        {
            return pixelOutOfRangeHelper.IndexIsOutOfRange(x, y, image);
        }

        Color GetClosestPixel(int x, int y, Bitmap image)
        {
            return pixelOutOfRangeHelper.GetClosestPixel(x, y, image);
        }

        public int GetTotalGradient(int xGradient, int yGradient)
        {
            int xGradientSquared = xGradient * xGradient;
            int yGradientSquared = yGradient * yGradient;
            int total = Convert.ToInt32(Math.Sqrt(xGradientSquared + yGradientSquared));
            return total > 255 ? 255 : total;
        }
    }
}
