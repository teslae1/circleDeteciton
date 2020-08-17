using ImageProcessor;
using System.Drawing;
using System.Runtime.InteropServices;

namespace circleDeteciton
{
    class GaussianFilter
    {
            ImageFactory imageFactory = new ImageFactory();
            int kernelSize = 6;
        public Image Filter(Image image)
        {
            imageFactory.Load(image);
            imageFactory.GaussianBlur(kernelSize);
            return imageFactory.Image;
        }
    }
}
