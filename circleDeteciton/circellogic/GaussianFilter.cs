using System.Drawing;
using System.Windows.Forms;

namespace circleDeteciton.circellogic
{
    class GaussianFilter : ImageFilter
    {

        /// <summary>
        /// returns an image blurred by gaussian filter
        /// </summary>
        
        // * performance could be increased by: bigger kernel, which would result in smaller accuracy

        //5*5 kernel with sigma value 1.0 
        
        int[,] kernel =
        {
            {1, 4 , 7 , 4 , 1 },
            {4, 16, 26, 16, 4 },
            {7, 26, 41, 26, 7 },
            {4, 16, 26, 16, 4 },
            {1, 4 , 7 , 4 , 1 }
        };
        int sumOfKernelValues = 273;
        public override Image Filter(Image image)
        {
            var imageBitMap = new Bitmap(image);
            var result = new Bitmap(image);
            int width = image.Width;
            int height = image.Height;
            int kernelSize = kernel.GetLength(0);
            // explained:
            // foreach pixel that can fit kernel matrix:
            for(int y = kernelSize / 2; y < height - (kernelSize / 2);y++)
                for(int x = kernelSize / 2; x < width - (kernelSize / 2);x++)
                {
                    var newColor = GetKernelValue(imageBitMap, x, y);
                    result.SetPixel(x, y, Color.FromArgb(newColor, newColor, newColor));
                }

            return result;
        }

        int GetKernelValue(Bitmap bitmap, int x, int y)
        {
            // need OG poss of pixel:
            // need image
            // guaranteed fit of matrix
            // convolute around with kernel
            int gradient = 0;
            for(int row = y - 2; row <= y + 2;row++)
                for(int col = x - 2; col <= x + 2; col++)
                {
                    var pixelColorValue = bitmap.GetPixel(col, row).R;
                    var kernelMultiplier = kernel[(row - y) + 2, (col - x) + 2];
                    gradient += pixelColorValue * kernelMultiplier;
                }
            // the sum of the results divided by total kernel sum = new value for the pixel
            return gradient / sumOfKernelValues;
        }

    }
}
