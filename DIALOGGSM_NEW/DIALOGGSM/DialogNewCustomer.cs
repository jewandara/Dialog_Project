using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dialog.MessageServer;

namespace DIALOGGSM
{
    public partial class DialogNewCustomer : Form
    {

        #region DEFINE

        private int AID;//APP NUMBER
        private int LID;//USER APP NUMBER
        private String USERNUMBER; //User Phone Number
        private String LOGUSERID; //User ID
        private String REGION; //User Region

        private Boolean passWordOK;
        private Boolean comPassWordOK;
        private Boolean custName;
        private Boolean custEmail;
        private Boolean custNumber;

        #endregion


        public DialogNewCustomer(String RE, int SYS_APP_ID, int LOG_APP_ID, String USER_LOGIN_ID, String USER_NUMBER)
        {
            InitializeComponent();
            this.Text = "Dialog Customer - DIALOG MOBILE Customer Complaint/NewCustomer";
            REGION = RE;
            AID = SYS_APP_ID;
            LID = LOG_APP_ID;
            LOGUSERID = USER_LOGIN_ID;
            USERNUMBER = USER_NUMBER;
            labelLog.Text = USERNUMBER + " : SYS_APP_ID - " + SYS_APP_ID.ToString() + " : LOG_APP_ID - " + LOG_APP_ID.ToString() + " : LOGIN SYSTEM";
            passWordOK = false;
            comPassWordOK = false;
            custName = false;
            custEmail = false;
            custNumber = false;
        }

        private void DialogNewCustomer_Load(object sender, EventArgs e)
        {
            comboBoxCustGender.Items.Insert(0, "MALE");
            comboBoxCustGender.Items.Insert(1, "FEMALE");
            comboBoxCustGender.SelectedIndex = 0;
        }





        private void textBoxNumber_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int number = Int32.Parse(textBoxNumber.Text);
                labelNumberE.ForeColor = Color.Red;
                labelNumberE.Visible = false;
                if (textBoxNumber.Text.Length == 7)
                {
                    labelNumberE.ForeColor = Color.Green;
                    labelNumberE.Visible = true;
                    labelNumberE.Text = "Valid user id is entered.";
                    custNumber = true;
                }
                else if (textBoxNumber.Text.Length > 7)
                {
                    labelNumberE.ForeColor = Color.Red;
                    labelNumberE.Visible = true;
                    labelNumberE.Text = "Inalid user id is entered. Delete another " + (textBoxNumber.Text.Length - 7).ToString() + " numbers.";
                    return;
                }
                else
                {
                    labelNumberE.ForeColor = Color.Red;
                    labelNumberE.Visible = true;
                    labelNumberE.Text = "Enter another " + (7 - textBoxNumber.Text.Length).ToString() + " more numbers.";
                    return;
                }
            }
            catch (Exception) {
                labelNumberE.ForeColor = Color.Red;
                labelNumberE.Visible = true;
                labelNumberE.Text = "Please enter numbers only.";
                return;
            }
        }

        private void textBoxPassWord_TextChanged(object sender, EventArgs e)
        {
            labelPassWordE.ForeColor = Color.Red;
            labelPassWordE.Visible = false;
            if (textBoxPassWord.Text.Length > 4)
            {
                PasswordScore passwordStrengthScore = PasswordAdvisor.CheckStrength(textBoxPassWord.Text);
                switch (passwordStrengthScore)
                {
                    case PasswordScore.Blank:
                        labelPassWordE.ForeColor = Color.Red;
                        labelPassWordE.Text = "Blank Password.";
                        labelPassWordE.Visible = true;
                        break;
                    case PasswordScore.VeryWeak:
                        labelPassWordE.ForeColor = Color.Red;
                        labelPassWordE.Text = "Very Weak Password.";
                        labelPassWordE.Visible = true;
                        break;
                    case PasswordScore.Weak:
                        labelPassWordE.ForeColor = Color.DarkOrange;
                        labelPassWordE.Text = "Weak Password.";
                        labelPassWordE.Visible = true;
                        passWordOK = true;
                        break;
                    case PasswordScore.Medium:
                        labelPassWordE.ForeColor = Color.LimeGreen;
                        labelPassWordE.Text = "Medium Password.";
                        labelPassWordE.Visible = true;
                        passWordOK = true;
                        break;
                    case PasswordScore.Strong:
                        labelPassWordE.ForeColor = Color.Green;
                        labelPassWordE.Text = "Strong Password.";
                        labelPassWordE.Visible = true;
                        passWordOK = true;
                        break;
                    case PasswordScore.VeryStrong:
                        labelPassWordE.ForeColor = Color.Green;
                        labelPassWordE.Text = "Very Strong Password.";
                        labelPassWordE.Visible = true;
                        passWordOK = true;
                        break;
                }
            }
            else
            {
                labelPassWordE.ForeColor = Color.Red;
                labelPassWordE.Text = "Password must be more than 4 characters.";
                labelPassWordE.Visible = true;
            }
        }

        private void textBoxComPassWord_TextChanged(object sender, EventArgs e)
        {            
            labelComPasswordE.ForeColor = Color.Red;
            labelComPasswordE.Visible = false;
            if (textBoxComPassWord.Text == textBoxPassWord.Text)
            {
                if (passWordOK)
                {
                    labelComPasswordE.ForeColor = Color.Green;
                    labelComPasswordE.Text = "Valied password match.";
                    labelComPasswordE.Visible = true;
                    comPassWordOK = true;
                }
                else
                {
                    labelComPasswordE.ForeColor = Color.Red;
                    labelComPasswordE.Text = "Invalied password match.";
                    labelComPasswordE.Visible = true;
                }
            }
            else
            {
                labelComPasswordE.ForeColor = Color.Red;
                labelComPasswordE.Text = "Comfirm password does not match.";
                labelComPasswordE.Visible = true;
            }
        }
    
        private void textBoxCustName_TextChanged(object sender, EventArgs e)
        {
            labelCustNameE.ForeColor = Color.Red;
            labelCustNameE.Visible = false;
            if (textBoxCustName.Text.Length > 6)
            {
                labelCustNameE.ForeColor = Color.Green;
                labelCustNameE.Text = "Valied Customer Name.";
                labelCustNameE.Visible = true;
                custName = true;
            }
            else
            {
                labelCustNameE.ForeColor = Color.Red;
                labelCustNameE.Text = "Enter more than 6 characters.";
                labelCustNameE.Visible = true;
            }
        }

        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {
            labelEmailE.ForeColor = Color.Red;
            labelEmailE.Visible = false;
            if (textBoxEmail.Text != "")
            {
                validate callValidate = new validate();
                if (callValidate.IsValidEmail(textBoxEmail.Text))
                {
                    labelEmailE.ForeColor = Color.Green;
                    labelEmailE.Text = "Valied Customer Email.";
                    labelEmailE.Visible = true;
                    custEmail = true;
                }
                else
                {
                    labelEmailE.ForeColor = Color.Red;
                    labelEmailE.Text = "Inalied Customer Email.";
                    labelEmailE.Visible = true;
                }
            }
        }

        private void textBoxAddress_TextChanged(object sender, EventArgs e)
        {
            labelAddressE.ForeColor = Color.Red;
            labelAddressE.Visible = false;
            if (textBoxAddress.Text.Length > 10)
            {
                labelAddressE.ForeColor = Color.Green;
                labelAddressE.Text = "Valied Customer Address.";
                labelAddressE.Visible = true;
            }
            else
            {
                labelAddressE.ForeColor = Color.Red;
                labelAddressE.Text = "Enter more than 10 characters.";
                labelAddressE.Visible = true;
            }
        }












    









        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxNumber.Text = "";
            textBoxPassWord.Text = "";
            textBoxComPassWord.Text = "";
            textBoxCustName.Text = "";
            textBoxEmail.Text = "";
            textBoxAddress.Text = "";

            labelNumberE.Text = "User Id can not be an empty string or null.";
            labelPassWordE.Text = "Password can not be an empty string or null.";
            labelComPasswordE.Text = "Confirm password can not be an empty string or null.";
            labelCustNameE.Text = "Customer Name can not be an empty string or null.";
            labelEmailE.Text = "Email can not be an empty string or null.";
            labelAddressE.Text = "";

            labelNumberE.Visible = false;
            labelPassWordE.Visible = false;
            labelComPasswordE.Visible = false;
            labelCustNameE.Visible = false;
            labelEmailE.Visible = false;
            labelAddressE.Visible = false;

            labelPassWordE.ForeColor = Color.Red;
            labelComPasswordE.ForeColor = Color.Red;
            labelCustNameE.ForeColor = Color.Red;
            labelEmailE.ForeColor = Color.Red;
            labelAddressE.ForeColor = Color.Red;

            custNumber = false; 
            passWordOK= false; 
            comPassWordOK = false; 
            custName = false; 
            custEmail= false;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            labelNumberE.Visible = false;
            labelPassWordE.Visible = false;
            labelComPasswordE.Visible = false;
            labelCustNameE.Visible = false;
            labelEmailE.Visible = false;
            labelAddressE.Visible = false;

            if (!custNumber) { labelNumberE.Visible = true; MessageBox.Show("Check customer details again.", "Customer Details Error"); }
            else if (!passWordOK) { labelPassWordE.Visible = true; MessageBox.Show("Check customer details again.", "Customer Details Error"); }
            else if (!comPassWordOK) { labelComPasswordE.Visible = true; MessageBox.Show("Check customer details again.", "Customer Details Error"); }
            else if (!custName) { labelCustNameE.Visible = true; MessageBox.Show("Check customer details again.", "Customer Details Error"); }
            else if (!custEmail) { labelEmailE.Visible = true; MessageBox.Show("Check customer details again.", "Customer Details Error"); }
            else if (custNumber && passWordOK && comPassWordOK && custName && custEmail) 
            { MessageBox.Show("Check customer details again.", "Customer ok"); }
            else { MessageBox.Show("Check customer details again.", "Customer Details Error"); }
        }









    }
}
