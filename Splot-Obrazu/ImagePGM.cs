using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splot_Obrazu
{
    enum ImageFormat
    { p1, p2, p3, p4, p5, X }


    class ImagePGM
    {
        private string path;
        private ImageFormat format;
        private int width, height, depth;
        private int[,] values;


        public ImagePGM(string path) { this.path = path; }
        public ImagePGM(ImageFormat format, int width, int height, int depth, int[,] values)
        {
            this.format = format;
            this.width = width;
            this.height = height;
            this.depth = depth;
            this.values = values;
        }

        public void print()
        {
            for (int i = 0; i < this.height; i++)
            {
                for (int j = 0; j < this.width; j++)
                {
                    Console.Write(this.values[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        public Bitmap MakeBitmap( int mag)
        {
            int width = this.Width * mag;
            int height = this.Height * mag;
            Bitmap result = new Bitmap(width, height);
            Graphics gr = Graphics.FromImage(result);
            for (int i = 0; i < this.Height; ++i)
            {
                for (int j = 0; j < this.Width; ++j)
                {
                    int pixelColor = this.Values[i, j];
                    if (pixelColor > 255) pixelColor = 255;
                    else if (pixelColor < 0) pixelColor = 0;
                    Color c = Color.FromArgb(pixelColor, pixelColor, pixelColor);
                    SolidBrush sb = new SolidBrush(c);
                    gr.FillRectangle(sb, j * mag, i * mag, mag, mag);
                }
            }
            return result;
        }




        public void saveASCIIPGMImage(string name)
        {

        }

        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public int Depth { get => depth; set => depth = value; }
        public int[,] Values { get => values; set => values = value; }
        public string Path { get => path; set => path = value; }
        internal ImageFormat Format { get => format; set => format = value; }
    }
}
