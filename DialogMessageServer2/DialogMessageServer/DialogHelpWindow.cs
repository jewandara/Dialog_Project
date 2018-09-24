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
    public partial class DialogHelpWindow : Form
    {
        public DialogHelpWindow(String TabName)
        {
            InitializeComponent();
            if (TabName == "dialogTabOne")
            {
                dialogTabApplication.SelectedIndex = 0; 
                HelpLabel.Text = "Server Help";
            }
            else if (TabName == "dialogTabTwo")
            {
                dialogTabApplication.SelectedIndex = 1; 
                HelpLabel.Text = "Program Help";
            }
            else { HelpLabel.Text = "Server Help"; }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DialogHelpWindow_Load(object sender, EventArgs e)
        {
            callTab();
        }

        private void dialogTabApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
            callTab();
        }

        private void callTab()
        {
            if (dialogTabApplication.SelectedTab == dialogTabApplication.TabPages["dialogTabOne"])
            {
                HelpLabel.Text = "Server Help";
            }
            else if (dialogTabApplication.SelectedTab == dialogTabApplication.TabPages["dialogTabTwo"])
            {
                HelpLabel.Text = "Program Help";
            }
            else { HelpLabel.Text = "Server Help"; }
        }

    }
}
