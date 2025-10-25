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
                MessageBox.Show("Vui lòng nhập đầy đủ tài khoản và mật khẩu.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                    MessageBox.Show($"Chào mừng {hoTen} đã đăng nhập thành công!", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    Mainlayout mainForm = new Mainlayout();
                                    mainForm.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    MessageBox.Show("Mật khẩu không chính xác. Vui lòng thử lại.", "Lỗi Đăng Nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Tài khoản không tồn tại. Vui lòng kiểm tra lại Email.", "Lỗi Đăng Nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi kết nối database: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
    }
}
