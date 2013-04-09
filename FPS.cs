using System;
using System.Diagnostics;
using System.Drawing;

namespace CubeApp
{
    class FPS // считает количество отрисованных кадров в сек
    {
        private long t0;
        private long t1;
        private int fps;
        private int frames;
        Stopwatch stopWatch = new Stopwatch();

        public FPS()
        {
            frames = 0;
            stopWatch.Start();
        }

        public void SetFrameRendered()
        {
            frames++;
            t1 = stopWatch.ElapsedMilliseconds;
            if (t1 - t0 >= 1000)
            {
                fps = frames;
                t0 = t1;
                frames = 0;
            }
        }

        public void Draw(Bitmap image)
        {
            Graphics g = Graphics.FromImage(image);
            Font drawFont = new Font("Arial", 7);
            String drawString = "FPS: " + Convert.ToString(fps);
            Color red = ColorTranslator.FromHtml("#d69d85");
            SolidBrush drawBrush = new SolidBrush(red);
            PointF drawPoint = new PointF(0, 11);
            g.DrawString(drawString, drawFont, drawBrush, drawPoint);
            g.Dispose();
        }
    }
}