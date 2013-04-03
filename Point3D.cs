using System;

namespace CubeApp
{
    class Point3D // Координаты точки
    {
        public double x, y, z; // математические координаты

        public Point3D(double _x, double _y, double _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }

        public int GetIntX() { return Convert.ToInt32(x); }
        public int GetIntY() { return Convert.ToInt32(y); }
        public int GetIntZ() { return Convert.ToInt32(z); }
    }
}