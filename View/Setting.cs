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
            // (Code này đã đúng)
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
            // (Code này đã đúng)
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
            // (Code này đã đúng)
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

                string themeToSave = "light";
                string darkThemeText = rm.GetString("Theme_Dark", culture);
                if (cmbGiaoDien.SelectedItem != null && cmbGiaoDien.SelectedItem.ToString() == darkThemeText)
                {
                    themeToSave = "dark";
                }
                Properties.Settings.Default.Theme = themeToSave;

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

                ThemeManager.ApplyTheme(themeToSave);

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

        // --- ĐÃ CẬP NHẬT (Hoàn thiện logic áp dụng Giao diện) ---
        public void UpdateUIText()
        {
            ResourceManager rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(Setting).Assembly);
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;

            try
            {
                // 1. TẢI COMBOBOX (Code này đã đúng)
                LoadSettings();

                // 2. TẢI NGÔN NGỮ (Code này đã đúng)
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
                txtSearch.PlaceholderText = rm.GetString("Search_Placeholder", culture);

                // --- 3. ÁP DỤNG MÀU GIAO DIỆN (Đã hoàn thiện) ---

                // Áp dụng màu nền chính của UserControl (hòa vào nền MainLayout)
                this.BackColor = ThemeManager.BackgroundColor;

                // (Giả sử tên 2 panel "card" của bạn là panelUserSupport và panelSystemSettings)
                // (Nếu tên khác, bạn hãy sửa lại 2 dòng dưới)
                roundedPanel2.BackColor = ThemeManager.PanelColor;
                roundedPanel3.BackColor = ThemeManager.PanelColor;

                // Áp dụng màu chữ (cho tất cả Label và CheckBox)
                lblBaoCao.ForeColor = ThemeManager.TextColor;
                lblUserSupport.ForeColor = ThemeManager.TextColor;
                lblSystemSettings.ForeColor = ThemeManager.TextColor;
                lblLanguage.ForeColor = ThemeManager.TextColor;
                lblTheme.ForeColor = ThemeManager.TextColor;
                lblNotification.ForeColor = ThemeManager.TextColor;
                cbEmail.ForeColor = ThemeManager.TextColor;
                cbUngDung.ForeColor = ThemeManager.TextColor;
                lblUserManual.ForeColor = ThemeManager.TextColor;

                // (Giả sử 2 control này là LinkLabel nên sẽ có màu riêng,
                // nhưng nếu là Label, chúng cũng sẽ được đổi màu)
                lblViewDocument.ForeColor = ThemeManager.TextColor;
                lblQuestion.ForeColor = ThemeManager.TextColor;

                // Áp dụng màu cho ComboBox
                cmbNgonNgu.BackColor = ThemeManager.PanelColor; // Màu nền của ô
                cmbNgonNgu.ForeColor = ThemeManager.TextColor; // Màu chữ
                cmbGiaoDien.BackColor = ThemeManager.PanelColor;
                cmbGiaoDien.ForeColor = ThemeManager.TextColor;

                // Áp dụng màu cho TextBox
                txtSearch.BackColor = ThemeManager.PanelColor;
                txtSearch.ForeColor = ThemeManager.TextColor;

                // Áp dụng màu cho Nút bấm
                btnSave.BackColor = ThemeManager.AccentColor;
                btnSave.ForeColor = Color.White;
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

        private void lblViewDocument_Click(object sender, EventArgs e)
        {

        }
    }
}