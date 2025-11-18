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
using System.Globalization;
using System.Threading;

namespace Environmental_Monitoring.View
{
    public partial class CreateUpdateEmployee : Form
    {
        private readonly int id = 0;
        private Model.Employee employee;
        public event EventHandler DataAdded;

        public bool IsAddMode { get; private set; }

        private ResourceManager rm;

        public CreateUpdateEmployee(int? id = 0)
        {
            InitializeComponent();
            this.id = id ?? 0;

            this.IsAddMode = (this.id == 0);
            btnCancel.Click += new EventHandler(btnCancel_Click);

            this.Load += new System.EventHandler(this.CreateUpdateEmployee_Load);

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CreateUpdateEmployee_Load(object sender, EventArgs e)
        {
            rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(CreateUpdateEmployee).Assembly);
            UpdateUIText();
            LoadForm();
        }

        private void LoadForm()
        {
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;
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

        /// <summary>
        /// Tải danh sách vai trò vào ComboBox và dịch tên vai trò theo ngôn ngữ hiện tại.
        /// </summary>
        private void LoadRole()
        {
            DataTable rolesTable = RoleRepo.Instance.GetAll();

            List<RoleDisplayItem> translatedRoles = new List<RoleDisplayItem>();

            foreach (DataRow row in rolesTable.Rows)
            {
                int roleId = Convert.ToInt32(row["RoleID"]);
                string originalRoleName = row["RoleName"].ToString();

                string translatedName = TranslateRoleName(originalRoleName);

                translatedRoles.Add(new RoleDisplayItem
                {
                    RoleId = roleId,
                    TranslatedRoleName = translatedName
                });
            }

            cbbRole.DataSource = translatedRoles;
            cbbRole.DisplayMember = "TranslatedRoleName";
            cbbRole.ValueMember = "RoleId";
        }

        private Model.Employee GetData()
        {
            Model.Employee emp = new Model.Employee();

            emp.EmployeeID = id;
            emp.MaNhanVien = txtMaNV.Text;
            emp.HoTen = txtHoTen.Text;
            emp.TruongBoPhan = chkTruongBoPhan.Checked ? 1 : 0;
            emp.DiaChi = txtDiaChi.Text;
            emp.SoDienThoai = txtSDT.Text;
            emp.Email = txtEmail.Text;
            emp.PasswordHash = txtMatKhau.Text;
            emp.RoleID = int.Parse(cbbRole.SelectedValue.ToString());

            emp.NamSinh = dtpNamSinh.Value;

            return emp;
        }

        private void SetData(Model.Employee model)
        {
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;

            txtMaNV.Text = model.MaNhanVien;
            txtHoTen.Text = model.HoTen;

            if (model.NamSinh.HasValue)
            {
                dtpNamSinh.Value = model.NamSinh.Value;
            }
            else
            {
                dtpNamSinh.Value = DateTime.Now;
            }

            chkTruongBoPhan.Checked = (model.TruongBoPhan == 1);
            txtDiaChi.Text = model.DiaChi;
            txtSDT.Text = model.SoDienThoai;
            txtEmail.Text = model.Email;
            cbbRole.SelectedValue = model.RoleID;

            string placeholderPassword = rm.GetString("Placeholder_Password", culture);
            txtMatKhau.PlaceholderText = (id != 0) ? rm.GetString("Placeholder_Password_Update", culture) : placeholderPassword;
        }

        private bool ValidateData()
        {
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;
            string validationTitle = rm.GetString("Validation_Title", culture);

            if (string.IsNullOrWhiteSpace(txtMaNV.Text))
            {
                MessageBox.Show(rm.GetString("Validation_EmployeeCodeRequired", culture), validationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNV.Focus();
                return false;
            }

            string email = txtEmail.Text.Trim();
            if (!string.IsNullOrWhiteSpace(email) && !ValidationHelper.IsValidEmailFormat(email))
            {
                MessageBox.Show("Định dạng email không hợp lệ.", validationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            string phone = txtSDT.Text.Trim();
            if (!string.IsNullOrWhiteSpace(phone) && !ValidationHelper.IsValidVietnamesePhone(phone))
            {
                MessageBox.Show("Số điện thoại không hợp lệ. (Phải là 10 số, bắt đầu bằng 0).", validationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }

            string password = txtMatKhau.Text;

            if (id == 0)
            {
                if (string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Mật khẩu là bắt buộc khi tạo mới nhân viên.", validationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhau.Focus();
                    return false;
                }

                if (password.Length < 6)
                {
                    MessageBox.Show("Mật khẩu phải có tối thiểu 6 ký tự.", validationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhau.Focus();
                    return false;
                }
            }
            else if (id != 0 && !string.IsNullOrWhiteSpace(password) && password.Length < 6)
            {
                MessageBox.Show("Mật khẩu mới phải có tối thiểu 6 ký tự.", validationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return false;
            }

            return true;
        }

        private void Save()
        {
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;

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

            if (EmployeeRepo.Instance.ExistsEmail(emp.Email, id))
            {
                MessageBox.Show(rm.GetString("Validation_EmailExists", culture), rm.GetString("Validation_Title", culture), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (id == 0)
            {
                emp.PasswordHash = BCrypt.Net.BCrypt.HashPassword(emp.PasswordHash);
                EmployeeRepo.Instance.InsertEmployee(emp);

                object empIdObj = DataProvider.Instance.ExecuteScalar("SELECT LAST_INSERT_ID()", null);
                int newEmployeeId = (empIdObj != null && int.TryParse(empIdObj.ToString(), out int eid)) ? eid : 0;

                string noiDung = $"Nhân viên '{emp.HoTen}' (Mã: {emp.MaNhanVien}) vừa được thêm.";
                int adminRoleID = 5;
                NotificationService.CreateNotification("NhanVienMoi", noiDung, adminRoleID, null, newEmployeeId);
            }
            else
            {
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

        private void UpdateUIText()
        {
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;

            lblMaNV.Text = rm.GetString("EmployeeID", culture);
            lblHoTen.Text = rm.GetString("FullName", culture);
            lblAddress.Text = rm.GetString("Address", culture);
            lblEmail.Text = rm.GetString("Email", culture);
            lblPass.Text = rm.GetString("Password", culture);
            lblYear.Text = rm.GetString("BirthYear", culture);
            chkTruongBoPhan.Text = rm.GetString("IsDepartmentHead", culture);
            lblSDT.Text = rm.GetString("Phone", culture);
            lblRole.Text = rm.GetString("Role", culture);

            txtMaNV.PlaceholderText = rm.GetString("Placeholder_EmployeeCode", culture);
            txtHoTen.PlaceholderText = rm.GetString("Placeholder_FullName", culture);
            txtDiaChi.PlaceholderText = rm.GetString("Placeholder_Address", culture);
            txtEmail.PlaceholderText = rm.GetString("Placeholder_Email", culture);
            txtMatKhau.PlaceholderText = rm.GetString("Placeholder_Password", culture);


            txtSDT.PlaceholderText = rm.GetString("Placeholder_Phone", culture);

            btnSave.Text = rm.GetString("Button_Save", culture);
            btnCancel.Text = rm.GetString("Button_Cancel", culture);

            try
            {
                this.BackColor = ThemeManager.BackgroundColor_Popup_QLNV;

                foreach (Control c in this.Controls)
                {
                    if (c is Label)
                    {
                        c.ForeColor = ThemeManager.TextColor;
                    }
                    if (c is CheckBox)
                    {
                        c.ForeColor = ThemeManager.TextColor;
                    }
                }

                txtMaNV.BackColor = ThemeManager.PanelColor;
                txtMaNV.ForeColor = ThemeManager.TextColor;
                txtHoTen.BackColor = ThemeManager.PanelColor;
                txtHoTen.ForeColor = ThemeManager.TextColor;
                txtDiaChi.BackColor = ThemeManager.PanelColor;
                txtDiaChi.ForeColor = ThemeManager.TextColor;
                txtEmail.BackColor = ThemeManager.PanelColor;
                txtEmail.ForeColor = ThemeManager.TextColor;
                txtMatKhau.BackColor = ThemeManager.PanelColor;
                txtMatKhau.ForeColor = ThemeManager.TextColor;

                dtpNamSinh.BackColor = ThemeManager.PanelColor;
                dtpNamSinh.ForeColor = ThemeManager.TextColor;

                txtSDT.BackColor = ThemeManager.PanelColor;
                txtSDT.ForeColor = ThemeManager.TextColor;
                cbbRole.BackColor = ThemeManager.PanelColor;
                cbbRole.ForeColor = ThemeManager.TextColor;

                btnSave.BackColor = ThemeManager.AccentColor;
                btnSave.ForeColor = Color.White;
                btnCancel.BackColor = ThemeManager.SecondaryPanelColor;
                btnCancel.ForeColor = ThemeManager.TextColor;
            }
            catch (Exception) { }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        public class RoleDisplayItem
        {
            public int RoleId { get; set; }
            public string TranslatedRoleName { get; set; }
        }

        private string TranslateRoleName(string originalRoleName)
        {
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;

            switch (originalRoleName.ToLower())
            {
                case "admin":
                    return rm.GetString("Role_Admin", culture);
                case "business":
                    return rm.GetString("Role_Business", culture);
                case "field":
                    return rm.GetString("Role_Field", culture);
                case "lab":
                    return rm.GetString("Role_Lab", culture);
                case "plan":
                    return rm.GetString("Role_Plan", culture);
                case "result":
                    return rm.GetString("Role_Result", culture);
                default:
                    return originalRoleName;
            }
        }


    }
}