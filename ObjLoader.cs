using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace CubeApp
{
    class ObjLoader
    {
        static void Load(string path, Object3D obj)
        {
            double a;
            double b;
            double c;

            // Для распознования точки точкой
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";

            foreach (string line in File.ReadLines(@"C:\1.txt"))
            {
                // Получаем координаты вершин
                if ((line.ElementAt(0) == 'v') && (line.ElementAt(1) == ' '))
                {
                    var parts = line.Split(' ');

                    // начинаем не с [0] т.к. в нём хранится 'v'
                    a = Convert.ToDouble(parts[1], provider);
                    b = Convert.ToDouble(parts[2], provider);
                    c = Convert.ToDouble(parts[3], provider);

                    Console.WriteLine(a);
                    //Console.WriteLine(b);
                    //Console.WriteLine(c);
                }

                // Получаем номера точек для построения граней
                if ((line.ElementAt(0) == 'f') && (line.ElementAt(1) == ' '))
                {
                    var parts = line.Split(' ');

                    // начинаем не с [0] т.к. в нём хранится 'f'
                    a = (int)parts[1].ElementAt(0) - 48;
                    b = (int)parts[2].ElementAt(0) - 48;
                    c = (int)parts[3].ElementAt(0) - 48;

                    Console.WriteLine(a);
                    //Console.WriteLine(b);
                    //Console.WriteLine(c);
                }
                
            }

        }
    }
}