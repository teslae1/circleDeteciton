using ImageProcessor;
using ImageProcessor.Imaging.Filters.EdgeDetection;
using System.Drawing;
using System.Runtime.InteropServices;

namespace circleDeteciton
{
    class LibrarySobelFilter 
    {
        ImageFactory imageFactory = new ImageFactory();
        SobelEdgeFilter sobelFilter = new SobelEdgeFilter();
        public Image Filter(Image image, bool grayScale)
        {
            imageFactory.Load(image);
            imageFactory.DetectEdges(sobelFilter, grayScale);
            return imageFactory.Image;
        }
    }
}
