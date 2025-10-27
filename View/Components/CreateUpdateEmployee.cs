using Environmental_Monitoring.Controller.Data;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Environmental_Monitoring.View
{
    public partial class CreateUpdateEmployee : Form
    {
        private readonly int id = 0;
        private Model.Employee employee;
        public event EventHandler DataAdded;
        public CreateUpdateEmployee(int? id = 0)
        {
            InitializeComponent();
            this.id = id??0;
        }

        private void LoadForm()
        {
            LoadRole();
            if (id == 0)
            {
                this.Text = "Thêm nhân viên";
            }
            else
            {
                this.Text = "Cập nhật nhân viên";
                employee = EmployeeRepo.Instance.GetById(id);
                SetData(employee);
            }
        }

        private void LoadRole()
        {
            cbbRole.DataSource = RoleRepo.Instance.GetAll();
            cbbRole.DisplayMember = "RoleName";
            cbbRole.ValueMember = "RoleId";
        }

        private Model.Employee GetData()
        {
            Model.Employee emp = new Model.Employee();

            emp.EmployeeID = id;
            emp.MaNhanVien = txbMaNV.Text;
            emp.HoTen = txbHoTen.Text;
            emp.PhongBan = txbPhong.Text;
            emp.DiaChi = txbDiaChi.Text;
            emp.SoDienThoai = txbSDT.Text;
            emp.Email = txbEmail.Text;
            emp.PasswordHash = txbPass.Text;
            emp.RoleID = int.Parse(cbbRole.SelectedValue.ToString());
            int namSinh = 0;
            int.TryParse(txbNamSinh.Text, out namSinh);
            emp.NamSinh = namSinh;

            return emp;
        }

        private void SetData(Model.Employee model)
        {
            txbMaNV.Text = model.MaNhanVien;
            txbHoTen.Text = model.HoTen;
            txbNamSinh.Text = model.NamSinh.ToString();
            txbPhong.Text = model.PhongBan;
            txbDiaChi.Text = model.DiaChi;
            txbSDT.Text = model.SoDienThoai;
            txbEmail.Text = model.Email;
            cbbRole.SelectedValue = model.RoleID;
        }


        private bool ValidateData()
        {
            if (string.IsNullOrWhiteSpace(txbMaNV.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã nhân viên.", "Lỗi xác thực", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbMaNV.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txbHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập Họ tên.", "Lỗi xác thực", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbHoTen.Focus();
                return false;
            }

            if (cbbRole.SelectedValue == null || cbbRole.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn một Vai trò.", "Lỗi xác thực", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbbRole.Focus();
                return false;
            }

            int namSinh = 0;
            if (string.IsNullOrWhiteSpace(txbNamSinh.Text))
            {
                MessageBox.Show("Vui lòng nhập Năm sinh.", "Lỗi xác thực", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbNamSinh.Focus();
                return false;
            }
            if (!int.TryParse(txbNamSinh.Text, out namSinh))
            {
                MessageBox.Show("Năm sinh phải là một con số (ví dụ: 1990).", "Lỗi xác thực", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbNamSinh.Focus();
                return false;
            }
            if (namSinh <= 1900 || namSinh > DateTime.Now.Year)
            {
                MessageBox.Show("Năm sinh không hợp lệ.", "Lỗi xác thực", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbNamSinh.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txbEmail.Text))
            {
                MessageBox.Show("Vui lòng nhập Email.", "Lỗi xác thực", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbEmail.Focus();
                return false;
            }
            try
            {
                System.Net.Mail.MailAddress mailAddress = new System.Net.Mail.MailAddress(txbEmail.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Định dạng Email không hợp lệ.", "Lỗi xác thực", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txbPhong.Text))
            {
                MessageBox.Show("Vui lòng nhập Phòng ban.", "Lỗi xác thực", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbPhong.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txbDiaChi.Text))
            {
                MessageBox.Show("Vui lòng nhập Địa chỉ.", "Lỗi xác thực", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbDiaChi.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txbSDT.Text))
            {
                MessageBox.Show("Vui lòng nhập Số điện thoại.", "Lỗi xác thực", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbSDT.Focus();
                return false;
            }
            if (id == 0 && string.IsNullOrWhiteSpace(txbPass.Text))
            {
                MessageBox.Show("Vui lòng nhập Mật khẩu.", "Lỗi xác thực", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbPass.Focus();
                return false;
            }
            if (id == 0 && txbPass.Text.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự.", "Lỗi xác thực", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbPass.Focus();
                return false;
            }
            return true;
        }

        private void Save()
        {
            if(!ValidateData())
            {
                return;
            }
            Model.Employee emp = GetData();
            if(EmployeeRepo.Instance.ExistsMaNhanVien(emp.MaNhanVien, id))
            {
                MessageBox.Show("Mã nhân viên này đã tồn tại.", "Lỗi xác thực", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (id == 0)
            {
                // Thêm mới
                emp.PasswordHash = BCrypt.Net.BCrypt.HashPassword(emp.PasswordHash);
                EmployeeRepo.Instance.InsertEmployee(emp);
                MessageBox.Show("Thêm nhân viên thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Cập nhật
                if(string.IsNullOrEmpty(txbPass.Text))
                {
                    emp.PasswordHash = employee.PasswordHash;
                }
                else
                {
                    emp.PasswordHash = BCrypt.Net.BCrypt.HashPassword(emp.PasswordHash);
                }
                EmployeeRepo.Instance.UpdateEmployee(emp);
                MessageBox.Show("Cập nhật nhân viên thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            CloseForm();
        }

        private void CloseForm()
        {
            DataAdded?.Invoke(this, EventArgs.Empty);
            this.Close();
        }

        private void CreateUpdateEmployee_Load(object sender, EventArgs e)
        {
            LoadForm();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
            
        }
    }
}
