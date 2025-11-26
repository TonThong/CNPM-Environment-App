using Environmental_Monitoring.Model;
using System;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Windows.Forms;

namespace Environmental_Monitoring.View.ContractContent
{
    public partial class AddEditParameterForm : Form
    {
        public ParameterDTO ResultParameter { get; private set; }
        private ResourceManager rm;
        private CultureInfo culture;

        // Constructor: Nhận param để sửa (hoặc null để thêm), isFixedName để khóa tên
        public AddEditParameterForm(ParameterDTO param = null, bool isFixedName = false)
        {
            InitializeComponent();
            InitializeLocalization();
            InitializeComboBox();

            // Xóa sự kiện cũ để tránh lặp
            btnOK.Click -= btnOK_Click;
            btnOK.Click += btnOK_Click;
            btnCancel.Click -= btnCancel_Click;
            btnCancel.Click += btnCancel_Click;

            if (param != null)
            {
                // Chế độ: Sửa/Xem chi tiết
                this.Text = rm.GetString("AddEditParam_EditTitle", culture) ?? "Chi tiết thông số";

                txtTenThongSo.Text = param.TenThongSo;
                txtDonVi.Text = param.DonVi;
                numGioiHanMin.Value = param.Min ?? 0;
                numGioiHanMax.Value = param.Max ?? 0;
                if (txtPhuongPhap != null) txtPhuongPhap.Text = param.PhuongPhap;
                SetSelectedPhuTrach(param.PhuTrach);

                // Logic khóa tên
                if (isFixedName)
                {
                    txtTenThongSo.ReadOnly = true;
                    txtTenThongSo.BackColor = System.Drawing.Color.WhiteSmoke;
                }

                ResultParameter = param;
            }
            else
            {
                // Chế độ: Thêm mới hoàn toàn
                this.Text = rm.GetString("AddEditParam_AddTitle", culture) ?? "Thêm thông số mới";
                ResultParameter = new ParameterDTO();
                txtTenThongSo.ReadOnly = false;
            }
        }

        // Khởi tạo ngôn ngữ
        private void InitializeLocalization()
        {
            rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(AddEditParameterForm).Assembly);
            culture = Thread.CurrentThread.CurrentUICulture;

            if (btnOK != null) btnOK.Text = rm.GetString("Button_Save", culture) ?? "Lưu";
            if (btnCancel != null) btnCancel.Text = rm.GetString("Button_Cancel", culture) ?? "Hủy";
            if (lblTenThongSo != null) lblTenThongSo.Text = rm.GetString("Grid_ParamName", culture) ?? "Tên thông số";
            if (lblDonVi != null) lblDonVi.Text = rm.GetString("Grid_Unit", culture) ?? "Đơn vị";
            if (lblGioiHanMin != null) lblGioiHanMin.Text = rm.GetString("Grid_Min", culture) ?? "Min";
            if (lblGioiHanMax != null) lblGioiHanMax.Text = rm.GetString("Grid_Max", culture) ?? "Max";
            if (lblPhuTrach != null) lblPhuTrach.Text = rm.GetString("Grid_Department", culture) ?? "Phụ trách";
        }

        // Khởi tạo ComboBox Phụ trách
        private void InitializeComboBox()
        {
            if (cbbPhuTrach == null) return;
            cbbPhuTrach.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbPhuTrach.Items.Clear();
            cbbPhuTrach.Items.Add(new DepartmentItem { Value = "HienTruong", DisplayName = GetLocalizedDepartment("HienTruong") });
            cbbPhuTrach.Items.Add(new DepartmentItem { Value = "ThiNghiem", DisplayName = GetLocalizedDepartment("ThiNghiem") });
            cbbPhuTrach.SelectedIndex = 0;
        }

        // Lấy tên bộ phận theo ngôn ngữ
        private string GetLocalizedDepartment(string dbValue)
        {
            if (string.IsNullOrEmpty(dbValue)) return "";
            if (dbValue.Equals("HienTruong", StringComparison.OrdinalIgnoreCase)) return rm.GetString("Dept_Field", culture) ?? "Hiện trường";
            if (dbValue.Equals("ThiNghiem", StringComparison.OrdinalIgnoreCase)) return rm.GetString("Dept_Lab", culture) ?? "Thí nghiệm";
            return dbValue;
        }

        // Chọn item trong ComboBox Phụ trách
        private void SetSelectedPhuTrach(string value)
        {
            if (string.IsNullOrEmpty(value)) return;
            foreach (DepartmentItem item in cbbPhuTrach.Items)
            {
                if (item.Value.Equals(value, StringComparison.OrdinalIgnoreCase))
                {
                    cbbPhuTrach.SelectedItem = item;
                    return;
                }
            }
        }

        // Sự kiện nút Lưu
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenThongSo.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên thông số!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ResultParameter == null) ResultParameter = new ParameterDTO();

            ResultParameter.TenThongSo = txtTenThongSo.Text.Trim();
            ResultParameter.DonVi = txtDonVi.Text.Trim();
            ResultParameter.Min = numGioiHanMin.Value;
            ResultParameter.Max = numGioiHanMax.Value;
            if (txtPhuongPhap != null) ResultParameter.PhuongPhap = txtPhuongPhap.Text.Trim();

            if (cbbPhuTrach.SelectedItem is DepartmentItem dept) ResultParameter.PhuTrach = dept.Value;
            else ResultParameter.PhuTrach = "ThiNghiem";

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // Sự kiện nút Hủy
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private class DepartmentItem
        {
            public string Value { get; set; }
            public string DisplayName { get; set; }
            public override string ToString() => DisplayName;
        }
    }
}