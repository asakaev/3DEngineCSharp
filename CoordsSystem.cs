namespace CubeApp
{
    // Система координат пока нужна для хранения поворота объекта (не используется нигде)
    // + положение в мире
    class CoordsSystem
    {
        public Vertex placeInWorld; // положение в мировой системе
        public Vertex rotation; // вращение вокруг своей оси
        double axesSize;
        Vertex[] axes = new Vertex[3];

        public CoordsSystem()
        {
            placeInWorld = new Vertex(0, 0, 0);
            rotation = new Vertex(0, 0, 0);
            axesSize = 50;

            // для рисования осей
            axes[0] = new Vertex(axesSize,0,0);
            axes[1] = new Vertex(0, axesSize, 0);
            axes[2] = new Vertex(0, 0, axesSize);
        }
    }
}