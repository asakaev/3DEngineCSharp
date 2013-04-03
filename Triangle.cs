namespace CubeApp
{
    class Triangle
    {
        public Point3D[] tri = new Point3D[3];
        public Point3D[] triOrg = new Point3D[3];
        public int id;

        public Triangle(double x1, double y1, double z1, 
            double x2, double y2, double z2,
            double x3, double y3, double z3,
            int _id)
        {
            // точки будут модицифироваться
            tri[0] = new Point3D(x1, y1, z1);
            tri[1] = new Point3D(x2, y2, z2);
            tri[2] = new Point3D(x3, y3, z3);

            // точки будут хранить оригинальное значение
            triOrg[0] = new Point3D(x1, y1, z1);
            triOrg[1] = new Point3D(x2, y2, z2);
            triOrg[2] = new Point3D(x3, y3, z3);

            id = _id;
        }
    }
}