namespace Scene3D
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
            this.button1 = new System.Windows.Forms.Button();
            this.sp = new System.Windows.Forms.SplitContainer();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.right = new System.Windows.Forms.Button();
            this.left = new System.Windows.Forms.Button();
            this.down = new System.Windows.Forms.Button();
            this.up = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.barX = new System.Windows.Forms.TrackBar();
            this.barZ = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.sp)).BeginInit();
            this.sp.Panel2.SuspendLayout();
            this.sp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barZ)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(913, 59);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Stop";
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
            // sp.Panel2
            // 
            this.sp.Panel2.Controls.Add(this.button3);
            this.sp.Panel2.Controls.Add(this.button2);
            this.sp.Panel2.Controls.Add(this.right);
            this.sp.Panel2.Controls.Add(this.left);
            this.sp.Panel2.Controls.Add(this.down);
            this.sp.Panel2.Controls.Add(this.up);
            this.sp.Panel2.Controls.Add(this.label4);
            this.sp.Panel2.Controls.Add(this.label3);
            this.sp.Panel2.Controls.Add(this.label2);
            this.sp.Panel2.Controls.Add(this.label1);
            this.sp.Panel2.Controls.Add(this.barX);
            this.sp.Panel2.Controls.Add(this.barZ);
            this.sp.Panel2.Controls.Add(this.button1);
            this.sp.Size = new System.Drawing.Size(1000, 662);
            this.sp.SplitterDistance = 564;
            this.sp.TabIndex = 12;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(432, 9);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 23;
            this.button3.Text = "Zoom In";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(432, 36);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 22;
            this.button2.Text = "Zoom Out";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // right
            // 
            this.right.Location = new System.Drawing.Point(325, 34);
            this.right.Name = "right";
            this.right.Size = new System.Drawing.Size(25, 25);
            this.right.TabIndex = 21;
            this.right.Text = "<";
            this.right.UseVisualStyleBackColor = true;
            this.right.Click += new System.EventHandler(this.right_Click);
            // 
            // left
            // 
            this.left.Location = new System.Drawing.Point(387, 34);
            this.left.Name = "left";
            this.left.Size = new System.Drawing.Size(25, 25);
            this.left.TabIndex = 20;
            this.left.Text = ">";
            this.left.UseVisualStyleBackColor = true;
            this.left.Click += new System.EventHandler(this.left_Click);
            // 
            // down
            // 
            this.down.Location = new System.Drawing.Point(356, 7);
            this.down.Name = "down";
            this.down.Size = new System.Drawing.Size(25, 25);
            this.down.TabIndex = 19;
            this.down.Text = "^";
            this.down.UseVisualStyleBackColor = true;
            this.down.Click += new System.EventHandler(this.down_Click);
            // 
            // up
            // 
            this.up.Location = new System.Drawing.Point(356, 34);
            this.up.Name = "up";
            this.up.Size = new System.Drawing.Size(25, 25);
            this.up.TabIndex = 18;
            this.up.Text = "†";
            this.up.UseVisualStyleBackColor = true;
            this.up.Click += new System.EventHandler(this.up_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(265, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "0 deg";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(265, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "0 deg";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "X axis";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Y axis";
            // 
            // barX
            // 
            this.barX.LargeChange = 1;
            this.barX.Location = new System.Drawing.Point(53, 46);
            this.barX.Maximum = 8;
            this.barX.Minimum = -8;
            this.barX.Name = "barX";
            this.barX.Size = new System.Drawing.Size(206, 45);
            this.barX.TabIndex = 13;
            this.barX.Scroll += new System.EventHandler(this.barX_Scroll);
            // 
            // barZ
            // 
            this.barZ.LargeChange = 1;
            this.barZ.Location = new System.Drawing.Point(53, 3);
            this.barZ.Maximum = 8;
            this.barZ.Minimum = -8;
            this.barZ.Name = "barZ";
            this.barZ.Size = new System.Drawing.Size(206, 45);
            this.barZ.TabIndex = 12;
            this.barZ.Scroll += new System.EventHandler(this.barZ_Scroll);
            // 
            // mform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 662);
            this.Controls.Add(this.sp);
            this.Name = "mform";
            this.Text = "Form1";
            this.sp.Panel2.ResumeLayout(false);
            this.sp.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sp)).EndInit();
            this.sp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barZ)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SplitContainer sp;
        private System.Windows.Forms.TrackBar barZ;
        private System.Windows.Forms.TrackBar barX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button right;
        private System.Windows.Forms.Button left;
        private System.Windows.Forms.Button down;
        private System.Windows.Forms.Button up;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;

    }
}

