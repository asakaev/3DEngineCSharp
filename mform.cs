using Scene3D;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Scene3D
{
    public partial class mform : Form
    {
        Rasterizer render; // рендер (виртуальный монитор)
        Timer timer = new Timer(); // таймер для анимации
        Scene scene = new Scene();
        double mv = 0.4; // для плавающего движения вверх-вниз

        double y; // для поворотов
        Model ufo;

        public mform()
        {
            InitializeComponent();

            Text = Application.ProductName + " v" + Application.ProductVersion; // заголовок
            render = new Rasterizer(sp.Panel1);
            timer.Interval = 15; // between 1 ms and 20 ms разброс т.к. не реалтайм
            timer.Tick += new EventHandler(timer_Tick);
            timer.Enabled = true;

            // добавляем модели и их начальные значения
            ufo = ObjLoader.Load("ufo.obj", 2);
            scene.AddObject(ufo);
            ufo.AppendMove(0, 50, 0); // в метрах

            Model rassv = ObjLoader.Load("Rassv.obj", 75/4.5);
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

            Model obraz = ObjLoader.Load("obr.obj", 55/4);
            scene.AddObject(obraz);
            obraz.AppendMove(-140, 0, 76);

            Model gogol = ObjLoader.Load("gogol.obj", 55 / 4);
            scene.AddObject(gogol);
            gogol.AppendMove(-200, 0, -155);

            Model tree = ObjLoader.Load("Tree.obj", 2);
            scene.AddObject(tree);
            tree.AppendMove(-20, 0, -40);

            scene.cam.AppendMove(0, 100, -80);
            scene.cam.AppendRotate(-60, 0, 0);
        }

        void UpdateKeys()
        {
            if (Keyboard.IsKeyDown('W')) { scene.cam.AppendMove(0, 0, 10); }
            if (Keyboard.IsKeyDown('A')) { scene.cam.AppendMove(-10, 0, 0); }
            if (Keyboard.IsKeyDown('S')) { scene.cam.AppendMove(0, 0, -10); }
            if (Keyboard.IsKeyDown('D')) { scene.cam.AppendMove(10, 0, 0); }

            if (Keyboard.IsKeyDown('1')) { scene.cam.AppendMove(0, -10, 0); }
            if (Keyboard.IsKeyDown('2')) { scene.cam.AppendMove(0, 10, 0); }

            if (Keyboard.IsKeyDown('T')) { scene.cam.AppendMove(0, 10, 0); }
            if (Keyboard.IsKeyDown('F')) { scene.cam.AppendMove(-10, 0, 0); }
            if (Keyboard.IsKeyDown('G')) { scene.cam.AppendMove(0, -10, 0); }
            if (Keyboard.IsKeyDown('H')) { scene.cam.AppendMove(10, 0, 0); }


            if (Keyboard.IsKeyDown('E')) { scene.ActivateNext(); }
            if (Keyboard.IsKeyDown('Q')) { scene.ActivatePrev(); }

            if (Keyboard.IsKeyDown('X')) // увеличение объекта
            {
                if (scene.activeObject != -1) // если какой-нибудь выбран
                {
                    double x, y, z;
                    x = y = z = -0.01;
                    scene.objects[scene.activeObject].AppendScale(x,y,z);
                }
            }
            if (Keyboard.IsKeyDown('Z')) // уменьшение
            {
                if (scene.activeObject != -1) // если какой-нибудь выбран
                {
                    double x, y, z;
                    x = y = z = 0.01;
                    scene.objects[scene.activeObject].AppendScale(x, y, z);
                }
            }

            if (Keyboard.IsKeyDown('O')) // поворот
            {
                if (scene.activeObject != -1) // если какой-нибудь выбран
                {
                    scene.objects[scene.activeObject].AppendRotate(0, 3, 0);
                }
            }
            if (Keyboard.IsKeyDown('P')) // поворот
            {
                if (scene.activeObject != -1) // если какой-нибудь выбран
                {
                    scene.objects[scene.activeObject].AppendRotate(0, -3, 0);
                }
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            ufo.AppendRotate(0.3, 0.3, 0.3);
            ufo.AppendMove(0, mv, 0);
            if ((ufo.move.y > 100) || (ufo.move.y < 49)) { mv *= -1; }
            scene.AppendRotate(0, y, 0);
            scene.DrawScene(render.GetBuffer());
            render.BufferToPanel();
            UpdateKeys();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!timer.Enabled)
            {
                timer.Enabled = !(timer.Enabled);
                button1.Text = "Stop";
            }
            else
            {
                timer.Enabled = !(timer.Enabled);
                button1.Text = "Run";
            }
        }

        private void barZ_Scroll(object sender, EventArgs e)
        {
            y = (double)barZ.Value / 10;
            label3.Text = Convert.ToString(y) + "     ";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double x, y, z;
            x = y = z = 0.1;
            scene.AppendScale(x,y,z);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double x, y, z;
            x = y = z = -0.1;
            scene.AppendScale(x, y, z);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            scene.cam.AppendRotate(-1, 0, 0);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            scene.cam.AppendRotate(1, 0, 0);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            scene.cam.AppendMove(10, 0, 0);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            scene.cam.AppendMove(-10, 0, 0);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            scene.cam.AppendMove(0, 10, 0);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            scene.cam.AppendMove(0, -10, 0);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            scene.cam.AppendMove(0, 0, 10);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            scene.cam.AppendMove(0, 0, -10);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            scene.cam.AppendRotate(1, 0, 0);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            scene.cam.AppendRotate(-1, 0, 0);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            scene.cam.AppendRotate(0, 1, 0);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            scene.cam.AppendRotate(0, -1, 0);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            scene.cam.AppendRotate(0, 0, 1);
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            scene.cam.AppendRotate(0, 0, -1);
        }
    }
}