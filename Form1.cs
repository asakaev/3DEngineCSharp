using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Threading;

namespace OBJViewer
{
	public partial class Form1 : Form
	{
		private System.Timers.Timer fpstimer;
		private Thread renderthread; // поток для отрисовки
		private int fps;
        Scene scene = new Scene();
        Image w, a, s, d, rotate;
        Image wOn, aOn, sOn, dOn, rotateOn;
        bool isRotating = false;
        string modelToLoad;

		public Form1()
		{
			InitializeComponent();
            this.Start(); // loading painter control
            LoadImages();
            this.BackColor = Color.FromArgb(30, 30, 30);
            LoadModels();
            scene.cam.MoveToCamDirection(0, 0, -1000); 
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
                    Text += ", FPS: " + fps;
                    Text += scene.GetActiveName();
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
                if (isRotating)
                {
                    scene.AppendRotate(0, 0.05, 0);
                }

                UpdateKeys(); // опрос клавиш
                scene.DrawScene(razorPainterWFCtl1.RP);
				razorPainterWFCtl1.RazorPaint();
                CheckMenuItem();
			}
			fps++;
		}

        void LoadModels() // добавляем модели и их начальные значения
        {
            Model rassv = ObjLoader.Load("Rassv.obj", 75 / 4.5);
            scene.AddObject(rassv);
            rassv.AppendMove(0, 0, 100);

            Model liga = ObjLoader.Load("liga.obj", 50 / 18);
            scene.AddObject(liga);
            liga.AppendMove(95, 0, 100);

            Model mir = ObjLoader.Load("Mir.obj", 43.4 / 4.4);
            scene.AddObject(mir);
            mir.AppendMove(150, 0, 100);

            Model ammo = ObjLoader.Load("amm.obj", 59 / 2.5);
            scene.AddObject(ammo);
            ammo.AppendRotate(0, 54, 0);
            ammo.AppendMove(178, 0, -110);

            Model busStop1 = ObjLoader.Load("ost.obj", 15);
            scene.AddObject(busStop1);
            busStop1.AppendMove(50, 0, -30);

            Model busStop2 = ObjLoader.Load("ost.obj", 15);
            scene.AddObject(busStop2);
            busStop2.AppendMove(110, 0, 80);
            busStop2.AppendRotate(0, 180, 0);

            Model obraz = ObjLoader.Load("obr.obj", 55 / 4);
            scene.AddObject(obraz);
            obraz.AppendMove(-140, 0, 76);

            Model gogol = ObjLoader.Load("gogol.obj", 55 / 4);
            scene.AddObject(gogol);
            gogol.AppendMove(-200, 0, -155);

            Model tree = ObjLoader.Load("Tree.obj", 2);
            scene.AddObject(tree);
            tree.AppendMove(-20, 0, -40);
        }

        void UpdateKeys()
        {
            double mv = 2;
            double rot = 0.3;

            // Вперед-назад
            if (Kb.IsKeyDown('W')) { scene.cam.MoveToCamDirection(0,0,mv); }
            if (Kb.IsKeyDown('S')) { scene.cam.MoveToCamDirection(0, 0, -mv); }

            // Приставные шаги
            if (Kb.IsKeyDown('A')) { scene.cam.MoveToCamDirection(mv, 0, 0); }
            if (Kb.IsKeyDown('D')) { scene.cam.MoveToCamDirection(-mv, 0, 0); }

            // Повороты головы
            if (Kb.IsKeyDown('Q')) { scene.cam.AppendRotate(0, -rot, 0); }
            if (Kb.IsKeyDown('E')) { scene.cam.AppendRotate(0, rot, 0); }

            // Джет пак
            if (Kb.IsKeyDown('1')) { scene.cam.MoveToCamDirection(0, mv, 0); }
            if (Kb.IsKeyDown('2')) { scene.cam.MoveToCamDirection(0, -mv, 0); }
            
            // Наклоны головы
            if (Kb.IsKeyDown('R')) { scene.cam.AppendRotate(rot, 0, 0); }
            if (Kb.IsKeyDown('F')) { scene.cam.AppendRotate(-rot, 0, 0); }

            if (Kb.IsKeyDown('U')) { scene.AppendRotate(0, 1, 0); }
            if (Kb.IsKeyDown('I')) { scene.AppendRotate(0, -1, 0); }

            if (Kb.IsKeyDown('T'))
            {
                if (scene.activeObject != -1) // если какой-нибудь выбран
                {
                    scene.objects[scene.activeObject].AppendMove(0, 0, 10);
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

            if (Kb.IsKeyDown('Y'))
            {
                if (scene.activeObject != -1)
                {
                    scene.objects[scene.activeObject].AppendMove(0, 10, 0);
                }
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

        // Buttons actions
        #region

        // W
        private void button1_Click(object sender, EventArgs e)
        {
            scene.cam.MoveToCamDirection(0, 0, 40);
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            button1.Image = wOn;
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            button1.Image = w;
        }

        // A
        private void button2_Click(object sender, EventArgs e)
        {
            scene.cam.MoveToCamDirection(40, 0, 0);
        }

        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            button2.Image = aOn;
        }

        private void button2_MouseUp(object sender, MouseEventArgs e)
        {
            button2.Image = a;
        }

        // S
        private void button3_Click(object sender, EventArgs e)
        {
            scene.cam.MoveToCamDirection(0, 0, -40);
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            button3.Image = sOn;
        }

        private void button3_MouseUp(object sender, MouseEventArgs e)
        {
            button3.Image = s;
        }

        // D
        private void button4_Click(object sender, EventArgs e)
        {
            scene.cam.MoveToCamDirection(-40, 0, 0);
        }

        private void button4_MouseDown(object sender, MouseEventArgs e)
        {
            button4.Image = dOn;
        }

        private void button4_MouseUp(object sender, MouseEventArgs e)
        {
            button4.Image = d;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (isRotating)
            {
                isRotating = false;
                button5.Image = rotate;
            }
            else
            {
                isRotating = true;
                button5.Image = rotateOn;
            }
        }

        #endregion

        void LoadImages()
        {
            w = Image.FromFile(@"..\res\w.bmp");
            wOn = Image.FromFile(@"..\res\wOn.bmp");
            button1.Image = w;
            button1.TabStop = false;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;

            a = Image.FromFile(@"..\res\a.bmp");
            aOn = Image.FromFile(@"..\res\aOn.bmp");
            button2.Image = a;
            button2.TabStop = false;
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;

            s = Image.FromFile(@"..\res\s.bmp");
            sOn = Image.FromFile(@"..\res\sOn.bmp");
            button3.Image = s;
            button3.TabStop = false;
            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 0;

            d = Image.FromFile(@"..\res\d.bmp");
            dOn = Image.FromFile(@"..\res\dOn.bmp");
            button4.Image = d;
            button4.TabStop = false;
            button4.FlatStyle = FlatStyle.Flat;
            button4.FlatAppearance.BorderSize = 0;

            rotate = Image.FromFile(@"..\res\rotate.bmp");
            rotateOn = Image.FromFile(@"..\res\rotateOn.bmp");
            button5.Image = rotate;
            button5.TabStop = false;
            button5.FlatStyle = FlatStyle.Flat;
            button5.FlatAppearance.BorderSize = 0;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Select OBJ model";
            fdlg.InitialDirectory = @"..\";
            fdlg.Filter = "OBJ files (*.obj)|*.obj";
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                modelToLoad = fdlg.SafeFileName;
            }

            scene = new Scene();
            razorPainterWFCtl1.cam = this.scene.cam;
            scene.cam.MoveToCamDirection(0, 0, -1000);
            Model newModel = ObjLoader.Load(modelToLoad, 50);  
            scene.AddObject(newModel);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scene = new Scene();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void turnRotationOnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isRotating)
            {
                isRotating = true;
                button5.Image = rotateOn;
                turnRotationOnToolStripMenuItem.Text = "Turn Rotation Off";
            }
            else
            {
                isRotating = false;
                button5.Image = rotate;
                turnRotationOnToolStripMenuItem.Text = "Turn Rotation On";
            }
        }

        void CheckMenuItem()
        {
            if (isRotating)
            {
                turnRotationOnToolStripMenuItem.Text = "Turn Rotation Off";
            }
            else
            {
                turnRotationOnToolStripMenuItem.Text = "Turn Rotation On";
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("notepad.exe", @"..\res\help.txt");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string caption = "About " + Application.ProductName + " v" + Application.ProductVersion;
            string message = "Super Turbo Information";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;
            result = MessageBox.Show(message, caption, buttons);
        }
	}
}