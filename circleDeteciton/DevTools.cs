using System;
using System.Drawing;

namespace circleDeteciton
{
    static class DevTools
    {
        public static void Print(object o)
        {
            System.Diagnostics.Debug.WriteLine(o);
        }

        public static void PrintImage(Image img)
        {
            Bitmap image = new Bitmap(img);
            string printable = "";
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    printable += "|" + image.GetPixel(x, y).R;
                }
                printable += "\n";
            }
            Print(printable);
        }

        public static Bitmap DrawCrossOnCircle(Circle circle, Bitmap img, Color color)
        {
            for (int x = circle.Center.X - circle.Radius; x <= circle.Center.X + circle.Radius; x++)
                img.SetPixel(x, circle.Center.Y, color);
            for (int y = circle.Center.Y - circle.Radius; y <= circle.Center.Y + circle.Radius; y++)
                img.SetPixel(circle.Center.X, y, color);
            return img;
        }

        public static Bitmap GetBlackImage(int width, int height)
        {
            var image = new Bitmap(width, height);
            for (int y = 0; y < image.Height; y++)
                for (int x = 0; x < image.Width; x++)
                    image.SetPixel(x, y, Color.Black);
            return image;
        }

        public static Bitmap DrawCircle(Image image, Circle circle, Color color)
        {
            var center = circle.Center;
            var radius = circle.Radius;
            var result = new Bitmap(image);
            for (double i = 0.0; i < 360; i += 0.1)
            {
                double angle = i * (Math.PI / 180);
                int x = (int)(center.X + radius * Math.Cos(angle));
                int y = (int)(center.Y + radius * Math.Sin(angle));
                if (x >= 0 && x < result.Width &&
                    y >= 0 && y < result.Height)
                    result.SetPixel(x, y, color);
            }
            return result;
        }

    }
}
