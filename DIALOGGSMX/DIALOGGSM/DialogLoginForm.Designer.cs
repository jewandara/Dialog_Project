namespace DIALOGGSM
{
    partial class DialogLoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogLoginForm));
            this.pictureBoxLoginForm = new System.Windows.Forms.PictureBox();
            this.buttonEnter = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.version = new System.Windows.Forms.Label();
            this.ProgrameName = new System.Windows.Forms.Label();
            this.LocationLabel = new System.Windows.Forms.Label();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.textBoxPassWord = new System.Windows.Forms.TextBox();
            this.labelProKEY = new System.Windows.Forms.Label();
            this.labelMessage = new System.Windows.Forms.Label();
            this.labelMessage2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoginForm)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxLoginForm
            // 
            this.pictureBoxLoginForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxLoginForm.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxLoginForm.Image")));
            this.pictureBoxLoginForm.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxLoginForm.Name = "pictureBoxLoginForm";
            this.pictureBoxLoginForm.Size = new System.Drawing.Size(750, 380);
            this.pictureBoxLoginForm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLoginForm.TabIndex = 0;
            this.pictureBoxLoginForm.TabStop = false;
            this.pictureBoxLoginForm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBoxLoginForm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // buttonEnter
            // 
            this.buttonEnter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonEnter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEnter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEnter.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonEnter.Image = ((System.Drawing.Image)(resources.GetObject("buttonEnter.Image")));
            this.buttonEnter.Location = new System.Drawing.Point(603, 296);
            this.buttonEnter.Name = "buttonEnter";
            this.buttonEnter.Size = new System.Drawing.Size(83, 32);
            this.buttonEnter.TabIndex = 1;
            this.buttonEnter.Text = "ENTER";
            this.buttonEnter.UseVisualStyleBackColor = true;
            this.buttonEnter.Click += new System.EventHandler(this.button1_Click);
            this.buttonEnter.MouseLeave += new System.EventHandler(this.buttonEnter_MouseLeave);
            this.buttonEnter.MouseHover += new System.EventHandler(this.buttonEnter_MouseHover);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonCancel.Image")));
            this.buttonCancel.Location = new System.Drawing.Point(303, 296);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(83, 32);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "CANCEL";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            this.buttonCancel.MouseLeave += new System.EventHandler(this.buttonCancel_MouseLeave);
            this.buttonCancel.MouseHover += new System.EventHandler(this.buttonCancel_MouseHover);
            // 
            // version
            // 
            this.version.AutoSize = true;
            this.version.BackColor = System.Drawing.Color.Red;
            this.version.Cursor = System.Windows.Forms.Cursors.Default;
            this.version.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.version.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.version.Image = ((System.Drawing.Image)(resources.GetObject("version.Image")));
            this.version.Location = new System.Drawing.Point(298, 105);
            this.version.Name = "version";
            this.version.Size = new System.Drawing.Size(40, 13);
            this.version.TabIndex = 9;
            this.version.Text = "1.1.1.0";
            // 
            // ProgrameName
            // 
            this.ProgrameName.AutoSize = true;
            this.ProgrameName.BackColor = System.Drawing.Color.Red;
            this.ProgrameName.Cursor = System.Windows.Forms.Cursors.Default;
            this.ProgrameName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProgrameName.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ProgrameName.Image = ((System.Drawing.Image)(resources.GetObject("ProgrameName.Image")));
            this.ProgrameName.Location = new System.Drawing.Point(298, 47);
            this.ProgrameName.Name = "ProgrameName";
            this.ProgrameName.Size = new System.Drawing.Size(343, 25);
            this.ProgrameName.TabIndex = 8;
            this.ProgrameName.Text = "Dialog Mobile Customer Complaint";
            // 
            // LocationLabel
            // 
            this.LocationLabel.AutoSize = true;
            this.LocationLabel.BackColor = System.Drawing.Color.Red;
            this.LocationLabel.Cursor = System.Windows.Forms.Cursors.Default;
            this.LocationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LocationLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.LocationLabel.Image = ((System.Drawing.Image)(resources.GetObject("LocationLabel.Image")));
            this.LocationLabel.Location = new System.Drawing.Point(298, 79);
            this.LocationLabel.Name = "LocationLabel";
            this.LocationLabel.Size = new System.Drawing.Size(298, 16);
            this.LocationLabel.TabIndex = 7;
            this.LocationLabel.Text = "Access Network Planning, Western North Region";
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.BackColor = System.Drawing.SystemColors.Info;
            this.textBoxUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxUserName.ForeColor = System.Drawing.Color.DimGray;
            this.textBoxUserName.Location = new System.Drawing.Point(303, 156);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(383, 31);
            this.textBoxUserName.TabIndex = 10;
            this.textBoxUserName.Text = "USER NAME";
            this.textBoxUserName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxUserName.TextChanged += new System.EventHandler(this.textBoxUserName_TextChanged);
            this.textBoxUserName.MouseEnter += new System.EventHandler(this.textBoxUserName_MouseEnter);
            this.textBoxUserName.MouseLeave += new System.EventHandler(this.textBoxUserName_MouseLeave);
            // 
            // textBoxPassWord
            // 
            this.textBoxPassWord.BackColor = System.Drawing.SystemColors.Info;
            this.textBoxPassWord.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPassWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPassWord.ForeColor = System.Drawing.Color.DimGray;
            this.textBoxPassWord.Location = new System.Drawing.Point(303, 210);
            this.textBoxPassWord.Name = "textBoxPassWord";
            this.textBoxPassWord.PasswordChar = '*';
            this.textBoxPassWord.Size = new System.Drawing.Size(383, 31);
            this.textBoxPassWord.TabIndex = 11;
            this.textBoxPassWord.Text = "PASS WORD";
            this.textBoxPassWord.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxPassWord.MouseEnter += new System.EventHandler(this.textBoxPassWord_MouseEnter);
            this.textBoxPassWord.MouseLeave += new System.EventHandler(this.textBoxPassWord_MouseLeave);
            // 
            // labelProKEY
            // 
            this.labelProKEY.AutoSize = true;
            this.labelProKEY.BackColor = System.Drawing.Color.Red;
            this.labelProKEY.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelProKEY.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProKEY.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelProKEY.Image = ((System.Drawing.Image)(resources.GetObject("labelProKEY.Image")));
            this.labelProKEY.Location = new System.Drawing.Point(646, 352);
            this.labelProKEY.Name = "labelProKEY";
            this.labelProKEY.Size = new System.Drawing.Size(15, 12);
            this.labelProKEY.TabIndex = 12;
            this.labelProKEY.Text = "ID";
            // 
            // labelMessage
            // 
            this.labelMessage.AutoSize = true;
            this.labelMessage.BackColor = System.Drawing.Color.Red;
            this.labelMessage.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMessage.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelMessage.Image = ((System.Drawing.Image)(resources.GetObject("labelMessage.Image")));
            this.labelMessage.Location = new System.Drawing.Point(313, 250);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(52, 13);
            this.labelMessage.TabIndex = 13;
            this.labelMessage.Text = "MESAGE";
            this.labelMessage.Visible = false;
            // 
            // labelMessage2
            // 
            this.labelMessage2.AutoSize = true;
            this.labelMessage2.BackColor = System.Drawing.Color.Red;
            this.labelMessage2.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelMessage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMessage2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelMessage2.Image = ((System.Drawing.Image)(resources.GetObject("labelMessage2.Image")));
            this.labelMessage2.Location = new System.Drawing.Point(313, 268);
            this.labelMessage2.Name = "labelMessage2";
            this.labelMessage2.Size = new System.Drawing.Size(52, 13);
            this.labelMessage2.TabIndex = 14;
            this.labelMessage2.Text = "MESAGE";
            this.labelMessage2.Visible = false;
            // 
            // DialogLoginForm
            // 
            this.AcceptButton = this.buttonEnter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(750, 380);
            this.Controls.Add(this.labelMessage2);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.labelProKEY);
            this.Controls.Add(this.textBoxPassWord);
            this.Controls.Add(this.textBoxUserName);
            this.Controls.Add(this.version);
            this.Controls.Add(this.ProgrameName);
            this.Controls.Add(this.LocationLabel);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonEnter);
            this.Controls.Add(this.pictureBoxLoginForm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DialogLoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TransparencyKey = System.Drawing.Color.Gray;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DialogLoginForm_FormClosing);
            this.Load += new System.EventHandler(this.DialogLoginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoginForm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxLoginForm;
        private System.Windows.Forms.Button buttonEnter;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label version;
        private System.Windows.Forms.Label ProgrameName;
        private System.Windows.Forms.Label LocationLabel;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.TextBox textBoxPassWord;
        private System.Windows.Forms.Label labelProKEY;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.Label labelMessage2;
    }
}