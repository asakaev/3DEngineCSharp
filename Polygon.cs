namespace CubeApp
{
    // Triangle (faces, грани)
    class Polygon
    {
        public Vertex[] tri = new Vertex[3];
        public int id;

        public Polygon(Vertex a, Vertex b, Vertex c, int _id)
        {
            tri[0] = a; // указатели на вершины
            tri[1] = b;
            tri[2] = c;
            id = _id;
        }
    }
}