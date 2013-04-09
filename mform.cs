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
        Model ak47 = ObjLoader.Load("C:\\ak47.txt", 150);
        Model cube = ObjLoader.Load("C:\\cube.txt", 20);
        Scene sc = new Scene();
        double mv = 0.4; // для плавающего движения вверх-вниз

        public mform()
        {
            InitializeComponent();
            Text = Application.ProductName + " v" + Application.ProductVersion; // заголовок
            render = new Render(sp.Panel1);
            timer.Interval = 15; // between 1 ms and 20 ms разброс т.к. не реалтайм
            timer.Tick += new EventHandler(timer_Tick);
            timer.Enabled = true;

            // добавляем модели и их начальные значения
            sc.AddObject(ak47);
            sc.AddObject(cube);
            Transform.MoveModel(ak47, 0, 0, 0);
            Transform.RotateModel(ak47, 0, 0, -20);
            Transform.MoveModel(cube, 430, 230, 0);
            Transform.RotateModel(cube, 20, 0, 0);
        }

        void timer_Tick(object sender, EventArgs e) // 170 FPS в OnPiaint vs 65 FPS в Timer (no loop)
        {
            Transform.RotateModel(ak47, 0, 0.2, 0);
            Transform.RotateModel(cube, 0, 3, 0);
            Transform.MoveModel(ak47, 0,mv,0);
            if ((ak47.cs.placeInWorld.y > 50) || (ak47.cs.placeInWorld.y < 0)) { mv *= -1; }
            Draw.Scene(sc, render.GetBuffer());  
            render.BufferToPanel();
        }

        protected override void OnPaint(PaintEventArgs pea) { }
 
        private void formGraphics_Load(object sender, EventArgs e)
        {
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

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}