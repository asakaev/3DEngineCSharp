using System;
using System.Diagnostics;
using System.Drawing;

namespace Scene3D
{
    class FPS // считает количество отрисованных кадров в сек
    {
        private long t0;
        private long t1;
        public int _fps;
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
                _fps = frames;
                t0 = t1;
                frames = 0;
            }
        }
    }
}