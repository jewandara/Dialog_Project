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
    public partial class DialogSelectAllComplaint : Form
    {

        #region DEFINE

        private int AID;//APP NUMBER
        private int LID;//USER APP NUMBER
        private String USERNUMBER; //User Phone Number
        private String CUSTNUMBER; //Cust Phone Number
        private String LOGUSERID; //User ID
        private String REGION; //User Region

        #endregion


        public DialogSelectAllComplaint(String RE, int SYS_APP_ID, int LOG_APP_ID, String USER_LOGIN_ID, String USER_NUMBER, String CUST_NUMBER)
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
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DialogSelectAllComplaint_Load(object sender, EventArgs e)
        {

        }
    }
}
