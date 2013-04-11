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
        Model o1 = ObjLoader.Load("ak47.obj", 180);
        Model o2 = ObjLoader.Load("cube.obj", 20);
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
            sc.AddObject(o1);
            sc.AddObject(o2);
            Transform.MoveModel(o1, 0, 0, 0);
            Transform.RotateModel(o1, 0, 0, -20);
            Transform.MoveModel(o2, 430, 230, 0);
            Transform.RotateModel(o2, 20, 0, 0);
        }

        void timer_Tick(object sender, EventArgs e) // 170 FPS в OnPiaint vs 65 FPS в Timer (no loop)
        {
            Transform.RotateModel(o1, 0, 0.2, 0);
            Transform.RotateModel(o2, 0, 3, 0);
            Transform.MoveModel(o1, 0,mv,0);
            if ((o1.cs.placeInWorld.y > 50) || (o1.cs.placeInWorld.y < 0)) { mv *= -1; }
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