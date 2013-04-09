using System.Collections.Generic;

namespace CubeApp
{
    abstract class Object3D
    {
        public Vertex[] points;
        public Vertex[] pointsEdited;
        public int pointsCount;

        public List<Polygon> tris = new List<Polygon>();

        // положение в мире и угол поворота относительно своего центра
        public CoordsSystem cs = new CoordsSystem();
        public int trisCount; // количество треугольников в объекте
        static public int globalID; // будет раздавать id для треугольников внутри

        public void ResetEditedPoints()
        {
            for (int i = 0; i < pointsCount; i++)
            {
                pointsEdited[i].x = points[i].x;
                pointsEdited[i].y = points[i].y;
                pointsEdited[i].z = points[i].z;
            }
        }
    }
}