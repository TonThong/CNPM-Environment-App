using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Environmental_Monitoring.View.Components;
using Environmental_Monitoring.View;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BCrypt.Net;
using Environmental_Monitoring.Controller.Data;
using Environmental_Monitoring.Controller;
using Environmental_Monitoring.Model;
using System.Resources;
using System.Globalization; // Đảm bảo using này
using System.Threading;     // Đảm bảo using này

namespace Environmental_Monitoring.View
{
    public partial class Login : Form
    {
        private ResourceManager rm;

        public Login()
        {
            // LoadSavedLanguage(); // <-- Đã xóa, vì Program.cs đã làm việc này
            InitializeComponent();
            InitializeLocalization();

            panelLanguage.Visible = false;

            txtMatKhau.UseSystemPasswordChar = true;
            picShowPass.Image = Properties.Resources.eyeclose;
            picShowPass.Parent = txtMatKhau;
            picShowPass.BackColor = txtMatKhau.BackColor;
            picShowPass.Location = new Point(txtMatKhau.Width - picShowPass.Width - 8, (txtMatKhau.Height - picShowPass.Height) / 2);
            picShowPass.BringToFront();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            // Thêm dòng này để tải ngôn ngữ khi form load
            // (Vì LoadSavedLanguage() đã bị xóa khỏi constructor)
            LoadSavedLanguage();
            UpdateUIText();
        }

        #region Language Selection Logic

        // Hàm này vẫn cần thiết để UpdateUIText() chạy đúng khi form load
        private void LoadSavedLanguage()
        {
            string savedLanguage = Properties.Settings.Default.Language;
            if (string.IsNullOrEmpty(savedLanguage))
            {
                savedLanguage = "vi-VN";
            }

            try
            {
                CultureInfo culture = new CultureInfo(savedLanguage);
                Thread.CurrentThread.CurrentUICulture = culture;
                Thread.CurrentThread.CurrentCulture = culture;
            }
            catch (Exception)
            {
                CultureInfo culture = new CultureInfo("vi-VN");
                Thread.CurrentThread.CurrentUICulture = culture;
                Thread.CurrentThread.CurrentCulture = culture;
            }
        }

        private void InitializeLocalization()
        {
            rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(Login).Assembly);
        }

        private void SetLanguage(string cultureName)
        {
            CultureInfo newCulture = new CultureInfo(cultureName);

            Thread.CurrentThread.CurrentUICulture = newCulture;
            Thread.CurrentThread.CurrentCulture = newCulture;

            Properties.Settings.Default.Language = newCulture.Name;
            Properties.Settings.Default.Save();

            UpdateUIText();
        }

        public void UpdateUIText()
        {
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;

            this.Text = rm.GetString("Login_Title", culture);
            lbTaiKhoan.Text = rm.GetString("Login_Username", culture);
            txtTaiKhoan.PlaceholderText = rm.GetString("Login_Username", culture);
            txtMatKhau.PlaceholderText = rm.GetString("Login_Password", culture);
            linkForgot.Text = rm.GetString("Login_ForgotPassword", culture);
            btnDangNhap.Text = rm.GetString("Login_LoginButton", culture);
            lbDangNhapFaceID.Text = rm.GetString("Login_FaceID", culture);

            if (culture.Name == "vi-VN")
            {
                pbFlag.Image = Properties.Resources.flag_vi;
            }
            else
            {
                pbFlag.Image = Properties.Resources.flag_en;
            }
        }

        private void pbFlag_Click(object sender, EventArgs e)
        {
            panelLanguage.BringToFront();
            panelLanguage.Visible = !panelLanguage.Visible;
        }

        private void btnVietnamese_Click(object sender, EventArgs e)
        {
            SetLanguage("vi-VN");
            panelLanguage.Visible = false;
        }

        private void btnEnglish_Click(object sender, EventArgs e)
        {
            SetLanguage("en");
            panelLanguage.Visible = false;
        }

        #endregion

        #region Login & Form Logic

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string taiKhoan = txtTaiKhoan.Text.Trim();
            string matKhau = txtMatKhau.Text;

            if (string.IsNullOrEmpty(taiKhoan) || string.IsNullOrEmpty(matKhau))
            {
                alertPanel.ShowAlert("Vui lòng nhập đầy đủ tài khoản và mật khẩu.", AlertPanel.AlertType.Error);
                return;
            }

            try
            {
                Model.Employee user = EmployeeRepo.Instance.Login(taiKhoan);

                if (user != null && BCrypt.Net.BCrypt.Verify(matKhau, user.PasswordHash))
                {
                    UserSession.StartSession(user);

                    alertPanel.OnClose += AlertPanel_Success_OnClose;
                    alertPanel.ShowAlert("Đăng nhập thành công!", AlertPanel.AlertType.Success);

                    this.Enabled = false;
                }
                else
                {
                    alertPanel.ShowAlert("Tài khoản hoặc mật khẩu không chính xác. Vui lòng thử lại.", AlertPanel.AlertType.Error);
                }
            }
            catch (Exception ex)
            {
                alertPanel.ShowAlert("Lỗi kết nối dữ liệu hệ thống: " + ex.Message, AlertPanel.AlertType.Error);
            }
        }

        private void AlertPanel_Success_OnClose(object sender, EventArgs e)
        {
            alertPanel.OnClose -= AlertPanel_Success_OnClose;

            string savedLanguage = Properties.Settings.Default.Language;
            if (string.IsNullOrEmpty(savedLanguage))
            {
                savedLanguage = "vi-VN"; 
            }

            try
            {
                CultureInfo culture = new CultureInfo(savedLanguage);
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
            catch (Exception)
            {
                CultureInfo culture = new CultureInfo("vi-VN");
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
            Mainlayout mainForm = new Mainlayout();
            mainForm.Show();
            this.Hide();
        }

        private void picShowPass_Click(object sender, EventArgs e)
        {
            if (txtMatKhau.UseSystemPasswordChar == true)
            {
                txtMatKhau.UseSystemPasswordChar = false;
                picShowPass.Image = Properties.Resources.eye;
            }
            else
            {
                txtMatKhau.UseSystemPasswordChar = true;
                picShowPass.Image = Properties.Resources.eyeclose;
            }
        }

        private void linkForgot_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ForgotPasswordForm forgotForm = new ForgotPasswordForm(this.alertPanel);
            forgotForm.ShowDialog();
        }

        #endregion
    }
}