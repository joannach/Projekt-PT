namespace SharpGLWinformsApplication_warcaby
{
   partial class SharpGLForm
   {
       private System.ComponentModel.IContainer components;
       private Emgu.CV.UI.ImageBox ibOriginal;
       private Emgu.CV.UI.ImageBox ibProcessed;
       private System.Windows.Forms.Button btnPausseorResume;
       private System.Windows.Forms.TextBox txtXYZPromien;
       private System.Windows.Forms.Button btn_wlacz;


        
       protected override void Dispose(bool disposing)
       {
           if (disposing && (components != null))
           {
               components.Dispose();
           }
           base.Dispose(disposing);
       }


       
       private SharpGL.OpenGLControl openGLControl;
       private System.Windows.Forms.Label label1;
       private System.Windows.Forms.Panel panel1;
       private System.Windows.Forms.TextBox ruchy;
       private System.Windows.Forms.Panel panel2;
       private System.Windows.Forms.TextBox bicia;
       private System.Windows.Forms.Label label2;
       private System.Windows.Forms.Panel panel3;
       private System.Windows.Forms.Label label3;
       private System.Windows.Forms.Panel panel4;
       private System.Windows.Forms.Label label_czerwone;
       private System.Windows.Forms.Panel panel5;
       private System.Windows.Forms.Label label8;
       private System.Windows.Forms.Label label7;
       private System.Windows.Forms.Label label6;
       private System.Windows.Forms.Label label5;
       private System.Windows.Forms.TextBox textBox_stan_bicia_mozliwe;
       private System.Windows.Forms.TextBox textBox_stan_ruchy_wykonane;
       private System.Windows.Forms.TextBox textBox_stan_ruchy;
       private System.Windows.Forms.TextBox textBox_stan_czerwone_pionki;
       private System.Windows.Forms.TextBox textBox_stan_zielone_pionki;
       private System.Windows.Forms.Label label10;
       private System.Windows.Forms.Label label9;
       private System.Windows.Forms.Panel panel6;
       private System.Windows.Forms.Label label11;
       private System.Windows.Forms.TextBox polazielone;
       private System.Windows.Forms.TextBox polaczerwone;
       private System.Windows.Forms.Button button_ruch_przeciwnika;
       private System.Windows.Forms.Panel panel7;
       private System.Windows.Forms.Label label_ruch;
       private Emgu.CV.UI.ImageBox ibProcessed2;
       private System.Windows.Forms.Button button_wykryj_plansze;
       private System.Windows.Forms.Button button_sprawdź;
       private System.Windows.Forms.RichTextBox richTextBox_pola;
       private System.Windows.Forms.Button button_pionki;
   }
}

