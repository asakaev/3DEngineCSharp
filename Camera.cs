namespace Scene3D
{
    class Camera
    {
        public Vertex rotation = new Vertex(0, 0, 0);
        public Vertex move = new Vertex(0, 0, 0);
        Scene s;

        public Camera(Scene _s)
        {
            s = _s;
        }

        public void AppendRotate(double x, double y, double z)
        {
            rotation.x += x;
            rotation.y += y;
            rotation.z += z;

            // проверяем на полный поворот вокруг осей
            if ((rotation.x > 359) || (rotation.x < -359)) { rotation.x = rotation.x % 360; }
            if ((rotation.y > 359) || (rotation.y < -359)) { rotation.y = rotation.y % 360; }
            if ((rotation.z > 359) || (rotation.z < -359)) { rotation.z = rotation.z % 360; }
        }

        public void AppendMove(double x, double y, double z)
        {
            move.x += x;
            move.y += y;
            move.z += z;
        }

        public void MoveRotate()
        {
            for (int i = 0; i < s.objectsCount; i++)
            {
                Model m = s.objects[i];
                for (int j = 0; j < m.vtxCount; j++)
                {
                    // Rotation
                    double xR = rotation.x;
                    double yR = rotation.y;
                    double zR = rotation.z;
                    Transform.RotateVertex(m.points[j], xR, yR, zR);

                    // Move
                    m.points[j].x -= move.x;
                    m.points[j].y -= move.y;
                    m.points[j].z -= move.z;
                }
            }
        }
    }
}