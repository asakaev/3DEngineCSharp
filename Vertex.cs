using System;

namespace Scene3D
{
    class Vertex // Координаты точки
    {
        public double x, y, z; // математические координаты
        public double xOrg, yOrg, zOrg; // они же, но меняться не будут (отталкиваемся от них)

        public Vertex(double _x, double _y, double _z)
        {
            xOrg = x = _x;
            yOrg = y = _y;
            zOrg = z = _z;
        }

        public int GetIntX() { return Convert.ToInt32(x); }
        public int GetIntY() { return Convert.ToInt32(y); }
        public int GetIntZ() { return Convert.ToInt32(z); }
    }
}