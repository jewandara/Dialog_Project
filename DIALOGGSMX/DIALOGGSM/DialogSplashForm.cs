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

        public DialogSplashForm()
        {
            InitializeComponent();
            SetAndStartTimer();
        }

        #region EVENTS

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


        //private void formFadeIn(int duration, double startSteps, double endStep)
        //{
        //    Timer timer = new Timer();
        //    timer.Interval = 1000 / duration;
        //    timer.Tick += (arg1, arg2) =>
        //    {
        //        Opacity = startSteps;
        //        startSteps = startSteps + 0.01;
        //        if (startSteps >= endStep)
        //        {
        //            timer.Stop();
        //            timer.Dispose();
        //        }
        //    };
        //    timer.Start();
        //}

        //private void formFadeOut(int duration, double startSteps, double endStep)
        //{
        //    Timer timer = new Timer();
        //    timer.Interval = 1000 / duration;
        //    timer.Tick += (arg1, arg2) =>
        //    {
        //        Opacity = startSteps;
        //        startSteps = startSteps - 0.01;
        //        if (startSteps <= endStep)
        //        {
        //            timer.Stop();
        //            timer.Dispose();
        //        }
        //    };
        //    timer.Start();
        //}

    }
}
