using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace CubeApp
{
    class ObjLoader
    {
        // путь формат записи и масштабирование
        public static Model Load(string path, double scale)
        {
            Model obj = new Model();

            // Для распознования точки точкой, а не запятой
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";

            foreach (string line in File.ReadLines(@path))
            {
                // проверка на пустую строку
                if (!String.IsNullOrEmpty(line))
                {
                    // Получаем координаты вершин
                    if ((line.ElementAt(0) == 'v') && (line.ElementAt(1) == ' '))
                    {
                        var parts = line.Split(' ');

                        // начинаем не с [0] т.к. в нём хранится 'v'
                        double x = Convert.ToDouble(parts[1], provider) * scale;
                        double y = Convert.ToDouble(parts[2], provider) * scale;
                        double z = Convert.ToDouble(parts[3], provider) * scale;
                        obj.AddPoint(x, y, z);
                    }

                    // Получаем номера точек для построения граней
                    if ((line.ElementAt(0) == 'f') && (line.ElementAt(1) == ' '))
                    {
                        var parts = line.Split(' ');

                        var v1 = parts[1];
                        var vv1 = v1.Split('/');
                        int a = Convert.ToInt32(vv1[0]);

                        var v2 = parts[2];
                        var vv2 = v2.Split('/');
                        int b = Convert.ToInt32(vv2[0]);

                        var v3 = parts[3];
                        var vv3 = v3.Split('/');
                        int c = Convert.ToInt32(vv3[0]);

                        obj.AddPolygon(a, b, c);
                    }
                }
            }
            return obj;
        }
    }
}