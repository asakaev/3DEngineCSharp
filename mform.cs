using Scene3D;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Scene3D
{
    public partial class mform : Form
    {
        Render render; // рендер (виртуальный монитор)
        Timer timer = new Timer(); // таймер для анимации
        Scene scene = new Scene();
        double mv = 0.4; // для плавающего движения вверх-вниз

        // для поворотов
        double y;
        double x;
        Model ufo;

        public mform()
        {
            InitializeComponent();
            Text = Application.ProductName + " v" + Application.ProductVersion; // заголовок
            render = new Render(sp.Panel1);
            timer.Interval = 15; // between 1 ms and 20 ms разброс т.к. не реалтайм
            timer.Tick += new EventHandler(timer_Tick);
            timer.Enabled = true;

            // добавляем модели и их начальные значения
            ufo = ObjLoader.Load("ufo.obj", 2);
            scene.AddObject(ufo);
            Transform.MoveModel(ufo, 0, 50, 0);

            Model rassv = ObjLoader.Load("Rassv.obj", 75/4.5);
            scene.AddObject(rassv);
            Transform.MoveModel(rassv, 0, 0, 100); // в метрах

            Model liga = ObjLoader.Load("liga.obj", 50 / 18);
            scene.AddObject(liga);
            Transform.MoveModel(liga, 95, 0, 100);

            Model mir = ObjLoader.Load("Mir.obj", 43.4 / 4.4);
            scene.AddObject(mir);
            Transform.MoveModel(mir, 150, 0, 100);

            Model ammo = ObjLoader.Load("amm.obj", 59 / 2.5);
            scene.AddObject(ammo);
            Transform.MoveModel(ammo, 178, 0, -110);
            Transform.RotateModel(ammo, 0, 54, 0);

            Model busStop1 = ObjLoader.Load("ost.obj", 15);
            scene.AddObject(busStop1);
            Transform.MoveModel(busStop1, 50, 0, -30);

            Model busStop2 = ObjLoader.Load("ost.obj", 15);
            scene.AddObject(busStop2);
            Transform.MoveModel(busStop2, 110, 0, 80);
            Transform.RotateModel(busStop2, 0, 180, 0);

            Model obraz = ObjLoader.Load("obr.obj", 55/4);
            scene.AddObject(obraz);
            Transform.MoveModel(obraz, -140, 0, 76);

            Model gogol = ObjLoader.Load("gogol.obj", 55 / 4);
            scene.AddObject(gogol);
            Transform.MoveModel(gogol, -200, 0, -155);

            Model tree = ObjLoader.Load("Tree.obj", 2);
            scene.AddObject(tree);
            Transform.MoveModel(tree, -20, 0, -40);

            Transform.RotateScene(scene, -90, 0, 0); // - по Х двигает ВНИЗ!
            Transform.SceneScale(scene, 0.5);
        }

        void timer_Tick(object sender, EventArgs e) // 170 FPS в OnPaint vs 65 FPS в Timer (no loop)
        {
            Transform.RotateModel(ufo, 0.1, 0.1, 0.1);
            Transform.MoveModel(ufo, 0, mv, 0);
            if ((ufo.placeInWorld.y > 50) || (ufo.placeInWorld.y < 0)) { mv *= -1; }

            Transform.RotateScene(scene, x, y, 0);
            scene.DrawScene(render.GetBuffer());  
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
            y = (double)barZ.Value / 4;
            label3.Text = Convert.ToString(y) + "     ";
        }

        private void barX_Scroll(object sender, EventArgs e)
        {
            x = (double)barX.Value / 4;
            label4.Text = Convert.ToString(x) + "     ";
        }

        private void up_Click(object sender, EventArgs e)
        {
            Transform.MoveScene(scene, 0, 10, 0);
        }

        private void down_Click(object sender, EventArgs e)
        {
            Transform.MoveScene(scene, 0, -10, 0);
        }

        private void left_Click(object sender, EventArgs e)
        {
            Transform.MoveScene(scene, -10, 0, 0);
        }

        private void right_Click(object sender, EventArgs e)
        {
            Transform.MoveScene(scene, 10, 0, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Transform.SceneScale(scene, 1.1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Transform.SceneScale(scene, 0.9);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            scene.ActivateNext();
        }
    }
}