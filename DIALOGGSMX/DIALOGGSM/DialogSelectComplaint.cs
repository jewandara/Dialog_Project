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
        private String ComplaintID ;
        private String ComplaintCustNumber;
        private String ComplaintCustLong;
        private String ComplaintCustLat;
        private String ComplaintCustData;
        private String ComplaintCustSMSDate;

        public DialogSelectComplaint(String CompID)
        {
            ComplaintID = CompID;
            InitializeComponent();
            DisplaySelectComplaint();
        }

        private void DisplaySelectComplaint()
        {
            config callServer = new config();
            DataTable _dt_SelectCustomer = callServer.dialogServerInsert("TAB_LOAD_COMPLAINT_SELECT", ComplaintID);
            foreach (DataRow dr in _dt_SelectCustomer.Rows)
            {
                CompID.Text = dr["ComplaintID"].ToString().ToUpper();
                textBoxComplaintCustNumber.Text = dr["CustNumber"].ToString();
                //textBoxComplaintCustName.Text = "SMS Customer";
                textBoxComplaintCustLong.Text = dr["Longitude"].ToString();
                textBoxComplaintCustLat.Text = dr["Latitude"].ToString();

                ComplaintCustNumber = dr["CustNumber"].ToString();
                ComplaintCustLong = dr["Longitude"].ToString();
                ComplaintCustLat = dr["Latitude"].ToString();
                ComplaintCustData = dr["ComplaintData"].ToString();
                ComplaintCustSMSDate = dr["MessageTime"].ToString();
            }
        }

        private void DialogSelectComplaint_Load(object sender, EventArgs e)
        {
            comboBoxAntennaBandtype.Items.Insert(0, "GSM");
            comboBoxAntennaBandtype.Items.Insert(1, "DCS");
            comboBoxAntennaBandtype.Items.Insert(2, "3G");
            comboBoxAntennaBandtype.Items.Insert(3, "4G");
            comboBoxAntennaBandtype.SelectedIndex = 0;
        }



        private void button3_Click(object sender, EventArgs e)
        {
            String Data = loadXmlHeader() + LoadXmlCustomer() + LoadXmalDialogSite() + loadXmlFooter();
            String pathOfSave = "Temp\\Dialog.kml";
            try
            {
                if (pathOfSave == "")
                {
                    string dummyFileName = "Dialog";
                    SaveFileDialog sf = new SaveFileDialog();
                    sf.FileName = dummyFileName;
                    sf.Filter = "Google Earth Kml (*.kml*)|*.kml*";

                    if (sf.ShowDialog() == DialogResult.OK)
                    {
                        pathOfSave = sf.FileName + ".kml";
                    }
                }
                if (!File.Exists(pathOfSave))
                {
                    File.Create(pathOfSave).Dispose();
                    using (TextWriter tw = new StreamWriter(pathOfSave))
                    {
                        tw.Close();
                    }
                    using (StreamWriter outfile = new StreamWriter(pathOfSave))
                    {
                        outfile.Write(Data);
                    }
                }
                else
                {
                    using (StreamWriter outfile = new StreamWriter(pathOfSave))
                    {
                        outfile.Write(Data);
                    }
                }
            }
            catch (Exception) { }
            System.Diagnostics.Process.Start(pathOfSave);
            this.Close();
        }





        private String loadXmlHeader()
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
                                        "<href>Dialog\\Dialog.png</href>" + Environment.NewLine +
                                        "</Icon>" + Environment.NewLine +
                                        "<hotSpot x='20' y='2' xunits='pixels' yunits='pixels'/>" + Environment.NewLine +
                                        "</IconStyle>" + Environment.NewLine +
                                    "</Style>" + Environment.NewLine + Environment.NewLine +

                                    "<Style id='s_ylw-pushpin_hl'>" + Environment.NewLine +
                                        "<IconStyle>" + Environment.NewLine +
                                        "<scale>1.3</scale>" + Environment.NewLine +
                                        "<Icon>" + Environment.NewLine +
                                        "<href>Dialog\\Dialog.png</href>" + Environment.NewLine +
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


                                    "<Style id='s_ylw-pushpin_home'>" + Environment.NewLine +
                                        "<IconStyle>" + Environment.NewLine +
                                        "<scale>1.1</scale>" + Environment.NewLine +
                                        "<Icon>" + Environment.NewLine +
                                        "<href>Dialog\\home.png</href>" + Environment.NewLine +
                                        "</Icon>" + Environment.NewLine +
                                        "<hotSpot x='20' y='2' xunits='pixels' yunits='pixels'/>" + Environment.NewLine +
                                        "</IconStyle>" + Environment.NewLine +
                                    "</Style>" + Environment.NewLine + Environment.NewLine +

                                    "<Style id='s_ylw-pushpin_hl_home'>" + Environment.NewLine +
                                        "<IconStyle>" + Environment.NewLine +
                                        "<scale>1.3</scale>" + Environment.NewLine +
                                        "<Icon>" + Environment.NewLine +
                                        "<href>Dialog\\home.png</href>" + Environment.NewLine +
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

        private String LoadXmlCustomer()
        {
            String xmlCustomerData;
            Double[] DegreeArr = { -175, -170, -165, -160, -155, -150, -145, -140, -135, -130, -125, -120, -115, -110, -105, -100, -95, -90, -85, -80, -75, -70, -65, -60, -55, -50, -45, -40, -35, -25, -20, -15, -10, -5, 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105, 110, 115, 120, 125, 130, 135, 140, 145, 150, 155, 160, 165, 170, 175, 180};

            xmlCustomerData = "<Folder>" + Environment.NewLine +
                                "<name>CUSTOMER_DATA</name>" + Environment.NewLine +
                                "<open>1</open>" + Environment.NewLine +
                                "<Placemark>" + Environment.NewLine +
                                    "<name>SMS Customer : " + textBoxComplaintCustNumber.Text + "</name>" + Environment.NewLine +
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
                                        "<coordinates>" + textBoxComplaintCustLong.Text + "," + textBoxComplaintCustLat.Text + ",0</coordinates>" + Environment.NewLine +
                                        "</Point>" + Environment.NewLine +
                                "</Placemark>" + Environment.NewLine;

            xmlCustomerData += "<Placemark><name>LayerGSM</name><open>1</open><styleUrl>#msn_ylw-pushpin_HOME_CIRCAL</styleUrl><Polygon><tessellate>1</tessellate><outerBoundaryIs><LinearRing><coordinates>";
            for (int i = 0; i < DegreeArr.Length; i++){ xmlCustomerData += getLocationLongitude(Convert.ToDouble(ComplaintCustLong), 0.02, DegreeArr[i]) + "," + getLocationLatitude(Convert.ToDouble(ComplaintCustLat), 0.02, DegreeArr[i]) + ",0 "; }
            xmlCustomerData += "</coordinates></LinearRing></outerBoundaryIs></Polygon></Placemark>";

            xmlCustomerData += "</Folder>" + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            return xmlCustomerData;
        }

        private String LoadXmalDialogSite()
        {
            if (checkBoxSelectDialogSites.Checked == true)
            {
                try
                {
                    int i = 0;
                    String xmlSiteData = "<Folder>" + Environment.NewLine + "<name>DIALOG_SITE_DATA</name>" + Environment.NewLine;
                    String ComplaintArea = "@ComLong = " + ComplaintCustLong + ", @ComLat = " + ComplaintCustLat + ", @ComCust = " + (numericUpDownCustomerAreaCircal.Value/100).ToString();
                    config callServer = new config();
                    DataTable _dt_SelectCompalaintAreSite = callServer.dialogServerInsert("TAB_LOAD_COMPLAINT_BITWEEN", ComplaintArea);
                    foreach (DataRow dr in _dt_SelectCompalaintAreSite.Rows)
                    {
                        i = i + 1;
                        progressBarSite.Value = (i / _dt_SelectCompalaintAreSite.Rows.Count)*100;
                        this.Refresh();
                        xmlSiteData += "<Folder>" + Environment.NewLine +
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
                        if (checkBoxViewAntennaBeamwidth.Checked == false && checkBoxViewAntennaDirection.Checked == false) { }
                        else { xmlSiteData += LoadXmalDialogSectors(dr["SiteID"].ToString()); }
                        xmlSiteData += "</Folder>" + Environment.NewLine + Environment.NewLine;
                    }
                    xmlSiteData += "</Folder>" + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                    return xmlSiteData;
                }
                catch (Exception) { return null; }
            }
            return null;
        }

        private String LoadXmalDialogSectors(String SiteID)
        {
            try
            {
                String xmlSectorData = "";
                config callServer = new config();
                DataTable _dt_SelectSiteSector = null;
                if (comboBoxAntennaBandtype.SelectedIndex == 0)
                {
                    _dt_SelectSiteSector = callServer.dialogServerInsert("TAB_LOAD_DIALOG_SECTOR_LIKE_GSM", SiteID);
                }
                else if (comboBoxAntennaBandtype.SelectedIndex == 1)
                {
                    _dt_SelectSiteSector = callServer.dialogServerInsert("TAB_LOAD_DIALOG_SECTOR_LIKE_DCS", SiteID);
                }
                else if (comboBoxAntennaBandtype.SelectedIndex == 2)
                {
                    _dt_SelectSiteSector = callServer.dialogServerInsert("TAB_LOAD_DIALOG_SECTOR_LIKE_3G", SiteID);
                }
                else if (comboBoxAntennaBandtype.SelectedIndex == 3)
                {
                    _dt_SelectSiteSector = callServer.dialogServerInsert("TAB_LOAD_DIALOG_SECTOR_LIKE_4G", SiteID);
                }
                else { return null; }

                foreach (DataRow dr in _dt_SelectSiteSector.Rows)
                {
                    xmlSectorData += "<Folder>" + Environment.NewLine + "<name>" + dr["SectorID"].ToString().ToUpper() + "</name>" + Environment.NewLine;
                    xmlSectorData += LoadXmalDialogAntenna(dr["SiteID"].ToString(), dr["AntennaID"].ToString());
                    xmlSectorData += "</Folder>" + Environment.NewLine;
                }
                return xmlSectorData;
            }
            catch (Exception) { return null; }
        }

        private String LoadXmalDialogAntenna(String SiteID, String AntennaID)
        {
            try
            {
                Double coordinatesLat = 0;
                Double coordinatesLog = 0;

                Double[] DegreeArr = { 0, -45, -40, -35, -30, -25, -20, -15, -12, -10, -5, 0, 5, 10, 12, 15, 20, 25, 30, 35, 40, 45 };
                Double[] LengthArr = { 0, 2, 3, 4, 5, 6, 7, 8, 8.5, 9, 9.5, 9.8, 9.5, 9, 8.5, 8, 7, 6, 5, 4, 3, 2 };

                String xmlAntennaData = "";
                config callServer = new config();
                DataTable _dt_SelectSiteAntenna = callServer.dialogServerInsert("TAB_LOAD_DIALOG_ANTENNA","@SiteID = '" + SiteID + "', @AntennaID	= '" + AntennaID + "'");

                foreach (DataRow dr in _dt_SelectSiteAntenna.Rows)
                {        
                    if (checkBoxViewAntennaDirection.Checked == true)
                    {
                        coordinatesLat = getLocationLatitude(Convert.ToDouble(dr["Latitude"]), Convert.ToDouble(numericUpDownDownAntennaDirection.Value / 110000), Convert.ToDouble(dr["Azimuth"]));
                        coordinatesLog = getLocationLongitude(Convert.ToDouble(dr["Longitude"]), Convert.ToDouble(numericUpDownDownAntennaDirection.Value / 110000), Convert.ToDouble(dr["Azimuth"]));

                        xmlAntennaData += "<Placemark>" + Environment.NewLine +
                                            "<name>Antenna ID : " + dr["AntennaID"].ToString() + "</name>" + Environment.NewLine +
                                                "<description><![CDATA[" + Environment.NewLine +
                                                "<table  style='width:400px; background:#208080'>" + Environment.NewLine +
                                                    "<tr><td style='width:160px; padding: 2px;'>Antenna ID</td><td style='background:#FFC0CB; font-size: 90%;'>" + dr["AntennaID"].ToString() + "</td></tr>" + Environment.NewLine +
                                                    "<tr><td style='width:160px; padding: 2px;'>Antenna Type</td><td style='background:#FFC0CB; font-size: 90%;'>" + dr["AntennaType"].ToString() + "</td></tr>" + Environment.NewLine +
                                                    "<tr><td style='width:160px; padding: 2px;'>Antenna Height(m)</td><td style='background:#FFC0CB; font-size: 90%;'>" + dr["Height"].ToString() + "</td></tr>" + Environment.NewLine +
                                                    "<tr><td style='width:160px; padding: 2px;'>Antenna Azimuth</td><td style='background:#FFC0CB; font-size: 90%;'>" + dr["Azimuth"].ToString() + "</td></tr>" + Environment.NewLine +
                                                    "<tr><td style='width:160px; padding: 2px;'>Antenna Tilt(M)</td><td style='background:#FFC0CB; font-size: 90%;'>" + dr["MecTilt"].ToString() + "</td></tr>" + Environment.NewLine +
                                                    //"<tr><td style='width:160px; padding: 2px;'>Antenna Tilt(E)</td><td style='background:#FFC0CB; font-size: 90%;'>" + dr["ETilt"].ToString() + "</td></tr>" + Environment.NewLine +
                                                    //"<tr><td style='width:160px; padding: 2px;'>Electrical Azimuth</td><td style='background:#FFC0CB; font-size: 90%;'>" + dr["ElectricalAzimuth"].ToString() + "</td></tr>" + Environment.NewLine +
                                                    //"<tr><td style='width:160px; padding: 2px;'>Electrical Beamwidth</td><td style='background:#FFC0CB; font-size: 90%;'>" + dr["ElectricalBeamwidth"].ToString() + "</td></tr>" + Environment.NewLine +
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




                    if (checkBoxViewAntennaBeamwidth.Checked == true)
                    {
                        xmlAntennaData += createBeemWidth(Convert.ToDouble(dr["Longitude"]), Convert.ToDouble(dr["Latitude"]), Convert.ToDouble(dr["Azimuth"]));
                    }



                }


                return xmlAntennaData;
            }
            catch (Exception) { return null; }
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

        private String createBeemWidth(Double Longitude, Double Latitude, Double Degree)
        {
            String DData = "";
            Double AnternaLength = Convert.ToDouble(numericUpDownAntennaBeamwidth.Value);

            Double[] DegreeArr = { -175, -170, -165, -160, -155, -150, -145, -140, -135, -130, -125, -120, -115, -110, -105, -100, -95, -90, -85, -80, -75, -70, -65, -60, -55, -50, -45, -40, -35, -25, -20, -15, -10, -5, 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105, 110, 115, 120, 125, 130, 135, 140, 145, 150, 155, 160, 165, 170, 175, 180};
            Double[] LengthArr = { 0.02, 0.04, 0.16, 0.32, 0.64, 1, 1.2, 1.5, 2, 2.8, 3.1, 4.6, 5.2, 6.2, 7, 7.8, 8.6, 9.4, 10.2, 11.8, 12.6, 13.3, 14, 14.7, 13.3, 14.1 ,14.8, 15.4, 15.9, 16.3, 16.6, 16.8, 16.9, 17, 17, 17, 17, 17, 16.9, 16.8, 16.6, 16.3, 15.9, 15.4, 14.8, 14.1, 13.3, 12.4, 11.4, 10.1, 12, 11.4, 10.6, 9.8, 8.8, 7.8, 7, 6.2, 5.2, 4.6, 3.1, 2.8, 2, 1.5, 1.2, 1, 0.64, 0.32, 0.16, 0.04, 0.02};
                
            DData += "<Placemark><name>LayerGSM</name><open>1</open><styleUrl>#msn_ylw-pushpin</styleUrl><Polygon><tessellate>1</tessellate><outerBoundaryIs><LinearRing><coordinates>";
            for (int i = 0; i < DegreeArr.Length; i++) { DData += getLocationLongitude(Longitude, (AnternaLength * LengthArr[i]) / 2300000, (Degree + DegreeArr[i])) + "," + getLocationLatitude(Latitude, (AnternaLength * LengthArr[i]) / 2300000, (Degree + DegreeArr[i])) + ",0 "; }
            DData += "</coordinates></LinearRing></outerBoundaryIs></Polygon></Placemark>";

            DData += "<Placemark><name>LayerGSM</name><open>1</open><styleUrl>#msn_ylw-pushpin</styleUrl><Polygon><tessellate>1</tessellate><outerBoundaryIs><LinearRing><coordinates>";
            for (int i = 0; i < DegreeArr.Length; i++) { DData += getLocationLongitude(Longitude, (AnternaLength * LengthArr[i]) / 3500000, (Degree + DegreeArr[i])) + "," + getLocationLatitude(Latitude, (AnternaLength * LengthArr[i]) / 3500000, (Degree + DegreeArr[i])) + ",0 "; }
            DData += "</coordinates></LinearRing></outerBoundaryIs></Polygon></Placemark>";

            return DData;
        }






        private void groupBoxAntennaView_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //textBox1.Text = LoadXmlCustomer();
        }









    }
}
