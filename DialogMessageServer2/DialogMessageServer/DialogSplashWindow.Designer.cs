namespace DialogMessageServer
{
    partial class DialogSplashWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogSplashWindow));
            this.CopyrightLabel = new System.Windows.Forms.Label();
            this.LocationLabel = new System.Windows.Forms.Label();
            this.ProgrameName = new System.Windows.Forms.Label();
            this.version = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CopyrightLabel
            // 
            this.CopyrightLabel.AutoSize = true;
            this.CopyrightLabel.BackColor = System.Drawing.Color.Red;
            this.CopyrightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CopyrightLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.CopyrightLabel.Image = ((System.Drawing.Image)(resources.GetObject("CopyrightLabel.Image")));
            this.CopyrightLabel.Location = new System.Drawing.Point(508, 379);
            this.CopyrightLabel.Name = "CopyrightLabel";
            this.CopyrightLabel.Size = new System.Drawing.Size(130, 12);
            this.CopyrightLabel.TabIndex = 0;
            this.CopyrightLabel.Text = "Copyright All Rights Reserved";
            this.CopyrightLabel.UseWaitCursor = true;
            // 
            // LocationLabel
            // 
            this.LocationLabel.AutoSize = true;
            this.LocationLabel.BackColor = System.Drawing.Color.Red;
            this.LocationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LocationLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.LocationLabel.Image = ((System.Drawing.Image)(resources.GetObject("LocationLabel.Image")));
            this.LocationLabel.Location = new System.Drawing.Point(58, 313);
            this.LocationLabel.Name = "LocationLabel";
            this.LocationLabel.Size = new System.Drawing.Size(354, 20);
            this.LocationLabel.TabIndex = 1;
            this.LocationLabel.Text = "Access Network Planning, Western North Region";
            this.LocationLabel.UseWaitCursor = true;
            // 
            // ProgrameName
            // 
            this.ProgrameName.AutoSize = true;
            this.ProgrameName.BackColor = System.Drawing.Color.Red;
            this.ProgrameName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProgrameName.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ProgrameName.Image = ((System.Drawing.Image)(resources.GetObject("ProgrameName.Image")));
            this.ProgrameName.Location = new System.Drawing.Point(58, 338);
            this.ProgrameName.Name = "ProgrameName";
            this.ProgrameName.Size = new System.Drawing.Size(118, 13);
            this.ProgrameName.TabIndex = 2;
            this.ProgrameName.Text = "Dialog Customer Server";
            this.ProgrameName.UseWaitCursor = true;
            // 
            // version
            // 
            this.version.AutoSize = true;
            this.version.BackColor = System.Drawing.Color.Red;
            this.version.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.version.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.version.Image = ((System.Drawing.Image)(resources.GetObject("version.Image")));
            this.version.Location = new System.Drawing.Point(58, 357);
            this.version.Name = "version";
            this.version.Size = new System.Drawing.Size(40, 13);
            this.version.TabIndex = 3;
            this.version.Text = "1.1.1.0";
            this.version.UseWaitCursor = true;
            // 
            // DialogSplashWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(650, 400);
            this.Controls.Add(this.version);
            this.Controls.Add(this.ProgrameName);
            this.Controls.Add(this.LocationLabel);
            this.Controls.Add(this.CopyrightLabel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(650, 400);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(650, 400);
            this.Name = "DialogSplashWindow";
            this.Opacity = 0D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TransparencyKey = System.Drawing.SystemColors.ControlDark;
            this.UseWaitCursor = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DialogSplashWindow_FormClosing);
            this.Load += new System.EventHandler(this.DialogSplashWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label CopyrightLabel;
        private System.Windows.Forms.Label LocationLabel;
        private System.Windows.Forms.Label ProgrameName;
        private System.Windows.Forms.Label version;
    }
}