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

        // возвращает округленные значения для вывода на экран
        public int GetIntX() { return (int)Math.Round(x); }
        public int GetIntY() { return (int)Math.Round(y); }
        public int GetIntZ() { return (int)Math.Round(z); }
    }
}