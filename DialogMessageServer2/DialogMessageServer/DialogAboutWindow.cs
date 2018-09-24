using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogMessageServer
{
    public partial class DialogAboutWindow : Form
    {


        #region ABOUT

        public DialogAboutWindow()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "32793")
            {
                label1.Visible = true;
                logoPictureBox.Visible = false;
                textBox2.Visible = true;
                textBox2.Enabled = true;
                this.Refresh();
            }
            else
            {
                label1.Visible = false;
                logoPictureBox.Visible = true;
                textBox2.Visible = false;
                textBox2.Enabled = false;
                this.Refresh();
            }

        }

        private void label6_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = "mailto:jewandara@hotmail.com?subject=Dialog Mobile Server&body=";
            proc.Start();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process sInfo = new System.Diagnostics.Process();
            sInfo.StartInfo.FileName = "http://www.iet.edu.lk/";
            sInfo.Start();
        }

        private void label23_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process sInfo = new System.Diagnostics.Process();
            sInfo.StartInfo.FileName = "https://www.dialog.lk/";
            sInfo.Start();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process sInfo = new System.Diagnostics.Process();
            sInfo.StartInfo.FileName = "http://www.iet.edu.lk/";
            sInfo.Start();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBox2.Text == "SKY" || textBox2.Text == "sky" || textBox2.Text == "Sky" || textBox2.Text == "sKY" || textBox2.Text == "SKy" || textBox2.Text == "skY")
            {
                pictureBox2.Enabled = true;
                pictureBox4.Enabled = true;
                pictureBox5.Enabled = true;
                pictureBox5.Enabled = true; 

                pictureBox2.Visible = true;
                pictureBox4.Visible = true;
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
                this.Refresh();
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process sInfo = new System.Diagnostics.Process();
            sInfo.StartInfo.FileName = "https://plus.google.com/103067357527305652236/posts";
            sInfo.Start();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process sInfo = new System.Diagnostics.Process();
            sInfo.StartInfo.FileName = "https://lk.linkedin.com/pub/rachitha-jeewandara/103/6b2/5a7";
            sInfo.Start();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process sInfo = new System.Diagnostics.Process();
            sInfo.StartInfo.FileName = "https://www.freelancer.com/u/jeewandara.html";
            sInfo.Start();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process sInfo = new System.Diagnostics.Process();
            sInfo.StartInfo.FileName = "https://www.elance.com/s/megazoon/";
            sInfo.Start();
        }

        #endregion


    }
}
