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
        /// Hàm khởi tạo (Constructor) cho UserControl.
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
        /// Xử lý các lỗi dữ liệu (DataError) của DataGridView một cách thầm lặng.
        /// </summary>
        private void dgvEmployee_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            string errorInfo = $"LỖI DATAERROR:\n" +
                               $"- Cột Index: {e.ColumnIndex}\n" +
                               $"- Tên Cột: {dgvEmployee.Columns[e.ColumnIndex].Name}\n" +
                               $"- Dòng Index: {e.RowIndex}\n" +
                               $"- Lỗi: {e.Exception.Message}";
            Console.WriteLine(errorInfo);
            e.ThrowException = false;
        }

        /// <summary>
        /// Xử lý phím bấm (ví dụ: nhấn Enter trong ô tìm kiếm).
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
        /// Tải dữ liệu nhân viên lên DataGridView theo trang và từ khóa tìm kiếm.
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
        /// Cập nhật tiêu đề các cột trong DataGridView theo ngôn ngữ hiện tại.
        /// </summary>
        private void EditHeaderTitle()
        {
            ResourceManager rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(Employee).Assembly);
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;

            if (dgvEmployee.Columns.Contains("EmployeeID")) dgvEmployee.Columns["EmployeeID"].Visible = false;
            if (dgvEmployee.Columns.Contains("RoleID")) dgvEmployee.Columns["RoleID"].Visible = false;

            if (dgvEmployee.Columns.Contains("MaNhanVien")) dgvEmployee.Columns["MaNhanVien"].HeaderText = rm.GetString("EmployeeID", culture);
            if (dgvEmployee.Columns.Contains("HoTen")) dgvEmployee.Columns["HoTen"].HeaderText = rm.GetString("FullName", culture);
            if (dgvEmployee.Columns.Contains("NamSinh")) dgvEmployee.Columns["NamSinh"].HeaderText = rm.GetString("BirthYear", culture);

            string headerDeptHead = rm.GetString("IsDepartmentHead", culture);
            if (dgvEmployee.Columns.Contains("TruongBoPhan"))
                dgvEmployee.Columns["TruongBoPhan"].HeaderText = (headerDeptHead == "IsDepartmentHead") ? "Manager" : headerDeptHead;

            if (dgvEmployee.Columns.Contains("DiaChi")) dgvEmployee.Columns["DiaChi"].HeaderText = rm.GetString("Address", culture);
            if (dgvEmployee.Columns.Contains("SoDienThoai")) dgvEmployee.Columns["SoDienThoai"].HeaderText = rm.GetString("Phone", culture);
            if (dgvEmployee.Columns.Contains("Email")) dgvEmployee.Columns["Email"].HeaderText = rm.GetString("Email", culture);
            if (dgvEmployee.Columns.Contains("RoleName")) dgvEmployee.Columns["RoleName"].HeaderText = rm.GetString("Role", culture);
        }

        /// <summary>
        /// Thêm các cột hành động (Sửa, Xóa) bằng hình ảnh vào DataGridView.
        /// </summary>
        private void AddActionColumns()
        {
            ResourceManager rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(Employee).Assembly);
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;

            Color defaultBackColor = dgvEmployee.DefaultCellStyle.BackColor;

            if (dgvEmployee.Columns["colEdit"] == null)
            {
                DataGridViewImageColumn colEdit = new DataGridViewImageColumn
                {
                    Name = "colEdit",
                    HeaderText = rm.GetString("Edit", culture),
                    Image = Properties.Resources.edit,
                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                    DefaultCellStyle = {
                        Padding = new Padding(5),
                        SelectionBackColor = defaultBackColor
                    },
                    Width = 40
                };
                dgvEmployee.Columns.Add(colEdit);
            }
            else
            {
                dgvEmployee.Columns["colEdit"].HeaderText = rm.GetString("Edit", culture);
            }

            if (dgvEmployee.Columns["colDelete"] == null)
            {
                DataGridViewImageColumn colDelete = new DataGridViewImageColumn
                {
                    Name = "colDelete",
                    HeaderText = rm.GetString("Delete", culture),
                    Image = Properties.Resources.delete,
                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                    DefaultCellStyle = {
                        Padding = new Padding(5),
                        SelectionBackColor = defaultBackColor
                    },
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
        /// Xử lý sự kiện khi người dùng nhấp vào một ô trong DataGridView (để Sửa hoặc Xóa).
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
                Console.WriteLine("Lỗi CellContentClick: Không thể lấy EmployeeId tại dòng " + e.RowIndex);
                return;
            }

            if (columnName == "colEdit")
            {
                CreateUpdateEmployee form = new CreateUpdateEmployee(employeeId);
                form.DataAdded += Form_AddedEmployee;
                form.ShowDialog();
            }
            else if (columnName == "colDelete")
            {
                // --- CẬP NHẬT LOGIC KIỂM TRA XÓA ---
                UsageDetails usage = EmployeeRepo.Instance.GetEmployeeUsageDetails(employeeId);

                if (usage != null)
                {
                    string msg = "";

                    // Hiển thị thông báo tổng quan dựa trên Key trả về từ EmployeeRepo
                    if (usage.ReasonKey == "Usage_ContractCount")
                    {
                        msg = culture.Name == "vi-VN"
                            ? $"Không thể xóa. Nhân viên này đang phụ trách {usage.Value} hợp đồng."
                            : $"Cannot delete. This employee is in charge of {usage.Value} contracts.";
                    }
                    else if (usage.ReasonKey == "Usage_SampleHTCount")
                    {
                        msg = culture.Name == "vi-VN"
                            ? $"Không thể xóa. Nhân viên này đang lấy mẫu cho {usage.Value} mẫu hiện trường."
                            : $"Cannot delete. This employee is assigned to {usage.Value} field samples.";
                    }
                    else if (usage.ReasonKey == "Usage_SamplePTNCount")
                    {
                        msg = culture.Name == "vi-VN"
                            ? $"Không thể xóa. Nhân viên này đang phân tích {usage.Value} mẫu thí nghiệm."
                            : $"Cannot delete. This employee is analyzing {usage.Value} lab samples.";
                    }
                    else
                    {
                        // Fallback cho các trường hợp khác (nếu có)
                        msg = $"Cannot delete. Data in use (Code: {usage.ReasonKey}, Val: {usage.Value})";
                    }

                    mainForm?.ShowGlobalAlert(msg, AlertPanel.AlertType.Error);
                    return;
                }

                // Nếu không bị ràng buộc -> Xác nhận xóa
                string code = dgvEmployee.Rows[e.RowIndex].Cells["MaNhanVien"].Value.ToString();
                string confirmTitle = rm.GetString("Alert_DeleteConfirm_Title", culture);
                string confirmMsg = string.Format(rm.GetString("Alert_DeleteConfirm_Message", culture), code);

                DialogResult result = MessageBox.Show(
                    confirmMsg,
                    confirmTitle,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    EmployeeRepo.Instance.DeleteEmployee(employeeId);

                    string successMsg = rm.GetString("Alert_DeleteSuccess", culture);
                    mainForm?.ShowGlobalAlert(successMsg, AlertPanel.AlertType.Success);

                    LoadData();
                }
            }
        }

        /// <summary>
        /// --- THÊM: Xử lý sự kiện rê chuột để đổi con trỏ thành Hand ---
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
        /// Cập nhật trạng thái (Enabled/Disabled) của các nút phân trang.
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
        /// Xử lý sự kiện sau khi Form CreateUpdateEmployee được đóng (Thêm mới hoặc Cập nhật).
        /// </summary>
        private void Form_AddedEmployee(object sender, EventArgs e)
        {
            LoadData();

            Mainlayout mainForm = this.ParentForm as Mainlayout;
            ResourceManager rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(Employee).Assembly);
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;

            bool isAdd = false;
            if (sender is CreateUpdateEmployee addEditForm)
            {
                isAdd = addEditForm.IsAddMode;
            }

            string resourceKey = isAdd ? "Alert_AddEmployee_Success" : "Alert_AddEmployee_Edit";
            string successMsg = rm.GetString(resourceKey, culture);

            mainForm?.ShowGlobalAlert(successMsg, AlertPanel.AlertType.Success);
        }

        /// <summary>
        /// Xử lý khi UserControl được tải, gán các sự kiện ban đầu cho các nút.
        /// </summary>
        private void Employee_Load(object sender, EventArgs e)
        {
            LoadData();
            UpdateUIText();

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
        /// Cập nhật lại tất cả văn bản và màu sắc (Theme) trên UI.
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

                dgvEmployee.DefaultCellStyle.BackColor = ThemeManager.PanelColor;
                dgvEmployee.DefaultCellStyle.ForeColor = ThemeManager.TextColor;
                dgvEmployee.DefaultCellStyle.SelectionBackColor = ThemeManager.AccentColor;
                dgvEmployee.DefaultCellStyle.SelectionForeColor = Color.White;

                dgvEmployee.ColumnHeadersDefaultCellStyle.BackColor = ThemeManager.SecondaryPanelColor;
                dgvEmployee.ColumnHeadersDefaultCellStyle.ForeColor = ThemeManager.TextColor;
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút 'Thêm Nhân Viên'.
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            CreateUpdateEmployee form = new CreateUpdateEmployee();
            form.DataAdded += Form_AddedEmployee;
            form.ShowDialog();
        }

        /// <summary>
        /// (Hàm rỗng) Xử lý sự kiện KeyDown của ô tìm kiếm.
        /// </summary>
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
        }

        /// <summary>
        /// (Hàm rỗng) Xử lý sự kiện KeyPress của ô tìm kiếm.
        /// </summary>
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        /// <summary>
        /// (Hàm rỗng) Xử lý sự kiện TextChanged của ô tìm kiếm.
        /// </summary>
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Xử lý sự kiện nhấn nút 'Trang Đầu'.
        /// </summary>
        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage = 1;
                LoadData();
            }
        }

        /// <summary>
        /// Xử lý sự kiện nhấn nút 'Trang Trước'.
        /// </summary>
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadData();
            }
        }

        /// <summary>
        /// Xử lý sự kiện nhấn nút 'Trang Sau'.
        /// </summary>
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                LoadData();
            }
        }

        /// <summary>
        /// Xử lý sự kiện nhấn nút 'Trang Cuối'.
        /// </summary>
        private void btnLast_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage = totalPages;
                LoadData();
            }
        }

        /// <summary>
        /// Định dạng lại ô (Cell) trong DataGridView (ví dụ: hiển thị dấu tick '✓').
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

                        if (isHead)
                        {
                            e.Value = "✓";
                        }
                        else
                        {
                            e.Value = "";
                        }

                        e.FormattingApplied = true;
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    catch (FormatException)
                    {
                        e.Value = "";
                        e.FormattingApplied = true;
                    }
                }
                else
                {
                    e.Value = "";
                    e.FormattingApplied = true;
                }
            }
        }
    }
}