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

namespace Environmental_Monitoring.View
{
    public partial class Employee : UserControl
    {
        private int currentPage = 1;
        private int pageSize = 10;
        private int totalRecords = 0;
        private int totalPages = 0;
        public Employee()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.Employee_Load);
        }

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

        private void EditHeaderTitle()
        {
            ResourceManager rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(Employee).Assembly);
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;

            dgvEmployee.Columns["EmployeeID"].Visible = false;
            dgvEmployee.Columns["RoleID"].Visible = false;

            dgvEmployee.Columns["MaNhanVien"].HeaderText = rm.GetString("EmployeeID", culture);
            dgvEmployee.Columns["HoTen"].HeaderText = rm.GetString("FullName", culture);
            dgvEmployee.Columns["NamSinh"].HeaderText = rm.GetString("BirthYear", culture);
            dgvEmployee.Columns["PhongBan"].HeaderText = rm.GetString("Department", culture);
            dgvEmployee.Columns["DiaChi"].HeaderText = rm.GetString("Address", culture);
            dgvEmployee.Columns["SoDienThoai"].HeaderText = rm.GetString("Phone", culture);
            dgvEmployee.Columns["Email"].HeaderText = rm.GetString("Email", culture);
            dgvEmployee.Columns["RoleName"].HeaderText = rm.GetString("Role", culture);
        }

        private void AddActionColumns()
        {
            ResourceManager rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(Employee).Assembly);
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;

            if (dgvEmployee.Columns["colEdit"] == null)
            {
                DataGridViewImageColumn colEdit = new DataGridViewImageColumn
                {
                    Name = "colEdit",
                    HeaderText = rm.GetString("Edit", culture),
                    Image = Properties.Resources.edit,
                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                    DefaultCellStyle = { Padding = new Padding(5) },
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
                    DefaultCellStyle = { Padding = new Padding(5) },
                    Width = 40
                };
                dgvEmployee.Columns.Add(colDelete);
            }
            else
            {
                dgvEmployee.Columns["colDelete"].HeaderText = rm.GetString("Delete", culture);
            }
        }

        private void dgvEmployee_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvEmployee.Rows[e.RowIndex].IsNewRow) return;

            Mainlayout mainForm = this.ParentForm as Mainlayout;
            ResourceManager rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(Employee).Assembly);
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;

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
                    string errorMsg = rm.GetString("Alert_DeleteError_InUse", culture);
                    mainForm?.ShowGlobalAlert(errorMsg, AlertPanel.AlertType.Error);
                    return;
                }

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

        // === HÀM NÀY ĐÃ ĐƯỢC CẬP NHẬT ===
        private void Form_AddedEmployee(object sender, EventArgs e)
        {
            LoadData();

            Mainlayout mainForm = this.ParentForm as Mainlayout;
            ResourceManager rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(Employee).Assembly);
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;

            // Kiểm tra xem form gửi sự kiện là form nào
            bool isAdd = false;
            if (sender is CreateUpdateEmployee addEditForm)
            {
                isAdd = addEditForm.IsAddMode;
            }

            // Chọn chuỗi resource phù hợp
            string resourceKey = isAdd ? "Alert_AddEmployee_Success" : "Alert_AddEmployee_Edit";
            string successMsg = rm.GetString(resourceKey, culture);

            mainForm?.ShowGlobalAlert(successMsg, AlertPanel.AlertType.Success);
        }
        // ===================================

        private void Employee_Load(object sender, EventArgs e)
        {
            string savedLanguage = Properties.Settings.Default.Language;
            string cultureName = "vi";
            if (savedLanguage == "English")
            {
                cultureName = "en";
            }
            CultureInfo culture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            LoadData();

            UpdateUIText();

            btnFirst.Click -= btnFirst_Click;
            btnFirst.Click += btnFirst_Click;

            btnPrevious.Click -= btnPrevious_Click;
            btnPrevious.Click += btnPrevious_Click;

            btnNext.Click -= btnNext_Click;
            btnNext.Click += btnNext_Click;

            btnLast.Click -= btnLast_Click;
            btnLast.Click += btnLast_Click;
        }

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
                string keySearch = txtSearch.Text;
                PagedResult result = EmployeeRepo.Instance.GetEmployees(1, 1000, keySearch);
                dgvEmployee.DataSource = result.Data;
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