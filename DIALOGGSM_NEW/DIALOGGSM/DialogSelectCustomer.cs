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
    public partial class DialogSelectCustomer : Form
    {



        #region DEFINE

        private int AID;//APP NUMBER
        private int LID;//USER APP NUMBER
        private String USERNUMBER; //User Phone Number
        private String CUSTNUMBER; //Cust Phone Number
        private String LOGUSERID; //User ID
        private String CUSTOMERTYPE; //User Type
        private String REGION; //User Region
        private config callServer = new config();//class config
        private String savePath;//Excel save path

        #endregion



        #region FUNCTION



        public DialogSelectCustomer(String RE, int SYS_APP_ID, int LOG_APP_ID, String USER_LOGIN_ID, String USER_NUMBER, String CUST_NUMBER)
        {
            InitializeComponent();
            this.Text = "Dialog Customer " + CUST_NUMBER + " - DIALOG MOBILE Customer Complaint";
            REGION = RE;
            AID = SYS_APP_ID;
            LID = LOG_APP_ID;
            LOGUSERID = USER_LOGIN_ID;
            USERNUMBER = USER_NUMBER;
            CUSTNUMBER = CUST_NUMBER;
            labelLog.Text = USERNUMBER + " : SYS_APP_ID - " + SYS_APP_ID.ToString() + " : LOG_APP_ID - " + LOG_APP_ID.ToString() + " : LOGIN SYSTEM";
            comboBoxCustType.Items.Insert(0, "OUT");
            comboBoxCustType.Items.Insert(1, "SYS");
            comboBoxGender.Items.Insert(0, "MALE");
            comboBoxGender.Items.Insert(1, "FEMALE");
            DisplaySelectCustomer(AID, LID, LOGUSERID, USERNUMBER, CUSTNUMBER);
        }

        private void DisplaySelectCustomer(int SYS_APP_ID, int LOG_APP_ID, String USER_LOGIN_ID, String USER_NUMBER, String CUST_NUMBER)
        {
            try
            {
                DataTable _dt_SelectCustomer = callServer.dialogServerInsert("TAB_LOAD_CUSTOMER_SELECT", " @SYSAPPID = " + AID + ", @USERAPPID	= " + LID + ", @USERNUMBER = '" + USERNUMBER + "', @USERLOGID = '" + LOGUSERID + "', @CUSTNUMBER = '" + CUSTNUMBER + "'");
                DataRow dr = _dt_SelectCustomer.Rows[0];
                labelCustNumber.Text = dr["CustNumber"].ToString();
                textBoxCustNumber.Text = dr["CustNumber"].ToString();
                textBoxCustName.Text = dr["CustName"].ToString();
                textBoxCustAddressOne.Text = dr["CustAddresOne"].ToString();
                textBoxCustAddressTwo.Text = dr["CustAddresTwo"].ToString();
                textBoxCustEmail.Text = dr["CustEmail"].ToString();
                textBoxCallingTime.Text = dr["CustCallTime"].ToString();
                textBoxPassWordChangeDate.Text = dr["PassWordChangeDate"].ToString();
                textBoxInsertDate.Text = dr["InsertedDate"].ToString();
                textBoxModifiedDate.Text = dr["ModifiedDate"].ToString();
                textBoxCustID.Text = dr["CustID"].ToString().ToUpper();
                textBoxPassInCorrectCount.Text = dr["FaultPWCount"].ToString();
                CUSTOMERTYPE = dr["CustType"].ToString();

                if (dr["CustGender"].ToString() == "MALE") { comboBoxGender.SelectedIndex = 0; }
                else if (dr["CustGender"].ToString() == "FEMALE") { comboBoxGender.SelectedIndex = 1; }
                else { }

                if (dr["lock"].ToString() == "False") { checkBoxActive.Checked = true; }
                else if (dr["lock"].ToString() == "True") { checkBoxActive.Checked = false; }
                else { }

                if (CUSTOMERTYPE == "OUT")
                {  
                    textBoxCustEmail.ReadOnly = true;
                    textBoxCustEmail.BackColor = Color.LightCyan;
                    textBoxCustName.ReadOnly = true;
                    textBoxCustName.BackColor = Color.LightCyan;
                    textBoxCustAddressOne.ReadOnly = true;
                    textBoxCustAddressOne.BackColor = Color.LightCyan;
                    textBoxCustEmail.ReadOnly = true;
                    textBoxCustEmail.BackColor = Color.LightCyan;
                    comboBoxGender.SelectedIndex = 0;
                    comboBoxCustType.SelectedIndex = 0;
                    checkBoxActive.Enabled = false;
                    buttonDeleteLogin.Enabled = false;
                    buttonDelete.Enabled = false;
                    buttonSelectCustomerSetPassCountDefault.Enabled = false;
                    comboBoxCustType.SelectedIndexChanged += new System.EventHandler(this.comboBoxCustType_SelectedIndexChanged);
                }
                else if (CUSTOMERTYPE == "SYS")
                {
                    comboBoxGender.SelectedIndex = 1; 
                    comboBoxCustType.SelectedIndex = 1; 
                    checkBoxActive.Enabled = true;
                    buttonDeleteLogin.Enabled = true;
                    buttonDelete.Enabled = true;
                    buttonSelectCustomerSetPassCountDefault.Enabled = true;
                    buttonSelectCustomerUpdateName.Click += new System.EventHandler(this.buttonSelectCustomerUpdateName_Click);
                    buttonSelectCustomerUpdateGender.Click += new System.EventHandler(this.buttonSelectCustomerUpdateGender_Click);
                    buttonSelectCustomerUpdateAddress.Click += new System.EventHandler(this.buttonSelectCustomerUpdateAddress_Click);
                    buttonSelectCustomerUpdateEmail.Click += new System.EventHandler(this.buttonSelectCustomerUpdateEmail_Click);
                    buttonSelectCustomerSetPassCountDefault.Click += new System.EventHandler(this.buttonSelectCustomerSetPassCountDefault_Click);
                    buttonDeleteLogin.Click += new System.EventHandler(this.buttonDeleteLogin_Click);
                    buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
                    textBoxCustEmail.TextChanged += new System.EventHandler(this.textBoxCustEmail_TextChanged);
                    comboBoxGender.SelectedIndexChanged += new System.EventHandler(this.comboBoxGender_SelectedIndexChanged);
                    comboBoxCustType.SelectedIndexChanged += new System.EventHandler(this.comboBoxCustType_SelectedIndexChanged);
                    checkBoxActive.CheckedChanged += new System.EventHandler(this.checkBoxActive_CheckedChanged);
                }
                else 
                {
                    comboBoxGender.Enabled = false;
                    comboBoxCustType.Enabled = false;
                    checkBoxActive.Enabled = false;
                    buttonDeleteLogin.Enabled = false;
                    buttonDelete.Enabled = false;
                    buttonSelectCustomerSetPassCountDefault.Enabled = false;
                }
           }
            catch (Exception) { }
        }


        private void comboBoxCustType_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonSelectCustomerChangeType.Enabled = true;
        }

        private void buttonSelectCustomerChangeType_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable _dt_type = callServer.dialogServerInsert("TAB_LOAD_CUSTOMER_UPDATE", " @SYSAPPID = " + AID + ", @USERAPPID	= " + LID + ", @USERNUMBER = '" + USERNUMBER + "', @USERLOGID = '" + LOGUSERID + "', @CUSTNUMBER = '" + CUSTNUMBER + "', @UPDATETYPE = 'TYPE', @UPDATEDATA = '" + comboBoxCustType.Text + "'");
                DataRow dr = _dt_type.Rows[0];
                if (dr["SUCCES"].ToString() == "1") { MessageBox.Show(dr["MESSAGE"].ToString(), "Customer Type Update", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); }
                else { MessageBox.Show(dr["MESSAGE"].ToString(), "Customer Type Update", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            catch (Exception) { MessageBox.Show("Call the administrator.", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Hand); }
            buttonSelectCustomerChangeType.Enabled = false;
        }


        private void textBoxCustName_TextChanged(object sender, EventArgs e)
        {
            if (CUSTOMERTYPE == "SYS")
            {
                if (textBoxCustName.TextLength > 4) { buttonSelectCustomerUpdateName.Enabled = true; labelError4.Text = ""; ; }
                else { buttonSelectCustomerUpdateName.Enabled = false; labelError4.Text = "> 4"; ; }
            }
        }

        private void buttonSelectCustomerUpdateName_Click(object sender, EventArgs e)
        {
            if (textBoxCustName.TextLength > 4)
            {
                try
                {
                    DataTable _dt_name = callServer.dialogServerInsert("TAB_LOAD_CUSTOMER_UPDATE", " @SYSAPPID = " + AID + ", @USERAPPID	= " + LID + ", @USERNUMBER = '" + USERNUMBER + "', @USERLOGID = '" + LOGUSERID + "', @CUSTNUMBER = '" + CUSTNUMBER + "', @UPDATETYPE = 'NAME', @UPDATEDATA = '" + textBoxCustName.Text + "'");
                    DataRow dr = _dt_name.Rows[0];
                    if (dr["SUCCES"].ToString() == "1") { MessageBox.Show(dr["MESSAGE"].ToString(), "Customer Name Update", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); }
                    else { MessageBox.Show(dr["MESSAGE"].ToString(), "Customer Name Update", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
                catch (Exception) { MessageBox.Show("Call the administrator.", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Hand); }
                buttonSelectCustomerUpdateName.Enabled = false;
            }
            else { buttonSelectCustomerUpdateName.Enabled = false; labelError4.Text = "> 4"; ; }
        }


        private void comboBoxGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CUSTOMERTYPE == "SYS") { buttonSelectCustomerUpdateGender.Enabled = true; }
        }

        private void buttonSelectCustomerUpdateGender_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable _dt_gender = callServer.dialogServerInsert("TAB_LOAD_CUSTOMER_UPDATE", " @SYSAPPID = " + AID + ", @USERAPPID	= " + LID + ", @USERNUMBER = '" + USERNUMBER + "', @USERLOGID = '" + LOGUSERID + "', @CUSTNUMBER = '" + CUSTNUMBER + "', @UPDATETYPE = 'GENDER', @UPDATEDATA = '" + comboBoxGender.Text + "'");
                DataRow dr = _dt_gender.Rows[0];
                if (dr["SUCCES"].ToString() == "1") { MessageBox.Show(dr["MESSAGE"].ToString(), "Customer Gender Update", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); }
                else { MessageBox.Show(dr["MESSAGE"].ToString(), "Customer Gender Update", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            catch (Exception) { MessageBox.Show("Call the administrator.", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Hand); }
            buttonSelectCustomerUpdateGender.Enabled = false;
        }


        private void textBoxCustAddressOne_TextChanged(object sender, EventArgs e)
        {
            if (CUSTOMERTYPE == "SYS")
            {
                if (textBoxCustAddressOne.TextLength > 6) { buttonSelectCustomerUpdateAddress.Enabled = true; labelError6.Text = ""; }
                else { buttonSelectCustomerUpdateAddress.Enabled = false; labelError6.Text = "> 6"; }
            }
        }

        private void buttonSelectCustomerUpdateAddress_Click(object sender, EventArgs e)
        {
            if (textBoxCustAddressOne.TextLength > 6)
            {
                try
                {
                    DataTable _dt_address = callServer.dialogServerInsert("TAB_LOAD_CUSTOMER_UPDATE", " @SYSAPPID = " + AID + ", @USERAPPID	= " + LID + ", @USERNUMBER = '" + USERNUMBER + "', @USERLOGID = '" + LOGUSERID + "', @CUSTNUMBER = '" + CUSTNUMBER + "', @UPDATETYPE = 'ADDRESS', @UPDATEDATA = '" + textBoxCustAddressOne.Text + "'");
                    DataRow dr = _dt_address.Rows[0];
                    if (dr["SUCCES"].ToString() == "1") { MessageBox.Show(dr["MESSAGE"].ToString(), "Customer Address Update", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); }
                    else { MessageBox.Show(dr["MESSAGE"].ToString(), "Customer Address Update", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }       
                catch (Exception) { MessageBox.Show("Call the administrator.", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Hand); }
                buttonSelectCustomerUpdateAddress.Enabled = false;
            }
            else { buttonSelectCustomerUpdateAddress.Enabled = false; labelError6.Text = "> 6"; ; }
        }


        private void textBoxCustEmail_TextChanged(object sender, EventArgs e)
        {
            if (CUSTOMERTYPE == "SYS")
            {
                pictureBoxWorning.Visible = false;
                pictureBoxSuccess.Visible = false;
                if (textBoxCustEmail.Text != "")
                {
                    validate callValidate = new validate();
                    if (callValidate.IsValidEmail(textBoxCustEmail.Text))
                    { pictureBoxWorning.Visible = false; buttonSelectCustomerUpdateEmail.Enabled = true; pictureBoxSuccess.Visible = true; }
                    else
                    { pictureBoxWorning.Visible = true; pictureBoxSuccess.Visible = false; }
                }
            }
        }

        private void buttonSelectCustomerUpdateEmail_Click(object sender, EventArgs e)
        {
            validate callValidate = new validate();
            if (callValidate.IsValidEmail(textBoxCustEmail.Text))
            {
                try
                {
                    DataTable _dt_email = callServer.dialogServerInsert("TAB_LOAD_CUSTOMER_UPDATE", " @SYSAPPID = " + AID + ", @USERAPPID	= " + LID + ", @USERNUMBER = '" + USERNUMBER + "', @USERLOGID = '" + LOGUSERID + "', @CUSTNUMBER = '" + CUSTNUMBER + "', @UPDATETYPE = 'EMAIL', @UPDATEDATA = '" + textBoxCustEmail.Text + "'");
                    DataRow dr = _dt_email.Rows[0];
                    if (dr["SUCCES"].ToString() == "1") { MessageBox.Show(dr["MESSAGE"].ToString(), "Email Update", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); }
                    else { MessageBox.Show(dr["MESSAGE"].ToString(), "Email Update", MessageBoxButtons.OK, MessageBoxIcon.Warning); }  
                }       
                catch (Exception) { MessageBox.Show("Call the administrator.", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Hand); }
                buttonSelectCustomerUpdateEmail.Enabled = false; 
                pictureBoxWorning.Visible = false;
                pictureBoxSuccess.Visible = false;
            }
            else
            {
                pictureBoxWorning.Visible = true;
                pictureBoxSuccess.Visible = false;
            }
        }


        private void checkBoxActive_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable _dt_active = null;
                if (checkBoxActive.Checked == true) { _dt_active = callServer.dialogServerInsert("TAB_LOAD_CUSTOMER_ACTIVE", " @SYSAPPID = " + AID + ", @USERAPPID	= " + LID + ", @USERNUMBER = '" + USERNUMBER + "', @USERLOGID = '" + LOGUSERID + "', @CUSTNUMBER = '" + CUSTNUMBER + "', @ACTIVE = 0"); }
                if (checkBoxActive.Checked == false) { _dt_active = callServer.dialogServerInsert("TAB_LOAD_CUSTOMER_ACTIVE", " @SYSAPPID = " + AID + ", @USERAPPID	= " + LID + ", @USERNUMBER = '" + USERNUMBER + "', @USERLOGID = '" + LOGUSERID + "', @CUSTNUMBER = '" + CUSTNUMBER + "', @ACTIVE = 1"); }
                DataRow dr = _dt_active.Rows[0];
                if (dr["SUCCES"].ToString() == "1") { MessageBox.Show(dr["MESSAGE"].ToString(), "Active Customer", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); }
                else { MessageBox.Show(dr["MESSAGE"].ToString(), "Active Customer", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            catch (Exception) { MessageBox.Show("Call the administrator.", "Activate Error", MessageBoxButtons.OK, MessageBoxIcon.Hand); }
        }

        private void buttonSelectCustomerSetPassCountDefault_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable _dt_passcount = callServer.dialogServerInsert("TAB_LOAD_CUSTOMER_PASS_DEFAULT", " @SYSAPPID = " + AID + ", @USERAPPID	= " + LID + ", @USERNUMBER = '" + USERNUMBER + "', @USERLOGID = '" + LOGUSERID + "', @CUSTNUMBER = '" + CUSTNUMBER + "'");
                DataRow dr = _dt_passcount.Rows[0];
                if (dr["SUCCES"].ToString() == "1") { MessageBox.Show(dr["MESSAGE"].ToString(), "Password Count Set Default", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); }
                else { MessageBox.Show(dr["MESSAGE"].ToString(), "Password Count Set Default", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            catch (Exception) { MessageBox.Show("Call the administrator.", "Pass Count Default Error", MessageBoxButtons.OK, MessageBoxIcon.Hand); }
        }



        #endregion



        #region BUTTONS


        private void buttonView_Click(object sender, EventArgs e)
        {
            DialogSelectAllComplaint selectCustComplaintForm = new DialogSelectAllComplaint(REGION, AID, LID, LOGUSERID, USERNUMBER, CUSTNUMBER);
            selectCustComplaintForm.ShowDialog();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                savePath = "";
                if (savePath == "")
                {
                    string dummyFileName = "Customer_" + CUSTNUMBER;
                    SaveFileDialog sf = new SaveFileDialog();
                    sf.FileName = dummyFileName;
                    sf.Filter = "Microsoft Office xls (*.xls*)|*.xls*";
                    if (sf.ShowDialog() == DialogResult.OK) { savePath = sf.FileName + ".xls"; }
                }
                if (!System.IO.File.Exists(savePath))
                {
                    System.IO.File.Copy(@"Reports\customer_report.xls", savePath);
                    if (backgroundWorkerSaveCustomer.IsBusy != true)
                    {
                        groupBoxCustView.Visible = false;
                        panelPro.Visible = true;
                        panelPro.Location = new Point(200, 175);
                        buttonOK.Enabled = false;
                        buttonDeleteLogin.Enabled = false;
                        buttonDelete.Enabled = false;
                        buttonSave.Enabled = false;
                        buttonView.Enabled = false;
                        backgroundWorkerSaveCustomer.RunWorkerAsync();
                    }
                }
                else { System.IO.File.Copy(@"Reports\customer_report.xls", savePath); }
            }
            catch (Exception) { MessageBox.Show("Sorry, We can not create the excel file.\rSelected path already exists.", "Rename The Excel File", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("+" + CUSTNUMBER + "\r  --------------\rAre you sure you want to delete this customer ?\r\r( Customer profile data and all complaints from the customer will be deleted )", "Warning Delete Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    DataTable _dt_delete = callServer.dialogServerInsert("TAB_LOAD_CUSTOMER_DELETE", " @SYSAPPID = " + AID + ", @USERAPPID	= " + LID + ", @USERNUMBER = '" + USERNUMBER + "', @USERLOGID = '" + LOGUSERID + "', @CUSTNUMBER = '" + CUSTNUMBER + "'");
                    DataRow dr = _dt_delete.Rows[0];
                    if (dr["SUCCES"].ToString() == "1") { MessageBox.Show(dr["MESSAGE"].ToString(), "Delete Customer", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); }
                    else { MessageBox.Show(dr["MESSAGE"].ToString(), "Delete Customer", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                    this.Close();
                }
                catch (Exception) { MessageBox.Show("Call the administrator.", "Customer Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Hand); }
            }
        }

        private void buttonDeleteLogin_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("+" + CUSTNUMBER + "\r  --------------\rAre you sure you want to delete customer login ?\r\r( Customer login data and password will be deleted. )", "Warning Delete Customer Login", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    config callServer = new config();
                    DataTable _dt_deletelog = callServer.dialogServerInsert("TAB_LOAD_CUSTOMER_LOGIN_DELETE", " @SYSAPPID = " + AID + ", @USERAPPID	= " + LID + ", @USERNUMBER = '" + USERNUMBER + "', @USERLOGID = '" + LOGUSERID + "', @CUSTNUMBER = '" + CUSTNUMBER + "'");
                    DataRow dr = _dt_deletelog.Rows[0];
                    if (dr["SUCCES"].ToString() == "1") { MessageBox.Show(dr["MESSAGE"].ToString(), "Delete Customer Login", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); }
                    else { MessageBox.Show(dr["MESSAGE"].ToString(), "Delete Customer Login", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                    this.Close();
                }
                catch (Exception) { MessageBox.Show("Call the administrator.", "Customer Delete Login Error", MessageBoxButtons.OK, MessageBoxIcon.Hand); }
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DialogSelectCustomer_Load(object sender, EventArgs e)
        {
        }


        #endregion



        #region BLACKGROUND WORK


        private void backgroundWorkerSaveCustomer_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker worker = sender as BackgroundWorker;
                DataTable _dt_SelectCustomer = callServer.dialogServerInsert("TAB_LOAD_CUSTOMER_SELECT", " @SYSAPPID = " + AID + ", @USERAPPID	= " + LID + ", @USERNUMBER = '" + USERNUMBER + "', @USERLOGID = '" + LOGUSERID + "', @CUSTNUMBER = '" + CUSTNUMBER + "'");
                DataRow dr = _dt_SelectCustomer.Rows[0];
                worker.ReportProgress(1);
                System.Data.OleDb.OleDbConnection MyConnection;
                System.Data.OleDb.OleDbCommand myCommand = new System.Data.OleDb.OleDbCommand();
                MyConnection = new System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + savePath + "';Extended Properties=Excel 8.0;");
                MyConnection.Open();
                myCommand.Connection = MyConnection;
                worker.ReportProgress(2);
                myCommand.CommandText = "INSERT INTO [Dialog$B2:B2] VALUES ('ID : " + dr["CustNumber"].ToString() + "')";
                myCommand.ExecuteNonQuery();
                worker.ReportProgress(3);
                myCommand.CommandText = "INSERT INTO [Dialog$B3:B3] VALUES ('" + DateTime.Now.ToString("HH:mm:ss tt").ToString() + "')";
                myCommand.ExecuteNonQuery();
                worker.ReportProgress(4);
                myCommand.CommandText = "INSERT INTO [Dialog$B28:B28] VALUES ('TAKEN BY : " + REGION + "')";
                myCommand.ExecuteNonQuery();
                worker.ReportProgress(5);
                myCommand.CommandText = "INSERT INTO [Dialog$B29:B29] VALUES ('+" + USERNUMBER + "')";
                myCommand.ExecuteNonQuery();
                worker.ReportProgress(6);
                myCommand.CommandText = "INSERT INTO [Customer$C2:C2] VALUES ('" + dr["CustID"].ToString() + "')";
                myCommand.ExecuteNonQuery();
                worker.ReportProgress(7);
                myCommand.CommandText = "INSERT INTO [Customer$C3:C3] VALUES ('" + dr["CustNumber"].ToString() + "')";
                myCommand.ExecuteNonQuery();
                worker.ReportProgress(8);
                myCommand.CommandText = "INSERT INTO [Customer$C4:C4] VALUES ('" + dr["CustName"].ToString() + "')";
                myCommand.ExecuteNonQuery();
                worker.ReportProgress(9);
                myCommand.CommandText = "INSERT INTO [Customer$C5:C5] VALUES ('" + dr["CustGender"].ToString() + "')";
                myCommand.ExecuteNonQuery();
                worker.ReportProgress(10);
                myCommand.CommandText = "INSERT INTO [Customer$C7:C7] VALUES ('" + dr["CustAddresOne"].ToString() + "')";
                myCommand.ExecuteNonQuery();
                worker.ReportProgress(11);
                myCommand.CommandText = "INSERT INTO [Customer$C8:C8] VALUES ('" + dr["CustAddresTwo"].ToString() + "')";
                myCommand.ExecuteNonQuery();
                worker.ReportProgress(12);
                myCommand.CommandText = "INSERT INTO [Customer$C9:C9] VALUES ('" + dr["CustCallTime"].ToString() + "')";
                myCommand.ExecuteNonQuery();
                worker.ReportProgress(13);
                myCommand.CommandText = "INSERT INTO [Customer$C10:C10] VALUES ('" + dr["CustType"].ToString() + "')";
                myCommand.ExecuteNonQuery();
                worker.ReportProgress(14);
                myCommand.CommandText = "INSERT INTO [Customer$C11:C11] VALUES ('" + dr["PassWordChangeDate"].ToString() + "')";
                myCommand.ExecuteNonQuery();
                worker.ReportProgress(15);
                myCommand.CommandText = "INSERT INTO [Customer$C12:C12] VALUES ('" + dr["InsertedDate"].ToString() + "')";
                myCommand.ExecuteNonQuery();
                worker.ReportProgress(16);
                myCommand.CommandText = "INSERT INTO [Customer$C13:C13] VALUES ('" + dr["ModifiedDate"].ToString() + "')";
                myCommand.ExecuteNonQuery();
                worker.ReportProgress(17);
                myCommand.CommandText = "INSERT INTO [Customer$C21:C21] VALUES ('" + DateTime.Now.ToString("HH:mm:ss tt").ToString() + "')";
                myCommand.ExecuteNonQuery();
                worker.ReportProgress(18);
                myCommand.CommandText = "INSERT INTO [Customer$C23:C23] VALUES ('By: " + REGION + "')";
                myCommand.ExecuteNonQuery();
                worker.ReportProgress(19);
                DataTable _dt_SelectComplaints = callServer.dialogServerInsert("TAB_LOAD_COMPLAINT_BY_CUST", " @SYSAPPID = " + AID + ", @USERAPPID	= " + LID + ", @USERNUMBER = '" + USERNUMBER + "', @USERLOGID = '" + LOGUSERID + "', @CUSTNUMBER = '" + CUSTNUMBER + "'");
                int presentage = 0;
                int i = 0;
                foreach (DataRow Comdr in _dt_SelectComplaints.Rows)
                {
                    i = i + 1;
                    presentage = (i / _dt_SelectComplaints.Rows.Count) * 80;
                    myCommand.CommandText = "INSERT INTO [Complaints$] ([Complaint_ID],[Longitude],[Latitude],[Complaint_Title],[Complaint_Data],[Complaint_Type],[Message_Time],[Inserted_Date]) VALUES ('" + Comdr["CustID"].ToString() + "','" + Comdr["Longitude"].ToString() + "','" + Comdr["Latitude"].ToString() + "','" + Comdr["ComplaintTitle"].ToString() + "','" + Comdr["ComplaintData"].ToString() + "','" + Comdr["ComplaintType"].ToString() + "','" + Comdr["MessageTime"].ToString() + "','" + Comdr["InsertedDate"].ToString() + "')";
                    myCommand.ExecuteNonQuery();
                    if (worker.CancellationPending == true) { e.Cancel = true; break; }
                    else { worker.ReportProgress(20 + presentage); }
                }
                MyConnection.Close();
                worker.ReportProgress(100);
                MessageBox.Show("The Excel file created successfully", "Saving Excel File", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception) { MessageBox.Show("Server Error!"); return; }
        }

        private void backgroundWorkerSaveCustomer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            labelPresentage.Text = "In progress, please wait... " + e.ProgressPercentage.ToString() + "%";
            progressBarSaveCust.Value = e.ProgressPercentage;
        }

        private void backgroundWorkerSaveCustomer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true) { labelPresentage.Text = "Excel Canceled"; }
            else if (e.Error != null) { labelPresentage.Text = "Excel Error: " + e.Error.Message; }
            else { labelPresentage.Text = "Excel Done"; }
            buttonCancel.Visible = false;
            buttonClose.Visible = true;
            buttonSave.Enabled = true;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (backgroundWorkerSaveCustomer.WorkerSupportsCancellation == true)
            { backgroundWorkerSaveCustomer.CancelAsync(); }
            buttonCancel.Visible = false;
            buttonClose.Visible = true;
            buttonSave.Enabled = true;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            groupBoxCustView.Visible = true;
            panelPro.Visible = false;
            panelPro.Location = new Point(240, 550);
            buttonOK.Enabled = true;
            buttonDeleteLogin.Enabled = false;
            buttonDelete.Enabled = true;
            buttonSave.Enabled = true;
            buttonView.Enabled = true;
        }


        #endregion



    }
}