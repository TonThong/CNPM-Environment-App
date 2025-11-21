using Environmental_Monitoring.Controller;
using Environmental_Monitoring.Controller.Data;
using Environmental_Monitoring.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading;
using System.Windows.Forms;

namespace Environmental_Monitoring.View.ContractContent
{
    public partial class AddEditParameterForm : Form
    {
        public Parameter Parameter { get; private set; }

        // Biến cờ xác định đang Thêm mới hay Sửa
        private bool _isNew = true;

        // Thuộc tính để lấy ID mẫu đã chọn từ Form
        public int? SelectedTemplateID
        {
            get
            {
                if (cbbTemplate != null && cbbTemplate.SelectedItem is TemplateItem item)
                    return item.Id;
                return null;
            }
        }

        private ResourceManager rm;
        private CultureInfo culture;

        public AddEditParameterForm(Parameter parameter = null)
        {
            InitializeComponent();
            InitializeLocalization();

            // Cài đặt ComboBox chỉ cho chọn
            if (cbbPhuTrach != null) cbbPhuTrach.DropDownStyle = ComboBoxStyle.DropDownList;
            if (cbbTemplate != null) cbbTemplate.DropDownStyle = ComboBoxStyle.DropDownList;

            // Xóa và đăng ký lại sự kiện để tránh trùng lặp
            if (btnCancel != null)
            {
                btnCancel.Click -= btnCancel_Click;
                btnCancel.Click += btnCancel_Click;
            }

            if (btnOK != null)
            {
                btnOK.Click -= btnOK_Click;
                btnOK.Click += btnOK_Click;
            }

            UpdateUIText();
            LoadTemplates();

            if (parameter != null)
            {
                // --- CHẾ ĐỘ CHỈNH SỬA ---
                _isNew = false; // Đánh dấu là đang sửa
                Parameter = parameter;

                txtTenThongSo.Text = parameter.TenThongSo;
                txtDonVi.Text = parameter.DonVi;
                numGioiHanMin.Value = parameter.GioiHanMin ?? 0;
                numGioiHanMax.Value = parameter.GioiHanMax ?? 0;

                // Chọn đúng mục Phụ trách
                if (parameter.PhuTrach != null)
                {
                    bool found = false;
                    foreach (DepartmentItem item in cbbPhuTrach.Items)
                    {
                        if (item.Value == parameter.PhuTrach)
                        {
                            cbbPhuTrach.SelectedItem = item;
                            found = true;
                            break;
                        }
                    }
                    // Fallback nếu không tìm thấy item khớp
                    if (!found && !string.IsNullOrEmpty(parameter.PhuTrach))
                    {
                        // Có thể thêm tạm vào hoặc bỏ qua
                    }
                }

                this.Text = rm.GetString("AddEditParam_EditTitle", culture) ?? "Chỉnh sửa thông số";

                // Khi chỉnh sửa thì disable chọn mẫu
                if (cbbTemplate != null) cbbTemplate.Enabled = false;
            }
            else
            {
                // --- CHẾ ĐỘ THÊM MỚI ---
                _isNew = true; // Đánh dấu là thêm mới
                Parameter = new Parameter();
                this.Text = rm.GetString("AddEditParam_AddTitle", culture) ?? "Thêm thông số mới";
                if (cbbTemplate != null) cbbTemplate.Enabled = true;
            }
        }

        private void InitializeLocalization()
        {
            rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(AddEditParameterForm).Assembly);
            culture = Thread.CurrentThread.CurrentUICulture;
        }

        private void UpdateUIText()
        {
            if (btnOK != null) btnOK.Text = rm.GetString("Button_Save", culture) ?? "Lưu";
            if (btnCancel != null) btnCancel.Text = rm.GetString("Button_Cancel", culture) ?? "Hủy";
            if (lblTenThongSo != null) lblTenThongSo.Text = rm.GetString("Grid_ParamName", culture) ?? "Tên thông số";
            if (lblDonVi != null) lblDonVi.Text = rm.GetString("Grid_Unit", culture) ?? "Đơn vị";
            if (lblGioiHanMin != null) lblGioiHanMin.Text = rm.GetString("Grid_Min", culture) ?? "Min";
            if (lblGioiHanMax != null) lblGioiHanMax.Text = rm.GetString("Grid_Max", culture) ?? "Max";
            if (lblPhuTrach != null) lblPhuTrach.Text = rm.GetString("Grid_Department", culture) ?? "Phụ trách";
            if (lblTemplate != null) lblTemplate.Text = rm.GetString("Label_Template", culture) ?? "Mẫu Môi Trường";
        }

        private string GetLocalizedDepartment(string dbValue)
        {
            if (string.IsNullOrEmpty(dbValue)) return "";
            if (dbValue.Equals("HienTruong", StringComparison.OrdinalIgnoreCase) || dbValue.Equals("Field", StringComparison.OrdinalIgnoreCase))
                return rm.GetString("Dept_Field", culture) ?? "Hiện trường";
            if (dbValue.Equals("ThiNghiem", StringComparison.OrdinalIgnoreCase) || dbValue.Equals("Laboratory", StringComparison.OrdinalIgnoreCase))
                return rm.GetString("Dept_Lab", culture) ?? "Thí nghiệm";
            return dbValue;
        }

        private string GetLocalizedTemplateName(string dbName)
        {
            if (string.IsNullOrEmpty(dbName)) return "";
            string lowerName = dbName.ToLower();
            if (lowerName.Contains("không khí") || lowerName.Contains("air"))
                return rm.GetString("Template_Air", culture) ?? "Môi trường không khí";
            if (lowerName.Contains("nước") || lowerName.Contains("water"))
                return rm.GetString("Template_Water", culture) ?? "Môi trường nước";
            if (lowerName.Contains("đất") || lowerName.Contains("soil"))
                return rm.GetString("Template_Soil", culture) ?? "Môi trường đất";
            if (lowerName.Contains("tiếng ồn") || lowerName.Contains("độ rung") || lowerName.Contains("noise") || lowerName.Contains("vibration"))
                return rm.GetString("Template_Noise", culture) ?? "Tiếng ồn và độ rung";
            return dbName;
        }

        public void SetPhuTrachOptions(string[] options)
        {
            cbbPhuTrach.Items.Clear();
            foreach (var opt in options)
            {
                cbbPhuTrach.Items.Add(new DepartmentItem
                {
                    Value = opt,
                    DisplayName = GetLocalizedDepartment(opt)
                });
            }
            if (cbbPhuTrach.Items.Count > 0) cbbPhuTrach.SelectedIndex = 0;
        }

        private void LoadTemplates()
        {
            if (cbbTemplate == null) return;

            try
            {
                string query = "SELECT TemplateID, TenMau FROM SampleTemplates WHERE TenMau NOT LIKE 'Template cho %'";
                DataTable dt = DataProvider.Instance.ExecuteQuery(query);

                cbbTemplate.Items.Clear();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string rawName = row["TenMau"].ToString();
                        cbbTemplate.Items.Add(new TemplateItem
                        {
                            Id = Convert.ToInt32(row["TemplateID"]),
                            Name = GetLocalizedTemplateName(rawName)
                        });
                    }
                }
            }
            catch { }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra đầu vào
            if (string.IsNullOrWhiteSpace(txtTenThongSo.Text) || cbbPhuTrach.SelectedItem == null)
            {
                string msg = rm.GetString("AddEditParam_ValidationWarning", culture) ?? "Vui lòng nhập Tên thông số và chọn bộ phận Phụ trách!";
                MessageBox.Show(msg, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 2. Cập nhật object từ UI
                Parameter.TenThongSo = txtTenThongSo.Text.Trim();
                Parameter.DonVi = txtDonVi.Text.Trim();
                Parameter.GioiHanMin = numGioiHanMin.Value;
                Parameter.GioiHanMax = numGioiHanMax.Value;

                if (cbbPhuTrach.SelectedItem is DepartmentItem deptItem)
                    Parameter.PhuTrach = deptItem.Value;
                else
                    Parameter.PhuTrach = cbbPhuTrach.SelectedItem?.ToString();

                // 3. Lưu vào DB
                SaveToDatabase();

                // 4. Đóng Form
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveToDatabase()
        {
            // Kiểm tra trùng tên (trừ chính nó nếu đang edit)
            string checkQuery = "SELECT COUNT(*) FROM Parameters WHERE TenThongSo = @ten AND ParameterID != @id";
            object count = DataProvider.Instance.ExecuteScalar(checkQuery, new object[] { Parameter.TenThongSo, Parameter.ParameterID });
            if (Convert.ToInt32(count) > 0)
            {
                throw new Exception("Tên thông số đã tồn tại. Vui lòng chọn tên khác.");
            }

            if (_isNew) // SỬ DỤNG CỜ _isNew THAY VÌ KIỂM TRA ID
            {
                // --- INSERT ---
                string insertQuery = @"INSERT INTO Parameters (TenThongSo, DonVi, GioiHanMin, GioiHanMax, PhuTrach, ONhiem) 
                                       VALUES (@Ten, @DonVi, @Min, @Max, @PhuTrach, 0)";

                int rows = DataProvider.Instance.ExecuteNonQuery(insertQuery,
                    new object[] { Parameter.TenThongSo, Parameter.DonVi, Parameter.GioiHanMin, Parameter.GioiHanMax, Parameter.PhuTrach });

                if (rows <= 0) throw new Exception("Không thể thêm thông số vào cơ sở dữ liệu.");

                // Nếu có chọn Mẫu, lưu liên kết vào bảng TemplateParameters
                if (cbbTemplate != null && cbbTemplate.SelectedItem is TemplateItem selectedTemplate)
                {
                    // Lấy ID vừa tạo để liên kết
                    // Lưu ý: Dùng MAX(ID) là cách đơn giản nhất trong context này, 
                    // nhưng trong môi trường nhiều người dùng đồng thời nên dùng SELECT LAST_INSERT_ID() trong cùng transaction.
                    object newIdObj = DataProvider.Instance.ExecuteScalar("SELECT MAX(ParameterID) FROM Parameters");

                    if (newIdObj != null && newIdObj != DBNull.Value)
                    {
                        int newParamId = Convert.ToInt32(newIdObj);

                        // Kiểm tra xem đã tồn tại liên kết chưa (đề phòng duplicate)
                        string checkLink = "SELECT COUNT(*) FROM TemplateParameters WHERE TemplateID = @tid AND ParameterID = @pid";
                        object linkCount = DataProvider.Instance.ExecuteScalar(checkLink, new object[] { selectedTemplate.Id, newParamId });

                        if (Convert.ToInt32(linkCount) == 0)
                        {
                            string linkQuery = "INSERT INTO TemplateParameters (TemplateID, ParameterID) VALUES (@tid, @pid)";
                            DataProvider.Instance.ExecuteNonQuery(linkQuery, new object[] { selectedTemplate.Id, newParamId });
                        }
                    }
                }
            }
            else
            {
                // --- UPDATE ---
                string updateQuery = @"UPDATE Parameters 
                                       SET TenThongSo = @Ten, DonVi = @DonVi, GioiHanMin = @Min, GioiHanMax = @Max, PhuTrach = @PhuTrach 
                                       WHERE ParameterID = @ID";

                int rows = DataProvider.Instance.ExecuteNonQuery(updateQuery,
                    new object[] { Parameter.TenThongSo, Parameter.DonVi, Parameter.GioiHanMin, Parameter.GioiHanMax, Parameter.PhuTrach, Parameter.ParameterID });

                if (rows <= 0) throw new Exception("Không thể cập nhật thông tin. Có thể thông số đã bị xóa.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void cbbPhuTrach_SelectedIndexChanged(object sender, EventArgs e) { }

        // Class hiển thị Mẫu
        private class TemplateItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public override string ToString() => Name;
        }

        // Class hiển thị Phụ trách
        private class DepartmentItem
        {
            public string Value { get; set; }
            public string DisplayName { get; set; }
            public override string ToString() => DisplayName;
        }
    }
}