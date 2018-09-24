using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dialog.MessageServer;
using System.Threading;

namespace DIALOGGSM
{
    public partial class DialogMainForm : Form
    {


        #region DEFINE

        private int AID;//APP NUMBER
        private int LID;//USER APP NUMBER
        private String USERNUMBER; //User Phone Number
        private String LOGUSERID; //User ID
        private String REGION; //User Region
        private config callServer = new config();

        #endregion



        #region START

        public DialogMainForm(int SYS_APP_ID, int LOG_APP_ID, String USER_LOGIN_ID, String USER_NUMBER)
        {
            try
            {
                InitializeComponent();
                AID = SYS_APP_ID;
                LID = LOG_APP_ID;
                LOGUSERID = USER_LOGIN_ID;
                USERNUMBER = USER_NUMBER;
                listViewDialogCustomer.Items.Clear();
                DataTable dt = callServer.dialogServerInsert("FORM_LOAD", " @SYSAPPID = " + AID + ", @USERAPPID	= " + LID + ", @USERNUMBER = '" + USERNUMBER + "', @USERLOGID = '" + LOGUSERID + "'");
                DataRow dr = dt.Rows[0];
                REGION = dr["REGION"].ToString();
                textBoxApplicationData.Text = REGION + "  |  " + dr["MESAGE"].ToString();
                labelLog.Text = USERNUMBER + " : SYS_APP_ID - " + SYS_APP_ID.ToString() + " : LOG_APP_ID - " + LOG_APP_ID.ToString() + " : LOGIN SYSTEM";
            }
            catch (Exception) { MessageBox.Show("Call the administrator.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Hand); } 
        }

        private void DialogMainForm_Load(object sender, EventArgs e)
        {

            //DisplayNETCustomerTab();
            //this.Refresh();
            //DisplaySMSComplaintTab();
            //this.Refresh();
        }

        private void DialogMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        #endregion







        #region TAB Customer


        private void DisplayCustomerTab(String CUST_TYPE)
        {
            try
            {
                listViewDialogCustomer.Items.Clear();
                DataTable dt = callServer.dialogServerInsert("TAB_LOAD_CUSTOMER", " @SYSAPPID = " + AID + ", @USERAPPID	= " + LID + ", @USERNUMBER = '" + USERNUMBER + "', @USERLOGID = '" + LOGUSERID + "', @CUSTTYPE = '" + CUST_TYPE + "'");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    ListViewItem listitem = new ListViewItem(dr["CustNumber"].ToString());
                    listitem.SubItems.Add(dr["CustName"].ToString());
                    listitem.SubItems.Add(dr["CustType"].ToString());
                    listitem.SubItems.Add(dr["CustEmail"].ToString());
                    listitem.SubItems.Add(dr["CustAddresOne"].ToString());
                    listitem.SubItems.Add(dr["CustCallTime"].ToString());
                    listitem.SubItems.Add(dr["ModifiedDate"].ToString());
                    listViewDialogCustomer.Items.Add(listitem);
                }
            }
            catch (Exception) { }
        }

        private void buttonCustomerRefreshNewCustomer_Click(object sender, EventArgs e)
        {
            DisplayCustomerTab("OUT");
        }

        private void buttonCustomerRefreshNetCustomer_Click(object sender, EventArgs e)
        {
            DisplayCustomerTab("SYS");
        }

        private void textBoxCustomerSearch_TextChanged(object sender, EventArgs e)
        {
            if (textBoxCustomerSearch.Text != "")
            {
                try
                {
                    listViewDialogCustomer.Items.Clear();
                    DataTable _dt_customer = null;
                    if (radioButtonCustomerSearchByCustNumber.Checked == true)
                    { _dt_customer = callServer.dialogServerInsert("TAB_LOAD_CUSTOMER_SEARCH", " @SYSAPPID = " + AID + ", @USERAPPID = " + LID + ", @USERNUMBER = '" + USERNUMBER + "', @USERLOGID = '" + LOGUSERID +  "', @TEXT = '" + textBoxCustomerSearch.Text + "', @TYPE = 'BY_NUMBER'"); }
                    if (radioButtonCustomerSearchByCustName.Checked == true)
                    { _dt_customer = callServer.dialogServerInsert("TAB_LOAD_CUSTOMER_SEARCH", " @SYSAPPID = " + AID + ", @USERAPPID	= " + LID + ", @USERNUMBER = '" + USERNUMBER + "', @USERLOGID = '" + LOGUSERID + "', @TEXT = '" + textBoxCustomerSearch.Text + "', @TYPE = 'BY_NAME'"); }
                    if (radioButtonCustomerSearchByEmail.Checked == true)
                    { _dt_customer = callServer.dialogServerInsert("TAB_LOAD_CUSTOMER_SEARCH", " @SYSAPPID = " + AID + ", @USERAPPID	= " + LID + ", @USERNUMBER = '" + USERNUMBER + "', @USERLOGID = '" + LOGUSERID + "', @TEXT = '" + textBoxCustomerSearch.Text + "', @TYPE = 'BY_EMAIL'"); }

                    if (_dt_customer != null)
                    {
                        for (int i = 0; i < _dt_customer.Rows.Count; i++)
                        {
                            DataRow dr = _dt_customer.Rows[i];
                            ListViewItem listitem = new ListViewItem(dr["CustNumber"].ToString());
                            listitem.SubItems.Add(dr["CustName"].ToString());
                            listitem.SubItems.Add(dr["CustType"].ToString());
                            listitem.SubItems.Add(dr["CustEmail"].ToString());
                            listitem.SubItems.Add(dr["CustAddresOne"].ToString());
                            listitem.SubItems.Add(dr["CustCallTime"].ToString());
                            listitem.SubItems.Add(dr["ModifiedDate"].ToString());
                            listViewDialogCustomer.Items.Add(listitem);
                        }
                    }
                }
                catch (Exception) { }
            }
        }

        private void listViewDialogCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonCustomerViewSelectCustomer.Enabled = true;
            buttonCustomerViewSelectComplaint.Enabled = true;
        }

        private void buttonCustomerViewSelectCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                String CUSTNUMBER = listViewDialogCustomer.Items[listViewDialogCustomer.SelectedIndices[0]].Text;
                DialogSelectCustomer selectCustForm = new DialogSelectCustomer(REGION, AID, LID, LOGUSERID, USERNUMBER, CUSTNUMBER);
                selectCustForm.Show();
            }
            catch (Exception) { buttonCustomerViewSelectCustomer.Enabled = false; }
        }

        private void listViewDialogCustomer_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                String CUSTNUMBER = listViewDialogCustomer.Items[listViewDialogCustomer.SelectedIndices[0]].Text;
                DialogSelectCustomer selectCustForm = new DialogSelectCustomer(REGION, AID, LID, LOGUSERID, USERNUMBER, CUSTNUMBER);
                selectCustForm.Show();
            }
            catch (Exception) { buttonCustomerViewSelectCustomer.Enabled = false; }
        }

        private void buttonCustomerViewSelectComplaint_Click(object sender, EventArgs e)
        {
            try
            {
                String CUSTNUMBER = listViewDialogCustomer.Items[listViewDialogCustomer.SelectedIndices[0]].Text;
                DialogSelectAllComplaint selectCustComplaintForm = new DialogSelectAllComplaint(REGION, AID, LID, LOGUSERID, USERNUMBER, CUSTNUMBER);
                selectCustComplaintForm.ShowDialog();
            }
            catch (Exception) { buttonCustomerViewSelectComplaint.Enabled = false; }
        }

        private void buttonCustomerSaveInExcel_Click(object sender, EventArgs e)
        {
            DialogSaveExcelCustomer openExcel = new DialogSaveExcelCustomer(REGION, AID, LID, LOGUSERID, USERNUMBER);
            openExcel.ShowDialog();
        }

        private void buttonCustomerAddNewCustomer_Click(object sender, EventArgs e)
        {
            DialogNewCustomer newCustForm = new DialogNewCustomer(REGION, AID, LID, LOGUSERID, USERNUMBER);
            newCustForm.ShowDialog();
        } 


        #endregion



        #region TAB SMS Complaint


        private void DisplayComplaintTab(String COMP_TYPE)
        {
            try
            {
                listViewDialogSMSComplaint.Items.Clear();
                DataTable _dt_SMSComplaint = callServer.dialogServerInsert("TAB_LOAD_COMPLAINT", " @SYSAPPID = " + AID + ", @USERAPPID	= " + LID + ", @USERNUMBER = '" + USERNUMBER + "', @USERLOGID = '" + LOGUSERID + "', @COMPTYPE = '" + COMP_TYPE + "'");
                for (int i = 0; i < _dt_SMSComplaint.Rows.Count; i++)
                {
                    DataRow dr = _dt_SMSComplaint.Rows[i];
                    ListViewItem listitem = new ListViewItem(dr["ComplaintID"].ToString());
                    if (dr["ComplaintView"].ToString() == "False") { listitem.Font = new Font(listViewDialogSMSComplaint.Font, FontStyle.Bold); }
                    listitem.SubItems.Add(dr["CustNumber"].ToString());
                    listitem.SubItems.Add(dr["CustName"].ToString());
                    listitem.SubItems.Add(dr["Longitude"].ToString());
                    listitem.SubItems.Add(dr["Latitude"].ToString());
                    listitem.SubItems.Add(dr["ComplaintTitle"].ToString());
                    listitem.SubItems.Add(dr["ComplaintData"].ToString());
                    listitem.SubItems.Add(dr["MessageTime"].ToString());
                    listitem.SubItems.Add(dr["InsertedDate"].ToString());
                    listViewDialogSMSComplaint.Items.Add(listitem);
                }
            }
            catch (Exception) { MessageBox.Show("Call the administrator.", "Load Cpmplaint Error", MessageBoxButtons.OK, MessageBoxIcon.Hand); }
        }

        private void buttonSMSComplaintRefreshNewComplaint_Click(object sender, EventArgs e)
        {
            DisplayComplaintTab("SMS");
        }

        private void listViewDialogSMSComplaint_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonSMSComplaintViewSelectSMSComplaint.Enabled = true;
        }

        private void buttonSMSComplaintViewSelectSMSComplaint_Click(object sender, EventArgs e)
        {
            try
            {
                String COMPID = listViewDialogSMSComplaint.Items[listViewDialogSMSComplaint.SelectedIndices[0]].Text;
                DialogSelectComplaint selectCustComplaintForm = new DialogSelectComplaint(REGION, AID, LID, LOGUSERID, USERNUMBER, COMPID);
                selectCustComplaintForm.ShowDialog();
            }
            catch (Exception) { buttonSMSComplaintViewSelectSMSComplaint.Enabled = false; }
        }

        private void listViewDialogSMSComplaint_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                String COMPID = listViewDialogSMSComplaint.Items[listViewDialogSMSComplaint.SelectedIndices[0]].Text;
                DialogSelectComplaint selectCustComplaintForm = new DialogSelectComplaint(REGION, AID, LID, LOGUSERID, USERNUMBER, COMPID);
                selectCustComplaintForm.ShowDialog();
            }
            catch (Exception) { buttonSMSComplaintViewSelectSMSComplaint.Enabled = false; }
        }

        private void buttonSMSComplaintDeleteSelectSMSComplaint_Click(object sender, EventArgs e)
        {

        }

        private void buttonSMSComplaintViewInGoogleEarth_Click(object sender, EventArgs e)
        {

        }

        private void buttonSMSComplaintSaveInExcel_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (textBoxComplaintSearch.Text != "")
            //{
            //    listViewDialogSMSComplaint.Items.Clear();
            //    config callServer = new config();
            //    DataTable _dt_customer = callServer.dialogServerInsert("TAB_LOAD_CUSTOMER_SEARCH_BY_NUMBER", textBoxCustomerSearch.Text);
            //    for (int i = 0; i < _dt_customer.Rows.Count; i++)
            //    {
            //        DataRow dr = _dt_customer.Rows[i];
            //        ListViewItem listitem = new ListViewItem(dr["CustNumber"].ToString());
            //        listitem.SubItems.Add(dr["CustName"].ToString());
            //        listitem.SubItems.Add(dr["CustGender"].ToString());
            //        listitem.SubItems.Add(dr["CustRegion"].ToString());
            //        listitem.SubItems.Add(dr["CustEmail"].ToString());
            //        listitem.SubItems.Add(dr["CustInsert"].ToString());
            //        listitem.SubItems.Add(dr["CustModified"].ToString());
            //        listViewDialogSMSComplaint.Items.Add(listitem);
            //    }
            //}
        }

  
        #endregion



        #region TAB Dialog Site


        private void DisplayDialogSiteTab()
        {
            listViewDialogSites.Items.Clear();
            DataTable _dt_DialogSite = callServer.dialogServerInsert("TAB_LOAD_DIALOG_SITES");
            for (int i = 0; i < _dt_DialogSite.Rows.Count; i++)
            {
                DataRow dr = _dt_DialogSite.Rows[i];
                ListViewItem listitem = new ListViewItem(dr["SiteID"].ToString());
                listitem.SubItems.Add(dr["SiteName"].ToString());
                listitem.SubItems.Add(dr["SiteUID"].ToString());
                listitem.SubItems.Add(dr["Longitude"].ToString());
                listitem.SubItems.Add(dr["Latitude"].ToString());
                listitem.SubItems.Add(dr["SiteStatus"].ToString());
                listitem.SubItems.Add(dr["TowerOwner"].ToString());
                listitem.SubItems.Add(dr["TowerType"].ToString());
                listitem.SubItems.Add(dr["Modified"].ToString());
                listViewDialogSites.Items.Add(listitem);
                //alertWaitForm.Refresh();
            }
        }

        private void buttonSite_Click(object sender, EventArgs e)
        {
            listViewDialogSites.Items.Clear();
            DataTable _dt_DialogSite = callServer.dialogServerInsert("TAB_LOAD_DIALOG_SITES_LIKE_AM");
            for (int i = 0; i < _dt_DialogSite.Rows.Count; i++)
            {
                DataRow dr = _dt_DialogSite.Rows[i];
                ListViewItem listitem = new ListViewItem(dr["SiteID"].ToString());
                listitem.SubItems.Add(dr["SiteName"].ToString());
                listitem.SubItems.Add(dr["SiteUID"].ToString());
                listitem.SubItems.Add(dr["Longitude"].ToString());
                listitem.SubItems.Add(dr["Latitude"].ToString());
                listitem.SubItems.Add(dr["SiteStatus"].ToString());
                listitem.SubItems.Add(dr["TowerOwner"].ToString());
                listitem.SubItems.Add(dr["TowerType"].ToString());
                listitem.SubItems.Add(dr["Modified"].ToString());
                listViewDialogSites.Items.Add(listitem);
            }
        }

        private void buttonSiteAN_Click(object sender, EventArgs e)
        {

        }

        private void buttonSiteBA_Click(object sender, EventArgs e)
        {

        }

        private void buttonSiteBD_Click(object sender, EventArgs e)
        {

        }

        private void buttonSiteCM_Click(object sender, EventArgs e)
        {

        }

        private void buttonSiteGA_Click(object sender, EventArgs e)
        {

        }

        private void buttonSiteGM_Click(object sender, EventArgs e)
        {

        }

        private void buttonSiteHA_Click(object sender, EventArgs e)
        {

        }

        private void buttonSiteKA_Click(object sender, EventArgs e)
        {

        }

        private void buttonSiteKE_Click(object sender, EventArgs e)
        {

        }

        private void buttonSiteKI_Click(object sender, EventArgs e)
        {

        }

        private void buttonSiteKL_Click(object sender, EventArgs e)
        {

        }

        private void buttonSiteKU_Click(object sender, EventArgs e)
        {

        }

        private void buttonSiteMA_Click(object sender, EventArgs e)
        {

        }

        private void buttonSiteMO_Click(object sender, EventArgs e)
        {

        }

        private void buttonSiteMR_Click(object sender, EventArgs e)
        {

        }

        private void buttonSiteMT_Click(object sender, EventArgs e)
        {

        }

        private void buttonSiteMU_Click(object sender, EventArgs e)
        {

        }

        private void buttonSiteNU_Click(object sender, EventArgs e)
        {

        }

        private void buttonSitePO_Click(object sender, EventArgs e)
        {

        }

        private void buttonSitePU_Click(object sender, EventArgs e)
        {

        }

        private void buttonSiteRA_Click(object sender, EventArgs e)
        {

        }

        private void buttonSiteTR_Click(object sender, EventArgs e)
        {

        }

        private void buttonSiteVA_Click(object sender, EventArgs e)
        {

        }


        #endregion

        private void buttonDialogSiteAddNewSite_Click(object sender, EventArgs e)
        {

        }

        private void buttonDialogSiteSearchSite_Click(object sender, EventArgs e)
        {

        }

        private void listViewDialogSites_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void statusStripDialogMobileServer_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }




        #region TAB Other Site



        #endregion


        
        #region TAB Login User



        #endregion



















        //private void textBoxUserSettingsYourPassWord_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (textBoxUserSettingsYourPassWord.Text == "")
        //    {
        //        labelUserSettingsNewPassWord.Enabled = false;
        //        labelUserSettingsComfPassWord.Enabled = false;
        //        textBoxUserSettingsNewPassWord.Enabled = false;
        //        textBoxUserSettingsComfPassWord.Enabled = false;
        //        buttonUserSettingsSAVE.Enabled = false;
        //        buttonUserSettingsCANCEL.Enabled = false;
        //    }
        //    else
        //    {
        //        labelUserSettingsNewPassWord.Enabled = true;
        //        labelUserSettingsComfPassWord.Enabled = true;
        //        textBoxUserSettingsNewPassWord.Enabled = true;
        //        textBoxUserSettingsComfPassWord.Enabled = true;
        //        buttonUserSettingsSAVE.Enabled = true;
        //        buttonUserSettingsCANCEL.Enabled = true;
        //    }
        //}



    }
}
