namespace DIALOGGSM
{
    partial class DialogSaveExcelCustomer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogSaveExcelCustomer));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelDialogComplaintTopic = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelDateAndMonth = new System.Windows.Forms.Panel();
            this.labelTo = new System.Windows.Forms.Label();
            this.labelFrom = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.monthCalendarFromDate = new System.Windows.Forms.MonthCalendar();
            this.monthCalendarToDate = new System.Windows.Forms.MonthCalendar();
            this.radioButton1000Customers = new System.Windows.Forms.RadioButton();
            this.radioButton100Customers = new System.Windows.Forms.RadioButton();
            this.radioButton10Customers = new System.Windows.Forms.RadioButton();
            this.radioButtonAllCustomers = new System.Windows.Forms.RadioButton();
            this.radioButtonSelectDate = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxCustType = new System.Windows.Forms.ComboBox();
            this.radioButtonOrderByIDate = new System.Windows.Forms.RadioButton();
            this.radioButtonOrderByMDate = new System.Windows.Forms.RadioButton();
            this.checkBoxCallOut = new System.Windows.Forms.CheckBox();
            this.checkBoxCallIn = new System.Windows.Forms.CheckBox();
            this.checkBoxCustAddresTwo = new System.Windows.Forms.CheckBox();
            this.checkBoxCustAddresOne = new System.Windows.Forms.CheckBox();
            this.checkBoxCustEmail = new System.Windows.Forms.CheckBox();
            this.checkBoxCustGender = new System.Windows.Forms.CheckBox();
            this.checkBoxCustNumber = new System.Windows.Forms.CheckBox();
            this.checkBoxCustName = new System.Windows.Forms.CheckBox();
            this.checkBoxCustID = new System.Windows.Forms.CheckBox();
            this.buttonOpenExcelFile = new System.Windows.Forms.Button();
            this.buttonSaveExcelFile = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelLog = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panelDateAndMonth.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(780, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 46;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // labelDialogComplaintTopic
            // 
            this.labelDialogComplaintTopic.AutoSize = true;
            this.labelDialogComplaintTopic.BackColor = System.Drawing.Color.White;
            this.labelDialogComplaintTopic.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelDialogComplaintTopic.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelDialogComplaintTopic.Location = new System.Drawing.Point(55, 14);
            this.labelDialogComplaintTopic.Name = "labelDialogComplaintTopic";
            this.labelDialogComplaintTopic.Size = new System.Drawing.Size(343, 24);
            this.labelDialogComplaintTopic.TabIndex = 49;
            this.labelDialogComplaintTopic.Text = "DIALOG COMPLAINT CUSTOMERS";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 403);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(760, 23);
            this.progressBar1.TabIndex = 50;
            this.progressBar1.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(12, 454);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(760, 23);
            this.progressBar2.TabIndex = 51;
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(636, 515);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(136, 23);
            this.buttonClose.TabIndex = 52;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panelDateAndMonth);
            this.groupBox1.Controls.Add(this.radioButton1000Customers);
            this.groupBox1.Controls.Add(this.radioButton100Customers);
            this.groupBox1.Controls.Add(this.radioButton10Customers);
            this.groupBox1.Controls.Add(this.radioButtonAllCustomers);
            this.groupBox1.Controls.Add(this.radioButtonSelectDate);
            this.groupBox1.Location = new System.Drawing.Point(12, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(760, 211);
            this.groupBox1.TabIndex = 55;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Select Customer Excel Data ";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // panelDateAndMonth
            // 
            this.panelDateAndMonth.Controls.Add(this.labelTo);
            this.panelDateAndMonth.Controls.Add(this.labelFrom);
            this.panelDateAndMonth.Controls.Add(this.label2);
            this.panelDateAndMonth.Controls.Add(this.label1);
            this.panelDateAndMonth.Controls.Add(this.monthCalendarFromDate);
            this.panelDateAndMonth.Controls.Add(this.monthCalendarToDate);
            this.panelDateAndMonth.Enabled = false;
            this.panelDateAndMonth.Location = new System.Drawing.Point(247, 10);
            this.panelDateAndMonth.Name = "panelDateAndMonth";
            this.panelDateAndMonth.Size = new System.Drawing.Size(496, 197);
            this.panelDateAndMonth.TabIndex = 60;
            // 
            // labelTo
            // 
            this.labelTo.AutoSize = true;
            this.labelTo.Location = new System.Drawing.Point(294, 12);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(30, 13);
            this.labelTo.TabIndex = 8;
            this.labelTo.Text = "Date";
            // 
            // labelFrom
            // 
            this.labelFrom.AutoSize = true;
            this.labelFrom.Location = new System.Drawing.Point(63, 12);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(30, 13);
            this.labelFrom.TabIndex = 7;
            this.labelFrom.Text = "Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(254, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "To  :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "From  :";
            // 
            // monthCalendarFromDate
            // 
            this.monthCalendarFromDate.Location = new System.Drawing.Point(12, 29);
            this.monthCalendarFromDate.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.monthCalendarFromDate.MinDate = new System.DateTime(2015, 1, 1, 0, 0, 0, 0);
            this.monthCalendarFromDate.Name = "monthCalendarFromDate";
            this.monthCalendarFromDate.TabIndex = 3;
            this.monthCalendarFromDate.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendarFromDate_DateChanged);
            // 
            // monthCalendarToDate
            // 
            this.monthCalendarToDate.Location = new System.Drawing.Point(257, 29);
            this.monthCalendarToDate.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.monthCalendarToDate.MinDate = new System.DateTime(2015, 1, 1, 0, 0, 0, 0);
            this.monthCalendarToDate.Name = "monthCalendarToDate";
            this.monthCalendarToDate.TabIndex = 4;
            this.monthCalendarToDate.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendarToDate_DateChanged);
            // 
            // radioButton1000Customers
            // 
            this.radioButton1000Customers.AutoSize = true;
            this.radioButton1000Customers.Location = new System.Drawing.Point(49, 159);
            this.radioButton1000Customers.Name = "radioButton1000Customers";
            this.radioButton1000Customers.Size = new System.Drawing.Size(123, 17);
            this.radioButton1000Customers.TabIndex = 2;
            this.radioButton1000Customers.Text = "Top 1000 Customers";
            this.radioButton1000Customers.UseVisualStyleBackColor = true;
            this.radioButton1000Customers.CheckedChanged += new System.EventHandler(this.radioButton1000Customers_CheckedChanged);
            // 
            // radioButton100Customers
            // 
            this.radioButton100Customers.AutoSize = true;
            this.radioButton100Customers.Location = new System.Drawing.Point(49, 128);
            this.radioButton100Customers.Name = "radioButton100Customers";
            this.radioButton100Customers.Size = new System.Drawing.Size(117, 17);
            this.radioButton100Customers.TabIndex = 1;
            this.radioButton100Customers.Text = "Top 100 Customers";
            this.radioButton100Customers.UseVisualStyleBackColor = true;
            this.radioButton100Customers.CheckedChanged += new System.EventHandler(this.radioButton100Customers_CheckedChanged);
            // 
            // radioButton10Customers
            // 
            this.radioButton10Customers.AutoSize = true;
            this.radioButton10Customers.Location = new System.Drawing.Point(49, 97);
            this.radioButton10Customers.Name = "radioButton10Customers";
            this.radioButton10Customers.Size = new System.Drawing.Size(111, 17);
            this.radioButton10Customers.TabIndex = 3;
            this.radioButton10Customers.Text = "Top 10 Customers";
            this.radioButton10Customers.UseVisualStyleBackColor = true;
            this.radioButton10Customers.CheckedChanged += new System.EventHandler(this.radioButton10Customers_CheckedChanged);
            // 
            // radioButtonAllCustomers
            // 
            this.radioButtonAllCustomers.AutoSize = true;
            this.radioButtonAllCustomers.Checked = true;
            this.radioButtonAllCustomers.Location = new System.Drawing.Point(49, 66);
            this.radioButtonAllCustomers.Name = "radioButtonAllCustomers";
            this.radioButtonAllCustomers.Size = new System.Drawing.Size(88, 17);
            this.radioButtonAllCustomers.TabIndex = 0;
            this.radioButtonAllCustomers.TabStop = true;
            this.radioButtonAllCustomers.Text = "All Customers";
            this.radioButtonAllCustomers.UseVisualStyleBackColor = true;
            this.radioButtonAllCustomers.CheckedChanged += new System.EventHandler(this.radioButtonAllCustomers_CheckedChanged);
            // 
            // radioButtonSelectDate
            // 
            this.radioButtonSelectDate.AutoSize = true;
            this.radioButtonSelectDate.Location = new System.Drawing.Point(49, 35);
            this.radioButtonSelectDate.Name = "radioButtonSelectDate";
            this.radioButtonSelectDate.Size = new System.Drawing.Size(125, 17);
            this.radioButtonSelectDate.TabIndex = 2;
            this.radioButtonSelectDate.Text = "Order By Select Date";
            this.radioButtonSelectDate.UseVisualStyleBackColor = true;
            this.radioButtonSelectDate.CheckedChanged += new System.EventHandler(this.radioButtonSelectDate_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxCustType);
            this.groupBox2.Controls.Add(this.radioButtonOrderByIDate);
            this.groupBox2.Controls.Add(this.radioButtonOrderByMDate);
            this.groupBox2.Controls.Add(this.checkBoxCallOut);
            this.groupBox2.Controls.Add(this.checkBoxCallIn);
            this.groupBox2.Controls.Add(this.checkBoxCustAddresTwo);
            this.groupBox2.Controls.Add(this.checkBoxCustAddresOne);
            this.groupBox2.Controls.Add(this.checkBoxCustEmail);
            this.groupBox2.Controls.Add(this.checkBoxCustGender);
            this.groupBox2.Controls.Add(this.checkBoxCustNumber);
            this.groupBox2.Controls.Add(this.checkBoxCustName);
            this.groupBox2.Controls.Add(this.checkBoxCustID);
            this.groupBox2.Location = new System.Drawing.Point(12, 289);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(760, 103);
            this.groupBox2.TabIndex = 56;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " Select Customer Details ";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // comboBoxCustType
            // 
            this.comboBoxCustType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCustType.FormattingEnabled = true;
            this.comboBoxCustType.Location = new System.Drawing.Point(544, 23);
            this.comboBoxCustType.Name = "comboBoxCustType";
            this.comboBoxCustType.Size = new System.Drawing.Size(187, 21);
            this.comboBoxCustType.TabIndex = 65;
            this.comboBoxCustType.SelectedIndexChanged += new System.EventHandler(this.comboBoxCustType_SelectedIndexChanged);
            // 
            // radioButtonOrderByIDate
            // 
            this.radioButtonOrderByIDate.AutoSize = true;
            this.radioButtonOrderByIDate.Location = new System.Drawing.Point(573, 69);
            this.radioButtonOrderByIDate.Name = "radioButtonOrderByIDate";
            this.radioButtonOrderByIDate.Size = new System.Drawing.Size(133, 17);
            this.radioButtonOrderByIDate.TabIndex = 0;
            this.radioButtonOrderByIDate.Text = "Order By Inserted Date";
            this.radioButtonOrderByIDate.UseVisualStyleBackColor = true;
            this.radioButtonOrderByIDate.CheckedChanged += new System.EventHandler(this.radioButton7_CheckedChanged);
            // 
            // radioButtonOrderByMDate
            // 
            this.radioButtonOrderByMDate.AutoSize = true;
            this.radioButtonOrderByMDate.Checked = true;
            this.radioButtonOrderByMDate.Location = new System.Drawing.Point(573, 48);
            this.radioButtonOrderByMDate.Name = "radioButtonOrderByMDate";
            this.radioButtonOrderByMDate.Size = new System.Drawing.Size(135, 17);
            this.radioButtonOrderByMDate.TabIndex = 1;
            this.radioButtonOrderByMDate.TabStop = true;
            this.radioButtonOrderByMDate.Text = "Order By Modified Date";
            this.radioButtonOrderByMDate.UseVisualStyleBackColor = true;
            // 
            // checkBoxCallOut
            // 
            this.checkBoxCallOut.AutoSize = true;
            this.checkBoxCallOut.Checked = true;
            this.checkBoxCallOut.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCallOut.Location = new System.Drawing.Point(372, 75);
            this.checkBoxCallOut.Name = "checkBoxCallOut";
            this.checkBoxCallOut.Size = new System.Drawing.Size(136, 17);
            this.checkBoxCallOut.TabIndex = 11;
            this.checkBoxCallOut.Text = "Customer Call Time Out";
            this.checkBoxCallOut.UseVisualStyleBackColor = true;
            // 
            // checkBoxCallIn
            // 
            this.checkBoxCallIn.AutoSize = true;
            this.checkBoxCallIn.Checked = true;
            this.checkBoxCallIn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCallIn.Location = new System.Drawing.Point(214, 75);
            this.checkBoxCallIn.Name = "checkBoxCallIn";
            this.checkBoxCallIn.Size = new System.Drawing.Size(128, 17);
            this.checkBoxCallIn.TabIndex = 10;
            this.checkBoxCallIn.Text = "Customer Call Time In";
            this.checkBoxCallIn.UseVisualStyleBackColor = true;
            // 
            // checkBoxCustAddresTwo
            // 
            this.checkBoxCustAddresTwo.AutoSize = true;
            this.checkBoxCustAddresTwo.Checked = true;
            this.checkBoxCustAddresTwo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCustAddresTwo.Location = new System.Drawing.Point(47, 75);
            this.checkBoxCustAddresTwo.Name = "checkBoxCustAddresTwo";
            this.checkBoxCustAddresTwo.Size = new System.Drawing.Size(130, 17);
            this.checkBoxCustAddresTwo.TabIndex = 9;
            this.checkBoxCustAddresTwo.Text = "Customer Addres Two";
            this.checkBoxCustAddresTwo.UseVisualStyleBackColor = true;
            // 
            // checkBoxCustAddresOne
            // 
            this.checkBoxCustAddresOne.AutoSize = true;
            this.checkBoxCustAddresOne.Checked = true;
            this.checkBoxCustAddresOne.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCustAddresOne.Location = new System.Drawing.Point(372, 50);
            this.checkBoxCustAddresOne.Name = "checkBoxCustAddresOne";
            this.checkBoxCustAddresOne.Size = new System.Drawing.Size(129, 17);
            this.checkBoxCustAddresOne.TabIndex = 8;
            this.checkBoxCustAddresOne.Text = "Customer Addres One";
            this.checkBoxCustAddresOne.UseVisualStyleBackColor = true;
            // 
            // checkBoxCustEmail
            // 
            this.checkBoxCustEmail.AutoSize = true;
            this.checkBoxCustEmail.Checked = true;
            this.checkBoxCustEmail.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCustEmail.Location = new System.Drawing.Point(214, 50);
            this.checkBoxCustEmail.Name = "checkBoxCustEmail";
            this.checkBoxCustEmail.Size = new System.Drawing.Size(98, 17);
            this.checkBoxCustEmail.TabIndex = 7;
            this.checkBoxCustEmail.Text = "Customer Email";
            this.checkBoxCustEmail.UseVisualStyleBackColor = true;
            // 
            // checkBoxCustGender
            // 
            this.checkBoxCustGender.AutoSize = true;
            this.checkBoxCustGender.Checked = true;
            this.checkBoxCustGender.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCustGender.Location = new System.Drawing.Point(47, 50);
            this.checkBoxCustGender.Name = "checkBoxCustGender";
            this.checkBoxCustGender.Size = new System.Drawing.Size(108, 17);
            this.checkBoxCustGender.TabIndex = 5;
            this.checkBoxCustGender.Text = "Customer Gender";
            this.checkBoxCustGender.UseVisualStyleBackColor = true;
            // 
            // checkBoxCustNumber
            // 
            this.checkBoxCustNumber.AutoSize = true;
            this.checkBoxCustNumber.Checked = true;
            this.checkBoxCustNumber.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCustNumber.Location = new System.Drawing.Point(214, 25);
            this.checkBoxCustNumber.Name = "checkBoxCustNumber";
            this.checkBoxCustNumber.Size = new System.Drawing.Size(110, 17);
            this.checkBoxCustNumber.TabIndex = 4;
            this.checkBoxCustNumber.Text = "Customer Number";
            this.checkBoxCustNumber.UseVisualStyleBackColor = true;
            // 
            // checkBoxCustName
            // 
            this.checkBoxCustName.AutoSize = true;
            this.checkBoxCustName.Checked = true;
            this.checkBoxCustName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCustName.Location = new System.Drawing.Point(372, 25);
            this.checkBoxCustName.Name = "checkBoxCustName";
            this.checkBoxCustName.Size = new System.Drawing.Size(101, 17);
            this.checkBoxCustName.TabIndex = 3;
            this.checkBoxCustName.Text = "Customer Name";
            this.checkBoxCustName.UseVisualStyleBackColor = true;
            // 
            // checkBoxCustID
            // 
            this.checkBoxCustID.AutoSize = true;
            this.checkBoxCustID.Checked = true;
            this.checkBoxCustID.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCustID.Location = new System.Drawing.Point(47, 25);
            this.checkBoxCustID.Name = "checkBoxCustID";
            this.checkBoxCustID.Size = new System.Drawing.Size(84, 17);
            this.checkBoxCustID.TabIndex = 2;
            this.checkBoxCustID.Text = "Customer ID";
            this.checkBoxCustID.UseVisualStyleBackColor = true;
            // 
            // buttonOpenExcelFile
            // 
            this.buttonOpenExcelFile.Location = new System.Drawing.Point(324, 515);
            this.buttonOpenExcelFile.Name = "buttonOpenExcelFile";
            this.buttonOpenExcelFile.Size = new System.Drawing.Size(136, 23);
            this.buttonOpenExcelFile.TabIndex = 57;
            this.buttonOpenExcelFile.Text = "Open Excel File";
            this.buttonOpenExcelFile.UseVisualStyleBackColor = true;
            // 
            // buttonSaveExcelFile
            // 
            this.buttonSaveExcelFile.Location = new System.Drawing.Point(12, 515);
            this.buttonSaveExcelFile.Name = "buttonSaveExcelFile";
            this.buttonSaveExcelFile.Size = new System.Drawing.Size(136, 23);
            this.buttonSaveExcelFile.TabIndex = 59;
            this.buttonSaveExcelFile.Text = "Save Excel File";
            this.buttonSaveExcelFile.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 429);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 60;
            this.label3.Text = "Processing . . .";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 480);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 61;
            this.label4.Text = "Processing . . .";
            // 
            // labelLog
            // 
            this.labelLog.AutoSize = true;
            this.labelLog.ForeColor = System.Drawing.Color.LightGreen;
            this.labelLog.Location = new System.Drawing.Point(4, 545);
            this.labelLog.Name = "labelLog";
            this.labelLog.Size = new System.Drawing.Size(29, 13);
            this.labelLog.TabIndex = 100;
            this.labelLog.Text = "LOG";
            // 
            // DialogSaveExcelCustomer
            // 
            this.AcceptButton = this.buttonOpenExcelFile;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.labelLog);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonSaveExcelFile);
            this.Controls.Add(this.buttonOpenExcelFile);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.labelDialogComplaintTopic);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "DialogSaveExcelCustomer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " Save All Customers To Excel File";
            this.Load += new System.EventHandler(this.DialogSaveExcelCustomer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelDateAndMonth.ResumeLayout(false);
            this.panelDateAndMonth.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelDialogComplaintTopic;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonOpenExcelFile;
        private System.Windows.Forms.Button buttonSaveExcelFile;
        private System.Windows.Forms.RadioButton radioButtonOrderByMDate;
        private System.Windows.Forms.RadioButton radioButtonOrderByIDate;
        private System.Windows.Forms.RadioButton radioButton100Customers;
        private System.Windows.Forms.RadioButton radioButton10Customers;
        private System.Windows.Forms.RadioButton radioButtonAllCustomers;
        private System.Windows.Forms.RadioButton radioButton1000Customers;
        private System.Windows.Forms.MonthCalendar monthCalendarToDate;
        private System.Windows.Forms.MonthCalendar monthCalendarFromDate;
        private System.Windows.Forms.RadioButton radioButtonSelectDate;
        private System.Windows.Forms.CheckBox checkBoxCallOut;
        private System.Windows.Forms.CheckBox checkBoxCallIn;
        private System.Windows.Forms.CheckBox checkBoxCustAddresTwo;
        private System.Windows.Forms.CheckBox checkBoxCustAddresOne;
        private System.Windows.Forms.CheckBox checkBoxCustEmail;
        private System.Windows.Forms.CheckBox checkBoxCustGender;
        private System.Windows.Forms.CheckBox checkBoxCustNumber;
        private System.Windows.Forms.CheckBox checkBoxCustName;
        private System.Windows.Forms.CheckBox checkBoxCustID;
        private System.Windows.Forms.Panel panelDateAndMonth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.ComboBox comboBoxCustType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelLog;
    }
}