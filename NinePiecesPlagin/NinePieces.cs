using PluginInterface;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace NinePiecesPlagin
{
    [Version(1, 0)]
    public class NinePieces : IPlugin
    {
        public string Name => "Разбить изображение на 9 частей и перемешать";

        public string Author => "Oppe";

        public void Transform(Bitmap bitmap)
        {
            int pieceWidth = bitmap.Width / 3;
            int pieceHeight = bitmap.Height / 3;

            List<Bitmap> pieces = new List<Bitmap>();
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    Rectangle rect = new Rectangle(x * pieceWidth, y * pieceHeight, pieceWidth, pieceHeight);
                    pieces.Add(bitmap.Clone(rect, bitmap.PixelFormat));
                }
            }

            Random rnd = new Random();
            for (int i = pieces.Count - 1; i > 0; i--)
            {
                int j = rnd.Next(i + 1);
                (pieces[i], pieces[j]) = (pieces[j], pieces[i]);
            }

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                int index = 0;
                for (int y = 0; y < 3; y++)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        g.DrawImage(pieces[index], x * pieceWidth, y * pieceHeight);
                        index++;
                    }
                }
            }
        }
    }
}
