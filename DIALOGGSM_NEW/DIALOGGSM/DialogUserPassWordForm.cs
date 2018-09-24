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
    public partial class DialogUserPassWordForm : Form
    {

        public Boolean returnOK;

        public DialogUserPassWordForm(String USERNUMBER)
        {
            InitializeComponent();
            labelUserNumber.Text = USERNUMBER;
            returnOK = false;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            returnOK = true;
        }

        private void textBoxPass_TextChanged(object sender, EventArgs e)
        {
           // //if (textBoxPass.Text == "   Your Pass Word")
           // //{
           //    textBoxPass.Text = "";
           // //    textBoxPass.PasswordChar = '\0';
           // //    textBoxPass.ForeColor = Color.DimGray;
           // //}
           //// else 
           // if (textBoxPass.Text == "")
           // {
           //     textBoxPass.Text = "   Your Pass Word";
           //     textBoxPass.PasswordChar = '\0';
           //     textBoxPass.ForeColor = Color.DimGray;
           // }
           // else
           // {
           //     textBoxPass.PasswordChar = '*';
           //     textBoxPass.ForeColor = Color.Black;
           // }
        }



    }
}
