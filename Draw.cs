using System;
using System.Drawing;
using System.Text;

namespace CubeApp
{
    class Draw
    {
        private static void Triangle(Polygon t, CoordsSystem cs, Bitmap image)
        {
            Graphics g = Graphics.FromImage(image);
            Color green = ColorTranslator.FromHtml("#4ec9b0"); // green
            Pen myPen = new Pen(green);

            // Центр дисплея
            int xC = image.Width / 2;
            int yC = image.Height / 2;

            myPen.Color = green;
            int x1, y1, x2, y2;

            // перенос учитывая мировую систему и центр дисплея
            x1 = (int)Math.Round(xC + t.tri[0].x + cs.placeInWorld.x);
            y1 = (int)Math.Round(yC - t.tri[0].y - cs.placeInWorld.y);
            x2 = (int)Math.Round(xC + t.tri[1].x + cs.placeInWorld.x);
            y2 = (int)Math.Round(yC - t.tri[1].y - cs.placeInWorld.y);
            g.DrawLine(myPen, x1, y1, x2, y2);

            x1 = (int)Math.Round(xC + t.tri[1].x + cs.placeInWorld.x);
            y1 = (int)Math.Round(yC - t.tri[1].y - cs.placeInWorld.y);
            x2 = (int)Math.Round(xC + t.tri[2].x + cs.placeInWorld.x);
            y2 = (int)Math.Round(yC - t.tri[2].y - cs.placeInWorld.y);
            g.DrawLine(myPen, x1, y1, x2, y2);

            x1 = (int)Math.Round(xC + t.tri[2].x + cs.placeInWorld.x);
            y1 = (int)Math.Round(yC - t.tri[2].y - cs.placeInWorld.y);
            x2 = (int)Math.Round(xC + t.tri[0].x + cs.placeInWorld.x);
            y2 = (int)Math.Round(yC - t.tri[0].y - cs.placeInWorld.y);
            g.DrawLine(myPen, x1, y1, x2, y2);

            myPen.Dispose();
            g.Dispose();
        }

        private static void Object3D(Object3D obj, Bitmap image)
        {
            for (int i = 0; i < obj.trisCount; i++)
            {
                Draw.Triangle(obj.tris[i], obj.cs, image);
            }

            // информация об угле поворота и положение в мировой системе
            if (obj is Hexahedron)
            {
                Graphics g = Graphics.FromImage(image);
                int x = image.Width / 2 + obj.cs.placeInWorld.GetIntX();
                int y = image.Height / 2 - obj.cs.placeInWorld.GetIntY();
                Font drawFont = new Font("Arial", 7);
                Color red = ColorTranslator.FromHtml("#d69d85");
                SolidBrush drawBrush = new SolidBrush(red);
                PointF drawP = new PointF(x, y); // cent + xc
                String drawS = "RotationX: " + obj.cs.rotation.GetIntX()
                    + "\nWorldX: " + obj.cs.placeInWorld.GetIntX()
                    + "\nWorldY: " + obj.cs.placeInWorld.GetIntY();
                g.DrawString(drawS, drawFont, drawBrush, drawP);
                g.Dispose();
            }

            // Рисование букв у прямоугольника
            if (true) // obj is CRectangle
            {
                Graphics g = Graphics.FromImage(image);

                // Центр дисплея
                int xC = image.Width / 2;
                int yC = image.Height / 2;
                char letter = 'a';

                Font drawFont = new Font("Arial", 7);
                Color red = ColorTranslator.FromHtml("#d69d85");
                SolidBrush drawBrush = new SolidBrush(red);
                PointF drawP = new PointF(0, 0); // cent + xc
                String drawS = "";

                for (int i = 0; i < obj.pointsCount; i++)
                {
                    drawS = "";
                    drawP.X = xC + obj.cs.placeInWorld.GetIntX() + obj.pointsEdited[i].GetIntX();
                    drawP.Y = yC - obj.cs.placeInWorld.GetIntY() - obj.pointsEdited[i].GetIntY();
                    drawS += letter++;
                    g.DrawString(drawS, drawFont, drawBrush, drawP);
                }
                g.Dispose();
            }
        }

        public static void Scene(Scene sc, Bitmap image)
        {
            for (int i = 0; i < sc.objectsCount; i++)
            {
                Draw.Object3D(sc.objects[i], image);
            }
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
            g.DrawLine(myPen, 0, h / 2, w, h / 2); // x
            g.DrawLine(myPen, w / 2, 0, w / 2, h); // y

            int little = (int)Math.Round((w - (Math.Sqrt(3) * h)) / 2); // 120 градусов наклон оси z
            g.DrawLine(myPen, little, h, w - little, 0); // z

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


    }
}