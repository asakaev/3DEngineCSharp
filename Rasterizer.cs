using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Scene3D
{
    class Rasterizer
    {
        private Panel p; // «устройство вывода», монитор (в нашем случае панель)
        private Bitmap buffer;
        public int w;
        public int h;
        private FPS fps = new FPS(); // считает и показывает фпс
        public byte[] data;
        Graphics g;

        public Rasterizer(Panel _p)
        {
            p = _p;
            w = p.ClientSize.Width;
            h = p.ClientSize.Height;
            data = new byte[w * h * 3]; // RGB byte array
            buffer = new Bitmap(w, h, PixelFormat.Format24bppRgb);
            g = p.CreateGraphics();
        }

        public void BufferToPanel() // Выводит сформированный кадр на панель
        {
            ByteArrayToBitmap();
            g.DrawImage(buffer, 0, 0, new Rectangle(0, 0, w, h), GraphicsUnit.Pixel);
            fps.SetFrameRendered();
            CheckBuffer();
        }

        public void ByteArrayToBitmap()
        {
            // фиксируем в памяти данных Bitmap
            BitmapData bitmData = buffer.LockBits(new Rectangle(0, 0, w, h),
            ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            // указатель на первую строку зафиксированных данных
            IntPtr bitmPointer = bitmData.Scan0;
            int startIndex = 0;
            for (int y = 0; y < h; ++y)
            {
                Marshal.Copy(data, startIndex, bitmPointer, w * 3);
                // смещаемся в фиксированных данных
                bitmPointer = (IntPtr)((int)bitmPointer + bitmData.Stride);
                startIndex += w * 3;
            }
            buffer.UnlockBits(bitmData); // разблокируем данные Bitmap в памяти
        }

        public void CheckBuffer()
        {
            // отслеживаем изменение размера формы
            if ((w != p.ClientSize.Width) || (h != p.ClientSize.Height))
            {
                w = p.ClientSize.Width;
                h = p.ClientSize.Height;
                data = new byte[w * h * 3];
                Bitmap temp = buffer;
                buffer = new Bitmap(w, h, PixelFormat.Format32bppPArgb);
                temp.Dispose();
                Graphics tmp = g;
                tmp.Dispose();
                g = p.CreateGraphics();
            }
        }

        public string GetFPS() { return Convert.ToString(fps._fps); }
    }
}