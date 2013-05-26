// Test control fronend for WindowsForms for RazorGDIPainter library
//   (c) Mokrov Ivan
// special for habrahabr.ru
// under MIT license

using Scene3D;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FastDrawWF
{
	public partial class FastDrawCtrl : UserControl
	{
		#region Component Designer generated code
		private System.ComponentModel.IContainer components = null;
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}

			lock (this)
			{
				if (RazorGFX != null) RazorGFX.Dispose();
				if (RazorBMP != null) RazorBMP.Dispose();
				if (hDCGraphics != null) hDCGraphics.Dispose();
				RP.Dispose();
			}

			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
            this.SuspendLayout();
            // 
            // RazorPainterWFCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "RazorPainterWFCtl";
            this.Size = new System.Drawing.Size(1280, 720);
            this.ResumeLayout(false);

		}
		#endregion

		private readonly HandleRef hDCRef;
		private readonly Graphics hDCGraphics;
		public readonly Rasterizer RP;

		/// <summary>
		/// root Bitmap
		/// </summary>
		public Bitmap RazorBMP { get; private set; }

		/// <summary>
		/// Graphics object to paint on RazorBMP
		/// </summary>
		public Graphics RazorGFX { get; private set; }

		/// <summary>
		/// Lock it to avoid resize/repaint race
		/// </summary>
		public readonly object RazorLock = new object();

		public FastDrawCtrl()
		{
			InitializeComponent();

			this.MinimumSize = new Size(1, 1);

			SetStyle(ControlStyles.DoubleBuffer, false);
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.Opaque, true);

			hDCGraphics = CreateGraphics();
			hDCRef = new HandleRef(hDCGraphics, hDCGraphics.GetHdc());

			RP = new Rasterizer();
			RazorBMP = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
			RazorGFX = Graphics.FromImage(RazorBMP);

			this.Resize += (sender, args) =>
			{
				lock (RazorLock)
				{
					if (RazorGFX != null) RazorGFX.Dispose();
					if (RazorBMP != null) RazorBMP.Dispose();
					RazorBMP = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
					RazorGFX = Graphics.FromImage(RazorBMP);
				}
			};
		}

		/// <summary>
		/// After all in-memory paint on RazorGFX, call it to display it on control
		/// </summary>
		public void RazorPaint()
		{
			RP.Paint(hDCRef, RazorBMP);
		}
	}
}
