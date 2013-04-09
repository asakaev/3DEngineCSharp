namespace CubeApp
{
    // Hexahedron is a polyhedron with six faces :)
    class Hexahedron:Object3D
    {
        // можно задавать в координатах
        public Hexahedron(Vertex _v0, Vertex _v1, Vertex _v2, Vertex _v3,
            Vertex _v4, Vertex _v5, Vertex _v6, Vertex _v7)
        {
            pointsCount = 8;
            points = new Vertex[8] { _v0, _v1, _v2, _v3, _v4, _v5, _v6, _v7 };
            pointsEdited = new Vertex[8];
            
            for (int i = 0; i < 8; i++) // копируем координаты. одни храним, вторые для отрисовки
            {
                pointsEdited[i] = new Vertex(points[i].x, points[i].y, points[i].z);
            }

            BulidTriangles();
        }

        // а можно только по width, height, depth
        public Hexahedron(double w, double h, double d, double scale)
        {
            Vertex _v0 = new Vertex(0, 0, d); //vertex v0
            Vertex _v1 = new Vertex(0, h, d); //vertex v1
            Vertex _v2 = new Vertex(w, h, d); //vertex v2
            Vertex _v3 = new Vertex(w, 0, d); //vertex v3

            Vertex _v4 = new Vertex(0, 0, 0); //vertex v4
            Vertex _v5 = new Vertex(0, h, 0); //vertex v5
            Vertex _v6 = new Vertex(w, h, 0); //vertex v6
            Vertex _v7 = new Vertex(w, 0, 0); //vertex v7

            pointsCount = 8;
            points = new Vertex[8] { _v0, _v1, _v2, _v3, _v4, _v5, _v6, _v7 };

            // масштабируем модель
            for (int i = 0; i < 8; i++)
            {
                points[i].x *= scale;
                points[i].y *= scale;
                points[i].z *= scale;

            }

            pointsEdited = new Vertex[8];

            // копируем координаты. одни храним, вторые для отрисовки
            for (int i = 0; i < 8; i++) 
            {
                pointsEdited[i] = new Vertex(points[i].x, points[i].y, points[i].z);
            }

            BulidTriangles();
        }

        private void BulidTriangles()
        {
            Vertex a = pointsEdited[0]; // для лучшего понимания
            Vertex b = pointsEdited[1];
            Vertex c = pointsEdited[2];
            Vertex d = pointsEdited[3];

            Vertex e = pointsEdited[4];
            Vertex f = pointsEdited[5];
            Vertex g = pointsEdited[6];
            Vertex h = pointsEdited[7];

            globalID = 0; // сбрасываем счётчик треугольнков

            // лицевая сторона
            tris.Add(new Polygon(a, b, c, globalID++));
            tris.Add(new Polygon(a, c, d, globalID++));

            // задняя
            tris.Add(new Polygon(e, f, g, globalID++));
            tris.Add(new Polygon(e, g, h, globalID++));

            // левая
            tris.Add(new Polygon(a, b, f, globalID++));
            tris.Add(new Polygon(a, f, e, globalID++));

            // правая
            tris.Add(new Polygon(d, c, g, globalID++));
            tris.Add(new Polygon(d, g, h, globalID++));

            // верх
            tris.Add(new Polygon(b, f, g, globalID++));
            tris.Add(new Polygon(b, g, c, globalID++));

            // низ
            tris.Add(new Polygon(a, e, h, globalID++));
            tris.Add(new Polygon(a, h, d, globalID++));

            trisCount = tris.Count;
        }
    }
}