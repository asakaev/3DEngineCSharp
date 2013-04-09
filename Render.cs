using System;
using System.Drawing;
using System.Windows.Forms;

namespace CubeApp
{
    class Render
    {
        private Panel pan; // «устройство вывода», монитор (в нашем случае панель)
        private Bitmap buffer;
        private int w;
        private int h;
        private int xC;
        private int yC;

        public Render(Panel sur)
        {
            pan = sur;
            w = pan.ClientSize.Width;
            h = pan.ClientSize.Height;
            xC = pan.ClientSize.Width / 2;
            yC = pan.ClientSize.Height / 2;
            buffer = new Bitmap(w, h);
        }

        public Bitmap GetBuffer() { return buffer; }

        public void BufferToPanel() // Выводит сформированный кадр на панель
        {
            Graphics g = pan.CreateGraphics();
            g.DrawImage(buffer, 0, 0, new Rectangle(0, 0, w, h), GraphicsUnit.Pixel);
            g.Dispose();
        }

        public Bitmap GetNewBuffSize()
        {
            // отслеживаем изменение размера формы (событие сделать как?)
            if ((w != pan.ClientSize.Width) || (h != pan.ClientSize.Height))
            {
                w = pan.ClientSize.Width;
                h = pan.ClientSize.Height;

                Bitmap temp = buffer;
                buffer = new Bitmap(w, h);
                temp.Dispose();
            }
            return buffer;
        }

        public Point GetCenter()
        {
            return new Point(w, h);
        }
    }
}