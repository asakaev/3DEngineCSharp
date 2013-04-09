using System.Collections.Generic;

namespace CubeApp
{
    class Scene
    {
        public List<Model> objects = new List<Model>();
        public int objectsCount;
        public CoordsSystem cs = new CoordsSystem();

        public Scene()
        {
            objectsCount = 0;
        }

        public void AddObject(Model obj)
        {
            objects.Add(obj);
            objectsCount++;
        }
    }
}
