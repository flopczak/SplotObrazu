using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplotObrazu
{
    class Program
    {
        static readonly string baboonImagePath = @"C:\Users\user\Pictures\baboon.pgm";

        static void Main(string[] args)
        {
            ImagePGM baboonImage = new ImagePGM(baboonImagePath);
            ImageReader imgReader = new ImageReader(baboonImage);

            
        }
    }
}
