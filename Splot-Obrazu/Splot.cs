using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splot_Obrazu
{
    class Splot
    {
        
       

        private int[,] getPixelNeighborhood(ImagePGM image ,int y, int x)
        {
            return new int[,] { { image.Values[y - 1, x - 1], image.Values[y - 1, x ],image.Values[y-1,x+1], },
                {image.Values[y,x-1],image.Values[y,x],image.Values[y,x+1] },
                {image.Values[y+1,x-1],image.Values[y+1,x],image.Values[y-1,x+1] } };
        }

        private float pixelFilter(ImagePGM image, int x, int y,  float[,] filter)
        { 
            float temp = 0;
            int[,] pixelNeighborgood = getPixelNeighborhood(image, y, x);
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    temp += (pixelNeighborgood[i,j] * filter[i, j]);
                }
            }
            return temp;
        }

        public void lineFilter(ImagePGM image, ImagePGM imageCopy, int line , float[,] filter)
        {
            for(int i = 1; i < image.Width-1; i++)
            {
                image.Values[line,i] = (int)pixelFilter(imageCopy,i,line, filter);
                
            }
        }

        public void centerFilter(ImagePGM image, ImagePGM imageCopy, float[,] filter)
        {
            for (int i = 1; i < image.Height - 2; i++)
            {
                lineFilter(image, imageCopy, i, filter);
            }
        }


        public void cornersFilter(ImagePGM image, float[,] filter)
        {
            image.Values[0, 0] = (int)((image.Values[0, 0] * filter[1, 1]) + (image.Values[0, 1] * filter[1, 2]) 
                                        + (image.Values[1, 0] * filter[2, 1]) + (image.Values[1, 1] * filter[2, 2]));
            

            image.Values[0, image.Width-1] = (int)((image.Values[0, image.Width-1] * filter[1, 1]) + (image.Values[0, image.Width-2] * filter[1, 0])
                                        + (image.Values[1, image.Width-2] * filter[2, 0]) + (image.Values[1, image.Width-1] * filter[2, 1]));
            

            image.Values[image.Height-1, 0] = (int)((image.Values[image.Height-1, 0] * filter[1, 1]) + (image.Values[image.Height-2, 0] * filter[0, 1])
                                        + (image.Values[image.Height-2, 1] * filter[0, 2]) + (image.Values[image.Height-1, 1] * filter[1, 2]));
     

            image.Values[image.Height-1, image.Width-1] = (int)((image.Values[image.Height - 1, image.Width - 1] * filter[1, 1]) + (image.Values[image.Height - 2, image.Width - 1] * filter[0, 1])
                                        + (image.Values[image.Height - 2, image.Width - 2] * filter[0, 0]) + (image.Values[image.Height - 1, image.Width - 2] * filter[1, 0]));
 
        }

        public void sidesFilter(ImagePGM image, float[,] filter)
        {
            for (int i = 1; i < image.Width - 1; i++)
            {
                image.Values[0, i] = (int)upperPixelFilter(image, i, 0, filter);
                image.Values[image.Height - 1, i] = (int)bottomPixelFilter(image, i, image.Height - 1, filter);
            }

            for (int i = 1; i < image.Height - 1; i++)
            {
                image.Values[i, 0] = (int)leftSidePixelFilter(image, i, 0, filter);
                image.Values[i, image.Width - 1] = (int)rightSidePixelFilter(image, i, image.Width - 1, filter);
            }

        }

        private float upperPixelFilter(ImagePGM image, int x, int y, float[,] filter)
        {
            float temp = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 1; j < 3; j++)
                {
                    temp += (image.Values[x + i - 1, y + j - 1] * filter[i, j]);
                }
            }
            return temp;
        }
        private float bottomPixelFilter(ImagePGM image, int x, int y, float[,] filter)
        {
            float temp = 0;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    temp += (image.Values[y - i, x + j - 1] * filter[i, j]);
                }
            }
            return temp;
        }
        private float leftSidePixelFilter(ImagePGM image, int x, int y, float[,] filter)
        {
            float temp = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 1; j < 3; j++)
                {
                    temp += (image.Values[x + i - 1, y + j - 1] * filter[i, j]);
                }
            }
            return temp;
        }
        private float rightSidePixelFilter(ImagePGM image, int x, int y, float[,] filter)
        {
            float temp = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    temp += (image.Values[x + i - 1, y + j - 1] * filter[i, j]);
                }
            }
            return temp;
        }
        public void filterImage(ImagePGM image, ImagePGM imageCopy, float[,] filter )
        {
            centerFilter(image, imageCopy, filter);
            sidesFilter(image, filter);
            cornersFilter(image, filter);
        }

        public async Task asyncCenterFilter(ImagePGM image, ImagePGM imageCopy, float[,] filter)
        {
            for (int i = 1; i < (image.Height - 2)/2; i++)
            {
                Task.Run(() => lineFilter(image, imageCopy, i, filter));
            }
            for (int i = (image.Height - 2) / 2; i < image.Height - 2; i++)
            {
                Task.Run(() => lineFilter(image, imageCopy, i, filter));
            }
            
        }
        public async Task asyncFilterImage(ImagePGM image, ImagePGM imageCopy, float[,] filter)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            await asyncCenterFilter(image,imageCopy,filter);
            await Task.Run(() => cornersFilter(image, filter));
            await Task.Run(() => sidesFilter(image, filter));

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("czas asynchornicznie: "+elapsedMs+"ms");
        }
    }
}
