using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.ComponentModel;

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
            { return null; }
        }

        public DataTable dialogServerSendMessagePublic(String SqlComd)
        {
            return dialogServerSendMessage(SqlComd);
        }
    }


    public class config
    {
        private SqlConfigClass sqlMessageServer = new SqlConfigClass();

        public DataTable dialogServerInsert(String sqlEvent, String IDEventData = "")
        {
            const String KEY = "79B8242B-22A1-4AE8-B52B-D6A298B17AE2";
            switch (sqlEvent)
            {
                case "SEVER_START_MESSAGE":
                    return sqlMessageServer.dialogServerSendMessagePublic("EXECUTE _sp_DIALOG_SMS_SERVER_OPEN @KEY = '" + KEY + "'");
                case "SERVER_NEW_CUSTOMER":
                    return sqlMessageServer.dialogServerSendMessagePublic("EXECUTE _sp_dialog_NEW_USER_LOGIN  @KEY = '" + KEY + "', @CustAppID	= 8," + IDEventData);
                case "SERVER_COMPLAINT":
                    return sqlMessageServer.dialogServerSendMessagePublic("EXECUTE _sp_DIALOG_SMS_SERVER_NEW_COMPLAINT @KEY = '" + KEY + "', " + IDEventData);
                case "SERVER_COMPLAINT_DEG":
                    return sqlMessageServer.dialogServerSendMessagePublic("EXECUTE _sp_DIALOG_SMS_SERVER_NEW_COMPLAINT_DEG @KEY = '" + KEY + "', " + IDEventData);
                case "SERVER_PASS_CHANGE":
                    return sqlMessageServer.dialogServerSendMessagePublic("EXECUTE _sp_DIALOG_SMS_SERVER_CHANGE_PASS @KEY = '" + KEY + "', " + IDEventData);
                case "SERVER_RESTART_PC":
                    return sqlMessageServer.dialogServerSendMessagePublic("EXECUTE _sp_DIALOG_SMS_SERVER_RESTART_PC @KEY = '" + KEY + "', " + IDEventData);
            }
            return null;
        }

    }


    public class PortConfigClass
    {


        #region PORTS DEFINE

        private SerialPort _dialogServerSerialPort;
        private String[] _searchGSMSerialPortNameArry;
        private Boolean _IsTruePortOK = false;
        private String _truePortName;

        #endregion


        #region CALL PORTS


        private void InitializePort()
        {
            _dialogServerSerialPort = new System.IO.Ports.SerialPort();
            _dialogServerSerialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(_dialogServerSerialPort_DataReceived);
            _dialogServerSerialPort.BaudRate = 9600;
            _searchGSMSerialPortNameArry = SerialPort.GetPortNames();
        }

        private void _dialogServerSerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            String _portD = _dialogServerSerialPort.ReadExisting().ToString();
            System.Threading.Thread.Sleep(100);
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"Revision:1137B03SIM900A64_ST_ENHANCE\r\n\r\nOK\r\n*$");
            System.Text.RegularExpressions.Match match = regex.Match(_portD);
            if (match.Success) { _IsTruePortOK = true; } else { _IsTruePortOK = false; }
        }

        private String dialogServerSearchPort()
        {
            InitializePort();
            foreach (string GSMportneme in _searchGSMSerialPortNameArry)
            {
                _dialogServerSerialPort.PortName = GSMportneme;
                _dialogServerSerialPort.Open();
                for (int i = 0; i < 3; i++)
                {
                    _dialogServerSerialPort.Write("AT+CGMR\r");
                    System.Threading.Thread.Sleep(1200);
                    if (_IsTruePortOK)
                    {
                        _truePortName = GSMportneme;
                        return _truePortName;
                    }
                }
                _dialogServerSerialPort.Dispose();
                _dialogServerSerialPort.Close();
            }
            return "";
        }

        private String dialogOpenServer()
        {
            try
            {
                if (dialogServerSearchPort() != "")
                {
                    const String KEY = "79B8242B-22A1-4AE8-B52B-D6A298B17AE2";
                    SqlConfigClass sqlMessageServerDataTable = new SqlConfigClass();
                    DataTable dt = sqlMessageServerDataTable.dialogServerSendMessagePublic("EXECUTE _sp_DIALOG_SMS_SERVER_OPEN @KEY = '" + KEY + "'");
                    DataRow dr = dt.Rows[0];
                    if (dr["SUCCES"].ToString() == "1")
                    {
                        _dialogServerSerialPort.Write("AT+CMGF=1\r");
                        System.Threading.Thread.Sleep(500);
                        _dialogServerSerialPort.Write("AT+CMGS=\"+" + dr["AdminNumber"].ToString() + "\"\r");
                        System.Threading.Thread.Sleep(1000);
                        _dialogServerSerialPort.Write("Server is started at " + DateTime.Now.ToString("h:mm:ss tt") + " on " + DateTime.Today.ToString("D") + "\r\rDialog Access Network Planning, \rWestern North Region." + (char)(26));
                        System.Threading.Thread.Sleep(1000);
                        _dialogServerSerialPort.Close();
                    }
                    return _truePortName;
                }
            }
            catch (SqlException)
            {
                _dialogServerSerialPort.Close();
                MessageBox.Show("The Sql Server can't be opened. Check the internet connection and try again.", "Sql Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
            catch (Exception)
            {
                _dialogServerSerialPort.Close();
                MessageBox.Show("The message server serial port can't be opened. Check the com port and try again.", "Port Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
            _dialogServerSerialPort.Close();
            MessageBox.Show("The message server serial port can't be opened. Check the com port and try again.", "Port Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return "";
        }

        public String dialogServer()
        {
            return dialogOpenServer();
        }



        #endregion


    }


}