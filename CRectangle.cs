using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeApp
{
    class CRectangle:Object3D
    {
        // обход точек по часовой стрелке
        public CRectangle(Vertex _a, Vertex _b, Vertex _c, Vertex _d)
        {
            points = new Vertex[4] { _a, _b, _c, _d };
            pointsEdited = new Vertex[4];
            pointsCount = 4;

            for (int i = 0; i < 4; i++) // копируем координаты. одни храним, вторые для отрисовки
            {
                pointsEdited[i] = new Vertex(points[i].x, points[i].y, points[i].z);
            }

            Vertex a = pointsEdited[0]; // для лучшего понимания
            Vertex b = pointsEdited[1];
            Vertex c = pointsEdited[2];
            Vertex d = pointsEdited[3];

            tris.Add(new Polygon(a, b, c, globalID++)); // верхний треугольник
            tris.Add(new Polygon(a, c, d, globalID++)); // нижний
            trisCount = tris.Count;
        }
    }
}