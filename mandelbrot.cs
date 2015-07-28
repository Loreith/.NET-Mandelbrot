using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            int hieght = 1080;
            int width =  1920;
            int iteration = 100;


            Bitmap Mandelbrot = new Bitmap(width, hieght);  
            int apparentwidth = 3;                           //The size on the Argand diagram of the space
            int apparenthieght = 2;

            double xt = -2 - (apparentwidth / width);           //The starting points for the process
            double yt = -1 - (apparenthieght / hieght);         //The take away makes sure that it does the first point on the first run-through. The function in brakets incraments the Argand coordinate in relation to pixels on the screen.

            int x = -1;                                       //The coordinates of the pixel of operation, used to tell the method which colours the pixel which one it is.
            int y = -1;

            for (int xi = 0; xi <= width; xi++)              //Makes the application run for each pixel on the width of the screen.
            {
                xt += apparentwidth / width;                 //Incraments the Cartesian point for the formula to operate on. This will make sure that the real number is always corresponding to the correct Argand coordinate.
                x+=1;                                         //Incraments the pixel coordinate on screen by one.

                yt = -1 - (apparenthieght / hieght);         //Resets the y coordinate on the Argand diagram so the formula covers the entire screen.
                y = -1;

                for (int yi = 0; yi <= hieght; yi++)
                {
                    yt += apparenthieght / hieght;           //Incraments the Cartesian point for the formula to operate on. This will make sure that the imaginary number is always corresponding to the correct Argand coordinate.
                    y += 1;


                    Complex Zn = new Complex(0, 0);         //The seed origionally, set to zero. Zn is used as I cannot put n or n1 in subscript
                    Complex Zn1 = new Complex(0, 0);         //The outcome of each iteration
                    Complex c = new Complex(xt, yt);       //The constant used for the coordinate
                    					                    //Mesures when the number escapes for colouring

                    for (int it = 0; it <= iteration; it++)
                    {
                        Zn1 = Zn * Zn + c;                     				//The formula
						int escapeTime = 0;  
                        escapeTime = escapeTime + 10;                        //Incraments the escape time(If it escapes after one, it will equal 1, ect.)

                        string hex = "#" + escapeTime.ToString("X");
                        System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml(hex);        //These two turn the escape time into a colour.

                        if (Zn1.Real < -2)
                        {
                            Mandelbrot.SetPixel(x, y, col);
                            break;
                        }
                        if (Zn1.Real > 1)
                        {
                            Mandelbrot.SetPixel(x, y, col);
                            break;
                        }
                        if (Zn1.Imaginary < -1)
                        {
                            Mandelbrot.SetPixel(x, y, col);
                            break;
                        }
                        if (Zn1.Imaginary > 1)
                        {
                            Mandelbrot.SetPixel(x, y, col);
                            break;
                        }                                            //These Eight lines check if the number has escaped in any direction.
                        if (escapeTime == iteration)
                        {
                            Mandelbrot.SetPixel(x, y, col);
                            break;
                        }
                    }
                }
            }
			Mandelbrot.Save(“filepath”);
        }
    }
}
