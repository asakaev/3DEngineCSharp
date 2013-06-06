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

        // Накапливаем поворот
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

            move.x += x;// *Math.Cos(rotation.z) * Math.Cos(rotation.y);
            move.y += y;// *Math.Sin(rotation.z);
            move.z += z;// *Math.Cos(rotation.z) * Math.Sin(rotation.y);

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
            
            // если нет пересечения и не обошли все объекты
            while ((!flag) && (i < s.objectsCount))
            {
                flag = IsIntersectWith(s.objects[i++]);
            }

            if (flag) { return true; }
            else { return false; }
        }

        // проверка пересечения камеры с одной! моделью
        public bool IsIntersectWith(Model o)
        {
            double minDistance = o.distance * 0.8; // если оставить (* 1), то далеко

            // расстояние между точками (камера и объект)
            double x = Math.Pow((move.x - o.move.x), 2);
            double y = Math.Pow((move.y - o.move.y), 2);
            double z = Math.Pow((move.z - o.move.z), 2);
            double distance = Math.Sqrt(x + y + z);
            dis = distance;

            // если слишком близко подошли то возвращаем «пересечение»
            if (distance <= minDistance) { return true; }
            else { return false; }
        }

        public string GetDistance()
        {
            return Convert.ToString(dis);
        }
    }
}