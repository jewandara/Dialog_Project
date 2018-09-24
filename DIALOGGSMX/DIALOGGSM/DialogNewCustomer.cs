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
    public partial class DialogNewCustomer : Form
    {
        public DialogNewCustomer()
        {
            InitializeComponent();
        }

        private void DialogNewCustomer_Load(object sender, EventArgs e)
        {
            comboBoxCustGender.Items.Insert(0, "MALE");
            comboBoxCustGender.Items.Insert(1, "FEMALE");
            comboBoxCustGender.SelectedIndex = 0;
        }




    }
}
