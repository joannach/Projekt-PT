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
   }
}

