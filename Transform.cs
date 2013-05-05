using System;

namespace Scene3D
{
    class Transform
    {
        public static void RotateVertex(Vertex p, double _x, double _y, double _z)
        {
            double radX = (Math.PI * _x) / 180;
            double radY = (Math.PI * _y) / 180;
            double radZ = (Math.PI * _z) / 180;

            double x = p.x;
            double y = p.y;
            double z = p.z;

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