﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Scene3D
{
    class Render
    {
        private Panel pan; // «устройство вывода», монитор (в нашем случае панель)
        private Bitmap buffer;
        private int w;
        private int h;
        private int xC;
        private int yC;
        private FPS fps = new FPS(); // считает и показывает фпс

        public Render(Panel sur)
        {
            pan = sur;
            w = pan.ClientSize.Width;
            h = pan.ClientSize.Height;
            xC = pan.ClientSize.Width / 2;
            yC = pan.ClientSize.Height / 2;
            buffer = new Bitmap(w, h, PixelFormat.Format32bppPArgb);
        }

        public Bitmap GetBuffer()
        {
            // отслеживаем изменение размера формы (событие сделать как?)
            if ((w != pan.ClientSize.Width) || (h != pan.ClientSize.Height))
            {
                w = pan.ClientSize.Width;
                h = pan.ClientSize.Height;

                Bitmap temp = buffer;
                buffer = new Bitmap(w, h, PixelFormat.Format32bppPArgb);
                temp.Dispose();
            }
            return buffer;
        }

        public void BufferToPanel() // Выводит сформированный кадр на панель
        {
            Graphics g = pan.CreateGraphics();
            fps.Draw(buffer);
            g.DrawImage(buffer, 0, 0, new Rectangle(0, 0, w, h), GraphicsUnit.Pixel);
            fps.SetFrameRendered();
            g.Dispose();
        }
    }
}