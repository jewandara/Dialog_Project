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
    public partial class Form1 : Form
    {

        #region DEFINE

        //private String ComplaintID;
        //private String LoginID;
        //private String AppKeyID;
        private String ComplaintCustNumber;
        private String ComplaintCustLong;
        private String ComplaintCustLat;
        private String ComplaintCustData;
        private String ComplaintCustSMSDate;
        private String pathSaveKmlFile;
        private String folderSaveImageFile;

        private Boolean checkDialogSitesOK = true;
        private Double DialogSitesAreaValue = 200;
        private String OtherSiteOwnerName = "MOBITEL";

        //private Boolean checkAntennaBandtypeOK;
        private String checkAntennaBandtype = "GSM";



        private Boolean checkDirectionLineOK = true;     
        private Decimal AntennaDirectionValue = 1600;

        private Boolean checkAntennaFullBeamwidthOK = true;
        private Decimal AntennaFullBeamwidthValue = 200;

        private Boolean checkAntennaHalfBeamwidthOK = true;
        private Decimal AntennaHalfBeamwidthValue= 200;



   
        #endregion

        private void DisplaySelectComplaint()
        {

                config callServer = new config();
                DataTable _dt_SelectCustomer = callServer.dialogServerInsert("TAB_LOAD_COMPLAINT_SELECT", " @KEY = 'B7970DE6-36D5-4E23-9F3C-9BDDCB09DC59', @LOGID = 'DC07BD80-BA74-441A-ABF9-D1D9554B48FA', @COMPID = 'FD87578C-DFD8-4B97-9675-294E87149AC5'");
                foreach (DataRow dr in _dt_SelectCustomer.Rows)
                {
                    ComplaintCustNumber = dr["CustNumber"].ToString();
                    ComplaintCustLong = dr["Longitude"].ToString();
                    ComplaintCustLat = dr["Latitude"].ToString();
                    ComplaintCustData = dr["ComplaintData"].ToString();
                    ComplaintCustSMSDate = dr["MessageTime"].ToString();
                }
 
        }

        public Form1()
        {
            InitializeComponent();
            DisplaySelectComplaint();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }






        private int prestentageProgressDSiteValue = 0;
        private int prestentageProgressOSiteValue = 0;
        private int pro = 0;

        private void backgroundWorkerSiteCreate_DoWork(object sender, DoWorkEventArgs e)
        {
            String kmlSiteData = "";
            config callServer = new config();
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

            String Data = loadXmlHeader(folderSaveImageFile) + LoadXmlCustomer(2) + kmlSiteData + loadXmlFooter();
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
               
            
            //= (prestentageProgressDSiteValue * 3) + prestentageProgressDSiteValue;
            //labelProgressSiteBar.Visible = true;
            progressBarSector.Value = e.ProgressPercentage;
            
            pro = pro + 2;
            if (pro < 80) { progressBarSite.Value = 20 + pro; }
            labelProgressSiteBar.Text = "Site/Processing... " + e.ProgressPercentage.ToString() + " %";
        }

        private void backgroundWorkerSiteCreate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            labelProgressSiteBar.Visible = true;
            if (e.Cancelled) { labelProgressSiteBar.Text = "Processing Canceled !"; }
            else if (e.Error != null) { MessageBox.Show("Sorry, Processing kml file error. Try again." + Environment.NewLine + e.ToString(), "Kml File Error", MessageBoxButtons.OK, MessageBoxIcon.Error); this.Close(); }
            else 
            {
                labelProgressSiteBar.Text = "Processing Completed... 100 %"; 
                MessageBox.Show("The kml file created successfully", "Google Kml File", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                System.Diagnostics.Process.Start(pathSaveKmlFile);
            }
        }






        void backgroundWorkerSectorCreate_DoWork(object sender, DoWorkEventArgs e)
        {

        }


        #region KML FUNCTION

        private String loadXmlHeader(String imagePath)
        {
            return "<?xml version='1.0' encoding='UTF-8'?>" + Environment.NewLine + "<kml xmlns='http://www.opengis.net/kml/2.2' xmlns:gx='http://www.google.com/kml/ext/2.2' xmlns:kml='http://www.opengis.net/kml/2.2' xmlns:atom='http://www.w3.org/2005/Atom'>" + Environment.NewLine +
                                    "<Document>" + Environment.NewLine + "<name>DIALOG</name>" + Environment.NewLine + Environment.NewLine +


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

            xmlCustomerData += "</Folder>" + Environment.NewLine + Environment.NewLine + Environment.NewLine ;
            return xmlCustomerData;
        }

        private String LoadXmalDialogSectors(object sender, String SiteID, String BandType)
        {
            BackgroundWorker SectorWorker = sender as BackgroundWorker;
            String xmlSectorData = "";
            config callServer = new config();
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
            config callServer = new config();
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
                if (checkAntennaFullBeamwidthOK) { xmlAntennaData += createFulBeamWidth(Convert.ToDouble(dr["Longitude"]), Convert.ToDouble(dr["Latitude"]), Convert.ToDouble(dr["Azimuth"])); }
                if (checkAntennaHalfBeamwidthOK) { xmlAntennaData += createHalfBeamWidth(Convert.ToDouble(dr["Longitude"]), Convert.ToDouble(dr["Latitude"]), Convert.ToDouble(dr["Azimuth"]), dr["AntennaID"].ToString(), dr["AntennaType"].ToString(), dr["Height"].ToString(), dr["Azimuth"].ToString(), dr["MecTilt"].ToString(), dr["PortName"].ToString(), dr["MinFrequency_MHz"].ToString(), dr["MaxFrequency_MHz"].ToString()); }
            }
            return xmlAntennaData;
        }

        private String createFulBeamWidth(Double Longitude, Double Latitude, Double Degree)
        {
            String DData = "";
            Double AnternaLength = Convert.ToDouble(AntennaFullBeamwidthValue);
            Double[] DegreeArr = { -175, -170, -165, -160, -155, -150, -145, -140, -135, -130, -125, -120, -115, -110, -105, -100, -95, -90, -85, -80, -75, -70, -65, -60, -55, -50, -45, -40, -35, -25, -20, -15, -10, -5, 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105, 110, 115, 120, 125, 130, 135, 140, 145, 150, 155, 160, 165, 170, 175, 180 };
            Double[] LengthArr = { 0.02, 0.04, 0.16, 0.32, 0.64, 1, 1.2, 1.5, 2, 2.8, 3.1, 4.6, 5.2, 6.2, 7, 7.8, 8.6, 9.4, 10.2, 11.8, 12.6, 13.3, 14, 14.7, 13.3, 14.1, 14.8, 15.4, 15.9, 16.3, 16.6, 16.8, 16.9, 17, 17, 17, 17, 17, 16.9, 16.8, 16.6, 16.3, 15.9, 15.4, 14.8, 14.1, 13.3, 12.4, 11.4, 10.1, 12, 11.4, 10.6, 9.8, 8.8, 7.8, 7, 6.2, 5.2, 4.6, 3.1, 2.8, 2, 1.5, 1.2, 1, 0.64, 0.32, 0.16, 0.04, 0.02 };
            DData += "<Placemark><name>_max_Full_BeamWidth</name><open>1</open><styleUrl>#msn_ylw-pushpin</styleUrl><Polygon><tessellate>1</tessellate><outerBoundaryIs><LinearRing><coordinates>";
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
            Data += "<Placemark><name>_Half_BeamWidth</name><open>1</open>" + Environment.NewLine +
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

      
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (backgroundWorkerSectorCreate.WorkerSupportsCancellation == true)
            {
                backgroundWorkerSectorCreate.CancelAsync();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        //    try
        //    {
                pathSaveKmlFile = "";
                folderSaveImageFile = "";
                labelProgressSiteBar.Visible = true;
                labelProgressSectorBar.Visible = true;
                if (pathSaveKmlFile == "")
                {
                    SaveFileDialog sf = new SaveFileDialog();
                    sf.FileName = "Dialog_Complaint";
                    sf.Filter = "Google Earth Kml (*.kml*)|*.kml*";
                    if (sf.ShowDialog() == DialogResult.OK) { pathSaveKmlFile = sf.FileName + ".kml"; folderSaveImageFile = sf.FileName + " _googlefiles"; }
                    progressBarSite.Value = 0;
                    labelProgressSiteBar.Text = "Creating Folder/Processing... 0 %";
                    Image imageD = Image.FromFile(@"Images\D.png");
                    Image imageM = Image.FromFile(@"Images\M.png");
                    progressBarSite.Value = 1;
                    labelProgressSiteBar.Text = "Creating Images/Processing... 1 %";
                    Image imageH = Image.FromFile(@"Images\H.png");
                    Image imageL = Image.FromFile(@"Images\L.png");
                    progressBarSite.Value = 2;
                    labelProgressSiteBar.Text = "Creating Images/Processing... 2 %";
                    Image imageE = Image.FromFile(@"Images\E.png");
                    Image imageD5 = Image.FromFile(@"Images\Dx50.png");
                    progressBarSite.Value = 3;
                    labelProgressSiteBar.Text = "Creating Images/Processing... 3 %";
                    Image imageM5 = Image.FromFile(@"Images\Mx50.png");
                    Image imageH5 = Image.FromFile(@"Images\Hx50.png");
                    progressBarSite.Value = 4;
                    labelProgressSiteBar.Text = "Creating Images/Processing... 4 %";
                    Image imageL5 = Image.FromFile(@"Images\Lx50.png");
                    Image imageE5 = Image.FromFile(@"Images\Ex50.png");
                    progressBarSite.Value = 5;
                    labelProgressSiteBar.Text = "Creating Images/Processing... 5 %";
                    if (!File.Exists(folderSaveImageFile))
                    {
                        Directory.CreateDirectory(folderSaveImageFile);
                        imageD.Save(folderSaveImageFile + "\\_site_icon-dialog.png");
                        imageM.Save(folderSaveImageFile + "\\_site_icon-mobitel.png");
                        progressBarSite.Value = 6;
                        labelProgressSiteBar.Text = "Creating Images/Processing... 6 %";
                        imageH.Save(folderSaveImageFile + "\\_site_icon-hutch.png");
                        imageL.Save(folderSaveImageFile + "\\_site_icon-lankabell.png");
                        progressBarSite.Value = 7;
                        labelProgressSiteBar.Text = "Creating Images/Processing... 7 %";
                        imageE.Save(folderSaveImageFile + "\\_site_icon-etisalat.png");
                        imageD5.Save(folderSaveImageFile + "\\_site_icon-dialog5.png");
                        progressBarSite.Value = 8;
                        labelProgressSiteBar.Text = "Creating Images/Processing... 8 %";
                        imageM5.Save(folderSaveImageFile + "\\_site_icon-mobitel5.png");
                        imageH5.Save(folderSaveImageFile + "\\_site_icon-hutch5.png");
                        progressBarSite.Value = 9;
                        labelProgressSiteBar.Text = "Creating Images/Processing... 9 %";
                        imageL5.Save(folderSaveImageFile + "\\_site_icon-lankabell5.png");
                        imageE5.Save(folderSaveImageFile + "\\_site_icon-etisalat5.png");
                        Image imageHOME = Image.FromFile(@"Images\HOME.png");
                        imageHOME.Save(folderSaveImageFile + "\\_icon-HOME.png");
                        progressBarSite.Value = 10;
                        labelProgressSiteBar.Text = "Complete Images/Processing... 10 %";
                        if (backgroundWorkerSiteCreate.IsBusy != true) { backgroundWorkerSiteCreate.RunWorkerAsync(); }
                        //CreateSite();
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
                            labelProgressSiteBar.Text = "Creating Images/Processing... 6 %";
                            imageH.Save(folderSaveImageFile + "\\_site_icon-hutch.png");
                            imageL.Save(folderSaveImageFile + "\\_site_icon-lankabell.png");
                            progressBarSite.Value = 7;
                            labelProgressSiteBar.Text = "Creating Images/Processing... 7 %";
                            imageE.Save(folderSaveImageFile + "\\_site_icon-etisalat.png");
                            imageD5.Save(folderSaveImageFile + "\\_site_icon-dialog5.png");
                            progressBarSite.Value = 8;
                            labelProgressSiteBar.Text = "Creating Images/Processing... 8 %";
                            imageM5.Save(folderSaveImageFile + "\\_site_icon-mobitel5.png");
                            imageH5.Save(folderSaveImageFile + "\\_site_icon-hutch5.png");
                            progressBarSite.Value = 9;
                            labelProgressSiteBar.Text = "Creating Images/Processing... 9 %";
                            imageL5.Save(folderSaveImageFile + "\\_site_icon-lankabell5.png");
                            imageE5.Save(folderSaveImageFile + "\\_site_icon-etisalat5.png");
                            Image imageHOME = Image.FromFile(@"Images\HOME.png");
                            imageHOME.Save(folderSaveImageFile + "\\_icon-HOME.png");
                            progressBarSite.Value = 10;
                            labelProgressSiteBar.Text = "Complete Images/Processing... 10 %";
                            if (backgroundWorkerSiteCreate.IsBusy != true) { backgroundWorkerSiteCreate.RunWorkerAsync(); }
                        }
                    }
                }
            //}
            //catch (Exception) { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label2.Text = prestentageProgressDSiteValue.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label3.Text = prestentageProgressOSiteValue.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label4.Text = pro.ToString();
        }






    }
}
