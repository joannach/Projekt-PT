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
        Texture Textures = new Texture();
        

        string[] file = {
        @"Data\szach.bmp"
                        };



        public SharpGLForm()
        {
            InitializeComponent();

           // this.window = new Form1();
            //this.window.Parent = this; // było samo this, czyli ze rodzicem jest główne okno. Wiec zmieniasz na
           // this.window.Dock = DockStyle.Fill;
           // window.Show();

           // window.Pokaz();

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
            ((System.ComponentModel.ISupportInitialize)(this.ibOriginal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ibProcessed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
            this.SuspendLayout();
            // 
            // ibOriginal
            // 
            this.ibOriginal.Location = new System.Drawing.Point(12, 110);
            this.ibOriginal.Name = "ibOriginal";
            this.ibOriginal.Size = new System.Drawing.Size(401, 480);
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
            this.txtXYZPromien.Location = new System.Drawing.Point(169, 596);
            this.txtXYZPromien.Multiline = true;
            this.txtXYZPromien.Name = "txtXYZPromien";
            this.txtXYZPromien.ReadOnly = true;
            this.txtXYZPromien.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtXYZPromien.Size = new System.Drawing.Size(331, 89);
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
            // SharpGLForm
            // 
            this.ClientSize = new System.Drawing.Size(1329, 708);
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
 
        public enum TEX
        {
            TESS = 4,    
        }
    
        
        public bool LoadTexture(string FileName, ref uint Texture)
        {
            OpenGL gl = openGLControl.OpenGL;
            

            gl.Enable(OpenGL.GL_TEXTURE_2D);
            Textures.Create(gl, "szach.bmp");

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
 
        // rys szachownicy 
        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs e)
        {
            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;
            //gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            //Textures.Create(gl, "szach.bmp");
           // Textures.Bind(gl);

            //"szach.bmp"


           // LoadTexture(file, "szach.bmp");
            //  Clear the color and depth buffer.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            //  Load the identity rix.
            gl.LoadIdentity(); 
            //  Rotate around the Y axis.
            //gl.Rotate(50, 45, -30); //(rotation, 3.0f, 1.0f, 0.0f);
           // gl.Normal(30, 50, 30);

            gl.Enable(OpenGL.GL_CULL_FACE);
            gl.CullFace(OpenGL.GL_BACK);
            gl.FrontFace(OpenGL.GL_CW);


            //dok 
            gl.Begin(OpenGL.GL_QUADS); // rysowanie figury (czworokątów)
            gl.Color(255.0f, 255.0f, 255.0f);
            gl.Normal(0.0f, 1.0f, 0.0f); // Normalna wskazująca w dół
            gl.TexCoord(0.0f, 1.0f);
            gl.Vertex(-2.0f, -1.0f, -2.0f);

            // Prawy górny
            gl.TexCoord(0.0f, 0.0f);
            gl.Vertex(2.0f, -1.0f, -2.0f);
            // Lewy górny
            gl.TexCoord(1.0f, 0.0f);
            gl.Vertex(2.0f, -1.0f, 2.0f);
            gl.TexCoord(1.0f, 1.0f);
            gl.Vertex(-2.0f, -1.0f, 2.0f);

      

             
            // Prawy dolny
            gl.End();



            //

            //  Nudge the rotation.
        // rotation += 3.0f;

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
            gl.LookAt(-5, 5, -5, 0, 0, 0, 0, 1, 0);

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
        
        
    }
}
