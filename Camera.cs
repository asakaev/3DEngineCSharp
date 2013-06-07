using System;
namespace Scene3D
{
    class Camera
    {
        public Vertex rotation = new Vertex(0, 0, 0); // куда голова повернута
        public Vertex direction = new Vertex(0, 0, 0); // куда двигаться
        public Vertex inWorld = new Vertex(0, 0, 0);    
        Scene s;

        public Camera(Scene _s) { s = _s; }

        // Принимает вектор, направление движения (0,0,s) — вперед, s — скорость
        public void MoveToCamDirection(double x, double y, double z)
        {
            direction.x = x; direction.y = y; direction.z = z;

            // Оставляем координаты, к которым можно вернуться
            double backX = inWorld.x;
            double backY = inWorld.y;
            double backZ = inWorld.z;

            // Рассчитываем новое положение камеры, учитывая поворот головы
            Transform.RotateVertex(direction, rotation.x, rotation.y, rotation.z);
            inWorld.x -= direction.x;
            inWorld.y -= direction.y;
            inWorld.z += direction.z;

            if (CamInsectWithModels()) // проверка пересечения камеры с моделями
            {
                // если есть пересечение, то откатываемся
                inWorld.x = backX; inWorld.y = backY; inWorld.z = backZ;        
            }
        }

        public void AppendRotate(double x, double y, double z) // Накапливаем поворот
        {
            rotation.x += x; rotation.y += y; rotation.z += z;

            // проверяем на полный поворот вокруг осей
            if ((rotation.x > 359) || (rotation.x < -359)) { rotation.x = rotation.x % 360; }
            if ((rotation.y > 359) || (rotation.y < -359)) { rotation.y = rotation.y % 360; }
            if ((rotation.z > 359) || (rotation.z < -359)) { rotation.z = rotation.z % 360; }
        }

        public void MoveRotate()
        {
            for (int i = 0; i < s.objectsCount; i++)
            {
                Model m = s.objects[i];
                for (int j = 0; j < m.vtxCount; j++)
                {
                    // Move
                    m.points[j].x -= inWorld.x;
                    m.points[j].y -= inWorld.y;
                    m.points[j].z -= inWorld.z;

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
            double minDistance = o.distance * 0.6; // если оставить (* 1), то далеко

            // расстояние между точками (камера и объект)
            double x = Math.Pow((inWorld.x - o.inWorld.x), 2);
            double y = Math.Pow((inWorld.y - o.inWorld.y), 2);
            double z = Math.Pow((inWorld.z - o.inWorld.z), 2);
            double distance = Math.Sqrt(x + y + z);

            // если слишком близко подошли то возвращаем «пересечение»
            if (distance <= minDistance) { return true; }
            else { return false; }
        }
    }
}