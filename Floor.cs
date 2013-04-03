using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeApp
{
    class Floor:Object3D
    {
        public Floor(double w, double h)
        {
            globalID = 0;
            // дальний треугольник
            list.Add(new Triangle(0, 0, -h, 0, 0, 0, w, 0, 0, globalID++));

            // ближний
            list.Add(new Triangle(0, 0, -h, w, 0, 0, w, 0, -h, globalID++));
            trisCount = list.Count;
        }
    }
}