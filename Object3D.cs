using System.Collections.Generic;

namespace CubeApp
{
    abstract class Object3D
    {
        public List<Triangle> list = new List<Triangle>();
        public CoordsSystem cs = new CoordsSystem();
        public int trisCount;
        public int globalID; // будет раздавать id для треугольников внутри
    }
}