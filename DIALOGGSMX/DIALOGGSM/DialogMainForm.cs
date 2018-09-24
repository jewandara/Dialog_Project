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

        DialogWaitForm alertWaitForm;
        private String UserID;
        private String KeyID;

        #region Server Customer Start

        public DialogMainForm(String AppKeyID, String AppUserID)
        {
            InitializeComponent();
            UserID = AppUserID;
            KeyID = AppKeyID;
        }

        delegate void SetListViewItemCallBack(ListViewItem Item);

        private void AddListViewItem(ListViewItem Item)
        {
            if (this.listViewDialogCustomer.InvokeRequired)
            {
                SetListViewItemCallBack d = new SetListViewItemCallBack(AddListViewItem);
                this.Invoke(d, new object[] { Item });
            }
            else
            {
                this.listViewDialogCustomer.Items.Add(Item);
            }
        }

        private void AddListViewItem2(ListViewItem Item)
        {
            if (this.listViewDialogCustomer.InvokeRequired)
            {
                SetListViewItemCallBack d = new SetListViewItemCallBack(AddListViewItem2);
                this.Invoke(d, new object[] { Item });
            }
            else
            {
                this.listViewDialogSites.Items.Add(Item);
            }
        }


        private void backgroundWorkerDialogMobileServer_DoWork(object sender, DoWorkEventArgs e)
        {


            BackgroundWorker worker = sender as BackgroundWorker;
            config callServer = new config();

            DataTable dt;
            dt = callServer.dialogServerInsert("TAB_LOAD_CUSTOMER_SMS");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                ListViewItem listitem = new ListViewItem(dr["CustNumber"].ToString());
                listitem.SubItems.Add(dr["CustName"].ToString());
                listitem.SubItems.Add(dr["CustGender"].ToString());
                listitem.SubItems.Add(dr["CustRegion"].ToString());
                listitem.SubItems.Add(dr["CustEmail"].ToString());
                listitem.SubItems.Add(dr["CustInsert"].ToString());
                listitem.SubItems.Add(dr["CustModified"].ToString());
                AddListViewItem(listitem);
            }
            dt.Dispose();

            dt = callServer.dialogServerInsert("TAB_LOAD_CUSTOMER_NET");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                ListViewItem listitem = new ListViewItem(dr["CustNumber"].ToString());
                listitem.SubItems.Add(dr["CustName"].ToString());
                listitem.SubItems.Add(dr["CustGender"].ToString());
                listitem.SubItems.Add(dr["CustRegion"].ToString());
                listitem.SubItems.Add(dr["CustEmail"].ToString());
                listitem.SubItems.Add(dr["CustInsert"].ToString());
                listitem.SubItems.Add(dr["CustModified"].ToString());
                AddListViewItem(listitem);
                worker.ReportProgress(Convert.ToInt32((i / dt.Rows.Count) * (25 / 100)));
            }
            dt.Dispose();

            dt = callServer.dialogServerInsert("TAB_LOAD_DIALOG_SITES");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                ListViewItem listitem = new ListViewItem(dr["SiteID"].ToString());
                listitem.SubItems.Add(dr["SiteName"].ToString());
                listitem.SubItems.Add(dr["SiteUID"].ToString());
                listitem.SubItems.Add(dr["Longitude"].ToString());
                listitem.SubItems.Add(dr["Latitude"].ToString());
                listitem.SubItems.Add(dr["SiteStatus"].ToString());
                listitem.SubItems.Add(dr["TowerOwner"].ToString());
                listitem.SubItems.Add(dr["TowerType"].ToString());
                listitem.SubItems.Add(dr["Modified"].ToString());
                AddListViewItem2(listitem);
            }
            dt.Dispose();

        }

        private void backgroundWorkerDialogMobileServer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripStatusLabelData.Text = "Processing . . .";
            alertWaitForm.Refresh();
        }

        private void backgroundWorkerDialogMobileServer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                toolStripStatusLabelData.Text = "Canceled !";
            }
            else if (e.Error != null)
            {
                toolStripStatusLabelData.Text = "Error: " + e.Error.Message;
            }
            else
            {
                toolStripStatusLabelData.Text = "Ready";
            }
            alertWaitForm.Close();
            this.Enabled = true;
        }





        private void LoadServer()
        {
            this.toolStripProgressBarWait.Visible = true;
            this.Enabled = false;
            alertWaitForm = new DialogWaitForm();
            alertWaitForm.Show();

            alertWaitForm.Refresh();
            //DisplayCustomerTab();
            this.toolStripProgressBarWait.Value = 20;

            alertWaitForm.Refresh();
            DisplaySMSComplaintTab();
            this.toolStripProgressBarWait.Value = 40;

            alertWaitForm.Refresh();
            DisplayDialogSiteTab();
            this.toolStripProgressBarWait.Value = 60;

            alertWaitForm.Refresh();
            alertWaitForm.Close();
            this.Enabled = true;
            this.toolStripProgressBarWait.Visible = false;
        }


        //private void button1_Click(object sender, EventArgs e)
        //{
        //    this.Enabled = false;
        //    LoadServer();
        //    this.Enabled = true;
        //    this.Refresh();
        //}





        private void DialogMainForm_Load(object sender, EventArgs e)
        {
            //if (backgroundWorkerDialogMobileServer.IsBusy != true)
            //{
            //    alertWaitForm = new DialogWaitForm();
            //    alertWaitForm.Show();
            //    alertWaitForm.Refresh();
            //    this.Enabled = false;
            //    backgroundWorkerDialogMobileServer.RunWorkerAsync();
            //}
            this.Refresh();


            //LoadServer();


           // DialogWaitForm alertWait = new DialogWaitForm();
           //// alertWait.ShowDialog();
           // _delReportDone = new delWorkWaitReport(DisplayCustomerTab);
           // Thread _thread = new Thread(new ThreadStart(involkCall));
           // _thread.Start();

           // _delReportDone = new delWorkWaitReport(DisplayDialogSiteTab);
           // Thread _thread2 = new Thread(new ThreadStart(involkCall));
           // _thread2.Start();

            //config callServer = new config();
            //DataTable _dt_customer = callServer.dialogServerInsert("TAB_LOAD_CUSTOMER");
            //for (int i = 0; i < _dt_customer.Rows.Count; i++)
            //{
            //    DataRow dr = _dt_customer.Rows[i];
            //    ListViewItem listitem = new ListViewItem(dr["CustNumber"].ToString());
            //    listitem.SubItems.Add(dr["CustName"].ToString());
            //    listitem.SubItems.Add(dr["CustGender"].ToString());
            //    listitem.SubItems.Add(dr["CustRegion"].ToString());
            //    listitem.SubItems.Add(dr["CustEmail"].ToString());
            //    listitem.SubItems.Add(dr["CustInsert"].ToString());
            //    listitem.SubItems.Add(dr["CustModified"].ToString());
            //    listViewDialogCustomer.Items.Add(listitem);
            //}

            //DataTable _dt_dialogSite = callServer.dialogServerInsert("TAB_LOAD_DIALOG_SITES");
            //for (int i = 0; i < _dt_dialogSite.Rows.Count; i++)
            //{
            //    DataRow dr = _dt_dialogSite.Rows[i];
            //    ListViewItem listitem = new ListViewItem(dr["SiteID"].ToString());
            //    listitem.SubItems.Add(dr["SiteName"].ToString());
            //    listitem.SubItems.Add(dr["SiteUID"].ToString());
            //    listitem.SubItems.Add(dr["Longitude"].ToString());
            //    listitem.SubItems.Add(dr["Latitude"].ToString());
            //    listitem.SubItems.Add(dr["SiteStatus"].ToString());
            //    listitem.SubItems.Add(dr["TowerOwner"].ToString());
            //    listitem.SubItems.Add(dr["TowerType"].ToString());
            //    listitem.SubItems.Add(dr["Modified"].ToString());
            //    listViewDialogSites.Items.Add(listitem);
            //}

        }

        private void DialogMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        #endregion





















        #region TAB Customer

        private void DisplaySMSCustomerTab()
        {
            config callServer = new config();
            listViewDialogCustomer.Items.Clear();
            DataTable dt = callServer.dialogServerInsert("TAB_LOAD_CUSTOMER_SMS", KeyID);
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

        private void DisplayNETCustomerTab()
        {
            config callServer = new config();
            listViewDialogCustomer.Items.Clear();
            DataTable dt = callServer.dialogServerInsert("TAB_LOAD_CUSTOMER_NET", KeyID);
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

        private void buttonCustomerRefreshNewCustomer_Click(object sender, EventArgs e)
        {
            DisplaySMSCustomerTab();
        }

        private void buttonCustomerRefreshNetCustomer_Click(object sender, EventArgs e)
        {
            DisplayNETCustomerTab();
        }

        private void textBoxCustomerSearch_TextChanged(object sender, EventArgs e)
        {
            if (textBoxCustomerSearch.Text != "")
            {
                listViewDialogCustomer.Items.Clear();
                config callServer = new config();
                DataTable _dt_customer = null;
                if (radioButtonCustomerSearchByCustNumber.Checked == true)
                { _dt_customer = callServer.dialogServerInsert("TAB_LOAD_CUSTOMER_SEARCH", "@KEY = '" + KeyID + "', @TEXT = '" + textBoxCustomerSearch.Text + "', @TYPE = 'BY_NUMBER'"); }
                if (radioButtonCustomerSearchByCustName.Checked == true)
                { _dt_customer = callServer.dialogServerInsert("TAB_LOAD_CUSTOMER_SEARCH", "@KEY = '" + KeyID + "', @TEXT = '" + textBoxCustomerSearch.Text + "', @TYPE = 'BY_NAME'"); }
                if (radioButtonCustomerSearchByEmail.Checked == true)
                { _dt_customer = callServer.dialogServerInsert("TAB_LOAD_CUSTOMER_SEARCH", "@KEY = '" + KeyID + "', @TEXT = '" + textBoxCustomerSearch.Text + "', @TYPE = 'BY_EMAIL'"); }

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
        }

        private void listViewDialogCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonCustomerViewSelectCustomer.Enabled = true;
        }

        private void buttonCustomerViewSelectCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                String CusID = listViewDialogCustomer.Items[listViewDialogCustomer.SelectedIndices[0]].Text;
                DialogSelectCustomer selectCustForm = new DialogSelectCustomer(KeyID, CusID);
                selectCustForm.ShowDialog();
            }
            catch(Exception)
            {
                buttonCustomerViewSelectCustomer.Enabled = false;
            }
        }

        private void buttonCustomerAddNewCustomer_Click(object sender, EventArgs e)
        {
            DialogNewCustomer newCustForm = new DialogNewCustomer();
            newCustForm.ShowDialog();
        } 

        private void buttonCustomerSaveInExcel_Click(object sender, EventArgs e)
        {

        }

        private void listViewDialogCustomer_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                String CusID = listViewDialogCustomer.Items[listViewDialogCustomer.SelectedIndices[0]].Text;
                DialogSelectCustomer selectCustForm = new DialogSelectCustomer(KeyID, CusID);
                selectCustForm.ShowDialog();
            }
            catch (Exception)
            {
                buttonCustomerViewSelectCustomer.Enabled = false;
            }
        }

        #endregion



        #region TAB SMS Complaint


        private void DisplaySMSComplaintTab()
        {
            listViewDialogSMSComplaint.Items.Clear();
            config callServer = new config();
            DataTable _dt_SMSComplaint = callServer.dialogServerInsert("TAB_LOAD_SMSCOMPLAINT");
            for (int i = 0; i < _dt_SMSComplaint.Rows.Count; i++)
            {
                DataRow dr = _dt_SMSComplaint.Rows[i];
                ListViewItem listitem = new ListViewItem(dr["ComplaintID"].ToString());
                listitem.SubItems.Add(dr["CustNumber"].ToString());
                listitem.SubItems.Add(dr["CustName"].ToString());
                listitem.SubItems.Add(dr["Longitude"].ToString());
                listitem.SubItems.Add(dr["Latitude"].ToString());
                listitem.SubItems.Add(dr["ComplaintData"].ToString());
                listitem.SubItems.Add(dr["MessageTime"].ToString());
                listitem.SubItems.Add(dr["ComplainInsert"].ToString());
                listitem.SubItems.Add(dr["ComplainModified"].ToString());
                listViewDialogSMSComplaint.Items.Add(listitem);
                alertWaitForm.Refresh();
            }
        }

        private void buttonSMSComplaintRefreshNewComplaint_Click(object sender, EventArgs e)
        {
            DisplaySMSComplaintTab();
        }

        private void listViewDialogSMSComplaint_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonSMSComplaintViewSelectSMSComplaint.Enabled = true;
        }

        private void buttonSMSComplaintViewSelectSMSComplaint_Click(object sender, EventArgs e)
        {
            try
            {
                String CusID = listViewDialogSMSComplaint.Items[listViewDialogSMSComplaint.SelectedIndices[0]].Text;
                DialogSelectComplaint selectCustForm = new DialogSelectComplaint(CusID);
                selectCustForm.ShowDialog();
            }
            catch (Exception)
            {
                buttonSMSComplaintViewSelectSMSComplaint.Enabled = false;
            }
        }

        private void listViewDialogSMSComplaint_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                String CusID = listViewDialogSMSComplaint.Items[listViewDialogSMSComplaint.SelectedIndices[0]].Text;
                DialogSelectComplaint selectCustForm = new DialogSelectComplaint(CusID);
                selectCustForm.ShowDialog();
            }
            catch (Exception)
            {
                buttonSMSComplaintViewSelectSMSComplaint.Enabled = false;
            }
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
            if (textBoxComplaintSearch.Text != "")
            {
                listViewDialogSMSComplaint.Items.Clear();
                config callServer = new config();
                DataTable _dt_customer = callServer.dialogServerInsert("TAB_LOAD_CUSTOMER_SEARCH_BY_NUMBER", textBoxCustomerSearch.Text);
                for (int i = 0; i < _dt_customer.Rows.Count; i++)
                {
                    DataRow dr = _dt_customer.Rows[i];
                    ListViewItem listitem = new ListViewItem(dr["CustNumber"].ToString());
                    listitem.SubItems.Add(dr["CustName"].ToString());
                    listitem.SubItems.Add(dr["CustGender"].ToString());
                    listitem.SubItems.Add(dr["CustRegion"].ToString());
                    listitem.SubItems.Add(dr["CustEmail"].ToString());
                    listitem.SubItems.Add(dr["CustInsert"].ToString());
                    listitem.SubItems.Add(dr["CustModified"].ToString());
                    listViewDialogSMSComplaint.Items.Add(listitem);
                }
            }
        }

  
        #endregion



        #region TAB Dialog Site


        private void DisplayDialogSiteTab()
        {
            listViewDialogSites.Items.Clear();
            config callServer = new config();
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
                alertWaitForm.Refresh();
            }
        }

        private void buttonSite_Click(object sender, EventArgs e)
        {
            listViewDialogSites.Items.Clear();
            config callServer = new config();
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
            listViewDialogSites.Items.Clear();
            config callServer = new config();
            DataTable _dt_DialogSite = callServer.dialogServerInsert("TAB_LOAD_DIALOG_SITES_LIKE_AN");
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

        private void buttonSiteBA_Click(object sender, EventArgs e)
        {
            listViewDialogSites.Items.Clear();
            config callServer = new config();
            DataTable _dt_DialogSite = callServer.dialogServerInsert("TAB_LOAD_DIALOG_SITES_LIKE_BA");
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

        private void buttonSiteBD_Click(object sender, EventArgs e)
        {
            listViewDialogSites.Items.Clear();
            config callServer = new config();
            DataTable _dt_DialogSite = callServer.dialogServerInsert("TAB_LOAD_DIALOG_SITES_LIKE_BD");
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

        private void buttonSiteCM_Click(object sender, EventArgs e)
        {
            listViewDialogSites.Items.Clear();
            config callServer = new config();
            DataTable _dt_DialogSite = callServer.dialogServerInsert("TAB_LOAD_DIALOG_SITES_LIKE_CM");
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

        private void buttonSiteGA_Click(object sender, EventArgs e)
        {
            listViewDialogSites.Items.Clear();
            config callServer = new config();
            DataTable _dt_DialogSite = callServer.dialogServerInsert("TAB_LOAD_DIALOG_SITES_LIKE_GA");
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

        private void buttonSiteGM_Click(object sender, EventArgs e)
        {
            listViewDialogSites.Items.Clear();
            config callServer = new config();
            DataTable _dt_DialogSite = callServer.dialogServerInsert("TAB_LOAD_DIALOG_SITES_LIKE_GM");
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

        private void buttonSiteHA_Click(object sender, EventArgs e)
        {
            listViewDialogSites.Items.Clear();
            config callServer = new config();
            DataTable _dt_DialogSite = callServer.dialogServerInsert("TAB_LOAD_DIALOG_SITES_LIKE_HA");
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

        private void buttonSiteKA_Click(object sender, EventArgs e)
        {
            listViewDialogSites.Items.Clear();
            config callServer = new config();
            DataTable _dt_DialogSite = callServer.dialogServerInsert("TAB_LOAD_DIALOG_SITES_LIKE_KA");
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

        private void buttonSiteKE_Click(object sender, EventArgs e)
        {
            listViewDialogSites.Items.Clear();
            config callServer = new config();
            DataTable _dt_DialogSite = callServer.dialogServerInsert("TAB_LOAD_DIALOG_SITES_LIKE_KE");
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

        private void buttonSiteKI_Click(object sender, EventArgs e)
        {
            listViewDialogSites.Items.Clear();
            config callServer = new config();
            DataTable _dt_DialogSite = callServer.dialogServerInsert("TAB_LOAD_DIALOG_SITES_LIKE_KI");
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

        private void buttonSiteKL_Click(object sender, EventArgs e)
        {
            listViewDialogSites.Items.Clear();
            config callServer = new config();
            DataTable _dt_DialogSite = callServer.dialogServerInsert("TAB_LOAD_DIALOG_SITES_LIKE_KL");
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

        private void buttonSiteKU_Click(object sender, EventArgs e)
        {
            listViewDialogSites.Items.Clear();
            config callServer = new config();
            DataTable _dt_DialogSite = callServer.dialogServerInsert("TAB_LOAD_DIALOG_SITES_LIKE_KU");
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

        private void buttonSiteMA_Click(object sender, EventArgs e)
        {
            listViewDialogSites.Items.Clear();
            config callServer = new config();
            DataTable _dt_DialogSite = callServer.dialogServerInsert("TAB_LOAD_DIALOG_SITES_LIKE_MA");
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

        private void buttonSiteMO_Click(object sender, EventArgs e)
        {
            listViewDialogSites.Items.Clear();
            config callServer = new config();
            DataTable _dt_DialogSite = callServer.dialogServerInsert("TAB_LOAD_DIALOG_SITES_LIKE_MO");
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

        private void buttonSiteMR_Click(object sender, EventArgs e)
        {
            listViewDialogSites.Items.Clear();
            config callServer = new config();
            DataTable _dt_DialogSite = callServer.dialogServerInsert("TAB_LOAD_DIALOG_SITES_LIKE_MR");
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

        private void buttonSiteMT_Click(object sender, EventArgs e)
        {
            listViewDialogSites.Items.Clear();
            config callServer = new config();
            DataTable _dt_DialogSite = callServer.dialogServerInsert("TAB_LOAD_DIALOG_SITES_LIKE_MT");
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

        private void buttonSiteMU_Click(object sender, EventArgs e)
        {
            listViewDialogSites.Items.Clear();
            config callServer = new config();
            DataTable _dt_DialogSite = callServer.dialogServerInsert("TAB_LOAD_DIALOG_SITES_LIKE_MU");
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

        private void buttonSiteNU_Click(object sender, EventArgs e)
        {
            listViewDialogSites.Items.Clear();
            config callServer = new config();
            DataTable _dt_DialogSite = callServer.dialogServerInsert("TAB_LOAD_DIALOG_SITES_LIKE_NU");
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

        private void buttonSitePO_Click(object sender, EventArgs e)
        {
            listViewDialogSites.Items.Clear();
            config callServer = new config();
            DataTable _dt_DialogSite = callServer.dialogServerInsert("TAB_LOAD_DIALOG_SITES_LIKE_PO");
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

        private void buttonSitePU_Click(object sender, EventArgs e)
        {
            listViewDialogSites.Items.Clear();
            config callServer = new config();
            DataTable _dt_DialogSite = callServer.dialogServerInsert("TAB_LOAD_DIALOG_SITES_LIKE_PU");
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

        private void buttonSiteRA_Click(object sender, EventArgs e)
        {
            listViewDialogSites.Items.Clear();
            config callServer = new config();
            DataTable _dt_DialogSite = callServer.dialogServerInsert("TAB_LOAD_DIALOG_SITES_LIKE_RA");
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

        private void buttonSiteTR_Click(object sender, EventArgs e)
        {
            listViewDialogSites.Items.Clear();
            config callServer = new config();
            DataTable _dt_DialogSite = callServer.dialogServerInsert("TAB_LOAD_DIALOG_SITES_LIKE_TR");
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

        private void buttonSiteVA_Click(object sender, EventArgs e)
        {
            listViewDialogSites.Items.Clear();
            config callServer = new config();
            DataTable _dt_DialogSite = callServer.dialogServerInsert("TAB_LOAD_DIALOG_SITES_LIKE_VA");
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


        #endregion


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
