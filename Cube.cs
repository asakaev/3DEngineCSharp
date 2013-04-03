using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeApp
{
    class Cube:Object3D
    {
        public Cube(double size)
        {
            double hs = size / 2;
            globalID = 0;

            // лицевая сторона
            list.Add(new Triangle(-hs, -hs, hs, -hs, hs, hs, hs, hs, hs, globalID++));
            list.Add(new Triangle(-hs, -hs, hs, hs, hs, hs, hs, -hs, hs, globalID++));

            // задняя
            list.Add(new Triangle(-hs, -hs, -hs, -hs, hs, -hs, hs, hs, -hs, globalID++));
            list.Add(new Triangle(-hs, -hs, -hs, hs, hs, -hs, hs, -hs, -hs, globalID++));

            // левая
            list.Add(new Triangle(-hs, -hs, hs, -hs, hs, hs, -hs, hs, -hs, globalID++));
            list.Add(new Triangle(-hs, -hs, hs, -hs, hs, -hs, -hs, -hs, -hs, globalID++));

            // правая
            list.Add(new Triangle(hs, -hs, hs, hs, hs, hs, hs, hs, -hs, globalID++));
            list.Add(new Triangle(hs, -hs, hs, hs, hs, -hs, hs, -hs, -hs, globalID++));

            // верх
            list.Add(new Triangle(-hs, hs, hs, -hs, hs, -hs, hs, hs, -hs, globalID++));
            list.Add(new Triangle(-hs, hs, hs, hs, hs, -hs, hs, hs, hs, globalID++));

            // низ
            list.Add(new Triangle(-hs, -hs, hs, -hs, -hs, -hs, hs, -hs, -hs, globalID++));
            list.Add(new Triangle(-hs, -hs, hs, hs, -hs, -hs, hs, -hs, hs, globalID++));

            trisCount = list.Count;
        }
    }
}