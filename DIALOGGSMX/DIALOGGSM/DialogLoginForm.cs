using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Dialog.MessageServer;
using System.IO;

namespace DIALOGGSM
{
    public partial class DialogLoginForm : Form
    {
        
        int x, y;
        System.Drawing.Point Newpoint = new System.Drawing.Point();
        private String parthKEY = "";


        #region MAIN FUNCTIONS


        public DialogLoginForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if( e.Button == System.Windows.Forms.MouseButtons.Left)
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

        private void buttonEnter_MouseHover(object sender, EventArgs e)
        {
            buttonEnter.ForeColor = Color.Black;
        }

        private void buttonEnter_MouseLeave(object sender, EventArgs e)
        {
            buttonEnter.ForeColor = Color.White;
        }

        private void buttonCancel_MouseHover(object sender, EventArgs e)
        {
            buttonCancel.ForeColor = Color.Black;
        }

        private void buttonCancel_MouseLeave(object sender, EventArgs e)
        {
            buttonCancel.ForeColor = Color.White;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DialogLoginForm_Load(object sender, EventArgs e)
        {
            this.Enabled = false;
            readKey();
        }

        private void readKey() 
        {
            try
            {
                String path = @"license.zionkey";
                if (File.Exists(path))
                {
                    using (StreamReader licKey = File.OpenText(path))
                    {
                        parthKEY = licKey.ReadLine();
                    }
                }
                config callServer = new config();
                DataTable dt = callServer.dialogServerInsert("LOAD_APPLICATION", parthKEY);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    if (dr["SUCESS"].ToString() == "1")
                    {
                        labelProKEY.Text = dr["MESAGE"].ToString();
                        this.Enabled = true;
                        return;
                    }
                    else
                    {
                        DialogAppKeyForm keyForm = new DialogAppKeyForm();
                        var result = keyForm.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            readKey();
                        }
                        else 
                        {
                            this.Close();
                        }
                    }
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Can not find license Key." + Environment.NewLine + "Try again.", "Application Error !", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void DialogLoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        
        private void textBoxUserName_MouseEnter(object sender, EventArgs e)
        {
            if (textBoxUserName.Text == "USER NAME")
            {
                textBoxUserName.Text = "";
            }
        }

        private void textBoxPassWord_MouseEnter(object sender, EventArgs e)
        {
            if (textBoxPassWord.Text == "PASS WORD")
            {
                textBoxPassWord.Text = "";
            }
        }

        private void textBoxPassWord_MouseLeave(object sender, EventArgs e)
        {
            if (textBoxPassWord.Text == "")
            {
                textBoxPassWord.Text = "PASS WORD";
            }
        }

        private void textBoxUserName_MouseLeave(object sender, EventArgs e)
        {
            if (textBoxUserName.Text == "")
            {
                textBoxUserName.Text = "USER NAME";
            }

        }

        #endregion


        private void button1_Click(object sender, EventArgs e)
        {
            String UserID = textBoxUserName.Text;
            labelMessage.Text = "";
            labelMessage2.Text = "";
            labelMessage.Visible = true;
            labelMessage2.Visible = true;
            labelMessage.ForeColor = Color.White;
            labelMessage2.ForeColor = Color.White;
            String KeyData = "@UserID = '" + UserID + "' , @UserPass = '" + textBoxPassWord.Text + "' , @ProKey = '" + parthKEY + "'";

            config callServer = new config();
            DataTable dt = callServer.dialogServerInsert("CREATE_APPLICATION_KEY", KeyData);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                if (dr["SUCESS"].ToString() == "1")
                {
                    try
                    {
                        this.Hide();
                        this.Visible = false;
                        DialogMainForm MainForm = new DialogMainForm(parthKEY, UserID);
                        MainForm.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                else
                {
                    labelMessage.Text = dr["MESAGE"].ToString();
                    labelMessage2.Text = dr["MESAGE2"].ToString();
                }
            }
        }


        private void textBoxUserName_TextChanged(object sender, EventArgs e)
        {
            labelMessage.Text = "";
            labelMessage.Visible = true;
            labelMessage.ForeColor = Color.White;
            if (textBoxUserName.Text == "USER NAME" || textBoxUserName.Text == "") { }
            else
            {
                config callServer = new config();
                DataTable dt = callServer.dialogServerInsert("LOGIN_USER_EXISTS", textBoxUserName.Text);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    if (dr["SUCESS"].ToString() == "1")
                    {
                        labelMessage.ForeColor = Color.Green;
                        labelMessage.Text = textBoxUserName.Text + ", " + dr["MESAGE"].ToString();
                    }
                    else
                    {
                        labelMessage.ForeColor = Color.White;
                        labelMessage.Text = textBoxUserName.Text + ", " + dr["MESAGE"].ToString();
                    }
                }
            }
        }


    }
}
