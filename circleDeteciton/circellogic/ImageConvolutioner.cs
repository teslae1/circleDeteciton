using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace circleDeteciton
{
    class ImageConvolutioner
    {
        ImageGrayScaleFilter imageScaleConverter = new ImageGrayScaleFilter();
        ImageSobelFilter sobelOperator = new ImageSobelFilter();
        public Image Convolute(Image image)
        {
            var grayScaleImage = imageScaleConverter.Filter(image);
            //sobel operator
            var postSobel = sobelOperator.Filter(grayScaleImage);

            return postSobel;
        }

       
    }
}