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
    public partial class DialogSelectComplaint : Form
    {


        #region DEFINE

        //Saving Folder path
        private String pathSaveKmlFile = "";
        //Saving Kmal path
        private String folderSaveImageFile = "";
        //Select Dialog Site
        private Boolean checkDialogSitesOK = true;
        //Area of sites
        private Decimal DialogSitesAreaValue = 200;
        //Other sites
        private String OtherSiteOwnerName = "MOBITEL";
        //Band Type
        private String checkAntennaBandtype = "GSM";
        //Select Site Line
        private Boolean checkDirectionLineOK = true;
        //Line Length
        private Decimal AntennaDirectionValue = 500;
        //Select Full Beamwidth
        private Boolean checkAntennaFullBeamwidthOK = false;
        //Full Beamwidth Length
        private Decimal AntennaFullBeamwidthValue = 750;
        //Select Half Beamwidth
        private Boolean checkAntennaHalfBeamwidthOK = true;
        //Half Beamwidth Length
        private Decimal AntennaHalfBeamwidthValue = 1000;
        //Customer Circal Redious
        private int selectCustomerLineLength = 1;

        private int AID;//APP NUMBER
        private int LID;//USER APP NUMBER
        private String USERNUMBER; //User Phone Number
        private String LOGUSERID; //User ID
        private String COMPLAINTID; //Complaint Id
        private String REGION; //User Region
        private config callServer = new config();//class config

        private String ComplaintCustNumber;
        private String ComplaintCustLong;
        private String ComplaintCustLat;
        private String ComplaintCustData;
        private String ComplaintCustSMSDate;


        #endregion




        #region FORM


        public DialogSelectComplaint(String RE, int SYS_APP_ID, int LOG_APP_ID, String USER_LOGIN_ID, String USER_NUMBER, String COMPLAINT_ID)
        {
            InitializeComponent();
            comboBoxAntennaBandtype.Items.Insert(0, "NONE");
            comboBoxAntennaBandtype.Items.Insert(1, "GSM");
            comboBoxAntennaBandtype.Items.Insert(2, "DCS");
            comboBoxAntennaBandtype.Items.Insert(3, "3G");
            comboBoxAntennaBandtype.Items.Insert(4, "4G");
            comboBoxAntennaBandtype.SelectedIndex = 1;
            comboBoxCustomerLine.Items.Insert(0, "100 METER");
            comboBoxCustomerLine.Items.Insert(1, "200 METER");
            comboBoxCustomerLine.Items.Insert(2, "500 METER");
            comboBoxCustomerLine.Items.Insert(3, "1 KILOMETER");
            comboBoxCustomerLine.Items.Insert(4, "2 KILOMETER");
            comboBoxCustomerLine.Items.Insert(5, "5 KILOMETER");
            comboBoxCustomerLine.Items.Insert(6, "10 KILOMETER");
            comboBoxCustomerLine.SelectedIndex = 2;

            this.Text = "Dialog Complaint " + COMPLAINT_ID + " - DIALOG MOBILE Customer Complaint";
            REGION = RE;
            AID = SYS_APP_ID;
            LID = LOG_APP_ID;
            LOGUSERID = USER_LOGIN_ID;
            USERNUMBER = USER_NUMBER;
            COMPLAINTID = COMPLAINT_ID;
            labelLog.Text = USERNUMBER + " : SYS_APP_ID - " + SYS_APP_ID.ToString() + " : LOG_APP_ID - " + LOG_APP_ID.ToString() + " : LOGIN SYSTEM";
            DisplaySelectComplaint();
        }

        private void DisplaySelectComplaint()
        {
            try
            {
                DataTable _dt_SelectComplaint = callServer.dialogServerInsert("TAB_LOAD_COMPLAINT_SELECT", " @SYSAPPID = " + AID + ", @USERAPPID	= " + LID + ", @USERNUMBER = '" + USERNUMBER + "', @USERLOGID = '" + LOGUSERID + "', @COMPLAINTID = '" + COMPLAINTID + "'");
                DataRow dr = _dt_SelectComplaint.Rows[0];
                CompID.Text = dr["ComplaintID"].ToString().ToUpper();
                labelCustName.Text = dr["CustName"].ToString();
                labelCustNumber.Text = "+" + dr["CustNumber"].ToString();
                textBoxComplaintCustTitleData.Text = dr["ComplaintTitle"].ToString() + Environment.NewLine + dr["ComplaintData"].ToString();
                textBoxComplaintCustLong.Text = dr["Longitude"].ToString();
                textBoxComplaintCustLat.Text = dr["Latitude"].ToString();

                ComplaintCustNumber = dr["CustNumber"].ToString();
                ComplaintCustLong = dr["Longitude"].ToString();
                ComplaintCustLat = dr["Latitude"].ToString();
                ComplaintCustData = dr["ComplaintData"].ToString();
                ComplaintCustSMSDate = dr["MessageTime"].ToString();
            }
            catch (Exception) { MessageBox.Show("Call the administrator.", "Load Cpmplaint Error", MessageBoxButtons.OK, MessageBoxIcon.Hand); }
        }

        private void loadFormValusOfComplaint()
        {
            ComplaintCustLong = textBoxComplaintCustLong.Text;
            ComplaintCustLat = textBoxComplaintCustLat.Text;
            labelProgressSiteBar.Text = "Processing . . . 0 %";
            labelProgressSectorBar.Text = "Processing . . . 0 %";
            progressBarSite.Value = 0;
            progressBarSector.Value = 1;
            if (checkBoxSelectDialogSites.Checked) { checkDialogSitesOK = true; }
            else { checkDialogSitesOK = false; }
            labelProgressSiteBar.Text = "Processing . . . 1 %";
            progressBarSector.Value = 1;
            DialogSitesAreaValue = numericUpDownCustomerAreaCircal.Value * Convert.ToDecimal(1000);
            labelProgressSiteBar.Text = "Processing . . . 2 %";
            progressBarSector.Value = 2;
            if (checkBoxSelectOtherSites.Checked)
            {
                if (radioButtonMobitelSites.Checked) { OtherSiteOwnerName = "MOBITEL"; }
                else if (radioButtonHutchSites.Checked) { OtherSiteOwnerName = "HUTCH"; }
                else if (radioButtonEtisalatSites.Checked) { OtherSiteOwnerName = "ETISALAT"; }
                else if (radioButtonLankaBellSites.Checked) { OtherSiteOwnerName = "LANKABELL"; }
                else { OtherSiteOwnerName = ""; }
            }
            else { OtherSiteOwnerName = ""; }
            labelProgressSiteBar.Text = "Processing . . . 3 %";
            progressBarSector.Value = 3;
            if (comboBoxAntennaBandtype.SelectedIndex == 0) { checkAntennaBandtype = ""; }
            else if (comboBoxAntennaBandtype.SelectedIndex == 1) { checkAntennaBandtype = "GSM"; }
            else if (comboBoxAntennaBandtype.SelectedIndex == 2) { checkAntennaBandtype = "DCS"; }
            else if (comboBoxAntennaBandtype.SelectedIndex == 3) { checkAntennaBandtype = "3G"; }
            else if (comboBoxAntennaBandtype.SelectedIndex == 4) { checkAntennaBandtype = "4G"; }
            else { checkAntennaBandtype = ""; }
            labelProgressSiteBar.Text = "Processing . . . 4 %";
            progressBarSector.Value = 4;
            if (checkBoxViewAntennaDirectionLine.Checked) { checkDirectionLineOK = true; AntennaDirectionValue = numericUpDownAntennaLineDistance.Value; }
            else { checkDirectionLineOK = false; }
            labelProgressSiteBar.Text = "Processing . . . 5 %";
            progressBarSector.Value = 5;
            if (checkBoxViewAntennaFullBeamwidth.Checked) { checkAntennaFullBeamwidthOK = true; AntennaFullBeamwidthValue = numericUpDownAntennaFullBeamwidth.Value; }
            else { checkAntennaFullBeamwidthOK = false; }
            labelProgressSiteBar.Text = "Processing . . . 6 %";
            progressBarSector.Value = 6;
            if (checkBoxViewAntennaHalfBeamwidth.Checked) { checkAntennaHalfBeamwidthOK = true; AntennaHalfBeamwidthValue = numericUpDownAntennaHalfBeamwidth.Value; }
            else { checkAntennaHalfBeamwidthOK = false; }
            labelProgressSiteBar.Text = "Processing . . . 7 %";
            progressBarSector.Value = 7;
            if (comboBoxCustomerLine.SelectedIndex == 0) { selectCustomerLineLength = 1; }
            else if (comboBoxCustomerLine.SelectedIndex == 1) { selectCustomerLineLength = 2; }
            else if (comboBoxCustomerLine.SelectedIndex == 2) { selectCustomerLineLength = 5; }
            else if (comboBoxCustomerLine.SelectedIndex == 3) { selectCustomerLineLength = 10; }
            else if (comboBoxCustomerLine.SelectedIndex == 4) { selectCustomerLineLength = 20; }
            else if (comboBoxCustomerLine.SelectedIndex == 5) { selectCustomerLineLength = 50; }
            else if (comboBoxCustomerLine.SelectedIndex == 6) { selectCustomerLineLength = 100; }
            else { selectCustomerLineLength = 1; }
            labelProgressSiteBar.Text = "Processing . . . 8 %";
            progressBarSector.Value = 8;
        }

        private void hideFormComplaint()
        {
            groupBoxSiteView.Enabled = false;
            groupBoxAntennaView.Enabled = false;
            buttonDeleteComplaint.Enabled = false;
            buttonViewComplaint.Enabled = false;
            buttonSaveKml.Enabled = false;
            buttonOK.Enabled = false;
            buttonOpenEarth.Visible = false;
            buttonOpenEarth.Enabled = false;
            buttonCancel.Visible = true;
            buttonCancel.Enabled = true;
        }

        private void showFormComplaint()
        {
            groupBoxSiteView.Enabled = true;
            groupBoxAntennaView.Enabled = true;
            buttonDeleteComplaint.Enabled = true;
            buttonViewComplaint.Enabled = true;
            buttonSaveKml.Enabled = true;
            buttonOK.Enabled = true;
            buttonOpenEarth.Visible = true;
            buttonOpenEarth.Enabled = true;
            buttonCancel.Visible = false;
            buttonCancel.Enabled = false;
        }
        
        
        #endregion




        #region FUNCTIONS


        private void DialogSelectComplaint_Load(object sender, EventArgs e)
        {

        }

        private void checkBoxSelectOtherSites_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSelectOtherSites.Checked)
            {
                radioButtonMobitelSites.Enabled = true;
                radioButtonHutchSites.Enabled = true;
                radioButtonEtisalatSites.Enabled = true;
                radioButtonLankaBellSites.Enabled = true;
            }
            else 
            {
                radioButtonMobitelSites.Enabled = false;
                radioButtonHutchSites.Enabled = false;
                radioButtonEtisalatSites.Enabled = false;
                radioButtonLankaBellSites.Enabled = false;
            }
        }

        private void checkBoxSelectDialogSites_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSelectDialogSites.Checked) { groupBoxAntennaView.Enabled = true; }
            else { groupBoxAntennaView.Enabled = false; }
        }

        private void comboBoxAntennaBandtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxAntennaBandtype.SelectedIndex == 0)
            {
                panelAntennaFulBeamwidth.Enabled = false;
                panelAntennaHalfBeamwidth.Enabled = false;
                panelAntennaLineDirection.Enabled = false;
            }
            else
            {
                panelAntennaFulBeamwidth.Enabled = true;
                panelAntennaHalfBeamwidth.Enabled = true;
                panelAntennaLineDirection.Enabled = true;
            }
        }

        private void checkBoxViewAntennaFullBeamwidth_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxViewAntennaFullBeamwidth.Checked) { numericUpDownAntennaFullBeamwidth.Enabled = true; }
            else { numericUpDownAntennaFullBeamwidth.Enabled = false; }           
        }

        private void checkBoxViewAntennaHalfBeamwidth_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxViewAntennaFullBeamwidth.Checked) { numericUpDownAntennaHalfBeamwidth.Enabled = true; }
            else { numericUpDownAntennaHalfBeamwidth.Enabled = false; }   
        }

        private void checkBoxViewAntennaDirectionLine_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxViewAntennaFullBeamwidth.Checked) { numericUpDownAntennaLineDistance.Enabled = true; }
            else { numericUpDownAntennaLineDistance.Enabled = false; }   
        }


        #endregion




        #region BUTTONS


        private void buttonOpenEarth_Click(object sender, EventArgs e)
        {
            try
            {
                labelAccuracy.Visible = true;
                pathSaveKmlFile = "";
                folderSaveImageFile = "";
                labelProgressSiteBar.Visible = true;
                labelProgressSectorBar.Visible = true;
                loadFormValusOfComplaint();
                hideFormComplaint();
                if (pathSaveKmlFile == "")
                {
                    SaveFileDialog sf = new SaveFileDialog();
                    sf.FileName = "Dialog_Complaint";
                    sf.Filter = "Google Earth Kml (*.kml*)|*.kml*";
                    if (sf.ShowDialog() == DialogResult.OK) { pathSaveKmlFile = sf.FileName + ".kml"; folderSaveImageFile = sf.FileName + " _googlefiles"; }
                    progressBarSite.Value = 0;
                    labelProgressSiteBar.Text = "Creating Folder/Processing . . . 10 %";
                    Image imageD = Image.FromFile(@"Images\D.png");
                    Image imageM = Image.FromFile(@"Images\M.png");
                    progressBarSite.Value = 1;
                    labelProgressSiteBar.Text = "Creating Images/Processing . . . 11 %";
                    Image imageH = Image.FromFile(@"Images\H.png");
                    Image imageL = Image.FromFile(@"Images\L.png");
                    progressBarSite.Value = 2;
                    labelProgressSiteBar.Text = "Creating Images/Processing . . . 12 %";
                    Image imageE = Image.FromFile(@"Images\E.png");
                    Image imageD5 = Image.FromFile(@"Images\Dx50.png");
                    progressBarSite.Value = 3;
                    labelProgressSiteBar.Text = "Creating Images/Processing . . . 13 %";
                    Image imageM5 = Image.FromFile(@"Images\Mx50.png");
                    Image imageH5 = Image.FromFile(@"Images\Hx50.png");
                    progressBarSite.Value = 4;
                    labelProgressSiteBar.Text = "Creating Images/Processing . . . 14 %";
                    Image imageL5 = Image.FromFile(@"Images\Lx50.png");
                    Image imageE5 = Image.FromFile(@"Images\Ex50.png");
                    progressBarSite.Value = 5;
                    labelProgressSiteBar.Text = "Creating Images/Processing . . . 15 %";
                    if (!File.Exists(folderSaveImageFile))
                    {
                        Directory.CreateDirectory(folderSaveImageFile);
                        imageD.Save(folderSaveImageFile + "\\_site_icon-dialog.png");
                        imageM.Save(folderSaveImageFile + "\\_site_icon-mobitel.png");
                        progressBarSite.Value = 6;
                        labelProgressSiteBar.Text = "Creating Images/Processing . . . 16 %";
                        imageH.Save(folderSaveImageFile + "\\_site_icon-hutch.png");
                        imageL.Save(folderSaveImageFile + "\\_site_icon-lankabell.png");
                        progressBarSite.Value = 7;
                        labelProgressSiteBar.Text = "Creating Images/Processing . . . 17 %";
                        imageE.Save(folderSaveImageFile + "\\_site_icon-etisalat.png");
                        imageD5.Save(folderSaveImageFile + "\\_site_icon-dialog5.png");
                        progressBarSite.Value = 8;
                        labelProgressSiteBar.Text = "Creating Images/Processing . . . 18 %";
                        imageM5.Save(folderSaveImageFile + "\\_site_icon-mobitel5.png");
                        imageH5.Save(folderSaveImageFile + "\\_site_icon-hutch5.png");
                        progressBarSite.Value = 9;
                        labelProgressSiteBar.Text = "Creating Images/Processing . . . 19 %";
                        imageL5.Save(folderSaveImageFile + "\\_site_icon-lankabell5.png");
                        imageE5.Save(folderSaveImageFile + "\\_site_icon-etisalat5.png");
                        Image imageHOME = Image.FromFile(@"Images\HOME.png");
                        imageHOME.Save(folderSaveImageFile + "\\_icon-HOME.png");
                        progressBarSite.Value = 10;
                        labelProgressSiteBar.Text = "Complete Images/Processing . . . 20 %";
                        if (backgroundWorkerSiteCreate.IsBusy != true) { backgroundWorkerSiteCreate.RunWorkerAsync(); }
                    }
                    else
                    {
                        var result = MessageBox.Show("The file path " + pathSaveKmlFile + " already exists. Are you sure you want to save this ?", "File already exists", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            Directory.CreateDirectory(folderSaveImageFile);
                            imageD.Save(folderSaveImageFile + "\\_site_icon-dialog.png");
                            imageM.Save(folderSaveImageFile + "\\_site_icon-mobitel.png");
                            progressBarSite.Value = 6;
                            labelProgressSiteBar.Text = "Creating Images/Processing . . . 16 %";
                            imageH.Save(folderSaveImageFile + "\\_site_icon-hutch.png");
                            imageL.Save(folderSaveImageFile + "\\_site_icon-lankabell.png");
                            progressBarSite.Value = 7;
                            labelProgressSiteBar.Text = "Creating Images/Processing . . . 17 %";
                            imageE.Save(folderSaveImageFile + "\\_site_icon-etisalat.png");
                            imageD5.Save(folderSaveImageFile + "\\_site_icon-dialog5.png");
                            progressBarSite.Value = 8;
                            labelProgressSiteBar.Text = "Creating Images/Processing . . . 18 %";
                            imageM5.Save(folderSaveImageFile + "\\_site_icon-mobitel5.png");
                            imageH5.Save(folderSaveImageFile + "\\_site_icon-hutch5.png");
                            progressBarSite.Value = 9;
                            labelProgressSiteBar.Text = "Creating Images/Processing . . . 19 %";
                            imageL5.Save(folderSaveImageFile + "\\_site_icon-lankabell5.png");
                            imageE5.Save(folderSaveImageFile + "\\_site_icon-etisalat5.png");
                            Image imageHOME = Image.FromFile(@"Images\HOME.png");
                            imageHOME.Save(folderSaveImageFile + "\\_icon-HOME.png");
                            progressBarSite.Value = 10;
                            labelProgressSiteBar.Text = "Complete Images/Processing . . . 20 %";
                            if (backgroundWorkerSiteCreate.IsBusy != true) { backgroundWorkerSiteCreate.RunWorkerAsync(); }
                        }
                    }
                }
            }
            catch (Exception) {
                progressBarSite.Value = 0;
                progressBarSector.Value = 0;
                this.Close();
            }
        }

        private void buttonSaveKml_Click(object sender, EventArgs e)
        {
            try
            {
                labelAccuracy.Visible = true;
                pathSaveKmlFile = "";
                folderSaveImageFile = "";
                labelProgressSiteBar.Visible = true;
                labelProgressSectorBar.Visible = true;
                loadFormValusOfComplaint();
                hideFormComplaint();
                if (pathSaveKmlFile == "")
                {
                    SaveFileDialog sf = new SaveFileDialog();
                    sf.FileName = "Dialog_Complaint";
                    sf.Filter = "Google Earth Kml (*.kml*)|*.kml*";
                    if (sf.ShowDialog() == DialogResult.OK) { pathSaveKmlFile = sf.FileName + ".kml"; folderSaveImageFile = sf.FileName + " _googlefiles"; }
                    progressBarSite.Value = 0;
                    labelProgressSiteBar.Text = "Creating Folder/Processing . . . 10 %";
                    Image imageD = Image.FromFile(@"Images\D.png");
                    Image imageM = Image.FromFile(@"Images\M.png");
                    progressBarSite.Value = 1;
                    labelProgressSiteBar.Text = "Creating Images/Processing . . . 11 %";
                    Image imageH = Image.FromFile(@"Images\H.png");
                    Image imageL = Image.FromFile(@"Images\L.png");
                    progressBarSite.Value = 2;
                    labelProgressSiteBar.Text = "Creating Images/Processing . . . 12 %";
                    Image imageE = Image.FromFile(@"Images\E.png");
                    Image imageD5 = Image.FromFile(@"Images\Dx50.png");
                    progressBarSite.Value = 3;
                    labelProgressSiteBar.Text = "Creating Images/Processing . . . 13 %";
                    Image imageM5 = Image.FromFile(@"Images\Mx50.png");
                    Image imageH5 = Image.FromFile(@"Images\Hx50.png");
                    progressBarSite.Value = 4;
                    labelProgressSiteBar.Text = "Creating Images/Processing . . . 14 %";
                    Image imageL5 = Image.FromFile(@"Images\Lx50.png");
                    Image imageE5 = Image.FromFile(@"Images\Ex50.png");
                    progressBarSite.Value = 5;
                    labelProgressSiteBar.Text = "Creating Images/Processing . . . 15 %";
                    if (!File.Exists(folderSaveImageFile))
                    {
                        Directory.CreateDirectory(folderSaveImageFile);
                        imageD.Save(folderSaveImageFile + "\\_site_icon-dialog.png");
                        imageM.Save(folderSaveImageFile + "\\_site_icon-mobitel.png");
                        progressBarSite.Value = 6;
                        labelProgressSiteBar.Text = "Creating Images/Processing . . . 16 %";
                        imageH.Save(folderSaveImageFile + "\\_site_icon-hutch.png");
                        imageL.Save(folderSaveImageFile + "\\_site_icon-lankabell.png");
                        progressBarSite.Value = 7;
                        labelProgressSiteBar.Text = "Creating Images/Processing . . . 17 %";
                        imageE.Save(folderSaveImageFile + "\\_site_icon-etisalat.png");
                        imageD5.Save(folderSaveImageFile + "\\_site_icon-dialog5.png");
                        progressBarSite.Value = 8;
                        labelProgressSiteBar.Text = "Creating Images/Processing . . . 18 %";
                        imageM5.Save(folderSaveImageFile + "\\_site_icon-mobitel5.png");
                        imageH5.Save(folderSaveImageFile + "\\_site_icon-hutch5.png");
                        progressBarSite.Value = 9;
                        labelProgressSiteBar.Text = "Creating Images/Processing . . . 19 %";
                        imageL5.Save(folderSaveImageFile + "\\_site_icon-lankabell5.png");
                        imageE5.Save(folderSaveImageFile + "\\_site_icon-etisalat5.png");
                        Image imageHOME = Image.FromFile(@"Images\HOME.png");
                        imageHOME.Save(folderSaveImageFile + "\\_icon-HOME.png");
                        progressBarSite.Value = 10;
                        labelProgressSiteBar.Text = "Complete Images/Processing . . . 20 %";
                        if (backgroundWorkerSiteCreate.IsBusy != true) { backgroundWorkerSiteCreate.RunWorkerAsync(); }
                    }
                    else
                    {
                        var result = MessageBox.Show("The file path " + pathSaveKmlFile + " already exists. Are you sure you want to save this ?", "File already exists", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            Directory.CreateDirectory(folderSaveImageFile);
                            imageD.Save(folderSaveImageFile + "\\_site_icon-dialog.png");
                            imageM.Save(folderSaveImageFile + "\\_site_icon-mobitel.png");
                            progressBarSite.Value = 6;
                            labelProgressSiteBar.Text = "Creating Images/Processing . . . 16 %";
                            imageH.Save(folderSaveImageFile + "\\_site_icon-hutch.png");
                            imageL.Save(folderSaveImageFile + "\\_site_icon-lankabell.png");
                            progressBarSite.Value = 7;
                            labelProgressSiteBar.Text = "Creating Images/Processing . . . 17 %";
                            imageE.Save(folderSaveImageFile + "\\_site_icon-etisalat.png");
                            imageD5.Save(folderSaveImageFile + "\\_site_icon-dialog5.png");
                            progressBarSite.Value = 8;
                            labelProgressSiteBar.Text = "Creating Images/Processing . . . 18 %";
                            imageM5.Save(folderSaveImageFile + "\\_site_icon-mobitel5.png");
                            imageH5.Save(folderSaveImageFile + "\\_site_icon-hutch5.png");
                            progressBarSite.Value = 9;
                            labelProgressSiteBar.Text = "Creating Images/Processing . . . 19 %";
                            imageL5.Save(folderSaveImageFile + "\\_site_icon-lankabell5.png");
                            imageE5.Save(folderSaveImageFile + "\\_site_icon-etisalat5.png");
                            Image imageHOME = Image.FromFile(@"Images\HOME.png");
                            imageHOME.Save(folderSaveImageFile + "\\_icon-HOME.png");
                            progressBarSite.Value = 10;
                            labelProgressSiteBar.Text = "Complete Images/Processing . . . 20 %";
                            if (backgroundWorkerSiteCreate.IsBusy != true) { backgroundWorkerSiteCreate.RunWorkerAsync(); }
                        }
                    }
                }
            }
            catch (Exception)
            {
                progressBarSite.Value = 0;
                progressBarSector.Value = 0;
            }
        }

        private void buttonViewComplaint_Click(object sender, EventArgs e)
        {
            DialogSelectAllComplaint selectCustComplaintForm = new DialogSelectAllComplaint(REGION, AID, LID, LOGUSERID, USERNUMBER, ComplaintCustNumber);
            selectCustComplaintForm.ShowDialog();
        }

        private void buttonDeleteComplaint_Click(object sender, EventArgs e)
        {

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (backgroundWorkerSiteCreate.WorkerSupportsCancellation == true)
            {
                backgroundWorkerSiteCreate.CancelAsync();
            }
            backgroundWorkerSiteCreate.CancelAsync();
        }


        #endregion




        #region BACKGROUNDWORK


        private int prestentageProgressDSiteValue = 0;
        private int prestentageProgressOSiteValue = 0;
        private int pro = 0;

        private void backgroundWorkerSiteCreate_DoWork(object sender, DoWorkEventArgs e)
        {
            String kmlSiteData = "";
            BackgroundWorker worker = sender as BackgroundWorker;
            if (checkDialogSitesOK)
            {
                DataTable _dt_SelectCompalaintAreSite = callServer.dialogServerInsert("FUNCTION_BITWEEN_SITES", "@ComLong = " + ComplaintCustLong + ", @ComLat = " + ComplaintCustLat + ", @ComCust = " + (DialogSitesAreaValue / 110530).ToString() + ", 	@owner = 'DIALOG'");
                kmlSiteData = "<Folder>" + Environment.NewLine + "<name>DIALOG_SITE_DATA</name>" + Environment.NewLine;
                prestentageProgressDSiteValue = _dt_SelectCompalaintAreSite.Rows.Count;
                foreach (DataRow dr in _dt_SelectCompalaintAreSite.Rows)
                {
                    worker.ReportProgress(0);
                    if (worker.CancellationPending) { e.Cancel = true; break; }
                    kmlSiteData += "<Folder>" + Environment.NewLine +
                            "<name>" + dr["SiteName"].ToString().ToUpper() + "</name>" + Environment.NewLine +
                            "<Placemark>" + Environment.NewLine +
                            "<name>" + dr["SiteName"].ToString() + "</name>" + Environment.NewLine +
                                "<description><![CDATA[" + Environment.NewLine +
                                "<table  style='width:300px; background:#F08080'>" + Environment.NewLine +
                                "<tr><td style='width:100px; padding: 5px;'>Site ID</td><td style='background:#FFC0CB; font-size: 90%;'>" + dr["SiteID"].ToString() + "</td></tr>" + Environment.NewLine +
                                "<tr><td style='width:100px; padding: 5px;'>Site Name</td><td style='background:#FFC0CB; font-size: 90%;'>" + dr["SiteName"].ToString() + "</td></tr>" + Environment.NewLine +
                                "<tr><td style='width:100px; padding: 5px;'>Site UID Name</td><td style='background:#FFC0CB; font-size: 90%;'>" + dr["SiteUID"].ToString() + "</td></tr>" + Environment.NewLine +
                                "<tr><td style='width:100px; padding: 5px;'>Longitude</td><td style='background:#FFC0CB; font-size: 90%;'>" + dr["Longitude"].ToString() + "</td></tr>" + Environment.NewLine +
                                "<tr><td style='width:100px; padding: 5px;'>Latitude</td><td style='background:#FFC0CB; font-size: 90%;'>" + dr["Latitude"].ToString() + "</td></tr>" + Environment.NewLine +
                                "<tr><td style='width:100px; padding: 5px;'>Site Status</td><td style='background:#FFC0CB; font-size: 90%;'>" + dr["SiteStatus"].ToString() + "</td></tr>" + Environment.NewLine +
                                "<tr><td style='width:100px; padding: 5px;'>Tower Owner</td><td style='background:#FFC0CB; font-size: 90%;'>" + dr["TowerOwner"].ToString() + "</td></tr>" + Environment.NewLine +
                                "<tr><td style='width:100px; padding: 5px;'>Tower Type</td><td style='background:#FFC0CB; font-size: 90%;'>" + dr["TowerType"].ToString() + "</td></tr>" + Environment.NewLine +
                                "</table>" + Environment.NewLine +
                                "]]></description>" + Environment.NewLine +
                            "<styleUrl>#m_ylw-pushpin</styleUrl>" + Environment.NewLine +
                            "<Point>" + Environment.NewLine +
                                "<gx:drawOrder>5</gx:drawOrder>" + Environment.NewLine +
                                "<coordinates>" + dr["Longitude"].ToString() + "," + dr["Latitude"].ToString() + ",0</coordinates>" + Environment.NewLine +
                             "</Point>" + Environment.NewLine +
                            "</Placemark>" + Environment.NewLine;
                    if (checkAntennaBandtype != "") { kmlSiteData += LoadXmalDialogSectors(sender, dr["SiteID"].ToString(), checkAntennaBandtype); }
                    kmlSiteData += "</Folder>" + Environment.NewLine + Environment.NewLine;
                    //worker.ReportProgress(100);
                }
                kmlSiteData += "</Folder>" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            }
            if (OtherSiteOwnerName != "")
            {
                DataTable _dt_SelectCompalaintOtherAreSite = callServer.dialogServerInsert("FUNCTION_BITWEEN_SITES", "@ComLong = " + ComplaintCustLong + ", @ComLat = " + ComplaintCustLat + ", @ComCust = " + (DialogSitesAreaValue / 110530).ToString() + ", 	@owner = '" + OtherSiteOwnerName + "'");
                kmlSiteData += "<Folder>" + Environment.NewLine + "<name>" + OtherSiteOwnerName + "_SITE_DATA</name>" + Environment.NewLine;
                prestentageProgressOSiteValue = _dt_SelectCompalaintOtherAreSite.Rows.Count;
                foreach (DataRow dr in _dt_SelectCompalaintOtherAreSite.Rows)
                {
                    worker.ReportProgress(0);
                    if (worker.CancellationPending) { e.Cancel = true; break; }
                    kmlSiteData += "<Folder>" + Environment.NewLine +
                            "<name>" + dr["SiteName"].ToString().ToUpper() + "</name>" + Environment.NewLine +
                                "<Placemark>" + Environment.NewLine +
                                "<name>" + dr["SiteName"].ToString() + "</name>" + Environment.NewLine +
                                    "<description><![CDATA[" + Environment.NewLine +
                                    "<table  style='width:300px; background:#F08080'>" + Environment.NewLine +
                                    "<tr><td style='width:100px; padding: 5px;'>Site ID</td><td style='background:#FFC0CB; font-size: 90%;'>" + dr["SiteID"].ToString() + "</td></tr>" + Environment.NewLine +
                                    "<tr><td style='width:100px; padding: 5px;'>Site Name</td><td style='background:#FFC0CB; font-size: 90%;'>" + dr["SiteName"].ToString() + "</td></tr>" + Environment.NewLine +
                                    "<tr><td style='width:100px; padding: 5px;'>Longitude</td><td style='background:#FFC0CB; font-size: 90%;'>" + dr["Longitude"].ToString() + "</td></tr>" + Environment.NewLine +
                                    "<tr><td style='width:100px; padding: 5px;'>Latitude</td><td style='background:#FFC0CB; font-size: 90%;'>" + dr["Latitude"].ToString() + "</td></tr>" + Environment.NewLine +
                                    "<tr><td style='width:100px; padding: 5px;'>Site Height</td><td style='background:#FFC0CB; font-size: 90%;'>" + dr["Height"].ToString() + "</td></tr>" + Environment.NewLine +
                                    "<tr><td style='width:100px; padding: 5px;'>Tower Oparater</td><td style='background:#FFC0CB; font-size: 90%;'>" + dr["Oparater"].ToString() + "</td></tr>" + Environment.NewLine +
                                    "</table>" + Environment.NewLine +
                                    "]]></description>" + Environment.NewLine +
                                    "<styleUrl>#m_icon_" + OtherSiteOwnerName.ToLower() + "</styleUrl>" + Environment.NewLine +
                                    "<Point>" + Environment.NewLine +
                                        "<gx:drawOrder>5</gx:drawOrder>" + Environment.NewLine +
                                        "<coordinates>" + dr["Longitude"].ToString() + "," + dr["Latitude"].ToString() + ",0</coordinates>" + Environment.NewLine +
                                    "</Point>" + Environment.NewLine +
                                "</Placemark>" + Environment.NewLine +
                            "</Folder>" + Environment.NewLine + Environment.NewLine;
                    worker.ReportProgress(100);
                }
                kmlSiteData += "</Folder>" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            }

            String Data = loadXmlHeader(folderSaveImageFile) + LoadXmlCustomer(selectCustomerLineLength) + kmlSiteData + loadXmlFooter();
            if (!File.Exists(pathSaveKmlFile))
            {
                File.Create(pathSaveKmlFile).Dispose();
                using (TextWriter tw = new StreamWriter(pathSaveKmlFile)) { tw.Close(); }
                using (StreamWriter outfile = new StreamWriter(pathSaveKmlFile)) { outfile.Write(Data); }
            }
            else { using (StreamWriter outfile = new StreamWriter(pathSaveKmlFile)) { outfile.Write(Data); } System.Diagnostics.Process.Start(pathSaveKmlFile); }


        }

        private void backgroundWorkerSiteCreate_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarSector.Value = e.ProgressPercentage;
            pro = pro + 2;
            if (pro < 60) { progressBarSite.Value = 20 + pro; }
            labelProgressSiteBar.Text = "Site / Processing . . . " + progressBarSite.Value.ToString() + " %";
            labelProgressSectorBar.Text = "Sectors / Processing . . . " + e.ProgressPercentage.ToString() + " %";
        }

        private void backgroundWorkerSiteCreate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled) { labelProgressSiteBar.Text = "Processing Canceled !"; }
            else if (e.Error != null) { MessageBox.Show("Sorry, Processing kml file error. Try again." + Environment.NewLine + e.ToString(), "Kml File Error", MessageBoxButtons.OK, MessageBoxIcon.Error); this.Close(); }
            else
            {
                labelProgressSiteBar.Text = "Processing Completed . . . 100 %";
                labelProgressSectorBar.Text = "Processing Completed . . . 100 %";
                progressBarSite.Value =100;
                progressBarSector.Value = 100;
                showFormComplaint();
                MessageBox.Show("The kml file created successfully", "Google Kml File", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                System.Diagnostics.Process.Start(pathSaveKmlFile);
            }
        }


        #endregion




        #region KML FUNCTION


        private String loadXmlHeader(String imagePath)
        {
            return "<?xml version='1.0' encoding='UTF-8'?>" + Environment.NewLine + "<kml xmlns='http://www.opengis.net/kml/2.2' xmlns:gx='http://www.google.com/kml/ext/2.2' xmlns:kml='http://www.opengis.net/kml/2.2' xmlns:atom='http://www.w3.org/2005/Atom'>" + Environment.NewLine +
                                    "<Document>" + Environment.NewLine + "<name>DIALOG_" + checkAntennaBandtype + "</name>" + Environment.NewLine + Environment.NewLine +


                                     "<Style id='sh_ylw-pushpin_red'>" + Environment.NewLine +
                                        "<IconStyle>" + Environment.NewLine +
                                            "<scale>1.3</scale>" + Environment.NewLine +
                                            "<Icon>" + Environment.NewLine +
                                                "<href>http://maps.google.com/mapfiles/kml/pushpin/ylw-pushpin.png</href>" + Environment.NewLine +
                                            "</Icon>" + Environment.NewLine +
                                            "<hotSpot x='20' y='2' xunits='pixels' yunits='pixels'/>" + Environment.NewLine +
                                        "</IconStyle>" + Environment.NewLine +
                                        "<PolyStyle>" + Environment.NewLine +
                                            "<color>660000ff</color>" + Environment.NewLine +
                                            "<outline>0</outline>" + Environment.NewLine +
                                        "</PolyStyle>" + Environment.NewLine +
                                    "</Style>" + Environment.NewLine + Environment.NewLine +

                                    "<StyleMap id='msn_ylw-pushpin_red'>" + Environment.NewLine +
                                        "<Pair>" + Environment.NewLine +
                                            "<key>normal</key>" + Environment.NewLine +
                                            "<styleUrl>#sn_ylw-pushpin_red</styleUrl>" + Environment.NewLine +
                                        "</Pair>" + Environment.NewLine +
                                        "<Pair>" + Environment.NewLine +
                                            "<key>highlight</key>" + Environment.NewLine +
                                            "<styleUrl>#sh_ylw-pushpin_red</styleUrl>" + Environment.NewLine +
                                        "</Pair>" + Environment.NewLine +
                                    "</StyleMap>" + Environment.NewLine + Environment.NewLine +

                                    "<Style id='sn_ylw-pushpin_red'>" + Environment.NewLine +
                                        "<IconStyle>" + Environment.NewLine +
                                            "<scale>1.1</scale>" + Environment.NewLine +
                                            "<Icon>" + Environment.NewLine +
                                                "<href>http://maps.google.com/mapfiles/kml/pushpin/ylw-pushpin.png</href>" + Environment.NewLine +
                                            "</Icon>" + Environment.NewLine +
                                            "<hotSpot x='20' y='2' xunits='pixels' yunits='pixels'/>" + Environment.NewLine +
                                        "</IconStyle>" + Environment.NewLine +
                                        "<PolyStyle>" + Environment.NewLine +
                                            "<color>660000ff</color>" + Environment.NewLine +
                                            "<outline>0</outline>" + Environment.NewLine +
                                        "</PolyStyle>" + Environment.NewLine +
                                    "</Style>" + Environment.NewLine + Environment.NewLine +




                                    "<StyleMap id='msn_ylw-pushpin'>" + Environment.NewLine +
                                        "<Pair>" + Environment.NewLine +
                                            "<key>normal</key>" + Environment.NewLine +
                                            "<styleUrl>#sn_ylw-pushpin</styleUrl>" + Environment.NewLine +
                                        "</Pair>" + Environment.NewLine +
                                        "<Pair>" + Environment.NewLine +
                                            "<key>highlight</key>" + Environment.NewLine +
                                            "<styleUrl>#sh_ylw-pushpin</styleUrl>" + Environment.NewLine +
                                        "</Pair>" + Environment.NewLine +
                                    "</StyleMap>" + Environment.NewLine + Environment.NewLine +

                                        "<Style id='sh_ylw-pushpin'>" + Environment.NewLine +
                                        "<IconStyle>" + Environment.NewLine +
                                            "<scale>1.3</scale>" + Environment.NewLine +
                                            "<Icon>" + Environment.NewLine +
                                                "<href>http://maps.google.com/mapfiles/kml/pushpin/ylw-pushpin.png</href>" + Environment.NewLine +
                                            "</Icon>" + Environment.NewLine +
                                            "<hotSpot x='20' y='2' xunits='pixels' yunits='pixels'/>" + Environment.NewLine +
                                        "</IconStyle>" + Environment.NewLine +
                                        "<PolyStyle>" + Environment.NewLine +
                                            "<color>66ffff00</color>" + Environment.NewLine +
                                            "<outline>0</outline>" + Environment.NewLine +
                                        "</PolyStyle>" + Environment.NewLine +
                                    "</Style>" + Environment.NewLine + Environment.NewLine +

                                    "<Style id='sn_ylw-pushpin'>" + Environment.NewLine +
                                        "<IconStyle>" + Environment.NewLine +
                                            "<scale>1.1</scale>" + Environment.NewLine +
                                            "<Icon>" + Environment.NewLine +
                                                "<href>http://maps.google.com/mapfiles/kml/pushpin/ylw-pushpin.png</href>" + Environment.NewLine +
                                            "</Icon>" + Environment.NewLine +
                                            "<hotSpot x='20' y='2' xunits='pixels' yunits='pixels'/>" + Environment.NewLine +
                                        "</IconStyle>" + Environment.NewLine +
                                        "<PolyStyle>" + Environment.NewLine +
                                            "<color>66ffff00</color>" + Environment.NewLine +
                                            "<outline>0</outline>" + Environment.NewLine +
                                        "</PolyStyle>" + Environment.NewLine +
                                    "</Style>" + Environment.NewLine + Environment.NewLine +





                                    "<Style id='inline'>" + Environment.NewLine +
                                        "<LineStyle>" + Environment.NewLine +
                                        "<color>ff0000ff</color>" + Environment.NewLine +
                                        "<width>2</width>" + Environment.NewLine +
                                        "</LineStyle>" + Environment.NewLine +
                                        "<PolyStyle>" + Environment.NewLine +
                                        "<fill>0</fill>" + Environment.NewLine +
                                        "</PolyStyle>" + Environment.NewLine +
                                    "</Style>" + Environment.NewLine + Environment.NewLine +

                                    "<Style id='inline1'>" + Environment.NewLine +
                                        "<LineStyle>" + Environment.NewLine +
                                        "<color>ff0000ff</color>" + Environment.NewLine +
                                        "<width>2</width>" + Environment.NewLine +
                                        "</LineStyle>" + Environment.NewLine +
                                        "<PolyStyle>" + Environment.NewLine +
                                        "<fill>0</fill>" + Environment.NewLine +
                                        "</PolyStyle>" + Environment.NewLine +
                                    "</Style>" + Environment.NewLine + Environment.NewLine +

                                    "<StyleMap id='inline0'>" + Environment.NewLine +
                                        "<Pair>" + Environment.NewLine +
                                            "<key>normal</key>" + Environment.NewLine +
                                            "<styleUrl>#inline</styleUrl>" + Environment.NewLine +
                                        "</Pair>" + Environment.NewLine +
                                        "<Pair>" + Environment.NewLine +
                                            "<key>highlight</key>" + Environment.NewLine +
                                            "<styleUrl>#inline1</styleUrl>" + Environment.NewLine +
                                        "</Pair>" + Environment.NewLine +
                                    "</StyleMap>" + Environment.NewLine + Environment.NewLine +

                                    "<Style id='inline2'>" + Environment.NewLine +
                                        "<LineStyle>" + Environment.NewLine +
                                        "<color>ffffaa00</color>" + Environment.NewLine +
                                        "<width>2</width>" + Environment.NewLine +
                                        "</LineStyle>" + Environment.NewLine +
                                        "<PolyStyle>" + Environment.NewLine +
                                        "<fill>0</fill>" + Environment.NewLine +
                                        "</PolyStyle>" + Environment.NewLine +
                                    "</Style>" + Environment.NewLine + Environment.NewLine +

                                    "<Style id='inline3'>" + Environment.NewLine +
                                        "<LineStyle>" + Environment.NewLine +
                                        "<color>ffffaa00</color>" + Environment.NewLine +
                                        "<width>2</width>" + Environment.NewLine +
                                        "</LineStyle>" + Environment.NewLine +
                                        "<PolyStyle>" + Environment.NewLine +
                                        "<fill>0</fill>" + Environment.NewLine +
                                        "</PolyStyle>" + Environment.NewLine +
                                    "</Style>" + Environment.NewLine + Environment.NewLine +

                                    "<StyleMap id='inline4'>" + Environment.NewLine +
                                        "<Pair>" + Environment.NewLine +
                                            "<key>normal</key>" + Environment.NewLine +
                                            "<styleUrl>#inline2</styleUrl>" + Environment.NewLine +
                                        "</Pair>" + Environment.NewLine +
                                        "<Pair>" + Environment.NewLine +
                                            "<key>highlight</key>" + Environment.NewLine +
                                            "<styleUrl>#inline3</styleUrl>" + Environment.NewLine +
                                        "</Pair>" + Environment.NewLine +
                                    "</StyleMap>" + Environment.NewLine + Environment.NewLine +



                                    "<Style id='s_ylw-pushpin'>" + Environment.NewLine +
                                        "<IconStyle>" + Environment.NewLine +
                                        "<scale>1.1</scale>" + Environment.NewLine +
                                        "<Icon>" + Environment.NewLine +
                                        "<href>" + imagePath + "\\_site_icon-dialog.png</href>" + Environment.NewLine +
                                        "</Icon>" + Environment.NewLine +
                                        "<hotSpot x='20' y='2' xunits='pixels' yunits='pixels'/>" + Environment.NewLine +
                                        "</IconStyle>" + Environment.NewLine +
                                    "</Style>" + Environment.NewLine + Environment.NewLine +

                                    "<Style id='s_ylw-pushpin_hl'>" + Environment.NewLine +
                                        "<IconStyle>" + Environment.NewLine +
                                        "<scale>1.3</scale>" + Environment.NewLine +
                                        "<Icon>" + Environment.NewLine +
                                        "<href>" + imagePath + "\\_site_icon-dialog.png</href>" + Environment.NewLine +
                                        "</Icon>" + Environment.NewLine +
                                        "<hotSpot x='20' y='2' xunits='pixels' yunits='pixels'/>" + Environment.NewLine +
                                        "</IconStyle>" + Environment.NewLine +
                                    "</Style>" + Environment.NewLine + Environment.NewLine +

                                    "<StyleMap id='m_ylw-pushpin'>" + Environment.NewLine +
                                        "<Pair>" + Environment.NewLine +
                                            "<key>normal</key>" + Environment.NewLine +
                                            "<styleUrl>#s_ylw-pushpin</styleUrl>" + Environment.NewLine +
                                        "</Pair>" + Environment.NewLine +
                                        "<Pair>" + Environment.NewLine +
                                            "<key>highlight</key>" +
                                            "<styleUrl>#s_ylw-pushpin_hl</styleUrl>" + Environment.NewLine +
                                        "</Pair>" + Environment.NewLine +
                                    "</StyleMap>" + Environment.NewLine + Environment.NewLine +


                                    "<Style id='s_site_mobitel'>" + Environment.NewLine +
                                        "<IconStyle>" + Environment.NewLine +
                                        "<scale>1.1</scale>" + Environment.NewLine +
                                        "<Icon>" + Environment.NewLine +
                                        "<href>" + imagePath + "\\_site_icon-mobitel.png</href>" + Environment.NewLine +
                                        "</Icon>" + Environment.NewLine +
                                        "<hotSpot x='20' y='2' xunits='pixels' yunits='pixels'/>" + Environment.NewLine +
                                        "</IconStyle>" + Environment.NewLine +
                                    "</Style>" + Environment.NewLine + Environment.NewLine +

                                    "<Style id='s_site_mobitel_hl'>" + Environment.NewLine +
                                        "<IconStyle>" + Environment.NewLine +
                                        "<scale>1.3</scale>" + Environment.NewLine +
                                        "<Icon>" + Environment.NewLine +
                                        "<href>" + imagePath + "\\_site_icon-mobitel.png</href>" + Environment.NewLine +
                                        "</Icon>" + Environment.NewLine +
                                        "<hotSpot x='20' y='2' xunits='pixels' yunits='pixels'/>" + Environment.NewLine +
                                        "</IconStyle>" + Environment.NewLine +
                                    "</Style>" + Environment.NewLine + Environment.NewLine +

                                    "<StyleMap id='m_icon_mobitel'>" + Environment.NewLine +
                                        "<Pair>" + Environment.NewLine +
                                            "<key>normal</key>" + Environment.NewLine +
                                            "<styleUrl>#s_site_mobitel</styleUrl>" + Environment.NewLine +
                                        "</Pair>" + Environment.NewLine +
                                        "<Pair>" + Environment.NewLine +
                                            "<key>highlight</key>" +
                                            "<styleUrl>#s_site_mobitel_hl</styleUrl>" + Environment.NewLine +
                                        "</Pair>" + Environment.NewLine +
                                    "</StyleMap>" + Environment.NewLine + Environment.NewLine +


                                    "<Style id='s_site_lankabell'>" + Environment.NewLine +
                                        "<IconStyle>" + Environment.NewLine +
                                        "<scale>1.1</scale>" + Environment.NewLine +
                                        "<Icon>" + Environment.NewLine +
                                        "<href>" + imagePath + "\\_site_icon-lankabell.png</href>" + Environment.NewLine +
                                        "</Icon>" + Environment.NewLine +
                                        "<hotSpot x='20' y='2' xunits='pixels' yunits='pixels'/>" + Environment.NewLine +
                                        "</IconStyle>" + Environment.NewLine +
                                    "</Style>" + Environment.NewLine + Environment.NewLine +

                                    "<Style id='s_site_lankabell_hl'>" + Environment.NewLine +
                                        "<IconStyle>" + Environment.NewLine +
                                        "<scale>1.3</scale>" + Environment.NewLine +
                                        "<Icon>" + Environment.NewLine +
                                        "<href>" + imagePath + "\\_site_icon-lankabell.png</href>" + Environment.NewLine +
                                        "</Icon>" + Environment.NewLine +
                                        "<hotSpot x='20' y='2' xunits='pixels' yunits='pixels'/>" + Environment.NewLine +
                                        "</IconStyle>" + Environment.NewLine +
                                    "</Style>" + Environment.NewLine + Environment.NewLine +

                                    "<StyleMap id='m_icon_lankabell'>" + Environment.NewLine +
                                        "<Pair>" + Environment.NewLine +
                                            "<key>normal</key>" + Environment.NewLine +
                                            "<styleUrl>#s_site_lankabell</styleUrl>" + Environment.NewLine +
                                        "</Pair>" + Environment.NewLine +
                                        "<Pair>" + Environment.NewLine +
                                            "<key>highlight</key>" +
                                            "<styleUrl>#s_site_lankabell_hl</styleUrl>" + Environment.NewLine +
                                        "</Pair>" + Environment.NewLine +
                                    "</StyleMap>" + Environment.NewLine + Environment.NewLine +


                                    "<Style id='s_site_hutch'>" + Environment.NewLine +
                                        "<IconStyle>" + Environment.NewLine +
                                        "<scale>1.1</scale>" + Environment.NewLine +
                                        "<Icon>" + Environment.NewLine +
                                        "<href>" + imagePath + "\\_site_icon-hutch.png</href>" + Environment.NewLine +
                                        "</Icon>" + Environment.NewLine +
                                        "<hotSpot x='20' y='2' xunits='pixels' yunits='pixels'/>" + Environment.NewLine +
                                        "</IconStyle>" + Environment.NewLine +
                                    "</Style>" + Environment.NewLine + Environment.NewLine +

                                    "<Style id='s_site_hutch_hl'>" + Environment.NewLine +
                                        "<IconStyle>" + Environment.NewLine +
                                        "<scale>1.3</scale>" + Environment.NewLine +
                                        "<Icon>" + Environment.NewLine +
                                        "<href>" + imagePath + "\\_site_icon-hutch.png</href>" + Environment.NewLine +
                                        "</Icon>" + Environment.NewLine +
                                        "<hotSpot x='20' y='2' xunits='pixels' yunits='pixels'/>" + Environment.NewLine +
                                        "</IconStyle>" + Environment.NewLine +
                                    "</Style>" + Environment.NewLine + Environment.NewLine +

                                    "<StyleMap id='m_icon_hutch'>" + Environment.NewLine +
                                        "<Pair>" + Environment.NewLine +
                                            "<key>normal</key>" + Environment.NewLine +
                                            "<styleUrl>#s_site_hutch</styleUrl>" + Environment.NewLine +
                                        "</Pair>" + Environment.NewLine +
                                        "<Pair>" + Environment.NewLine +
                                            "<key>highlight</key>" +
                                            "<styleUrl>#s_site_hutch_hl</styleUrl>" + Environment.NewLine +
                                        "</Pair>" + Environment.NewLine +
                                    "</StyleMap>" + Environment.NewLine + Environment.NewLine +


                                    "<Style id='s_site_etisalat'>" + Environment.NewLine +
                                        "<IconStyle>" + Environment.NewLine +
                                        "<scale>1.1</scale>" + Environment.NewLine +
                                        "<Icon>" + Environment.NewLine +
                                        "<href>" + imagePath + "\\_site_icon-etisalat.png</href>" + Environment.NewLine +
                                        "</Icon>" + Environment.NewLine +
                                        "<hotSpot x='20' y='2' xunits='pixels' yunits='pixels'/>" + Environment.NewLine +
                                        "</IconStyle>" + Environment.NewLine +
                                    "</Style>" + Environment.NewLine + Environment.NewLine +

                                    "<Style id='s_site_etisalat_hl'>" + Environment.NewLine +
                                        "<IconStyle>" + Environment.NewLine +
                                        "<scale>1.3</scale>" + Environment.NewLine +
                                        "<Icon>" + Environment.NewLine +
                                        "<href>" + imagePath + "\\_site_icon-etisalat.png</href>" + Environment.NewLine +
                                        "</Icon>" + Environment.NewLine +
                                        "<hotSpot x='20' y='2' xunits='pixels' yunits='pixels'/>" + Environment.NewLine +
                                        "</IconStyle>" + Environment.NewLine +
                                    "</Style>" + Environment.NewLine + Environment.NewLine +

                                    "<StyleMap id='m_icon_etisalat'>" + Environment.NewLine +
                                        "<Pair>" + Environment.NewLine +
                                            "<key>normal</key>" + Environment.NewLine +
                                            "<styleUrl>#s_site_etisalat</styleUrl>" + Environment.NewLine +
                                        "</Pair>" + Environment.NewLine +
                                        "<Pair>" + Environment.NewLine +
                                            "<key>highlight</key>" +
                                            "<styleUrl>#s_site_etisalat_hl</styleUrl>" + Environment.NewLine +
                                        "</Pair>" + Environment.NewLine +
                                    "</StyleMap>" + Environment.NewLine + Environment.NewLine +



                                    "<Style id='s_ylw-pushpin_home'>" + Environment.NewLine +
                                        "<IconStyle>" + Environment.NewLine +
                                        "<scale>1.1</scale>" + Environment.NewLine +
                                        "<Icon>" + Environment.NewLine +
                                        "<href>" + imagePath + "\\_icon-HOME.png</href>" + Environment.NewLine +
                                        "</Icon>" + Environment.NewLine +
                                        "<hotSpot x='20' y='2' xunits='pixels' yunits='pixels'/>" + Environment.NewLine +
                                        "</IconStyle>" + Environment.NewLine +
                                    "</Style>" + Environment.NewLine + Environment.NewLine +

                                    "<Style id='s_ylw-pushpin_hl_home'>" + Environment.NewLine +
                                        "<IconStyle>" + Environment.NewLine +
                                        "<scale>1.3</scale>" + Environment.NewLine +
                                        "<Icon>" + Environment.NewLine +
                                        "<href>" + imagePath + "\\_icon-HOME.png</href>" + Environment.NewLine +
                                        "</Icon>" + Environment.NewLine +
                                        "<hotSpot x='20' y='2' xunits='pixels' yunits='pixels'/>" + Environment.NewLine +
                                        "</IconStyle>" + Environment.NewLine +
                                    "</Style>" + Environment.NewLine + Environment.NewLine +

                                    "<StyleMap id='m_ylw-pushpin_home'>" + Environment.NewLine +
                                        "<Pair>" + Environment.NewLine +
                                            "<key>normal</key>" + Environment.NewLine +
                                            "<styleUrl>#s_ylw-pushpin_home</styleUrl>" + Environment.NewLine +
                                        "</Pair>" + Environment.NewLine +
                                        "<Pair>" + Environment.NewLine +
                                            "<key>highlight</key>" +
                                            "<styleUrl>#s_ylw-pushpin_hl_home</styleUrl>" + Environment.NewLine +
                                        "</Pair>" + Environment.NewLine +
                                    "</StyleMap>" + Environment.NewLine + Environment.NewLine +

                                    "<Style id='sh_ylw-pushpin_HOME_CIRCAL'>" + Environment.NewLine +
                                        "<IconStyle>" + Environment.NewLine +
                                            "<scale>1.3</scale>" + Environment.NewLine +
                                            "<Icon>" + Environment.NewLine +
                                        "<href>http://maps.google.com/mapfiles/kml/pushpin/ylw-pushpin.png</href>" + Environment.NewLine +
                                        "</Icon>" + Environment.NewLine +
                                            "<hotSpot x='20' y='2' xunits='pixels' yunits='pixels'/>" + Environment.NewLine +
                                        "</IconStyle>" + Environment.NewLine +
                                        "<PolyStyle>" + Environment.NewLine +
                                            "<color>66ffff00</color>" + Environment.NewLine +
                                            "<fill>0</fill>" + Environment.NewLine +
                                        "</PolyStyle>" + Environment.NewLine +
                                    "</Style>" + Environment.NewLine +

                                    "<Style id='sn_ylw-pushpin_HOME_CIRCAL'>" + Environment.NewLine +
                                        "<IconStyle>" + Environment.NewLine +
                                            "<scale>1.1</scale>" + Environment.NewLine +
                                            "<Icon>" + Environment.NewLine +
                                        "<href>http://maps.google.com/mapfiles/kml/pushpin/ylw-pushpin.png</href></Icon>" + Environment.NewLine +
                                            "<hotSpot x='20' y='2' xunits='pixels' yunits='pixels'/>" + Environment.NewLine +
                                        "</IconStyle>" + Environment.NewLine +
                                        "<PolyStyle>" + Environment.NewLine +
                                            "<color>66ffff00</color>" + Environment.NewLine +
                                            "<fill>0</fill>" + Environment.NewLine +
                                        "</PolyStyle>" + Environment.NewLine +
                                    "</Style>" + Environment.NewLine +

                                    "<StyleMap id='msn_ylw-pushpin_HOME_CIRCAL'>" + Environment.NewLine +
                                        "<Pair>" + Environment.NewLine +
                                            "<key>normal</key>" + Environment.NewLine +
                                            "<styleUrl>#sn_ylw-pushpin_HOME_CIRCAL</styleUrl>" + Environment.NewLine +
                                        "</Pair>" + Environment.NewLine +
                                        "<Pair>" + Environment.NewLine +
                                            "<key>highlight</key>" + Environment.NewLine +
                                            "<styleUrl>#sh_ylw-pushpin_HOME_CIRCAL</styleUrl>" + Environment.NewLine +
                                        "</Pair>" + Environment.NewLine +
                                    "</StyleMap>" + Environment.NewLine +


                                    "<Folder>" + Environment.NewLine +
                                    "<name>DIALOG_CUSTOMER_LOCATION</name>" + Environment.NewLine + Environment.NewLine;

        }

        private String loadXmlFooter()
        {
            return "</Folder>" + Environment.NewLine + "</Document>" + Environment.NewLine + "</kml>" + Environment.NewLine;
        }

        private String LoadXmlCustomer(int custArea)
        {
            String xmlCustomerData;
            Double[] DegreeArr = { -180, -175, -170, -165, -160, -155, -150, -145, -140, -135, -130, -125, -120, -115, -110, -105, -100, -95, -90, -85, -80, -75, -70, -65, -60, -55, -50, -45, -40, -35, -25, -20, -15, -10, -5, 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105, 110, 115, 120, 125, 130, 135, 140, 145, 150, 155, 160, 165, 170, 175, 180 };

            xmlCustomerData = "<Folder>" + Environment.NewLine +
                                "<name>CUSTOMER_DATA</name>" + Environment.NewLine +
                                "<open>1</open>" + Environment.NewLine +
                                "<Placemark>" + Environment.NewLine +
                                    "<name>SMS Customer</name>" + Environment.NewLine +
                                        "<description><![CDATA[" + Environment.NewLine +
                                        "<table  style='width:300px; background:#33CC66'>" + Environment.NewLine +
                                            "<tr><td style='width:100px; padding: 5px;'>Number</td><td style='background:#99FFCC; font-size: 90%;'>" + ComplaintCustNumber + "</td></tr>" + Environment.NewLine +
                                            "<tr><td style='width:100px; padding: 5px;'>Name</td><td style='background:#99FFCC; font-size: 90%;'>SMS Customer</td></tr>" + Environment.NewLine +
                                            "<tr><td style='width:100px; padding: 5px;'>Longitude</td><td style='background:#99FFCC; font-size: 90%;'>" + ComplaintCustLong + "</td></tr>" + Environment.NewLine +
                                            "<tr><td style='width:100px; padding: 5px;'>Latitude</td><td style='background:#99FFCC; font-size: 90%;'>" + ComplaintCustLat + "</td></tr>" + Environment.NewLine +
                                            "<tr><td style='width:100px; padding: 5px;'>Complaint</td><td style='background:#99FFCC; font-size: 90%;'>" + ComplaintCustData + "</td></tr>" + Environment.NewLine +
                                            "<tr><td style='width:100px; padding: 5px;'>SMS Date</td><td style='background:#99FFCC; font-size: 90%;'>" + ComplaintCustSMSDate + "</td></tr>" + Environment.NewLine +
                                        "</table>" + Environment.NewLine +
                                        "]]></description>" + Environment.NewLine +
                                    "<styleUrl>#m_ylw-pushpin_home</styleUrl>" + Environment.NewLine +
                                    "<Point>" + Environment.NewLine +
                                        "<gx:drawOrder>5</gx:drawOrder>" + Environment.NewLine +
                                        "<coordinates>" + ComplaintCustLong + "," + ComplaintCustLat + ",0</coordinates>" + Environment.NewLine +
                                        "</Point>" + Environment.NewLine +
                                "</Placemark>" + Environment.NewLine;

            xmlCustomerData += "<Placemark><name>LayerGSM</name><open>1</open><styleUrl>#msn_ylw-pushpin_HOME_CIRCAL</styleUrl><Polygon><tessellate>1</tessellate><outerBoundaryIs><LinearRing><coordinates>";
            for (int i = 0; i < DegreeArr.Length; i++) { xmlCustomerData += getLocationLongitude(Convert.ToDouble(ComplaintCustLong), (0.000904 * custArea), DegreeArr[i]) + "," + getLocationLatitude(Convert.ToDouble(ComplaintCustLat), (0.000904 * custArea), DegreeArr[i]) + ",0 "; }
            xmlCustomerData += "</coordinates></LinearRing></outerBoundaryIs></Polygon></Placemark>";

            xmlCustomerData += "</Folder>" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            return xmlCustomerData;
        }

        private String LoadXmalDialogSectors(object sender, String SiteID, String BandType)
        {
            BackgroundWorker SectorWorker = sender as BackgroundWorker;
            String xmlSectorData = "";
            DataTable _dt_SelectSiteSector = callServer.dialogServerInsert("FUNCTION_SECTORS", " @SiteID = '" + SiteID + "', @SectorBand = '" + BandType + "'");
            foreach (DataRow dr in _dt_SelectSiteSector.Rows)
            {
                xmlSectorData += "<Folder>" + Environment.NewLine + "<name>" + dr["SectorID"].ToString().ToUpper() + "</name>" + Environment.NewLine;
                xmlSectorData += LoadXmalDialogAntenna(dr["SiteID"].ToString(), dr["AntennaID"].ToString());
                xmlSectorData += "</Folder>" + Environment.NewLine;
                SectorWorker.ReportProgress(100);
            }
            return xmlSectorData;
        }

        private String LoadXmalDialogAntenna(String SiteID, String AntennaID)
        {
            Double coordinatesLat = 0;
            Double coordinatesLog = 0;
            String xmlAntennaData = "";
            DataTable _dt_SelectSiteAntenna = callServer.dialogServerInsert("FUNCTION_ANTENNA", "@SiteID = '" + SiteID + "', @AntennaID	= '" + AntennaID + "'");
            foreach (DataRow dr in _dt_SelectSiteAntenna.Rows)
            {
                coordinatesLat = getLocationLatitude(Convert.ToDouble(dr["Latitude"]), Convert.ToDouble(AntennaDirectionValue / 110530), Convert.ToDouble(dr["Azimuth"]));
                coordinatesLog = getLocationLongitude(Convert.ToDouble(dr["Longitude"]), Convert.ToDouble(AntennaDirectionValue / 110530), Convert.ToDouble(dr["Azimuth"]));
                if (checkDirectionLineOK)
                {
                    xmlAntennaData += "<Placemark>" + Environment.NewLine +
                                        "<name>Antenna ID : " + dr["AntennaID"].ToString() + "</name>" + Environment.NewLine +
                                            "<description><![CDATA[" + Environment.NewLine +
                                            "<table  style='width:400px; background:#208080'>" + Environment.NewLine +
                                                "<tr><td style='width:160px; padding: 2px;'>Antenna ID</td><td style='background:#FFC0CB; font-size: 90%;'>" + dr["AntennaID"].ToString() + "</td></tr>" + Environment.NewLine +
                                                "<tr><td style='width:160px; padding: 2px;'>Antenna Type</td><td style='background:#FFC0CB; font-size: 90%;'>" + dr["AntennaType"].ToString() + "</td></tr>" + Environment.NewLine +
                                                "<tr><td style='width:160px; padding: 2px;'>Antenna Height(m)</td><td style='background:#FFC0CB; font-size: 90%;'>" + dr["Height"].ToString() + "</td></tr>" + Environment.NewLine +
                                                "<tr><td style='width:160px; padding: 2px;'>Antenna Azimuth</td><td style='background:#FFC0CB; font-size: 90%;'>" + dr["Azimuth"].ToString() + "</td></tr>" + Environment.NewLine +
                                                "<tr><td style='width:160px; padding: 2px;'>Antenna Tilt(M)</td><td style='background:#FFC0CB; font-size: 90%;'>" + dr["MecTilt"].ToString() + "</td></tr>" + Environment.NewLine +
                                                "<tr><td style='width:160px; padding: 2px;'>Port Name</td><td style='background:#FFC0CB; font-size: 90%;'>" + dr["PortName"].ToString() + "</td></tr>" + Environment.NewLine +
                                                "<tr><td style='width:160px; padding: 2px;'>MinFrequency_MHz</td><td style='background:#FFC0CB; font-size: 90%;'>" + dr["MinFrequency_MHz"].ToString() + "</td></tr>" + Environment.NewLine +
                                                "<tr><td style='width:160px; padding: 2px;'>MaxFrequency_MHz</td><td style='background:#FFC0CB; font-size: 90%;'>" + dr["MaxFrequency_MHz"].ToString() + "</td></tr>" + Environment.NewLine +
                                            "</table>" + Environment.NewLine +
                                            "]]></description>" + Environment.NewLine +
                                            "<styleUrl>#inline4</styleUrl>" + Environment.NewLine +
                                        "<LineString>" + Environment.NewLine +
                                            "<tessellate>1</tessellate>" + Environment.NewLine +
                                            "<coordinates>" + coordinatesLog.ToString() + "," + coordinatesLat.ToString() + ",0 " + dr["Longitude"].ToString() + "," + dr["Latitude"].ToString() + ",0</coordinates>" + Environment.NewLine +
                                        "</LineString>" + Environment.NewLine +
                                        "</Placemark>" + Environment.NewLine;
                }
                if (checkAntennaFullBeamwidthOK) { xmlAntennaData += createFulBeamWidth(Convert.ToDouble(dr["Longitude"]), Convert.ToDouble(dr["Latitude"]), Convert.ToDouble(dr["Azimuth"]), dr["AntennaID"].ToString()); }
                if (checkAntennaHalfBeamwidthOK) { xmlAntennaData += createHalfBeamWidth(Convert.ToDouble(dr["Longitude"]), Convert.ToDouble(dr["Latitude"]), Convert.ToDouble(dr["Azimuth"]), dr["AntennaID"].ToString(), dr["AntennaType"].ToString(), dr["Height"].ToString(), dr["Azimuth"].ToString(), dr["MecTilt"].ToString(), dr["PortName"].ToString(), dr["MinFrequency_MHz"].ToString(), dr["MaxFrequency_MHz"].ToString()); }
            }
            return xmlAntennaData;
        }

        private String createFulBeamWidth(Double Longitude, Double Latitude, Double Degree, String AntennaID)
        {
            String DData = "";
            Double AnternaLength = Convert.ToDouble(AntennaFullBeamwidthValue);
            Double[] DegreeArr = { -175, -170, -165, -160, -155, -150, -145, -140, -135, -130, -125, -120, -115, -110, -105, -100, -95, -90, -85, -80, -75, -70, -65, -60, -55, -50, -45, -40, -35, -25, -20, -15, -10, -5, 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105, 110, 115, 120, 125, 130, 135, 140, 145, 150, 155, 160, 165, 170, 175, 180 };
            Double[] LengthArr = { 0.02, 0.04, 0.16, 0.32, 0.64, 1, 1.2, 1.5, 2, 2.8, 3.1, 4.6, 5.2, 6.2, 7, 7.8, 8.6, 9.4, 10.2, 11.8, 12.6, 13.3, 14, 14.7, 13.3, 14.1, 14.8, 15.4, 15.9, 16.3, 16.6, 16.8, 16.9, 17, 17, 17, 17, 17, 16.9, 16.8, 16.6, 16.3, 15.9, 15.4, 14.8, 14.1, 13.3, 12.4, 11.4, 10.1, 12, 11.4, 10.6, 9.8, 8.8, 7.8, 7, 6.2, 5.2, 4.6, 3.1, 2.8, 2, 1.5, 1.2, 1, 0.64, 0.32, 0.16, 0.04, 0.02 };
            DData += "<Placemark><name>Antenna ID : " + AntennaID + " ( Max_Full_BeamWidth )</name><open>1</open><styleUrl>#msn_ylw-pushpin</styleUrl><Polygon><tessellate>1</tessellate><outerBoundaryIs><LinearRing><coordinates>";
            for (int i = 0; i < DegreeArr.Length; i++) { DData += getLocationLongitude(Longitude, (AnternaLength * LengthArr[i]) / 2300000, (Degree + DegreeArr[i])) + "," + getLocationLatitude(Latitude, (AnternaLength * LengthArr[i]) / 2300000, (Degree + DegreeArr[i])) + ",0 "; }
            DData += "</coordinates></LinearRing></outerBoundaryIs></Polygon></Placemark>";
            //DData += "<Placemark><name>_mini_Full_BeamWidth</name><open>1</open><styleUrl>#msn_ylw-pushpin</styleUrl><Polygon><tessellate>1</tessellate><outerBoundaryIs><LinearRing><coordinates>";
            //for (int i = 0; i < DegreeArr.Length; i++) { DData += getLocationLongitude(Longitude, (AnternaLength * LengthArr[i]) / 3500000, (Degree + DegreeArr[i])) + "," + getLocationLatitude(Latitude, (AnternaLength * LengthArr[i]) / 3500000, (Degree + DegreeArr[i])) + ",0 "; }
            //DData += "</coordinates></LinearRing></outerBoundaryIs></Polygon></Placemark>";
            return DData;
        }

        private String createHalfBeamWidth(Double Longitude, Double Latitude, Double Degree, String ID, String Type, String Height, String Azimuth, String MecTilt, String PortName, String MinF, String MaxF)
        {
            String Data = "";
            Double AnternaLength = Convert.ToDouble(AntennaHalfBeamwidthValue);
            Double[] DegreeArr = { -35, -30, -25, -20, -15, -10, -5, 0, 0, 5, 10, 15, 20, 25, 30, 35 };
            Double[] LengthArr = { 0, 1, 2, 5, 10, 13, 15, 16, 17, 17, 16, 15, 13, 10, 5, 2, 1, 0 };
            Data += "<Placemark><name>Antenna ID : " + ID + " ( Half_BeamWidth )</name><open>1</open>" + Environment.NewLine +
                    "<description><![CDATA[" + Environment.NewLine +
                    "<table  style='width:400px; background:#208080'>" + Environment.NewLine +
                        "<tr><td style='width:160px; padding: 2px;'>Antenna ID</td><td style='background:#FFC0CB; font-size: 90%;'>" + ID + "</td></tr>" + Environment.NewLine +
                        "<tr><td style='width:160px; padding: 2px;'>Antenna Type</td><td style='background:#FFC0CB; font-size: 90%;'>" + Type + "</td></tr>" + Environment.NewLine +
                        "<tr><td style='width:160px; padding: 2px;'>Antenna Height(m)</td><td style='background:#FFC0CB; font-size: 90%;'>" + Height + "</td></tr>" + Environment.NewLine +
                        "<tr><td style='width:160px; padding: 2px;'>Antenna Azimuth</td><td style='background:#FFC0CB; font-size: 90%;'>" + Azimuth + "</td></tr>" + Environment.NewLine +
                        "<tr><td style='width:160px; padding: 2px;'>Antenna Tilt(M)</td><td style='background:#FFC0CB; font-size: 90%;'>" + MecTilt + "</td></tr>" + Environment.NewLine +
                        "<tr><td style='width:160px; padding: 2px;'>Port Name</td><td style='background:#FFC0CB; font-size: 90%;'>" + PortName + "</td></tr>" + Environment.NewLine +
                        "<tr><td style='width:160px; padding: 2px;'>MinFrequency_MHz</td><td style='background:#FFC0CB; font-size: 90%;'>" + MinF + "</td></tr>" + Environment.NewLine +
                        "<tr><td style='width:160px; padding: 2px;'>MaxFrequency_MHz</td><td style='background:#FFC0CB; font-size: 90%;'>" + MaxF + "</td></tr>" + Environment.NewLine +
                    "</table>" + Environment.NewLine +
                    "]]></description>" + Environment.NewLine +
                    "<styleUrl>#msn_ylw-pushpin_red</styleUrl><Polygon><tessellate>1</tessellate><outerBoundaryIs><LinearRing><coordinates>";
            for (int i = 0; i < DegreeArr.Length; i++) { Data += getLocationLongitude(Longitude, (AnternaLength * LengthArr[i]) / 2300000, (Degree + DegreeArr[i])) + "," + getLocationLatitude(Latitude, (AnternaLength * LengthArr[i]) / 2300000, (Degree + DegreeArr[i])) + ",0 "; }
            Data += "</coordinates></LinearRing></outerBoundaryIs></Polygon></Placemark>";
            return Data;
        }

        private Double getLocationLatitude(Double latitude, Double length, Double degree)
        {
            Double data;
            data = latitude + (length * (Math.Cos(degree * (Math.PI / 180.0))));
            return data;
        }

        private Double getLocationLongitude(Double longitude, Double length, Double degree)
        {
            Double data;
            data = longitude + (length * (Math.Sin(degree * (Math.PI / 180.0))));
            return data;
        }


        #endregion



    }
}
