using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace OBJViewer
{
	public class Rasterizer : IDisposable
    {
		[DllImport("gdi32")]
		private extern static int SetDIBitsToDevice(HandleRef hDC, int xDest, int yDest, int dwWidth, int dwHeight, int XSrc, int YSrc, int uStartScan, int cScanLines, ref int lpvBits, ref BITMAPINFO lpbmi, uint fuColorUse);

		[StructLayout(LayoutKind.Sequential)]
		private struct BITMAPINFOHEADER
		{
			public int		bihSize;
			public int		bihWidth;
			public int		bihHeight;
			public short	bihPlanes;
			public short	bihBitCount;
			public int		bihCompression;
			public int		bihSizeImage;
			public double	bihXPelsPerMeter;
			public double	bihClrUsed;
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct BITMAPINFO
		{
			public BITMAPINFOHEADER biHeader;
			public int biColors;
		}

		private int _width;
		private int _height;
		private int[] _pArray;
		private GCHandle _gcHandle;
	    private BITMAPINFO _BI;

        public byte[] data; // тут данные хранятся
        public byte[] back;

		public int Width { get { return _width; } }
		public int Height { get { return _height; } }

		~Rasterizer()
		{
			Dispose();
		}

		public void Dispose()
		{
			if (_gcHandle.IsAllocated)
				_gcHandle.Free();
			GC.SuppressFinalize(this);
		}

		private void Realloc(int width, int height)
		{
			if (_gcHandle.IsAllocated)
				_gcHandle.Free();

			_width = width;
			_height = height;
            int size = _width * _height;

            _pArray = new int[size];

            data = new byte[size * 4]; // RGBA byte array
            back = new byte[size * 4];
            for (int i = 0; i < size*4; i++)
            {
                back[i] = 30;
            }

			_gcHandle = GCHandle.Alloc(_pArray, GCHandleType.Pinned);
			_BI = new BITMAPINFO
			{
				biHeader =
				{
					bihBitCount = 32,
					bihPlanes = 1,
					bihSize = 40,
					bihWidth = _width,
					bihHeight = -_height,
					bihSizeImage = (_width * _height) << 2
				}
			};
		}

		public void Paint(HandleRef hRef, Bitmap bitmap)
		{

			if (bitmap.Width != _width || bitmap.Height != _height)
				Realloc(bitmap.Width, bitmap.Height);

			_gcHandle = GCHandle.Alloc(_pArray, GCHandleType.Pinned);

            BitmapData bitmData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
            ImageLockMode.ReadWrite, PixelFormat.Format32bppRgb);
            // указатель на первую строку зафиксированных данных
            IntPtr bitmPointer = bitmData.Scan0;
            int startIndex = 0;

            for (int y = 0; y < bitmap.Height; ++y)
            {
                //Marshal.Copy(back, startIndex, bitmPointer, bitmap.Width * 4);
                Marshal.Copy(data, startIndex, bitmPointer, bitmap.Width * 4);
                // смещаемся в фиксированных данных
                bitmPointer = (IntPtr)((int)bitmPointer + bitmData.Stride);
                startIndex += bitmap.Width * 4;
            }

            Marshal.Copy(bitmData.Scan0, _pArray, 0, _width * _height);
            SetDIBitsToDevice(hRef, 0, 0, _width, _height, 0, 0, 0, _height, ref _pArray[0], ref _BI, 0);
            
            bitmap.UnlockBits(bitmData); // разблокируем данные Bitmap в памяти

            Array.Copy(back, 0, data, 0, _width * _height * 4); // background clear
		}
    }
}