using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splot_Obrazu
{
    class Splot
    {
        public static float f = 1.111111111111111111E-1f;
        float[,] filter = new float[3, 3] { {f, f, f }, { f, f, f }, { f, f, f }  };

        private float pixelFilter(ImagePGM image, int x, int y)
        { 
            float temp = 0;
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    temp += (image.Values[x+i-1,y+j-1] * filter[i, j]);
                }
            }
            return temp;
        }

        public void lineFilter(ImagePGM image, int line)
        {
            for(int i = 1; i < image.Width-1; i++)
            {
                Console.Write(i +"pix przed: "+ image.Values[line, i] + ' ');
                image.Values[line,i] = (int)pixelFilter(image,i,line);
                Console.Write(i+"pix po: "+ image.Values[line, i] + ' ');
                Console.WriteLine();
            }
        }
        public void cornersFilter(ImagePGM image)
        {
            Console.WriteLine(image.Values[0, 0]);
            image.Values[0, 0] = (int)((image.Values[0, 0] * filter[1, 1]) + (image.Values[0, 1] * filter[1, 2]) 
                                        + (image.Values[1, 0] * filter[2, 1]) + (image.Values[1, 1] * filter[2, 2]));
            Console.WriteLine(image.Values[0, 0]);

            Console.WriteLine(image.Values[0, image.Width - 1]);
            image.Values[0, image.Width-1] = (int)((image.Values[0, image.Width-1] * filter[1, 1]) + (image.Values[0, image.Width-2] * filter[1, 0])
                                        + (image.Values[1, image.Width-2] * filter[2, 0]) + (image.Values[1, image.Width-1] * filter[2, 1]));
            Console.WriteLine(image.Values[0, image.Width - 1]);


            Console.WriteLine(image.Values[image.Height - 1, 0]);
            image.Values[image.Height-1, 0] = (int)((image.Values[image.Height-1, 0] * filter[1, 1]) + (image.Values[image.Height-2, 0] * filter[0, 1])
                                        + (image.Values[image.Height-2, 1] * filter[0, 2]) + (image.Values[image.Height-1, 1] * filter[1, 2]));
            Console.WriteLine(image.Values[image.Height - 1, 0]);


            Console.WriteLine(image.Values[image.Height - 1, image.Width - 1]);
            image.Values[image.Height-1, image.Width-1] = (int)((image.Values[image.Height - 1, image.Width - 1] * filter[1, 1]) + (image.Values[image.Height - 2, image.Width - 1] * filter[0, 1])
                                        + (image.Values[image.Height - 2, image.Width - 2] * filter[0, 0]) + (image.Values[image.Height - 1, image.Width - 2] * filter[1, 0]));
            Console.WriteLine(image.Values[image.Height - 1, image.Width - 1]);
        }
        
        public void sidesFilter(ImagePGM image)
        {
            for (int i = 1; i < image.Width - 1; i++)
            {
                Console.Write(i + "pix przed: " + image.Values[image.Height - 1, i] + ' ');
               // image.Values[0, i] = (int)upperPixelFilter(image, i, 0);
               // image.Values[image.Height-1, i] = (int)bottomPixelFilter(image, i, image.Height-1);
               // image.Values[image.Height - 1, i] = (int)bottomPixelFilter(image, i, 0);
                Console.Write(i + "pix po: " + image.Values[image.Height - 1, i] + ' ');
                Console.WriteLine();
            }

        }

        private float upperPixelFilter(ImagePGM image, int x, int y)
        {
            float temp = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 1; j < 3; j++)
                {
                    temp += (image.Values[x + i + 1, y + j - 1] * filter[i, j]);
                }
            }
            return temp;
        }
        private float bottomPixelFilter(ImagePGM image, int x, int y)
        {
            float temp = 0;
            for (int i = 1; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    temp += (image.Values[y + i - 1, x + j - 1] * filter[i, j]);
                }
            }
            return temp;
        }
        private float leftSidePixelFilter(ImagePGM image, int x, int y)
        {
            float temp = 0;
            for (int i = 1; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    temp += (image.Values[x + i - 1, y + j - 1] * filter[i, j]);
                }
            }
            return temp;
        }
    }
}
