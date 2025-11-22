using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Environmental_Monitoring.Controller.Data;
using System.Resources;
using System.Globalization;
using System.Threading;
using Environmental_Monitoring.Model;
using Environmental_Monitoring.Controller;
using Environmental_Monitoring.View.Components;

namespace Environmental_Monitoring.View.ContractContent
{
    public partial class PlanContent : UserControl
    {
        private int currentContractId;
        private const string CHECKBOX_COLUMN_NAME = "SelectParameter";

        private Dictionary<int, HashSet<int>> _selectedParamsMap = new Dictionary<int, HashSet<int>>();
        private int _currentViewingTemplateId = -1;

        private ResourceManager rm;
        private CultureInfo culture;

        public PlanContent()
        {
            InitializeComponent();
            InitializeLocalization();
        }

        private void InitializeLocalization()
        {
            rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(PlanContent).Assembly);
            culture = Thread.CurrentThread.CurrentUICulture;
        }

        /// <summary>
        /// Phương thức được gọi từ Contract.cs thông qua Reflection khi nhấn Enter ở ô tìm kiếm.
        /// CẬP NHẬT: Xử lý ký tự đặc biệt (%, *, [, ]) để tìm kiếm chính xác.
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        public void PerformSearch(string keyword)
        {
            // Kiểm tra xem Grid có dữ liệu DataTable không để thực hiện lọc
            if (roundedDataGridView1.DataSource is DataTable dt)
            {
                try
                {
                    // 1. Áp dụng bộ lọc (Filter)
                    if (string.IsNullOrWhiteSpace(keyword))
                    {
                        // Nếu từ khóa rỗng, bỏ lọc -> hiện tất cả
                        dt.DefaultView.RowFilter = string.Empty;
                    }
                    else
                    {
                        // XỬ LÝ QUAN TRỌNG: Escape các ký tự đặc biệt của RowFilter
                        // % -> [%], [ -> [[]], ] -> []], * -> [*], ' -> ''
                        string safeKeyword = keyword.Replace("'", "''")
                                                    .Replace("[", "[[]")
                                                    .Replace("]", "[]]")
                                                    .Replace("%", "[%]")
                                                    .Replace("*", "[*]")
                                                    .Trim();

                        string lowerKey = keyword.ToLower(); // Dùng bản gốc để check mapping từ khóa

                        // Tạo danh sách các điều kiện lọc (OR)
                        List<string> filterParts = new List<string>();

                        // a. Tìm theo Tên Thông Số
                        filterParts.Add($"TenThongSo LIKE '%{safeKeyword}%'");

                        // b. Tìm theo Đơn Vị (Mới)
                        filterParts.Add($"DonVi LIKE '%{safeKeyword}%'");

                        // c. Tìm theo Min/Max
                        filterParts.Add($"Convert(GioiHanMin, 'System.String') LIKE '%{safeKeyword}%'");
                        filterParts.Add($"Convert(GioiHanMax, 'System.String') LIKE '%{safeKeyword}%'");

                        // d. Tìm theo Phụ Trách
                        filterParts.Add($"PhuTrach LIKE '%{safeKeyword}%'");

                        // Mapping thông minh cho Phòng Phụ Trách
                        if (lowerKey.Contains("hiện") || lowerKey.Contains("hien") ||
                            lowerKey.Contains("trường") || lowerKey.Contains("truong") ||
                            lowerKey.Contains("scene") || lowerKey.Contains("field"))
                        {
                            filterParts.Add("PhuTrach = 'HienTruong'");
                            filterParts.Add("PhuTrach = 'Field'");
                            filterParts.Add("PhuTrach = 'Scene'");
                        }

                        if (lowerKey.Contains("thí") || lowerKey.Contains("thi") ||
                            lowerKey.Contains("nghiệm") || lowerKey.Contains("nghiem") ||
                            lowerKey.Contains("lab"))
                        {
                            filterParts.Add("PhuTrach = 'ThiNghiem'");
                            filterParts.Add("PhuTrach = 'Laboratory'");
                        }

                        // Kết hợp tất cả điều kiện bằng toán tử OR
                        dt.DefaultView.RowFilter = string.Join(" OR ", filterParts);
                    }

                    // 2. Khôi phục trạng thái Checkbox
                    if (_currentViewingTemplateId != -1 && _selectedParamsMap.ContainsKey(_currentViewingTemplateId))
                    {
                        HashSet<int> selectedForThis = _selectedParamsMap[_currentViewingTemplateId];

                        foreach (DataGridViewRow row in roundedDataGridView1.Rows)
                        {
                            if (row.Cells["ParameterID"].Value != null)
                            {
                                int pId = Convert.ToInt32(row.Cells["ParameterID"].Value);
                                bool isSelected = selectedForThis.Contains(pId);
                                row.Cells[CHECKBOX_COLUMN_NAME].Value = isSelected;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Filter Error: " + ex.Message);
                }
            }
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
                string title = (type == AlertPanel.AlertType.Success) ? rm.GetString("Alert_SuccessTitle", culture) : rm.GetString("Alert_ErrorTitle", culture);
                MessageBox.Show(message, title, MessageBoxButtons.OK,
                    (type == AlertPanel.AlertType.Success) ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            }
        }

        // ... [Keep Helper methods GetLocalizedDepartment, GetLocalizedTemplateName] ...
        private string GetLocalizedDepartment(string dbValue)
        {
            if (string.IsNullOrEmpty(dbValue)) return "";
            if (dbValue.Equals("HienTruong", StringComparison.OrdinalIgnoreCase) || dbValue.Equals("Field", StringComparison.OrdinalIgnoreCase))
                return rm.GetString("Dept_Field", culture) ?? "Field";
            if (dbValue.Equals("ThiNghiem", StringComparison.OrdinalIgnoreCase) || dbValue.Equals("Laboratory", StringComparison.OrdinalIgnoreCase))
                return rm.GetString("Dept_Lab", culture) ?? "Laboratory";
            return dbValue;
        }

        private string GetLocalizedTemplateName(string dbName)
        {
            if (string.IsNullOrEmpty(dbName)) return "";
            string lowerName = dbName.ToLower();
            if (lowerName.Contains("không khí") || lowerName.Contains("air"))
                return rm.GetString("Template_Air", culture) ?? "Air Environment";
            if (lowerName.Contains("nước") || lowerName.Contains("water"))
                return rm.GetString("Template_Water", culture) ?? "Water Environment";
            if (lowerName.Contains("đất") || lowerName.Contains("soil"))
                return rm.GetString("Template_Soil", culture) ?? "Soil Environment";
            if (lowerName.Contains("tiếng ồn") || lowerName.Contains("độ rung") || lowerName.Contains("noise") || lowerName.Contains("vibration"))
                return rm.GetString("Template_Noise", culture) ?? "Noise & Vibration";
            return dbName;
        }

        public void UpdateUIText()
        {
            culture = Thread.CurrentThread.CurrentUICulture;

            if (lbContractID != null) lbContractID.Text = rm.GetString("Plan_ContractIDLabel", culture) + (currentContractId != 0 ? " " + currentContractId : "");
            if (btnContracts != null) btnContracts.Text = rm.GetString("Plan_ContractListButton", culture);
            if (btnAddParameter != null) btnAddParameter.Text = rm.GetString("Plan_AddParamButton", culture);
            if (roundedButton2 != null) roundedButton2.Text = rm.GetString("Button_Save", culture);

            var btnCancel = this.Controls.Find("btnCancel", true).FirstOrDefault();
            if (btnCancel != null) btnCancel.Text = rm.GetString("Button_Cancel", culture);

            if (label1 != null) label1.Text = rm.GetString("Plan_SampleTemplatesLabel", culture);
            if (label2 != null) label2.Text = rm.GetString("Plan_ParametersLabel", culture);

            UpdateGridHeaders();

            if (currentContractId != 0)
            {
                // Reload templates logic if needed, but usually kept static
            }
        }

        private void UpdateGridHeaders()
        {
            if (roundedDataGridView1.Columns.Contains(CHECKBOX_COLUMN_NAME))
                roundedDataGridView1.Columns[CHECKBOX_COLUMN_NAME].HeaderText = rm.GetString("Grid_Select", culture);
            if (roundedDataGridView1.Columns.Contains("TenThongSo"))
                roundedDataGridView1.Columns["TenThongSo"].HeaderText = rm.GetString("Grid_ParamName", culture);
            if (roundedDataGridView1.Columns.Contains("DonVi"))
                roundedDataGridView1.Columns["DonVi"].HeaderText = rm.GetString("Grid_Unit", culture);
            if (roundedDataGridView1.Columns.Contains("GioiHanMin"))
                roundedDataGridView1.Columns["GioiHanMin"].HeaderText = rm.GetString("Grid_Min", culture);
            if (roundedDataGridView1.Columns.Contains("GioiHanMax"))
                roundedDataGridView1.Columns["GioiHanMax"].HeaderText = rm.GetString("Grid_Max", culture);
            if (roundedDataGridView1.Columns.Contains("PhuTrach"))
                roundedDataGridView1.Columns["PhuTrach"].HeaderText = rm.GetString("Grid_Department", culture);
        }

        private void AddCheckboxColumnToGrid()
        {
            if (roundedDataGridView1.Columns.Contains(CHECKBOX_COLUMN_NAME)) return;

            Color defaultBackColor = roundedDataGridView1.DefaultCellStyle.BackColor;
            if (defaultBackColor.IsEmpty) defaultBackColor = Color.White;

            DataGridViewCheckBoxColumn chkCol = new DataGridViewCheckBoxColumn
            {
                Name = CHECKBOX_COLUMN_NAME,
                HeaderText = rm.GetString("Grid_Select", culture),
                Width = 60,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Frozen = true,
                DefaultCellStyle = { SelectionBackColor = defaultBackColor, Alignment = DataGridViewContentAlignment.MiddleCenter }
            };
            roundedDataGridView1.Columns.Add(chkCol);
            roundedDataGridView1.Columns[CHECKBOX_COLUMN_NAME].DisplayIndex = 0;
        }

        private void PlanContent_Load(object sender, EventArgs e)
        {
            // --- Cấu hình giao diện ---
            roundedDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Đăng ký sự kiện cho các nút (Đảm bảo nút Save được đăng ký)
            btnAddParameter.Click -= btnAddParameter_Click;
            btnAddParameter.Click += btnAddParameter_Click;

            if (roundedButton2 != null)
            {
                roundedButton2.Click -= roundedButton2_Click; // Xóa sự kiện cũ nếu có
                roundedButton2.Click += roundedButton2_Click; // Đăng ký sự kiện mới
            }

            roundedDataGridView1.CellDoubleClick += roundedDataGridView1_CellDoubleClick;
            roundedDataGridView1.CellContentClick -= roundedDataGridView1_CellContentClick;
            roundedDataGridView1.CellContentClick += roundedDataGridView1_CellContentClick;
            roundedDataGridView1.CurrentCellDirtyStateChanged += RoundedDataGridView1_CurrentCellDirtyStateChanged;
            roundedDataGridView1.CellMouseEnter += roundedDataGridView1_CellMouseEnter;
            roundedDataGridView1.CellMouseLeave += roundedDataGridView1_CellMouseLeave;
            roundedDataGridView1.CellFormatting += roundedDataGridView1_CellFormatting;

            roundedDataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            roundedDataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            roundedDataGridView1.RowHeadersVisible = false;
            roundedDataGridView1.AllowUserToAddRows = false;

            AddCheckboxColumnToGrid();

            if (roundedDataGridView1.Columns.Contains(CHECKBOX_COLUMN_NAME))
            {
                roundedDataGridView1.Columns[CHECKBOX_COLUMN_NAME].Visible = false;
            }

            checkedListBox1.CheckOnClick = true;
            checkedListBox1.SelectedIndexChanged -= checkedListBox1_SelectedIndexChanged_1;
            checkedListBox1.SelectedIndexChanged += checkedListBox1_SelectedIndexChanged_1;
            checkedListBox1.ItemCheck -= checkedListBox1_ItemCheck;
            checkedListBox1.ItemCheck += checkedListBox1_ItemCheck;
            checkedListBox1.MouseUp += CheckedListBox1_MouseUp;
            checkedListBox1.SelectionMode = SelectionMode.One;
            checkedListBox1.Cursor = Cursors.Hand;

            try
            {
                label1.Visible = true; label1.ForeColor = Color.Black; label1.BringToFront();
                label2.Visible = true; label2.ForeColor = Color.Black; label2.BringToFront();
            }
            catch { }

            roundedDataGridView1.DefaultCellStyle.ForeColor = Color.Black;

            var btnCancel = this.Controls.Find("btnCancel", true).FirstOrDefault();
            if (btnCancel != null) btnCancel.Click += btnCancel_Click;

            UpdateUIText();
            ResetForm();

            // Đảm bảo các nút hiển thị
            if (roundedButton2 != null) roundedButton2.Visible = true;
            if (btnAddParameter != null) btnAddParameter.Visible = true;
            if (btnCancel != null) btnCancel.Visible = true;
        }

        private void UpdateAddButtonState()
        {
            bool isContractSelected = (currentContractId != 0);
            bool isAnyTemplateChecked = (checkedListBox1.CheckedItems.Count > 0);

            if (btnAddParameter != null)
            {
                btnAddParameter.Enabled = (isContractSelected && isAnyTemplateChecked);
            }

            // SỬA LỖI: Bật/Tắt nút Save dựa trên việc đã chọn hợp đồng hay chưa
            if (roundedButton2 != null)
            {
                roundedButton2.Enabled = isContractSelected;
            }
        }

        private void roundedDataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 &&
                roundedDataGridView1.Columns[e.ColumnIndex].Name == "PhuTrach" && e.Value != null)
            {
                string originalValue = e.Value.ToString();
                e.Value = GetLocalizedDepartment(originalValue);
                e.FormattingApplied = true;
            }
        }

        private void roundedDataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && roundedDataGridView1.Columns[e.ColumnIndex].Name == CHECKBOX_COLUMN_NAME)
                roundedDataGridView1.Cursor = Cursors.Hand;
        }

        private void roundedDataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            roundedDataGridView1.Cursor = Cursors.Default;
        }

        private void CheckedListBox1_MouseUp(object sender, MouseEventArgs e)
        {
            int index = checkedListBox1.IndexFromPoint(e.Location);
            if (index != -1)
            {
                var item = checkedListBox1.Items[index] as SampleTemplateDisplayItem;
                if (item != null) LoadParametersForSingleTemplate(item.TemplateID);
            }
        }

        private void ResetForm()
        {
            currentContractId = 0;
            _selectedParamsMap.Clear();
            _currentViewingTemplateId = -1;
            if (rm != null) lbContractID.Text = rm.GetString("Plan_ContractIDLabel", culture);
            checkedListBox1.DataSource = null;
            checkedListBox1.Items.Clear();
            roundedDataGridView1.DataSource = null;
            if (roundedDataGridView1.Columns.Contains(CHECKBOX_COLUMN_NAME))
                roundedDataGridView1.Columns[CHECKBOX_COLUMN_NAME].Visible = false;
            ClearParameterControls();
            UpdateAddButtonState();
        }

        private void RoundedDataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (roundedDataGridView1.IsCurrentCellDirty) roundedDataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void roundedDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != roundedDataGridView1.Columns[CHECKBOX_COLUMN_NAME].Index) return;
            if (_currentViewingTemplateId == -1) return;

            bool isChecked = Convert.ToBoolean(roundedDataGridView1.Rows[e.RowIndex].Cells[CHECKBOX_COLUMN_NAME].Value);
            int paramId = Convert.ToInt32(roundedDataGridView1.Rows[e.RowIndex].Cells["ParameterID"].Value);

            if (!_selectedParamsMap.ContainsKey(_currentViewingTemplateId))
                _selectedParamsMap[_currentViewingTemplateId] = new HashSet<int>();

            if (isChecked) _selectedParamsMap[_currentViewingTemplateId].Add(paramId);
            else _selectedParamsMap[_currentViewingTemplateId].Remove(paramId);
        }

        private void checkedListBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (checkedListBox1.SelectedItem is SampleTemplateDisplayItem selectedItem)
                {
                    LoadParametersForSingleTemplate(selectedItem.TemplateID);
                }
            }
            catch { }
        }

        private void LoadParametersForSingleTemplate(int templateId)
        {
            try
            {
                _currentViewingTemplateId = templateId;

                string q = @"SELECT p.ParameterID, p.TenThongSo, p.DonVi, p.GioiHanMin, p.GioiHanMax, p.PhuTrach
                             FROM TemplateParameters tp
                             JOIN Parameters p ON tp.ParameterID = p.ParameterID
                             WHERE tp.TemplateID = @templateID";

                DataTable dt = DataProvider.Instance.ExecuteQuery(q, new object[] { templateId });

                if (dt == null || dt.Rows.Count == 0)
                {
                    roundedDataGridView1.DataSource = null;
                    if (roundedDataGridView1.Columns.Contains(CHECKBOX_COLUMN_NAME))
                        roundedDataGridView1.Columns[CHECKBOX_COLUMN_NAME].Visible = false;
                    ClearParameterControls();
                    return;
                }

                roundedDataGridView1.DataSource = dt;

                if (roundedDataGridView1.Columns.Contains(CHECKBOX_COLUMN_NAME))
                    roundedDataGridView1.Columns[CHECKBOX_COLUMN_NAME].Visible = true;

                roundedDataGridView1.RowHeadersVisible = false;

                if (roundedDataGridView1.Columns["ParameterID"] != null) roundedDataGridView1.Columns["ParameterID"].Visible = false;
                if (roundedDataGridView1.Columns.Contains("GiaTri")) roundedDataGridView1.Columns["GiaTri"].Visible = false;

                UpdateGridHeaders();

                if (_selectedParamsMap.ContainsKey(templateId))
                {
                    HashSet<int> selectedForThis = _selectedParamsMap[templateId];
                    foreach (DataGridViewRow row in roundedDataGridView1.Rows)
                    {
                        int pId = Convert.ToInt32(row.Cells["ParameterID"].Value);
                        if (selectedForThis.Contains(pId)) row.Cells[CHECKBOX_COLUMN_NAME].Value = true;
                        else row.Cells[CHECKBOX_COLUMN_NAME].Value = false;
                    }
                }
                else
                {
                    foreach (DataGridViewRow row in roundedDataGridView1.Rows) row.Cells[CHECKBOX_COLUMN_NAME].Value = false;
                }

                ClearParameterControls();
            }
            catch (Exception ex)
            {
                ShowAlert(rm.GetString("Error_LoadParameters", culture) + ": " + ex.Message, AlertPanel.AlertType.Error);
            }
        }

        // --- XỬ LÝ NÚT LƯU ---
        private void roundedButton2_Click(object sender, EventArgs e)
        {
            if (currentContractId == 0)
            {
                ShowAlert(rm.GetString("Plan_SelectContract", culture), AlertPanel.AlertType.Error);
                return;
            }

            var selectedBaseTemplates = checkedListBox1.CheckedItems.OfType<SampleTemplateDisplayItem>().ToList();

            if (selectedBaseTemplates.Count == 0)
            {
                ShowAlert(rm.GetString("Plan_SelectSampleTemplate", culture), AlertPanel.AlertType.Error);
                return;
            }

            // Kiểm tra xem mẫu đã chọn có thông số nào được tick chưa
            foreach (var tpl in selectedBaseTemplates)
            {
                if (!_selectedParamsMap.ContainsKey(tpl.TemplateID) || _selectedParamsMap[tpl.TemplateID].Count == 0)
                {
                    string templateName = tpl.TenMau;
                    string errorMsg = culture.Name == "vi-VN"
                        ? $"Mẫu '{templateName}' được chọn nhưng chưa có thông số nào được chọn."
                        : $"Template '{templateName}' is selected but no parameters are selected.";
                    ShowAlert(errorMsg, AlertPanel.AlertType.Error);
                    return;
                }
            }

            try
            {
                // 1. Lấy mã đơn từ ID hợp đồng
                string maDon = DataProvider.Instance.ExecuteScalar("SELECT MaDon FROM Contracts WHERE ContractID = @contractId", new object[] { currentContractId }).ToString();
                if (string.IsNullOrEmpty(maDon))
                {
                    ShowAlert(rm.GetString("Plan_ContractCodeNotFound", culture), AlertPanel.AlertType.Error);
                    return;
                }

                // 2. Duyệt qua các mẫu được chọn để tạo EnvironmentalSamples
                foreach (var baseTemplate in selectedBaseTemplates)
                {
                    string sampleCode = $"{maDon} - {baseTemplate.TenMau}";
                    int baseTemplateId = baseTemplate.TemplateID;

                    // Kiểm tra trùng
                    string checkSampleQuery = "SELECT COUNT(*) FROM EnvironmentalSamples WHERE MaMau = @mamau AND ContractID = @contractId";
                    object sampleExists = DataProvider.Instance.ExecuteScalar(checkSampleQuery, new object[] { sampleCode, currentContractId });
                    if (Convert.ToInt32(sampleExists) > 0) continue;

                    // Tạo Template riêng cho mẫu này
                    string newSpecificTemplateName = $"Template cho {maDon} - {baseTemplate.TenMau}";
                    string insertTemplateQuery = "INSERT INTO SampleTemplates (TenMau) VALUES (@tenmau)";
                    DataProvider.Instance.ExecuteNonQuery(insertTemplateQuery, new object[] { newSpecificTemplateName });

                    int newSpecificTemplateId = Convert.ToInt32(DataProvider.Instance.ExecuteScalar("SELECT LAST_INSERT_ID()", null));

                    // Copy các thông số đã tick sang Template mới
                    if (_selectedParamsMap.ContainsKey(baseTemplateId))
                    {
                        HashSet<int> paramsForThisSample = _selectedParamsMap[baseTemplateId];

                        foreach (int paramId in paramsForThisSample)
                        {
                            string insertParamLinkQuery = "INSERT INTO TemplateParameters (TemplateID, ParameterID) VALUES (@TID, @PID)";
                            DataProvider.Instance.ExecuteNonQuery(insertParamLinkQuery, new object[] { newSpecificTemplateId, paramId });
                        }
                    }

                    // Insert mẫu vào bảng EnvironmentalSamples
                    string insertSampleQuery = "INSERT INTO EnvironmentalSamples (MaMau, ContractID, TemplateID) VALUES (@mamau, @contractId, @templateId)";
                    DataProvider.Instance.ExecuteNonQuery(insertSampleQuery, new object[] { sampleCode, currentContractId, newSpecificTemplateId });
                }

                // 3. CẬP NHẬT TIẾN TRÌNH HỢP ĐỒNG (Sang bước 2 - Hiện trường)
                string updateContractQuery = @"UPDATE Contracts SET TienTrinh = 2 WHERE ContractID = @contractId;";
                DataProvider.Instance.ExecuteNonQuery(updateContractQuery, new object[] { this.currentContractId });

                // 4. Tạo thông báo cho phòng Hiện trường
                int hienTruongRoleID = 10;
                string noiDungHienTruong = $"Hợp đồng '{maDon}' đã lập kế hoạch xong, cần lấy mẫu hiện trường.";
                NotificationService.CreateNotification("ChinhSua", noiDungHienTruong, hienTruongRoleID, this.currentContractId, null);

                string successMsg = string.Format(rm.GetString("Plan_SaveSuccess", culture), maDon);
                ShowAlert(successMsg, AlertPanel.AlertType.Success);

                ResetForm();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Duplicate entry") && ex.Message.Contains("MaMau"))
                {
                    ShowAlert(rm.GetString("Plan_DuplicateSampleError", culture), AlertPanel.AlertType.Error);
                }
                else
                {
                    ShowAlert(rm.GetString("Error_SavePlan", culture) + ": " + ex.Message, AlertPanel.AlertType.Error);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void btnContracts_Click(object sender, EventArgs e)
        {
            try
            {
                string query = @"SELECT ContractID, MaDon, NgayKy, NgayTraKetQua, Status FROM Contracts WHERE TienTrinh = 1";
                DataTable dt = DataProvider.Instance.ExecuteQuery(query);
                using (PopUpContract popup = new PopUpContract(dt))
                {
                    popup.ContractSelected += (contractId) =>
                    {
                        this.currentContractId = contractId;
                        lbContractID.Text = rm.GetString("Plan_ContractIDLabel", culture) + " " + contractId.ToString();

                        LoadSampleTemplates();

                        UpdateAddButtonState(); // Cập nhật trạng thái nút ngay khi chọn HĐ
                    };
                    popup.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ShowAlert(rm.GetString("Error_LoadContracts", culture) + ": " + ex.Message, AlertPanel.AlertType.Error);
            }
        }

        private void LoadSampleTemplates()
        {
            try
            {
                string q = "SELECT TemplateID, TenMau FROM SampleTemplates WHERE TenMau NOT LIKE 'Template cho %'";
                DataTable dt = DataProvider.Instance.ExecuteQuery(q);

                checkedListBox1.BeginUpdate();
                try
                {
                    checkedListBox1.DataSource = null;
                    checkedListBox1.Items.Clear();

                    if (dt == null || dt.Rows.Count == 0) { ClearParameterControls(); return; }

                    var list = dt.AsEnumerable().Select(r => new SampleTemplateDisplayItem
                    {
                        TemplateID = r.Field<int>("TemplateID"),
                        TenMau = GetLocalizedTemplateName(r.Field<string>("TenMau"))
                    }).ToList();

                    checkedListBox1.DataSource = list;
                    checkedListBox1.DisplayMember = "TenMau";
                    checkedListBox1.ValueMember = "TemplateID";

                    for (int i = 0; i < checkedListBox1.Items.Count; i++) checkedListBox1.SetItemChecked(i, false);

                    ClearParameterControls();
                    roundedDataGridView1.DataSource = null;
                    if (roundedDataGridView1.Columns.Contains(CHECKBOX_COLUMN_NAME))
                        roundedDataGridView1.Columns[CHECKBOX_COLUMN_NAME].Visible = false;
                }
                finally { checkedListBox1.EndUpdate(); }
            }
            catch (Exception ex) { ShowAlert(rm.GetString("Error_LoadSampleTemplates", culture) + ": " + ex.Message, AlertPanel.AlertType.Error); }
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
            {
                UpdateAddButtonState();
            });
        }

        private void ClearParameterControls()
        {
            lblParamNameValue.Text = string.Empty;
            lblUnitValue.Text = string.Empty;
            lblDeptValue.Text = string.Empty;
        }

        private void roundedDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                if (e.RowIndex == -1 && e.ColumnIndex == roundedDataGridView1.Columns[CHECKBOX_COLUMN_NAME].Index) return;

                var row = roundedDataGridView1.Rows[e.RowIndex];
                lblParamNameValue.Text = row.Cells["TenThongSo"].Value?.ToString() ?? string.Empty;
                lblUnitValue.Text = row.Cells["DonVi"].Value?.ToString() ?? string.Empty;

                string rawDept = row.Cells["PhuTrach"].Value?.ToString() ?? string.Empty;
                lblDeptValue.Text = GetLocalizedDepartment(rawDept);
            }
            catch { }
        }

        private void btnAddParameter_Click(object sender, EventArgs e)
        {
            using (var dlg = new AddEditParameterForm())
            {
                dlg.SetPhuTrachOptions(new[] { "HienTruong", "ThiNghiem" });

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ShowAlert(rm.GetString("Plan_AddParamSuccess", culture) ?? "Thêm thông số thành công.", AlertPanel.AlertType.Success);
                    ReloadCurrentParameters();
                }
            }
        }

        private void ReloadCurrentParameters()
        {
            if (_currentViewingTemplateId != -1)
            {
                LoadParametersForSingleTemplate(_currentViewingTemplateId);
            }
        }

        private void roundedDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex == roundedDataGridView1.Columns[CHECKBOX_COLUMN_NAME].Index) return;
            var row = roundedDataGridView1.Rows[e.RowIndex];
            int paramId = Convert.ToInt32(row.Cells["ParameterID"].Value);
            Parameter param = null;
            try
            {
                string query = "SELECT * FROM Parameters WHERE ParameterID = @id";
                DataTable dt = DataProvider.Instance.ExecuteQuery(query, new object[] { paramId });
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    param = new Parameter { ParameterID = Convert.ToInt32(dr["ParameterID"]), TenThongSo = dr["TenThongSo"].ToString(), DonVi = dr["DonVi"].ToString(), GioiHanMin = (dr["GioiHanMin"] == DBNull.Value) ? (decimal?)null : Convert.ToDecimal(dr["GioiHanMin"]), GioiHanMax = (dr["GioiHanMax"] == DBNull.Value) ? (decimal?)null : Convert.ToDecimal(dr["GioiHanMax"]), PhuTrach = dr["PhuTrach"].ToString() };
                }
            }
            catch (Exception ex) { ShowAlert(rm.GetString("Error_LoadParamInfo", culture) + ": " + ex.Message, AlertPanel.AlertType.Error); return; }
            if (param == null) return;

            using (var dlg = new AddEditParameterForm(param))
            {
                dlg.SetPhuTrachOptions(new[] { "HienTruong", "ThiNghiem" });
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ReloadCurrentParameters();
                }
            }
        }

        private void lbCustomer_Click(object sender, EventArgs e) { }
        private void lbContract_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void cbbParameters_SelectedIndexChanged(object sender, EventArgs e) { }
        private void checkedListBox1_ItemCheck_1(object sender, ItemCheckEventArgs e) { }

        public class SampleTemplateDisplayItem
        {
            public int TemplateID { get; set; }
            public string TenMau { get; set; }
            public override string ToString() { return TenMau; }
        }
    }
}