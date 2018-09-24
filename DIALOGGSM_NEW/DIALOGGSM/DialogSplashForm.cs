using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DIALOGGSM
{
    public partial class DialogSplashForm : Form
    {

        #region FIELDS
            Timer timer = new Timer();
            bool fadeIn = true;
        #endregion


        #region EVENTS

        public DialogSplashForm()
        {
            InitializeComponent();
            SetAndStartTimer();
        }

        private void SetAndStartTimer()
        {
            timer.Interval = 100;
            timer.Tick += new EventHandler(t_Tick);
            timer.Start();
        }

        void t_Tick(object sender, EventArgs e)
        {
            if (fadeIn)
            {
                if (this.Opacity < 1.0)
                {
                    this.Opacity += 0.05;
                    this.Refresh();
                }
                else
                {
                    fadeIn = false;
                }
            }
            if (!(fadeIn))
            {
                timer.Stop();
                this.Close();
            }
        }

        #endregion


    }
}
