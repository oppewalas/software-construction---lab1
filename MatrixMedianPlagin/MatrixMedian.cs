using PluginInterface;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace MatrixMedianPlagin
{
    [Version(1, 0)]
    public class MatrixMedian : IPlugin
    {
        public string Name => "Медианный фильтр 3x3";
        public string Author => "Oppe";

        public void Transform(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            Bitmap source = (Bitmap)bitmap.Clone();

            for (int x = 1; x < width - 1; x++)
            {
                for (int y = 1; y < height - 1; y++)
                {
                    List<int> rValues = new List<int>();
                    List<int> gValues = new List<int>();
                    List<int> bValues = new List<int>();

                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            Color pixel = source.GetPixel(x + i, y + j);
                            rValues.Add(pixel.R);
                            gValues.Add(pixel.G);
                            bValues.Add(pixel.B);
                        }
                    }

                    rValues.Sort();
                    gValues.Sort();
                    bValues.Sort();

                    int medianR = rValues[4];
                    int medianG = gValues[4];
                    int medianB = bValues[4];

                    bitmap.SetPixel(x, y, Color.FromArgb(medianR, medianG, medianB));
                }
            }
        }
    }
}
