using CubeApp;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CubeApp
{
    public partial class mform : Form
    {

        Render vmon; // рендер (виртуальный монитор)
        Bitmap buff; // указатель на буфер изображения
        Timer timer = new Timer(); // таймер для анимации
        Panel view; // панелька на которой выводим изображения
        Background background; // оси координат и фон
        FPS fps; // считает и показывает фпс
        //Cube fig = new Cube(300);
        Cube cc = new Cube(250);


        Floor f = new Floor(200, 300);

        double mv = 0.4;



        public mform()
        {
            InitializeComponent();
            Text = Application.ProductName + " v" + Application.ProductVersion; // заголовок
            fps = new FPS(); // будет считать и показывать FPS
            view = sp.Panel1;

            vmon = new Render(view);
            buff = vmon.GetBuffer();
            background = new Background();

            timer.Interval = 15; // between 1 ms and 20 ms разброс т.к. не реалтайм
            timer.Tick += new EventHandler(timer_Tick);
            timer.Enabled = false;

            // начальные значения
            Transform.RotateModel(f, 15, 0, 0);
            Transform.MoveModel(f, 0, -250, 0);

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
            background.DrawAxes(buff);
            Transform.RotateModel(cc, 0.1, 0.1, 0.1);
            Transform.MoveModel(cc, 0,mv,0);
            if ((cc.cs.placeInWorld.y > 50) || (cc.cs.placeInWorld.y < 0)) { mv = mv * -1; }
            Draw.Object3D(cc, buff);


            Transform.RotateModel(f, 0, 0.3, 0);
            Draw.Object3D(f, buff);

            fps.Draw(buff);
            vmon.BufferToPanel();
            fps.SetFrameRendered();
            buff = vmon.GetNewBuffSize();
        }

        //protected override void OnPaint(PaintEventArgs pea) { }

        private void button2_Click(object sender, EventArgs e)
        {
            // Делает недействительной конкретную область элемента управления
            // и вызывает отправку сообщения изображения элементу управления.
            //this.Invalidate();
            //cube.TranXYZ(1.2, 1, 1);
        }


        private void button4_Click(object sender, EventArgs e)
        {
            //this.Invalidate();
            //cube.TranXYZ(0.8, 1, 1);
        }

        // ?
        private void formGraphics_Load(object sender, EventArgs e)
        {

        }

        // ?
        private void button7_Click(object sender, EventArgs e)
        {
            this.Invalidate();
            //cube.Move(-15, 0, 0);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Invalidate();
            //cube.Move(15, 0, 0);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Invalidate();
            //cube.AngleChange(3, 3, 3);
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (timer.Enabled)
            {
                timer.Enabled = !(timer.Enabled);
                button1.Text = "Run";
            }
            else
            {
                timer.Enabled = !(timer.Enabled);
                button1.Text = "Stop";
            }


        }

        // ?
        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            //cube.RotationXYZ(hScrollBar1.Value / 100, 0, 0);
            //cube.degreeX = 0;
            //cube.AngleChange(hScrollBar1.Value,0,0);
            background.DrawAxes(buff);
            //cube.Draw(buff);
            fps.Draw(buff);
            vmon.BufferToPanel();
            fps.SetFrameRendered();
            buff = vmon.GetNewBuffSize();
            //hScrollBar1.Value = (int)cube.degreeX;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            //cube.degreeX = 0;
            //cube.AngleChange(trackBar1.Value, 0, 0);
            background.DrawAxes(buff);
            //cube.Draw(buff);
            fps.Draw(buff);
            vmon.BufferToPanel();
            fps.SetFrameRendered();
            buff = vmon.GetNewBuffSize();
        }

    }
}