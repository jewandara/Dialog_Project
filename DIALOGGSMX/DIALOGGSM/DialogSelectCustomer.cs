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
        public DialogSelectCustomer(String KeyID, String CustID)
        {
            InitializeComponent();
            DisplaySelectCustomer(KeyID,CustID);
        }

        private void DisplaySelectCustomer(String KeyID, String CustID)
        {
            config callServer = new config();
            DataTable _dt_SelectCustomer = callServer.dialogServerInsert("TAB_LOAD_CUSTOMER_SELECT", " @KEY = '" + KeyID + "', @CUSTID = '" + CustID + "'");
            foreach (DataRow dr in _dt_SelectCustomer.Rows)
            {
                labelCustNumber.Text = dr["CustNumber"].ToString();
                textBoxCustNumber.Text = dr["CustNumber"].ToString();
                textBoxCustName.Text = dr["CustName"].ToString();
                textBoxCustGender.Text = dr["CustGender"].ToString();
                textBoxCustAddressOne.Text = dr["CustAddresOne"].ToString();
                textBoxCustAddressTwo.Text = dr["CustAddresTwo"].ToString();
                textBoxCustEmail.Text = dr["CustEmail"].ToString();
                textBoxCallingTime.Text = dr["CustCallTime"].ToString();
                textBoxPassWordChangeDate.Text = dr["PassWordChangeDate"].ToString();
                textBoxInsertDate.Text = dr["InsertedDate"].ToString();
                textBoxModifiedDate.Text = dr["ModifiedDate"].ToString();
                textBoxCustID.Text = dr["CustID"].ToString().ToUpper();
                textBoxPassInCorrectCount.Text = dr["FaultPWCount"].ToString();
            }
        }

        private void DialogSelectCustomer_Load(object sender, EventArgs e)
        {

        }


    }
}
