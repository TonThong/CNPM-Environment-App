using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Environmental_Monitoring.Controller;
using System.Resources;
using System.Globalization;
using System.Threading;
using Environmental_Monitoring.View.Components;
using Environmental_Monitoring.Controller.Data;

namespace Environmental_Monitoring.View.ContractContent
{
    public partial class BusinessContent : UserControl
    {
        private ResourceManager rm;
        private CultureInfo culture;

        public BusinessContent()
        {
            InitializeComponent();
            InitializeLocalization();
            btnSave.Click += BtnSave_Click;

            culture = Thread.CurrentThread.CurrentUICulture;
            cmbContractType.Items.Clear();
            cmbContractType.Items.Add(rm.GetString("ContractType_Quarterly", culture));
            cmbContractType.Items.Add(rm.GetString("ContractType_Semiannual", culture));

            if (this.Controls.Find("cmbContractType", true).Length > 0)
            {
                cmbContractType.SelectedIndex = 0;
            }

            ClearFields();

            this.Load += BusinessContent_Load;
        }

        private void BusinessContent_Load(object sender, EventArgs e)
        {
            LoadEmployees();
            UpdateUIText();
        }

        private void InitializeLocalization()
        {
            rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(Contract).Assembly);
            culture = Thread.CurrentThread.CurrentUICulture;
        }

        public void UpdateUIText()
        {
            culture = Thread.CurrentThread.CurrentUICulture;

            lblTitle.Text = rm.GetString("Business_Title", culture);
            lblCustomer.Text = rm.GetString("Business_Customer", culture);
            lblContract.Text = rm.GetString("Business_Contract", culture);

            txtboxCustomerName.PlaceholderText = rm.GetString("Business_CustomerName_Placeholder", culture);
            txtboxPhone.PlaceholderText = rm.GetString("Business_CustomerSymbol_Placeholder", culture);
            txtboxAddress.PlaceholderText = rm.GetString("Business_Address_Placeholder", culture);
            txtboxOwner.PlaceholderText = rm.GetString("Business_Representative_Placeholder", culture);
            txtEmailCustomer.PlaceholderText = rm.GetString("Business_EmailCustomer_Placeholder", culture);

            txtboxIDContract.PlaceholderText = rm.GetString("Business_ContractID_Placeholder", culture);

            txtboxEmployee.PlaceholderText = rm.GetString("Business_Employee_Placeholder", culture);

            btnSave.Text = rm.GetString("Button_Save", culture);

            if (btnCancel != null)
            {
                btnCancel.Text = rm.GetString("Button_Cancel", culture);
            }

            string selectedItem = cmbContractType.SelectedItem?.ToString();
            cmbContractType.Items.Clear();
            string quarterly = rm.GetString("ContractType_Quarterly", culture);
            string semiannual = rm.GetString("ContractType_Semiannual", culture);
            cmbContractType.Items.Add(quarterly);
            cmbContractType.Items.Add(semiannual);

            if (selectedItem == quarterly || selectedItem == "Quy")
                cmbContractType.SelectedItem = quarterly;
            else if (selectedItem == semiannual || selectedItem == "6 Thang")
                cmbContractType.SelectedItem = semiannual;
            else if (cmbContractType.Items.Count > 0)
                cmbContractType.SelectedIndex = 0;
        }

        private void ShowAlert(string message, AlertPanel.AlertType type)
        {
            var mainLayout = this.FindForm() as Mainlayout;
            if (mainLayout != null)
            {
                mainLayout.ShowGlobalAlert(message, type);
            }
            else
            {
                MessageBox.Show(message, type.ToString(), MessageBoxButtons.OK,
                    (type == AlertPanel.AlertType.Success) ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            }
        }

        private void LoadEmployees()
        {
            try
            {
                if (UserSession.CurrentUser != null)
                {
                    txtboxEmployee.Text = UserSession.CurrentUser.HoTen;
                }
            }
            catch (Exception ex)
            {
                ShowAlert(rm.GetString("Error_LoadEmployees", culture) + ": " + ex.Message, AlertPanel.AlertType.Error);
            }
        }

        /// <summary>
        /// Tạo MaDon mới dựa trên ContractID lớn nhất hiện tại.
        /// </summary>
        private string GenerateNewMaDon()
        {
            string queryMaxId = "SELECT MAX(ContractID) FROM Contracts";
            object maxIdResult = DataProvider.Instance.ExecuteScalar(queryMaxId, null);

            int currentMaxId = 0;
            if (maxIdResult != null && maxIdResult != DBNull.Value)
            {
                currentMaxId = Convert.ToInt32(maxIdResult) + 1;
            }
            else
            {
                currentMaxId = 1;
            }

            return $"HD-{currentMaxId}";
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtboxCustomerName.Text) ||
                    string.IsNullOrWhiteSpace(txtboxPhone.Text) ||
                    string.IsNullOrWhiteSpace(txtboxAddress.Text) ||
                    string.IsNullOrWhiteSpace(txtboxOwner.Text) ||
                    string.IsNullOrWhiteSpace(txtboxIDContract.Text) ||
                    (cmbContractType == null || cmbContractType.SelectedIndex == -1) ||
                    string.IsNullOrWhiteSpace(txtboxEmployee.Text))
                {
                    ShowAlert(rm.GetString("Error_AllFieldsRequired", culture), AlertPanel.AlertType.Error);
                    return;
                }

                string email = txtEmailCustomer.Text.Trim();
                if (!string.IsNullOrWhiteSpace(email) && !ValidationHelper.IsValidEmailFormat(email))
                {
                    ShowAlert(rm.GetString("Error_InvalidEmailFormat", culture), AlertPanel.AlertType.Error);
                    txtEmailCustomer.Focus();
                    return;
                }
                string maDon = txtboxIDContract.Text.Trim();

                string tenKhachHang = txtboxCustomerName.Text.Trim();
                string kyHieuDoanhNghiep = txtboxPhone.Text.Trim();
                string diaChi = txtboxAddress.Text.Trim();
                string nguoiDaiDien = txtboxOwner.Text.Trim();
                string loaiHopDong = cmbContractType != null ? cmbContractType.Text : string.Empty;
                DateTime ngayTraKetQua = dtpDueDate.Value;
                DateTime ngayKy = DateTime.Now;

                int employeeId = UserSession.CurrentUser?.EmployeeID ?? 0;
                if (employeeId == 0)
                {
                    ShowAlert("Không tìm thấy EmployeeID của người dùng. Không thể lưu.", AlertPanel.AlertType.Error);
                    return;
                }

                string insertCustomer = @"INSERT INTO Customers (TenDoanhNghiep, KyHieuDoanhNghiep, DiaChi, TenNguoiDaiDien, Email)
                                            VALUES (@ten, @kyhieu, @diaChi, @nguoiDaiDien, @email) 
                                            ON DUPLICATE KEY UPDATE CustomerID=LAST_INSERT_ID(CustomerID);";

                DataProvider.Instance.ExecuteNonQuery(insertCustomer, new object[] { tenKhachHang, kyHieuDoanhNghiep, diaChi, nguoiDaiDien, email });

                int newCustomerId = Convert.ToInt32(DataProvider.Instance.ExecuteScalar("SELECT LAST_INSERT_ID()", null));

                string insertContract = @"INSERT INTO Contracts (MaDon, CustomerID, EmployeeID, NgayKy, NgayTraKetQua, ContractType, Status, TienTrinh)
                                            VALUES (@maDon, @customerID, @employeeID, @ngayKy, @ngayTra, @loai, 'Active', 1)";
                DataProvider.Instance.ExecuteNonQuery(insertContract, new object[] { maDon, newCustomerId, employeeId, ngayKy, ngayTraKetQua, loaiHopDong });

                int newContractId = Convert.ToInt32(DataProvider.Instance.ExecuteScalar("SELECT LAST_INSERT_ID()", null));

                string noiDungAdmin = $"Hợp đồng mới '{maDon}' vừa được tạo.";
                int adminRoleID = 5;
                NotificationService.CreateNotification("HopDongMoi", noiDungAdmin, adminRoleID, newContractId, null);

                int keHoachRoleID = 6;
                if (keHoachRoleID == 0)
                {
                    ShowAlert("RoleID phòng Kế Hoạch chưa được thiết lập. Thông báo cho Kế Hoạch bị bỏ qua.", AlertPanel.AlertType.Error);
                }
                else
                {
                    string noiDungKeHoach = $"Hợp đồng mới '{maDon}' cần được lập kế hoạch chi tiết.";
                    NotificationService.CreateNotification("HopDongMoi", noiDungKeHoach, keHoachRoleID, newContractId, null);
                }

                ShowAlert(rm.GetString("Business_SaveSuccess", culture), AlertPanel.AlertType.Success);
                ClearFields();

                ShowAlert(rm.GetString("Success_ContractCreated", culture), AlertPanel.AlertType.Success);

                ClearFields();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Duplicate entry") && ex.Message.Contains("MaDon"))
                {
                    ShowAlert(rm.GetString("Error_ContractIDExists", culture), AlertPanel.AlertType.Error);
                }
                else
                {
                    ShowAlert(rm.GetString("Error_General", culture) + ": " + ex.Message, AlertPanel.AlertType.Error);
                }
            }
        }

        private void ClearFields()
        {
            txtboxCustomerName.Text = string.Empty;

            txtboxIDContract.Text = GenerateNewMaDon();

            txtboxPhone.Text = string.Empty;
            txtboxAddress.Text = string.Empty;
            txtboxOwner.Text = string.Empty;
            txtEmailCustomer.Text = string.Empty;
            if (cmbContractType != null) cmbContractType.SelectedIndex = -1;

            dtpDueDate.Value = DateTime.Now;

            if (UserSession.CurrentUser != null)
            {
                txtboxEmployee.Text = UserSession.CurrentUser.HoTen;
            }
            else
            {
                txtboxEmployee.Text = string.Empty;
            }
        }
    }
}