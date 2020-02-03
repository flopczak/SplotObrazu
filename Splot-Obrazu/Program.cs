using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Splot_Obrazu
{
    static class Program
    {

        static readonly string baboonImagePath = @"C:\Users\user\Pictures\baboon.pgm";
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {
            ImagePGM baboonImage = new ImagePGM(baboonImagePath);
            ImageReader imgReader = new ImageReader(baboonImage);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 form = new Form1();
            form.PictureBox1.Image = baboonImage.MakeBitmap(1);
            Splot s = new Splot();
            // s.lineFilter(baboonImage, 1);
            s.sidesFilter(baboonImage);
            s.cornersFilter(baboonImage);
            Application.Run(form);
        }
    }
}
