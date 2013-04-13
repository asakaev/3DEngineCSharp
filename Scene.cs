using System.Collections.Generic;
using System.Drawing;

namespace Scene3D
{
    class Scene
    {
        public List<Model> objects = new List<Model>();
        public int objectsCount;
        public int polyCount;
        public int vtxCount;
        private int activeObject;

        public Scene()
        {
            objectsCount = 0;
            polyCount = 0;
            activeObject = -1; // нет активных объектов
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

        public void DrawScene(Bitmap image)
        {
            Draw.Background(image); // отрисовка фона
            Draw.PolyPointsObjectsCount(this, image); // инфа о сцене в углу

            for (int i = 0; i < objectsCount; i++) // для всех объектов сцены
            {
                Color color;
                if (objects[i].active == true) // выбираем цвет, которым рисовать объект
                {
                    color = ColorTranslator.FromHtml("#d69d85"); // orange
                }
                else
                {
                    color = ColorTranslator.FromHtml("#4ec9b0"); // green
                }

                for (int j = 0; j < objects[i].polyCount; j++) // для всех треугольников
                {
                    Draw.Triangle(objects[i].tris[j], objects[i].placeInWorld, image, color);
                }
            }
        }

        public void ActivateNext()
        {
            if (activeObject != -1) // если есть активный объект
            {
                if (activeObject != objectsCount-1) // если не последний объект в сцене
                {
                    activeObject++;
                    objects[activeObject].active = true;
                    objects[activeObject-1].active = false;
                }
                else // если последний
                {
                    objects[activeObject].active = false;
                    activeObject = -1;
                }
            }
            else // если не было активных
            {
                objects[0].active = true;
                activeObject++;
            }
        }
    }
}