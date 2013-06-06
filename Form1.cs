using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Threading;

namespace Scene3D
{
	public partial class Form1 : Form
	{
		private System.Timers.Timer fpstimer;
		private Thread renderthread;
		private int fps;
        Scene scene = new Scene();
        double mv = 0.4; // для плавающего движения вверх-вниз
        //double y; // для поворотов
        Model ufo;
        Model cube;

		public Form1()
		{
			InitializeComponent();
            LoadModels();
            //scene.cam.AppendMove(0, 100, -80);
            //scene.cam.AppendRotate(-60, 0, 0);
            scene.cam.AppendMove(0, 0, -100);
		}

		public void Update(MethodInvoker callback)
		{
			if (IsDisposed || Disposing)
				return;

			try
			{
				if (this.InvokeRequired)
					this.Invoke(callback);
				else
					callback();
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		private void Form1_Shown(object sender, EventArgs e)
		{
			fpstimer = new System.Timers.Timer(1000);
			fpstimer.Elapsed += (sender1, args) =>
			{
				Update(delegate
				{
                    Text = Application.ProductName;
                    Text += " v" + Application.ProductVersion;
                    Text += ", Poly: " + Convert.ToString(scene.polyCount);
                    Text += ", Points: " + Convert.ToString(scene.vtxCount);
                    Text += ", Objects: " + Convert.ToString(scene.objectsCount);
                    Text += ", FPS: " + fps;
                    Text += scene.GetActiveName();
                    Text += ", DIS: " + scene.cam.GetDistance();
                    fps = 0;
				});
			};
			fpstimer.Start();

			renderthread = new Thread(() =>
			{
				while (true)
					Render();
			});
			renderthread.Start();
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			renderthread.Abort();
			fpstimer.Stop();
		}

		private void Render()
		{
			lock (razorPainterWFCtl1.RazorLock)
			{
                //ufo.AppendRotate(0.3, 0.3, 0.3);
                //ufo.AppendMove(0, mv, 0);
                //if ((ufo.move.y > 100) || (ufo.move.y < 49)) { mv *= -1; }
                //scene.AppendRotate(0, 0.01, 0);
                //if (true) { Close(); }

                //if (scene.cam.IsIntersectWith(cube)) { Close(); }

                UpdateKeys();
                scene.DrawScene(razorPainterWFCtl1.RP);
				razorPainterWFCtl1.RazorPaint();
			}
			fps++;
		}

        void LoadModels() // добавляем модели и их начальные значения
        {
            //cube = ObjLoader.Load("cube.obj", 300);
            //scene.AddObject(cube);
            //cube.AppendMove(0, 50, 0); // в метрах

            //ufo = ObjLoader.Load("ufo.obj", 2);
            //scene.AddObject(ufo);
            //ufo.AppendMove(0, 50, 0); // в метрах

            Model rassv = ObjLoader.Load("Rassv.obj", 75 / 4.5);
            scene.AddObject(rassv);
            rassv.AppendMove(0, 0, 100);

            //Model liga = ObjLoader.Load("liga.obj", 50 / 18);
            //scene.AddObject(liga);
            //liga.AppendMove(95, 0, 100);

            //Model mir = ObjLoader.Load("Mir.obj", 43.4 / 4.4);
            //scene.AddObject(mir);
            //mir.AppendMove(150, 0, 100);

            //Model ammo = ObjLoader.Load("amm.obj", 59 / 2.5);
            //scene.AddObject(ammo);
            //ammo.AppendRotate(0, 54, 0);
            //ammo.AppendMove(178, 0, -110);

            //Model busStop1 = ObjLoader.Load("ost.obj", 15);
            //scene.AddObject(busStop1);
            //busStop1.AppendMove(50, 0, -30);

            //Model busStop2 = ObjLoader.Load("ost.obj", 15);
            //scene.AddObject(busStop2);
            //busStop2.AppendMove(110, 0, 80);
            //busStop2.AppendRotate(0, 180, 0);

            //Model obraz = ObjLoader.Load("obr.obj", 55 / 4);
            //scene.AddObject(obraz);
            //obraz.AppendMove(-140, 0, 76);

            //Model gogol = ObjLoader.Load("gogol.obj", 55 / 4);
            //scene.AddObject(gogol);
            //gogol.AppendMove(-200, 0, -155);

            //Model tree = ObjLoader.Load("Tree.obj", 2);
            //scene.AddObject(tree);
            //tree.AppendMove(-20, 0, -40);
        }

        void UpdateKeys()
        {
            if (Kb.IsKeyDown('W')) { scene.cam.AppendMove(0, 0, 5); }
            if (Kb.IsKeyDown('A')) { scene.cam.AppendMove(-5, 0, 0); }
            if (Kb.IsKeyDown('S')) { scene.cam.AppendMove(0, 0, -5); }
            if (Kb.IsKeyDown('D')) { scene.cam.AppendMove(5, 0, 0); }

            if (Kb.IsKeyDown('C')) { scene.cam.AppendRotate(1, 0, 0); }
            if (Kb.IsKeyDown('V')) { scene.cam.AppendRotate(-1, 0, 0); }

            if (Kb.IsKeyDown('B')) { scene.cam.AppendRotate(0, 1, 0); }
            if (Kb.IsKeyDown('N')) { scene.cam.AppendRotate(0, -1, 0); }

            if (Kb.IsKeyDown('U')) { scene.AppendRotate(0, 1, 0); }
            if (Kb.IsKeyDown('I')) { scene.AppendRotate(0, -1, 0); }

            if (Kb.IsKeyDown('1')) { scene.cam.AppendMove(0, -10, 0); }
            if (Kb.IsKeyDown('2')) { scene.cam.AppendMove(0, 10, 0); }

            if (Kb.IsKeyDown('T'))
            {
                if (scene.activeObject != -1) // если какой-нибудь выбран
                {
                    scene.objects[scene.activeObject].AppendMove(0, 0, 10);
                }
            }

            if (Kb.IsKeyDown('F'))
            {
                if (scene.activeObject != -1)
                {
                    scene.objects[scene.activeObject].AppendMove(-10, 0, 0);
                }
            }

            if (Kb.IsKeyDown('G'))
            {
                if (scene.activeObject != -1)
                {
                    scene.objects[scene.activeObject].AppendMove(0, 0, -10);
                }
            }

            if (Kb.IsKeyDown('H'))
            {
                if (scene.activeObject != -1)
                {
                    scene.objects[scene.activeObject].AppendMove(10, 0, 0);
                }
            }

            if (Kb.IsKeyDown('R'))
            {
                if (scene.activeObject != -1)
                {
                    scene.objects[scene.activeObject].AppendMove(0, -10, 0);
                }
            }

            if (Kb.IsKeyDown('Y'))
            {
                if (scene.activeObject != -1)
                {
                    scene.objects[scene.activeObject].AppendMove(0, 10, 0);
                }
            }

            if (Kb.IsKeyDown('E'))
            {
                scene.ActivateNext();
            }
            if (Kb.IsKeyDown('Q'))
            {
                scene.ActivatePrev();
            }

            if (Kb.IsKeyDown('X')) // увеличение объекта
            {
                if (scene.activeObject != -1) // если какой-нибудь выбран
                {
                    double x, y, z;
                    x = y = z = -0.01;
                    scene.objects[scene.activeObject].AppendScale(x, y, z);
                }
            }
            if (Kb.IsKeyDown('Z')) // уменьшение
            {
                if (scene.activeObject != -1) // если какой-нибудь выбран
                {
                    double x, y, z;
                    x = y = z = 0.01;
                    scene.objects[scene.activeObject].AppendScale(x, y, z);
                }
            }

            if (Kb.IsKeyDown('O')) // поворот
            {
                if (scene.activeObject != -1) // если какой-нибудь выбран
                {
                    scene.objects[scene.activeObject].AppendRotate(0, 3, 0);
                }
            }
            if (Kb.IsKeyDown('P')) // поворот
            {
                if (scene.activeObject != -1) // если какой-нибудь выбран
                {
                    scene.objects[scene.activeObject].AppendRotate(0, -3, 0);
                }
            }
        }

	}
}
