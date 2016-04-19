namespace warcaby
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.ibProcessed = new Emgu.CV.UI.ImageBox();
            this.ibOriginal = new Emgu.CV.UI.ImageBox();
            this.btnPausseorResume = new System.Windows.Forms.Button();
            this.txtXYZPromien = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ibProcessed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ibOriginal)).BeginInit();
            this.SuspendLayout();
            // 
            // ibProcessed
            // 
            this.ibProcessed.Location = new System.Drawing.Point(671, 12);
            this.ibProcessed.Name = "ibProcessed";
            this.ibProcessed.Size = new System.Drawing.Size(640, 480);
            this.ibProcessed.TabIndex = 2;
            this.ibProcessed.TabStop = false;
            // 
            // ibOriginal
            // 
            this.ibOriginal.Location = new System.Drawing.Point(12, 22);
            this.ibOriginal.Name = "ibOriginal";
            this.ibOriginal.Size = new System.Drawing.Size(640, 480);
            this.ibOriginal.TabIndex = 2;
            this.ibOriginal.TabStop = false;
            // 
            // btnPausseorResume
            // 
            this.btnPausseorResume.Location = new System.Drawing.Point(12, 552);
            this.btnPausseorResume.Name = "btnPausseorResume";
            this.btnPausseorResume.Size = new System.Drawing.Size(132, 44);
            this.btnPausseorResume.TabIndex = 3;
            this.btnPausseorResume.Text = "PAUZA";
            this.btnPausseorResume.UseVisualStyleBackColor = true;
            this.btnPausseorResume.Click += new System.EventHandler(this.btnPausseorResume_Click);
            // 
            // txtXYZPromien
            // 
            this.txtXYZPromien.ForeColor = System.Drawing.Color.YellowGreen;
            this.txtXYZPromien.Location = new System.Drawing.Point(184, 509);
            this.txtXYZPromien.Multiline = true;
            this.txtXYZPromien.Name = "txtXYZPromien";
            this.txtXYZPromien.ReadOnly = true;
            this.txtXYZPromien.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtXYZPromien.Size = new System.Drawing.Size(468, 111);
            this.txtXYZPromien.TabIndex = 4;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(671, 498);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(209, 20);
            this.textBox1.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(671, 541);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1326, 632);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txtXYZPromien);
            this.Controls.Add(this.btnPausseorResume);
            this.Controls.Add(this.ibOriginal);
            this.Controls.Add(this.ibProcessed);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ibProcessed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ibOriginal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox ibProcessed;
        private Emgu.CV.UI.ImageBox ibOriginal;
        private System.Windows.Forms.Button btnPausseorResume;
        private System.Windows.Forms.TextBox txtXYZPromien;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
    }
}

