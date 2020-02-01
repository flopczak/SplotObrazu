using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SplotObrazu
{
   
    class ImageReader
    {
        
        public ImageReader(ImagePGM imagePGM)
        {
            FileStream fs = new FileStream(imagePGM.Path, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            switch (NextNonCommentLine(br))
            {
                case "P1":
                    imagePGM.Format = ImageFormat.p1;
                    break;
                case "P2":
                    imagePGM.Format = ImageFormat.p2;
                    break;
                case "P3":
                    imagePGM.Format = ImageFormat.p3;
                    break;
                case "P4":
                    imagePGM.Format = ImageFormat.p4;
                    break;
                case "P5":
                    imagePGM.Format = ImageFormat.p5;
                    break;
                default:
                    imagePGM.Format = ImageFormat.X;
                    break;

            }
            string temp = NextNonCommentLine(br);
            string[] temp2 = temp.Split(' ');
            Console.WriteLine(temp2[0] + " " + temp2[1]);
            
            imagePGM.Width = Int32.Parse(temp.Split(' ')[0]);
            imagePGM.Height = Int32.Parse(temp.Split(' ')[1]);
            imagePGM.Depth = Int32.Parse(NextNonCommentLine(br));
            imagePGM.Values = new int[imagePGM.Height, imagePGM.Width];
            for(int i = 0; i < imagePGM.Height; i++)
            {
                for(int j = 0; j< imagePGM.Width; j++)
                {
                    imagePGM.Values[i, j] = br.ReadByte();
                    Console.Write(imagePGM.Values[i, j] + " ");
                }
            }
            br.Close();
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
    }

}

