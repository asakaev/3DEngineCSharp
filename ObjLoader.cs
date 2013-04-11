using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace CubeApp
{
    class ObjLoader
    {
        public static Model Load(string fname, double scale) // путь и масштабирование
        {
            Model obj = new Model();
            // Для распознования точки точкой, а не запятой
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            string path = @"..\..\models\" + fname;

            if (File.Exists(path)) // проверяем есть ли файл
            {
                foreach (string line in File.ReadLines(path))
                {
                    if (!String.IsNullOrEmpty(line)) // проверка на пустую строку
                    {
                        // Получаем координаты вершин
                        if ((line.ElementAt(0) == 'v') && (line.ElementAt(1) == ' '))
                        {
                            var parts = line.Split(' ');
                            // начинаем не с [0] т.к. в нём хранится 'v'
                            double x = Convert.ToDouble(parts[1], provider) * scale;
                            double y = Convert.ToDouble(parts[2], provider) * scale;
                            double z = Convert.ToDouble(parts[3], provider) * scale;
                            obj.AddVertex(x, y, z);
                        }

                        // Получаем номера точек для построения граней
                        if ((line.ElementAt(0) == 'f') && (line.ElementAt(1) == ' '))
                        {
                            var parts = line.Split(' ');
                            // Дополнительно сплит т.к. грани: f x/x/x
                            int a = Convert.ToInt32(parts[1].Split('/')[0]);
                            int b = Convert.ToInt32(parts[2].Split('/')[0]);
                            int c = Convert.ToInt32(parts[3].Split('/')[0]);
                            obj.AddPolygon(a, b, c);
                        }
                    }
                    obj.name = fname.Split('.')[0]; // записываем имя модели
                }
                return obj;
            }
            else return null;
        }
    }
}