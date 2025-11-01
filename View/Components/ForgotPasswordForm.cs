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

namespace Environmental_Monitoring.View
{
    public partial class ForgotPasswordForm : Form
    {
        string connectionString = "Server=sql12.freesqldatabase.com;Database=sql12800882;Uid=sql12800882;Pwd=TMlsWFrPxZ;";
        private Label label1;
        private Label label2;
        private Label label3;
        private RoundedTextBox txtTaiKhoan;
        private RoundedTextBox txtSoDienThoai;
        private RoundedTextBox txtMatKhauMoi;
        private RoundedTextBox txtXacNhanMatKhauMoi;
        private RoundedButton btnSave;

        // === THÊM 2 KHAI BÁO NÀY ===
        private PictureBox picShowPassNew;
        private PictureBox picShowPassConfirm;

        private AlertPanel loginAlertPanel;

        public ForgotPasswordForm(AlertPanel alertPanelFromLogin)
        {
            InitializeComponent();
            this.loginAlertPanel = alertPanelFromLogin;

            // === THÊM LOGIC NÀY VÀO CONSTRUCTOR ===
            // (Bạn cũng có thể đặt nó trong InitializeComponent, nhưng constructor sạch hơn)

            // Thiết lập icon cho Mật Khẩu Mới
            picShowPassNew = new PictureBox();
            picShowPassNew.Image = Properties.Resources.eyeclose;
            picShowPassNew.Size = new Size(24, 24); // Kích thước icon
            picShowPassNew.SizeMode = PictureBoxSizeMode.Zoom;
            picShowPassNew.Cursor = Cursors.Hand;
            picShowPassNew.Parent = txtMatKhauMoi; // Đặt nó LÊN TRÊN textbox
            picShowPassNew.BackColor = txtMatKhauMoi.BackColor;
            // Căn icon vào góc phải, ở giữa chiều cao
            picShowPassNew.Location = new Point(txtMatKhauMoi.Width - picShowPassNew.Width - 8, (txtMatKhauMoi.Height - picShowPassNew.Height) / 2);
            picShowPassNew.Click += picShowPassNew_Click; // Gắn sự kiện click
            picShowPassNew.BringToFront();

            // Thiết lập icon cho Xác Nhận Mật Khẩu Mới
            picShowPassConfirm = new PictureBox();
            picShowPassConfirm.Image = Properties.Resources.eyeclose;
            picShowPassConfirm.Size = new Size(24, 24);
            picShowPassConfirm.SizeMode = PictureBoxSizeMode.Zoom;
            picShowPassConfirm.Cursor = Cursors.Hand;
            picShowPassConfirm.Parent = txtXacNhanMatKhauMoi; // Đặt nó LÊN TRÊN textbox
            picShowPassConfirm.BackColor = txtXacNhanMatKhauMoi.BackColor;
            // Căn icon
            picShowPassConfirm.Location = new Point(txtXacNhanMatKhauMoi.Width - picShowPassConfirm.Width - 8, (txtXacNhanMatKhauMoi.Height - picShowPassConfirm.Height) / 2);
            picShowPassConfirm.Click += picShowPassConfirm_Click; // Gắn sự kiện click
            picShowPassConfirm.BringToFront();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string email = txtTaiKhoan.Text.Trim();
            string soDienThoai = txtSoDienThoai.Text.Trim();
            string newPassword = txtMatKhauMoi.Text;
            string confirmPassword = txtXacNhanMatKhauMoi.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(soDienThoai))
            {
                loginAlertPanel.ShowAlert("Vui lòng nhập Email và Số điện thoại để xác minh.", AlertPanel.AlertType.Error);
                return;
            }

            if (string.IsNullOrEmpty(newPassword) || newPassword.Length < 6)
            {
                loginAlertPanel.ShowAlert("Mật khẩu mới phải có ít nhất 6 ký tự.", AlertPanel.AlertType.Error);
                return;
            }

            if (newPassword != confirmPassword)
            {
                loginAlertPanel.ShowAlert("Mật khẩu mới và mật khẩu xác nhận không khớp.", AlertPanel.AlertType.Error);
                return;
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
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
                        loginAlertPanel.ShowAlert("Đổi mật khẩu thành công! Bạn có thể đăng nhập bằng mật khẩu mới.", AlertPanel.AlertType.Success);
                    }
                    else
                    {
                        loginAlertPanel.ShowAlert("Email hoặc Số điện thoại không chính xác. Vui lòng thử lại.", AlertPanel.AlertType.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                loginAlertPanel.ShowAlert("Lỗi kết nối CSDL: " + ex.Message, AlertPanel.AlertType.Error);
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
            label1.Size = new Size(269, 46);
            label1.TabIndex = 0;
            label1.Text = "Quên Mật Khẩu";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label2.Location = new Point(38, 99);
            label2.Name = "label2";
            label2.Size = new Size(222, 31);
            label2.TabIndex = 1;
            label2.Text = "Xác Thực Tài Khoản";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label3.Location = new Point(38, 248);
            label3.Name = "label3";
            label3.Size = new Size(162, 31);
            label3.TabIndex = 2;
            label3.Text = "Đổi Mật Khẩu";
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
            txtTaiKhoan.PlaceholderText = "Nhập Tài Khoản";
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
            txtSoDienThoai.PlaceholderText = "Nhập Số Điện Thoại";
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
            txtMatKhauMoi.PlaceholderText = "Nhập Mật Khẩu Mới";
            txtMatKhauMoi.ReadOnly = false;
            txtMatKhauMoi.Size = new Size(296, 40);
            txtMatKhauMoi.TabIndex = 5;
            txtMatKhauMoi.UseSystemPasswordChar = true;
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
            txtXacNhanMatKhauMoi.PlaceholderText = "Xác Nhận Mật Khẩu Mới";
            txtXacNhanMatKhauMoi.ReadOnly = false;
            txtXacNhanMatKhauMoi.Size = new Size(296, 40);
            txtXacNhanMatKhauMoi.TabIndex = 6;
            txtXacNhanMatKhauMoi.UseSystemPasswordChar = true;
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
            btnSave.Text = "Xác Nhận";
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

        // === THÊM 2 HÀM SỰ KIỆN CLICK NÀY VÀO ===

        private void picShowPassNew_Click(object sender, EventArgs e)
        {
            if (txtMatKhauMoi.UseSystemPasswordChar == true)
            {
                txtMatKhauMoi.UseSystemPasswordChar = false;
                picShowPassNew.Image = Properties.Resources.eye; 
            }
            else
            {
                txtMatKhauMoi.UseSystemPasswordChar = true;
                picShowPassNew.Image = Properties.Resources.eyeclose; 
            }
        }

        private void picShowPassConfirm_Click(object sender, EventArgs e)
        {
            if (txtXacNhanMatKhauMoi.UseSystemPasswordChar == true)
            {
                txtXacNhanMatKhauMoi.UseSystemPasswordChar = false;
                picShowPassConfirm.Image = Properties.Resources.eye; 
            }
            else
            {
                txtXacNhanMatKhauMoi.UseSystemPasswordChar = true;
                picShowPassConfirm.Image = Properties.Resources.eyeclose; // Ảnh mắt đóng
            }
        }
    }
}