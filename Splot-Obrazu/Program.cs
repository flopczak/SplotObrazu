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
            float f = 1.111111111111111111E-1f;
            float[,] filter1 = new float[3, 3] { {f, f, f }, { f, f, f }, { f, f, f }  };
            float[,] filter2 = new float[3, 3] { { 0f, 1.0f, 0.0f }, { 1.0f, -4.0f, 1.0f }, { 0.0f, 1.0f, 0.0f } };
            float[,] filter3 = new float[3, 3] { { 0f, -1.0f, 0.0f }, { -1.0f, 5.0f, -1.0f }, { 0.0f, -1.0f, 0.0f } };
            float[,] filter4 = new float[3, 3] { { -1.0f, -1.0f, -1.0f }, { -1.0f, 8.0f, -1.0f }, { -1.0f, -1.0f, -1.0f } };
            float[,] filter5 = new float[3, 3] { { 1.0f, 0.0f, -1.0f }, { 0.0f, 0.0f, 0.0f }, { -1.0f, 0.0f, 1.0f } };


            ImagePGM baboonImage = new ImagePGM(baboonImagePath);
            ImagePGM baboonImageCopy = new ImagePGM(baboonImagePath);
            ImageReader imgReader = new ImageReader(baboonImage);
            ImageReader imgReaderCopy = new ImageReader(baboonImageCopy);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 form = new Form1();
            form.PictureBox1.Image = baboonImage.MakeBitmap(1);
            Splot s = new Splot();
            Task t = s.asyncFilterImage(baboonImage, baboonImageCopy, filter1);
            Task.WaitAll(new Task[] {t});
            form.PictureBox2.Image = baboonImage.MakeBitmap(1);
            Application.Run(form);
        }
    }
}
