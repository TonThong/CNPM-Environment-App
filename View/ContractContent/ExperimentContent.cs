using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Environmental_Monitoring.Controller;
using Environmental_Monitoring.View.ContractContent;

namespace Environmental_Monitoring.View
{
    public partial class ExperimentContent : UserControl
    {
        private int currentContractId = 0;
        private bool isUpdatingCell = false;

        // Culture-aware decimal parser: prefer vi-VN when comma present to avoid treating comma as thousands separator
        private bool TryParseDecimalString(string s, out decimal result)
        {
            result = 0;
            if (string.IsNullOrWhiteSpace(s)) return false;
            s = s.Trim();

            bool hasComma = s.Contains(",");
            bool hasDot = s.Contains(".");
            var styles = System.Globalization.NumberStyles.Number;

            if (hasComma && !hasDot)
            {
                if (decimal.TryParse(s, styles, new System.Globalization.CultureInfo("vi-VN"), out result)) return true;
                if (decimal.TryParse(s, styles, System.Globalization.CultureInfo.InvariantCulture, out result)) return true;
            }
            else if (hasDot && !hasComma)
            {
                if (decimal.TryParse(s, styles, System.Globalization.CultureInfo.InvariantCulture, out result)) return true;
                if (decimal.TryParse(s, styles, new System.Globalization.CultureInfo("vi-VN"), out result)) return true;
            }
            else
            {
                if (decimal.TryParse(s, styles, System.Globalization.CultureInfo.CurrentCulture, out result)) return true;
                if (decimal.TryParse(s, styles, new System.Globalization.CultureInfo("vi-VN"), out result)) return true;
                if (decimal.TryParse(s, styles, System.Globalization.CultureInfo.InvariantCulture, out result)) return true;
            }

            return false;
        }

        public ExperimentContent()
        {
            InitializeComponent();
            roundedDataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            roundedDataGridView2.ForeColor = Color.Black;

            // ensure checkbox edits commit immediately
            roundedDataGridView2.CurrentCellDirtyStateChanged += RoundedDataGridView2_CurrentCellDirtyStateChanged;
            roundedDataGridView2.CellValueChanged += RoundedDataGridView2_CellValueChanged;

            // wire create template button if present in designer
            try
            {
                if (this.Controls.Find("btnCreateTemplate", true).Length > 0)
                {
                    btnCreateTemplate.Click += BtnCreateTemplate_Click;
                }
            }
            catch { }

            // wire CellEndEdit for GiaTri save
            roundedDataGridView2.CellEndEdit += RoundedDataGridView2_CellEndEdit_SaveGiaTri;
        }

        private void RoundedDataGridView2_CellEndEdit_SaveGiaTri(object? sender, DataGridViewCellEventArgs e)
        {
            if (isUpdatingCell || e.RowIndex < 0 || e.ColumnIndex < 0) return;

            isUpdatingCell = true;
            try
            {
                var col = roundedDataGridView2.Columns[e.ColumnIndex];
                if (col == null || col.Name != "GiaTri") return; // only handle GiaTri edits

                var row = roundedDataGridView2.Rows[e.RowIndex];
                object val = row.Cells["GiaTri"].Value;
                if (val == null || val == DBNull.Value || string.IsNullOrWhiteSpace(val.ToString()))
                    return; // nothing to save

                // parse decimal with culture-aware helper to handle "11,00" correctly
                if (!TryParseDecimalString(val.ToString(), out var giaTri))
                {
                    MessageBox.Show("Giá trị nhập không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (roundedDataGridView2.Columns.Contains("ParameterID"))
                {
                    int parameterId = Convert.ToInt32(row.Cells["ParameterID"].Value);

                    // find a sample for this contract (use first sample as target)
                    string sampleQ = "SELECT SampleID FROM EnvironmentalSamples WHERE ContractID = @contractId LIMIT 1";
                    object sampleObj = DataProvider.Instance.ExecuteScalar(sampleQ, new object[] { currentContractId });
                    if (sampleObj == null || !int.TryParse(sampleObj.ToString(), out int sampleId))
                    {
                        MessageBox.Show("Không tìm thấy mẫu để lưu giá trị.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // check existing result
                    string checkQ = "SELECT ResultID FROM Results WHERE SampleID = @sampleId AND ParameterID = @parameterId LIMIT 1";
                    object exist = DataProvider.Instance.ExecuteScalar(checkQ, new object[] { sampleId, parameterId });

                    if (exist != null && int.TryParse(exist.ToString(), out int resultId))
                    {
                        string updateQ = "UPDATE Results SET GiaTri = @giaTri, NgayPhanTich = CURRENT_TIMESTAMP WHERE ResultID = @resultId";
                        DataProvider.Instance.ExecuteNonQuery(updateQ, new object[] { giaTri, resultId });
                    }
                    else
                    {
                        string insertQ = "INSERT INTO Results (GiaTri, TrangThaiPheDuyet, NgayPhanTich, SampleID, ParameterID) VALUES (@giaTri, NULL, CURRENT_TIMESTAMP, @sampleId, @parameterId)";
                        DataProvider.Instance.ExecuteNonQuery(insertQ, new object[] { giaTri, sampleId, parameterId });
                    }

                    // optional: refresh DataGridView row status after save
                    BeginInvoke(new Action(() => LoadExperimentContract(currentContractId)));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu giá trị: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                isUpdatingCell = false;
            }
        }

        private void BtnCreateTemplate_Click(object? sender, EventArgs e)
        {
            if (currentContractId <= 0)
            {
                MessageBox.Show("Vui lòng chọn hợp đồng trước khi tạo mẫu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // load parameters where PhuTrach IS NULL OR = 'ThiNghiem'
            string q = "SELECT ParameterID, TenThongSo FROM Parameters WHERE PhuTrach IS NULL OR PhuTrach = 'ThiNghiem'";
            DataTable dt = DataProvider.Instance.ExecuteQuery(q);

            using (var dlg = new AddTemplateForm(dt))
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    // insert into SampleTemplates
                    string insertTemplate = "INSERT INTO SampleTemplates (TenMau) VALUES (@tenmau)";
                    DataProvider.Instance.ExecuteNonQuery(insertTemplate, new object[] { dlg.TemplateName });

                    // get last inserted template id
                    object last = DataProvider.Instance.ExecuteScalar("SELECT LAST_INSERT_ID()", null);
                    int templateId = 0;
                    if (last != null && int.TryParse(last.ToString(), out int tmp)) templateId = tmp;

                    if (templateId > 0)
                    {
                        // insert into TemplateParameters
                        foreach (var pid in dlg.SelectedParameterIds)
                        {
                            string ins = "INSERT INTO TemplateParameters (TemplateID, ParameterID) VALUES (@t, @p)";
                            DataProvider.Instance.ExecuteNonQuery(ins, new object[] { templateId, pid });
                        }

                        // create EnvironmentalSamples for this contract
                        string insertSample = "INSERT INTO EnvironmentalSamples (MaMau, ContractID, TemplateID) VALUES (@mamau, @contractId, @templateId)";
                        string sampleCode = $"M{currentContractId}_{templateId}_{DateTime.Now.Ticks % 10000}";
                        DataProvider.Instance.ExecuteNonQuery(insertSample, new object[] { sampleCode, currentContractId, templateId });

                        MessageBox.Show("Tạo mẫu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadExperimentContract(currentContractId);
                    }
                }
            }
        }

        private void btnCreateTemplate_Click(object? sender, EventArgs e)
        {
            // Designer-created event wiring calls this name; forward to implementation
            BtnCreateTemplate_Click(sender, e);
        }

        private void RoundedDataGridView2_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
        {
            if (roundedDataGridView2.IsCurrentCellDirty)
            {
                roundedDataGridView2.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void RoundedDataGridView2_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (isUpdatingCell || e.RowIndex < 0 || e.ColumnIndex < 0) return;

            isUpdatingCell = true;
            try
            {
                var colName = roundedDataGridView2.Columns[e.ColumnIndex].Name;
                var row = roundedDataGridView2.Rows[e.RowIndex];

                // enforce mutual exclusivity: Contaminated and Warning
                if (colName == "Contaminated")
                {
                    bool isCont = false;
                    var cell = row.Cells["Contaminated"];
                    if (cell.Value != null && bool.TryParse(cell.Value.ToString(), out bool b)) isCont = b;
                    // if contaminated, uncheck warning
                    if (isCont && roundedDataGridView2.Columns.Contains("Warning"))
                    {
                        row.Cells["Warning"].Value = false;
                    }
                }
                else if (colName == "Warning")
                {
                    bool isWarn = false;
                    var cell = row.Cells["Warning"];
                    if (cell.Value != null && bool.TryParse(cell.Value.ToString(), out bool b)) isWarn = b;
                    if (isWarn && roundedDataGridView2.Columns.Contains("Contaminated"))
                    {
                        row.Cells["Contaminated"].Value = false;
                    }
                }

                // update row styling
                BeginInvoke(new Action(() => UpdateRowStyle(e.RowIndex)));
            }
            finally
            {
                isUpdatingCell = false;
            }
        }

        private void UpdateRowStyle(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= roundedDataGridView2.Rows.Count) return;
            var row = roundedDataGridView2.Rows[rowIndex];
            bool isCont = false, isWarn = false;
            if (roundedDataGridView2.Columns.Contains("Contaminated"))
            {
                var c = row.Cells["Contaminated"].Value;
                if (c != null && bool.TryParse(c.ToString(), out bool b)) isCont = b;
            }
            if (roundedDataGridView2.Columns.Contains("Warning"))
            {
                var c = row.Cells["Warning"].Value;
                if (c != null && bool.TryParse(c.ToString(), out bool b)) isWarn = b;
            }

            if (isCont)
            {
                row.DefaultCellStyle.BackColor = Color.LightCoral;
                row.DefaultCellStyle.ForeColor = Color.Black;
            }
            else if (isWarn)
            {
                row.DefaultCellStyle.BackColor = Color.Orange;
                row.DefaultCellStyle.ForeColor = Color.Black;
            }
            else
            {
                row.DefaultCellStyle.BackColor = Color.LightGreen;
                row.DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        private void LoadExperimentContract(int contractId)
        {
            try
            {
                currentContractId = contractId;

                // For the given contract, list distinct parameters from samples' templates
                // and determine if any Result for that parameter in the contract is out of min/max
                // Also get an aggregated saved status flag (TrangThaiPheDuyet) if present
                string q = @"SELECT p.ParameterID, p.TenThongSo, p.DonVi, p.GioiHanMin, p.GioiHanMax,
                                MAX(CASE 
                                    WHEN r.GiaTri IS NULL THEN 0
                                    WHEN (p.GioiHanMin IS NOT NULL AND r.GiaTri < p.GioiHanMin) OR (p.GioiHanMax IS NOT NULL AND r.GiaTri > p.GioiHanMax) THEN 1
                                    ELSE 0 END) AS IsContaminated,
                                MAX(r.TrangThaiPheDuyet) AS StatusFlag,
                                MAX(r.GiaTri) AS GiaTri
                             FROM EnvironmentalSamples s
                             JOIN TemplateParameters tp ON s.TemplateID = tp.TemplateID
                             JOIN Parameters p ON tp.ParameterID = p.ParameterID
                             LEFT JOIN Results r ON r.SampleID = s.SampleID AND r.ParameterID = p.ParameterID
                             WHERE s.ContractID = @contractId
                               AND (p.PhuTrach IS NULL OR p.PhuTrach = 'ThiNghiem')
                             GROUP BY p.ParameterID, p.TenThongSo, p.DonVi, p.GioiHanMin, p.GioiHanMax";

                DataTable dt = DataProvider.Instance.ExecuteQuery(q, new object[] { contractId });

                if (dt == null)
                {
                    roundedDataGridView2.DataSource = null;
                    return;
                }

                // Convert StatusFlag/IsContaminated into two boolean editable columns: Contaminated and Warning
                if (!dt.Columns.Contains("Contaminated"))
                    dt.Columns.Add("Contaminated", typeof(bool));
                if (!dt.Columns.Contains("Warning"))
                    dt.Columns.Add("Warning", typeof(bool));

                foreach (DataRow row in dt.Rows)
                {
                    bool contaminated = false;
                    bool warning = false;

                    // prefer stored StatusFlag if available
                    if (dt.Columns.Contains("StatusFlag"))
                    {
                        var sf = row["StatusFlag"];
                        if (sf != DBNull.Value && sf != null && int.TryParse(sf.ToString(), out int sfv))
                        {
                            switch (sfv)
                            {
                                case 0: // Bad/Contaminated
                                    contaminated = true; warning = false; break;
                                case 2: // Warning
                                    contaminated = false; warning = true; break;
                                case 1: // Good
                                default:
                                    contaminated = false; warning = false; break;
                            }
                        }
                        else
                        {
                            // fallback to IsContaminated computed from values
                            var ic = row["IsContaminated"];
                            if (ic != DBNull.Value && ic != null && int.TryParse(ic.ToString(), out int icv))
                            {
                                contaminated = icv == 1;
                            }
                        }
                    }
                    else
                    {
                        var ic = row["IsContaminated"];
                        if (ic != DBNull.Value && ic != null && int.TryParse(ic.ToString(), out int icv))
                        {
                            contaminated = icv == 1;
                        }
                    }

                    row["Contaminated"] = contaminated;
                    row["Warning"] = warning;
                }

                // remove raw helper columns to avoid showing to user
                if (dt.Columns.Contains("IsContaminated")) dt.Columns.Remove("IsContaminated");
                if (dt.Columns.Contains("StatusFlag")) dt.Columns.Remove("StatusFlag");

                roundedDataGridView2.DataSource = dt;

                // Adjust headers and visibility
                if (roundedDataGridView2.Columns["ParameterID"] != null)
                    roundedDataGridView2.Columns["ParameterID"].Visible = false;
                if (roundedDataGridView2.Columns["TenThongSo"] != null)
                    roundedDataGridView2.Columns["TenThongSo"].HeaderText = "Tên thông số";
                if (roundedDataGridView2.Columns["DonVi"] != null)
                    roundedDataGridView2.Columns["DonVi"].HeaderText = "Đơn vị";
                if (roundedDataGridView2.Columns["GioiHanMin"] != null)
                    roundedDataGridView2.Columns["GioiHanMin"].HeaderText = "Min";
                if (roundedDataGridView2.Columns["GioiHanMax"] != null)
                    roundedDataGridView2.Columns["GioiHanMax"].HeaderText = "Max";

                // Replace/ensure Contaminated and Warning are checkbox columns and editable
                EnsureCheckBoxColumn("Contaminated", "Ô nhiễm", 0);
                EnsureCheckBoxColumn("Warning", "Cảnh báo sắp ô nhiễm", 1);

                // Ensure GiaTri column is editable and placed after parameter name
                if (roundedDataGridView2.Columns.Contains("GiaTri"))
                {
                    var gcol = roundedDataGridView2.Columns["GiaTri"];
                    gcol.ReadOnly = false;
                    int insertAt = Math.Min(2, roundedDataGridView2.Columns.Count);
                    roundedDataGridView2.Columns.Remove(gcol);
                    var newCol = new DataGridViewTextBoxColumn()
                    {
                        Name = "GiaTri",
                        HeaderText = "Giá trị",
                        DataPropertyName = "GiaTri",
                        ReadOnly = false
                    };
                    roundedDataGridView2.Columns.Insert(insertAt, newCol);
                }

                // Apply initial styling
                for (int i = 0; i < roundedDataGridView2.Rows.Count; i++)
                {
                    UpdateRowStyle(i);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu thử nghiệm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EnsureCheckBoxColumn(string name, string headerText, int desiredIndex)
        {
            if (!roundedDataGridView2.Columns.Contains(name)) return;

            var col = roundedDataGridView2.Columns[name];
            col.ReadOnly = false;
            int idx = col.Index;
            // remove and replace with checkbox column
            roundedDataGridView2.Columns.Remove(col);

            var chk = new DataGridViewCheckBoxColumn()
            {
                Name = name,
                DataPropertyName = name,
                HeaderText = headerText,
                TrueValue = true,
                FalseValue = false,
                ReadOnly = false,
                Width = 80
            };

            int insertAt = Math.Min(desiredIndex, roundedDataGridView2.Columns.Count);
            roundedDataGridView2.Columns.Insert(insertAt, chk);
        }

        private void ExperimentContent_Load_1(object sender, EventArgs e)
        {

        }

        private void btnContracts_Click(object sender, EventArgs e)
        {
            try
            {
                string query = @"SELECT ContractID, MaDon, NgayKy, NgayTraKetQua, Status FROM Contracts where TienTrinh = 3";
                DataTable dt = DataProvider.Instance.ExecuteQuery(query);

                using (ContractContent.PopUpContract popup = new ContractContent.PopUpContract(dt))
                {
                    popup.ContractSelected += (contractId) =>
                    {
                        LoadExperimentContract(contractId);
                    };

                    popup.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách hợp đồng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Save Contaminated/Warning flags into Results.TrangThaiPheDuyet for one sample of the contract
                var dt = roundedDataGridView2.DataSource as DataTable;
                if (dt == null)
                {
                    MessageBox.Show("Không có dữ liệu để lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Find one sample for the contract to attach results to (fallback)
                string sampleQ = "SELECT SampleID FROM EnvironmentalSamples WHERE ContractID = @contractId LIMIT 1";
                object sampleObj = DataProvider.Instance.ExecuteScalar(sampleQ, new object[] { currentContractId });
                if (sampleObj == null || !int.TryParse(sampleObj.ToString(), out int sampleId))
                {
                    MessageBox.Show("Không tìm thấy mẫu để lưu kết quả.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int savedCount = 0;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["ParameterID"] == DBNull.Value || row["ParameterID"] == null) continue;
                    int parameterId = Convert.ToInt32(row["ParameterID"]);

                    bool contaminated = false;
                    bool warning = false;
                    if (dt.Columns.Contains("Contaminated") && row["Contaminated"] != DBNull.Value && row["Contaminated"] != null)
                    {
                        if (bool.TryParse(row["Contaminated"].ToString(), out bool b)) contaminated = b;
                    }
                    if (dt.Columns.Contains("Warning") && row["Warning"] != DBNull.Value && row["Warning"] != null)
                    {
                        if (bool.TryParse(row["Warning"].ToString(), out bool b)) warning = b;
                    }

                    int flag;
                    if (contaminated) flag = 0; // contaminated -> 0 (Bad)
                    else if (warning) flag = 2; // warning -> 2
                    else flag = 1; // good -> 1

                    // check existing result for this sample+parameter
                    string checkQ = "SELECT ResultID FROM Results WHERE SampleID = @sampleId AND ParameterID = @parameterId LIMIT 1";
                    object exist = DataProvider.Instance.ExecuteScalar(checkQ, new object[] { sampleId, parameterId });

                    if (exist != null && int.TryParse(exist.ToString(), out int resultId))
                    {
                        string updateQ = "UPDATE Results SET TrangThaiPheDuyet = @flag, NgayPhanTich = CURRENT_TIMESTAMP WHERE ResultID = @resultId";
                        DataProvider.Instance.ExecuteNonQuery(updateQ, new object[] { flag, resultId });
                        savedCount++;
                    }
                    else
                    {
                        string insertQ = "INSERT INTO Results (GiaTri, TrangThaiPheDuyet, NgayPhanTich, SampleID, ParameterID) VALUES (NULL, @flag, CURRENT_TIMESTAMP, @sampleId, @parameterId)";
                        DataProvider.Instance.ExecuteNonQuery(insertQ, new object[] { flag, sampleId, parameterId });
                        savedCount++;
                    }
                }
                string query = @"UPDATE Contracts SET TienTrinh = 4 WHERE ContractID = @contractId;";
                DataProvider.Instance.ExecuteNonQuery(query, new object[] { this.currentContractId });
                MessageBox.Show($"Đã lưu {savedCount} mục ô nhiễm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // reload to refresh
                if (currentContractId > 0) LoadExperimentContract(currentContractId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
