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
using Environmental_Monitoring.Controller;

namespace Environmental_Monitoring
{
    public partial class Setting : UserControl
    {
        private ResourceManager rm;
        private bool isLoading = false;

        public Setting()
        {
            InitializeComponent();
            InitializeLocalization();
            this.Load += new System.EventHandler(this.Setting_Load);

            if (this.btnRegisterFace != null)
            {
                this.btnRegisterFace.Click += new System.EventHandler(this.btnRegisterFace_Click);
            }
        }

        private void InitializeLocalization()
        {
            rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(Setting).Assembly);
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            UpdateUIText();
        }

        private void LoadSettings()
        {
            isLoading = true;
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;

            cmbNgonNgu.Items.Clear();
            cmbNgonNgu.Items.Add("Tiếng Việt");
            cmbNgonNgu.Items.Add("English");

            string savedLanguageCode = Properties.Settings.Default.Language;
            if (savedLanguageCode == "en")
            {
                cmbNgonNgu.SelectedItem = "English";
            }
            else
            {
                cmbNgonNgu.SelectedItem = "Tiếng Việt";
            }

            string lightThemeText = rm.GetString("Theme_Light", culture);
            string darkThemeText = rm.GetString("Theme_Dark", culture);

            cmbGiaoDien.Items.Clear();
            cmbGiaoDien.Items.Add(lightThemeText);
            cmbGiaoDien.Items.Add(darkThemeText);

            string savedTheme = Properties.Settings.Default.Theme;

            if (savedTheme == "dark")
            {
                cmbGiaoDien.SelectedIndex = 1;
            }
            else
            {
                cmbGiaoDien.SelectedIndex = 0;
            }

            isLoading = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (isLoading) return;

            Mainlayout mainForm = this.ParentForm as Mainlayout;

            try
            {
                string selectedLanguageCode = "vi-VN";
                if (cmbNgonNgu.SelectedItem != null && cmbNgonNgu.SelectedItem.ToString() == "English")
                {
                    selectedLanguageCode = "en";
                }
                Properties.Settings.Default.Language = selectedLanguageCode;

                string themeToSave = "light";

                if (cmbGiaoDien.SelectedIndex == 1)
                {
                    themeToSave = "dark";
                }
                Properties.Settings.Default.Theme = themeToSave;


                Properties.Settings.Default.Save();

                CultureInfo newCulture = new CultureInfo(selectedLanguageCode);
                Thread.CurrentThread.CurrentCulture = newCulture;
                Thread.CurrentThread.CurrentUICulture = newCulture;

                ThemeManager.ApplyTheme(themeToSave);

                mainForm?.UpdateAllChildLanguages();

                string successMessage = rm.GetString("Alert_SaveSuccess", newCulture);
                mainForm?.ShowGlobalAlert(successMessage, AlertPanel.AlertType.Success);
            }
            catch (Exception ex)
            {
                string errorMessage = rm.GetString("Alert_SaveError", Thread.CurrentThread.CurrentUICulture);
                mainForm?.ShowGlobalAlert(errorMessage + ex.Message, AlertPanel.AlertType.Error);
            }
        }

        public void UpdateUIText()
        {
            if (isLoading) return;

            isLoading = true;
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;

            try
            {
                LoadSettings();

                lblBaoCao.Text = rm.GetString("Settings_Title", culture);
                lblUserSupport.Text = rm.GetString("Settings_UserSupport", culture);
                lblSystemSettings.Text = rm.GetString("Settings_SystemSettings", culture);
                lblLanguage.Text = rm.GetString("Settings_Language", culture);
                lblTheme.Text = rm.GetString("Settings_Theme", culture);
                btnSave.Text = rm.GetString("Settings_SaveButton", culture);
                lblUserManual.Text = rm.GetString("Settings_UserManual", culture);
                lblViewDocument.Text = rm.GetString("Settings_ViewDocument", culture);
                txtSearch.PlaceholderText = rm.GetString("Search_Placeholder", culture);

                this.BackColor = ThemeManager.BackgroundColor;
                roundedPanel2.BackColor = ThemeManager.PanelColor;
                roundedPanel3.BackColor = ThemeManager.PanelColor;

                lblBaoCao.ForeColor = ThemeManager.TextColor;
                lblUserSupport.ForeColor = ThemeManager.TextColor;
                lblSystemSettings.ForeColor = ThemeManager.TextColor;
                lblLanguage.ForeColor = ThemeManager.TextColor;
                lblTheme.ForeColor = ThemeManager.TextColor;
                lblUserManual.ForeColor = ThemeManager.TextColor;
                lblViewDocument.ForeColor = ThemeManager.TextColor;

                cmbNgonNgu.BackColor = ThemeManager.PanelColor;
                cmbNgonNgu.ForeColor = ThemeManager.TextColor;

                cmbGiaoDien.BackColor = ThemeManager.PanelColor;
                cmbGiaoDien.ForeColor = ThemeManager.TextColor;

                txtSearch.BackColor = ThemeManager.PanelColor;
                txtSearch.ForeColor = ThemeManager.TextColor;

                btnSave.BackColor = ThemeManager.AccentColor;
                btnSave.ForeColor = Color.White;

                if (lblFaceID != null && btnRegisterFace != null)
                {
                    lblFaceID.Text = rm.GetString("Settings_FaceID", culture);
                    btnRegisterFace.Text = rm.GetString("Settings_FaceID_Button", culture);

                    lblFaceID.ForeColor = ThemeManager.TextColor;
                    btnRegisterFace.BackColor = ThemeManager.SecondaryPanelColor;
                    btnRegisterFace.ForeColor = ThemeManager.TextColor;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = rm.GetString("Alert_LoadLanguageError", culture);
                Mainlayout mainForm = this.ParentForm as Mainlayout;
                mainForm?.ShowGlobalAlert(errorMessage + ex.Message, AlertPanel.AlertType.Error);
            }
            finally
            {
                isLoading = false;
            }
        }

        private void lblViewDocument_Click(object sender, EventArgs e) { }

        private void btnRegisterFace_Click(object sender, EventArgs e)
        {
            Mainlayout mainForm = this.ParentForm as Mainlayout;
            if (mainForm != null) mainForm.Hide();

            using (FaceIDRegistrationForm registerForm = new FaceIDRegistrationForm())
            {
                registerForm.ShowDialog();
            }

            if (mainForm != null) mainForm.Show();
        }

        private void lblTheme_Click(object sender, EventArgs e)
        {

        }
    }
}