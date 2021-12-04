namespace SNES_multiROM_Maker
{
    partial class SNESmulticartMaker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SNESmulticartMaker));
            this.button1 = new System.Windows.Forms.Button();
            this.firsttext = new System.Windows.Forms.TextBox();
            this.first = new System.Windows.Forms.Button();
            this.secondtext = new System.Windows.Forms.TextBox();
            this.second = new System.Windows.Forms.Button();
            this.thirdtext = new System.Windows.Forms.TextBox();
            this.third = new System.Windows.Forms.Button();
            this.fourthtext = new System.Windows.Forms.TextBox();
            this.fourth = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.eightmb = new System.Windows.Forms.RadioButton();
            this.sixteenmb = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(180, 355);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 33);
            this.button1.TabIndex = 0;
            this.button1.Text = "Build ROM";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // firsttext
            // 
            this.firsttext.BackColor = System.Drawing.SystemColors.HighlightText;
            this.firsttext.Location = new System.Drawing.Point(12, 111);
            this.firsttext.Name = "firsttext";
            this.firsttext.ReadOnly = true;
            this.firsttext.Size = new System.Drawing.Size(322, 20);
            this.firsttext.TabIndex = 3;
            // 
            // first
            // 
            this.first.Location = new System.Drawing.Point(12, 137);
            this.first.Name = "first";
            this.first.Size = new System.Drawing.Size(93, 19);
            this.first.TabIndex = 4;
            this.first.Text = "1st Game";
            this.first.UseVisualStyleBackColor = true;
            this.first.Click += new System.EventHandler(this.first_Click);
            // 
            // secondtext
            // 
            this.secondtext.BackColor = System.Drawing.SystemColors.HighlightText;
            this.secondtext.Location = new System.Drawing.Point(12, 162);
            this.secondtext.Name = "secondtext";
            this.secondtext.ReadOnly = true;
            this.secondtext.Size = new System.Drawing.Size(322, 20);
            this.secondtext.TabIndex = 5;
            // 
            // second
            // 
            this.second.Location = new System.Drawing.Point(12, 188);
            this.second.Name = "second";
            this.second.Size = new System.Drawing.Size(93, 19);
            this.second.TabIndex = 6;
            this.second.Text = "2nd Game";
            this.second.UseVisualStyleBackColor = true;
            this.second.Click += new System.EventHandler(this.second_Click);
            // 
            // thirdtext
            // 
            this.thirdtext.BackColor = System.Drawing.SystemColors.HighlightText;
            this.thirdtext.Location = new System.Drawing.Point(12, 213);
            this.thirdtext.Name = "thirdtext";
            this.thirdtext.ReadOnly = true;
            this.thirdtext.Size = new System.Drawing.Size(322, 20);
            this.thirdtext.TabIndex = 7;
            // 
            // third
            // 
            this.third.Location = new System.Drawing.Point(12, 239);
            this.third.Name = "third";
            this.third.Size = new System.Drawing.Size(93, 19);
            this.third.TabIndex = 8;
            this.third.Text = "3rd Game";
            this.third.UseVisualStyleBackColor = true;
            this.third.Click += new System.EventHandler(this.third_Click);
            // 
            // fourthtext
            // 
            this.fourthtext.BackColor = System.Drawing.SystemColors.HighlightText;
            this.fourthtext.Location = new System.Drawing.Point(12, 264);
            this.fourthtext.Name = "fourthtext";
            this.fourthtext.ReadOnly = true;
            this.fourthtext.Size = new System.Drawing.Size(322, 20);
            this.fourthtext.TabIndex = 9;
            // 
            // fourth
            // 
            this.fourth.Location = new System.Drawing.Point(12, 287);
            this.fourth.Name = "fourth";
            this.fourth.Size = new System.Drawing.Size(93, 19);
            this.fourth.TabIndex = 10;
            this.fourth.Text = "4th Game";
            this.fourth.UseVisualStyleBackColor = true;
            this.fourth.Click += new System.EventHandler(this.fourth_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 316);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "SNES Blaster Memory Size";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 329);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "(MUST SELECT ONE)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label3.Location = new System.Drawing.Point(298, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "About...";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // eightmb
            // 
            this.eightmb.AutoSize = true;
            this.eightmb.Checked = true;
            this.eightmb.Location = new System.Drawing.Point(12, 345);
            this.eightmb.Name = "eightmb";
            this.eightmb.Size = new System.Drawing.Size(61, 17);
            this.eightmb.TabIndex = 14;
            this.eightmb.TabStop = true;
            this.eightmb.Text = "8MByte";
            this.eightmb.UseVisualStyleBackColor = true;
            this.eightmb.CheckedChanged += new System.EventHandler(this.eightmb_CheckedChanged);
            // 
            // sixteenmb
            // 
            this.sixteenmb.AutoSize = true;
            this.sixteenmb.Location = new System.Drawing.Point(12, 368);
            this.sixteenmb.Name = "sixteenmb";
            this.sixteenmb.Size = new System.Drawing.Size(67, 17);
            this.sixteenmb.TabIndex = 15;
            this.sixteenmb.Text = "16MByte";
            this.sixteenmb.UseVisualStyleBackColor = true;
            this.sixteenmb.CheckedChanged += new System.EventHandler(this.sixteenmb_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(8, 312);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(144, 76);
            this.panel1.TabIndex = 16;
            // 
            // pictureBox1
            // 
            this.pictureBox1.InitialImage = global::SNES_multiROM_Maker.Properties.Resources.smalllogo2;
            this.pictureBox1.Location = new System.Drawing.Point(41, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(257, 100);
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(181, 308);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(131, 33);
            this.progressBar1.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(199, 290);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "ROM Space Used";
            // 
            // SNESmulticartMaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 395);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.sixteenmb);
            this.Controls.Add(this.eightmb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fourth);
            this.Controls.Add(this.fourthtext);
            this.Controls.Add(this.third);
            this.Controls.Add(this.thirdtext);
            this.Controls.Add(this.second);
            this.Controls.Add(this.secondtext);
            this.Controls.Add(this.first);
            this.Controls.Add(this.firsttext);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SNESmulticartMaker";
            this.Text = "SNES Multi-Cart Maker";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox firsttext;
        private System.Windows.Forms.Button first;
        private System.Windows.Forms.TextBox secondtext;
        private System.Windows.Forms.Button second;
        private System.Windows.Forms.TextBox thirdtext;
        private System.Windows.Forms.Button third;
        private System.Windows.Forms.TextBox fourthtext;
        private System.Windows.Forms.Button fourth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton eightmb;
        private System.Windows.Forms.RadioButton sixteenmb;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label4;
    }
}

