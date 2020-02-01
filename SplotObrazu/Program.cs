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
        static readonly string baboonImage = @"C:\Users\user\Pictures\baboon.pgm";

        static void Main(string[] args)
        {

            ImageReader image = new ImageReader(baboonImage);

            
        }
    }
}
