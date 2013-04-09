using System.Collections.Generic;

namespace CubeApp
{
    class Model
    {
        public List<Vertex> points = new List<Vertex>(); // координаты вершин
        public List<Polygon> tris = new List<Polygon>();
        public int pointsCount;
        public int trisCount; // количество треугольников в объекте

        // положение в мире и угол поворота относительно своего центра
        public CoordsSystem cs = new CoordsSystem();
        static public int globalID; // будет раздавать id для треугольников внутри

        public void AddPoint(double x, double y, double z)
        {
            points.Add(new Vertex(x, y, z));
            pointsCount++;
        }

        public void AddPolygon(int a, int b, int c)
        {
            tris.Add(new Polygon(points, a, b, c, globalID++));
            trisCount++;
        }
    }
}