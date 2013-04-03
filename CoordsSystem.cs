namespace CubeApp
{
    class CoordsSystem
    {
        public Point3D placeInWorld; // положение в мировой системе
        public Point3D rotation; // вращение вокруг своей оси

        public CoordsSystem()
        {
            placeInWorld = new Point3D(0, 0, 0);
            rotation = new Point3D(0, 0, 0);
        }
    }
}