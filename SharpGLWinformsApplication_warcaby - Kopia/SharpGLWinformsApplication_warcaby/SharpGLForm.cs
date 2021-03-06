﻿using System;
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
using System.Threading;
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
        Image<Gray, Byte> zdjTworzone2;
        Image<Gray, Byte> zdjTworzone3;
        Image<Gray, Byte> zdjTworzone4; 
      
        int[][] pola_final = new int[32][];
        double[][] srodki_pol = new double[32][];
        public Pole[,] pola = new Pole[8, 8];

        //pionki
        float zielony_X = 0;
        float zielony_Y = 0;
        float zielony_prom = 0;

        float czerwony_X = 0;
        float czerwony_Y = 0;
        float czerwony_prom = 0;


        float niebieski_X = 0;
        float niebieski_Y = 0;
        float niebieski_prom = 0;

        float zolty_X = 0;
        float zolty_Y = 0;
        float zolty_prom = 0;
        //

        float[] tablica_X_zielone= new float[12];
        float[] tablica_Y_zielone = new float[12];

        float[] tablica_X_czerwone = new float[12];
        float[] tablica_Y_czerwone = new float[12];

        float[] tablica_X_niebieskie = new float[12];
        float[] tablica_Y_niebieskie = new float[12];

        float[] tablica_X_zolte = new float[12];
        float[] tablica_Y_zolte = new float[12];

        //do wylicz() - macierz pola
        float gorny_lewy_X = 0;
        float gorny_lewy_Y = 0;

        float dolny_lewy_X = 0;
        float dolny_lewy_Y = 0;

        float gorny_prawy_X = 0;
        float gorny_prawy_Y = 0;

        float rozmiar_pola_wys = 0;
        float rozmiar_pola_szer = 0;

        float polowa_szer_pola = 0;
        float polowa_wys_pola = 0;

 
        bool czy_wykrywanie_pionkow = false;
        bool kolejna = false; 
        //koordynaty 

        float raz_X = 0;
        float dwa_X = 0;
        float trzy_X = 0;
        float raz_Y = 0;
        float dwa_Y = 0;
        float trzy_Y = 0;
        float raz_prom = 0;
        float dwa_prom = 0;
        float trzy_prom = 0;

        //wymiary planszy
        float wysokosc = 0;
        float szerokosc = 0;

        int delay = 0;

        float[,] pola_wirt_X;
        float[,] pola_wirt_Y;

        //Textures_ texture = new Textures_();
        Texture texture = new Texture();
        Texture texture_p = new Texture();

        string[] file = {
        @"Data\szach.bmp"
                        };



        public SharpGLForm()
        {
            InitializeComponent();
            try
            {
                capWebcam = new Capture(1);   // przechwytywanie 
            }
            catch (NullReferenceException except)
            {
               // txtXYZPromien.Text = except.Message;

                return;
            }
            SharpGL.OpenGL gl = this.openGLControl.OpenGL;
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            texture.Create(gl, "C://Users/dom/Desktop/01_szachownica.bmp");   // lub szachownica.bmp (8x8)

         

            textBox_stan_zielone_pionki.Text = "0";
            textBox_stan_czerwone_pionki.Text = "0";
            textBox_damki_niebieskie.Text = "0";
            textBox_stan_ruchy_wykonane.Text = "0";
            textBox_damki_żółte.Text = "0";
            damki_czerwone.AppendText("brak\n");
            damki_zielone.AppendText("brak\n");
            polaczerwone.AppendText("brak\n");
            polazielone.AppendText("brak\n");
            label_ruch.Text = "Ruch: zielone";
            label_ruch.BackColor = Color.Green;

            float wart_x = 6.5f;
            float wart_y = 1.0f;

            //tablica pol  wirtualych
            pola_wirt_X = new float[8,8];
            pola_wirt_Y = new float[8, 8];
            for (int i = 0; i < 8; i++)
            {
                //float nowa_polowa = polowa_szer_pola; 
                for (int j = 0; j < 8; j++)
                {
                    
                    pola_wirt_X[i, j] = wart_x;
                    pola_wirt_Y[i, j] = wart_y;

                    wart_x -= 0.77143f;
                    wart_y -= 0.77143f; 
                }
                wart_x = 6.5f;
                wart_y = 1.0f;
            }
        }
  
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ibOriginal = new Emgu.CV.UI.ImageBox();
            this.openGLControl = new SharpGL.OpenGLControl();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.damki_zielone = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.damki_czerwone = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.polazielone = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.polaczerwone = new System.Windows.Forms.TextBox();
            this.label_czerwone = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.textBox_damki_żółte = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_stan_ruchy_wykonane = new System.Windows.Forms.TextBox();
            this.textBox_damki_niebieskie = new System.Windows.Forms.TextBox();
            this.textBox_stan_czerwone_pionki = new System.Windows.Forms.TextBox();
            this.textBox_stan_zielone_pionki = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.button_ruch_przeciwnika = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label_ruch = new System.Windows.Forms.Label();
            this.button_wykryj_plansze = new System.Windows.Forms.Button();
            this.button_sprawdź = new System.Windows.Forms.Button();
            this.richTextBox_pola = new System.Windows.Forms.RichTextBox();
            this.button_pionki = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.button_pomoc = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ibOriginal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // ibOriginal
            // 
            this.ibOriginal.Location = new System.Drawing.Point(916, 252);
            this.ibOriginal.Name = "ibOriginal";
            this.ibOriginal.Size = new System.Drawing.Size(401, 363);
            this.ibOriginal.TabIndex = 2;
            this.ibOriginal.TabStop = false;
            // 
            // openGLControl
            // 
            this.openGLControl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.openGLControl.DrawFPS = true;
            this.openGLControl.Location = new System.Drawing.Point(411, 220);
            this.openGLControl.Name = "openGLControl";
            this.openGLControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl.RenderContextType = SharpGL.RenderContextType.FBO;
            this.openGLControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl.Size = new System.Drawing.Size(450, 450);
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
            this.label1.Size = new System.Drawing.Size(179, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Damki gracza zielonego - niebieskie:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.damki_zielone);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(36, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(187, 131);
            this.panel1.TabIndex = 7;
            // 
            // damki_zielone
            // 
            this.damki_zielone.Location = new System.Drawing.Point(0, 16);
            this.damki_zielone.Multiline = true;
            this.damki_zielone.Name = "damki_zielone";
            this.damki_zielone.ReadOnly = true;
            this.damki_zielone.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.damki_zielone.Size = new System.Drawing.Size(182, 112);
            this.damki_zielone.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.damki_czerwone);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(254, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(180, 131);
            this.panel2.TabIndex = 8;
            // 
            // damki_czerwone
            // 
            this.damki_czerwone.Location = new System.Drawing.Point(3, 16);
            this.damki_czerwone.Multiline = true;
            this.damki_czerwone.Name = "damki_czerwone";
            this.damki_czerwone.ReadOnly = true;
            this.damki_czerwone.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.damki_czerwone.Size = new System.Drawing.Size(169, 112);
            this.damki_czerwone.TabIndex = 8;
            this.damki_czerwone.TextChanged += new System.EventHandler(this.damki_czerwone_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(169, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Damki gracza czerwonego - żółte:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.polazielone);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(468, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(180, 131);
            this.panel3.TabIndex = 9;
            // 
            // polazielone
            // 
            this.polazielone.Location = new System.Drawing.Point(0, 16);
            this.polazielone.Multiline = true;
            this.polazielone.Name = "polazielone";
            this.polazielone.ReadOnly = true;
            this.polazielone.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.polazielone.Size = new System.Drawing.Size(174, 112);
            this.polazielone.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Pionki zielone:";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.polaczerwone);
            this.panel4.Controls.Add(this.label_czerwone);
            this.panel4.Location = new System.Drawing.Point(685, 9);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(180, 131);
            this.panel4.TabIndex = 10;
            // 
            // polaczerwone
            // 
            this.polaczerwone.Location = new System.Drawing.Point(3, 19);
            this.polaczerwone.Multiline = true;
            this.polaczerwone.Name = "polaczerwone";
            this.polaczerwone.ReadOnly = true;
            this.polaczerwone.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.polaczerwone.Size = new System.Drawing.Size(173, 109);
            this.polaczerwone.TabIndex = 10;
            // 
            // label_czerwone
            // 
            this.label_czerwone.AutoSize = true;
            this.label_czerwone.Location = new System.Drawing.Point(3, 0);
            this.label_czerwone.Name = "label_czerwone";
            this.label_czerwone.Size = new System.Drawing.Size(88, 13);
            this.label_czerwone.TabIndex = 0;
            this.label_czerwone.Text = "Pionki czerwone:";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.textBox_damki_żółte);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.textBox_stan_ruchy_wykonane);
            this.panel5.Controls.Add(this.textBox_damki_niebieskie);
            this.panel5.Controls.Add(this.textBox_stan_czerwone_pionki);
            this.panel5.Controls.Add(this.textBox_stan_zielone_pionki);
            this.panel5.Controls.Add(this.label10);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Location = new System.Drawing.Point(1023, 12);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(243, 131);
            this.panel5.TabIndex = 9;
            // 
            // textBox_damki_żółte
            // 
            this.textBox_damki_żółte.Location = new System.Drawing.Point(157, 82);
            this.textBox_damki_żółte.Name = "textBox_damki_żółte";
            this.textBox_damki_żółte.ReadOnly = true;
            this.textBox_damki_żółte.Size = new System.Drawing.Size(59, 20);
            this.textBox_damki_żółte.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Liczba damek żółtych:";
            // 
            // textBox_stan_ruchy_wykonane
            // 
            this.textBox_stan_ruchy_wykonane.Location = new System.Drawing.Point(157, 108);
            this.textBox_stan_ruchy_wykonane.Name = "textBox_stan_ruchy_wykonane";
            this.textBox_stan_ruchy_wykonane.ReadOnly = true;
            this.textBox_stan_ruchy_wykonane.Size = new System.Drawing.Size(59, 20);
            this.textBox_stan_ruchy_wykonane.TabIndex = 12;
            // 
            // textBox_damki_niebieskie
            // 
            this.textBox_damki_niebieskie.Location = new System.Drawing.Point(157, 60);
            this.textBox_damki_niebieskie.Name = "textBox_damki_niebieskie";
            this.textBox_damki_niebieskie.ReadOnly = true;
            this.textBox_damki_niebieskie.Size = new System.Drawing.Size(59, 20);
            this.textBox_damki_niebieskie.TabIndex = 11;
            // 
            // textBox_stan_czerwone_pionki
            // 
            this.textBox_stan_czerwone_pionki.Location = new System.Drawing.Point(157, 38);
            this.textBox_stan_czerwone_pionki.Name = "textBox_stan_czerwone_pionki";
            this.textBox_stan_czerwone_pionki.ReadOnly = true;
            this.textBox_stan_czerwone_pionki.Size = new System.Drawing.Size(59, 20);
            this.textBox_stan_czerwone_pionki.TabIndex = 10;
            // 
            // textBox_stan_zielone_pionki
            // 
            this.textBox_stan_zielone_pionki.Location = new System.Drawing.Point(157, 16);
            this.textBox_stan_zielone_pionki.Name = "textBox_stan_zielone_pionki";
            this.textBox_stan_zielone_pionki.ReadOnly = true;
            this.textBox_stan_zielone_pionki.Size = new System.Drawing.Size(59, 20);
            this.textBox_stan_zielone_pionki.TabIndex = 9;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 115);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(142, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Liczba wykonanych ruchów:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 63);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(132, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Liczba damek niebieskich:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(144, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Liczba czerwonych pionków:";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(131, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Liczba zielonych pionków:";
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
            // panel6
            // 
            this.panel6.Controls.Add(this.label11);
            this.panel6.Location = new System.Drawing.Point(916, 220);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(243, 24);
            this.panel6.TabIndex = 11;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(220, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Obserwacja stanu gry w czasie rzeczywistym:";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // button_ruch_przeciwnika
            // 
            this.button_ruch_przeciwnika.Location = new System.Drawing.Point(411, 164);
            this.button_ruch_przeciwnika.Name = "button_ruch_przeciwnika";
            this.button_ruch_przeciwnika.Size = new System.Drawing.Size(108, 38);
            this.button_ruch_przeciwnika.TabIndex = 12;
            this.button_ruch_przeciwnika.Text = "Ruch przeciwnika";
            this.button_ruch_przeciwnika.UseVisualStyleBackColor = true;
            this.button_ruch_przeciwnika.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.label_ruch);
            this.panel7.Location = new System.Drawing.Point(529, 164);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(200, 23);
            this.panel7.TabIndex = 13;
            // 
            // label_ruch
            // 
            this.label_ruch.AutoSize = true;
            this.label_ruch.Location = new System.Drawing.Point(3, 5);
            this.label_ruch.Name = "label_ruch";
            this.label_ruch.Size = new System.Drawing.Size(39, 13);
            this.label_ruch.TabIndex = 0;
            this.label_ruch.Text = "Ruch: ";
            // 
            // button_wykryj_plansze
            // 
            this.button_wykryj_plansze.Location = new System.Drawing.Point(116, 195);
            this.button_wykryj_plansze.Name = "button_wykryj_plansze";
            this.button_wykryj_plansze.Size = new System.Drawing.Size(132, 51);
            this.button_wykryj_plansze.TabIndex = 15;
            this.button_wykryj_plansze.Text = "START - wykryj planszę";
            this.button_wykryj_plansze.UseVisualStyleBackColor = true;
            this.button_wykryj_plansze.Click += new System.EventHandler(this.button_wykryj_plansze_Click);
            // 
            // button_sprawdź
            // 
            this.button_sprawdź.Location = new System.Drawing.Point(116, 252);
            this.button_sprawdź.Name = "button_sprawdź";
            this.button_sprawdź.Size = new System.Drawing.Size(132, 51);
            this.button_sprawdź.TabIndex = 16;
            this.button_sprawdź.Text = "Sprawdź plansze";
            this.button_sprawdź.UseVisualStyleBackColor = true;
            this.button_sprawdź.Click += new System.EventHandler(this.button_sprawdź_Click);
            // 
            // richTextBox_pola
            // 
            this.richTextBox_pola.Location = new System.Drawing.Point(12, 404);
            this.richTextBox_pola.Name = "richTextBox_pola";
            this.richTextBox_pola.Size = new System.Drawing.Size(340, 78);
            this.richTextBox_pola.TabIndex = 17;
            this.richTextBox_pola.TabStop = false;
            this.richTextBox_pola.Text = "";
            // 
            // button_pionki
            // 
            this.button_pionki.Location = new System.Drawing.Point(116, 309);
            this.button_pionki.Name = "button_pionki";
            this.button_pionki.Size = new System.Drawing.Size(132, 51);
            this.button_pionki.TabIndex = 18;
            this.button_pionki.Text = "Wykryj pionki";
            this.button_pionki.UseVisualStyleBackColor = true;
            this.button_pionki.Click += new System.EventHandler(this.button_pionki_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 378);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Pola planszy:";
            // 
            // button_pomoc
            // 
            this.button_pomoc.Location = new System.Drawing.Point(15, 662);
            this.button_pomoc.Name = "button_pomoc";
            this.button_pomoc.Size = new System.Drawing.Size(75, 23);
            this.button_pomoc.TabIndex = 20;
            this.button_pomoc.Text = "Pomoc";
            this.button_pomoc.UseVisualStyleBackColor = true;
            this.button_pomoc.Click += new System.EventHandler(this.button_pomoc_Click);
            // 
            // SharpGLForm
            // 
            this.ClientSize = new System.Drawing.Size(1329, 708);
            this.Controls.Add(this.button_pomoc);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.button_pionki);
            this.Controls.Add(this.richTextBox_pola);
            this.Controls.Add(this.button_sprawdź);
            this.Controls.Add(this.button_wykryj_plansze);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.button_ruch_przeciwnika);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ibOriginal);
            this.Controls.Add(this.openGLControl);
            this.Name = "SharpGLForm";
            ((System.ComponentModel.ISupportInitialize)(this.ibOriginal)).EndInit();
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
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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

            zdjOryginalne = capWebcam.QueryFrame();  // do zdjOryginalne wczytywanie obrazu przechwyconego z kamery 
            if (zdjOryginalne == null)
                return;
            zdjTworzone = zdjOryginalne.InRange(new Bgr(0, 0, 175), new Bgr(100, 100, 256)); //wartość minimalna i max filtru (czerwony ?)

            // InRange sprawdza czy elementy obrazu leżą pomiędza dwoma zmiennymi skalarnymi
            // TColor <byte> {lower higher} - TColor byte
            zdjTworzone = zdjTworzone.SmoothGaussian(9);

            CircleF[] circles = zdjTworzone.HoughCircles(new Gray(100), new Gray(50), 2, zdjTworzone.Height / 4, 10, 400)[0];
            //100 = prog Canny - Canny edge detector - operator wykrywania krawędzi wykorzystujacy alg wielostopniowyw w celu wykrycia szeregu krawędzi 
            //zdjTworzone.Height/4 =  min odległość w pikselach między ośrodkami wykrytych w okręgach
            // min i max promień wykrytego okręgu - koła, z pierwszego kanału
            foreach (CircleF circle in circles)
            {
              /*  if (txtXYZPromien.Text != "")
                    txtXYZPromien.AppendText(Environment.NewLine);
                txtXYZPromien.AppendText("Pozycja pilki : X = " + circle.Center.X.ToString().PadLeft(4) +       // x i y pozycja srodka okregu
                                        " , Y = " + circle.Center.Y.ToString().PadLeft(4) +
                                        " , Promien = " + circle.Radius.ToString("###.000").PadLeft(7));

                txtXYZPromien.ScrollToCaret();// przesunąć pasek przewijania w dół pola tekstowego */ 

                // rysowanie  małego zielonego kółka w środku wykrytego obiektu 
                // rysowanie okręgu o promieniu 3, mimo że wielkość wykrytego koła będzie znacznie większa
                // obiekty CvInvoke mogą zostać wykorzystane do wywołania innych funkcji OpenCV 
                CvInvoke.cvCircle(zdjOryginalne, new Point((int)circle.Center.X, (int)circle.Center.Y), 3, new MCvScalar(0, 255, 0), -1, LINE_TYPE.CV_AA, 0);
                //Grubość koła w pikselach, -1 wskazuje aby wypełnić koło ; AA -wygładzenie pikseli, 0 - bez przesuniecia 
                zdjOryginalne.Draw(circle, new Bgr(Color.Red), 3); // narysowac czerwone kolo wokol wykrytego obiektu 


            }
            ibOriginal.Image = zdjOryginalne;
            //ibProcessed.Image = zdjTworzone;
            //ibProcessed2.Image = zdjTworzone2;
        }

    
        void proces_wykryj_plansze(object sender, EventArgs arg)
        {
            //wykrywanie koordynatow w rogach planszy, wyliczenie srodkow kazdego pola (X, Y)

            zdjOryginalne = capWebcam.QueryFrame();  // do zdjOryginalne wczytywanie obrazu przechwyconego z kamery 
            if (zdjOryginalne == null)
                return;

             
            zdjTworzone = zdjOryginalne.InRange(new Bgr(153, 76, 0), new Bgr(255, 153, 51)); //wartość minimalna i max filtru  -  niebieskie 
            zdjTworzone2 = zdjOryginalne.InRange(new Bgr(0, 0, 255), new Bgr(153,153,255)); // czerwone
            zdjTworzone3 = zdjOryginalne.InRange(new Bgr(0, 153, 76), new Bgr(178, 255, 102)); //wartość minimalna i max filtru - zielone
            
            // InRange sprawdza czy elementy obrazu leżą pomiędza dwoma zmiennymi skalarnymi
            // TColor <byte> {lower higher} - TColor byte
            zdjTworzone = zdjTworzone.SmoothGaussian(9);
            zdjTworzone2 = zdjTworzone2.SmoothGaussian(9);
            zdjTworzone3 = zdjTworzone3.SmoothGaussian(9);

            CircleF[] circles = zdjTworzone.HoughCircles(new Gray(85), new Gray(40), 2, 30, 1, 30)[0]; // n
            CircleF[] circles2 = zdjTworzone2.HoughCircles(new Gray(85), new Gray(40), 2, 30, 1, 30)[0]; // cz
            CircleF[] circles3 = zdjTworzone3.HoughCircles(new Gray(85), new Gray(40), 2, 30, 1, 30)[0]; // z
            //CircleF[] circles = zdjTworzone.HoughCircles(new Gray(85), new Gray(40), 2, zdjTworzone.Height / 4, 10, 400)[0];
            //100 = prog Canny - Canny edge detector - operator wykrywania krawędzi wykorzystujacy alg wielostopniowyw w celu wykrycia szeregu krawędzi 
            //zdjTworzone.Height/4 =  min odległość w pikselach między ośrodkami wykrytych w okręgach
            // min i max promień wykrytego okręgu - koła, z pierwszego kanału
            foreach (CircleF circle in circles)
            {
               /* if (txtXYZPromien.Text != "")
                    txtXYZPromien.AppendText(Environment.NewLine);
                txtXYZPromien.AppendText("Pozycja pilki : X = " + circle.Center.X.ToString().PadLeft(4) +       // x i y pozycja srodka okregu
                                        " , Y = " + circle.Center.Y.ToString().PadLeft(4) +
                                        " , Promien = " + circle.Radius.ToString("###.000").PadLeft(7));

                txtXYZPromien.ScrollToCaret();// przesunąć pasek przewijania w dół pola tekstowego  */

                raz_X = circle.Center.X;
                raz_Y = circle.Center.Y;
                raz_prom = circle.Radius;
                // rysowanie  małego zielonego kółka w środku wykrytego obiektu 
                // rysowanie okręgu o promieniu 3, mimo że wielkość wykrytego koła będzie znacznie większa
                // obiekty CvInvoke mogą zostać wykorzystane do wywołania innych funkcji OpenCV 
                CvInvoke.cvCircle(zdjOryginalne, new Point((int)circle.Center.X, (int)circle.Center.Y), 3, new MCvScalar(0, 255, 0), -1, LINE_TYPE.CV_AA, 0);
                //Grubość koła w pikselach, -1 wskazuje aby wypełnić koło ; AA -wygładzenie pikseli, 0 - bez przesuniecia 
                zdjOryginalne.Draw(circle, new Bgr(Color.Red), 3); // narysowac czerwone kolo wokol wykrytego obiektu 
            }
            foreach (CircleF circle in circles2)
            {
                /*if (txtXYZPromien.Text != "")
                    txtXYZPromien.AppendText(Environment.NewLine);
                txtXYZPromien.AppendText("Pozycja pilki : X = " + circle.Center.X.ToString().PadLeft(4) +       // x i y pozycja srodka okregu
                                        " , Y = " + circle.Center.Y.ToString().PadLeft(4) +
                                        " , Promien = " + circle.Radius.ToString("###.000").PadLeft(7));

                txtXYZPromien.ScrollToCaret();// przesunąć pasek przewijania w dół pola tekstowego */ 

                dwa_X = circle.Center.X;
                dwa_Y = circle.Center.Y;
                dwa_prom = circle.Radius;

                // rysowanie  małego zielonego kółka w środku wykrytego obiektu 
                // rysowanie okręgu o promieniu 3, mimo że wielkość wykrytego koła będzie znacznie większa
                // obiekty CvInvoke mogą zostać wykorzystane do wywołania innych funkcji OpenCV 
                CvInvoke.cvCircle(zdjOryginalne, new Point((int)circle.Center.X, (int)circle.Center.Y), 3, new MCvScalar(0, 255, 0), -1, LINE_TYPE.CV_AA, 0);
                //Grubość koła w pikselach, -1 wskazuje aby wypełnić koło ; AA -wygładzenie pikseli, 0 - bez przesuniecia 
                zdjOryginalne.Draw(circle, new Bgr(Color.Red), 3); // narysowac czerwone kolo wokol wykrytego obiektu 
            }
            foreach (CircleF circle in circles3)
            {
               /* if (txtXYZPromien.Text != "")
                    txtXYZPromien.AppendText(Environment.NewLine);
                txtXYZPromien.AppendText("Pozycja pilki : X = " + circle.Center.X.ToString().PadLeft(4) +       // x i y pozycja srodka okregu
                                        " , Y = " + circle.Center.Y.ToString().PadLeft(4) +
                                        " , Promien = " + circle.Radius.ToString("###.000").PadLeft(7));

                txtXYZPromien.ScrollToCaret();// przesunąć pasek przewijania w dół pola tekstowego  */


               trzy_X = circle.Center.X;
               trzy_Y = circle.Center.Y;
               trzy_prom = circle.Radius;

                // rysowanie  małego zielonego kółka w środku wykrytego obiektu 
                // rysowanie okręgu o promieniu 3, mimo że wielkość wykrytego koła będzie znacznie większa
                // obiekty CvInvoke mogą zostać wykorzystane do wywołania innych funkcji OpenCV 
                CvInvoke.cvCircle(zdjOryginalne, new Point((int)circle.Center.X, (int)circle.Center.Y), 3, new MCvScalar(0, 255, 0), -1, LINE_TYPE.CV_AA, 0);
                //Grubość koła w pikselach, -1 wskazuje aby wypełnić koło ; AA -wygładzenie pikseli, 0 - bez przesuniecia 
                zdjOryginalne.Draw(circle, new Bgr(Color.Red), 3); // narysowac czerwone kolo wokol wykrytego obiektu 

                 
            }

                ibOriginal.Image = zdjOryginalne;
             //   ibProcessed.Image = zdjTworzone;
               // ibProcessed2.Image = zdjTworzone2;

                delay = 1;

            //wyliczenie polozenia srodkow pol    - wylicz()



        }

        void wylicz()
        {
            // wyliczenie srodkow pol szachownicy 

            MessageBox.Show("Sprawdzanie czy wykryto planszę");

            if (raz_X == 0 || dwa_X == 0 || trzy_X == 0)
            {
                MessageBox.Show("Nie wykryto planszy! Wybierz - wykryj plasze!");
            }
            else
            {
                Pole pole = new Pole();

                /*
               // test
                dwa_X = 33;
                dwa_Y = 9;
                dwa_prom = 26;

                raz_X = 45;
                raz_Y = 355;
                raz_prom = 26;

                trzy_X = 383;
                trzy_Y = 341;
                trzy_prom = 16;
                
                //*/
                gorny_lewy_X = dwa_X + dwa_prom;
                gorny_lewy_Y = dwa_Y + dwa_prom;

                dolny_lewy_X = raz_X + raz_prom;
                dolny_lewy_Y = raz_Y - raz_prom;

                gorny_prawy_X = trzy_X - trzy_prom;
                gorny_prawy_Y = trzy_Y + trzy_prom;

                wysokosc = dolny_lewy_Y - gorny_lewy_Y;
                szerokosc = gorny_prawy_X - gorny_lewy_X;


                MessageBox.Show("Wykryto planszę! Można rozpocząc grę!");
                MessageBox.Show("Wysokosc = " + wysokosc.ToString() + ". Szerokosc = " + szerokosc.ToString());


                //rozmiar każdego pola
                rozmiar_pola_szer = szerokosc / 8;
                rozmiar_pola_wys = wysokosc / 8;

                polowa_szer_pola = rozmiar_pola_szer / 2;
                polowa_wys_pola = rozmiar_pola_wys / 2;

               float nowa_polowa_szer = polowa_szer_pola;
               float nowa_polowa_wys = polowa_wys_pola;

               char nazwa_litera = 'A';
               int nazwa_int = 1;

               //srodki pol 
               for (int i = 0; i < 8; i++)
               {
                   //float nowa_polowa = polowa_szer_pola; 
                   for (int j = 0; j < 8; j++)
                   {
                       pola[i, j] = new Pole();
                       pola[i, j].X = polowa_szer_pola + i * rozmiar_pola_szer;
                       pola[i, j].Y = polowa_wys_pola + j * rozmiar_pola_wys;

                       pola[i, j].nazwa += nazwa_litera;
                       pola[i, j].nazwa += nazwa_int;

                       ++nazwa_int;
                   }
                   ++nazwa_litera;
                   nazwa_int = 1;
               }

               /*
                //srodki pol 
                for (int i = 0; i < 8; i++)
                {
                    //float nowa_polowa = polowa_szer_pola; 
                    for(int j=0; j<8;j++)
                    {
                        pola[i, j] = new Pole();
                        pola[i, j].X = polowa_szer_pola + i * rozmiar_pola_szer;
                        pola[i, j].Y = polowa_wys_pola + j * rozmiar_pola_wys;

                        pola[i,j].nazwa += nazwa_litera;
                        pola[i, j].nazwa += nazwa_int;

                        ++nazwa_int;
                        
                        
                    }
                    ++nazwa_litera;
                    nazwa_int = 1;

                   
                     

                    
                }*/




               /* for (int i = 0; i < 8; i++)
                {
                    //float nowa_polowa = polowa_szer_pola; 
                    for (int j = 0; j < 8; j++)
                    {
                        if (j == 0)
                        {
                            pola[i, j].Y = polowa_wys_pola;
                            pola[i, j].nazwa += nazwa_int.ToString();

                        }
                        else
                        {
                            pola[i, j].Y = nowa_polowa_wys;
                            pola[i, j].nazwa += (nazwa_int++).ToString();
                        }
                        nowa_polowa_wys += 2 * polowa_wys_pola;
                    }

                    nowa_polowa_wys = polowa_wys_pola;
                    nazwa_int = 1;

                  
                }
                */

                richTextBox_pola.Clear();
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        richTextBox_pola.AppendText("X: " + pola[i, j].X.ToString());
                        richTextBox_pola.AppendText(" Y: " + pola[i, j].Y.ToString());
                        richTextBox_pola.AppendText(" nazwa: " + pola[i, j].nazwa +"\n");

                         
                    }
                     
                } 


            }

           
          
        }
        
        private void button_wykryj_plansze_Click(object sender, EventArgs e)
        {
             

            //gdy mamy wlasciwy obiekt do przechwycenia
            //dodajemy funkcje obrazu do listy zadań aplikacji

          // wystąienie zdarzenia - pojawienie się przedmiotu przed ramką - wywolanie funkcji  
            Application.Idle += proces_wykryj_plansze;
            przechwytywanie = true;

            MessageBox.Show("Wykrywanie planszy!");

            if (delay == 1)
            {
                if (raz_X == 0.0 || dwa_X == 0.0 || trzy_X == 0.0)
                {
                    MessageBox.Show("Nie wykryto planszy! Proszę poprawic połozenie planszy!");
                    Application.Idle += proces_wykryj_plansze;
                    przechwytywanie = true;
                }
                else 
                {
                    //Application.Idle -= proces_wykryj_plansze;
                   // wylicz();
                }
            }
            else 
            {

              // MessageBox.Show("Ponowne wykrywanie planszy!");
            }


        }
        private void button_sprawdź_Click(object sender, EventArgs e)
        {
            if (przechwytywanie == true)
            {
               // Application.Idle -= procesRamkaIAktualizacjaGUI; // usun f.obrazu w liscie zadan aplikacji 
                Application.Idle -= proces_wykryj_plansze;
                przechwytywanie = false; // zmien znacznik - flage 
                wylicz();

            }
            else
            {
               // Application.Idle += procesRamkaIAktualizacjaGUI;
                Application.Idle += proces_wykryj_plansze;
                przechwytywanie = true;
               wylicz();
            }
        }

         
           ///////////////////////////////////////  openGL      //////////////////////////////////  

    
    
        int znak = 0;
        // rysowanie szachownicy 
        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs e)
        {
            if (znak == 0)
            {
                SharpGL.OpenGL gl = this.openGLControl.OpenGL;
                gl.Enable(OpenGL.GL_TEXTURE_2D); //wlacz teksture
                //  Clear the color and depth buffer.
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
                gl.LoadIdentity();
                gl.Translate(-4.0f, -4.0f, 0.0f);
               // gl.Rotate(rtri, 0.0f, 1.0f, 0.0f);
                texture.Bind(gl);
                //texture_p.Bind(gl);            


                //jest ok 
                /*
                gl.Begin(OpenGL.GL_QUADS); // rysowanie figury (czworokątów)
                gl.Color(255.0f, 255.0f, 255.0f);
                gl.Normal(0.0f, 1.0f, 0.0f); // Normalna wskazująca w dół (niepotrzebna ?)
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
                */

                gl.Begin(OpenGL.GL_QUADS); // rysowanie figury (czworokątów)
                gl.Color(255.0f, 255.0f, 255.0f);
               // gl.Normal(0.0f, 1.0f, 0.0f); // Normalna wskazująca w dół (niepotrzebna ?)
                gl.TexCoord(0.0f, 1.0f);
                gl.Vertex(0.0f, 0.0f, 1.0f);

                // Prawy górny
                gl.TexCoord(1.0f, 1.0f);
                gl.Vertex(8.0f, 0.0f, 1.0f);
                // Lewy górny
                gl.TexCoord(1.0f, 0.0f);
                gl.Vertex(8.0f, 8.0f, 1.0f);
                gl.TexCoord(0.0f, 0.0f);
                gl.Vertex(0.0f, 8.0f, 1.0f);
                // Prawy dolny
                gl.End();

                
          // rysowanie pionkow
                if (pola[0, 0] != null)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j <8; j++)
                        {

                            if (pola[i, j].czerwony == true)
                            {
                                var quadratic = gl.NewQuadric();
                                gl.Color(255.0f, 0, 0);
                                gl.PushMatrix();
                                //texture_p.Bind(gl);
                                //gl.Translate(-1f + (tablica_X_zielone[l]), 1.7f, -3.2f + (tablica_Y_zielone[l]));
                                // gl.Translate( -tablica_X_czerwone[l]/100, 1.7f, -3.2f );
                                gl.Translate(1.2f + i*0.8 , 1.2f+j*0.8  , 2.0f);
                                // gl.Translate( 1.1f, 1.7f, 0.65f); //to??? 28.51F ok ok ok 
                                // gl.Translate((tablica_X_czerwone[l]-300) / (-25.15), 1.7f, 0.65f);
                                // gl.Translate(tablica_X_czerwone[l] - 64.65f, 1.7f, -3.2f);
                                //gl.Translate(1.6f, 1.7f, -3.2f);
                                //gl.Rotate(0.0f, 0.0f, 1.0f, 0.0f);
                                gl.Cylinder(quadratic, 0.30f, 0.30f, 0.20f, 32, 1);
                                gl.Disk(quadratic, 0.10f, 0.30f, 32, 1);
                                gl.PopMatrix();
                                // }
                            }
                            else if (pola[i, j].zielony == true)
                            {

                                var quadratic = gl.NewQuadric();
                                gl.Color(0, 153.0f, 0);
                                gl.PushMatrix();
                                gl.Translate(1.2f + i * 0.8, 1.2f + j * 0.8, 2.0f);
                                gl.Cylinder(quadratic, 0.30f, 0.30f, 0.20f, 32, 1);
                                gl.Disk(quadratic, 0.10f, 0.30f, 32, 1);
                                gl.PopMatrix();
                            }
                            else if (pola[i, j].niebieski == true)
                            {
                                var quadratic = gl.NewQuadric();
                                gl.Color(0, 0, 153.0f);
                                gl.PushMatrix();
                                gl.Translate(1.2f + i * 0.8, 1.2f + j * 0.8, 2.0f);
                                gl.Cylinder(quadratic, 0.30f, 0.30f, 0.20f, 32, 1);
                                gl.Disk(quadratic, 0.10f, 0.30f, 32, 1);
                                gl.PopMatrix();
                            }
                            else if (pola[i, j].zolty == true)
                            {
                                var quadratic = gl.NewQuadric();
                                gl.Color(255.0f, 255.0f, 0);
                                gl.PushMatrix();
                                gl.Translate(1.2f + i * 0.8, 1.2f + j * 0.8, 2.0f);
                                gl.Cylinder(quadratic, 0.30f, 0.30f, 0.20f, 32, 1);
                                gl.Disk(quadratic, 0.10f, 0.30f, 32, 1);
                                gl.PopMatrix();
                            }

                        }
                    }
                }
            }
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
            //gl.Perspective(60.0f, (double)Width / (double)Height, 0.01, 100.0);
            gl.Ortho(-5.0, 5.0, -5.0, 5.0, -5.0, 5.0);
            //  Use the 'look at' helper function to position and aim the camera.
            //gl.LookAt(-5, 5, -5, 0, 0, 0, 0, 1, 0);
            
            //gl.LookAt(0, 15, -15, 0, 0, 0, 0, 5, 0);
           // gl.LookAt(0, 10, 0, 0, 0, 0, 1,0, 0);

            //  Set the modelview matrix.
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }

        /// <summary>
        /// The current rotation.
        /// </summary>
     

        private void openGLControl_Load(object sender, EventArgs e)
        {
          
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        int flaga = 0;
        int liczba_wykonanych_ruchow = 0;

     

        private void button1_Click(object sender, EventArgs e)
        {
            if (flaga == 0)
            {
                label_ruch.BackColor = Color.Green;
                label_ruch.Text = "Ruch: zielone";
                flaga = 1;
                liczba_wykonanych_ruchow++;
                textBox_stan_ruchy_wykonane.Text = liczba_wykonanych_ruchow.ToString();

            }
            else if (flaga == 1)
            {
                label_ruch.BackColor = Color.Red;
                label_ruch.Text = "Ruch: czerwone";
                flaga = 0;
                liczba_wykonanych_ruchow++;
                textBox_stan_ruchy_wykonane.Text = liczba_wykonanych_ruchow.ToString();
            }  
      }

        void proces_wykryj_pionki(object sender, EventArgs arg)
        {

            for (int i = 0; i < 8; i++ )
            {
                for (int j = 0; j < 8; j++)
                {
                    pola[i, j].zielony = false;
                    pola[i, j].czerwony = false;
                    pola[i, j].zolty = false;
                    pola[i, j].niebieski = false;
                }

            }
                znak = 1;
           

            SharpGL.OpenGL gl = this.openGLControl.OpenGL; 
            //wykrywanie pionkow niebieskich i czerownych 2

           
            zdjOryginalne = capWebcam.QueryFrame();  // do zdjOryginalne wczytywanie obrazu przechwyconego z kamery 
            if (zdjOryginalne == null)
                return;


            //zdjTworzone = zdjOryginalne.InRange(new Bgr(153, 76, 0), new Bgr(255, 153, 51)); //wartość minimalna i max filtru  -  niebieskie 
            zdjTworzone2 = zdjOryginalne.InRange(new Bgr(0, 0, 255), new Bgr(153, 153, 255)); // czerwone
            zdjTworzone = zdjOryginalne.InRange(new Bgr(0, 153, 76), new Bgr(178, 255, 102)); //wartość minimalna i max filtru - zielone
            zdjTworzone3 = zdjOryginalne.InRange(new Bgr(153, 76, 0), new Bgr(255, 153, 51)); // niebieskie
            zdjTworzone4 = zdjOryginalne.InRange(new Bgr(255, 255, 0), new Bgr(255, 255, 153)); // zolte

            // InRange sprawdza czy elementy obrazu leżą pomiędza dwoma zmiennymi skalarnymi
            // TColor <byte> {lower higher} - TColor byte
            zdjTworzone = zdjTworzone.SmoothGaussian(9);
            zdjTworzone2 = zdjTworzone2.SmoothGaussian(9);
            zdjTworzone3 = zdjTworzone3.SmoothGaussian(9);
            zdjTworzone4 = zdjTworzone4.SmoothGaussian(9);


            CircleF[] circles = zdjTworzone.HoughCircles(new Gray(85), new Gray(40), 2, 30, 1, 30)[0]; // zielone
            CircleF[] circles2 = zdjTworzone2.HoughCircles(new Gray(85), new Gray(40), 2, 30, 1, 30)[0]; // czerwone
            CircleF[] circles3 = zdjTworzone3.HoughCircles(new Gray(85), new Gray(40), 2, 30, 1, 30)[0]; // niebieskie
            CircleF[] circles4 = zdjTworzone4.HoughCircles(new Gray(85), new Gray(40), 2, 30, 1, 30)[0]; // zolte
            //CircleF[] circles3 = zdjTworzone3.HoughCircles(new Gray(85), new Gray(40), 2, 30, 1, 30)[0]; // 
            //CircleF[] circles = zdjTworzone.HoughCircles(new Gray(85), new Gray(40), 2, zdjTworzone.Height / 4, 10, 400)[0];
            //100 = prog Canny - Canny edge detector - operator wykrywania krawędzi wykorzystujacy alg wielostopniowyw w celu wykrycia szeregu krawędzi 
            //zdjTworzone.Height/4 =  min odległość w pikselach między ośrodkami wykrytych w okręgach
            // min i max promień wykrytego okręgu - koła, z pierwszego kanału
            foreach (CircleF circle in circles) //zielone
            {
                /*if (txtXYZPromien.Text != "")
                    txtXYZPromien.AppendText(Environment.NewLine);
                txtXYZPromien.AppendText("Pozycja pilki : X = " + circle.Center.X.ToString().PadLeft(4) +       // x i y pozycja srodka okregu
                                        " , Y = " + circle.Center.Y.ToString().PadLeft(4) +
                                        " , Promien = " + circle.Radius.ToString("###.000").PadLeft(7));

                txtXYZPromien.ScrollToCaret();// przesunąć pasek przewijania w dół pola tekstowego  */

                zielony_X = circle.Center.X;
                zielony_Y = circle.Center.Y;
                zielony_prom = circle.Radius;
                // rysowanie  małego zielonego kółka w środku wykrytego obiektu 
                // rysowanie okręgu o promieniu 3, mimo że wielkość wykrytego koła będzie znacznie większa
                // obiekty CvInvoke mogą zostać wykorzystane do wywołania innych funkcji OpenCV 
                CvInvoke.cvCircle(zdjOryginalne, new Point((int)circle.Center.X, (int)circle.Center.Y), 3, new MCvScalar(0, 255, 0), -1, LINE_TYPE.CV_AA, 0);
                //Grubość koła w pikselach, -1 wskazuje aby wypełnić koło ; AA -wygładzenie pikseli, 0 - bez przesuniecia 
                zdjOryginalne.Draw(circle, new Bgr(Color.Red), 3); // narysowac czerwone kolo wokol wykrytego obiektu 



                double d2_min = 123456789;
                int i_min = 0;
                int j_min = 0;
               

                for (int i = 0; i < 8; i++)
                {
                        for (int j = 0; j <8; j++)
                        {
                            double dx = (circle.Center.X - pola[i, j].X) - 2*polowa_szer_pola;
                            double dy = (circle.Center.Y - pola[i, j].Y) - 2*polowa_wys_pola;
                            double d2 = dx * dx + dy * dy;

                            if(d2<d2_min)
                            {
                                d2_min = d2;
                                i_min = i;
                                j_min = j;

                            }



                        }
                
                }

                if (d2_min < 5000000000)
                {
 
                    if (j_min == 7)
                    {
                        j_min = 0;
                    }
                    else if (j_min == 6)
                    {
                        j_min = 1;
                    }
                    else if (j_min == 5)
                    {
                        j_min = 2;
                    }
                    else if (j_min == 4)
                    {
                        j_min = 3;
                    }
                    else if (j_min == 3)
                    {
                        j_min = 4;
                    }
                    else if (j_min == 2)
                    {
                        j_min = 5;
                    }
                    else if (j_min == 1)
                    {
                        j_min = 6;
                    }
                    else if (j_min == 0)
                    {
                        j_min = 7;
                    }

                    pola[i_min, j_min].zielony = true;

                }
                else
                { 
                 // blad
                }


                            
            }
            foreach (CircleF circle in circles2)
            {
                /*if (txtXYZPromien.Text != "")
                    txtXYZPromien.AppendText(Environment.NewLine);
                txtXYZPromien.AppendText("Pozycja pilki : X = " + circle.Center.X.ToString().PadLeft(4) +       // x i y pozycja srodka okregu
                                        " , Y = " + circle.Center.Y.ToString().PadLeft(4) +
                                        " , Promien = " + circle.Radius.ToString("###.000").PadLeft(7));

                txtXYZPromien.ScrollToCaret();// przesunąć pasek przewijania w dół pola tekstowego  */

                czerwony_X = circle.Center.X;
                czerwony_Y = circle.Center.Y;
                czerwony_prom = circle.Radius;

                // rysowanie  małego zielonego kółka w środku wykrytego obiektu 
                // rysowanie okręgu o promieniu 3, mimo że wielkość wykrytego koła będzie znacznie większa
                // obiekty CvInvoke mogą zostać wykorzystane do wywołania innych funkcji OpenCV 
                CvInvoke.cvCircle(zdjOryginalne, new Point((int)circle.Center.X, (int)circle.Center.Y), 3, new MCvScalar(0, 255, 0), -1, LINE_TYPE.CV_AA, 0);
                //Grubość koła w pikselach, -1 wskazuje aby wypełnić koło ; AA -wygładzenie pikseli, 0 - bez przesuniecia 
                zdjOryginalne.Draw(circle, new Bgr(Color.Red), 3); // narysowac czerwone kolo wokol wykrytego obiektu 
                
                 double d2_min = 123456789;
                int i_min = 0;
                int j_min = 0;
               

                for (int i = 0; i < 8; i++)
                {
                        for (int j = 0; j <8; j++)
                        {

                            // dx + polowa_szer_pola?
                            double dx = (circle.Center.X - pola[i, j].X) -  2*polowa_szer_pola ;
                            double dy = (circle.Center.Y - pola[i, j].Y) - 2*polowa_wys_pola;
                            double d2 = dx * dx + dy * dy;

                            if(d2<d2_min)
                            {
                                d2_min = d2;
                                i_min = i;
                                j_min = j;

                            }



                        }
                
                }

                if (d2_min < 50000000)
                {
                    if (j_min == 7)
                    {
                        j_min = 0;
                    }
                    else if (j_min == 6)
                    {
                        j_min = 1;
                    }
                    else if (j_min == 5)
                    {
                        j_min = 2;
                    }
                    else if (j_min == 4)
                    {
                        j_min = 3;
                    }
                    else if (j_min == 3)
                    {
                        j_min = 4;
                    }
                    else if (j_min == 2)
                    {
                        j_min = 5;
                    }
                    else if (j_min == 1)
                    {
                        j_min = 6;
                    }
                    else if (j_min == 0)
                    {
                        j_min = 7;
                    }
                     
                    pola[i_min, j_min].czerwony = true;
                    
                }
                else
                {
                //blad
                }
                            
            }
          
            foreach (CircleF circle in circles3) // niebieskie
            {
               /* if (txtXYZPromien.Text != "")
                    txtXYZPromien.AppendText(Environment.NewLine);
                txtXYZPromien.AppendText("Pozycja pilki : X = " + circle.Center.X.ToString().PadLeft(4) +       // x i y pozycja srodka okregu
                                        " , Y = " + circle.Center.Y.ToString().PadLeft(4) +
                                        " , Promien = " + circle.Radius.ToString("###.000").PadLeft(7));

                txtXYZPromien.ScrollToCaret();// przesunąć pasek przewijania w dół pola tekstowego  */

                niebieski_X = circle.Center.X;
                niebieski_Y = circle.Center.Y;
                niebieski_prom = circle.Radius;

                // rysowanie  małego zielonego kółka w środku wykrytego obiektu 
                // rysowanie okręgu o promieniu 3, mimo że wielkość wykrytego koła będzie znacznie większa
                // obiekty CvInvoke mogą zostać wykorzystane do wywołania innych funkcji OpenCV 
                CvInvoke.cvCircle(zdjOryginalne, new Point((int)circle.Center.X, (int)circle.Center.Y), 3, new MCvScalar(0, 255, 0), -1, LINE_TYPE.CV_AA, 0);
                //Grubość koła w pikselach, -1 wskazuje aby wypełnić koło ; AA -wygładzenie pikseli, 0 - bez przesuniecia 
                zdjOryginalne.Draw(circle, new Bgr(Color.Red), 3); // narysowac czerwone kolo wokol wykrytego obiektu 

                double d2_min = 123456789;
                int i_min = 0;
                int j_min = 0;


                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {

                        // dx + polowa_szer_pola?
                        double dx = (circle.Center.X - pola[i, j].X) - 2 * polowa_szer_pola;
                        double dy = (circle.Center.Y - pola[i, j].Y) - 2 * polowa_wys_pola;
                        double d2 = dx * dx + dy * dy;

                        if (d2 < d2_min)
                        {
                            d2_min = d2;
                            i_min = i;
                            j_min = j;

                        }



                    }

                }

                if (d2_min < 50000000)
                {
                    if (j_min == 7)
                    {
                        j_min = 0;
                    }
                    else if (j_min == 6)
                    {
                        j_min = 1;
                    }
                    else if (j_min == 5)
                    {
                        j_min = 2;
                    }
                    else if (j_min == 4)
                    {
                        j_min = 3;
                    }
                    else if (j_min == 3)
                    {
                        j_min = 4;
                    }
                    else if (j_min == 2)
                    {
                        j_min = 5;
                    }
                    else if (j_min == 1)
                    {
                        j_min = 6;
                    }
                    else if (j_min == 0)
                    {
                        j_min = 7;
                    }

                    pola[i_min, j_min].niebieski = true;

                }
                else
                {
                    //blad
                }

            }
            foreach (CircleF circle in circles4) // zolte
            {
               /* if (txtXYZPromien.Text != "")
                    txtXYZPromien.AppendText(Environment.NewLine);
                txtXYZPromien.AppendText("Pozycja pilki : X = " + circle.Center.X.ToString().PadLeft(4) +       // x i y pozycja srodka okregu
                                        " , Y = " + circle.Center.Y.ToString().PadLeft(4) +
                                        " , Promien = " + circle.Radius.ToString("###.000").PadLeft(7));

                txtXYZPromien.ScrollToCaret();// przesunąć pasek przewijania w dół pola tekstowego  */

                zolty_X = circle.Center.X;
                zolty_Y = circle.Center.Y;
                zolty_prom = circle.Radius;

                // rysowanie  małego zielonego kółka w środku wykrytego obiektu 
                // rysowanie okręgu o promieniu 3, mimo że wielkość wykrytego koła będzie znacznie większa
                // obiekty CvInvoke mogą zostać wykorzystane do wywołania innych funkcji OpenCV 
                CvInvoke.cvCircle(zdjOryginalne, new Point((int)circle.Center.X, (int)circle.Center.Y), 3, new MCvScalar(0, 255, 0), -1, LINE_TYPE.CV_AA, 0);
                //Grubość koła w pikselach, -1 wskazuje aby wypełnić koło ; AA -wygładzenie pikseli, 0 - bez przesuniecia 
                zdjOryginalne.Draw(circle, new Bgr(Color.Red), 3); // narysowac czerwone kolo wokol wykrytego obiektu 

                double d2_min = 123456789;
                int i_min = 0;
                int j_min = 0;


                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {

                        // dx + polowa_szer_pola?
                        double dx = (circle.Center.X - pola[i, j].X) - 2 * polowa_szer_pola;
                        double dy = (circle.Center.Y - pola[i, j].Y) - 2 * polowa_wys_pola;
                        double d2 = dx * dx + dy * dy;

                        if (d2 < d2_min)
                        {
                            d2_min = d2;
                            i_min = i;
                            j_min = j;

                        }



                    }

                }

                if (d2_min < 50000000)
                {
                    if (j_min == 7)
                    {
                        j_min = 0;
                    }
                    else if (j_min == 6)
                    {
                        j_min = 1;
                    }
                    else if (j_min == 5)
                    {
                        j_min = 2;
                    }
                    else if (j_min == 4)
                    {
                        j_min = 3;
                    }
                    else if (j_min == 3)
                    {
                        j_min = 4;
                    }
                    else if (j_min == 2)
                    {
                        j_min = 5;
                    }
                    else if (j_min == 1)
                    {
                        j_min = 6;
                    }
                    else if (j_min == 0)
                    {
                        j_min = 7;
                    }

                    pola[i_min, j_min].zolty = true;

                }
                else
                {
                    //blad
                }

            }


            int liczba_zielonych = 0;
            int liczba_czerwonych = 0;
            int liczba_niebieskich = 0;
            int liczba_zoltych = 0;
            polazielone.Clear();
            polaczerwone.Clear();
            damki_czerwone.Clear();
            damki_zielone.Clear();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (pola[i, j].zielony == true)
                    {
                        polazielone.AppendText("pole: " + pola[i, j].nazwa + "\n");
                        liczba_zielonych++;

                    }
                    else if (pola[i, j].czerwony == true)
                    {
                        polaczerwone.AppendText("pole: " + pola[i, j].nazwa + "\n");
                        liczba_czerwonych++;
                    }
                    else if (pola[i, j].niebieski == true)   // gracz z pionkami zielonymi ma damki niebieskie
                    {
                        damki_zielone.AppendText("pole: " + pola[i, j].nazwa + "\n");
                        liczba_niebieskich++;

                    }
                    else if (pola[i, j].zolty == true)    // gracz z pionkami czerwonymi na damki zolte
                    {
                        damki_czerwone.AppendText("pole: " + pola[i, j].nazwa + "\n");
                        liczba_zoltych++;

                    }

                }
            }

            textBox_stan_zielone_pionki.Text = liczba_zielonych.ToString();
            textBox_stan_czerwone_pionki.Text = liczba_czerwonych.ToString();
            textBox_damki_niebieskie.Text = liczba_niebieskich.ToString();
            textBox_damki_żółte.Text = liczba_zoltych.ToString();



                //pola[2, 3].czerwony = true;
                ibOriginal.Image = zdjOryginalne;
           // ibProcessed.Image = zdjTworzone;
           // ibProcessed2.Image = zdjTworzone2;

            znak = 0; 
            //this.openGLControl.OpenGLDraw += new SharpGL.RenderEventHandler(this.openGLControl_OpenGLDraw);
            //przechwytywanie = false;
           //proces_rysuj_pionki_nowa();
          //  czy_wykrywanie_pionkow = false;
        }
        private void button_pionki_Click(object sender, EventArgs e)
        {
           

            int licznik = 0;
            //proces_wykryj_pionki(null, null);

            if (przechwytywanie == true && czy_wykrywanie_pionkow == true   && kolejna == true )
            {
               Application.Idle -= proces_wykryj_pionki;
                przechwytywanie = false; // zmien znacznik - flage 
                                 //czy_wykrywanie_pionkow = false;
                kolejna = false;

            }
            else
            {
                Application.Idle += proces_wykryj_pionki; 
                przechwytywanie = true;
                czy_wykrywanie_pionkow = true;
                kolejna = true;
                licznik++;
            }
            if (licznik > 0)
            {
                 
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void damki_czerwone_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_pomoc_Click(object sender, EventArgs e)
        {
            MessageBox.Show("W celu rozpoczęcia wizualizacji stanu gry należy wybrac - START - wykryj planszę. Następnie, po wykryciu planszy, wybierz opcję - Sprawdź planszę. Wówczas zostaną wyliczone pola planszy oraz nadane im nazwy według współrzednych szachownicy. Plansza jest gotowa. Zatem umieśc pionki na szachownicy i rozpocznij grę, wybierając opcję - Wykryj pionki. W górnej częsci okna wyświetlają się odpowiednio pozycje pionków czerwonych, zielonych oraz damek. Natomiast w górnym prawym rogu można odczytac informacje o stanie gry.");
            
        }
        
    }
}
