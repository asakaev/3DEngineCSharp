using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;

namespace Scene3D
{
    class Draw
    {
        public static void Triangle(Polygon t, Vertex placeInWorld, Rasterizer r, Color color)
        {
            //Graphics g = Graphics.FromImage(image);

            Pen myPen = new Pen(color);
            myPen.Color = color;
            int x1, y1, x2, y2;

            // перенос учитывая центр дисплея
            x1 = t.tri[0].GetIntX();
            y1 = t.tri[0].GetIntY();
            x2 = t.tri[1].GetIntX();
            y2 = t.tri[1].GetIntY();
            //g.DrawLine(myPen, x1, y1, x2, y2);
            drawLine(r, x1, y1, x2, y2);
            //drawLine(image, -5545, -5545, -8550, -8055);

            x1 = t.tri[1].GetIntX();
            y1 = t.tri[1].GetIntY();
            x2 = t.tri[2].GetIntX();
            y2 = t.tri[2].GetIntY();
            //g.DrawLine(myPen, x1, y1, x2, y2);
            drawLine(r, x1, y1, x2, y2);

            x1 = t.tri[2].GetIntX();
            y1 = t.tri[2].GetIntY();
            x2 = t.tri[0].GetIntX();
            y2 = t.tri[0].GetIntY();
            drawLine(r, x1, y1, x2, y2);
            //g.DrawLine(myPen, x1, y1, x2, y2);

            myPen.Dispose();
            //g.Dispose();
        }

        public static void Background(Rasterizer r)
        {
            int size = r.w * r.h * 3;
            for (int i = 0; i < size; i += 3)
            {
                r.data[i + 2] = 30; // R
                r.data[i + 1] = 30; // G
                r.data[i] = 30; // B
            }
        }

        public static void PolyPointsObjectsCount(Scene sc, Bitmap image)
        {
            Graphics g = Graphics.FromImage(image);
            Font drawFont = new Font("Arial", 7);
            String drawString = "Polygons: " + Convert.ToString(sc.polyCount);
            drawString += "\nPoints: " + Convert.ToString(sc.vtxCount);
            drawString += "\nObjects: " + Convert.ToString(sc.objectsCount);
            Color red = ColorTranslator.FromHtml("#d69d85");
            SolidBrush drawBrush = new SolidBrush(red);
            PointF drawPoint = new PointF(0, 21);
            g.DrawString(drawString, drawFont, drawBrush, drawPoint);
            g.Dispose();
        }

        public static void SetPixel(Rasterizer r, Color c, int _x, int _y)
        {
            if ( (_x >= 0) && (_y >= 0) && (_x < r.w) && (_y < r.h) )
            {
                int idx = (_y * r.w) * 3 + (_x * 3);
                r.data[idx + 2] = c.R; // R
                r.data[idx + 1] = c.G; // G
                r.data[idx] = c.B; // B
            }
        }

        // Bresenham's line algorithm
        public static void drawLine(Rasterizer r, int x1, int y1, int x2, int y2)
        {
            Color green = ColorTranslator.FromHtml("#4ec9b0"); // green
            Pen myPen = new Pen(green);

            int deltaX = Math.Abs(x2 - x1);
            int deltaY = Math.Abs(y2 - y1);
            int signX = x1 < x2 ? 1 : -1;
            int signY = y1 < y2 ? 1 : -1;
            //
            int error = deltaX - deltaY;
            //
            //setPixel(x2, y2);

            //image.SetPixel(-5, -5, green);
            SetPixel(r, green, x2, y2);

            while (x1 != x2 || y1 != y2)
            {
                //setPixel(x1, y1);
                SetPixel(r, green, x1, y1);

                int error2 = error * 2;
                //
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

