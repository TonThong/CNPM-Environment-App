using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BCrypt.Net;
using Environmental_Monitoring.View.Components;
using Environmental_Monitoring.Controller;
using Environmental_Monitoring.Controller.Data;
using System.Resources;
using System.Globalization;
using System.Threading;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Twilio.Rest.Verify.V2.Service;

namespace Environmental_Monitoring.View
{
    public partial class ForgotPasswordForm : Form
    {
        private string accountSid = "AC606500383bb02bb71bf5ecacdb6345c1";
        private string authToken = "0827bd67f891e76dad022dae2aa28cf3";
        private string verifyServiceSid = "VA960fe9cf0c028e40730898d8a2af5365";

        private AlertPanel loginAlertPanel;
        private ResourceManager rm;
        private string _currentPhoneNumber;

        public ForgotPasswordForm(AlertPanel alertPanelFromLogin)
        {
            InitializeComponent();
            this.loginAlertPanel = alertPanelFromLogin;
            InitializeLocalization();

            picShowPassNew.Image = Properties.Resources.eyeclose;
            picShowPassNew.BackColor = txtMatKhauMoi.BackColor;
            picShowPassNew.Location = new Point(
                txtMatKhauMoi.Location.X + txtMatKhauMoi.Width - picShowPassNew.Width - 8,
                txtMatKhauMoi.Location.Y + (txtMatKhauMoi.Height - picShowPassNew.Height) / 2
            );
            picShowPassNew.BringToFront();

            picShowPassConfirm.Image = Properties.Resources.eyeclose;
            picShowPassConfirm.BackColor = txtXacNhanMatKhauMoi.BackColor;
            picShowPassConfirm.Location = new Point(
                txtXacNhanMatKhauMoi.Location.X + txtXacNhanMatKhauMoi.Width - picShowPassConfirm.Width - 8,
                txtXacNhanMatKhauMoi.Location.Y + (txtXacNhanMatKhauMoi.Height - picShowPassConfirm.Height) / 2
            );
            picShowPassConfirm.BringToFront();

            UpdateUIText();
            SetInitialState();
        }

        #region UI State & Language

        private void SetInitialState()
        {
            txtSoDienThoai.Visible = true;
            btnSendCode.Visible = true;

            txtOTP.Visible = false; 
            btnVerifyCode.Visible = false;

            label3.Visible = false;
            txtMatKhauMoi.Visible = false;
            txtXacNhanMatKhauMoi.Visible = false;
            btnSave.Visible = false;
            picShowPassNew.Visible = false;
            picShowPassConfirm.Visible = false;
        }

        private void ShowOtpState()
        {
            txtSoDienThoai.Visible = false;
            btnSendCode.Visible = false;

            txtOTP.Visible = true; 
            btnVerifyCode.Visible = true;

            label3.Visible = false;
            txtMatKhauMoi.Visible = false;
            txtXacNhanMatKhauMoi.Visible = false;
            btnSave.Visible = false;
            picShowPassNew.Visible = false;
            picShowPassConfirm.Visible = false;
        }

        private void ShowResetPasswordState()
        {
            txtSoDienThoai.Visible = false;
            btnSendCode.Visible = false;

            txtOTP.Visible = false;
            btnVerifyCode.Visible = false;

            label2.Visible = false;
            label3.Visible = true;
            txtMatKhauMoi.Visible = true;
            txtXacNhanMatKhauMoi.Visible = true;
            btnSave.Visible = true;
            picShowPassNew.Visible = true;
            picShowPassConfirm.Visible = true;
        }

        private void InitializeLocalization()
        {
            rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(ForgotPasswordForm).Assembly);
        }

        public void UpdateUIText()
        {
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;

            this.Text = rm.GetString("Forgot_Title", culture);
            label1.Text = rm.GetString("Forgot_Title", culture);

            btnSave.Text = rm.GetString("Forgot_Button_Confirm", culture);
            btnSendCode.Text = rm.GetString("Forgot_Button_SendCode", culture);
            btnVerifyCode.Text = rm.GetString("Forgot_Button_VerifyCode", culture);

            txtOTP.PlaceholderText = rm.GetString("Forgot_Placeholder_OTP", culture);
            txtSoDienThoai.PlaceholderText = rm.GetString("Forgot_Placeholder_Phone", culture);
            txtMatKhauMoi.PlaceholderText = rm.GetString("Forgot_Placeholder_NewPass", culture);
            txtXacNhanMatKhauMoi.PlaceholderText = rm.GetString("Forgot_Placeholder_ConfirmPass", culture);

            try
            {
                this.BackColor = ThemeManager.BackgroundColor;
                label1.ForeColor = ThemeManager.TextColor;
                // label2.ForeColor = ThemeManager.TextColor;
                // label3.ForeColor = ThemeManager.TextColor;
                picShowPassNew.BackColor = txtMatKhauMoi.BackColor;
                picShowPassConfirm.BackColor = txtXacNhanMatKhauMoi.BackColor;
                txtOTP.BackColor = ThemeManager.PanelColor;
                txtOTP.ForeColor = ThemeManager.TextColor;
                txtSoDienThoai.BackColor = ThemeManager.PanelColor;
                txtSoDienThoai.ForeColor = ThemeManager.TextColor;
                txtMatKhauMoi.BackColor = ThemeManager.PanelColor;
                txtMatKhauMoi.ForeColor = ThemeManager.TextColor;
                txtXacNhanMatKhauMoi.BackColor = ThemeManager.PanelColor;
                txtXacNhanMatKhauMoi.ForeColor = ThemeManager.TextColor;
                btnSave.BackColor = ThemeManager.AccentColor;
                btnSave.BaseColor = ThemeManager.AccentColor;
                btnSave.ForeColor = Color.White;
                btnSendCode.BackColor = ThemeManager.AccentColor;
                btnSendCode.BaseColor = ThemeManager.AccentColor;
                btnSendCode.ForeColor = Color.White;
                btnVerifyCode.BackColor = ThemeManager.AccentColor;
                btnVerifyCode.BaseColor = ThemeManager.AccentColor;
                btnVerifyCode.ForeColor = Color.White;
            }
            catch (Exception) { }
        }
        #endregion

        #region Button Click Logic

        private void btnSendCode_Click(object sender, EventArgs e)
        {
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;
            _currentPhoneNumber = txtSoDienThoai.Text.Trim();

            if (string.IsNullOrEmpty(_currentPhoneNumber))
            {
                loginAlertPanel.ShowAlert(rm.GetString("Alert_Forgot_PhoneEmpty", culture), AlertPanel.AlertType.Error);
                return;
            }

            try
            {
                bool phoneExists = false;
                using (MySqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string queryCheck = "SELECT COUNT(*) FROM Employees WHERE SoDienThoai = @phone";
                    using (MySqlCommand cmdCheck = new MySqlCommand(queryCheck, connection))
                    {
                        cmdCheck.Parameters.AddWithValue("@phone", _currentPhoneNumber);
                        int count = Convert.ToInt32(cmdCheck.ExecuteScalar());
                        phoneExists = (count > 0);
                    }
                }

                if (!phoneExists)
                {
                    loginAlertPanel.ShowAlert(rm.GetString("Alert_Forgot_PhoneNotFound", culture), AlertPanel.AlertType.Error);
                    return;
                }

                if (SendVerifySms(_currentPhoneNumber))
                {
                    loginAlertPanel.ShowAlert(rm.GetString("Alert_Forgot_SendSuccess", culture), AlertPanel.AlertType.Success);
                    ShowOtpState();
                }
                else
                {
                    loginAlertPanel.ShowAlert(rm.GetString("Alert_Forgot_SendFail", culture), AlertPanel.AlertType.Error);
                }
            }
            catch (Exception ex)
            {
                loginAlertPanel.ShowAlert(rm.GetString("Alert_Forgot_DbError", culture) + ": " + ex.Message, AlertPanel.AlertType.Error);
            }
        }

        private void btnVerifyCode_Click(object sender, EventArgs e)
        {
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;
            string submittedOtp = txtOTP.Text.Trim();

            if (string.IsNullOrEmpty(submittedOtp))
            {
                loginAlertPanel.ShowAlert(rm.GetString("Alert_Forgot_OTPEmpty", culture), AlertPanel.AlertType.Error);
                return;
            }

            try
            {
                bool isOtpValid = CheckVerifySms(_currentPhoneNumber, submittedOtp);

                if (isOtpValid)
                {
                    loginAlertPanel.ShowAlert(rm.GetString("Alert_Forgot_OTPValid", culture), AlertPanel.AlertType.Success);
                    ShowResetPasswordState();
                }
                else
                {
                    loginAlertPanel.ShowAlert(rm.GetString("Alert_Forgot_OTPMismatch", culture), AlertPanel.AlertType.Error);
                }
            }
            catch (Exception ex)
            {
                loginAlertPanel.ShowAlert(rm.GetString("Alert_Forgot_DbError", culture) + ": " + ex.Message, AlertPanel.AlertType.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;

            string newPassword = txtMatKhauMoi.Text;
            string confirmPassword = txtXacNhanMatKhauMoi.Text;

            #region Input Validation
            if (string.IsNullOrEmpty(newPassword) || newPassword.Length < 6)
            {
                loginAlertPanel.ShowAlert(rm.GetString("Alert_Forgot_PasswordLength", culture), AlertPanel.AlertType.Error);
                return;
            }
            if (newPassword != confirmPassword)
            {
                loginAlertPanel.ShowAlert(rm.GetString("Alert_Forgot_PasswordMismatch", culture), AlertPanel.AlertType.Error);
                return;
            }
            #endregion

            try
            {
                using (MySqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string newPasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
                    string queryUpdate = "UPDATE Employees SET PasswordHash = @NewHash, ResetCode = NULL, CodeExpiry = NULL WHERE SoDienThoai = @SoDienThoai";

                    using (MySqlCommand cmdUpdate = new MySqlCommand(queryUpdate, connection))
                    {
                        cmdUpdate.Parameters.AddWithValue("@NewHash", newPasswordHash);
                        cmdUpdate.Parameters.AddWithValue("@SoDienThoai", _currentPhoneNumber);
                        cmdUpdate.ExecuteNonQuery();
                    }

                    loginAlertPanel.OnClose += AlertPanel_Success_OnClose;
                    loginAlertPanel.ShowAlert(rm.GetString("Alert_Forgot_Success", culture), AlertPanel.AlertType.Success);
                }
            }
            catch (Exception ex)
            {
                string errorMsg = rm.GetString("Alert_Forgot_DbError", culture);
                loginAlertPanel.ShowAlert(errorMsg + ": " + ex.Message, AlertPanel.AlertType.Error);
            }
        }

        #endregion

        #region Twilio Helpers
        private bool SendVerifySms(string phoneNumber)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("--- MOCK SMS (GIẢ LẬP) ---");
            System.Diagnostics.Debug.WriteLine($"Đang giả lập gửi đến SĐT: {phoneNumber}");
            System.Diagnostics.Debug.WriteLine($"Dùng ServiceSID: {verifyServiceSid}");
            System.Diagnostics.Debug.WriteLine("--- HÃY DÙNG MÃ '123456' ĐỂ TEST ---");
            return true;
#endif
            if (string.IsNullOrEmpty(verifyServiceSid) || verifyServiceSid.StartsWith("VA..."))
            {
                return false;
            }

            TwilioClient.Init(accountSid, authToken);
            try
            {
                string e164PhoneNumber = $"+84{phoneNumber.Substring(1)}";

                var verification = VerificationResource.Create(
                    to: e164PhoneNumber,
                    channel: "sms",
                    pathServiceSid: verifyServiceSid
                );

                return verification.Status == "pending";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi Twilio Verify: {ex.Message}");
                return false;
            }
        }

        private bool CheckVerifySms(string phoneNumber, string otpCode)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine($"--- MOCK SMS (GIẢ LẬP) --- Đang kiểm tra mã: {otpCode}");
            return (otpCode == "123456"); 
#endif
            TwilioClient.Init(accountSid, authToken);
            try
            {
                string e164PhoneNumber = $"+84{phoneNumber.Substring(1)}";

                var verificationCheck = VerificationCheckResource.Create(
                    to: e164PhoneNumber,
                    code: otpCode,
                    pathServiceSid: verifyServiceSid
                );

                return verificationCheck.Status == "approved";
            }
            catch (Twilio.Exceptions.ApiException ex)
            {
                Console.WriteLine($"Lỗi Twilio Verify Check: {ex.Message}");
                return false;
            }
        }
        #endregion

        #region Form Events
        private void AlertPanel_Success_OnClose(object sender, EventArgs e)
        {
            loginAlertPanel.OnClose -= AlertPanel_Success_OnClose;
            this.Close();
        }

        private void picShowPassNew_Click(object sender, EventArgs e)
        {
            if (txtMatKhauMoi.PasswordChar == '●')
            {
                txtMatKhauMoi.PasswordChar = '\0';
                picShowPassNew.Image = Properties.Resources.eye;
            }
            else
            {
                txtMatKhauMoi.PasswordChar = '●';
                picShowPassNew.Image = Properties.Resources.eyeclose;
            }
        }

        private void picShowPassConfirm_Click(object sender, EventArgs e)
        {
            if (txtXacNhanMatKhauMoi.PasswordChar == '●')
            {
                txtXacNhanMatKhauMoi.PasswordChar = '\0';
                picShowPassConfirm.Image = Properties.Resources.eye;
            }
            else
            {
                txtXacNhanMatKhauMoi.PasswordChar = '●';
                picShowPassConfirm.Image = Properties.Resources.eyeclose;
            }
        }

        private void txtMatKhauMoi_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSoDienThoai_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion
    }
}