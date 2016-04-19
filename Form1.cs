// Rozpoznawanie obrazu z gry warcaby oraz wizualizacja gry na komputerze .

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;

using OpenTK;


namespace warcaby
{

    public partial class Form1 : Form
    {
        //zmienne 
     // public Tess3D window; 
       // global
        
        Capture capWebcam = null; // kamera 
        bool przechwytywanie = false;
        Image<Bgr, Byte> zdjOryginalne;
        Image<Gray, Byte> zdjTworzone;
        
        public Form1()
        {
            InitializeComponent();
           // GameWindow window = new GameWindow(640, 480);
           // window.Run();
            //this.window
            // gl 
            

        }

        private void Form1_Load(object sender, EventArgs e)
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

            CircleF[] circles = zdjTworzone.HoughCircles(new Gray(100), new Gray(50), 2, zdjTworzone.Height/4, 10, 400) [0];
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
                CvInvoke.cvCircle(zdjOryginalne, new Point ((int)circle.Center.X, (int)circle.Center.Y ),3, new MCvScalar(0, 255, 0), -1, LINE_TYPE.CV_AA,0) ;
                //Grubość koła w pikselach, -1 wskazuje aby wypełnić koło ; AA -wygładzenie pikseli, 0 - bez przesuniecia 
                zdjOryginalne.Draw(circle, new Bgr(Color.Red), 3); // narysowac czerwone kolo wokol wykrytego obiektu 
                

                
            }

            ibOriginal.Image = zdjOryginalne;
            ibProcessed.Image = zdjTworzone;
        }


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

        private void button1_Click(object sender, EventArgs e)
        {
            GameWindow window = new GameWindow(640, 480);
            window.Run();

        }


        // open gl


      /* public Tess3D():base()
        {
            LightAmbient = new float[4];
            LightAmbient[0] = 1.0f;
            LightAmbient[1] = 1.0f;
            LightAmbient[2] = 1.0f;
            LightAmbient[3] = 1.0f;
            LightDiffuse = new float[4];
            LightDiffuse[0] = 1.0f;
            LightDiffuse[1] = 1.0f;
            LightDiffuse[2] = 1.0f;
            LightDiffuse[3] = 1.0f;
            LightPosition = new float[4];
            LightPosition[0] = 0.0f;
            LightPosition[1] = 0.0f;
            LightPosition[2] = -1.0f;
            LightPosition[3] = 1.0f;
            this.Textures = new uint[file.Length];
        }*/
        //protected override void InitGLContext()
        //{ }
    }

      //public Tess3D(): base()
       // {}
}
