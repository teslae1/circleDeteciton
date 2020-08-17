using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace circleDeteciton
{
    public class ImageGrayScaleFilter : ImageFilter
    {
        public override Image Filter(Image image)
        {
            var bitmap = new Bitmap(image);
            for (int y = 0; y < image.Height; y++)
                for (int x = 0; x < image.Width; x++)
                {
                    var pixel = bitmap.GetPixel(x, y);
                    var averageColorValue = (pixel.R + pixel.G + pixel.B) / 3;
                    var newPixel = Color.FromArgb(averageColorValue, averageColorValue, averageColorValue);
                    bitmap.SetPixel(x, y, newPixel);
                }

            return bitmap;
        }
    }
}
