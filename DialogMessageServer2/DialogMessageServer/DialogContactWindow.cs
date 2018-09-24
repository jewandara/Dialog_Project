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
    public partial class DialogContactWindow : Form
    {
        public DialogContactWindow()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonCANCEL_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
