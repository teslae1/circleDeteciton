using circleDeteciton.circellogic.settings;
using System.Drawing;

namespace circleDeteciton.circellogic
{
    class DoubleThresholdFilter : ImageFilter
    {
        double highThreshold = 100;
        double lowThreshold = 20;
        EdgeTracker edgeTracker = new EdgeTracker();

        public DoubleThresholdFilter()
        {

        }

        public DoubleThresholdFilter(Image image, DoubleThresholdFilterSettings settings)
        {
            highThreshold = settings.HighThreshold;
            lowThreshold = settings.LowThreshold;
            Filter(image);
        }

        public override Image Filter(Image image)
        {
            var bitmap = new Bitmap(image);
            var result = new Bitmap(image);
            Color currentPixel = new Color();
            for (int y = 1; y < image.Height - 1; y++)
                for (int x = 1; x < image.Width - 1; x++)
                {
                    currentPixel = bitmap.GetPixel(x, y);
                    if (IsStrongEdge(currentPixel))
                    {
                        // might need to be just set to currentcolor 
                        result.SetPixel(x, y, Color.White);
                        //check for neighbouring weak edge
                        edgeTracker.EdgeTrack(x, y, ref result, highThreshold, lowThreshold);
                    }
                    else if (IsNotEdge(currentPixel))
                        result.SetPixel(x, y, Color.Black);
                }

            Image cleanUp = DarkenAnyPixelsBelowThreshold(result, highThreshold);

            return cleanUp;
        }

        bool IsStrongEdge(Color pixel)
        {
            return pixel.R >= highThreshold;
        }

        bool IsNotEdge(Color pixel)
        {
            return pixel.R <= lowThreshold;
        }

        Image DarkenAnyPixelsBelowThreshold(Bitmap image, double threshold)
        {
            Bitmap result = new Bitmap(image);
            for (int y = 0; y < image.Height; y++)
                for (int x = 0; x < image.Width; x++)
                    if (image.GetPixel(x, y).R <= threshold)
                        result.SetPixel(x, y, Color.Black);
            return result;
        }
    }
}
