using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DIALOGGSM
{
    public partial class DialogWaitForm : Form
    {
        int x, y;
        System.Drawing.Point Newpoint = new System.Drawing.Point();

        public DialogWaitForm()
        {
            InitializeComponent();
        }

        public string Presentage
        {
            set { labelPresentage.Text = value; }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
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


    }
}
