using circleDeteciton.circellogic;
using System.Drawing;
using System.Windows.Forms;
using ImageProcessor;
using ImageProcessor.Imaging.Filters.EdgeDetection;
using System;

namespace circleDeteciton
{
    public partial class Form1 : Form
    {
        System.Drawing.Image image = System.Drawing.Image.FromFile(@"C:\Users\Ann\Pictures\steamengine.png");
        public Form1()
        {
            InitializeComponent();
            SetImageDisplayed(image);
        }

        Image GetTestImage()
        {
            var image = new Bitmap(3, 3);
            for (int y = 0; y < image.Height; y++)
                for (int x = 0; x < image.Width; x++)
                {
                    if (x > 1 || y == 0)
                        image.SetPixel(x, y, System.Drawing.Color.FromArgb(50, 50, 50));
                    else
                        image.SetPixel(x, y, System.Drawing.Color.FromArgb(100, 100, 100));
                }

            return image;
        }

        public void SetImageDisplayed(System.Drawing.Image image)
        {
            pictureBox.Image = image;
        }

        private void transformBtn_Click(object sender, System.EventArgs e)
        {
            var canny = new CannyFilter();
            var postCanny = canny.Filter(image);
            SetImageDisplayed(postCanny);
            //var result = new Bitmap(image);
            //var circleDetector = new CircleDetector();
            //var circles = circleDetector.GetCircles(postCanny);
            //foreach (var circle in circles)
            //    result = DevTools.DrawCircle(result, circle, Color.Blue);

        }



    }
}
