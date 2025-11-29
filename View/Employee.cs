using Environmental_Monitoring.Controller.Data;
using Environmental_Monitoring.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;
using System.Globalization;
using System.Resources;
using Environmental_Monitoring.View.Components;
using Environmental_Monitoring.View;
using Environmental_Monitoring.Controller;

namespace Environmental_Monitoring.View
{
    public partial class Employee : UserControl
    {
        private int currentPage = 1;
        private int pageSize = 10;
        private int totalRecords = 0;
        private int totalPages = 0;

        /// <summary>
        /// Khởi tạo UserControl, đăng ký các sự kiện cơ bản (Load, Formatting, Error, MouseEnter).
        /// </summary>
        public Employee()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.Employee_Load);
            this.dgvEmployee.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvEmployee_CellFormatting);
            this.dgvEmployee.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvEmployee_DataError);
            this.dgvEmployee.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmployee_CellMouseEnter);
        }

        /// <summary>
        /// Ngăn chặn ứng dụng bị crash khi DataGridView gặp lỗi dữ liệu (DataError).
        /// </summary>
        private void dgvEmployee_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        /// <summary>
        /// Bắt sự kiện nhấn phím Enter khi đang focus ở ô tìm kiếm để tải lại dữ liệu.
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if ((keyData == Keys.Enter) && (this.ActiveControl == txtSearch))
            {
                currentPage = 1;
                LoadData();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Lấy dữ liệu từ database theo phân trang và từ khóa, sau đó đổ vào DataGridView.
        /// </summary>
        private void LoadData()
        {
            string keySearch = txtSearch.Text;
            PagedResult result = EmployeeRepo.Instance.GetEmployees(currentPage, pageSize, keySearch);

            totalRecords = result.TotalCount;
            totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            if (totalPages == 0) totalPages = 1;

            dgvEmployee.DataSource = result.Data;

            EditHeaderTitle();
            AddActionColumns();
            UpdatePaginationControls();
        }

        /// <summary>
        /// Cấu hình tiêu đề cột (đa ngôn ngữ), ẩn cột ID, và điều chỉnh kích thước cột.
        /// </summary>
        private void EditHeaderTitle()
        {
            ResourceManager rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(Employee).Assembly);
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;

            // Ẩn cột ID hệ thống
            if (dgvEmployee.Columns.Contains("EmployeeID")) dgvEmployee.Columns["EmployeeID"].Visible = false;
            if (dgvEmployee.Columns.Contains("RoleID")) dgvEmployee.Columns["RoleID"].Visible = false;

            // 1. Mã Nhân Viên
            if (dgvEmployee.Columns.Contains("MaNhanVien"))
            {
                dgvEmployee.Columns["MaNhanVien"].HeaderText = rm.GetString("EmployeeID", culture);
                dgvEmployee.Columns["MaNhanVien"].Width = 100;
            }

            // 2. Họ Tên (Giãn tự động)
            if (dgvEmployee.Columns.Contains("HoTen"))
            {
                dgvEmployee.Columns["HoTen"].HeaderText = rm.GetString("FullName", culture);
                dgvEmployee.Columns["HoTen"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvEmployee.Columns["HoTen"].MinimumWidth = 200;
            }

            // 3. Năm Sinh
            if (dgvEmployee.Columns.Contains("NamSinh"))
            {
                dgvEmployee.Columns["NamSinh"].HeaderText = rm.GetString("BirthYear", culture);
                dgvEmployee.Columns["NamSinh"].Width = 90;
            }

            // 4. Trưởng bộ phận
            string headerDeptHead = rm.GetString("IsDepartmentHead", culture);
            if (dgvEmployee.Columns.Contains("TruongBoPhan"))
            {
                dgvEmployee.Columns["TruongBoPhan"].HeaderText = (headerDeptHead == "IsDepartmentHead") ? "Manager" : headerDeptHead;
                dgvEmployee.Columns["TruongBoPhan"].Width = 80;
            }

            // 5. Địa chỉ
            if (dgvEmployee.Columns.Contains("DiaChi"))
            {
                dgvEmployee.Columns["DiaChi"].HeaderText = rm.GetString("Address", culture);
                dgvEmployee.Columns["DiaChi"].Width = 120;
            }

            // 6. Số điện thoại
            if (dgvEmployee.Columns.Contains("SoDienThoai"))
            {
                dgvEmployee.Columns["SoDienThoai"].HeaderText = rm.GetString("Phone", culture);
                dgvEmployee.Columns["SoDienThoai"].Width = 100;
            }

            // 7. Email
            if (dgvEmployee.Columns.Contains("Email"))
            {
                dgvEmployee.Columns["Email"].HeaderText = rm.GetString("Email", culture);
                dgvEmployee.Columns["Email"].Width = 180;
            }

            // 8. Vai trò
            if (dgvEmployee.Columns.Contains("RoleName"))
            {
                dgvEmployee.Columns["RoleName"].HeaderText = rm.GetString("Role", culture);
                dgvEmployee.Columns["RoleName"].Width = 90;
            }
        }

        /// <summary>
        /// Thêm hoặc cập nhật tiêu đề cho các cột nút bấm hành động (Sửa, Xóa).
        /// </summary>
        private void AddActionColumns()
        {
            ResourceManager rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(Employee).Assembly);
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;

            Color defaultBackColor = dgvEmployee.DefaultCellStyle.BackColor;

            // Cột Sửa
            if (dgvEmployee.Columns["colEdit"] == null)
            {
                DataGridViewImageColumn colEdit = new DataGridViewImageColumn
                {
                    Name = "colEdit",
                    HeaderText = rm.GetString("Edit", culture),
                    Image = Properties.Resources.edit,
                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                    DefaultCellStyle = { Padding = new Padding(5), SelectionBackColor = defaultBackColor },
                    Width = 40
                };
                dgvEmployee.Columns.Add(colEdit);
            }
            else
            {
                dgvEmployee.Columns["colEdit"].HeaderText = rm.GetString("Edit", culture);
            }

            // Cột Xóa
            if (dgvEmployee.Columns["colDelete"] == null)
            {
                DataGridViewImageColumn colDelete = new DataGridViewImageColumn
                {
                    Name = "colDelete",
                    HeaderText = rm.GetString("Delete", culture),
                    Image = Properties.Resources.delete,
                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                    DefaultCellStyle = { Padding = new Padding(5), SelectionBackColor = defaultBackColor },
                    Width = 40
                };
                dgvEmployee.Columns.Add(colDelete);
            }
            else
            {
                dgvEmployee.Columns["colDelete"].HeaderText = rm.GetString("Delete", culture);
            }
        }

        /// <summary>
        /// Xử lý logic khi người dùng click vào nút Sửa hoặc Xóa trên lưới (bao gồm kiểm tra ràng buộc trước khi xóa).
        /// </summary>
        private void dgvEmployee_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvEmployee.Rows[e.RowIndex].IsNewRow) return;

            Mainlayout mainForm = this.ParentForm as Mainlayout;
            ResourceManager rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(Employee).Assembly);
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;

            string columnName = dgvEmployee.Columns[e.ColumnIndex].Name;
            object idValue = dgvEmployee.Rows[e.RowIndex].Cells["EmployeeId"].Value;

            if (idValue == DBNull.Value || !int.TryParse(idValue.ToString(), out int employeeId))
            {
                return;
            }

            // Logic Sửa
            if (columnName == "colEdit")
            {
                CreateUpdateEmployee form = new CreateUpdateEmployee(employeeId);
                form.DataAdded += Form_AddedEmployee;
                form.ShowDialog();
            }
            // Logic Xóa
            else if (columnName == "colDelete")
            {
                UsageDetails usage = EmployeeRepo.Instance.GetEmployeeUsageDetails(employeeId);

                // Kiểm tra ràng buộc dữ liệu
                if (usage != null)
                {
                    string msg = "";
                    if (usage.ReasonKey == "Usage_ContractCount")
                        msg = culture.Name == "vi-VN" ? $"Không thể xóa. Nhân viên đang phụ trách {usage.Value} hợp đồng." : $"Cannot delete. Handling {usage.Value} contracts.";
                    else if (usage.ReasonKey == "Usage_SampleHTCount")
                        msg = culture.Name == "vi-VN" ? $"Không thể xóa. Đang lấy mẫu {usage.Value} mẫu hiện trường." : $"Cannot delete. Assigned to {usage.Value} field samples.";
                    else if (usage.ReasonKey == "Usage_SamplePTNCount")
                        msg = culture.Name == "vi-VN" ? $"Không thể xóa. Đang phân tích {usage.Value} mẫu thí nghiệm." : $"Cannot delete. Analyzing {usage.Value} lab samples.";
                    else
                        msg = $"Cannot delete. Data in use ({usage.ReasonKey})";

                    mainForm?.ShowGlobalAlert(msg, AlertPanel.AlertType.Error);
                    return;
                }

                // Xác nhận xóa
                string code = dgvEmployee.Rows[e.RowIndex].Cells["MaNhanVien"].Value.ToString();
                string confirmTitle = rm.GetString("Alert_DeleteConfirm_Title", culture);
                string confirmMsg = string.Format(rm.GetString("Alert_DeleteConfirm_Message", culture), code);

                if (MessageBox.Show(confirmMsg, confirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    EmployeeRepo.Instance.DeleteEmployee(employeeId);
                    mainForm?.ShowGlobalAlert(rm.GetString("Alert_DeleteSuccess", culture), AlertPanel.AlertType.Success);
                    LoadData();
                }
            }
        }

        /// <summary>
        /// Thay đổi con trỏ chuột thành hình bàn tay khi rê vào nút Sửa/Xóa.
        /// </summary>
        private void dgvEmployee_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                dgvEmployee.Cursor = Cursors.Default;
                return;
            }

            string columnName = dgvEmployee.Columns[e.ColumnIndex].Name;
            if (columnName == "colEdit" || columnName == "colDelete")
            {
                dgvEmployee.Cursor = Cursors.Hand;
            }
            else
            {
                dgvEmployee.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Cập nhật trạng thái hiển thị (Enabled) của các nút điều hướng phân trang.
        /// </summary>
        private void UpdatePaginationControls()
        {
            ResourceManager rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(Employee).Assembly);
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;
            string pageText = rm.GetString("Pagination_Page", culture);

            lblPageInfo.Text = $"{pageText} {currentPage} / {totalPages}";

            btnFirst.Enabled = (currentPage > 1);
            btnPrevious.Enabled = (currentPage > 1);
            btnNext.Enabled = (currentPage < totalPages);
            btnLast.Enabled = (currentPage < totalPages);
        }

        /// <summary>
        /// Callback chạy sau khi form Thêm/Sửa đóng lại để tải lại dữ liệu và hiện thông báo.
        /// </summary>
        private void Form_AddedEmployee(object sender, EventArgs e)
        {
            LoadData();
            Mainlayout mainForm = this.ParentForm as Mainlayout;
            ResourceManager rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(Employee).Assembly);
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;

            bool isAdd = (sender is CreateUpdateEmployee addEditForm) && addEditForm.IsAddMode;
            string resourceKey = isAdd ? "Alert_AddEmployee_Success" : "Alert_AddEmployee_Edit";

            mainForm?.ShowGlobalAlert(rm.GetString(resourceKey, culture), AlertPanel.AlertType.Success);
        }

        /// <summary>
        /// Sự kiện Load của UserControl: Tải dữ liệu, cập nhật ngôn ngữ và gán sự kiện click.
        /// </summary>
        private void Employee_Load(object sender, EventArgs e)
        {
            LoadData();
            UpdateUIText();

            // Đăng ký lại sự kiện để tránh duplicate
            btnAdd.Click -= btnAdd_Click;
            btnAdd.Click += btnAdd_Click;

            dgvEmployee.CellContentClick -= dgvEmployee_CellContentClick;
            dgvEmployee.CellContentClick += dgvEmployee_CellContentClick;

            btnFirst.Click -= btnFirst_Click;
            btnFirst.Click += btnFirst_Click;

            btnPrevious.Click -= btnPrevious_Click;
            btnPrevious.Click += btnPrevious_Click;

            btnNext.Click -= btnNext_Click;
            btnNext.Click += btnNext_Click;

            btnLast.Click -= btnLast_Click;
            btnLast.Click += btnLast_Click;

            dgvEmployee.ReadOnly = true;
        }

        /// <summary>
        /// Cập nhật toàn bộ ngôn ngữ và giao diện (Theme) cho trang.
        /// [UPDATE]: Đã cập nhật Header style (Bold, Center, Nền #629EFF).
        /// </summary>
        public void UpdateUIText()
        {
            ResourceManager rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(Employee).Assembly);
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;

            lblTitle.Text = rm.GetString("Employee_Title", culture);
            btnAdd.Text = rm.GetString("Employee_AddButton", culture);
            txtSearch.PlaceholderText = rm.GetString("Search_Placeholder", culture);

            EditHeaderTitle();
            AddActionColumns();
            UpdatePaginationControls();

            try
            {
                // Áp dụng Theme
                this.BackColor = ThemeManager.BackgroundColor;
                lblTitle.ForeColor = ThemeManager.TextColor;
                lblPageInfo.ForeColor = ThemeManager.TextColor;
                txtSearch.BackColor = ThemeManager.PanelColor;
                txtSearch.ForeColor = ThemeManager.TextColor;
                btnAdd.BackColor = ThemeManager.AccentColor;
                btnAdd.ForeColor = Color.White;

                dgvEmployee.BackgroundColor = ThemeManager.PanelColor;
                dgvEmployee.GridColor = ThemeManager.BorderColor;
                dgvEmployee.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                dgvEmployee.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

                // --- CẤU HÌNH HEADER 
                // 1. Tắt Visual Styles để dùng màu nền tùy chỉnh
                dgvEmployee.EnableHeadersVisualStyles = false;

                // 2. Nền màu #629EFF
                dgvEmployee.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#629EFF");

                // 3. Chữ màu trắng (cho nổi trên nền xanh)
                dgvEmployee.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;

                // 4. In đậm toàn bộ & Căn giữa
                dgvEmployee.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                dgvEmployee.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                // --------------------------------------

                dgvEmployee.DefaultCellStyle.BackColor = ThemeManager.PanelColor;
                dgvEmployee.DefaultCellStyle.ForeColor = ThemeManager.TextColor;
                dgvEmployee.DefaultCellStyle.SelectionBackColor = ThemeManager.AccentColor;
                dgvEmployee.DefaultCellStyle.SelectionForeColor = Color.White;
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Mở form thêm mới nhân viên.
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            CreateUpdateEmployee form = new CreateUpdateEmployee();
            form.DataAdded += Form_AddedEmployee;
            form.ShowDialog();
        }

        // Các hàm sự kiện chưa sử dụng
        private void txtSearch_KeyDown(object sender, KeyEventArgs e) { }
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e) { }
        private void txtSearch_TextChanged(object sender, EventArgs e) { }

        // Các hàm xử lý sự kiện nút phân trang
        private void btnFirst_Click(object sender, EventArgs e) { if (currentPage > 1) { currentPage = 1; LoadData(); } }
        private void btnPrevious_Click(object sender, EventArgs e) { if (currentPage > 1) { currentPage--; LoadData(); } }
        private void btnNext_Click(object sender, EventArgs e) { if (currentPage < totalPages) { currentPage++; LoadData(); } }
        private void btnLast_Click(object sender, EventArgs e) { if (currentPage < totalPages) { currentPage = totalPages; LoadData(); } }

        /// <summary>
        /// Định dạng hiển thị dữ liệu cell (ví dụ: chuyển true/false thành dấu tích).
        /// </summary>
        private void dgvEmployee_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dgvEmployee.Columns[e.ColumnIndex].DataPropertyName == "TruongBoPhan")
            {
                if (e.Value != null && e.Value != DBNull.Value)
                {
                    try
                    {
                        bool isHead = Convert.ToBoolean(e.Value);
                        e.Value = isHead ? "✓" : "";
                        e.FormattingApplied = true;
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    catch (FormatException) { e.Value = ""; e.FormattingApplied = true; }
                }
                else { e.Value = ""; e.FormattingApplied = true; }
            }
        }
    }
}