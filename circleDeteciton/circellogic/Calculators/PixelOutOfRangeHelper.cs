using System.Drawing;

namespace circleDeteciton.circellogic.Calculators
{
    class PixelOutOfRangeCalculator
    {
        public bool IndexIsOutOfRange(int x, int y, Bitmap image)
        {
            return x < 0 || x > image.Width - 1 ||
                y < 0 || y > image.Height - 1;
        }

        public Color GetClosestPixel(int x, int y, Bitmap image)
        {
            // right
            if (x > image.Width - 1 && YIsWithingRange(y, image))
                return image.GetPixel(x - 1, y);
            // left
            if (x < 0 && YIsWithingRange(y, image))
                return image.GetPixel(x + 1, y);
            // up
            if (y > image.Height - 1 && XIsWithingRange(x, image))
                return image.GetPixel(x, y - 1);
            // down
            if (y < 0 && XIsWithingRange(x, image))
                return image.GetPixel(x, y + 1);
            //left top corner
            if (y < 0 && x < 0)
                return image.GetPixel(0, 0);
            //right top corner 
            if (y < 0 && x >= image.Width)
                return image.GetPixel(image.Width - 1, 0);
            //right bottom corner
            if (y >= image.Height && x >= image.Width)
                return image.GetPixel(image.Width - 1, image.Height - 1);
            //left bottom corner
            if (y >= image.Height && x < 0)
                return image.GetPixel(0, image.Height - 1);

            return new Color();
        }

        bool YIsWithingRange(int y, Bitmap image)
        {
            return y >= 0 && y <= image.Height - 1;
        }

        bool XIsWithingRange(int x, Bitmap image)
        {
            return x >= 0 && x <= image.Width - 1;
        }
    }
}
