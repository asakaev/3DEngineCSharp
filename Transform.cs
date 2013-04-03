using System;

namespace CubeApp
{
    class Transform
    {
        public static void RotateTriangle(Triangle t, CoordsSystem cs)
        {
            double radX = (Math.PI * cs.rotation.x) / 180;
            double radY = (Math.PI * cs.rotation.y) / 180;
            double radZ = (Math.PI * cs.rotation.z) / 180;

            for (int i = 0; i < 3; i++)
            {
                double x = t.triOrg[i].x;
                double y = t.triOrg[i].y;
                double z = t.triOrg[i].z;

                // Поворот относительно оси X
                t.tri[i].y = (y * Math.Cos(radX)) - (z * Math.Sin(radX));
                t.tri[i].z = (y * Math.Sin(radX)) + (z * Math.Cos(radX));

                // Поворот относительно оси Y
                z = t.tri[i].z;
                t.tri[i].x = (x * Math.Cos(radY)) - (z * Math.Sin(radY));
                t.tri[i].z = (x * Math.Sin(radY)) + (z * Math.Cos(radY));

                // Поворот относительно оси Z
                x = t.tri[i].x;
                y = t.tri[i].y;
                t.tri[i].x = (x * Math.Cos(radZ)) - (y * Math.Sin(radZ));
                t.tri[i].y = (x * Math.Sin(radZ)) + (y * Math.Cos(radZ));
            }
        }

        public static void RotateModel(Object3D obj, double degX, double degY, double degZ)
        {
            // сохраняем угол поворота объкта относительно своего центра
            obj.cs.rotation.x += degX;
            obj.cs.rotation.y += degY;
            obj.cs.rotation.z += degZ;

            if ((obj.cs.rotation.x > 359) || (obj.cs.rotation.x < -359)) { obj.cs.rotation.x = obj.cs.rotation.x % 360; }
            if ((obj.cs.rotation.y > 359) || (obj.cs.rotation.y < -359)) { obj.cs.rotation.y = obj.cs.rotation.y % 360; }
            if ((obj.cs.rotation.z > 359) || (obj.cs.rotation.z < -359)) { obj.cs.rotation.z = obj.cs.rotation.z % 360; }

            foreach (Triangle t in obj.list)
            {
                Transform.RotateTriangle(t, obj.cs);
            }
        }

        public static void MoveModel(Object3D obj, double movX, double movY, double movZ)
        {
            obj.cs.placeInWorld.x = obj.cs.placeInWorld.x + movX;
            obj.cs.placeInWorld.y = obj.cs.placeInWorld.y + movY;
            obj.cs.placeInWorld.z = obj.cs.placeInWorld.z + movZ;
        }
    }
}