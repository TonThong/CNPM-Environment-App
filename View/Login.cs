﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Environmental_Monitoring.View.Components;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BCrypt.Net;


namespace Environmental_Monitoring.View
{
    public partial class Login : Form
    {

        string connectionString = "Server=sql12.freesqldatabase.com;Database=sql12800882;Uid=sql12800882;Pwd=TMlsWFrPxZ;";

        public Login()
        {
            InitializeComponent();

            txtMatKhau.UseSystemPasswordChar = true;
            picShowPass.Image = Properties.Resources.eyeclose;
            picShowPass.Parent = txtMatKhau;
            picShowPass.BackColor = txtMatKhau.BackColor;
            picShowPass.Location = new Point(txtMatKhau.Width - picShowPass.Width - 8, (txtMatKhau.Height - picShowPass.Height) / 2);
            picShowPass.BringToFront();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

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
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT HoTen, PasswordHash, RoleID FROM Employees WHERE Email = @InputTaiKhoan";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@InputTaiKhoan", taiKhoan);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string passwordHashFromDb = reader["PasswordHash"].ToString();
                                string hoTen = reader["HoTen"].ToString();
                                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(matKhau, passwordHashFromDb);

                                if (isPasswordValid)
                                {
                                    alertPanel.OnClose += AlertPanel_Success_OnClose;
                                    alertPanel.ShowAlert("Đăng nhập thành công!", AlertPanel.AlertType.Success);

                                    this.Enabled = false;
                                }
                                else
                                {
                                    alertPanel.ShowAlert("Mật khẩu không chính xác. Vui lòng thử lại.", AlertPanel.AlertType.Error);
                                }
                            }
                            else
                            {
                                alertPanel.ShowAlert("Tài khoản không tồn tại. Vui lòng kiểm tra lại Email.", AlertPanel.AlertType.Error);
                            }
                        }
                    }
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
    }
}
