using CubeApp;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CubeApp
{
    public partial class mform : Form
    {

        Render render; // рендер (виртуальный монитор)
        Bitmap buff; // указатель на буфер изображения
        Timer timer = new Timer(); // таймер для анимации
        FPS fps; // считает и показывает фпс

        Model m = ObjLoader.Load("C:\\teapot.txt", 100);
        Scene sc = new Scene();
        double mv = 0.4;

        public mform()
        {
            InitializeComponent();
            Text = Application.ProductName + " v" + Application.ProductVersion; // заголовок
            fps = new FPS(); // будет считать и показывать FPS

            render = new Render(sp.Panel1);
            buff = render.GetBuffer();

            timer.Interval = 15; // between 1 ms and 20 ms разброс т.к. не реалтайм
            timer.Tick += new EventHandler(timer_Tick);
            timer.Enabled = true;

            sc.AddObject(m);

            // начальные значения
            Transform.MoveModel(m, 0, -150, 0);
            //Transform.RotateModel(m, 30, 30, 30);
            //CheckAspectRatio();
        }

        void CheckAspectRatio()
        {
            //if (view.Width / view.Height > 16 / 9) // если квадрат
            //{
            //    this.sp.Panel1.Height = 500;
            //    sp.Panel1.Width = 500;
            //    this.sp.SplitterDistance = this.sp.Width - this.sp.SplitterWidth;
            //}
        }

        
        void timer_Tick(object sender, EventArgs e) // 170 FPS в OnPiaint vs 65 FPS в Timer (no loop)
        {
            Draw.Background(buff);
            Transform.RotateModel(m, 0.1, 0.1, 0.1);
            Transform.MoveModel(m, 0,mv,0);
            if ((m.cs.placeInWorld.y > 50) || (m.cs.placeInWorld.y < 0)) { mv = mv * -1; }
            Draw.Scene(sc, buff);

            fps.Draw(buff);
            render.BufferToPanel();
            fps.SetFrameRendered();
            buff = render.GetNewBuffSize();
        }

        //protected override void OnPaint(PaintEventArgs pea) { }
 
        private void formGraphics_Load(object sender, EventArgs e)
        {
            // ?
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
            // ?
        }
    }
}