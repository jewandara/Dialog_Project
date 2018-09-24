namespace DIALOGGSM
{
    partial class Form1
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
            this.backgroundWorkerSiteCreate = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerSectorCreate = new System.ComponentModel.BackgroundWorker();
            this.button1 = new System.Windows.Forms.Button();
            this.progressBarSite = new System.Windows.Forms.ProgressBar();
            this.progressBarSector = new System.Windows.Forms.ProgressBar();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelcom = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelProgressSiteBar = new System.Windows.Forms.Label();
            this.labelProgressSectorBar = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // backgroundWorkerSiteCreate
            // 
            this.backgroundWorkerSiteCreate.WorkerReportsProgress = true;
            this.backgroundWorkerSiteCreate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerSiteCreate_DoWork);
            this.backgroundWorkerSiteCreate.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerSiteCreate_ProgressChanged);
            this.backgroundWorkerSiteCreate.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerSiteCreate_RunWorkerCompleted);
            // 
            // backgroundWorkerSectorCreate
            // 
            this.backgroundWorkerSectorCreate.WorkerReportsProgress = true;
            this.backgroundWorkerSectorCreate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerSectorCreate_DoWork);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(56, 222);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // progressBarSite
            // 
            this.progressBarSite.Location = new System.Drawing.Point(23, 65);
            this.progressBarSite.Name = "progressBarSite";
            this.progressBarSite.Size = new System.Drawing.Size(660, 23);
            this.progressBarSite.TabIndex = 1;
            // 
            // progressBarSector
            // 
            this.progressBarSector.Location = new System.Drawing.Point(23, 117);
            this.progressBarSector.Name = "progressBarSector";
            this.progressBarSector.Size = new System.Drawing.Size(660, 23);
            this.progressBarSector.TabIndex = 2;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(435, 262);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelcom
            // 
            this.labelcom.AutoSize = true;
            this.labelcom.Location = new System.Drawing.Point(356, 310);
            this.labelcom.Name = "labelcom";
            this.labelcom.Size = new System.Drawing.Size(35, 13);
            this.labelcom.TabIndex = 4;
            this.labelcom.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(279, 351);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "label1";
            // 
            // labelProgressSiteBar
            // 
            this.labelProgressSiteBar.AutoSize = true;
            this.labelProgressSiteBar.Location = new System.Drawing.Point(31, 143);
            this.labelProgressSiteBar.Name = "labelProgressSiteBar";
            this.labelProgressSiteBar.Size = new System.Drawing.Size(68, 13);
            this.labelProgressSiteBar.TabIndex = 6;
            this.labelProgressSiteBar.Text = "Processing...";
            this.labelProgressSiteBar.Visible = false;
            // 
            // labelProgressSectorBar
            // 
            this.labelProgressSectorBar.AutoSize = true;
            this.labelProgressSectorBar.Location = new System.Drawing.Point(20, 91);
            this.labelProgressSectorBar.Name = "labelProgressSectorBar";
            this.labelProgressSectorBar.Size = new System.Drawing.Size(68, 13);
            this.labelProgressSectorBar.TabIndex = 8;
            this.labelProgressSectorBar.Text = "Processing...";
            this.labelProgressSectorBar.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(503, 382);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(608, 382);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 10;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(503, 350);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(633, 350);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "label3";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(503, 467);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 13;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(503, 451);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "label4";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 514);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.labelProgressSectorBar);
            this.Controls.Add(this.labelProgressSiteBar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelcom);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.progressBarSector);
            this.Controls.Add(this.progressBarSite);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorkerSiteCreate;
        private System.ComponentModel.BackgroundWorker backgroundWorkerSectorCreate;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar progressBarSite;
        private System.Windows.Forms.ProgressBar progressBarSector;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelcom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelProgressSiteBar;
        private System.Windows.Forms.Label labelProgressSectorBar;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label4;

    }
}