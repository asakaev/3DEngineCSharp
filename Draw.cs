using System;
using System.Drawing;
using System.Text;

namespace Scene3D
{
    class Draw
    {
        static public Random rnd = new Random();

        public static void Triangle(Polygon t, Vertex placeInWorld, Bitmap image, Color color)
        {
            Graphics g = Graphics.FromImage(image);

            if (false) // делать искусство
            {
                color = Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));
            }

            Pen myPen = new Pen(color);
            myPen.Color = color;
            int x1, y1, x2, y2;

            // перенос учитывая центр дисплея
            x1 = t.tri[0].GetIntX();
            y1 = t.tri[0].GetIntY();
            x2 = t.tri[1].GetIntX();
            y2 = t.tri[1].GetIntY();
            g.DrawLine(myPen, x1, y1, x2, y2);

            x1 = t.tri[1].GetIntX();
            y1 = t.tri[1].GetIntY();
            x2 = t.tri[2].GetIntX();
            y2 = t.tri[2].GetIntY();
            g.DrawLine(myPen, x1, y1, x2, y2);

            x1 = t.tri[2].GetIntX();
            y1 = t.tri[2].GetIntY();
            x2 = t.tri[0].GetIntX();
            y2 = t.tri[0].GetIntY();
            g.DrawLine(myPen, x1, y1, x2, y2);

            myPen.Dispose();
            g.Dispose();
        }

        public static void Background(Bitmap image)
        {
            int w = image.Width;
            int h = image.Height;
            Graphics g = Graphics.FromImage(image);
            Color darkGrey = Color.FromArgb(30, 30, 30);
            Color grey = Color.FromArgb(51, 51, 55);
            Pen myPen = new Pen(grey);
            g.FillRectangle(new SolidBrush(darkGrey), 0, 0, w, h); // фон

            // в углу размеры формы выводим
            Font drawFont = new Font("Arial", 7);
            String drawString = Convert.ToString(image.Size);
            Color red = ColorTranslator.FromHtml("#d69d85");
            SolidBrush drawBrush = new SolidBrush(red);
            PointF drawPoint = new PointF(0, 0);
            g.DrawString(drawString, drawFont, drawBrush, drawPoint);

            // Выполняет определяемые приложением задачи,
            // связанные с высвобождением или сбросом неуправляемых ресурсов.
            myPen.Dispose();
            g.Dispose();
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
    }
}