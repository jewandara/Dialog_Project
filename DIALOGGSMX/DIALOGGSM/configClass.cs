using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Dialog.MessageServer
{
    class SqlConfigClass
    {
        private String connectionString = null;
        private SqlConnection con;

        private void InitializeSql()
        {
            connectionString = "Data source=SQL5013.Smarterasp.net;Initial catalog=DB_9DA835_planetwn;User Id=DB_9DA835_planetwn_admin;Password=planetwn#12345;";
            con = new SqlConnection(connectionString);
        }

        private DataTable dialogServerSendMessage(String SqlComd)
        {
            try
            {
                InitializeSql();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(SqlComd, con);
                DataTable sqlDataTable = new DataTable();
                sqlDataAdapter.Fill(sqlDataTable);
                return sqlDataTable;
            }
            catch (Exception) 
            {
                var result = MessageBox.Show("Server Error", "Exit From Programe",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    Application.Exit();
                }
                Application.Exit();
                return null;
            }
        }

        public DataTable dialogServerSendMessagePublic(String SqlComd)
        {
            return dialogServerSendMessage(SqlComd);
        }
    }


    public class config
    {
        private SqlConfigClass sqlMessageServerDataTable = new SqlConfigClass();
        public DataTable dialogServerInsert(String sqlEvent, String IDEventData = "")
        {
            switch (sqlEvent)
            {
                case "LOAD_APPLICATION":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("EXECUTE _sp_dialog_LOAD_APP_KEY @KEY ='" + IDEventData + "'");
                case "LOGIN_USER_EXISTS":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("EXECUTE _sp_dialog_LOAD_LOGIN_APP_USER @APPUSER ='" + IDEventData + "'");
                case "CREATE_APPLICATION_KEY":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("EXECUTE _sp_dialog_SAVE_APP_KEY " + IDEventData);


                case "TAB_LOAD_CUSTOMER_SMS":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("EXECUTE _sp_view_dialog_TAB_LOAD_CUSTOMER_SMS @KEY ='" + IDEventData + "'");
                case "TAB_LOAD_CUSTOMER_NET":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("EXECUTE _sp_view_dialog_TAB_LOAD_CUSTOMER_NET @KEY ='" + IDEventData + "'");
                case "TAB_LOAD_CUSTOMER_SEARCH":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("EXECUTE _sp_view_dialog_TAB_LOAD_CUSTOMER_SEARCH " + IDEventData);
                case "TAB_LOAD_CUSTOMER_SELECT":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("_sp_view_dialog_TAB_LOAD_CUSTOMER_SELECT " + IDEventData);



                case "TAB_LOAD_SMSCOMPLAINT":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("EXECUTE _sp_view_dialog_TAB_LOAD_SMSCOMPLAINT");
                case "TAB_LOAD_COMPLAINT_SELECT":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("EXECUTE _sp_view_dialog_TAB_LOAD_COMPLAINT_SELECT @CompID ='" + IDEventData + "'");
                case "TAB_LOAD_COMPLAINT_BITWEEN":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("EXECUTE _sp_view_dialog_TAB_LOAD_DIALOG_SITES_BITWEEN " + IDEventData + "");


                case "TAB_LOAD_NETCOMPLAINT":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("EXECUTE _sp_view_dialog_TAB_LOAD_NETCOMPLAINT");



                case "TAB_LOAD_DIALOG_SITES":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("EXECUTE _sp_view_dialog_TAB_LOAD_DIALOG_SITES");
                case "TAB_LOAD_DIALOG_SITES_LIKE_AM":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("SELECT * FROM dbo._view_dialog_TAB_LOAD_DIALOG_SITES_LIKE WHERE SiteID LIKE 'AM%' ORDER BY Modified  DESC");
                case "TAB_LOAD_DIALOG_SITES_LIKE_AN":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("SELECT * FROM dbo._view_dialog_TAB_LOAD_DIALOG_SITES_LIKE WHERE SiteID LIKE 'AN%' ORDER BY Modified  DESC");
                case "TAB_LOAD_DIALOG_SITES_LIKE_BA":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("SELECT * FROM dbo._view_dialog_TAB_LOAD_DIALOG_SITES_LIKE WHERE SiteID LIKE 'BA%' ORDER BY Modified  DESC");
                case "TAB_LOAD_DIALOG_SITES_LIKE_BD":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("SELECT * FROM dbo._view_dialog_TAB_LOAD_DIALOG_SITES_LIKE WHERE SiteID LIKE 'BD%' ORDER BY Modified  DESC");
                case "TAB_LOAD_DIALOG_SITES_LIKE_CM":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("SELECT * FROM dbo._view_dialog_TAB_LOAD_DIALOG_SITES_LIKE WHERE SiteID LIKE 'CM%' ORDER BY Modified  DESC");
                case "TAB_LOAD_DIALOG_SITES_LIKE_GA":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("SELECT * FROM dbo._view_dialog_TAB_LOAD_DIALOG_SITES_LIKE WHERE SiteID LIKE 'GA%' ORDER BY Modified  DESC");
                case "TAB_LOAD_DIALOG_SITES_LIKE_GM":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("SELECT * FROM dbo._view_dialog_TAB_LOAD_DIALOG_SITES_LIKE WHERE SiteID LIKE 'GM%' ORDER BY Modified  DESC");
                case "TAB_LOAD_DIALOG_SITES_LIKE_HA":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("SELECT * FROM dbo._view_dialog_TAB_LOAD_DIALOG_SITES_LIKE WHERE SiteID LIKE 'HA%' ORDER BY Modified  DESC");
                case "TAB_LOAD_DIALOG_SITES_LIKE_KA":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("SELECT * FROM dbo._view_dialog_TAB_LOAD_DIALOG_SITES_LIKE WHERE SiteID LIKE 'KA%' ORDER BY Modified  DESC");
                case "TAB_LOAD_DIALOG_SITES_LIKE_KE":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("SELECT * FROM dbo._view_dialog_TAB_LOAD_DIALOG_SITES_LIKE WHERE SiteID LIKE 'KE%' ORDER BY Modified  DESC");
                case "TAB_LOAD_DIALOG_SITES_LIKE_KI":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("SELECT * FROM dbo._view_dialog_TAB_LOAD_DIALOG_SITES_LIKE WHERE SiteID LIKE 'KI%' ORDER BY Modified  DESC");
                case "TAB_LOAD_DIALOG_SITES_LIKE_KL":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("SELECT * FROM dbo._view_dialog_TAB_LOAD_DIALOG_SITES_LIKE WHERE SiteID LIKE 'KL%' ORDER BY Modified  DESC");
                case "TAB_LOAD_DIALOG_SITES_LIKE_KU":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("SELECT * FROM dbo._view_dialog_TAB_LOAD_DIALOG_SITES_LIKE WHERE SiteID LIKE 'KU%' ORDER BY Modified  DESC");
                case "TAB_LOAD_DIALOG_SITES_LIKE_MA":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("SELECT * FROM dbo._view_dialog_TAB_LOAD_DIALOG_SITES_LIKE WHERE SiteID LIKE 'MA%' ORDER BY Modified  DESC");
                case "TAB_LOAD_DIALOG_SITES_LIKE_MO":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("SELECT * FROM dbo._view_dialog_TAB_LOAD_DIALOG_SITES_LIKE WHERE SiteID LIKE 'MO%' ORDER BY Modified  DESC");
                case "TAB_LOAD_DIALOG_SITES_LIKE_MR":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("SELECT * FROM dbo._view_dialog_TAB_LOAD_DIALOG_SITES_LIKE WHERE SiteID LIKE 'MR%' ORDER BY Modified  DESC");
                case "TAB_LOAD_DIALOG_SITES_LIKE_MT":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("SELECT * FROM dbo._view_dialog_TAB_LOAD_DIALOG_SITES_LIKE WHERE SiteID LIKE 'MT%' ORDER BY Modified  DESC");
                case "TAB_LOAD_DIALOG_SITES_LIKE_MU":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("SELECT * FROM dbo._view_dialog_TAB_LOAD_DIALOG_SITES_LIKE WHERE SiteID LIKE 'MU%' ORDER BY Modified  DESC");
                case "TAB_LOAD_DIALOG_SITES_LIKE_NU":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("SELECT * FROM dbo._view_dialog_TAB_LOAD_DIALOG_SITES_LIKE WHERE SiteID LIKE 'NU%' ORDER BY Modified  DESC");
                case "TAB_LOAD_DIALOG_SITES_LIKE_PO":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("SELECT * FROM dbo._view_dialog_TAB_LOAD_DIALOG_SITES_LIKE WHERE SiteID LIKE 'PO%' ORDER BY Modified  DESC");
                case "TAB_LOAD_DIALOG_SITES_LIKE_PU":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("SELECT * FROM dbo._view_dialog_TAB_LOAD_DIALOG_SITES_LIKE WHERE SiteID LIKE 'PU%' ORDER BY Modified  DESC");
                case "TAB_LOAD_DIALOG_SITES_LIKE_RA":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("SELECT * FROM dbo._view_dialog_TAB_LOAD_DIALOG_SITES_LIKE WHERE SiteID LIKE 'RA%' ORDER BY Modified  DESC");
                case "TAB_LOAD_DIALOG_SITES_LIKE_TR":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("SELECT * FROM dbo._view_dialog_TAB_LOAD_DIALOG_SITES_LIKE WHERE SiteID LIKE 'TR%' ORDER BY Modified  DESC");
                case "TAB_LOAD_DIALOG_SITES_LIKE_VA":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("SELECT * FROM dbo._view_dialog_TAB_LOAD_DIALOG_SITES_LIKE WHERE SiteID LIKE 'VA%' ORDER BY Modified  DESC");


                case "TAB_LOAD_DIALOG_ANTENNA":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("EXECUTE _sp_view_dialog_TAB_LOAD_DIALOG_ANTENNA " + IDEventData);





                case "TAB_LOAD_DIALOG_SECTOR_LIKE_GSM":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("EXECUTE _sp_view_dialog_TAB_LOAD_DIALOG_SECTOR_LIKE_GSM @SiteID	= '" + IDEventData + "'");
                case "TAB_LOAD_DIALOG_SECTOR_LIKE_DCS":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("EXECUTE _sp_view_dialog_TAB_LOAD_DIALOG_SECTOR_LIKE_DCS @SiteID	= '" + IDEventData + "'");
                case "TAB_LOAD_DIALOG_SECTOR_LIKE_3G":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("EXECUTE _sp_view_dialog_TAB_LOAD_DIALOG_SECTOR_LIKE_3G @SiteID	= '" + IDEventData + "'");
                case "TAB_LOAD_DIALOG_SECTOR_LIKE_4G":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("EXECUTE _sp_view_dialog_TAB_LOAD_DIALOG_SECTOR_LIKE_4G @SiteID	= '" + IDEventData + "'");




                case "TAB_LOAD_OTHER_SITES":
                    return sqlMessageServerDataTable.dialogServerSendMessagePublic("SELECT * FROM dbo.dialog_Customer WHERE CustName = 'Customer'");


            }
            return null;
        }
    }


}
