using System;
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
using Environmental_Monitoring.View;
using Environmental_Monitoring.View.Components;

namespace Environmental_Monitoring
{
    public partial class Setting : UserControl
    {
        public Setting()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.Setting_Load);
        }

        private void Setting_Load(object sender, EventArgs e)
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
            LoadSettings();
            UpdateUIText();
        }

        private void LoadSettings()
        {
            ResourceManager rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(Setting).Assembly);
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;
            cmbNgonNgu.Items.Clear();
            cmbNgonNgu.Items.Add("Tiếng Việt");
            cmbNgonNgu.Items.Add("English");
            cmbNgonNgu.SelectedItem = Properties.Settings.Default.Language;
            string lightThemeText = rm.GetString("Theme_Light", culture);
            string darkThemeText = rm.GetString("Theme_Dark", culture);
            cmbGiaoDien.Items.Clear();
            cmbGiaoDien.Items.Add(lightThemeText);
            cmbGiaoDien.Items.Add(darkThemeText);
            string savedTheme = Properties.Settings.Default.Theme;
            if (savedTheme == "dark")
            {
                cmbGiaoDien.SelectedItem = darkThemeText;
            }
            else
            {
                cmbGiaoDien.SelectedItem = lightThemeText;
            }
            cbEmail.Checked = Properties.Settings.Default.NotifyByEmail;
            cbUngDung.Checked = Properties.Settings.Default.NotifyInApp;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Mainlayout mainForm = this.ParentForm as Mainlayout;

            ResourceManager rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(Setting).Assembly);
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture; 

            try
            {
                if (cmbNgonNgu.SelectedItem != null)
                {
                    Properties.Settings.Default.Language = cmbNgonNgu.SelectedItem.ToString();
                }
                else
                {
                    Properties.Settings.Default.Language = "Tiếng Việt";
                }
                string darkThemeText = rm.GetString("Theme_Dark", culture);
                if (cmbGiaoDien.SelectedItem != null && cmbGiaoDien.SelectedItem.ToString() == darkThemeText)
                {
                    Properties.Settings.Default.Theme = "dark";
                }
                else
                {
                    Properties.Settings.Default.Theme = "light";
                }
                Properties.Settings.Default.NotifyByEmail = cbEmail.Checked;
                Properties.Settings.Default.NotifyInApp = cbUngDung.Checked;

                Properties.Settings.Default.Save();

                string successMessage = rm.GetString("Alert_SaveSuccess", culture);
                if (mainForm != null)
                {
                    mainForm.ShowGlobalAlert(successMessage, AlertPanel.AlertType.Success);
                }

                string selectedLanguage = Properties.Settings.Default.Language;
                string cultureName = "vi";
                if (selectedLanguage == "English")
                {
                    cultureName = "en";
                }
                CultureInfo newCulture = new CultureInfo(cultureName);
                Thread.CurrentThread.CurrentCulture = newCulture;
                Thread.CurrentThread.CurrentUICulture = newCulture;

                culture = newCulture;

                if (mainForm != null)
                {
                    mainForm.UpdateAllChildLanguages();
                }
                else
                {
                    this.UpdateUIText();
                }

            }
            catch (Exception ex)
            {
                string errorMessage = rm.GetString("Alert_SaveError", culture);
                if (mainForm != null)
                {
                    mainForm.ShowGlobalAlert(errorMessage + ex.Message, AlertPanel.AlertType.Error);
                }
            }
        }

        public void UpdateUIText()
        {
            ResourceManager rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(Setting).Assembly);
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;

            try
            {
                LoadSettings();

                lblBaoCao.Text = rm.GetString("Settings_Title", culture);
                lblUserSupport.Text = rm.GetString("Settings_UserSupport", culture);
                lblSystemSettings.Text = rm.GetString("Settings_SystemSettings", culture);
                lblLanguage.Text = rm.GetString("Settings_Language", culture);
                lblTheme.Text = rm.GetString("Settings_Theme", culture);
                lblNotification.Text = rm.GetString("Settings_Notification", culture);
                cbEmail.Text = rm.GetString("Email", culture);
                cbUngDung.Text = rm.GetString("Settings_App", culture);
                btnSave.Text = rm.GetString("Settings_SaveButton", culture);
                lblUserManual.Text = rm.GetString("Settings_UserManual", culture);
                lblViewDocument.Text = rm.GetString("Settings_ViewDocument", culture);
                lblQuestion.Text = rm.GetString("Settings_Question", culture);
                txtSearch.Text = rm.GetString("Search_Placeholder", culture);
            }
            catch (Exception ex)
            {
                string errorMessage = rm.GetString("Alert_LoadLanguageError", culture);
                Mainlayout mainForm = this.ParentForm as Mainlayout;
                if (mainForm != null)
                {
                    mainForm.ShowGlobalAlert(errorMessage + ex.Message, AlertPanel.AlertType.Error);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }
    }
}