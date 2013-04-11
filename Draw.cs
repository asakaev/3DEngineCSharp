using System;
using System.Drawing;
using System.Text;

namespace CubeApp
{
    class Draw
    {
        static public Random rnd = new Random();

        private static void Triangle(Polygon t, Vertex placeInWorld, Bitmap image)
        {
            Graphics g = Graphics.FromImage(image);
            Color green;

            if (false) // рисовать нормальным цветом
            {
                green = ColorTranslator.FromHtml("#4ec9b0"); // green
            }
            else // искусство
            { 
                green = Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));
            }
            Pen myPen = new Pen(green);

            // Центр дисплея
            int xC = image.Width / 2;
            int yC = image.Height / 2;

            myPen.Color = green;
            int x1, y1, x2, y2;

            // перенос учитывая мировую систему и центр дисплея
            x1 = (int)Math.Round(xC + t.tri[0].x + placeInWorld.x);
            y1 = (int)Math.Round(yC - t.tri[0].y - placeInWorld.y);
            x2 = (int)Math.Round(xC + t.tri[1].x + placeInWorld.x);
            y2 = (int)Math.Round(yC - t.tri[1].y - placeInWorld.y);
            g.DrawLine(myPen, x1, y1, x2, y2);

            x1 = (int)Math.Round(xC + t.tri[1].x + placeInWorld.x);
            y1 = (int)Math.Round(yC - t.tri[1].y - placeInWorld.y);
            x2 = (int)Math.Round(xC + t.tri[2].x + placeInWorld.x);
            y2 = (int)Math.Round(yC - t.tri[2].y - placeInWorld.y);
            g.DrawLine(myPen, x1, y1, x2, y2);

            x1 = (int)Math.Round(xC + t.tri[2].x + placeInWorld.x);
            y1 = (int)Math.Round(yC - t.tri[2].y - placeInWorld.y);
            x2 = (int)Math.Round(xC + t.tri[0].x + placeInWorld.x);
            y2 = (int)Math.Round(yC - t.tri[0].y - placeInWorld.y);
            g.DrawLine(myPen, x1, y1, x2, y2);

            myPen.Dispose();
            g.Dispose();
        }

        private static void Object3D(Model obj, Bitmap image)
        {
            for (int i = 0; i < obj.polyCount; i++)
            {
                Draw.Triangle(obj.tris[i], obj.placeInWorld, image);
            }
        }

        public static void Scene(Scene sc, Bitmap image)
        {
            Background(image); // отрисовка фона
            PolyPointsObjectsCount(sc, image); // инфа о сцене в углу

            for (int i = 0; i < sc.objectsCount; i++)
            {
                Draw.Object3D(sc.objects[i], image);
            }
        }

        private static void Background(Bitmap image)
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

        private static void PolyPointsObjectsCount(Scene sc, Bitmap image)
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