﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using System.Resources;
using Environmental_Monitoring.View.Components;
using Environmental_Monitoring.View; 

namespace Environmental_Monitoring.View
{
    public partial class Introduce : UserControl
    {
        public Introduce()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.Introduce_Load);
        }

        private void Introduce_Load(object sender, EventArgs e)
        {
            string savedLanguage = Properties.Settings.Default.Language;
            string cultureName = "vi";
            if (savedLanguage == "English")
            {
                cultureName = "en";
            }
            CultureInfo culture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            UpdateUIText();
        }

        public void UpdateUIText()
        {
            ResourceManager rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(Introduce).Assembly);
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;
            Mainlayout mainForm = this.ParentForm as Mainlayout;

            try
            {

                lblTitle.Text = rm.GetString("Introduce_Title", culture);
                lblAppVersion.Text = rm.GetString("Introduce_AppVersion", culture);

                lblPrivacyPolicy.Text = rm.GetString("Introduce_PrivacyPolicy", culture);
             

                lblNotice.Text = rm.GetString("Introduce_Notice", culture);
                lblNoticeText.Text = rm.GetString("Introduce_Notice_Text", culture);

                lblContact.Text = rm.GetString("Introduce_Contact", culture);
            }
            catch (Exception ex)
            {
                string errorMsg = rm.GetString("Alert_LoadLanguageError", culture);
                mainForm?.ShowGlobalAlert(errorMsg + ex.Message, AlertPanel.AlertType.Error);
            }
        }


        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}