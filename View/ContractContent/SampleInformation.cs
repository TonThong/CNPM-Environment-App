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
        public SampleDTO ResultSample { get; private set; }
        private BindingList<ParameterDTO> _currentParams;
        private int _baseTemplateId;
        private string _baseTemplateName;
        private bool _isReady = false;

        public SampleInformation(int baseTemplateId, string baseTemplateName, SampleDTO editSample = null)
        {
            InitializeComponent();

            this.btnLuuMau.Click += new EventHandler(btnLuuMau_Click_1);
            this.btnHuy.Click += new EventHandler(btnHuy_Click_1);

            _baseTemplateId = baseTemplateId;
            _baseTemplateName = baseTemplateName;

            if (editSample != null && editSample.Parameters != null)
            {
                _currentParams = new BindingList<ParameterDTO>(editSample.Parameters);
            }
            else
            {
                _currentParams = new BindingList<ParameterDTO>();
            }

            SetupUI(editSample);
            SetupGridView();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            BindParamsGrid();
            LoadComboBoxDataSafe();     // Load thông số
            LoadNenMauComboBox();       // <--- MỚI: Load danh sách tên nền mẫu

            _isReady = true;
        }

        // --- HÀM MỚI: LOAD TÊN NỀN MẪU TỪ DB ---
        private void LoadNenMauComboBox()
        {
            try
            {
                if (cbbNenMau == null) return;

                // Query lấy dữ liệu Duy Nhất (DISTINCT) để tránh trùng lặp
                // Giả sử bảng lưu tên nền mẫu là EnvironmentalSamples
                string query = "SELECT DISTINCT TenNenMau FROM EnvironmentalSamples WHERE TenNenMau IS NOT NULL AND TenNenMau <> '' ORDER BY TenNenMau ASC";

                DataTable dt = DataProvider.Instance.ExecuteQuery(query);

                cbbNenMau.Items.Clear();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        cbbNenMau.Items.Add(row["TenNenMau"].ToString());
                    }
                }

                // Cấu hình gợi ý khi gõ (Auto Complete)
                //cbbNenMau.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //cbbNenMau.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách nền mẫu: " + ex.Message);
            }
        }

        private void SetupUI(SampleDTO editSample)
        {
            // SỬA: Dùng cbbNenMau thay vì txtNenMau
            if (cbbNenMau != null)
            {
                // Nếu đang sửa thì hiện tên cũ, nếu thêm mới thì hiện tên template mặc định
                cbbNenMau.Text = editSample != null ? editSample.TenNenMau : _baseTemplateName;
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

            dgvParams.CellContentClick += dgvParams_CellContentClick;
            dgvParams.CellDoubleClick += dgvParams_CellDoubleClick;
        }

        private void BindParamsGrid()
        {
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
            catch (Exception ex) { }
            finally
            {
                cbbThongSo.SelectedIndexChanged += cbbThongSo_SelectedIndexChanged;
            }
        }

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
                int index = _currentParams.IndexOf(existing);
                _currentParams[index] = newParam;
            }
            else
            {
                _currentParams.Add(newParam);
            }

            if (dgvParams.DataSource == null) BindParamsGrid();
        }

        private void dgvParams_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvParams.Columns[e.ColumnIndex].Name == "colDelete")
            {
                _currentParams.RemoveAt(e.RowIndex);
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
                    _currentParams[e.RowIndex] = frm.ResultParameter;
                    dgvParams.InvalidateRow(e.RowIndex);
                }
            }
        }

        private void btnLuuMau_Click_1(object sender, EventArgs e)
        {
            // 1. KIỂM TRA NHẬP LIỆU VỚI COMBOBOX
            // SỬA: Kiểm tra cbbNenMau.Text thay vì txtNenMau.Text
            if (string.IsNullOrWhiteSpace(cbbNenMau.Text))
            {
                MessageBox.Show("Vui lòng nhập hoặc chọn Tên Nền Mẫu!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbbNenMau.Focus();
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
                // SỬA: Lấy giá trị từ ComboBox
                TenNenMau = cbbNenMau.Text.Trim(),
                KyHieuMau = txtKyHieu.Text.Trim(),
                ViTri = txtViTri.Text.Trim(),
                ToaDoX = x,
                ToaDoY = y,
                Parameters = _currentParams.ToList()
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnThemThongSo_Click_1(object sender, EventArgs e)
        {
            using (var frm = new AddEditParameterForm(null, isFixedName: false))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    AddParameterSafe(frm.ResultParameter);
                }
            }
        }

        public class ParameterCbbItem
        {
            public string Text { get; set; }
            public DataRow RowData { get; set; }
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {
        }
    }
}