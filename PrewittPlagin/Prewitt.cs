using PluginInterface;
using System;
using System.Drawing;

namespace PrewittPlagin
{
    [Version(1, 0)]
    public class Prewitt : IPlugin
    {
        public string Name => "Фильтр Прюитта";
        public string Author => "Oppe";

        public void Transform(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            int[,] matrixX = {
                { -1, 0, 1 },
                { -1, 0, 1 },
                { -1, 0, 1 }
            };

            int[,] matrixY = {
                { 1,  1,  1 },
                { 0,  0,  0 },
                { -1, -1, -1 }
            };

            Bitmap source = (Bitmap)bitmap.Clone();

            for (int x = 1; x < width - 1; x++)
            {
                for (int y = 1; y < height - 1; y++)
                {
                    int Gx = 0, Gy = 0;

                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            Color pixel = source.GetPixel(x + i, y + j);
                            int intensity = (pixel.R + pixel.G + pixel.B) / 3;

                            Gx += matrixX[j + 1, i + 1] * intensity;
                            Gy += matrixY[j + 1, i + 1] * intensity;
                        }
                    }

                    int g = Math.Min(255, Math.Abs(Gx) + Math.Abs(Gy));
                    bitmap.SetPixel(x, y, Color.FromArgb(g, g, g));
                }
            }
        }
    }
}
