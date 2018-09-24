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
            labelMessage.Text = "";
            labelMessage2.Text = "";
            labelMessage.Visible = true;
            labelMessage2.Visible = true;
            String KeyData = "@UserID = '" + textBoxUserID.Text + "' , @UserPass = '" + textBoxUserPass.Text + "' , @ProKey = '" + textBoxProKeyID.Text + "'";

            config callServer = new config();
            DataTable dt = callServer.dialogServerInsert("CREATE_APPLICATION_KEY", KeyData);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                if (dr["SUCESS"].ToString() == "1")
                {
                    try
                    {
                        using (FileStream stream = new FileStream(@"license.zionkey", FileMode.Create))
                        {
                            using (BinaryWriter writer = new BinaryWriter(stream))
                            {
                                writer.Write(textBoxProKeyID.Text);
                                writer.Write(textBoxUserPass.Text);
                                writer.Close();
                                this.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show (ex.ToString());
                    }
                }
                else
                {
                    labelMessage.ForeColor = Color.Red;
                    labelMessage.Text = dr["MESAGE"].ToString();
                    labelMessage2.Text = dr["MESAGE2"].ToString();
                }
            }
        }




        private void textBoxUserID_TextChanged(object sender, EventArgs e)
        {
            labelMessage.Text = "";
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
                }
                else
                {
                    labelMessage.ForeColor = Color.Red;
                    labelMessage.Text = textBoxUserID.Text + ", " + dr["MESAGE"].ToString();
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
            labelMessage2.Visible = true;
            config callServer = new config();
            DataTable dt = callServer.dialogServerInsert("LOAD_APPLICATION", textBoxProKeyID.Text);
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
