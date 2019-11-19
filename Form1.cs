using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace ImagineCup
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public Form1()
        {
            InitializeComponent();

        }
        private void metroButton1_Click(object sender, EventArgs e)
        {
            String image_file = string.Empty;
            String image_frompy = string.Empty;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = @"D:\";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image_file = dialog.FileName;


                var engine = Python.CreateEngine();

                //ScriptSource script = engine.CreateScriptSourceFromFile("C:\\Users\\User\\Desktop\\ImagineCup\\test.py");
                ScriptSource script = engine.CreateScriptSourceFromFile("C:\\Users\\User\\Desktop\\ImagineCup\\Mask_RCNN-20191112T075222Z-001\\Mask_RCNN\\samples\\demo.py");
                ScriptScope scope = engine.CreateScope();

                script.Execute(scope);

                //dynamic w = scope.GetVariable("Enter")();
                //image_frompy = w.enter(image_file) as string;

            }
            else if (dialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            //pictureBox.Image = Bitmap.FromFile(image_frompy);
            pictureBox.Image = Bitmap.FromFile(image_file);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            //pictureBox1.Image = Bitmap.FromFile(image_frompy);
            pictureBox1.Image = Bitmap.FromFile(image_file);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {

        }
        
        private void HelpButton_MouseHover(object sender, EventArgs e)
        {
            helpButton.BackColor = Color.transparent;

        }
    }
}
