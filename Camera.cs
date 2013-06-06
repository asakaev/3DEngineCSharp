using System;
namespace Scene3D
{
    class Camera
    {
        public Vertex rotation = new Vertex(0, 0, 0);
        public Vertex move = new Vertex(0, 0, 0);
        Scene s;
        public double dis;

        public Camera(Scene _s) { s = _s; }

        public void AppendRotate(double x, double y, double z)
        {
            rotation.x += x;
            rotation.y += y;
            rotation.z += z;

            // проверяем на полный поворот вокруг осей
            if ((rotation.x > 359) || (rotation.x < -359)) { rotation.x = rotation.x % 360; }
            if ((rotation.y > 359) || (rotation.y < -359)) { rotation.y = rotation.y % 360; }
            if ((rotation.z > 359) || (rotation.z < -359)) { rotation.z = rotation.z % 360; }
        }

        public void AppendMove(double x, double y, double z)
        {
            double backX = move.x;
            double backY = move.y;
            double backZ = move.z;

            move.x += x;
            move.y += y;
            move.z += z;

            if (CamInsectWithModels()) // проверка пересечения камеры с моделями
            {
                move.x = backX;
                move.y = backY;
                move.z = backZ;
            }

        }

        public void MoveRotate()
        {
            for (int i = 0; i < s.objectsCount; i++)
            {
                Model m = s.objects[i];
                for (int j = 0; j < m.vtxCount; j++)
                {
                    // Move
                    m.points[j].x -= move.x;
                    m.points[j].y -= move.y;
                    m.points[j].z -= move.z;

                    // Rotation
                    double xR = rotation.x;
                    double yR = rotation.y;
                    double zR = rotation.z;
                    Transform.RotateVertex(m.points[j], xR, yR, zR);
                }
            }
        }

        public bool CamInsectWithModels() // проверка пересечения каждой модели с камерой
        {
            bool flag = false;
            int i = 0;

            while ((!flag) && (i < s.objectsCount))
            {
                flag = IsIntersectWith(s.objects[i++]);
            }

            if (flag) { return true; }
            else { return false; }
        }

        public bool IsIntersectWith(Model o)
        {
            double magic = o.distance * 0.8; // не знаю почему 8
            double x = Math.Pow((move.x - o.move.x), 2);
            double y = Math.Pow((move.y - o.move.y), 2);
            double z = Math.Pow((move.z - o.move.z), 2);

            double distance = Math.Sqrt(x + y + z);
            dis = distance;

            if (distance <= magic) { return true; }
            else { return false; }
        }

        public string GetDistance()
        {
            return Convert.ToString(dis);
        }
    }
}