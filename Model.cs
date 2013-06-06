﻿using System;
using System.Collections.Generic;

namespace Scene3D
{
    class Model
    {
        public List<Vertex> points = new List<Vertex>(); // координаты вершин
        public List<Polygon> tris = new List<Polygon>();
        public int vtxCount;
        public int polyCount; // количество треугольников в объекте
        public string name;
        public bool active;
        public Vertex scale = new Vertex(1, 1, 1);
        public Vertex rotation = new Vertex(0, 0, 0);
        public Vertex move = new Vertex(0, 0, 0);
        public Scene s;
        public double distance;

        public void CheckDistance()
        {
            double max = 0;
            for (int i = 0; i < vtxCount; i++) // расстояние от центра масс до дальней точки фигуры
            {
                double length = Math.Sqrt(
                    Math.Pow((move.x - (scale.x * points[i].x + move.x)), 2) +
                    Math.Pow((move.y - (scale.y * points[i].y + move.y)), 2) +
                    Math.Pow((move.z - (scale.z * points[i].z + move.z)), 2));

                if (length > max) { max = length; }
            }
            distance = max;
        }

        public void AddVertex(double x, double y, double z)
        {
            points.Add(new Vertex(x, y, z));
            vtxCount++;
        }

        public void AddPolygon(int a, int b, int c)
        {
            tris.Add(new Polygon(points, a, b, c));
            polyCount++;
        }

        public void AppendScale(double x, double y, double z) // Масштабирование
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

        public void ScaleRotateMove() // преобразования
        {
            for (int i = 0; i < vtxCount; i++)
            {
                // Scale (масштаб)
                if ((scale.x != 0) && (scale.y != 0) && (scale.z != 0))
                {
                    points[i].x = points[i].xOrg / scale.x;
                    points[i].y = points[i].yOrg / scale.y;
                    points[i].z = points[i].zOrg / scale.z;
                }

                // Rotation
                Transform.RotateVertex(points[i], rotation.x, rotation.y, rotation.z);

                // Move
                points[i].x += move.x;
                points[i].y += move.y;
                points[i].z += move.z;
            }
            CheckDistance(); // рассчитываем расстояние внутри модели
        }
    }
}