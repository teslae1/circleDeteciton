using circleDeteciton.circellogic.Calculators;
using circleDeteciton.circellogic.settings;
using System.Drawing;

namespace circleDeteciton.circellogic
{
    class CannyFilter : ImageFilter
    {
        NonMaxSupressionFilter nonMaxSupressionFilter = new NonMaxSupressionFilter();
        DoubleThresholdFilter doubleThresholdFilter = new DoubleThresholdFilter();
        Bitmap sobelImage;
        EdgeDirection[,] edgeDirections;
        GrayScaleFilter grayScaleFilter = new GrayScaleFilter();
        GaussianFilter gaussianFilter = new GaussianFilter();
        GradientCalculator gradientCalculator = new GradientCalculator();
        DirectionCalculator directionCalculator = new DirectionCalculator();

        public Image Filter(Image image, DoubleThresholdFilterSettings thresholdFilterSettings)
        {
            doubleThresholdFilter = new DoubleThresholdFilter(thresholdFilterSettings);
            return Filter(image);
        }

        public override Image Filter(Image image)
        {
            Bitmap bitmap = new Bitmap(image);
            Image postGrayScale = grayScaleFilter.Filter(bitmap);
            Image postGaussian = gaussianFilter.Filter(postGrayScale);
            FillSobelImageAndEdgeDirections(postGaussian);
            Image postNonMaxSupression = nonMaxSupressionFilter.Filter(sobelImage, edgeDirections);
            Image postEdgeTracking = doubleThresholdFilter.Filter(postNonMaxSupression);
            
            return postEdgeTracking;
        }

        void FillSobelImageAndEdgeDirections(Image image)
        {
            Bitmap imageAsBitmap = new Bitmap(image);
            sobelImage = new Bitmap(image);
            edgeDirections = new EdgeDirection[image.Height, image.Width];
            int xGradient = 0;
            int yGradient = 0;
            Color newColor;
            for(int y = 0; y < image.Height;y++)
                for(int x = 0; x < image.Width; x++)
                {
                    xGradient = GetXGradient(x, y, imageAsBitmap);
                    yGradient = GetYGradient(x, y, imageAsBitmap);
                    newColor = GetTotalGradientAsColor(xGradient, yGradient);
                    sobelImage.SetPixel(x, y, newColor);
                    edgeDirections[y, x] = GetEdgeDirection(xGradient, yGradient);
                }
        }

        int GetXGradient(int x, int y, Bitmap image)
        {
            return gradientCalculator.GetXGradient(x, y, image);
        }

        int GetYGradient(int x, int y, Bitmap image)
        {
            return gradientCalculator.GetYGradient(x, y, image);
        }
        
        Color GetTotalGradientAsColor(int xGradient, int yGradient)
        {
            int totalGradient = gradientCalculator.GetTotalGradient(xGradient, yGradient);
            return Color.FromArgb(totalGradient, totalGradient, totalGradient);
        }

        EdgeDirection GetEdgeDirection(int xGradient, int yGradient)
        {
            return directionCalculator.GetDirectionOfEdge(xGradient, yGradient);
        }

    }
}
