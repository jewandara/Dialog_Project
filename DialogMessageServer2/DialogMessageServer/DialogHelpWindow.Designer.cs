namespace DialogMessageServer
{
    partial class DialogHelpWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogHelpWindow));
            this.buttonOK = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.HelpLabel = new System.Windows.Forms.Label();
            this.dialogTabTwo = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dialogTabOne = new System.Windows.Forms.TabPage();
            this.pictureBoxAdvertisement = new System.Windows.Forms.PictureBox();
            this.labelServerData = new System.Windows.Forms.Label();
            this.dialogTabApplication = new System.Windows.Forms.TabControl();
            this.panel1.SuspendLayout();
            this.dialogTabTwo.SuspendLayout();
            this.dialogTabOne.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAdvertisement)).BeginInit();
            this.dialogTabApplication.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(577, 427);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.HelpLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(684, 42);
            this.panel1.TabIndex = 44;
            // 
            // HelpLabel
            // 
            this.HelpLabel.AutoSize = true;
            this.HelpLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpLabel.Location = new System.Drawing.Point(68, 8);
            this.HelpLabel.Name = "HelpLabel";
            this.HelpLabel.Size = new System.Drawing.Size(136, 25);
            this.HelpLabel.TabIndex = 0;
            this.HelpLabel.Text = "Server Help";
            // 
            // dialogTabTwo
            // 
            this.dialogTabTwo.BackColor = System.Drawing.Color.White;
            this.dialogTabTwo.Controls.Add(this.textBox1);
            this.dialogTabTwo.Location = new System.Drawing.Point(4, 22);
            this.dialogTabTwo.Name = "dialogTabTwo";
            this.dialogTabTwo.Padding = new System.Windows.Forms.Padding(3);
            this.dialogTabTwo.Size = new System.Drawing.Size(676, 346);
            this.dialogTabTwo.TabIndex = 1;
            this.dialogTabTwo.Text = "Program";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(670, 340);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // dialogTabOne
            // 
            this.dialogTabOne.BackColor = System.Drawing.Color.White;
            this.dialogTabOne.Controls.Add(this.pictureBoxAdvertisement);
            this.dialogTabOne.Controls.Add(this.labelServerData);
            this.dialogTabOne.Location = new System.Drawing.Point(4, 22);
            this.dialogTabOne.Name = "dialogTabOne";
            this.dialogTabOne.Padding = new System.Windows.Forms.Padding(3);
            this.dialogTabOne.Size = new System.Drawing.Size(676, 346);
            this.dialogTabOne.TabIndex = 0;
            this.dialogTabOne.Text = "Server";
            // 
            // pictureBoxAdvertisement
            // 
            this.pictureBoxAdvertisement.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxAdvertisement.Image")));
            this.pictureBoxAdvertisement.Location = new System.Drawing.Point(526, 238);
            this.pictureBoxAdvertisement.Name = "pictureBoxAdvertisement";
            this.pictureBoxAdvertisement.Size = new System.Drawing.Size(122, 84);
            this.pictureBoxAdvertisement.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxAdvertisement.TabIndex = 1;
            this.pictureBoxAdvertisement.TabStop = false;
            // 
            // labelServerData
            // 
            this.labelServerData.AutoSize = true;
            this.labelServerData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelServerData.Location = new System.Drawing.Point(30, 18);
            this.labelServerData.Name = "labelServerData";
            this.labelServerData.Size = new System.Drawing.Size(382, 304);
            this.labelServerData.TabIndex = 0;
            this.labelServerData.Text = resources.GetString("labelServerData.Text");
            // 
            // dialogTabApplication
            // 
            this.dialogTabApplication.Controls.Add(this.dialogTabOne);
            this.dialogTabApplication.Controls.Add(this.dialogTabTwo);
            this.dialogTabApplication.Dock = System.Windows.Forms.DockStyle.Top;
            this.dialogTabApplication.Location = new System.Drawing.Point(0, 42);
            this.dialogTabApplication.Name = "dialogTabApplication";
            this.dialogTabApplication.SelectedIndex = 0;
            this.dialogTabApplication.Size = new System.Drawing.Size(684, 372);
            this.dialogTabApplication.TabIndex = 2;
            this.dialogTabApplication.SelectedIndexChanged += new System.EventHandler(this.dialogTabApplication_SelectedIndexChanged);
            // 
            // DialogHelpWindow
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(684, 462);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.dialogTabApplication);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(700, 500);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "DialogHelpWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dialog Message Server Help";
            this.Load += new System.EventHandler(this.DialogHelpWindow_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.dialogTabTwo.ResumeLayout(false);
            this.dialogTabTwo.PerformLayout();
            this.dialogTabOne.ResumeLayout(false);
            this.dialogTabOne.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAdvertisement)).EndInit();
            this.dialogTabApplication.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabPage dialogTabTwo;
        private System.Windows.Forms.TabPage dialogTabOne;
        private System.Windows.Forms.TabControl dialogTabApplication;
        private System.Windows.Forms.Label HelpLabel;
        private System.Windows.Forms.Label labelServerData;
        private System.Windows.Forms.PictureBox pictureBoxAdvertisement;
        private System.Windows.Forms.TextBox textBox1;
    }
}