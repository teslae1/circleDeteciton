using System.Drawing;

namespace circleDeteciton
{
    class ImageData
    {
        public double[,] DirectionPixels;
        public Bitmap GradientPixels;
        public ImageData(Bitmap gradientPixels, double[,] directionPixels)
        {
            DirectionPixels = directionPixels;
            GradientPixels = gradientPixels;
        }
    }
}
