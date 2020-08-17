using System;
using System.Drawing;

namespace circleDeteciton
{
    class ImageSobelFilter
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
        public Image Filter(Image image)
        {
            var bitmap = new Bitmap(image);
            var result = new Bitmap(image);
            int width = image.Width;
            int height = image.Height;
            //foreach pixel that fits the kernel size
            for (int y = 0; y < height;y++)
                for (int x = 0; x < width;x++)
                {
                    int xGradient = GetXGradient(bitmap, x, y);
                    int yGradient = GetYGradient(bitmap, x, y);
                    int totalGradient = GetTotalGradient(xGradient, yGradient);
                    result.SetPixel(x, y, Color.FromArgb(totalGradient, totalGradient, totalGradient));
                }

            return result;
        }

        int GetXGradient(Bitmap image, int x, int y)
        {
            int gradient = 0;
            for (int row = y - 1; row <= y + 1; row++)
                for (int col = x - 1; col <= x + 1; col += 2)
                {
                    var kernelMultiplier = GetXKernelMultiplier(row, col, x, y);
                    try
                    {
                    var currentColor = image.GetPixel(col, row).R;
                    gradient += currentColor * kernelMultiplier;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        //get closest pixel in range = in x range
                    }
                }

            return gradient;
        }

        int GetXKernelMultiplier(int row, int col, int centerPixelX, int centerPixelY)
        {
            int kernelRow = (row - centerPixelY) + 1;
            int kernelCol = (col - centerPixelX) + 1;
            return xKernel[kernelRow, kernelCol];
        }

        int GetYGradient(Bitmap image, int x, int y)
        {
            int gradient = 0;
            for (int row = y - 1; row <= y + 1; row += 2)
                for (int col = x - 1; col <= x + 1; col++)
                {
                    var kernelMultiplier = GetYKernelMultiplier(row, col, x, y);
                    var currentColor = image.GetPixel(col, row).R;
                    gradient += currentColor * kernelMultiplier;
                }

            return gradient;
        }

        int GetYKernelMultiplier(int row, int col, int x, int y)
        {
            int yKernelRow = (row - y) + 1;
            int yKernelCol = (col - x) + 1;
            return yKernel[yKernelRow, yKernelCol];
        }

        int GetTotalGradient(int xGradient, int yGradient)
        {
            var xGradientSquared = xGradient * xGradient;
            var yGradientSquared = yGradient * yGradient;
            var total = Convert.ToInt32(Math.Sqrt(xGradientSquared + yGradientSquared));
            return total > 255 ? 255 : total;
        }


    }
}
