using ImageProcessor;
using ImageProcessor.Imaging.Filters.Photo;
using System.Drawing;

namespace circleDeteciton.circellogic
{
    class GrayScaleFilter
    {
        ImageFactory imageFactory = new ImageFactory();
        public Image Filter(Image image)
        {
            imageFactory.Load(image);
            imageFactory.Filter(MatrixFilters.GreyScale);
            return imageFactory.Image;
        }
    }
}
