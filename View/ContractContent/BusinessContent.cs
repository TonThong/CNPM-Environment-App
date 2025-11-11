using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Environmental_Monitoring.Controller;

namespace Environmental_Monitoring.View.ContractContent
{
    public partial class BusinessContent : UserControl
    {
        public BusinessContent()
        {
            InitializeComponent();
            btnSave.Click += BtnSave_Click;
            LoadEmployees();

            cmbContractType.Items.Add("Quy");
            cmbContractType.Items.Add("6 Thang");

            if (this.Controls.Find("cmbContractType", true).Length > 0)
            {
                cmbContractType.SelectedIndex = 0;
            }
        }

        private void LoadEmployees()
        {
            try
            {
                string query = "SELECT HoTen FROM Employees";
                DataTable dt = DataProvider.Instance.ExecuteQuery(query);

                txtboxEmployee.Text = UserSession.CurrentUser.HoTen;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtboxCustomerName.Text) ||
                    (cmbContractType == null || string.IsNullOrWhiteSpace(cmbContractType.Text)) ||
                    string.IsNullOrWhiteSpace(txtboxEmployee.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin bắt buộc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maDon = txtboxIDContract.Text.Trim();
                string tenKhachHang = txtboxCustomerName.Text.Trim();
                string kyHieuDoanhNghiep = txtboxPhone.Text.Trim();
                string diaChi = txtboxAddress.Text.Trim();
                string nguoiDaiDien = txtboxOwner.Text.Trim();
                string loaiHopDong = cmbContractType != null ? cmbContractType.Text : string.Empty;
                string employeeName = txtboxEmployee.Text.Trim();
                DateTime? ngayTraKetQua = null;

                if (DateTime.TryParse(txtboxEndDate.Text.Trim(), out DateTime parsedDate))
                {
                    ngayTraKetQua = parsedDate;
                }

                string insertCustomer = @"INSERT INTO Customers (TenDoanhNghiep, KyHieuDoanhNghiep, DiaChi, TenNguoiDaiDien)
                                          VALUES (@ten, @kyhieu, @diaChi, @nguoiDaiDien) 
                                          ON DUPLICATE KEY UPDATE CustomerID=LAST_INSERT_ID(CustomerID);";
                DataProvider.Instance.ExecuteNonQuery(insertCustomer, new object[] { tenKhachHang, kyHieuDoanhNghiep, diaChi, nguoiDaiDien });

                object customerIdObj = DataProvider.Instance.ExecuteScalar("SELECT LAST_INSERT_ID()", null);
                int customerId = customerIdObj != null && int.TryParse(customerIdObj.ToString(), out int cid) ? cid : 0;

                if (customerId == 0)
                {
                    MessageBox.Show("Không thể xác định khách hàng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string insertContract = @"INSERT INTO Contracts (MaDon, NgayKy, NgayTraKetQua, ContractType, Status, CustomerID, EmployeeID)
                                          VALUES (@maDon, CURRENT_DATE, @ngayTra, @loaiHopDong, @status, @customerId, 
                                                  (SELECT EmployeeID FROM Employees WHERE HoTen = @employeeName LIMIT 1));";
                DataProvider.Instance.ExecuteNonQuery(insertContract, new object[] { maDon, ngayTraKetQua, loaiHopDong, "Active", customerId, employeeName });

                MessageBox.Show("Tạo hợp đồng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            txtboxCustomerName.Text = string.Empty;
            txtboxIDContract.Text = string.Empty;
            txtboxPhone.Text = string.Empty;
            txtboxAddress.Text = string.Empty;
            txtboxOwner.Text = string.Empty;
            if (cmbContractType != null) cmbContractType.SelectedIndex = -1;
            txtboxEmployee.Text = string.Empty;
            txtboxEndDate.Text = string.Empty;
        }
    }
}
