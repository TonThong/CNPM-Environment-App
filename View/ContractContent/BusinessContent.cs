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

            this.Load += BusinessContent_Load;
        }

        private void BusinessContent_Load(object sender, EventArgs e)
        {
            LoadEmployees();
            culture = Thread.CurrentThread.CurrentUICulture;
            cmbContractType.Items.Clear();
            cmbContractType.Items.Add(rm.GetString("ContractType_Quarterly", culture));
            cmbContractType.Items.Add(rm.GetString("ContractType_Semiannual", culture));

            if (this.Controls.Find("cmbContractType", true).Length > 0)
            {
                cmbContractType.SelectedIndex = 0;
            }

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
                if (!maDon.StartsWith("HD-"))
                {
                    ShowAlert(rm.GetString("Error_ContractIDPrefix", culture), AlertPanel.AlertType.Error);
                    return;
                }

                string tenKhachHang = txtboxCustomerName.Text.Trim();
                string kyHieuDoanhNghiep = txtboxPhone.Text.Trim();
                string diaChi = txtboxAddress.Text.Trim();
                string nguoiDaiDien = txtboxOwner.Text.Trim();
                string loaiHopDong = cmbContractType != null ? cmbContractType.Text : string.Empty;
                string employeeName = txtboxEmployee.Text.Trim();
                DateTime ngayTraKetQua = dtpDueDate.Value;

                string insertCustomer = @"INSERT INTO Customers (TenDoanhNghiep, KyHieuDoanhNghiep, DiaChi, TenNguoiDaiDien, Email)
                                            VALUES (@ten, @kyhieu, @diaChi, @nguoiDaiDien, @email) 
                                            ON DUPLICATE KEY UPDATE CustomerID=LAST_INSERT_ID(CustomerID);";

                DataProvider.Instance.ExecuteNonQuery(insertCustomer, new object[] { tenKhachHang, kyHieuDoanhNghiep, diaChi, nguoiDaiDien, email });

                object contractIdObj = DataProvider.Instance.ExecuteScalar("SELECT LAST_INSERT_ID()", null);
                int newContractId = (contractIdObj != null && int.TryParse(contractIdObj.ToString(), out int ctid)) ? ctid : 0;

                string noiDung = $"Hợp đồng mới '{maDon}' vừa được tạo.";
                int adminRoleID = 5;
                NotificationService.CreateNotification("HopDongMoi", noiDung, adminRoleID, newContractId, null);

              
                ShowAlert(rm.GetString("Error_CustomerUndefined", culture), AlertPanel.AlertType.Error);
                ClearFields();
            }
            catch (Exception ex)
            {
                ShowAlert(rm.GetString("Error_General", culture) + ": " + ex.Message, AlertPanel.AlertType.Error);
            }
        }

        private void ClearFields()
        {
            txtboxCustomerName.Text = string.Empty;
            txtboxIDContract.Text = string.Empty;
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