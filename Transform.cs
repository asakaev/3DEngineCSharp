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
            for (int i = 0; i < obj.vtxCount; i++)
            {
                RotateVertex(obj.points[i], degX, degY, degZ);
            }
        }

        public static void MoveModel(Model obj, double x, double y, double z)
        {
            obj.placeInWorld.x += x;
            obj.placeInWorld.y += y;
            obj.placeInWorld.z += z;
        }

        public static void RotateScene(Scene sc, double x, double y, double z)
        {
            for (int i = 0; i < sc.objectsCount; i++)
            {
                // вращаем объект относительно своего центра на угол поворота сцены
                RotateModel(sc.objects[i], x, y, z);
                // и вращаем ещё точку положения сцены отностильно начала координат сцены
                RotateVertex(sc.objects[i].placeInWorld, x, y, z);
            }
        }

        public static void MoveScene(Scene sc, double x, double y, double z)
        {
            for (int i = 0; i < sc.objectsCount; i++)
            {
                if (sc.objects[i].name != "coordsXYZ")
                {
                    MoveModel(sc.objects[i], x, y, z);
                }
            }
            //sc.cs.placeInWorld.x += x;
            //sc.cs.placeInWorld.y += y;
            //sc.cs.placeInWorld.z += z;

        }
    }
}