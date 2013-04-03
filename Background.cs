using System;
using System.Drawing;

namespace CubeApp
{
    class Background
    {
        public void DrawAxes(Bitmap image) // Рисование осей
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