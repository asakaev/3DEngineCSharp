namespace CubeApp
{
    partial class mform
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button4 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.button1 = new System.Windows.Forms.Button();
            this.sp = new System.Windows.Forms.SplitContainer();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sp)).BeginInit();
            this.sp.Panel2.SuspendLayout();
            this.sp.SuspendLayout();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(547, 11);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "TranX 0.8";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(628, 11);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 6;
            this.button7.Text = "MoveX -15";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(709, 11);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 7;
            this.button8.Text = "MoveX 15";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(803, 11);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 8;
            this.button9.Text = "button9";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.LargeChange = 1;
            this.hScrollBar1.Location = new System.Drawing.Point(9, 68);
            this.hScrollBar1.Maximum = 180;
            this.hScrollBar1.Minimum = -180;
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(558, 17);
            this.hScrollBar1.TabIndex = 9;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(3, 11);
            this.trackBar1.Maximum = 90;
            this.trackBar1.Minimum = -90;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(423, 45);
            this.trackBar1.TabIndex = 10;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(903, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Run";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // sp
            // 
            this.sp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sp.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.sp.IsSplitterFixed = true;
            this.sp.Location = new System.Drawing.Point(0, 0);
            this.sp.Name = "sp";
            this.sp.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // sp.Panel1
            // 
            this.sp.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // sp.Panel2
            // 
            this.sp.Panel2.Controls.Add(this.button1);
            this.sp.Panel2.Controls.Add(this.trackBar1);
            this.sp.Panel2.Controls.Add(this.button9);
            this.sp.Panel2.Controls.Add(this.hScrollBar1);
            this.sp.Panel2.Controls.Add(this.button8);
            this.sp.Panel2.Controls.Add(this.button2);
            this.sp.Panel2.Controls.Add(this.button4);
            this.sp.Panel2.Controls.Add(this.button7);
            this.sp.Size = new System.Drawing.Size(1001, 676);
            this.sp.SplitterDistance = 578;
            this.sp.TabIndex = 12;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(466, 11);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "TranX 1.2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // mform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 676);
            this.Controls.Add(this.sp);
            this.Name = "mform";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.formGraphics_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.sp.Panel2.ResumeLayout(false);
            this.sp.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sp)).EndInit();
            this.sp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SplitContainer sp;
        private System.Windows.Forms.Button button2;

    }
}

