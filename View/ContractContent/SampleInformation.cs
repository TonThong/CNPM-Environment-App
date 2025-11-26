using Environmental_Monitoring.Controller;
using Environmental_Monitoring.Controller.Data;
using Environmental_Monitoring.Model;
using System;
using System.ComponentModel; // Cần cho BindingList
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Environmental_Monitoring.View.ContractContent
{
    public partial class SampleInformation : Form
    {
        // Output result
        public SampleDTO ResultSample { get; private set; }

        // Dùng BindingList thay vì List thường để Grid tự cập nhật an toàn
        private BindingList<ParameterDTO> _currentParams;

        private int _baseTemplateId;
        private string _baseTemplateName;

        // Cờ chặn sự kiện
        private bool _isReady = false;

        public SampleInformation(int baseTemplateId, string baseTemplateName, SampleDTO editSample = null)
        {
            InitializeComponent();

            this.btnLuuMau.Click += new EventHandler(btnLuuMau_Click_1);
            this.btnHuy.Click += new EventHandler(btnHuy_Click_1);

            _baseTemplateId = baseTemplateId;
            _baseTemplateName = baseTemplateName;

            // 1. Khởi tạo danh sách an toàn
            if (editSample != null && editSample.Parameters != null)
            {
                _currentParams = new BindingList<ParameterDTO>(editSample.Parameters);
            }
            else
            {
                _currentParams = new BindingList<ParameterDTO>();
            }

            // 2. Setup giao diện tĩnh
            SetupUI(editSample);
            SetupGridView(); // Chỉ setup cột, KHÔNG bind data ở đây
        }

        // --- LOGIC MỚI: CHỈ NẠP DỮ LIỆU KHI FORM ĐÃ SẴN SÀNG ---
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Bind Grid an toàn trong OnLoad
            BindParamsGrid();

            // Nạp ComboBox
            LoadComboBoxDataSafe();

            // Đánh dấu Form đã sẵn sàng nhận sự kiện
            _isReady = true;
        }

        private void SetupUI(SampleDTO editSample)
        {
            if (txtNenMau != null)
            {
                txtNenMau.Text = editSample != null ? editSample.TenNenMau : _baseTemplateName;
            }

            if (editSample != null)
            {
                if (txtKyHieu != null) txtKyHieu.Text = editSample.KyHieuMau;
                if (txtViTri != null) txtViTri.Text = editSample.ViTri;

                string toaDoHienThi = "";
                if (!string.IsNullOrEmpty(editSample.ToaDoX) && !string.IsNullOrEmpty(editSample.ToaDoY))
                    toaDoHienThi = $"{editSample.ToaDoX}, {editSample.ToaDoY}";
                else
                    toaDoHienThi = editSample.ToaDoX + editSample.ToaDoY;

                if (txtToaDo != null) txtToaDo.Text = toaDoHienThi;
            }
        }

        private void SetupGridView()
        {
            dgvParams.AutoGenerateColumns = false;
            dgvParams.AllowUserToAddRows = false;
            dgvParams.Columns.Clear();

            // Định nghĩa cột thủ công
            dgvParams.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tên thông số", DataPropertyName = "TenThongSo", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvParams.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Đơn vị", DataPropertyName = "DonVi", Width = 80 });
            dgvParams.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Giới hạn", DataPropertyName = "HienThiMinMax", Width = 90 });
            dgvParams.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Phương pháp", DataPropertyName = "PhuongPhap", Width = 140 });

            dgvParams.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Phụ trách", DataPropertyName = "PhuTrach", Width = 100 });

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

            // Đăng ký sự kiện
            dgvParams.CellContentClick += dgvParams_CellContentClick;
            dgvParams.CellDoubleClick += dgvParams_CellDoubleClick;
        }

        private void BindParamsGrid()
        {
            // Reset DataSource an toàn
            dgvParams.DataSource = null;
            if (_currentParams.Count > 0)
            {
                dgvParams.DataSource = _currentParams;
            }
        }

        private void LoadComboBoxDataSafe()
        {
            try
            {
                // Ngắt sự kiện tạm thời
                cbbThongSo.SelectedIndexChanged -= cbbThongSo_SelectedIndexChanged;
                cbbThongSo.DataSource = null;
                cbbThongSo.Items.Clear();

                string query = @"SELECT p.* FROM Parameters p 
                                 JOIN TemplateParameters tp ON p.ParameterID = tp.ParameterID 
                                 WHERE tp.TemplateID = @tid
                                 ORDER BY p.TenThongSo ASC";

                DataTable dt = DataProvider.Instance.ExecuteQuery(query, new object[] { _baseTemplateId });

                if (dt != null && dt.Rows.Count > 0)
                {
                    // Convert DataTable sang List Object để tránh lỗi BindingContext
                    var listItems = dt.AsEnumerable().Select(row => new ParameterCbbItem
                    {
                        Text = row["TenThongSo"].ToString(),
                        RowData = row
                    }).ToList();

                    // Gán DataSource mới
                    cbbThongSo.DisplayMember = "Text";
                    cbbThongSo.DataSource = listItems;
                    cbbThongSo.SelectedIndex = -1; // Reset về trạng thái chưa chọn
                }
            }
            catch (Exception ex)
            {
                // Log error
            }
            finally
            {
                // Đăng ký lại sự kiện
                cbbThongSo.SelectedIndexChanged += cbbThongSo_SelectedIndexChanged;
            }
        }

        private void cbbThongSo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 1. Kiểm tra an toàn tuyệt đối
            if (!_isReady) return;
            if (cbbThongSo.SelectedIndex == -1) return;
            if (cbbThongSo.SelectedItem == null) return;

            try
            {
                if (cbbThongSo.SelectedItem is ParameterCbbItem selectedItem)
                {
                    DataRow dr = selectedItem.RowData;
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

                    using (var frm = new AddEditParameterForm(selectedParam, isFixedName: true))
                    {
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            AddParameterSafe(frm.ResultParameter);
                        }
                    }

                    // Reset ComboBox về -1 nhưng phải ngắt sự kiện để tránh lặp vô tận
                    cbbThongSo.SelectedIndexChanged -= cbbThongSo_SelectedIndexChanged;
                    cbbThongSo.SelectedIndex = -1;
                    cbbThongSo.SelectedIndexChanged += cbbThongSo_SelectedIndexChanged;
                }
            }
            catch { }
        }

        private void AddParameterSafe(ParameterDTO newParam)
        {
            var existing = _currentParams.FirstOrDefault(p => p.TenThongSo == newParam.TenThongSo);
            if (existing != null)
            {
                // Update
                int index = _currentParams.IndexOf(existing);
                _currentParams[index] = newParam;
            }
            else
            {
                // Add
                _currentParams.Add(newParam);
            }

            // Vì dùng BindingList, Grid sẽ tự cập nhật, nhưng ta bind lại cho chắc chắn nếu Grid đang null
            if (dgvParams.DataSource == null) BindParamsGrid();
        }

        private void dgvParams_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvParams.Columns[e.ColumnIndex].Name == "colDelete")
            {
                _currentParams.RemoveAt(e.RowIndex);
                // Nếu xóa hết, set DataSource về null để tránh lỗi hiển thị Grid
                if (_currentParams.Count == 0) dgvParams.DataSource = null;
            }
        }

        private void dgvParams_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _currentParams.Count) return;

            var paramToEdit = _currentParams[e.RowIndex];
            using (var frm = new AddEditParameterForm(paramToEdit, isFixedName: false))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    // BindingList tự update UI khi object thay đổi (nếu object implement INotifyPropertyChanged)
                    // Hoặc ta gán lại thủ công:
                    _currentParams[e.RowIndex] = frm.ResultParameter;
                }
            }
        }

        private void btnLuuMau_Click_1(object sender, EventArgs e)
        {
            // 1. KIỂM TRA NHẬP LIỆU (VALIDATION)
            if (string.IsNullOrWhiteSpace(txtNenMau.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên Nền Mẫu!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNenMau.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtViTri.Text))
            {
                MessageBox.Show("Vui lòng nhập Vị Trí Quan Trắc!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtViTri.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtKyHieu.Text))
            {
                MessageBox.Show("Vui lòng nhập Ký Hiệu!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKyHieu.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtToaDo.Text))
            {
                MessageBox.Show("Vui lòng nhập Tọa Độ!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtToaDo.Focus();
                return;
            }

            // 2. Kiểm tra danh sách thông số (Tùy chọn: bắt buộc phải có ít nhất 1 thông số)
            if (_currentParams.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ít nhất một thông số!", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string x = "", y = "";
            string rawToaDo = txtToaDo.Text.Trim();
            if (rawToaDo.Contains(","))
            {
                var parts = rawToaDo.Split(',');
                if (parts.Length >= 1) x = parts[0].Trim();
                if (parts.Length >= 2) y = parts[1].Trim();
            }
            else { x = rawToaDo; }

            ResultSample = new SampleDTO
            {
                BaseTemplateID = _baseTemplateId,
                TenNenMau = txtNenMau.Text.Trim(),
                KyHieuMau = txtKyHieu.Text.Trim(),
                ViTri = txtViTri.Text.Trim(),
                ToaDoX = x,
                ToaDoY = y,
                Parameters = _currentParams.ToList() // Convert BindingList về List thường cho DTO
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click_1(object sender, EventArgs e)
        {
            // Nút Hủy (roundedButton3)
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // --- CÁC NÚT KHÁC ---
        private void btnThemThongSo_Click_1(object sender, EventArgs e)
        {
            // roundedButton2
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