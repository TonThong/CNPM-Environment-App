using Environmental_Monitoring.Controller;
using Environmental_Monitoring.Controller.Data;
using Environmental_Monitoring.Model;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Environmental_Monitoring.View.ContractContent
{
    public partial class SampleInformation : Form
    {
        // Biến lưu kết quả trả về sau khi bấm Lưu
        public SampleDTO ResultSample { get; private set; }

        // Danh sách thông số hiện tại (Dùng BindingList để grid tự update)
        private BindingList<ParameterDTO> _currentParams;

        // Lưu ID của môi trường đang chọn (Air/Water/Soil...)
        private int _selectedEnvId = 0;

        // Cờ kiểm tra form đã load xong chưa (tránh sự kiện chạy lung tung khi khởi tạo)
        private bool _isReady = false;

        // Constructor: Nhận vào mẫu cần sửa (hoặc null nếu là thêm mới)
        public SampleInformation(SampleDTO editSample = null)
        {
            InitializeComponent();

            this.btnLuuMau.Click += new EventHandler(btnLuuMau_Click_1);
            this.btnHuy.Click += new EventHandler(btnHuy_Click_1);

            // Khởi tạo danh sách thông số
            if (editSample != null && editSample.Parameters != null)
            {
                _currentParams = new BindingList<ParameterDTO>(editSample.Parameters);
                _selectedEnvId = editSample.BaseTemplateID; // Nếu đang sửa, lấy lại ID môi trường cũ
            }
            else
            {
                _currentParams = new BindingList<ParameterDTO>();
            }

            SetupGridView(); // Cấu hình cột cho lưới

            // Nếu là chế độ Sửa, điền dữ liệu cũ vào các ô nhập liệu
            if (editSample != null) SetupUI(editSample);
        }

        // Sự kiện Form Load: Nơi nạp dữ liệu vào ComboBox
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            BindParamsGrid(); // Đổ dữ liệu vào lưới

            // 1. Load danh sách Môi trường (Air, Water...) vào ComboBox trên cùng
            LoadEnvironmentComboBox();

            // 2. Nếu đang Edit, chọn lại đúng môi trường cũ trong ComboBox
            if (_selectedEnvId > 0)
            {
                SetSelectedEnvironment(_selectedEnvId);
            }

            _isReady = true; // Đánh dấu form đã sẵn sàng
        }

        // Tải danh sách các Template môi trường gốc từ Database vào ComboBox
        private void LoadEnvironmentComboBox()
        {
            try
            {
                if (cbbMoiTruong == null) return;

                // Lấy các Template gốc (loại trừ các template con "Template cho...")
                string query = "SELECT TemplateID, TenMau FROM SampleTemplates WHERE TenMau NOT LIKE 'Template cho %' ORDER BY TenMau ASC";
                DataTable dt = DataProvider.Instance.ExecuteQuery(query);

                cbbMoiTruong.DataSource = dt;
                cbbMoiTruong.DisplayMember = "TenMau";
                cbbMoiTruong.ValueMember = "TemplateID";

                // Đăng ký sự kiện: Khi chọn môi trường -> Load thông số tương ứng
                cbbMoiTruong.SelectedIndexChanged += cbbMoiTruong_SelectedIndexChanged;

                // Mặc định chưa chọn gì
                cbbMoiTruong.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách môi trường: " + ex.Message);
            }
        }

        // Sự kiện: Khi người dùng đổi Môi trường -> Load lại danh sách Thông số tương ứng
        private void cbbMoiTruong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbMoiTruong.SelectedValue == null) return;

            try
            {
                int newEnvId = Convert.ToInt32(cbbMoiTruong.SelectedValue);

                // Nếu môi trường thay đổi so với lần trước
                if (newEnvId != _selectedEnvId)
                {
                    _selectedEnvId = newEnvId;
                    LoadComboBoxThongSo(_selectedEnvId); // Load lại ComboBox thông số
                }
            }
            catch { }
        }

        // Hàm hỗ trợ chọn lại môi trường cũ khi mở form Sửa
        private void SetSelectedEnvironment(int envId)
        {
            if (cbbMoiTruong == null) return;
            cbbMoiTruong.SelectedValue = envId;
            // Gọi thủ công hàm load thông số để đảm bảo ComboBox thông số có dữ liệu
            LoadComboBoxThongSo(envId);
        }

        // Tải danh sách thông số thuộc về môi trường cụ thể (dựa vào envId)
        private void LoadComboBoxThongSo(int envId)
        {
            try
            {
                // Ngắt sự kiện tạm thời để tránh lỗi khi reset
                cbbThongSo.SelectedIndexChanged -= cbbThongSo_SelectedIndexChanged;
                cbbThongSo.DataSource = null;
                cbbThongSo.Items.Clear();

                // Lấy thông số liên kết với TemplateID này
                string query = @"SELECT p.* FROM Parameters p 
                                 JOIN TemplateParameters tp ON p.ParameterID = tp.ParameterID 
                                 WHERE tp.TemplateID = @tid
                                 ORDER BY p.TenThongSo ASC";

                DataTable dt = DataProvider.Instance.ExecuteQuery(query, new object[] { envId });

                if (dt != null && dt.Rows.Count > 0)
                {
                    var listItems = dt.AsEnumerable().Select(row => new ParameterCbbItem
                    {
                        Text = row["TenThongSo"].ToString(),
                        RowData = row
                    }).ToList();

                    cbbThongSo.DisplayMember = "Text";
                    cbbThongSo.DataSource = listItems;
                    cbbThongSo.SelectedIndex = -1;
                }
            }
            catch { }
            finally
            {
                // Đăng ký lại sự kiện
                cbbThongSo.SelectedIndexChanged += cbbThongSo_SelectedIndexChanged;
            }
        }

        // Điền dữ liệu từ object SampleDTO vào các ô nhập liệu (Khi sửa)
        private void SetupUI(SampleDTO editSample)
        {
            if (txtNenMau != null) txtNenMau.Text = editSample.TenNenMau; // Tên tự đặt
            if (txtKyHieu != null) txtKyHieu.Text = editSample.KyHieuMau;
            if (txtViTri != null) txtViTri.Text = editSample.ViTri;

            string toaDoHienThi = "";
            if (!string.IsNullOrEmpty(editSample.ToaDoX) && !string.IsNullOrEmpty(editSample.ToaDoY))
                toaDoHienThi = $"{editSample.ToaDoX}, {editSample.ToaDoY}";
            else
                toaDoHienThi = editSample.ToaDoX + editSample.ToaDoY;

            if (txtToaDo != null) txtToaDo.Text = toaDoHienThi;
        }

        // Cấu hình các cột cho GridView hiển thị danh sách thông số
        private void SetupGridView()
        {
            dgvParams.AutoGenerateColumns = false;
            dgvParams.AllowUserToAddRows = false;
            dgvParams.Columns.Clear();

            dgvParams.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tên thông số", DataPropertyName = "TenThongSo", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvParams.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Đơn vị", DataPropertyName = "DonVi", Width = 80 });
            dgvParams.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Giới hạn", DataPropertyName = "HienThiMinMax", Width = 90 });
            dgvParams.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Phương pháp", DataPropertyName = "PhuongPhap", Width = 140 });
            dgvParams.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Phụ trách", DataPropertyName = "PhuTrach", Width = 100 });

            // Cột nút Xóa
            DataGridViewButtonColumn btnDel = new DataGridViewButtonColumn
            {
                Name = "colDelete",
                HeaderText = "",
                Text = "Xóa",
                UseColumnTextForButtonValue = true,
                Width = 50
            };
            btnDel.DefaultCellStyle.ForeColor = Color.Red;
            dgvParams.Columns.Add(btnDel);

            dgvParams.CellContentClick += dgvParams_CellContentClick;
            dgvParams.CellDoubleClick += dgvParams_CellDoubleClick;
        }

        // Gán nguồn dữ liệu cho GridView
        private void BindParamsGrid()
        {
            dgvParams.DataSource = null;
            if (_currentParams.Count > 0) dgvParams.DataSource = _currentParams;
        }

        // Xử lý sự kiện bấm nút Xóa thông số trên Grid
        private void dgvParams_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvParams.Columns[e.ColumnIndex].Name == "colDelete")
            {
                _currentParams.RemoveAt(e.RowIndex);
                if (_currentParams.Count == 0) dgvParams.DataSource = null;
            }
        }

        // Xử lý sự kiện Double Click để sửa thông số trên Grid
        private void dgvParams_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _currentParams.Count) return;
            var paramToEdit = _currentParams[e.RowIndex];

            // Mở form AddEditParameterForm ở chế độ Sửa
            using (var frm = new AddEditParameterForm(paramToEdit, isFixedName: false))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    _currentParams[e.RowIndex] = frm.ResultParameter;
                    dgvParams.InvalidateRow(e.RowIndex);
                }
            }
        }

        // Sự kiện: Khi chọn 1 thông số từ ComboBox -> Thêm vào lưới
        private void cbbThongSo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isReady) return;
            if (cbbThongSo.SelectedIndex == -1) return;
            if (cbbThongSo.SelectedItem == null) return;

            try
            {
                if (cbbThongSo.SelectedItem is ParameterCbbItem selectedItem)
                {
                    DataRow dr = selectedItem.RowData;

                    // Map dữ liệu từ DB sang ParameterDTO
                    ParameterDTO selectedParam = new ParameterDTO
                    {
                        ParameterID = Convert.ToInt32(dr["ParameterID"]),
                        TenThongSo = dr["TenThongSo"].ToString(),
                        DonVi = dr["DonVi"].ToString(),
                        Min = dr["GioiHanMin"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["GioiHanMin"]),
                        Max = dr["GioiHanMax"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["GioiHanMax"]),
                        PhuongPhap = dr["PhuongPhap"].ToString(),
                        PhuTrach = dr["PhuTrach"].ToString()
                    };

                    // Dùng thủ thuật này: Chỉ lấy dữ liệu vào DTO nhưng không mở form (Add thẳng)
                    // Hoặc mở form xác nhận nhưng khóa tên (isFixedName = true)
                    using (var frm = new AddEditParameterForm(selectedParam, isFixedName: true))
                    {
                        if (frm.ShowDialog() == DialogResult.OK) AddParameterSafe(frm.ResultParameter);
                    }

                    // Reset ComboBox để có thể chọn lại cái khác
                    cbbThongSo.SelectedIndexChanged -= cbbThongSo_SelectedIndexChanged;
                    cbbThongSo.SelectedIndex = -1;
                    cbbThongSo.SelectedIndexChanged += cbbThongSo_SelectedIndexChanged;
                }
            }
            catch { }
        }

        // Hàm thêm thông số an toàn (kiểm tra trùng lặp nếu cần)
        private void AddParameterSafe(ParameterDTO newParam)
        {
            var existing = _currentParams.FirstOrDefault(p => p.TenThongSo == newParam.TenThongSo);
            if (existing != null)
            {
                int index = _currentParams.IndexOf(existing);
                _currentParams[index] = newParam; // Update
            }
            else
            {
                _currentParams.Add(newParam); // Add new
            }
            if (dgvParams.DataSource == null) BindParamsGrid();
        }

        // Sự kiện nút Lưu Mẫu
        private void btnLuuMau_Click_1(object sender, EventArgs e)
        {
            // Validate: Bắt buộc chọn Môi trường
            if (cbbMoiTruong.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Môi trường!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate: Tên nền mẫu nhập tự do
            if (string.IsNullOrWhiteSpace(txtNenMau.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên Nền Mẫu!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNenMau.Focus();
                return;
            }

            // Validate các trường khác
            if (string.IsNullOrWhiteSpace(txtViTri.Text)) { MessageBox.Show("Nhập vị trí!"); txtViTri.Focus(); return; }
            if (string.IsNullOrWhiteSpace(txtKyHieu.Text)) { MessageBox.Show("Nhập ký hiệu!"); txtKyHieu.Focus(); return; }
            if (string.IsNullOrWhiteSpace(txtToaDo.Text)) { MessageBox.Show("Nhập tọa độ!"); txtToaDo.Focus(); return; }

            if (_currentParams.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ít nhất một thông số!", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xử lý tách chuỗi tọa độ
            string x = "", y = "";
            string rawToaDo = txtToaDo.Text.Trim();
            if (rawToaDo.Contains(","))
            {
                var parts = rawToaDo.Split(',');
                if (parts.Length >= 1) x = parts[0].Trim();
                if (parts.Length >= 2) y = parts[1].Trim();
            }
            else { x = rawToaDo; }

            // Tạo object kết quả trả về
            ResultSample = new SampleDTO
            {
                // Lưu ID môi trường đã chọn (Air/Water/Soil) làm BaseTemplateID
                BaseTemplateID = Convert.ToInt32(cbbMoiTruong.SelectedValue),

                // Lưu tên người dùng tự đặt (nhập tay)
                TenNenMau = txtNenMau.Text.Trim(),

                KyHieuMau = txtKyHieu.Text.Trim(),
                ViTri = txtViTri.Text.Trim(),
                ToaDoX = x,
                ToaDoY = y,
                Parameters = _currentParams.ToList()
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // Sự kiện nút Hủy
        private void btnHuy_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Sự kiện nút Thêm Thông Số (Tùy chỉnh/Custom)
        private void btnThemThongSo_Click_1(object sender, EventArgs e)
        {
            // Mở form trống, không khóa tên
            using (var frm = new AddEditParameterForm(null, isFixedName: false))
            {
                if (frm.ShowDialog() == DialogResult.OK) AddParameterSafe(frm.ResultParameter);
            }
        }

        // Class phụ trợ cho ComboBox
        public class ParameterCbbItem
        {
            public string Text { get; set; }
            public DataRow RowData { get; set; }
        }
    }
}