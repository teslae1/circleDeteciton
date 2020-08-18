using circleDeteciton.circellogic;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
namespace circleDeteciton
{
    public partial class Form1 : Form
    {
        Image image = Image.FromFile(@"C:\Users\Ann\Pictures\sodacan.jpg");
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
                        image.SetPixel(x, y, Color.FromArgb(50, 50, 50));
                    else
                        image.SetPixel(x, y, Color.FromArgb(100, 100, 100));
                }

            return image;
        }

        public void SetImageDisplayed(Image image)
        {
            pictureBox.Image = image;
        }

        private void transformBtn_Click(object sender, System.EventArgs e)
        {
            CircleDetector circleDetector = new CircleDetector();
            var detectionSettings = new CircleDetectionSettings(80, 120, 20);
            List<Circle> circles = circleDetector.GetCircles(image, detectionSettings);
            var result = new Bitmap(image);
            foreach (var circle in circles)
                DevTools.DrawCircle(ref result, circle, Color.Blue);
            SetImageDisplayed(result);
        }
    }
}
