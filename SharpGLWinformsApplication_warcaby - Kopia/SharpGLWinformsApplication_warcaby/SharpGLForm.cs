using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpGL;
//using OpenGL;
 


//
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;

using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Design;
using System.ComponentModel;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Assets;
using System.Runtime.InteropServices;
//
 

namespace SharpGLWinformsApplication_warcaby
{
    /// <summary>
    /// The main form class.
    /// </summary>
    public partial class SharpGLForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SharpGLForm"/> class.
        /// </summary>
        /// 
        
       // public Form1 window;
        Capture capWebcam = null; // kamera 
        bool przechwytywanie = false;
        Image<Bgr, Byte> zdjOryginalne;
        Image<Gray, Byte> zdjTworzone;
       // Texture Textures = new Texture();

        float rtri = 0;

        //Textures_ texture = new Textures_();
        Texture texture = new Texture();

        string[] file = {
        @"Data\szach.bmp"
                        };


        public enum TEX
        {
            szachownica = 0

        }



        public SharpGLForm()
        {
            InitializeComponent();

           // this.window = new Form1();
            //this.window.Parent = this; // było samo this, czyli ze rodzicem jest główne okno. Wiec zmieniasz na
           // this.window.Dock = DockStyle.Fill;
           // window.Show();

           // window.Pokaz();
            SharpGL.OpenGL gl = this.openGLControl.OpenGL;

            gl.Enable(OpenGL.GL_TEXTURE_2D);
            //texture.Create(gl, "szach.bmp");
            texture.Create(gl, "C://Users/dom/Desktop/szach.bmp");
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ibOriginal = new Emgu.CV.UI.ImageBox();
            this.ibProcessed = new Emgu.CV.UI.ImageBox();
            this.btnPausseorResume = new System.Windows.Forms.Button();
            this.txtXYZPromien = new System.Windows.Forms.TextBox();
            this.btn_wlacz = new System.Windows.Forms.Button();
            this.openGLControl = new SharpGL.OpenGLControl();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox_mozliwe_ruchy = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_mozliwe_bicia = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox_stan_czarne_pionki = new System.Windows.Forms.TextBox();
            this.textBox_stan_biale_pionki = new System.Windows.Forms.TextBox();
            this.textBox_stan_ruchy = new System.Windows.Forms.TextBox();
            this.textBox_stan_ruchy_wykonane = new System.Windows.Forms.TextBox();
            this.textBox_stan_bicia_mozliwe = new System.Windows.Forms.TextBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ibOriginal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ibProcessed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // ibOriginal
            // 
            this.ibOriginal.Location = new System.Drawing.Point(12, 227);
            this.ibOriginal.Name = "ibOriginal";
            this.ibOriginal.Size = new System.Drawing.Size(401, 363);
            this.ibOriginal.TabIndex = 2;
            this.ibOriginal.TabStop = false;
            // 
            // ibProcessed
            // 
            this.ibProcessed.Location = new System.Drawing.Point(1036, 12);
            this.ibProcessed.Name = "ibProcessed";
            this.ibProcessed.Size = new System.Drawing.Size(281, 360);
            this.ibProcessed.TabIndex = 2;
            this.ibProcessed.TabStop = false;
            // 
            // btnPausseorResume
            // 
            this.btnPausseorResume.Location = new System.Drawing.Point(12, 651);
            this.btnPausseorResume.Name = "btnPausseorResume";
            this.btnPausseorResume.Size = new System.Drawing.Size(131, 34);
            this.btnPausseorResume.TabIndex = 3;
            this.btnPausseorResume.Text = "PAUZA";
            this.btnPausseorResume.UseVisualStyleBackColor = true;
            this.btnPausseorResume.Click += new System.EventHandler(this.btnPausseorResume_Click);
            // 
            // txtXYZPromien
            // 
            this.txtXYZPromien.Location = new System.Drawing.Point(149, 596);
            this.txtXYZPromien.Multiline = true;
            this.txtXYZPromien.Name = "txtXYZPromien";
            this.txtXYZPromien.ReadOnly = true;
            this.txtXYZPromien.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtXYZPromien.Size = new System.Drawing.Size(283, 89);
            this.txtXYZPromien.TabIndex = 4;
            // 
            // btn_wlacz
            // 
            this.btn_wlacz.Location = new System.Drawing.Point(12, 596);
            this.btn_wlacz.Name = "btn_wlacz";
            this.btn_wlacz.Size = new System.Drawing.Size(131, 38);
            this.btn_wlacz.TabIndex = 5;
            this.btn_wlacz.Text = "WŁĄCZ";
            this.btn_wlacz.UseVisualStyleBackColor = true;
            this.btn_wlacz.Click += new System.EventHandler(this.button2_Click);
            // 
            // openGLControl
            // 
            this.openGLControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openGLControl.DrawFPS = true;
            this.openGLControl.Location = new System.Drawing.Point(0, 0);
            this.openGLControl.Name = "openGLControl";
            this.openGLControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl.RenderContextType = SharpGL.RenderContextType.FBO;
            this.openGLControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl.Size = new System.Drawing.Size(1329, 708);
            this.openGLControl.TabIndex = 0;
            this.openGLControl.OpenGLInitialized += new System.EventHandler(this.openGLControl_OpenGLInitialized);
            this.openGLControl.OpenGLDraw += new SharpGL.RenderEventHandler(this.openGLControl_OpenGLDraw);
            this.openGLControl.Resized += new System.EventHandler(this.openGLControl_Resized);
            this.openGLControl.Load += new System.EventHandler(this.openGLControl_Load);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Możliwe ruchy:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox_mozliwe_ruchy);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(243, 131);
            this.panel1.TabIndex = 7;
            // 
            // textBox_mozliwe_ruchy
            // 
            this.textBox_mozliwe_ruchy.Location = new System.Drawing.Point(0, 16);
            this.textBox_mozliwe_ruchy.Multiline = true;
            this.textBox_mozliwe_ruchy.Name = "textBox_mozliwe_ruchy";
            this.textBox_mozliwe_ruchy.ReadOnly = true;
            this.textBox_mozliwe_ruchy.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_mozliwe_ruchy.Size = new System.Drawing.Size(240, 111);
            this.textBox_mozliwe_ruchy.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBox_mozliwe_bicia);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(271, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(243, 131);
            this.panel2.TabIndex = 8;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(530, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(103, 131);
            this.panel3.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Możliwe bicia:";
            // 
            // textBox_mozliwe_bicia
            // 
            this.textBox_mozliwe_bicia.Location = new System.Drawing.Point(3, 16);
            this.textBox_mozliwe_bicia.Multiline = true;
            this.textBox_mozliwe_bicia.Name = "textBox_mozliwe_bicia";
            this.textBox_mozliwe_bicia.ReadOnly = true;
            this.textBox_mozliwe_bicia.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_mozliwe_bicia.Size = new System.Drawing.Size(240, 111);
            this.textBox_mozliwe_bicia.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Pionki czarne:";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label4);
            this.panel4.Location = new System.Drawing.Point(650, 12);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(103, 131);
            this.panel4.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Pionki białe:";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.textBox_stan_bicia_mozliwe);
            this.panel5.Controls.Add(this.textBox_stan_ruchy_wykonane);
            this.panel5.Controls.Add(this.textBox_stan_ruchy);
            this.panel5.Controls.Add(this.textBox_stan_biale_pionki);
            this.panel5.Controls.Add(this.textBox_stan_czarne_pionki);
            this.panel5.Controls.Add(this.label10);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Location = new System.Drawing.Point(774, 12);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(243, 131);
            this.panel5.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Stan gry:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Ilość czarnych pionków:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Ilość białych pionków:";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 67);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(121, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Ilość możliwych ruchów:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 110);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Ilość możliwych bić:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 91);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(133, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Ilość wykonanych ruchów:";
            // 
            // textBox_stan_czarne_pionki
            // 
            this.textBox_stan_czarne_pionki.Location = new System.Drawing.Point(145, 16);
            this.textBox_stan_czarne_pionki.Name = "textBox_stan_czarne_pionki";
            this.textBox_stan_czarne_pionki.ReadOnly = true;
            this.textBox_stan_czarne_pionki.Size = new System.Drawing.Size(59, 20);
            this.textBox_stan_czarne_pionki.TabIndex = 9;
            // 
            // textBox_stan_biale_pionki
            // 
            this.textBox_stan_biale_pionki.Location = new System.Drawing.Point(145, 38);
            this.textBox_stan_biale_pionki.Name = "textBox_stan_biale_pionki";
            this.textBox_stan_biale_pionki.ReadOnly = true;
            this.textBox_stan_biale_pionki.Size = new System.Drawing.Size(59, 20);
            this.textBox_stan_biale_pionki.TabIndex = 10;
            // 
            // textBox_stan_ruchy
            // 
            this.textBox_stan_ruchy.Location = new System.Drawing.Point(145, 60);
            this.textBox_stan_ruchy.Name = "textBox_stan_ruchy";
            this.textBox_stan_ruchy.ReadOnly = true;
            this.textBox_stan_ruchy.Size = new System.Drawing.Size(59, 20);
            this.textBox_stan_ruchy.TabIndex = 11;
            // 
            // textBox_stan_ruchy_wykonane
            // 
            this.textBox_stan_ruchy_wykonane.Location = new System.Drawing.Point(145, 84);
            this.textBox_stan_ruchy_wykonane.Name = "textBox_stan_ruchy_wykonane";
            this.textBox_stan_ruchy_wykonane.ReadOnly = true;
            this.textBox_stan_ruchy_wykonane.Size = new System.Drawing.Size(59, 20);
            this.textBox_stan_ruchy_wykonane.TabIndex = 12;
            // 
            // textBox_stan_bicia_mozliwe
            // 
            this.textBox_stan_bicia_mozliwe.Location = new System.Drawing.Point(145, 107);
            this.textBox_stan_bicia_mozliwe.Name = "textBox_stan_bicia_mozliwe";
            this.textBox_stan_bicia_mozliwe.ReadOnly = true;
            this.textBox_stan_bicia_mozliwe.Size = new System.Drawing.Size(59, 20);
            this.textBox_stan_bicia_mozliwe.TabIndex = 13;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label11);
            this.panel6.Location = new System.Drawing.Point(12, 197);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(243, 24);
            this.panel6.TabIndex = 11;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(220, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Obserwacja stanu gry w czasie rzeczywistym:";
            // 
            // SharpGLForm
            // 
            this.ClientSize = new System.Drawing.Size(1329, 708);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_wlacz);
            this.Controls.Add(this.txtXYZPromien);
            this.Controls.Add(this.btnPausseorResume);
            this.Controls.Add(this.ibProcessed);
            this.Controls.Add(this.ibOriginal);
            this.Controls.Add(this.openGLControl);
            this.Name = "SharpGLForm";
            ((System.ComponentModel.ISupportInitialize)(this.ibOriginal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ibProcessed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        private void SharpGLForm_Load(object sender, EventArgs e)
        {
            try
            {
                capWebcam = new Capture();   // przechwytywanie 
            }
            catch (NullReferenceException except)
            {
                txtXYZPromien.Text = except.Message;

                return;
            }

            //gdy mamy wlasciwy obiekt do przechwycenia
            //dodajemy funkcje obrazu do listy zadań aplikacji

            Application.Idle += procesRamkaIAktualizacjaGUI;   // wystąienie zdarzenia - pojawienie się przedmiotu przed ramką - wywolanie funkcji  
            przechwytywanie = true;                           //sczytywanie obrazu przez kamerę i aktualizacja zmiennej flagi - przechwytujemy 


        }

        public void Pokaz()
        {
            try
            {
                capWebcam = new Capture();   // przechwytywanie 
            }
            catch (NullReferenceException except)
            {
                txtXYZPromien.Text = except.Message;

                return;
            }

            //gdy mamy wlasciwy obiekt do przechwycenia
            //dodajemy funkcje obrazu do listy zadań aplikacji

            Application.Idle += procesRamkaIAktualizacjaGUI;   // wystąienie zdarzenia - pojawienie się przedmiotu przed ramką - wywolanie funkcji  
            przechwytywanie = true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (capWebcam != null)
            {
                capWebcam.Dispose();
            }
        }

        void procesRamkaIAktualizacjaGUI(object sender, EventArgs arg)
        {

            zdjOryginalne = capWebcam.QueryFrame();  // do zdj wczytywanie obrazu przechwyconego z kamery 
            if (zdjOryginalne == null)
                return;
            zdjTworzone = zdjOryginalne.InRange(new Bgr(0, 0, 175), new Bgr(100, 100, 256)); //wartość minimalna i max filtru (jeśli kolor jest większy niż lub równy tej wartości)

            // InRange sprawdza czy elementy obrazu leżą pomiędza dwoma zmiennymi skalarnymi
            // TColor <byte> {lower hihger} - TColor byte
            zdjTworzone = zdjTworzone.SmoothGaussian(9);

            CircleF[] circles = zdjTworzone.HoughCircles(new Gray(100), new Gray(50), 2, zdjTworzone.Height / 4, 10, 400)[0];
            //  100 prog Canny - Canny edge detector - operator wykrywania krawędzi wykorzystujacy alg wielostopniowyw w celu wykrycia szeregu krawędzi 
            //   zdjTworzone.Height/4 -  min odległość w pikselach między ośrodkami wykrytych kręgach
            // min i max promień wykrytego okręgu koła się z pierwszego kanału
            foreach (CircleF circle in circles)
            {
                if (txtXYZPromien.Text != "")
                    txtXYZPromien.AppendText(Environment.NewLine);
                txtXYZPromien.AppendText("Pozycja pilki : X = " + circle.Center.X.ToString().PadLeft(4) +       // x i y pozycji srodka okregu
                                        " , Y = " + circle.Center.Y.ToString().PadLeft(4) +
                                        " , Promien = " + circle.Radius.ToString("###.000").PadLeft(7));

                txtXYZPromien.ScrollToCaret();  // przesunąć pasek przewijania w dół pola tekstowego  

                // rysowanie  małego zielonego kółka w środku wykrytego obiektu 
                // rysowanie okręgu o promieniu 3, mimo że wielkość wykrytego koła będzie znacznie większa
                // obiekty CvInvoke mogą zostać wykorzystane do wywołania innych funkcji OpenCV 
                CvInvoke.cvCircle(zdjOryginalne, new Point((int)circle.Center.X, (int)circle.Center.Y), 3, new MCvScalar(0, 255, 0), -1, LINE_TYPE.CV_AA, 0);
                //Grubość koła w pikselach, -1 wskazuje aby wypełnić koło ; AA -wygładzenie pikseli, 0 - bez przesuniecia 
                zdjOryginalne.Draw(circle, new Bgr(Color.Red), 3); // narysowac czerwone kolo wokol wykrytego obiektu 



            }

            ibOriginal.Image = zdjOryginalne;
            ibProcessed.Image = zdjTworzone;
        }




       /* private void button1_Click(object sender, EventArgs e)
        {
            this.window = new Form1();
            window.Show();
            window.Pokaz();
            //window.Activate();

             
        }*/

        private void btnPausseorResume_Click(object sender, EventArgs e)
        {
            if (przechwytywanie == true)
            {
                Application.Idle -= procesRamkaIAktualizacjaGUI; // usun f.obrazu w liscie zadan aplikacji 
                przechwytywanie = false; // zmien znacznik - flage 
                btnPausseorResume.Text = "WZNÓW";

            }
            else
            {
                Application.Idle += procesRamkaIAktualizacjaGUI;
                przechwytywanie = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Pokaz();
        }


  
           /////////////////////////////////////////////////////////////////////////
 
      
    
        
        public bool LoadTexture(string FileName, ref uint Texture)
        {
            OpenGL gl = openGLControl.OpenGL;
            

            gl.Enable(OpenGL.GL_TEXTURE_2D);
            //Textures.Create(gl, "szach.bmp");

            Bitmap image = null;
            try
            {
                image = new Bitmap(FileName);
            }
            catch (System.ArgumentException)
            {
                MessageBox.Show("Could not load " + FileName + ". Please make sure that Data is a subfolder from where the application is running.",
                "Error", MessageBoxButtons.OK);
                return false;
            }
            if (image != null)
            {
                image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                System.Drawing.Imaging.BitmapData bitmapdata;
                Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
                bitmapdata = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                uint[] t = new uint[1];
                gl.GenTextures(1, t);
                Texture = t[0];
                gl.BindTexture(OpenGL.GL_TEXTURE_2D, Texture);
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_LINEAR);  
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_LINEAR_MIPMAP_NEAREST);
                gl.Build2DMipmaps(OpenGL.GL_TEXTURE_2D, (int)OpenGL.GL_RGB, image.Width, image.Height, OpenGL.GL_BGR_EXT, OpenGL.GL_UNSIGNED_BYTE, bitmapdata.Scan0);
                image.UnlockBits(bitmapdata);
                image.Dispose();
                return true;
            }
            return false;
        }

  

        //zdefiniowanie tekstury 2D
       // void glTexImage2D( OpenGL.enum target, GLint level, GLint internalformat, GLsizei width, GLsizei height, GLint border, GLenum format, GLenum type, const OpenGL void * pixels )


        // rys szachownicy 
        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs e)
        {
            //  Get the OpenGL object.
            //OpenGL gl = openGLControl.OpenGL;
            SharpGL.OpenGL gl = this.openGLControl.OpenGL;
            //gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.Enable(OpenGL.GL_TEXTURE_2D); //wlacz teksture
            //  Clear the color and depth buffer.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            
            gl.LoadIdentity();
            gl.Translate(0.0f, 0.0f, -6.0f);
            gl.Rotate(rtri, 0.0f, 1.0f, 0.0f);

            texture.Bind(gl);
          
            //ok ok ok ok ok 
            //dok 
            gl.Begin(OpenGL.GL_QUADS); // rysowanie figury (czworokątów)
            gl.Color(255.0f, 255.0f, 255.0f);
            gl.Normal(0.0f, 1.0f, 0.0f); // Normalna wskazująca w dół
            gl.TexCoord(0.0f, 1.0f);
            gl.Vertex(-4.0f, -1.0f, -4.0f);

            // Prawy górny
            gl.TexCoord(0.0f, 0.0f);
            gl.Vertex(4.0f, -1.0f, -4.0f);
            // Lewy górny
            gl.TexCoord(1.0f, 0.0f);
            gl.Vertex(4.0f, -1.0f, 4.0f);
            gl.TexCoord(1.0f, 1.0f);
            gl.Vertex(-4.0f, -1.0f, 4.0f);
            // Prawy dolny
            gl.End();

             

        }



        /// <summary>
        /// Handles the OpenGLInitialized event of the openGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            //  TODO: Initialise OpenGL here.

            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;

            //  Set the clear color.
            gl.ClearColor(0, 0, 0, 0);
            
        }

        //Makes the image into a texture, and returns the id of the texture
        

        /// <summary>
        /// Handles the Resized event of the openGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void openGLControl_Resized(object sender, EventArgs e)
        {
            //  TODO: Set the projection matrix here.

            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;

            //  Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);

            //  Load the identity.
            gl.LoadIdentity();

            //  Create a perspective transformation.
            gl.Perspective(60.0f, (double)Width / (double)Height, 0.01, 100.0);

            //  Use the 'look at' helper function to position and aim the camera.
            //gl.LookAt(-5, 5, -5, 0, 0, 0, 0, 1, 0);
            
            gl.LookAt(0, 15, -15, 0, 0, 0, 0, 5, 0);
            //  Set the modelview matrix.
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }

        /// <summary>
        /// The current rotation.
        /// </summary>
        private float rotation = 0.0f;

        private void openGLControl_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        
        
    }
}
