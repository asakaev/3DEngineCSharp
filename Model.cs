using System.Collections.Generic;

namespace Scene3D
{
    class Model
    {
        public List<Vertex> points = new List<Vertex>(); // координаты вершин
        public List<Polygon> tris = new List<Polygon>();
        public int vtxCount;
        public int polyCount; // количество треугольников в объекте
        public Vertex placeInWorld = new Vertex(0, 0, 0);
        public string name;
        public bool active;

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
    }
}