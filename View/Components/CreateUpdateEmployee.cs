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
using System.Threading;
using System.Globalization;
using System.Resources;


namespace Environmental_Monitoring.View
{
    public partial class CreateUpdateEmployee : Form
    {
        private readonly int id = 0;
        private Model.Employee employee;
        public event EventHandler DataAdded;

        private ResourceManager rm;
        private CultureInfo culture;

        public CreateUpdateEmployee(int? id = 0)
        {
            InitializeComponent();
            this.id = id ?? 0;
        }

        private void LoadForm()
        {
            LoadRole();
            if (id == 0)
            {
                this.Text = rm.GetString("Form_AddEmployee_Title", culture);
            }
            else
            {
                this.Text = rm.GetString("Form_UpdateEmployee_Title", culture);
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
            emp.MaNhanVien = txtMaNV.Text;
            emp.HoTen = txtHoTen.Text;
            emp.PhongBan = txtPhong.Text;
            emp.DiaChi = txtDiaChi.Text;
            emp.SoDienThoai = txtSDT.Text;
            emp.Email = txtEmail.Text;
            emp.PasswordHash = txtMatKhau.Text;
            emp.RoleID = int.Parse(cbbRole.SelectedValue.ToString());
            int namSinh = 0;
            int.TryParse(txtNamSinh.Text, out namSinh);
            emp.NamSinh = namSinh;

            return emp;
        }

        private void SetData(Model.Employee model)
        {
            txtMaNV.Text = model.MaNhanVien;
            txtHoTen.Text = model.HoTen;
            txtNamSinh.Text = model.NamSinh.ToString();
            txtPhong.Text = model.PhongBan;
            txtDiaChi.Text = model.DiaChi;
            txtSDT.Text = model.SoDienThoai;
            txtEmail.Text = model.Email;
            cbbRole.SelectedValue = model.RoleID;
            txtMatKhau.PlaceholderText = (id != 0) ? "     " : rm.GetString("Placeholder_Password", culture);
        }

        private bool ValidateData()
        {
            string validationTitle = rm.GetString("Validation_Title", culture);

            if (string.IsNullOrWhiteSpace(txtMaNV.Text))
            {
                MessageBox.Show(rm.GetString("Validation_EmployeeCodeRequired", culture), validationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNV.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show(rm.GetString("Validation_FullNameRequired", culture), validationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }

            if (cbbRole.SelectedValue == null || cbbRole.SelectedIndex == -1)
            {
                MessageBox.Show(rm.GetString("Validation_RoleRequired", culture), validationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbbRole.Focus();
                return false;
            }

            int namSinh = 0;
            if (string.IsNullOrWhiteSpace(txtNamSinh.Text))
            {
                MessageBox.Show(rm.GetString("Validation_BirthYearRequired", culture), validationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamSinh.Focus();
                return false;
            }
            if (!int.TryParse(txtNamSinh.Text, out namSinh))
            {
                MessageBox.Show(rm.GetString("Validation_BirthYearInvalid", culture), validationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamSinh.Focus();
                return false;
            }
            if (namSinh <= 1900 || namSinh > DateTime.Now.Year)
            {
                MessageBox.Show(rm.GetString("Validation_BirthYearRange", culture), validationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamSinh.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show(rm.GetString("Validation_EmailRequired", culture), validationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }
            try
            {
                System.Net.Mail.MailAddress mailAddress = new System.Net.Mail.MailAddress(txtEmail.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show(rm.GetString("Validation_EmailFormat", culture), validationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPhong.Text))
            {
                MessageBox.Show(rm.GetString("Validation_DepartmentRequired", culture), validationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhong.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                MessageBox.Show(rm.GetString("Validation_AddressRequired", culture), validationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtSDT.Text))
            {
                MessageBox.Show(rm.GetString("Validation_PhoneRequired", culture), validationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }
            if (id == 0 && string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show(rm.GetString("Validation_PasswordRequired", culture), validationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return false;
            }
            if (id == 0 && txtMatKhau.Text.Length < 6)
            {
                MessageBox.Show(rm.GetString("Validation_PasswordLength", culture), validationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return false;
            }
            return true;
        }
        private void Save()
        {
            if (!ValidateData())
            {
                return;
            }
            Model.Employee emp = GetData();
            if (EmployeeRepo.Instance.ExistsMaNhanVien(emp.MaNhanVien, id))
            {
                MessageBox.Show(rm.GetString("Validation_EmployeeCodeExists", culture), rm.GetString("Validation_Title", culture), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (id == 0)
            {
                emp.PasswordHash = BCrypt.Net.BCrypt.HashPassword(emp.PasswordHash);
                EmployeeRepo.Instance.InsertEmployee(emp);
            }
            else
            {
                // Cập nhật
                if (string.IsNullOrEmpty(txtMatKhau.Text))
                {
                    emp.PasswordHash = employee.PasswordHash;
                }
                else
                {
                    emp.PasswordHash = BCrypt.Net.BCrypt.HashPassword(emp.PasswordHash);
                }
                EmployeeRepo.Instance.UpdateEmployee(emp);
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
            string savedLanguage = Properties.Settings.Default.Language;
            string cultureName = "vi";
            if (savedLanguage == "English")
            {
                cultureName = "en";
            }
            culture = new CultureInfo(cultureName); 
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(CreateUpdateEmployee).Assembly);

            UpdateUIText();

            LoadForm();
        }

        private void UpdateUIText()
        {
            lblMaNV.Text = rm.GetString("EmployeeID", culture);
            lblHoTen.Text = rm.GetString("FullName", culture);
            lblAddress.Text = rm.GetString("Address", culture);
            lblEmail.Text = rm.GetString("Email", culture);
            lblPass.Text = rm.GetString("Password", culture);
            lblYear.Text = rm.GetString("BirthYear", culture);
            lblDepartment.Text = rm.GetString("Department", culture);
            lblSDT.Text = rm.GetString("Phone", culture);
            lblRole.Text = rm.GetString("Role", culture);

            txtMaNV.PlaceholderText = rm.GetString("Placeholder_EmployeeCode", culture);
            txtHoTen.PlaceholderText = rm.GetString("Placeholder_FullName", culture);
            txtDiaChi.PlaceholderText = rm.GetString("Placeholder_Address", culture);
            txtEmail.PlaceholderText = rm.GetString("Placeholder_Email", culture);
            txtMatKhau.PlaceholderText = rm.GetString("Placeholder_Password", culture);
            txtNamSinh.PlaceholderText = rm.GetString("Placeholder_BirthYear", culture);
            txtPhong.PlaceholderText = rm.GetString("Placeholder_Department", culture);
            txtSDT.PlaceholderText = rm.GetString("Placeholder_Phone", culture);

            btnSave.Text = rm.GetString("Button_Save", culture);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
    }
}