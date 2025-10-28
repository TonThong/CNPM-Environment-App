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

namespace Environmental_Monitoring.View
{
    public partial class Employee : UserControl
    {
        private int currentPage = 1;
        private int pageSize = 10; // Hiển thị 15 dòng mỗi trang
        private int totalRecords = 0;
        private int totalPages = 0;
        public Employee()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            //dgvEmployee.DataSource = EmployeeRepo.Instance.GetAll();
            string keySearch = txbSearch.Text;
            PagedResult result = EmployeeRepo.Instance.GetEmployees(currentPage, pageSize, keySearch);

            // Cập nhật trạng thái
            totalRecords = result.TotalCount;
            totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            if (totalPages == 0) totalPages = 1; // Đảm bảo luôn có ít nhất 1 trang

            // Gán dữ liệu
            dgvEmployee.DataSource = result.Data;

            EditHeaderTitle();
            AddActionColumns();
            UpdatePaginationControls();
        }

        private void EditHeaderTitle()
        {
            // ẩn cột
            dgvEmployee.Columns["EmployeeID"].Visible = false;
            dgvEmployee.Columns["RoleID"].Visible = false;

            dgvEmployee.Columns["MaNhanVien"].HeaderText = "Mã nhân viên";
            dgvEmployee.Columns["HoTen"].HeaderText = "Họ tên";
            dgvEmployee.Columns["NamSinh"].HeaderText = "Năm sinh";
            dgvEmployee.Columns["PhongBan"].HeaderText = "Phòng ban";
            dgvEmployee.Columns["DiaChi"].HeaderText = "Địa chỉ";
            dgvEmployee.Columns["SoDienThoai"].HeaderText = "Điện thoại";
            dgvEmployee.Columns["Email"].HeaderText = "Email";
            dgvEmployee.Columns["RoleName"].HeaderText = "Vai trò";
        }

        private void AddActionColumns()
        {
            DataGridViewImageColumn colEdit = new DataGridViewImageColumn();
            colEdit.Name = "colEdit";
            colEdit.HeaderText = "Sửa";
            colEdit.Image = Properties.Resources.edit;
            colEdit.ImageLayout = DataGridViewImageCellLayout.Zoom;

            colEdit.DefaultCellStyle.Padding = new Padding(5);

            DataGridViewImageColumn colDelete = new DataGridViewImageColumn();
            colDelete.Name = "colDelete";
            colDelete.HeaderText = "Xóa";
            colDelete.Image = Properties.Resources.delete;
            colDelete.ImageLayout = DataGridViewImageCellLayout.Zoom;

            colDelete.DefaultCellStyle.Padding = new Padding(5);

            if (dgvEmployee.Columns["colEdit"] == null)
            {
                dgvEmployee.Columns.Add(colEdit);
            }
            if (dgvEmployee.Columns["colDelete"] == null)
            {
                dgvEmployee.Columns.Add(colDelete);
            }

            dgvEmployee.Columns["colEdit"].Width = 40;
            dgvEmployee.Columns["colDelete"].Width = 40;
        }

        private void dgvEmployee_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvEmployee.Rows[e.RowIndex].IsNewRow) return;

            string columnName = dgvEmployee.Columns[e.ColumnIndex].Name;
            int employeeId = Convert.ToInt32(dgvEmployee.Rows[e.RowIndex].Cells["EmployeeId"].Value);

            if (columnName == "colEdit")
            {
                CreateUpdateEmployee form = new CreateUpdateEmployee(employeeId);
                form.DataAdded += Form_AddedEmployee;
                form.ShowDialog();
            }
            else if (columnName == "colDelete")
            {
                if (EmployeeRepo.Instance.ExistsAnotherTable(employeeId))
                {
                    MessageBox.Show("Không thể xóa!!! Dữ liệu này đã được sử dụng ở bên bảng khác.");
                    return;
                }
                string code = dgvEmployee.Rows[e.RowIndex].Cells["MaNhanVien"].Value.ToString();
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn XÓA nhân viên có Mã: " + code + "?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    EmployeeRepo.Instance.DeleteEmployee(employeeId);
                    MessageBox.Show("Xóa nhân viên thành công.");
                    LoadData();
                }
            }
        }

        private void UpdatePaginationControls()
        {
            // Cập nhật nhãn
            lblPageInfo.Text = $"Page {currentPage} / {totalPages}";

            // Bật/tắt các nút
            btnFirst.Enabled = (currentPage > 1);
            btnPrevious.Enabled = (currentPage > 1);
            btnNext.Enabled = (currentPage < totalPages);
            btnLast.Enabled = (currentPage < totalPages);
        }

        private void Form_AddedEmployee(object sender, EventArgs e)
        {
            LoadData();
        }

        private void Employee_Load(object sender, EventArgs e)
        {
            LoadData();
            btnFirst.Click += btnFirst_Click;
            btnPrevious.Click += btnPrevious_Click;
            btnNext.Click += btnNext_Click;
            btnLast.Click += btnLast_Click;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CreateUpdateEmployee form = new CreateUpdateEmployee();
            form.DataAdded += Form_AddedEmployee;
            form.ShowDialog();
        }

        private void txbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string keySearch = txbSearch.Text;
                DataTable result = EmployeeRepo.Instance.Filter(keySearch);
                dgvEmployee.DataSource = result;
                e.SuppressKeyPress = true;
            }
        }

        private void txbSearch_TextChanged(object sender, EventArgs e)
        {
            currentPage = 1;
            LoadData();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage = 1;
                LoadData();
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadData();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                LoadData();
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage = totalPages;
                LoadData();
            }
        }
    }
}
