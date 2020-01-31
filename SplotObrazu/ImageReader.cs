using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplotObrazu
{
    enum ImageFormat
    { p1, p2, p3, p4, p5, X }

    class ImageReader
    {
        private ImageFormat format;
        private int width, height, depth;
        public int[,] values;


        public ImageReader(string path)
        {
            string[] temp = File.ReadAllLines(path);

            switch (temp[0])
            {
                case "P1":
                    this.format = ImageFormat.p1;
                    break;
                case "P2":
                    this.format = ImageFormat.p2;
                    break;
                case "P3":
                    this.format = ImageFormat.p3;
                    break;
                case "P4":
                    this.format = ImageFormat.p4;
                    break;
                case "P5":
                    this.format = ImageFormat.p5;
                    break;
                default:
                    this.format = ImageFormat.X;
                    break;

            }
            this.width = Int32.Parse(temp[1].Split(' ')[0]);
            this.height = Int32.Parse(temp[1].Split(' ')[1]);
            if (temp[1].Split(' ').Length == 3)
            {
                this.depth = Int32.Parse(temp[1].Split(' ')[2]);
            }
            values = new int[this.height, this.width];
            for (int i = 0; i < this.height; i++)
            {
                for (int j = 0; j < this.width; j++)
                {
                    this.values[i, j] = Int32.Parse(temp[i + 2].Split(' ')[j]);
                }
            }

        }
        public void saveImage(string path)
        {

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


        private int Width { get => width; }
        private int Height { get => height; }
        private int Depth { get => depth; }
    }

}
}
