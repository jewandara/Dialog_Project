using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.IO.Ports;
using Dialog.MessageServer;

namespace DialogMessageServer
{
    public partial class DialogNotificationWindow : Form
    {



        #region DIFINE DATA

        private int x, y;
        Point dialogNewPoint = new Point();
        private String _portOpenData;
        delegate void dialogMessageSerialPortTextCallback(String[] _smsArryPort);

        #endregion




        #region OPEN PORT


        public DialogNotificationWindow(String _portName)
        {
            InitializeComponent();
            _dialogMessageServerSerialPort.PortName = _portName;
            _dialogMessageServerSerialPort.Open();
        }

        private void _dialogMessageServerSerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (_dialogMessageServerSerialPort.IsOpen)
            {
                String _portD = _dialogMessageServerSerialPort.ReadExisting().ToString();
                _portOpenData += _portD;

                Regex serverUpdateSMS = new Regex(@"\+(?<CMT>[\w ]+): ""\+(?<ID>\d+)"","""",""(?<Y>\d+)/(?<M>\d+)/(?<D>\d+),(?<Hour>\d+):(?<Minit>\d+):(?<Secon>\d+)\+(?<RestS>\d+)""\r\n(?<LOG>\d+(?:\.\d+)?) (?<LAT>\d+(?:\.\d+)?) (?<SMS>[\w ]+)\r\n*$");
                Match _match_serverUpdateSMS = serverUpdateSMS.Match(_portOpenData);

                Regex changePassSMS = new Regex(@"\+(?<CMT>[\w ]+): ""\+(?<ID>\d+)"","""",""(?<Y>\d+)/(?<M>\d+)/(?<D>\d+),(?<Hour>\d+):(?<Minit>\d+):(?<Secon>\d+)\+(?<RestS>\d+)""\r\nPASS (?<PASS>[\w ]+) (?<SMS>[\w ]+)\r\n*$");
                Match _match_changePassSMS = changePassSMS.Match(_portOpenData);

                Regex shoutDownSMS = new Regex(@"\+(?<CMT>[\w ]+): ""\+(?<ID>\d+)"","""",""(?<Y>\d+)/(?<M>\d+)/(?<D>\d+),(?<Hour>\d+):(?<Minit>\d+):(?<Secon>\d+)\+(?<RestS>\d+)""\r\nOFFPC (?<PASS>[\w ]+) (?<SMS>[\w ]+)\r\n*$");
                Match _match_shoutDownSMS = shoutDownSMS.Match(_portOpenData);

                Regex SMS = new Regex(@"\+(?<CMT>[\w ]+): ""\+(?<ID>\d+)"","""",""(?<Y>\d+)/(?<M>\d+)/(?<D>\d+),(?<Hour>\d+):(?<Minit>\d+):(?<Secon>\d+)\+(?<RestS>\d+)""\r\n(?<SMS>[\w ]+)\r\n*$");
                Match _match_SMS = SMS.Match(_portOpenData);


                if (_match_serverUpdateSMS.Success)
                {
                    _portOpenData = "";
                    String[] SMSCollection = new String[6];
                    SMSCollection[0] = "SEVER";
                    SMSCollection[1] = _match_serverUpdateSMS.Groups["ID"].Value;
                    SMSCollection[2] = _match_serverUpdateSMS.Groups["Y"].Value + "/" + _match_serverUpdateSMS.Groups["M"].Value + "/" + _match_serverUpdateSMS.Groups["D"].Value + " AT " + _match_serverUpdateSMS.Groups["Hour"].Value + ":" + _match_serverUpdateSMS.Groups["Minit"].Value + ":" + _match_serverUpdateSMS.Groups["Secon"].Value;
                    SMSCollection[3] = _match_serverUpdateSMS.Groups["LOG"].Value;
                    SMSCollection[4] = _match_serverUpdateSMS.Groups["LAT"].Value;
                    SMSCollection[5] = _match_serverUpdateSMS.Groups["SMS"].Value;
                    _dialogMessageSerialPortSetArry(SMSCollection);
                }
                else if (_match_changePassSMS.Success)
                {
                    _portOpenData = "";
                    String[] SMSCollection = new String[5];
                    SMSCollection[0] = "PASS";
                    SMSCollection[1] = _match_changePassSMS.Groups["ID"].Value;
                    SMSCollection[2] = _match_changePassSMS.Groups["Y"].Value + "/" + _match_changePassSMS.Groups["M"].Value + "/" + _match_changePassSMS.Groups["D"].Value + " AT " + _match_serverUpdateSMS.Groups["Hour"].Value + ":" + _match_serverUpdateSMS.Groups["Minit"].Value + ":" + _match_serverUpdateSMS.Groups["Secon"].Value;
                    SMSCollection[3] = _match_changePassSMS.Groups["PASS"].Value;
                    SMSCollection[4] = _match_changePassSMS.Groups["SMS"].Value;
                    _dialogMessageSerialPortSetArry(SMSCollection);
                }
                else if (_match_shoutDownSMS.Success)
                {
                    _portOpenData = "";
                    String[] SMSCollection = new String[5];
                    SMSCollection[0] = "OFF";
                    SMSCollection[1] = _match_shoutDownSMS.Groups["ID"].Value;
                    SMSCollection[2] = _match_shoutDownSMS.Groups["Y"].Value + "/" + _match_shoutDownSMS.Groups["M"].Value + "/" + _match_shoutDownSMS.Groups["D"].Value + " AT " + _match_serverUpdateSMS.Groups["Hour"].Value + ":" + _match_serverUpdateSMS.Groups["Minit"].Value + ":" + _match_serverUpdateSMS.Groups["Secon"].Value;
                    SMSCollection[3] = _match_shoutDownSMS.Groups["PASS"].Value;
                    SMSCollection[4] = _match_shoutDownSMS.Groups["SMS"].Value;
                    _dialogMessageSerialPortSetArry(SMSCollection);
                }
                else if (_match_SMS.Success)
                {
                    _portOpenData = "";
                    String[] SMSCollection = new String[4];
                    SMSCollection[0] = "NORMAL";
                    SMSCollection[1] = _match_SMS.Groups["ID"].Value;
                    SMSCollection[2] = _match_SMS.Groups["Y"].Value + "/" + _match_SMS.Groups["M"].Value + "/" + _match_SMS.Groups["D"].Value + " AT " + _match_serverUpdateSMS.Groups["Hour"].Value + ":" + _match_serverUpdateSMS.Groups["Minit"].Value + ":" + _match_serverUpdateSMS.Groups["Secon"].Value;
                    SMSCollection[3] = _match_SMS.Groups["SMS"].Value;
                    _dialogMessageSerialPortSetArry(SMSCollection);
                }
                else { }

            }
        }

        private void _dialogMessageSerialPortSetArry(String[] _smsArryPort)
        {
            if (this.InvokeRequired)
            {
                dialogMessageSerialPortTextCallback call = new dialogMessageSerialPortTextCallback(_dialogMessageSerialPortSetArry);
                this.Invoke(call, new object[] { _smsArryPort });
            }
            else
            {
                switch (_smsArryPort[0])
                {
                    case "SEVER":
                        _updateServer(_smsArryPort);
                        return;
                    case "PASS":
                        _changePassWord(_smsArryPort);
                        return;
                    case "OFF":
                        _restartPC(_smsArryPort);
                        return;
                    case "NORMAL":
                        _sendErrorSMS(_smsArryPort);
                        return;
                    default:
                        return;
                }
            }
        }


        #endregion




        #region PORT FUNCTION


        private void _updateServer(String[] SMSArry)
        {
            try
            {
                String CallString = " @CustNumber = '" + SMSArry[1] + "', @LoginPass = '', @LoginType = '', @CustName = 'SMS Customer', @CustGender	= '', @CustEmail = '', @CustAddresOne = '', @CustAddresTwo = '', @CalTimeIN	= '', @CalTimeOut = ''";
                config callServer = new config();
                DataTable dt = callServer.dialogServerInsert("SERVER_NEW_CUSTOMER", CallString);
                DataRow dr = dt.Rows[0];
                String CustomerOK = "";

                if (dr["SUCESS"].ToString() == "M") { CustomerOK = "+" + SMSArry[0] + Environment.NewLine + "New customer, complaint is received."; }
                else { CustomerOK = "+" + SMSArry[1] + Environment.NewLine + "New customer, complaint is received."; }

                CallString = " @USERID ='" + SMSArry[1] + "', @LOG = '" + SMSArry[3] + "', @LAT = '" + SMSArry[4] + "', @DATA = '" + SMSArry[5] + "', @TIME = '" + SMSArry[2] + "'";
                dt = callServer.dialogServerInsert("SERVER_COMPLAINT", CallString);
                DataRow dr2 = dt.Rows[0];

                if (dr2["SUCCES"].ToString() == "1")
                {
                    sendMessage(SMSArry[1],dr2["MESSAGE"].ToString());
                    DialogTitlelabel.Visible = false;
                    labelWelcome.Visible = false;
                    labelPlanning.Visible = false;
                    labelRegion.Visible = false;

                    NewMessageLabel.Visible = true;
                    labelTitleNewSMS.Visible = true;

                    NewMessagePictureBox.Visible = true;
                    pictureBoxViewStart.Visible = false;
                    NewMessageLabel.Text = CustomerOK + Environment.NewLine + dr2["MESSAGE"].ToString();

                    if (notificationToolStripMenuItem.Checked == true)
                    {
                        if (this.ClientRectangle.Contains(this.PointToClient(Cursor.Position)))
                        {
                            formFadeIn(100, this.Opacity, 1);
                        }
                        this.Visible = true;
                        showToolStripMenuItem.Enabled = false;
                        hideToolStripMenuItem.Enabled = true;
                    }
                    return;
                }
                else
                {
                    String smsOption = "\rType:\rLongitude<SPACE>Latitude<SPACE>Your_Complaint.";
                    sendMessage(SMSArry[1],dr2["MESSAGE"].ToString(),smsOption);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The message server serial port can't be opened. Check the COM port and try again." + ex.ToString(), "Message Server GSM SIM 800A COM Port Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Application.Exit();
            }
        }

        private void _changePassWord(String[] SMSArry)
        {
            try
            {
                config callServer = new config();
                DataTable dt = callServer.dialogServerInsert("SERVER_PASS_CHANGE", " @USERID = '" + SMSArry[1] + "', @PASS = '" + SMSArry[3] + "'");
                DataRow dr = dt.Rows[0];

                if (dr["SUCCES"].ToString() == "1")
                {
                    sendMessage(SMSArry[1],dr["MESSAGE"].ToString());
                    return;
                }
                else
                {
                    String smsOption = "\rRegister First.And type:\rPASS<SPACE>new_pass_word<SPACE>phone_number.";
                    sendMessage(SMSArry[1],dr["MESSAGE"].ToString(),smsOption);
                    return;
                }
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show("The message server serial port can't be opened." + Environment.NewLine + "Check the COM port and try again." + Environment.NewLine + Environment.NewLine + ex.ToString(), "Message Server GSM SIM 800A COM Port Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                if (result == DialogResult.OK) { Application.Exit(); }
                else { Application.Exit(); }
            }
        }

        private void _restartPC(String[] SMSArry)
        {
            try
            {
                config callServer = new config();
                DataTable dt = callServer.dialogServerInsert("SERVER_RESTART_PC", " @USERID = '" + SMSArry[1] + "'");
                DataRow dr = dt.Rows[0];

                if (dr["SUCCES"].ToString() == "1")
                {
                    System.Diagnostics.Process.Start("shutdown.exe", "-r -t 0");
                    return;
                }
                else
                {
                    String smsOption = "\rDo not try again.";
                    sendMessage(SMSArry[1], dr["MESSAGE"].ToString(), smsOption);
                    return;
                }
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show("The message server serial port can't be opened." + Environment.NewLine + "Check the COM port and try again." + Environment.NewLine + Environment.NewLine + ex.ToString(), "Message Server GSM SIM 800A COM Port Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                if (result == DialogResult.OK) { Application.Exit(); }
                else { Application.Exit(); }
            }
        }

        private void _sendErrorSMS(String[] SMSArry)
        {
            try
            {
                String smsOption = "\rLongitude<SPACE>Latitude<SPACE>Your_Complaint.";
                sendMessage(SMSArry[1], "Sorry, type:", smsOption);
                return;
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show("The message server serial port can't be opened." + Environment.NewLine + "Check the COM port and try again." + Environment.NewLine + Environment.NewLine + ex.ToString(), "Message Server GSM SIM 800A COM Port Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                if (result == DialogResult.OK) { Application.Exit(); }
                else { Application.Exit(); }
            }
        }

        private void sendMessage(String NUMB, String ESMS, String OPTION = "")
        {
            try
            {
                _dialogMessageServerSerialPort.Write("AT+CMGS=\"+" + NUMB + "\"\r");
                System.Threading.Thread.Sleep(800);
                _dialogMessageServerSerialPort.Write(ESMS + "\r" + OPTION + "\r\rThank You For Using Dialog Complaint Server." + (char)(26));
                System.Threading.Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                MessageBox.Show("The message server serial port can't be opened. Check the COM port and try again." + ex.ToString(), "Message Server GSM SIM 800A COM Port Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Application.Exit();
            }
        }


        #endregion




        #region FORM DATA


        private void DialogNotificationWindow_Load(object sender, EventArgs e)
        {
            if (_dialogMessageServerSerialPort.IsOpen)
            {
                this.Refresh();
                formFadeIn(100, this.Opacity, .5);
                this.Refresh();
            }
            else
            {
                Application.Exit();
            }
        }

        private void DialogNotificationWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            _dialogMessageServerSerialPort.Dispose();
            _dialogMessageServerSerialPort.Close();
            Application.Exit();
        }

        private void formFadeIn(int duration, double startSteps, double endStep)
        {
            Timer timer = new Timer();
            timer.Interval = 1000 / duration;
            timer.Tick += (arg1, arg2) =>
            {
                    Opacity = startSteps;
                    startSteps = startSteps + 0.02;
                    if (startSteps >= endStep)
                    {
                        timer.Stop();
                        timer.Dispose();
                    }
            };
            timer.Start();
        }

        private void formFadeOut(int duration, double startSteps, double endStep)
        {
            Timer timer = new Timer();
            timer.Interval = 1000 / duration;
            timer.Tick += (arg1, arg2) =>
            {
                    Opacity = startSteps;
                    startSteps = startSteps - 0.02;
                    if (startSteps <= endStep)
                    {
                        timer.Stop();
                        timer.Dispose();
                    }
            };
            timer.Start();
        }

        #endregion




        #region UGI DATA

        
        private void DialogNotificationWindow_MouseLeave(object sender, EventArgs e)
        {
            if (!this.ClientRectangle.Contains(this.PointToClient(Cursor.Position)))
            {
               formFadeOut(100, this.Opacity, 0.5);
            }        
        }

        private void DialogNotificationWindow_MouseEnter(object sender, EventArgs e)
        {
            if (this.ClientRectangle.Contains(this.PointToClient(Cursor.Position)))
            {
                formFadeIn(100, this.Opacity, 1);
            }
        }

        private void DialogWindowOpenButton_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Path.Combine(Application.StartupPath, "DIALOGGSM.exe"));
            }
            catch (Exception)
            {
                MessageBox.Show("Can find the instrolation path of dialog coustomer mobile server." + Environment.NewLine + "Try Again.", "Installation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
  
        private void DialogWindowHideButton_Click(object sender, EventArgs e)
        {
            if (!this.ClientRectangle.Contains(this.PointToClient(Cursor.Position)))
            {
                formFadeOut(100, this.Opacity, 0);
            }
            this.Visible = false;
            hideToolStripMenuItem.Enabled = false;
            showToolStripMenuItem.Enabled = true;
        }

        private void DialogWindowPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            x = Control.MousePosition.X - this.Location.X;
            y = Control.MousePosition.Y - this.Location.Y;
        }

        private void DialogWindowPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == System.Windows.Forms.MouseButtons.Left){
                dialogNewPoint = Control.MousePosition;
                dialogNewPoint.X -= (x);
                dialogNewPoint.Y -= (y);
                this.Location = dialogNewPoint;
            }
        }

        private void DialogNotificationWindow_VisibleChanged(object sender, EventArgs e)
        {
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            this.Left = screenWidth - (this.Width + 8);
            this.Top = screenHeight - (this.Height + 5);
        }

        private void serverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogHelpWindow Help = new DialogHelpWindow("dialogTabOne");
            Help.ShowDialog();
        }

        private void proToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogHelpWindow Help = new DialogHelpWindow("dialogTabTwo");
            Help.ShowDialog();
        }

        private void contactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogContactWindow newContact = new DialogContactWindow();
            newContact.ShowDialog();
        }

        private void programmerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogAboutWindow aboutForm = new DialogAboutWindow();
            aboutForm.ShowDialog();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ClientRectangle.Contains(this.PointToClient(Cursor.Position)))
            {
                formFadeIn(100, this.Opacity, 1);
            }
            this.Visible = true;
            showToolStripMenuItem.Enabled = false;
            hideToolStripMenuItem.Enabled = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to close the dialog message server  ?", "Dialog Message Server Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (result == DialogResult.No)
            {
                if (this.ClientRectangle.Contains(this.PointToClient(Cursor.Position)))
                {
                    formFadeIn(100, this.Opacity, 1);
                }
                this.Visible = true;
                showToolStripMenuItem.Enabled = false;
                hideToolStripMenuItem.Enabled = true;
            }
            else
            {
                if (!this.ClientRectangle.Contains(this.PointToClient(Cursor.Position)))
                {
                    formFadeOut(100, this.Opacity, 0);
                }
                this.Visible = false;
                hideToolStripMenuItem.Enabled = false;
                showToolStripMenuItem.Enabled = true;
            }
        }

        private void notificationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (notificationToolStripMenuItem.Checked) {
                notificationToolStripMenuItem.Checked = false;
            }
            else{
                notificationToolStripMenuItem.Checked = true;
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //DialogHelpWindow Help = new DialogHelpWindow("dialogTabOne");
            //Help.ShowDialog();
        }


        #endregion

 


    }
}
