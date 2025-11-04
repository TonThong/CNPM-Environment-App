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
// Thêm using này để gọi ThemeManager
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

            // Đọc mã ngôn ngữ ("vi-VN" hoặc "en")
            string savedLanguageCode = Properties.Settings.Default.Language;
            if (savedLanguageCode == "en")
            {
                cmbNgonNgu.SelectedItem = "English";
            }
            else
            {
                cmbNgonNgu.SelectedItem = "Tiếng Việt";
            }

            // Lấy văn bản theme đã được dịch
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
            isLoading = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (isLoading) return;

            Mainlayout mainForm = this.ParentForm as Mainlayout;

            try
            {
                // 1. LƯU NGÔN NGỮ
                string selectedLanguageCode = "vi-VN";
                if (cmbNgonNgu.SelectedItem != null && cmbNgonNgu.SelectedItem.ToString() == "English")
                {
                    selectedLanguageCode = "en";
                }
                Properties.Settings.Default.Language = selectedLanguageCode;

                // 2. LƯU THEME (ĐÃ SỬA LỖI LOGIC)
                string themeToSave = "light";

                // Lấy chuỗi "Dark" ở cả 2 ngôn ngữ để so sánh
                CultureInfo newCulture = new CultureInfo(selectedLanguageCode);
                CultureInfo oldCulture = Thread.CurrentThread.CurrentUICulture;
                string darkThemeText_New = rm.GetString("Theme_Dark", newCulture);
                string darkThemeText_Old = rm.GetString("Theme_Dark", oldCulture);

                string selectedThemeString = cmbGiaoDien.SelectedItem.ToString();

                // Nếu chuỗi đang chọn khớp với "Dark" ở ngôn ngữ cũ HOẶC ngôn ngữ mới -> lưu "dark"
                if (cmbGiaoDien.SelectedItem != null &&
                   (selectedThemeString == darkThemeText_New || selectedThemeString == darkThemeText_Old))
                {
                    themeToSave = "dark";
                }
                Properties.Settings.Default.Theme = themeToSave;

                // 3. LƯU CÁC CÀI ĐẶT KHÁC
                Properties.Settings.Default.NotifyByEmail = cbEmail.Checked;
                Properties.Settings.Default.NotifyInApp = cbUngDung.Checked;

                Properties.Settings.Default.Save();

                // 4. ÁP DỤNG CÀI ĐẶT
                // Áp dụng ngôn ngữ mới cho thread hiện tại
                Thread.CurrentThread.CurrentCulture = newCulture;
                Thread.CurrentThread.CurrentUICulture = newCulture;

                // Áp dụng theme mới
                ThemeManager.ApplyTheme(themeToSave);

                // Yêu cầu Mainlayout cập nhật ngôn ngữ cho tất cả các trang
                mainForm?.UpdateAllChildLanguages();

                // Hiển thị thông báo thành công (bằng ngôn ngữ MỚI)
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
                // Tải lại cài đặt để combobox hiển thị đúng
                LoadSettings();

                // Dịch các label
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

                // Áp dụng theme
                this.BackColor = ThemeManager.BackgroundColor;
                roundedPanel2.BackColor = ThemeManager.PanelColor;
                roundedPanel3.BackColor = ThemeManager.PanelColor;

                lblBaoCao.ForeColor = ThemeManager.TextColor;
                lblUserSupport.ForeColor = ThemeManager.TextColor;
                lblSystemSettings.ForeColor = ThemeManager.TextColor;
                lblLanguage.ForeColor = ThemeManager.TextColor;
                lblTheme.ForeColor = ThemeManager.TextColor;
                lblNotification.ForeColor = ThemeManager.TextColor;
                cbEmail.ForeColor = ThemeManager.TextColor;
                cbUngDung.ForeColor = ThemeManager.TextColor;
                lblUserManual.ForeColor = ThemeManager.TextColor;
                lblViewDocument.ForeColor = ThemeManager.TextColor;
                lblQuestion.ForeColor = ThemeManager.TextColor;

                cmbNgonNgu.BackColor = ThemeManager.PanelColor;
                cmbNgonNgu.ForeColor = ThemeManager.TextColor;

                cmbGiaoDien.BackColor = ThemeManager.PanelColor;
                cmbGiaoDien.ForeColor = ThemeManager.TextColor;

                txtSearch.BackColor = ThemeManager.PanelColor;
                txtSearch.ForeColor = ThemeManager.TextColor;

                btnSave.BackColor = ThemeManager.AccentColor;
                btnSave.ForeColor = Color.White;
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
    }
}