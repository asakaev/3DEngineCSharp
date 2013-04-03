using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeApp
{
    class Draw
    {
        public static void Triangle(Triangle t, CoordsSystem cs, Bitmap image)
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
            x1 = xC + t.tri[0].GetIntX() + (int)Math.Round(cs.placeInWorld.x);
            y1 = yC - t.tri[0].GetIntY() - (int)Math.Round(cs.placeInWorld.y);
            x2 = xC + t.tri[1].GetIntX() + (int)Math.Round(cs.placeInWorld.x);
            y2 = yC - t.tri[1].GetIntY() - (int)Math.Round(cs.placeInWorld.y);
            g.DrawLine(myPen, x1, y1, x2, y2);

            x1 = xC + t.tri[1].GetIntX() + (int)Math.Round(cs.placeInWorld.x);
            y1 = yC - t.tri[1].GetIntY() - (int)Math.Round(cs.placeInWorld.y);
            x2 = xC + t.tri[2].GetIntX() + (int)Math.Round(cs.placeInWorld.x);
            y2 = yC - t.tri[2].GetIntY() - (int)Math.Round(cs.placeInWorld.y);
            g.DrawLine(myPen, x1, y1, x2, y2);

            x1 = xC + t.tri[2].GetIntX() + (int)Math.Round(cs.placeInWorld.x);
            y1 = yC - t.tri[2].GetIntY() - (int)Math.Round(cs.placeInWorld.y);
            x2 = xC + t.tri[0].GetIntX() + (int)Math.Round(cs.placeInWorld.x);
            y2 = yC - t.tri[0].GetIntY() - (int)Math.Round(cs.placeInWorld.y);
            g.DrawLine(myPen, x1, y1, x2, y2);

            myPen.Dispose();
            g.Dispose();
        }

        public static void Object3D(Object3D obj, Bitmap image)
        {
            for (int i = 0; i < obj.trisCount; i++)
            {
                Draw.Triangle(obj.list[i], obj.cs, image);
            }

            // информация об угле поворота и положение в мировой системе
            if (obj is Cube)
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
        }
    }
}