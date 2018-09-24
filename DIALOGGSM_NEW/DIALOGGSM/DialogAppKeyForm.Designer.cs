namespace DIALOGGSM
{
    partial class DialogAppKeyForm
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.textBoxUserID = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxProKeyID = new System.Windows.Forms.TextBox();
            this.labelUserID = new System.Windows.Forms.Label();
            this.labelPass = new System.Windows.Forms.Label();
            this.labelKey = new System.Windows.Forms.Label();
            this.textBoxUserPass = new System.Windows.Forms.TextBox();
            this.labelMessage = new System.Windows.Forms.Label();
            this.labelMessage2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Enabled = false;
            this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOK.Location = new System.Drawing.Point(371, 141);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // textBoxUserID
            // 
            this.textBoxUserID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxUserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxUserID.Location = new System.Drawing.Point(106, 18);
            this.textBoxUserID.MaxLength = 15;
            this.textBoxUserID.Name = "textBoxUserID";
            this.textBoxUserID.Size = new System.Drawing.Size(340, 21);
            this.textBoxUserID.TabIndex = 0;
            this.textBoxUserID.TextChanged += new System.EventHandler(this.textBoxUserID_TextChanged);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Location = new System.Drawing.Point(106, 141);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textBoxProKeyID
            // 
            this.textBoxProKeyID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxProKeyID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxProKeyID.Location = new System.Drawing.Point(106, 70);
            this.textBoxProKeyID.MaxLength = 36;
            this.textBoxProKeyID.Name = "textBoxProKeyID";
            this.textBoxProKeyID.Size = new System.Drawing.Size(340, 21);
            this.textBoxProKeyID.TabIndex = 2;
            this.textBoxProKeyID.TextChanged += new System.EventHandler(this.textBoxProKeyID_TextChanged);
            // 
            // labelUserID
            // 
            this.labelUserID.AutoSize = true;
            this.labelUserID.Location = new System.Drawing.Point(55, 23);
            this.labelUserID.Name = "labelUserID";
            this.labelUserID.Size = new System.Drawing.Size(43, 13);
            this.labelUserID.TabIndex = 4;
            this.labelUserID.Text = "User ID";
            // 
            // labelPass
            // 
            this.labelPass.AutoSize = true;
            this.labelPass.Location = new System.Drawing.Point(39, 49);
            this.labelPass.Name = "labelPass";
            this.labelPass.Size = new System.Drawing.Size(59, 13);
            this.labelPass.TabIndex = 5;
            this.labelPass.Text = "Pass Word";
            // 
            // labelKey
            // 
            this.labelKey.AutoSize = true;
            this.labelKey.Location = new System.Drawing.Point(73, 75);
            this.labelKey.Name = "labelKey";
            this.labelKey.Size = new System.Drawing.Size(25, 13);
            this.labelKey.TabIndex = 6;
            this.labelKey.Text = "Key";
            // 
            // textBoxUserPass
            // 
            this.textBoxUserPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxUserPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxUserPass.Location = new System.Drawing.Point(106, 44);
            this.textBoxUserPass.MaxLength = 50;
            this.textBoxUserPass.Name = "textBoxUserPass";
            this.textBoxUserPass.PasswordChar = '*';
            this.textBoxUserPass.Size = new System.Drawing.Size(340, 21);
            this.textBoxUserPass.TabIndex = 1;
            // 
            // labelMessage
            // 
            this.labelMessage.AutoSize = true;
            this.labelMessage.Location = new System.Drawing.Point(106, 101);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(50, 13);
            this.labelMessage.TabIndex = 8;
            this.labelMessage.Text = "Message";
            this.labelMessage.Visible = false;
            // 
            // labelMessage2
            // 
            this.labelMessage2.AutoSize = true;
            this.labelMessage2.Location = new System.Drawing.Point(106, 119);
            this.labelMessage2.Name = "labelMessage2";
            this.labelMessage2.Size = new System.Drawing.Size(56, 13);
            this.labelMessage2.TabIndex = 9;
            this.labelMessage2.Text = "Message2";
            this.labelMessage2.Visible = false;
            // 
            // DialogAppKeyForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(504, 176);
            this.Controls.Add(this.labelMessage2);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.textBoxUserPass);
            this.Controls.Add(this.labelKey);
            this.Controls.Add(this.labelPass);
            this.Controls.Add(this.labelUserID);
            this.Controls.Add(this.textBoxProKeyID);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.textBoxUserID);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogAppKeyForm";
            this.Opacity = 0.9D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " Product Key";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DialogAppKeyForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.TextBox textBoxUserID;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxProKeyID;
        private System.Windows.Forms.Label labelUserID;
        private System.Windows.Forms.Label labelPass;
        private System.Windows.Forms.Label labelKey;
        private System.Windows.Forms.TextBox textBoxUserPass;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.Label labelMessage2;
    }
}