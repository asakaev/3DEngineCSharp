using System;
using System.Collections.Generic;
using System.Drawing;

namespace Scene3D
{
    class Scene
    {
        public List<Model> objects = new List<Model>();
        public int objectsCount;
        public int polyCount;
        public int vtxCount;
        public int activeObject; // номер активного объекта
        public Vertex scale = new Vertex(1, 1, 1);
        public Vertex rotation = new Vertex(0, 0, 0);
        public Vertex move = new Vertex(0, 0, 0);
        public Camera cam;
        bool perspective = true; // перспективная проекция

        public Scene()
        {
            objectsCount = 0;
            polyCount = 0;
            activeObject = -1; // нет активных объектов
            cam = new Camera(this); // создаем новую камеру и передаем ей эту сцену
        }

        public void AddObject(Model obj)
        {
            objects.Add(obj); // список и его метод Add, добавление нового элемента
            obj.s = this; // устанавливаем сцену родителем для всех моделей
            objectsCount++;

            // обновляем количество полигонов и вертексов в сцене
            polyCount = 0;
            vtxCount = 0;
            for (int i = 0; i < objectsCount; i++)
            {
                polyCount += objects[i].polyCount;
                vtxCount += objects[i].vtxCount;
            }
        }

        public void DrawScene(Rasterizer r)
        {
            DoTransform(); // Пересчет всех координат

            // Применение проекции (Либо параллельной, либо перспективной)
            Projection(r.Width, r.Height); // передаем размеры окна

            CColor c = new CColor(78, 201, 176); // green

            for (int i = 0; i < objectsCount; i++) // для всех объектов сцены
            {
                if (objects[i].active == false) // выбираем цвет, которым рисовать объект
                {
                    c.R = 78; // green
                    c.G = 201;
                    c.B = 176;
                }
                else // если активен
                {
                    c.R = 214; // orange
                    c.G = 157;
                    c.B = 133;
                }

                for (int j = 0; j < objects[i].polyCount; j++) // для всех треугольников
                {
                    Draw.Triangle(r, c, objects[i].tris[j]);
                }
            }
        }

        public string GetActiveName()
        {
            if (activeObject != -1)
            {
                return ", Active object: " + objects[activeObject].name;
            }
            else return null;
        }

        public void ActivateNext()
        {
            if (activeObject != -1) // если есть активный объект
            {
                if (activeObject != objectsCount-1) // если не последний объект в сцене
                {
                    activeObject++;
                    objects[activeObject].active = true;
                    objects[activeObject-1].active = false;
                }
                else // если последний
                {
                    objects[activeObject].active = false;
                    activeObject = -1;
                }
            }
            else // если не было активных
            {
                objects[0].active = true;
                activeObject++;
            }
        }

        public void ActivatePrev()
        {
            if (activeObject != -1) // если есть активный объект
            {
                if (activeObject != 0) // если не первый в сцене
                {
                    activeObject--;
                    objects[activeObject].active = true;
                    objects[activeObject + 1].active = false;
                }
                else // если первый
                {
                    objects[activeObject].active = false;
                    activeObject = -1;
                }
            }
            else // если не было активных
            {
                objects[objectsCount-1].active = true;
                activeObject = objectsCount - 1;
            }
        }

        public void AppendScale(double x, double y, double z)
        {
            scale.x += x;
            scale.y += y;
            scale.z += z;
        }

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
            move.x += x;
            move.y += y;
            move.z += z;
        }

        public void DoTransform() // пересчет всех координат
        {
            for (int i = 0; i < objectsCount; i++) // Операции с моделями
            {
                objects[i].ScaleRotateMove();
            }
            // Теперь со сценой и камерой
            ScaleRotateMove();
            cam.MoveRotate();
        }

        public void ScaleRotateMove() // рассчет для сцены
        {
            for (int i = 0; i < objectsCount; i++)
            {
                Model m = objects[i];
                for (int j = 0; j < m.vtxCount; j++)
                {
                    // Scale
                    m.points[j].x /= scale.x;
                    m.points[j].y /= scale.y;
                    m.points[j].z /= scale.z;

                    // Rotation
                    double xR = rotation.x;
                    double yR = rotation.y;
                    double zR = rotation.z;
                    Transform.RotateVertex(m.points[j], xR, yR, zR);

                    // Move
                    m.points[j].x += move.x;
                    m.points[j].y += move.y;
                    m.points[j].z += move.z;
                }
            }
        }

        public void Projection(double w, double h)
        {
            if (perspective)
            {
                // Центр дисплея
                double oX = w / 2;
                double oY = h / 2;

                double D = w;
                double Ofs = h/16; // расстояние от экранной плоскости до объекта

                for (int i = 0; i < objectsCount; i++)
                {
                    Model m = objects[i];
                    for (int j = 0; j < m.vtxCount; j++)
                    {
                        double k = D / (m.points[j].z + Ofs);
                        // чем дальше, тем меньше объект
                        m.points[j].x = oX + (m.points[j].x * k);
                        m.points[j].y = oY - (m.points[j].y * k);
                    }
                }
            }
            else
            {
                double oX = w / 2;
                double oY = h / 2;

                for (int i = 0; i < objectsCount; i++)
                {
                    Model m = objects[i];
                    for (int j = 0; j < m.vtxCount; j++)
                    {
                        m.points[j].x = oX + m.points[j].x;
                        m.points[j].y = oY - m.points[j].y;
                    }
                }
            }
        }
    }
}