using System;

namespace Scene3D
{
    class Transform
    {
        // принимает вершину и три угла, на которые её нужно повернуть (в градусах)
        public static void RotateVertex(Vertex p, double _x, double _y, double _z)
        {
            double radX = (Math.PI * _x) / 180; // перевод в радианы
            double radY = (Math.PI * _y) / 180;
            double radZ = (Math.PI * _z) / 180;

            double x = p.x; // временные координаты, что б не перезатирались данные
            double y = p.y; // последовательно выполняется поворот,
            double z = p.z; // опираясь на предыдущие шаги

            // Поворот относительно оси X
            p.y = (y * Math.Cos(radX)) - (z * Math.Sin(radX));
            p.z = (y * Math.Sin(radX)) + (z * Math.Cos(radX));

            // Поворот относительно оси Y
            z = p.z;
            p.x = (x * Math.Cos(radY)) - (z * Math.Sin(radY));
            p.z = (x * Math.Sin(radY)) + (z * Math.Cos(radY));

            // Поворот относительно оси Z
            x = p.x;
            y = p.y;
            p.x = (x * Math.Cos(radZ)) - (y * Math.Sin(radZ));
            p.y = (x * Math.Sin(radZ)) + (y * Math.Cos(radZ));
        }
    }
}