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
       private System.Windows.Forms.TextBox textBox_mozliwe_ruchy;
       private System.Windows.Forms.Panel panel2;
       private System.Windows.Forms.TextBox textBox_mozliwe_bicia;
       private System.Windows.Forms.Label label2;
       private System.Windows.Forms.Panel panel3;
       private System.Windows.Forms.Label label3;
       private System.Windows.Forms.Panel panel4;
       private System.Windows.Forms.Label label4;
       private System.Windows.Forms.Panel panel5;
       private System.Windows.Forms.Label label8;
       private System.Windows.Forms.Label label7;
       private System.Windows.Forms.Label label6;
       private System.Windows.Forms.Label label5;
       private System.Windows.Forms.TextBox textBox_stan_bicia_mozliwe;
       private System.Windows.Forms.TextBox textBox_stan_ruchy_wykonane;
       private System.Windows.Forms.TextBox textBox_stan_ruchy;
       private System.Windows.Forms.TextBox textBox_stan_biale_pionki;
       private System.Windows.Forms.TextBox textBox_stan_czarne_pionki;
       private System.Windows.Forms.Label label10;
       private System.Windows.Forms.Label label9;
       private System.Windows.Forms.Panel panel6;
       private System.Windows.Forms.Label label11;
   }
}

