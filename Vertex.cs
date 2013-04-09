using System;

namespace CubeApp
{
    class Vertex // Координаты точки
    {
        public double x, y, z; // математические координаты

        public Vertex(double _x, double _y, double _z)
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