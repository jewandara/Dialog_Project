using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DIALOGGSM
{
    public partial class DialogSaveExcelCustomer : Form
    {

        #region DEFINE

        private int AID;//APP NUMBER
        private int LID;//USER APP NUMBER
        private String USERNUMBER; //User Phone Number
        private String LOGUSERID; //User ID
        private String REGION; //User Region

        #endregion

        public DialogSaveExcelCustomer(String RE, int SYS_APP_ID, int LOG_APP_ID, String USER_LOGIN_ID, String USER_NUMBER)
        {
            InitializeComponent();
            this.Text = "Dialog Customer - DIALOG MOBILE Customer Complaint/SaveExcel";
            REGION = RE;
            AID = SYS_APP_ID;
            LID = LOG_APP_ID;
            LOGUSERID = USER_LOGIN_ID;
            USERNUMBER = USER_NUMBER;
            labelLog.Text = USERNUMBER + " : SYS_APP_ID - " + SYS_APP_ID.ToString() + " : LOG_APP_ID - " + LOG_APP_ID.ToString() + " : LOGIN SYSTEM";
        }

        private void DialogSaveExcelCustomer_Load(object sender, EventArgs e)
        {
            DateTime fromDate = DateTime.Now.AddDays(-30);
            monthCalendarFromDate.SetDate(fromDate);
            DateTime toDate = DateTime.Today;
            monthCalendarToDate.SetDate(toDate);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {

        }


        private void radioButtonSelectDate_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSelectDate.Checked) { panelDateAndMonth.Enabled = true;}
            else { panelDateAndMonth.Enabled = false; }
        }

        private void radioButtonAllCustomers_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSelectDate.Checked) { panelDateAndMonth.Enabled = true; }
            else { panelDateAndMonth.Enabled = false;}
        }

        private void radioButton10Customers_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSelectDate.Checked) { panelDateAndMonth.Enabled = true; }
            else { panelDateAndMonth.Enabled = false; }
        }

        private void radioButton100Customers_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSelectDate.Checked) { panelDateAndMonth.Enabled = true; }
            else { panelDateAndMonth.Enabled = false; }
        }

        private void radioButton1000Customers_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSelectDate.Checked) { panelDateAndMonth.Enabled = true; }
            else { panelDateAndMonth.Enabled = false; }
        }

        private void monthCalendarFromDate_DateChanged(object sender, DateRangeEventArgs e)
        {
            labelFrom.Text = monthCalendarFromDate.SelectionRange.Start.ToString();
        }

        private void monthCalendarToDate_DateChanged(object sender, DateRangeEventArgs e)
        {
            labelTo.Text = monthCalendarToDate.SelectionRange.Start.ToString();
        }






        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBoxCustType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }




    }
}
 