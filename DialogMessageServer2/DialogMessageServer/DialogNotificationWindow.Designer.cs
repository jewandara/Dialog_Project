namespace DialogMessageServer
{
    partial class DialogNotificationWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogNotificationWindow));
            this.DialogWindowPictureBox = new System.Windows.Forms.PictureBox();
            this.DialogNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.DialogContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notificationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.proToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.programmerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DialogTitlelabel = new System.Windows.Forms.Label();
            this.NewMessageLabel = new System.Windows.Forms.Label();
            this.NewMessagePictureBox = new System.Windows.Forms.PictureBox();
            this._dialogMessageServerSerialPort = new System.IO.Ports.SerialPort(this.components);
            this.labelTitleNewSMS = new System.Windows.Forms.Label();
            this.pictureBoxViewStart = new System.Windows.Forms.PictureBox();
            this.DialogWindowHideButton = new System.Windows.Forms.Button();
            this.DialogWindowOpenButton = new System.Windows.Forms.Button();
            this.labelWelcome = new System.Windows.Forms.Label();
            this.labelPlanning = new System.Windows.Forms.Label();
            this.labelRegion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DialogWindowPictureBox)).BeginInit();
            this.DialogContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NewMessagePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxViewStart)).BeginInit();
            this.SuspendLayout();
            // 
            // DialogWindowPictureBox
            // 
            this.DialogWindowPictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DialogWindowPictureBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.DialogWindowPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("DialogWindowPictureBox.Image")));
            this.DialogWindowPictureBox.Location = new System.Drawing.Point(0, 0);
            this.DialogWindowPictureBox.Name = "DialogWindowPictureBox";
            this.DialogWindowPictureBox.Size = new System.Drawing.Size(400, 34);
            this.DialogWindowPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.DialogWindowPictureBox.TabIndex = 0;
            this.DialogWindowPictureBox.TabStop = false;
            this.DialogWindowPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DialogWindowPictureBox_MouseDown);
            this.DialogWindowPictureBox.MouseLeave += new System.EventHandler(this.DialogNotificationWindow_MouseLeave);
            this.DialogWindowPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DialogWindowPictureBox_MouseMove);
            // 
            // DialogNotifyIcon
            // 
            this.DialogNotifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.DialogNotifyIcon.BalloonTipText = "Dialog Customer Message Notification Server";
            this.DialogNotifyIcon.BalloonTipTitle = "Dialog GSM";
            this.DialogNotifyIcon.ContextMenuStrip = this.DialogContextMenuStrip;
            this.DialogNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("DialogNotifyIcon.Icon")));
            this.DialogNotifyIcon.Text = "Dialog Customer Message Notification Server";
            this.DialogNotifyIcon.Visible = true;
            // 
            // DialogContextMenuStrip
            // 
            this.DialogContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.openToolStripMenuItem,
            this.showToolStripMenuItem,
            this.hideToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.DialogContextMenuStrip.Name = "DialogContextMenuStrip";
            this.DialogContextMenuStrip.Size = new System.Drawing.Size(153, 158);
            this.DialogContextMenuStrip.Text = "Dialog Customer Message Server Notification Context Menu";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.notificationToolStripMenuItem,
            this.serverToolStripMenuItem,
            this.proToolStripMenuItem});
            this.helpToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripMenuItem.Image")));
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.helpToolStripMenuItem.Text = "&Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // notificationToolStripMenuItem
            // 
            this.notificationToolStripMenuItem.Checked = true;
            this.notificationToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.notificationToolStripMenuItem.Name = "notificationToolStripMenuItem";
            this.notificationToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.notificationToolStripMenuItem.Text = "Notification";
            this.notificationToolStripMenuItem.Click += new System.EventHandler(this.notificationToolStripMenuItem_Click);
            // 
            // serverToolStripMenuItem
            // 
            this.serverToolStripMenuItem.Name = "serverToolStripMenuItem";
            this.serverToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.serverToolStripMenuItem.Text = "Server";
            this.serverToolStripMenuItem.Click += new System.EventHandler(this.serverToolStripMenuItem_Click);
            // 
            // proToolStripMenuItem
            // 
            this.proToolStripMenuItem.Name = "proToolStripMenuItem";
            this.proToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.proToolStripMenuItem.Text = "Program";
            this.proToolStripMenuItem.Click += new System.EventHandler(this.proToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contactToolStripMenuItem,
            this.programmerToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            // 
            // contactToolStripMenuItem
            // 
            this.contactToolStripMenuItem.Name = "contactToolStripMenuItem";
            this.contactToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.contactToolStripMenuItem.Text = "Contact";
            this.contactToolStripMenuItem.Click += new System.EventHandler(this.contactToolStripMenuItem_Click);
            // 
            // programmerToolStripMenuItem
            // 
            this.programmerToolStripMenuItem.Name = "programmerToolStripMenuItem";
            this.programmerToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.programmerToolStripMenuItem.Text = "Programmer";
            this.programmerToolStripMenuItem.Click += new System.EventHandler(this.programmerToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.DialogWindowOpenButton_Click);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("showToolStripMenuItem.Image")));
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.showToolStripMenuItem.Text = "&Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // hideToolStripMenuItem
            // 
            this.hideToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("hideToolStripMenuItem.Image")));
            this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            this.hideToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.hideToolStripMenuItem.Text = "&Hide";
            this.hideToolStripMenuItem.Click += new System.EventHandler(this.DialogWindowHideButton_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exitToolStripMenuItem.Image")));
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // DialogTitlelabel
            // 
            this.DialogTitlelabel.AutoSize = true;
            this.DialogTitlelabel.BackColor = System.Drawing.Color.White;
            this.DialogTitlelabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DialogTitlelabel.Location = new System.Drawing.Point(181, 90);
            this.DialogTitlelabel.Name = "DialogTitlelabel";
            this.DialogTitlelabel.Size = new System.Drawing.Size(195, 18);
            this.DialogTitlelabel.TabIndex = 1;
            this.DialogTitlelabel.Text = "Dialog Customer Complaint.";
            // 
            // NewMessageLabel
            // 
            this.NewMessageLabel.AutoSize = true;
            this.NewMessageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewMessageLabel.Location = new System.Drawing.Point(116, 87);
            this.NewMessageLabel.Name = "NewMessageLabel";
            this.NewMessageLabel.Size = new System.Drawing.Size(28, 16);
            this.NewMessageLabel.TabIndex = 3;
            this.NewMessageLabel.Text = "text";
            this.NewMessageLabel.Visible = false;
            // 
            // NewMessagePictureBox
            // 
            this.NewMessagePictureBox.Image = ((System.Drawing.Image)(resources.GetObject("NewMessagePictureBox.Image")));
            this.NewMessagePictureBox.Location = new System.Drawing.Point(9, 40);
            this.NewMessagePictureBox.Name = "NewMessagePictureBox";
            this.NewMessagePictureBox.Size = new System.Drawing.Size(115, 89);
            this.NewMessagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.NewMessagePictureBox.TabIndex = 5;
            this.NewMessagePictureBox.TabStop = false;
            this.NewMessagePictureBox.Visible = false;
            // 
            // _dialogMessageServerSerialPort
            // 
            this._dialogMessageServerSerialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this._dialogMessageServerSerialPort_DataReceived);
            // 
            // labelTitleNewSMS
            // 
            this.labelTitleNewSMS.AutoSize = true;
            this.labelTitleNewSMS.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitleNewSMS.Location = new System.Drawing.Point(114, 53);
            this.labelTitleNewSMS.Name = "labelTitleNewSMS";
            this.labelTitleNewSMS.Size = new System.Drawing.Size(202, 25);
            this.labelTitleNewSMS.TabIndex = 7;
            this.labelTitleNewSMS.Text = "NEW COMPLAINT";
            this.labelTitleNewSMS.Visible = false;
            // 
            // pictureBoxViewStart
            // 
            this.pictureBoxViewStart.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxViewStart.Image")));
            this.pictureBoxViewStart.Location = new System.Drawing.Point(46, 49);
            this.pictureBoxViewStart.Name = "pictureBoxViewStart";
            this.pictureBoxViewStart.Size = new System.Drawing.Size(120, 99);
            this.pictureBoxViewStart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxViewStart.TabIndex = 8;
            this.pictureBoxViewStart.TabStop = false;
            // 
            // DialogWindowHideButton
            // 
            this.DialogWindowHideButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DialogWindowHideButton.BackgroundImage")));
            this.DialogWindowHideButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DialogWindowHideButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DialogWindowHideButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DialogWindowHideButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DialogWindowHideButton.ForeColor = System.Drawing.Color.White;
            this.DialogWindowHideButton.Location = new System.Drawing.Point(292, 162);
            this.DialogWindowHideButton.Name = "DialogWindowHideButton";
            this.DialogWindowHideButton.Size = new System.Drawing.Size(63, 26);
            this.DialogWindowHideButton.TabIndex = 6;
            this.DialogWindowHideButton.Text = "Hide";
            this.DialogWindowHideButton.UseVisualStyleBackColor = true;
            this.DialogWindowHideButton.Click += new System.EventHandler(this.DialogWindowHideButton_Click);
            // 
            // DialogWindowOpenButton
            // 
            this.DialogWindowOpenButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DialogWindowOpenButton.BackgroundImage")));
            this.DialogWindowOpenButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DialogWindowOpenButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DialogWindowOpenButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DialogWindowOpenButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DialogWindowOpenButton.ForeColor = System.Drawing.Color.White;
            this.DialogWindowOpenButton.Location = new System.Drawing.Point(46, 162);
            this.DialogWindowOpenButton.Name = "DialogWindowOpenButton";
            this.DialogWindowOpenButton.Size = new System.Drawing.Size(63, 26);
            this.DialogWindowOpenButton.TabIndex = 9;
            this.DialogWindowOpenButton.Text = "Open";
            this.DialogWindowOpenButton.UseVisualStyleBackColor = true;
            this.DialogWindowOpenButton.Click += new System.EventHandler(this.DialogWindowOpenButton_Click);
            // 
            // labelWelcome
            // 
            this.labelWelcome.AutoSize = true;
            this.labelWelcome.BackColor = System.Drawing.Color.White;
            this.labelWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWelcome.Location = new System.Drawing.Point(180, 57);
            this.labelWelcome.Name = "labelWelcome";
            this.labelWelcome.Size = new System.Drawing.Size(91, 24);
            this.labelWelcome.TabIndex = 10;
            this.labelWelcome.Text = "Welcome";
            // 
            // labelPlanning
            // 
            this.labelPlanning.AutoSize = true;
            this.labelPlanning.BackColor = System.Drawing.Color.White;
            this.labelPlanning.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlanning.Location = new System.Drawing.Point(182, 108);
            this.labelPlanning.Name = "labelPlanning";
            this.labelPlanning.Size = new System.Drawing.Size(143, 12);
            this.labelPlanning.TabIndex = 11;
            this.labelPlanning.Text = "Dialog Access Network Planning,";
            // 
            // labelRegion
            // 
            this.labelRegion.AutoSize = true;
            this.labelRegion.BackColor = System.Drawing.Color.White;
            this.labelRegion.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRegion.Location = new System.Drawing.Point(182, 121);
            this.labelRegion.Name = "labelRegion";
            this.labelRegion.Size = new System.Drawing.Size(99, 12);
            this.labelRegion.TabIndex = 12;
            this.labelRegion.Text = "Western North Region.";
            // 
            // DialogNotificationWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(400, 200);
            this.ControlBox = false;
            this.Controls.Add(this.labelRegion);
            this.Controls.Add(this.labelPlanning);
            this.Controls.Add(this.labelWelcome);
            this.Controls.Add(this.DialogWindowOpenButton);
            this.Controls.Add(this.NewMessageLabel);
            this.Controls.Add(this.pictureBoxViewStart);
            this.Controls.Add(this.labelTitleNewSMS);
            this.Controls.Add(this.DialogWindowHideButton);
            this.Controls.Add(this.NewMessagePictureBox);
            this.Controls.Add(this.DialogTitlelabel);
            this.Controls.Add(this.DialogWindowPictureBox);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(400, 200);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 200);
            this.Name = "DialogNotificationWindow";
            this.Opacity = 0D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "DialogNotificationWindow";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DialogNotificationWindow_FormClosing);
            this.Load += new System.EventHandler(this.DialogNotificationWindow_Load);
            this.VisibleChanged += new System.EventHandler(this.DialogNotificationWindow_VisibleChanged);
            this.MouseEnter += new System.EventHandler(this.DialogNotificationWindow_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.DialogNotificationWindow_MouseLeave);
            this.MouseHover += new System.EventHandler(this.DialogNotificationWindow_MouseEnter);
            ((System.ComponentModel.ISupportInitialize)(this.DialogWindowPictureBox)).EndInit();
            this.DialogContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NewMessagePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxViewStart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox DialogWindowPictureBox;
        private System.Windows.Forms.NotifyIcon DialogNotifyIcon;
        private System.Windows.Forms.Label DialogTitlelabel;
        private System.Windows.Forms.Label NewMessageLabel;
        private System.Windows.Forms.PictureBox NewMessagePictureBox;
        private System.IO.Ports.SerialPort _dialogMessageServerSerialPort;
        private System.Windows.Forms.ContextMenuStrip DialogContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem notificationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem proToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contactToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem programmerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label labelTitleNewSMS;
        private System.Windows.Forms.PictureBox pictureBoxViewStart;
        private System.Windows.Forms.Button DialogWindowHideButton;
        private System.Windows.Forms.Button DialogWindowOpenButton;
        private System.Windows.Forms.Label labelWelcome;
        private System.Windows.Forms.Label labelPlanning;
        private System.Windows.Forms.Label labelRegion;
    }
}