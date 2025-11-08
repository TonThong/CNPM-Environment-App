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

namespace Environmental_Monitoring.View
{
    public partial class ForgotPasswordForm : Form
    {

        private Label label1;
        private Label label2;
        private Label label3;
        private RoundedTextBox txtTaiKhoan;
        private RoundedTextBox txtSoDienThoai;
        private RoundedTextBox txtMatKhauMoi;
        private RoundedTextBox txtXacNhanMatKhauMoi;
        private RoundedButton btnSave;

        private PictureBox picShowPassNew;
        private PictureBox picShowPassConfirm;

        private AlertPanel loginAlertPanel;
        private ResourceManager rm;

        public ForgotPasswordForm(AlertPanel alertPanelFromLogin)
        {
            InitializeComponent();
            this.loginAlertPanel = alertPanelFromLogin;

            InitializeLocalization();

            picShowPassNew = new PictureBox();
            picShowPassNew.Image = Properties.Resources.eyeclose;
            picShowPassNew.Size = new Size(24, 24);
            picShowPassNew.SizeMode = PictureBoxSizeMode.Zoom;
            picShowPassNew.Cursor = Cursors.Hand;
            this.Controls.Add(picShowPassNew);
            picShowPassNew.BackColor = txtMatKhauMoi.BackColor;
            picShowPassNew.Location = new Point(
                txtMatKhauMoi.Location.X + txtMatKhauMoi.Width - picShowPassNew.Width - 8,
                txtMatKhauMoi.Location.Y + (txtMatKhauMoi.Height - picShowPassNew.Height) / 2
            );
            picShowPassNew.Click += picShowPassNew_Click;
            picShowPassNew.BringToFront();

            picShowPassConfirm = new PictureBox();
            picShowPassConfirm.Image = Properties.Resources.eyeclose;
            picShowPassConfirm.Size = new Size(24, 24);
            picShowPassConfirm.SizeMode = PictureBoxSizeMode.Zoom;
            picShowPassConfirm.Cursor = Cursors.Hand;
            this.Controls.Add(picShowPassConfirm);
            picShowPassConfirm.BackColor = txtXacNhanMatKhauMoi.BackColor;
            picShowPassConfirm.Location = new Point(
                txtXacNhanMatKhauMoi.Location.X + txtXacNhanMatKhauMoi.Width - picShowPassConfirm.Width - 8,
                txtXacNhanMatKhauMoi.Location.Y + (txtXacNhanMatKhauMoi.Height - picShowPassConfirm.Height) / 2
            );
            picShowPassConfirm.Click += picShowPassConfirm_Click;
            picShowPassConfirm.BringToFront();

            UpdateUIText();
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
            label2.Text = rm.GetString("Forgot_Section_Verify", culture);
            label3.Text = rm.GetString("Forgot_Section_ChangePass", culture);
            btnSave.Text = rm.GetString("Forgot_Button_Confirm", culture);

            txtTaiKhoan.PlaceholderText = rm.GetString("Forgot_Placeholder_Email", culture);
            txtSoDienThoai.PlaceholderText = rm.GetString("Forgot_Placeholder_Phone", culture);
            txtMatKhauMoi.PlaceholderText = rm.GetString("Forgot_Placeholder_NewPass", culture);
            txtXacNhanMatKhauMoi.PlaceholderText = rm.GetString("Forgot_Placeholder_ConfirmPass", culture);


            try
            {
                this.BackColor = ThemeManager.BackgroundColor;
                label1.ForeColor = ThemeManager.TextColor;
                label2.ForeColor = ThemeManager.TextColor;
                label3.ForeColor = ThemeManager.TextColor;

                picShowPassNew.BackColor = txtMatKhauMoi.BackColor;
                picShowPassConfirm.BackColor = txtXacNhanMatKhauMoi.BackColor;

                txtTaiKhoan.BackColor = ThemeManager.PanelColor;
                txtTaiKhoan.ForeColor = ThemeManager.TextColor;
                txtSoDienThoai.BackColor = ThemeManager.PanelColor;
                txtSoDienThoai.ForeColor = ThemeManager.TextColor;
                txtMatKhauMoi.BackColor = ThemeManager.PanelColor;
                txtMatKhauMoi.ForeColor = ThemeManager.TextColor;
                txtXacNhanMatKhauMoi.BackColor = ThemeManager.PanelColor;
                txtXacNhanMatKhauMoi.ForeColor = ThemeManager.TextColor;

                btnSave.BackColor = ThemeManager.AccentColor;
                btnSave.BaseColor = ThemeManager.AccentColor;
                btnSave.ForeColor = Color.White;
            }
            catch (Exception) { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;

            string email = txtTaiKhoan.Text.Trim();
            string soDienThoai = txtSoDienThoai.Text.Trim();
            string newPassword = txtMatKhauMoi.Text;
            string confirmPassword = txtXacNhanMatKhauMoi.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(soDienThoai))
            {
                loginAlertPanel.ShowAlert(rm.GetString("Alert_Forgot_ValidationEmpty", culture), AlertPanel.AlertType.Error);
                return;
            }
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
            try
            {
                using (MySqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string queryVerify = "SELECT EmployeeID FROM Employees WHERE Email = @Email AND SoDienThoai = @SoDienThoai";
                    int employeeId = 0;
                    using (MySqlCommand cmdVerify = new MySqlCommand(queryVerify, connection))
                    {
                        cmdVerify.Parameters.AddWithValue("@Email", email);
                        cmdVerify.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
                        object result = cmdVerify.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            employeeId = Convert.ToInt32(result);
                        }
                    }
                    if (employeeId > 0)
                    {
                        string newPasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
                        string queryUpdate = "UPDATE Employees SET PasswordHash = @NewHash WHERE EmployeeID = @ID";
                        using (MySqlCommand cmdUpdate = new MySqlCommand(queryUpdate, connection))
                        {
                            cmdUpdate.Parameters.AddWithValue("@NewHash", newPasswordHash);
                            cmdUpdate.Parameters.AddWithValue("@ID", employeeId);
                            cmdUpdate.ExecuteNonQuery();
                        }
                        loginAlertPanel.OnClose += AlertPanel_Success_OnClose;
                        loginAlertPanel.ShowAlert(rm.GetString("Alert_Forgot_Success", culture), AlertPanel.AlertType.Success);
                    }
                    else
                    {
                        loginAlertPanel.ShowAlert(rm.GetString("Alert_Forgot_InvalidCredentials", culture), AlertPanel.AlertType.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMsg = rm.GetString("Alert_Forgot_DbError", culture);
                loginAlertPanel.ShowAlert(errorMsg + ": " + ex.Message, AlertPanel.AlertType.Error);
            }
        }

        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtTaiKhoan = new RoundedTextBox();
            txtSoDienThoai = new RoundedTextBox();
            txtMatKhauMoi = new RoundedTextBox();
            txtXacNhanMatKhauMoi = new RoundedTextBox();
            btnSave = new RoundedButton();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label1.Location = new Point(80, 9);
            label1.Name = "label1";
            label1.Size = new Size(0, 46);
            label1.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label2.Location = new Point(38, 99);
            label2.Name = "label2";
            label2.Size = new Size(0, 31);
            label2.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label3.Location = new Point(38, 248);
            label3.Name = "label3";
            label3.Size = new Size(0, 31);
            label3.TabIndex = 2;
            // 
            // txtTaiKhoan
            // 
            txtTaiKhoan.BorderRadius = 15;
            txtTaiKhoan.BorderThickness = 2;
            txtTaiKhoan.FocusBorderColor = Color.DimGray;
            txtTaiKhoan.HoverBorderColor = Color.DarkGray;
            txtTaiKhoan.Location = new Point(67, 142);
            txtTaiKhoan.Multiline = false;
            txtTaiKhoan.Name = "txtTaiKhoan";
            txtTaiKhoan.NormalBorderColor = Color.LightGray;
            txtTaiKhoan.Padding = new Padding(10);
            txtTaiKhoan.PasswordChar = '\0';
            txtTaiKhoan.PlaceholderText = "";
            txtTaiKhoan.ReadOnly = false;
            txtTaiKhoan.Size = new Size(296, 40);
            txtTaiKhoan.TabIndex = 3;
            txtTaiKhoan.UseSystemPasswordChar = false;
            // 
            // txtSoDienThoai
            // 
            txtSoDienThoai.BorderRadius = 15;
            txtSoDienThoai.BorderThickness = 2;
            txtSoDienThoai.FocusBorderColor = Color.DimGray;
            txtSoDienThoai.HoverBorderColor = Color.DarkGray;
            txtSoDienThoai.Location = new Point(67, 196);
            txtSoDienThoai.Multiline = false;
            txtSoDienThoai.Name = "txtSoDienThoai";
            txtSoDienThoai.NormalBorderColor = Color.LightGray;
            txtSoDienThoai.Padding = new Padding(10);
            txtSoDienThoai.PasswordChar = '\0';
            txtSoDienThoai.PlaceholderText = "";
            txtSoDienThoai.ReadOnly = false;
            txtSoDienThoai.Size = new Size(296, 40);
            txtSoDienThoai.TabIndex = 4;
            txtSoDienThoai.UseSystemPasswordChar = false;
            // 
            // txtMatKhauMoi
            // 
            txtMatKhauMoi.BorderRadius = 15;
            txtMatKhauMoi.BorderThickness = 2;
            txtMatKhauMoi.FocusBorderColor = Color.DimGray;
            txtMatKhauMoi.HoverBorderColor = Color.DarkGray;
            txtMatKhauMoi.Location = new Point(67, 298);
            txtMatKhauMoi.Multiline = false;
            txtMatKhauMoi.Name = "txtMatKhauMoi";
            txtMatKhauMoi.NormalBorderColor = Color.LightGray;
            txtMatKhauMoi.Padding = new Padding(10);
            txtMatKhauMoi.PasswordChar = '●';
            txtMatKhauMoi.PlaceholderText = "";
            txtMatKhauMoi.ReadOnly = false;
            txtMatKhauMoi.Size = new Size(296, 40);
            txtMatKhauMoi.TabIndex = 5;
            // 
            // txtXacNhanMatKhauMoi
            // 
            txtXacNhanMatKhauMoi.BorderRadius = 15;
            txtXacNhanMatKhauMoi.BorderThickness = 2;
            txtXacNhanMatKhauMoi.FocusBorderColor = Color.DimGray;
            txtXacNhanMatKhauMoi.HoverBorderColor = Color.DarkGray;
            txtXacNhanMatKhauMoi.Location = new Point(67, 353);
            txtXacNhanMatKhauMoi.Multiline = false;
            txtXacNhanMatKhauMoi.Name = "txtXacNhanMatKhauMoi";
            txtXacNhanMatKhauMoi.NormalBorderColor = Color.LightGray;
            txtXacNhanMatKhauMoi.Padding = new Padding(10);
            txtXacNhanMatKhauMoi.PasswordChar = '●';
            txtXacNhanMatKhauMoi.PlaceholderText = "";
            txtXacNhanMatKhauMoi.ReadOnly = false;
            txtXacNhanMatKhauMoi.Size = new Size(296, 40);
            txtXacNhanMatKhauMoi.TabIndex = 6;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.SeaGreen;
            btnSave.BaseColor = Color.SeaGreen;
            btnSave.BorderColor = Color.Transparent;
            btnSave.BorderRadius = 20;
            btnSave.BorderSize = 0;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            btnSave.ForeColor = Color.White;
            btnSave.HoverColor = Color.FromArgb(34, 139, 34);
            btnSave.Location = new Point(142, 416);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(153, 65);
            btnSave.TabIndex = 7;
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // ForgotPasswordForm
            // 
            BackColor = Color.FromArgb(217, 244, 227);
            ClientSize = new Size(430, 507);
            Controls.Add(btnSave);
            Controls.Add(txtXacNhanMatKhauMoi);
            Controls.Add(txtMatKhauMoi);
            Controls.Add(txtSoDienThoai);
            Controls.Add(txtTaiKhoan);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "ForgotPasswordForm";
            ResumeLayout(false);
            PerformLayout();

        }

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
    }
}