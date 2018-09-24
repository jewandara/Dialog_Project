using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dialog.MessageServer;
using System.IO;

namespace DIALOGGSM
{
    public partial class DialogAppKeyForm : Form
    {

        public DialogAppKeyForm()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                labelMessage.Text = "";
                labelMessage2.Text = "";
                labelMessage.Visible = true;
                labelMessage2.Visible = true;
                String KeyData = "@UserID = '" + textBoxUserID.Text + "' , @UserPass = '" + textBoxUserPass.Text + "' , @ProKey = '" + textBoxProKeyID.Text + "'";

                config callServer = new config();
                DataTable dt = callServer.dialogServerInsert("CREATE_APPLICATION", KeyData);

                DataRow dr = dt.Rows[0];
                if (dr["SUCESS"].ToString() == "1")
                {
                    System.Text.RegularExpressions.Regex rgx = new System.Text.RegularExpressions.Regex("@");
                    string result = rgx.Replace(dr["KEYCODE"].ToString(), "Z0I4O0N5X");
                    if (!File.Exists(@"license.zionkey"))
                    {
                        File.Create(@"license.zionkey");
                        TextWriter tw = new StreamWriter(@"license.zionkey");
                        tw.Write(result);
                        tw.Close();
                        MessageBox.Show("Created. Dialog message server application is ready to start. Run the application again.", "Product Key Activated Successful ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        this.Close();
                    }
                    else if (File.Exists(@"license.zionkey"))
                    {
                        using (var stream = new FileStream(@"license.zionkey", FileMode.Truncate))
                        { using (var writer = new StreamWriter(stream)) { writer.Write(""); } }
                        TextWriter tw = new StreamWriter(@"license.zionkey", true);
                        tw.Write(result);
                        tw.Close();
                        MessageBox.Show("Updated. Dialog message server application is ready to start. Run the application again.", "Product Key Activated Successful ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        this.Close();
                    }
                    else 
                    {
                        MessageBox.Show("Dialog message server application is ready to start. Run the application again.", "Product Key Activated Successful ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        this.Close();
                    }
                }
                else
                {
                    labelMessage.ForeColor = Color.Red;
                    labelMessage.Text = dr["MESAGE"].ToString();
                    labelMessage2.Text = dr["MESAGE2"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void textBoxUserID_TextChanged(object sender, EventArgs e)
        {
            labelMessage.Text = "";
            if (textBoxUserID.TextLength == 11)
            {
                labelMessage.Visible = true;
                config callServer = new config();
                DataTable dt = callServer.dialogServerInsert("LOGIN_USER_EXISTS", textBoxUserID.Text);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    if (dr["SUCESS"].ToString() == "1")
                    {
                        labelMessage.ForeColor = Color.Green;
                        labelMessage.Text = textBoxUserID.Text + ", " + dr["MESAGE"].ToString();
                        buttonOK.Enabled = true;
                    }
                    else
                    {
                        labelMessage.ForeColor = Color.Red;
                        labelMessage.Text = textBoxUserID.Text + ", " + dr["MESAGE"].ToString();
                    }
                }
            }
        }

        private void DialogAppKeyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Application.Exit();
        }

        private void textBoxProKeyID_TextChanged(object sender, EventArgs e)
        {
            labelMessage2.Text = "";
            if (textBoxProKeyID.TextLength == 36)
            {
                labelMessage2.Visible = true;
                config callServer = new config();
                DataTable dt = callServer.dialogServerInsert("SEARCH_APPLICATION", textBoxProKeyID.Text);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    if (dr["SUCESS"].ToString() == "1")
                    {
                        labelMessage2.ForeColor = Color.Green;
                        labelMessage2.Text = textBoxProKeyID.Text + ", " + dr["MESAGE"].ToString();
                    }
                    else
                    {
                        labelMessage2.ForeColor = Color.Red;
                        labelMessage2.Text = textBoxProKeyID.Text + ", " + dr["MESAGE"].ToString();
                    }
                }
            }
        }


    }
}
