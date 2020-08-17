using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace circleDeteciton.Tests
{
    [TestClass()]
    public class ImageGrayScaleFilterTests
    {
        string imagePath = @"C:\Users\Ann\Pictures\steamengine.png";
        [TestMethod()]
        public void FilterTest()
        {
            var filter = new ImageGrayScaleFilter();
            var image = Image.FromFile(imagePath);
        }
    }
}