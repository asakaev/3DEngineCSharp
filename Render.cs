using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
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

        const int SRCCOPY = 0xcc0020;
        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        private static extern int BitBlt(
          IntPtr hdcDest,     // handle to destination DC (device context)
          int nXDest,         // x-coord of destination upper-left corner
          int nYDest,         // y-coord of destination upper-left corner
          int nWidth,         // width of destination rectangle
          int nHeight,        // height of destination rectangle
          IntPtr hdcSrc,      // handle to source DC
          int nXSrc,          // x-coordinate of source upper-left corner
          int nYSrc,          // y-coordinate of source upper-left corner
          System.Int32 dwRop  // raster operation code
          );

        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        [DllImport("gdi32.dll", ExactSpelling = true, PreserveSig = true, SetLastError = true)]
        static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        public void FastDraw() // работает, но фпс такой же
        {
            Graphics g = pan.CreateGraphics();
            fps.Draw(buffer);
            Graphics gfxBitmap = Graphics.FromImage(buffer);
            IntPtr hdcBitmap = gfxBitmap.GetHdc();
            IntPtr buffPointer = buffer.GetHbitmap();
            SelectObject(hdcBitmap, buffPointer);
            IntPtr dc = g.GetHdc();
            BitBlt(dc, 0, 0, buffer.Width, buffer.Height, hdcBitmap, 0, 0, SRCCOPY);
            DeleteObject(buffPointer);
            gfxBitmap.ReleaseHdc(hdcBitmap);
            g.ReleaseHdc(dc);
            g.Dispose();
            fps.SetFrameRendered();
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