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
            const String KEY = "C8D918DB-296E-4105-A652-87A0B4BA3A65";
            switch (sqlEvent)
            {
                case "SEVER_START_MESSAGE":
                    return sqlMessageServer.dialogServerSendMessagePublic("EXECUTE _sp_DIALOG_SMS_SERVER_OPEN @KEY = '" + KEY + "'");
                case "SERVER_NEW_CUSTOMER":
                    return sqlMessageServer.dialogServerSendMessagePublic("EXECUTE _sp_dialog_New_Login  @KEY = '', " + IDEventData);
                case "SERVER_COMPLAINT":
                    return sqlMessageServer.dialogServerSendMessagePublic("EXECUTE _sp_DIALOG_SMS_SERVER_NEW_COMPLAINT @KEY = '" + KEY + "', " + IDEventData);
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



        private SerialPort _dialogMessageServerSerialPort;
        private String[] _searchGSMSerialPortNameArry;
        private Boolean _IsTruePortOK = false;
        public String _truePortName;
        public String _PortMessage;


        #endregion




        #region SEARCH PORTS



        private void InitializePort()
        {       
            _dialogMessageServerSerialPort = new System.IO.Ports.SerialPort();
            _dialogMessageServerSerialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(_dialogMessageServerSerialPort_DataReceived);
            _dialogMessageServerSerialPort.BaudRate = 9600;
            _searchGSMSerialPortNameArry = SerialPort.GetPortNames();
        }

        private Boolean dialogServerSearchPort()
        {
            try
            {
                InitializePort();
                foreach (string GSMportneme in _searchGSMSerialPortNameArry)
                {
                    _dialogMessageServerSerialPort.PortName = GSMportneme;
                    _dialogMessageServerSerialPort.Open();
                    _dialogMessageServerSerialPort.Write("AT+CMGF=1\r");
                    System.Threading.Thread.Sleep(100);
                    for (int i = 0; i < 3; i++)
                    {
                        _dialogMessageServerSerialPort.Write("AT\r");
                        System.Threading.Thread.Sleep(150);
                        if (_IsTruePortOK) 
                        {
                            _truePortName = GSMportneme;
                            _dialogMessageServerSerialPort.Dispose();
                            _dialogMessageServerSerialPort.Close();
                            return true; 
                        }
                    }
                    _dialogMessageServerSerialPort.Dispose();
                    _dialogMessageServerSerialPort.Close();
                }
            }
            catch(Exception){return false;}
            return false;
        }

        private void _dialogMessageServerSerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (_dialogMessageServerSerialPort.IsOpen)
            {
                String _portD = _dialogMessageServerSerialPort.ReadExisting().ToString();
                Regex regex = new Regex(@"OK");
                Match match = regex.Match(_portD);
                if (match.Success) { _IsTruePortOK = true; }
            }
        }



        #endregion




        #region PUBLIC CALL PORT



        private Boolean dialogOpenServer()
        {
            if (dialogServerSearchPort())
            {
                InitializePort();
                _dialogMessageServerSerialPort.PortName = _truePortName;
                _dialogMessageServerSerialPort.Open();
                if (_dialogMessageServerSerialPort.IsOpen)
                {
                    try
                    {
                        const String KEY = "C8D918DB-296E-4105-A652-87A0B4BA3A65";
                        SqlConfigClass sqlMessageServerDataTable = new SqlConfigClass();
                        DataTable dt = sqlMessageServerDataTable.dialogServerSendMessagePublic("EXECUTE _sp_DIALOG_SMS_SERVER_OPEN @KEY = '" + KEY + "'");
                        DataRow dr = dt.Rows[0];
                        if (dr["SUCCES"].ToString() == "1")
                        {
                            _dialogMessageServerSerialPort.Write("AT+CMGF=1\r");
                            System.Threading.Thread.Sleep(300);
                            _dialogMessageServerSerialPort.Write("AT+CMGS=\"+" + dr["AdminNumber"].ToString() + "\"\r");
                            System.Threading.Thread.Sleep(500);
                            _dialogMessageServerSerialPort.Write("Server is started at " + DateTime.Now.ToString("h:mm:ss tt") + " on " + DateTime.Today.ToString("D") + "\r\rDialog Access Network Planning, \rWestern North Region." + (char)(26));
                            System.Threading.Thread.Sleep(1000);
                            _dialogMessageServerSerialPort.Write("AT+CMGF=1\r");
                            System.Threading.Thread.Sleep(150);
                            _dialogMessageServerSerialPort.Write("AT+CMGD=?\r");
                            System.Threading.Thread.Sleep(1000);
                            _dialogMessageServerSerialPort.Close();
                            return true;
                        }
                        else if (dr["SUCCES"].ToString() == "0" || dr["ERROR"].ToString() == "1")
                        {
                            _dialogMessageServerSerialPort.Close();
                            _IsTruePortOK = false;
                            _PortMessage = "401:\r\r" + dr["MESSAGE"].ToString();
                            return false;
                        }
                        else
                        {
                            _dialogMessageServerSerialPort.Close();
                            _IsTruePortOK = false;
                            _PortMessage = "402:\r\r" + "The Sql Server can't be opened. Check the internet connection and try again.";
                            return false;
                        }
                    }
                    catch (Exception)
                    {
                        _dialogMessageServerSerialPort.Close();
                        _IsTruePortOK = false;
                        _PortMessage = "403:\r\r" + "The message server serial port can't be opened. Check the com port and try again. The GSM SIM 800A COM Port Warning.!";
                        return false;
                    }
                }
                else
                {
                    _dialogMessageServerSerialPort.Close();
                    _IsTruePortOK = false;
                    _PortMessage = "404:\r\r" + "The message server serial port can't be opened. Check the com port and try again. The GSM SIM 800A COM Port Warning.!";
                    return false;
                }
            }
            else
            {
                _dialogMessageServerSerialPort.Close();
                _IsTruePortOK = false;
                _PortMessage = "405:\r\r" + "The message server serial port can't be opened. Check the com port and try again. The GSM SIM 800A COM Port Warning.!";
                return false;
            }
        }

        public Boolean dialogServer()
        {
            return dialogOpenServer();
        }



        #endregion


    }


}