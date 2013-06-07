using System;

namespace Scene3D
{
    class Draw
    {
        public static void Triangle(Rasterizer r, CColor c, Polygon t)
        {
            int x1, y1, x2, y2;

            int x1zero = x1 = t.tri[0].GetIntX();
            int y1zero = y1 = t.tri[0].GetIntY();
            x2 = t.tri[1].GetIntX();
            y2 = t.tri[1].GetIntY();
            drawLine(r, c, x1, y1, x2, y2);

            x1 = x2;
            y1 = y2;
            x2 = t.tri[2].GetIntX();
            y2 = t.tri[2].GetIntY();
            drawLine(r, c, x1, y1, x2, y2);

            x1 = x2;
            y1 = y2;
            x2 = x1zero;
            y2 = y1zero;
            drawLine(r, c, x1, y1, x2, y2);
        }

        public static void SetPixel(Rasterizer r, CColor c, int _x, int _y)
        {
            // если мы не за пределами И если цвет отличается
            if ((_x >= 0) && (_y >= 0) && (_x < r.Width) && (_y < r.Height))
            {
                // с шагом 4, _ — номер строки
                int idx = (_y * r.Width) * 4 + (_x * 4);
                if ((r.data[idx] != c.B)) // Только одну компоненту смотрим
                {
                    // в альфа — мусор
                    r.data[idx + 2] = c.R; // R
                    r.data[idx + 1] = c.G; // G
                    r.data[idx] = c.B; // B
                }
            }
        }

        // Bresenham's line algorithm
        public static void drawLine(Rasterizer r, CColor c, int x1, int y1, int x2, int y2)
        {
            int deltaX = Math.Abs(x2 - x1);
            int deltaY = Math.Abs(y2 - y1);
            int signX = x1 < x2 ? 1 : -1;
            int signY = y1 < y2 ? 1 : -1;

            int error = deltaX - deltaY;
            SetPixel(r, c, x2, y2);

            while (x1 != x2 || y1 != y2)
            {
                SetPixel(r, c, x1, y1);
                int error2 = error * 2;
                if (error2 > -deltaY)
                {
                    error -= deltaY;
                    x1 += signX;
                }
                if (error2 < deltaX)
                {
                    error += deltaX;
                    y1 += signY;
                }
            }
        }
   }
}