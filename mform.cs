using CubeApp;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CubeApp
{
    public partial class mform : Form
    {
        Render render; // рендер (виртуальный монитор)
        Timer timer = new Timer(); // таймер для анимации
        Model o1 = ObjLoader.Load("cube.obj", 10);
        Model o2 = ObjLoader.Load("ammofos.obj", 3);
        Model o3 = ObjLoader.Load("coordsXYZ.obj", 200);
        Scene sc = new Scene();

        // для поворотов
        double z;
        double x;

        public mform()
        {
            InitializeComponent();
            Text = Application.ProductName + " v" + Application.ProductVersion; // заголовок
            render = new Render(sp.Panel1);
            timer.Interval = 15; // between 1 ms and 20 ms разброс т.к. не реалтайм
            timer.Tick += new EventHandler(timer_Tick);
            timer.Enabled = true;

            // добавляем модели и их начальные значения
            sc.AddObject(o1);
            sc.AddObject(o2);
            sc.AddObject(o3);

            Transform.MoveModel(o1, 250, 0, 0);
            Transform.RotateModel(o1, 15, 0, 0);
            Transform.MoveModel(o2, 0, 100, 0);
            Transform.RotateModel(o2, -20, 0, 0);
            //Transform.MoveScene(sc, 0, -200, 0);
        }

        void timer_Tick(object sender, EventArgs e) // 170 FPS в OnPaint vs 65 FPS в Timer (no loop)
        {
            Transform.RotateModel(o1, 0, 10, 0);
            Transform.RotateModel(o2, 0, -2, 0);
            //Transform.MoveModel(o1, 0,mv,0);

            Transform.RotateScene(sc, x, z, 0);
            label3.Text = Convert.ToString(z) + "     ";
            label4.Text = Convert.ToString(x) + "     ";

            Draw.Scene(sc, render.GetBuffer());  
            render.BufferToPanel();
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
            z = (double)barZ.Value / 4;
        }

        private void barX_Scroll(object sender, EventArgs e)
        {
            x = (double)barX.Value / 4;
        }

        private void up_Click(object sender, EventArgs e)
        {
            Transform.MoveScene(sc, 0, 10, 0);
        }

        private void down_Click(object sender, EventArgs e)
        {
            Transform.MoveScene(sc, 0, -10, 0);
        }

        private void left_Click(object sender, EventArgs e)
        {
            Transform.MoveScene(sc, -10, 0, 0);
        }

        private void right_Click(object sender, EventArgs e)
        {
            Transform.MoveScene(sc, 10, 0, 0);
        }
    }
}