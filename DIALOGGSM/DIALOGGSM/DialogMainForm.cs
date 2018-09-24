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
    public partial class DialogMainForm : Form
    {
        public DialogMainForm()
        {
            InitializeComponent();
        }

        private void cloudStartMenuButton1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogCustomerPopUpForm Formxx = new DialogCustomerPopUpForm();
            Formxx.ShowDialog();
        }

        private void dialogTabOne_Click(object sender, EventArgs e)
        {

        }

        private void dialogMainForm_Load(object sender, EventArgs e)
        {

        }

        private void DialogMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

    }
}
