using circleDeteciton.circellogic.Calculators;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace circleDeteciton.circellogic
{
    class NonMaxSupressionFilter
    {
        int[,] kernel =
            {
            {0,0,0},
            {0,0,0},
            {0,0,0}
        };
        PixelOutOfRangeCalculator pixelOutOfRangeCalc = new PixelOutOfRangeCalculator();
        public Image Filter(Image gradientPixels, EdgeDirection[,] directions)
        {
            Bitmap image = new Bitmap(gradientPixels);
            Bitmap result = new Bitmap(gradientPixels);
            Color colorResult = new Color();
            List<Color> pixelsToCheck = new List<Color>();
            for (int y = 0; y < image.Height; y++)
                for (int x = 0; x < image.Width; x++)
                {
                    EdgeDirection direction = directions[y, x];
                    if (direction == EdgeDirection.None)
                        colorResult = Color.Black;
                    else
                    {
                        Color currentPixel = image.GetPixel(x, y);
                        pixelsToCheck = GetPixelsDiagonalOfEdge(direction, x, y, image);
                        if (pixelsToCheck.Any(pixel => pixel.R > currentPixel.R))
                            colorResult = Color.Black;
                        else
                            colorResult = currentPixel;
                    }
                    result.SetPixel(x, y, colorResult);
                }

            return result;
        }

        List<Color> GetPixelsDiagonalOfEdge(EdgeDirection edgeDirection, int x, int y, Bitmap image)
        {
            List<Position> neighbouringPixelPositions = new List<Position>();
            switch (edgeDirection)
            {
                case EdgeDirection.Vertical:
                    neighbouringPixelPositions.Add(new Position(x + 1, y));
                    neighbouringPixelPositions.Add(new Position(x - 1, y));
                    break;
                case EdgeDirection.Horizontal:
                    neighbouringPixelPositions.Add(new Position(x, y + 1));
                    neighbouringPixelPositions.Add(new Position(x, y - 1));
                    break;
                case EdgeDirection.TopRightToLeftDown:
                    neighbouringPixelPositions.Add(new Position(x - 1, y - 1));
                    neighbouringPixelPositions.Add(new Position(x + 1, y + 1));
                    break;
                case EdgeDirection.TopLeftToRightDown:
                    neighbouringPixelPositions.Add(new Position(x + 1, y - 1));
                    neighbouringPixelPositions.Add(new Position(x - 1, y + 1));
                    break;
            }

            List<Color> pixelsDiagonalOfEdge = new List<Color>();
            foreach(var pos in neighbouringPixelPositions)
            {
                if (IsWithinRange(pos,image))
                    pixelsDiagonalOfEdge.Add(image.GetPixel(pos.X, pos.Y));
                else
                    pixelsDiagonalOfEdge.Add(GetClosestPixel(pos, image));
            }

            return pixelsDiagonalOfEdge;
        }

        bool IsWithinRange(Position pos, Bitmap image)
        {
            return pixelOutOfRangeCalc.IndexIsOutOfRange(pos.X, pos.Y, image) == false;
        }

        Color GetClosestPixel(Position pos, Bitmap image)
        {
            return pixelOutOfRangeCalc.GetClosestPixel(pos.X, pos.Y, image);
        }

    }
}
