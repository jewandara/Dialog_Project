using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GialogComplaintSMSSever
{
    public partial class DialogNewSMSNotifiWindow : Form
    {         
        public DialogNewSMSNotifiWindow()
        {
            InitializeComponent();
        }

        private void DialogNewSMSNotifiWindow_Load(object sender, EventArgs e)
        {
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            this.Left = screenWidth - this.Width;
            this.Top = screenHeight - this.Height;
            //formFadingAnimate();
            //formFadingOutAnimate();


            //timer1.Enabled = true;

           // string link = "http://www.geocities.com/basuabhinaba";
            //linkLabel1.Links.Add(0, link.Length, link);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //this.Hide();
        }

        private void DialogNewSMSNotifiWindow_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();

        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }


        private void formFadingAnimate()
        {
            int steps = 100;
            Timer timer = new Timer();

            timer.Interval = 10;

            int currentStep = 0;
            timer.Tick += (arg1, arg2) =>
            {
                Opacity = ((double)currentStep) / steps;
                currentStep++;

                if (currentStep >= steps)
                {
                    timer.Stop();
                    timer.Dispose();
                }
            };

            timer.Start();
        }




        private void formFadingOutAnimate()
        {
            int duration = 2000;  //in milliseconds
            int steps = 100;
            Timer timer = new Timer();
            timer.Interval = duration / steps;

            int currentStep = 100;
            timer.Tick += (arg1, arg2) =>
            {
                Opacity = ((double)currentStep) / steps;
                currentStep--;

                if (currentStep >= steps)
                {
                    timer.Stop();
                    timer.Dispose();
                }
            };

            timer.Start();
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }



    }
}
