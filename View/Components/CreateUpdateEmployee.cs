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
using System.Text.RegularExpressions; // Cần thêm thư viện này để check Regex

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

            // Cài đặt TextBox Mã NV thành chỉ đọc
            txtMaNV.ReadOnly = true;

            UpdateUIText();
            LoadForm();
        }

        private void LoadForm()
        {
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;
            LoadRole();

            if (id == 0) // Chế độ Thêm mới
            {
                this.Text = rm.GetString("Form_AddEmployee_Title", culture);

                // 1. Tự động lấy mã tiếp theo để hiển thị (NV + ID tiếp theo)
                txtMaNV.Text = GetNextEmployeeCode();
                txtMaNV.Enabled = false; // Vô hiệu hóa ô nhập liệu để người dùng biết là tự động

                // 2. Clear các ô khác (đề phòng)
                txtHoTen.Text = "";
                txtDiaChi.Text = "";
                txtEmail.Text = "";
                txtSDT.Text = "";
                txtMatKhau.Text = "";
            }
            else // Chế độ Cập nhật
            {
                this.Text = rm.GetString("Form_UpdateEmployee_Title", culture);
                employee = EmployeeRepo.Instance.GetById(id);
                SetData(employee);
                txtMaNV.ReadOnly = true; // Khi update thì không cho sửa mã nhân viên
            }

            // 3. SET FOCUS VÀO Ô HỌ TÊN
            // Sử dụng BeginInvoke để đảm bảo Form load xong mới focus được
            this.BeginInvoke((MethodInvoker)delegate {
                txtHoTen.Focus();
                txtHoTen.Select();
            });
        }

        /// <summary>
        /// Hàm lấy mã nhân viên dự kiến (NV + MaxID + 1)
        /// </summary>
        private string GetNextEmployeeCode()
        {
            try
            {
                // Lấy ID lớn nhất hiện tại trong bảng Employees
                string query = "SELECT MAX(EmployeeID) FROM Employees";
                object result = DataProvider.Instance.ExecuteScalar(query);

                int nextId = 1; // Mặc định là 1 nếu bảng rỗng

                if (result != null && result != DBNull.Value)
                {
                    nextId = Convert.ToInt32(result) + 1;
                }

                // Format thành NV0001, NV0015...
                return "NV" + nextId.ToString("D4");
            }
            catch
            {
                return "NV????"; // Trả về mã lỗi nếu kết nối DB có vấn đề
            }
        }

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
            // Nếu là thêm mới, Mã NV sẽ được xử lý sau khi Insert xong, ở đây lấy tạm giá trị hiển thị hoặc text tạm
            // Quan trọng là bước UPDATE lại mã chuẩn theo ID thật sự bên dưới hàm Save()
            emp.MaNhanVien = (id == 0) ? txtMaNV.Text : txtMaNV.Text;
            emp.HoTen = txtHoTen.Text.Trim();
            emp.TruongBoPhan = chkTruongBoPhan.Checked ? 1 : 0;
            emp.DiaChi = txtDiaChi.Text.Trim();
            emp.SoDienThoai = txtSDT.Text.Trim();
            emp.Email = txtEmail.Text.Trim();
            emp.PasswordHash = txtMatKhau.Text;

            if (cbbRole.SelectedValue != null)
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

        // --- Logic Validate Dữ Liệu Chặt Chẽ ---
        private bool ValidateData()
        {
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;
            string validationTitle = rm.GetString("Validation_Title", culture) ?? "Thông báo";

            // 1. Validate Họ Tên
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ và tên nhân viên.", validationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }

            // 2. Validate Năm Sinh (Tùy chọn: cảnh báo nếu < 18 tuổi)
            if (dtpNamSinh.Value > DateTime.Now.AddYears(-18))
            {
                // MessageBox.Show("Nhân viên chưa đủ 18 tuổi.", validationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // 3. Validate Địa chỉ
            if (string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ.", validationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return false;
            }

            // 4. Validate Email (Định dạng & Không được để trống)
            string email = txtEmail.Text.Trim();
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Vui lòng nhập Email.", validationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }
            // Regex check email cơ bản
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, emailPattern))
            {
                MessageBox.Show("Định dạng Email không hợp lệ (Ví dụ: abc@domain.com).", validationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            // 5. Validate Số điện thoại (Định dạng VN & Không để trống)
            string phone = txtSDT.Text.Trim();
            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại.", validationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }
            // Regex check SĐT Việt Nam: 10 số, bắt đầu bằng 0
            if (!Regex.IsMatch(phone, @"^0\d{9}$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ. Phải là 10 chữ số và bắt đầu bằng số 0.", validationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }

            // 6. Validate Vai trò
            if (cbbRole.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn vai trò (Role).", validationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbbRole.Focus();
                return false;
            }

            // 7. Validate Mật khẩu
            string password = txtMatKhau.Text;
            if (id == 0) // Thêm mới bắt buộc nhập
            {
                if (string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Mật khẩu là bắt buộc khi tạo mới.", validationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            else // Cập nhật (có thể để trống nếu không đổi pass)
            {
                if (!string.IsNullOrWhiteSpace(password) && password.Length < 6)
                {
                    MessageBox.Show("Mật khẩu mới phải có tối thiểu 6 ký tự.", validationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhau.Focus();
                    return false;
                }
            }

            return true;
        }

        private void Save()
        {
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;

            if (!ValidateData()) return; // Dừng nếu validate thất bại

            Model.Employee emp = GetData();

            // Kiểm tra trùng Email (Trừ chính mình nếu đang edit)
            if (EmployeeRepo.Instance.ExistsEmail(emp.Email, id))
            {
                MessageBox.Show(rm.GetString("Validation_EmailExists", culture), rm.GetString("Validation_Title", culture), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (id == 0) // --- TRƯỜNG HỢP THÊM MỚI ---
            {
                // Hash mật khẩu
                emp.PasswordHash = BCrypt.Net.BCrypt.HashPassword(emp.PasswordHash);

                // 1. Insert nhân viên (Mã NV sẽ được cập nhật lại sau)
                EmployeeRepo.Instance.InsertEmployee(emp);

                // 2. Lấy ID vừa tạo ra
                object empIdObj = DataProvider.Instance.ExecuteScalar("SELECT LAST_INSERT_ID()", null);
                int newEmployeeId = (empIdObj != null && int.TryParse(empIdObj.ToString(), out int eid)) ? eid : 0;

                if (newEmployeeId > 0)
                {
                    // 3. Tạo Mã NV chuẩn xác: NV + 4 số ID (Ví dụ ID=2 -> NV0002)
                    // Việc này đảm bảo mã NV luôn khớp với ID thật trong DB
                    string finalCode = "NV" + newEmployeeId.ToString("D4");

                    // 4. Update ngược lại vào Database
                    string updateCodeQuery = "UPDATE Employees SET MaNhanVien = @code WHERE EmployeeID = @id";
                    DataProvider.Instance.ExecuteNonQuery(updateCodeQuery, new object[] { finalCode, newEmployeeId });

                    // Tạo thông báo
                    string noiDung = $"Nhân viên '{emp.HoTen}' (Mã: {finalCode}) vừa được thêm.";
                    int adminRoleID = 5;
                    NotificationService.CreateNotification("NhanVienMoi", noiDung, adminRoleID, null, newEmployeeId);
                }
            }
            else // --- TRƯỜNG HỢP CẬP NHẬT ---
            {
                // Kiểm tra trùng mã NV (đề phòng trường hợp hiếm)
                if (EmployeeRepo.Instance.ExistsMaNhanVien(emp.MaNhanVien, id))
                {
                    MessageBox.Show(rm.GetString("Validation_EmployeeCodeExists", culture), rm.GetString("Validation_Title", culture), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(txtMatKhau.Text))
                {
                    emp.PasswordHash = employee.PasswordHash; // Giữ nguyên pass cũ
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

            // txtMaNV.PlaceholderText = rm.GetString("Placeholder_EmployeeCode", culture); // Ko cần placeholder nữa vì đã set tự động
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
                    if (c is Label) c.ForeColor = ThemeManager.TextColor;
                    if (c is CheckBox) c.ForeColor = ThemeManager.TextColor;
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
                case "admin": return rm.GetString("Role_Admin", culture);
                case "business": return rm.GetString("Role_Business", culture);
                case "field": return rm.GetString("Role_Field", culture);
                case "lab": return rm.GetString("Role_Lab", culture);
                case "plan": return rm.GetString("Role_Plan", culture);
                case "result": return rm.GetString("Role_Result", culture);
                default: return originalRoleName;
            }
        }
    }
}