using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace DIALOGGSM
{
    public partial class DialogLoginForm : Form
    {
        int x, y;
        System.Drawing.Point Newpoint = new System.Drawing.Point();
        public void splash()
        {
            Application.Run(new DialogSplashForm());
        }

        public DialogLoginForm()
        {
            Thread t = new Thread(new ThreadStart(splash));
            t.Start();
            Thread.Sleep(2000);
            InitializeComponent();
            t.Abort();
        }
        


        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if( e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Newpoint = Control.MousePosition;
                Newpoint.X -= (x);
                Newpoint.Y -= (y);
                this.Location = Newpoint;
            }

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            x = Control.MousePosition.X - this.Location.X;
            y = Control.MousePosition.Y - this.Location.Y;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Application.Run(new DialogMainForm());
            //this.Close();
            //Application.Run(new DialogMainForm());
            this.Visible = false;
            DialogMainForm MainForm = new DialogMainForm();
            MainForm.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
