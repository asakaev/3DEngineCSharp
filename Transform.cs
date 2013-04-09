using System;

namespace CubeApp
{
    class Transform
    {
        private static void RotateVertex(Vertex p, double _x, double _y, double _z)
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

        public static void RotateModel(Model obj, double degX, double degY, double degZ)
        {
            // накапливаем угол поворота объекта относительно своего центра
            // просто информация в объекте
            obj.cs.rotation.x += degX;
            obj.cs.rotation.y += degY;
            obj.cs.rotation.z += degZ;

            if ((obj.cs.rotation.x > 359) || (obj.cs.rotation.x < -359)) { obj.cs.rotation.x = obj.cs.rotation.x % 360; }
            if ((obj.cs.rotation.y > 359) || (obj.cs.rotation.y < -359)) { obj.cs.rotation.y = obj.cs.rotation.y % 360; }
            if ((obj.cs.rotation.z > 359) || (obj.cs.rotation.z < -359)) { obj.cs.rotation.z = obj.cs.rotation.z % 360; }

            for (int i = 0; i < obj.vtxCount; i++)
            {
                RotateVertex(obj.points[i], degX, degY, degZ);
            }
        }

        public static void MoveModel(Model obj, double movX, double movY, double movZ)
        {
            obj.cs.placeInWorld.x = obj.cs.placeInWorld.x + movX;
            obj.cs.placeInWorld.y = obj.cs.placeInWorld.y + movY;
            obj.cs.placeInWorld.z = obj.cs.placeInWorld.z + movZ;
        }
    }
}