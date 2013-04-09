using System.Collections.Generic;

namespace CubeApp
{
    class Scene
    {
        public List<Object3D> objects = new List<Object3D>();
        public int objectsCount;
        public CoordsSystem cs = new CoordsSystem();

        public Scene()
        {
            objectsCount = 0;
        }

        public void AddObject(Object3D obj)
        {
            objects.Add(obj);
            objectsCount++;
        }
    }
}
