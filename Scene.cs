using System.Collections.Generic;

namespace Scene3D
{
    class Scene
    {
        public List<Model> objects = new List<Model>();
        public int objectsCount;
        public int polyCount;
        public int vtxCount;

        public Scene()
        {
            objectsCount = 0;
            polyCount = 0;
        }

        public void AddObject(Model obj)
        {
            objects.Add(obj);
            objectsCount++;

            // обновляем количество полигонов и вертексов в сцене
            polyCount = 0;
            vtxCount = 0;
            for (int i = 0; i < objectsCount; i++)
            {
                polyCount += objects[i].polyCount;
                vtxCount += objects[i].vtxCount;
            }
        }
    }
}