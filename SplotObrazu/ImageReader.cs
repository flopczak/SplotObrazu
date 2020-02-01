using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SplotObrazu
{
    enum ImageFormat
    { p1, p2, p3, p4, p5, X }

    class ImageReader
    {
        private ImageFormat format;
        private int width, height, depth;
        public int[,] values;

        public ImageReader(string file)
        {
            FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            switch (NextNonCommentLine(br))
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
            string temp = NextNonCommentLine(br);
            string[] temp2 = temp.Split(' ');
            Console.WriteLine(temp2[0] + " " + temp2[1]);
            
            this.width = Int32.Parse(temp.Split(' ')[0]);
            this.height = Int32.Parse(temp.Split(' ')[1]);
            this.depth = Int32.Parse(NextNonCommentLine(br));
            values = new int[height,width];
            for(int i = 0; i < height; i++)
            {
                for(int j = 0; j< width; j++)
                {
                    values[i, j] = br.ReadByte();
                    Console.Write(values[i, j] + " ");
                }
            }
            br.Close();
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



        static string NextAnyLine(BinaryReader br)
        {
            string s = "";
            byte b = 0;
            while (b != 10) 
            {
                b = br.ReadByte();
                char c = (char)b;
                s += c;
            }
            return s.Trim();
        }

        static string NextNonCommentLine(BinaryReader br)
        {
            string s = NextAnyLine(br);
            while (s.StartsWith("#") || s == "")
                s = NextAnyLine(br);
            return s;
        }


        private int Width { get => width; }
        private int Height { get => height; }
        private int Depth { get => depth; }
    }

}

