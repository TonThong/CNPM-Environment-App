using System;
using System.Data;
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
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate required fields
                if (string.IsNullOrWhiteSpace(txtboxCustomerName.Text) ||
                    string.IsNullOrWhiteSpace(txtboxIDContract.Text) ||
                    string.IsNullOrWhiteSpace(txtboxContractType.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin bắt buộc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Prepare contract data
                string maDon = txtboxIDContract.Text.Trim();
                string tenKhachHang = txtboxCustomerName.Text.Trim();
                string soDienThoai = txtboxPhone.Text.Trim();
                string diaChi = txtboxAddress.Text.Trim();
                string nguoiDaiDien = txtboxOwner.Text.Trim();
                string loaiHopDong = txtboxContractType.Text.Trim();
                string maNhanVien = txtboxEmployee.Text.Trim();
                DateTime? ngayTraKetQua = null;

                if (DateTime.TryParse(txtboxEndDate.Text.Trim(), out DateTime parsedDate))
                {
                    ngayTraKetQua = parsedDate;
                }

                // Insert customer if not exists
                string insertCustomer = @"INSERT INTO Customers (TenDoanhNghiep, DiaChi, TenNguoiDaiDien, SoDienThoai)
                                          VALUES (@ten, @diaChi, @nguoiDaiDien, @soDienThoai) 
                                          ON DUPLICATE KEY UPDATE CustomerID=LAST_INSERT_ID(CustomerID);";
                DataProvider.Instance.ExecuteNonQuery(insertCustomer, new object[] { tenKhachHang, diaChi, nguoiDaiDien, soDienThoai });

                // Get CustomerID
                object customerIdObj = DataProvider.Instance.ExecuteScalar("SELECT LAST_INSERT_ID()", null);
                int customerId = customerIdObj != null && int.TryParse(customerIdObj.ToString(), out int cid) ? cid : 0;

                if (customerId == 0)
                {
                    MessageBox.Show("Không thể xác định khách hàng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Insert contract
                string insertContract = @"INSERT INTO Contracts (MaDon, NgayKy, NgayTraKetQua, ContractType, Status, CustomerID, EmployeeID)
                                          VALUES (@maDon, CURRENT_DATE, @ngayTra, @loaiHopDong, 'Pending', @customerId, 
                                                  (SELECT EmployeeID FROM Employees WHERE MaNhanVien = @maNhanVien LIMIT 1));";
                DataProvider.Instance.ExecuteNonQuery(insertContract, new object[] { maDon, ngayTraKetQua, loaiHopDong, customerId, maNhanVien });

                MessageBox.Show("Tạo hợp đồng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear fields
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
            txtboxContractType.Text = string.Empty;
            txtboxEmployee.Text = string.Empty;
            txtboxEndDate.Text = string.Empty;
        }
    }
}
