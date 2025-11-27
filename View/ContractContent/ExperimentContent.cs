using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Environmental_Monitoring.Controller;
using Environmental_Monitoring.View.ContractContent;
using System.Globalization;
using System.Resources;
using System.Threading;
using Environmental_Monitoring.View.Components;
using System.Collections.Generic;
using System.Linq;
using Environmental_Monitoring.Controller.Data;
using System.Reflection;
using System.Threading.Tasks;

namespace Environmental_Monitoring.View
{
    public partial class ExperimentContent : UserControl
    {
        private int currentContractId = 0;
        private bool isUpdatingCell = false;

        private ResourceManager rm;
        private CultureInfo culture;

        public ExperimentContent()
        {
            InitializeComponent();
            InitializeLocalization();
            this.Load += ExperimentContent_Load_1;
        }

        private void InitializeLocalization()
        {
            rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(ExperimentContent).Assembly);
            culture = Thread.CurrentThread.CurrentUICulture;
        }

        public void PerformSearch(string keyword)
        {
            if (roundedDataGridView2.DataSource is DataTable dt)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(keyword))
                    {
                        dt.DefaultView.RowFilter = string.Empty;
                    }
                    else
                    {
                        string safeKeyword = keyword.Replace("'", "''").Replace("[", "[[]").Replace("]", "[]]").Replace("%", "[%]").Replace("*", "[*]").Trim();
                        List<string> filterParts = new List<string>();

                        if (dt.Columns.Contains("SampleCode")) filterParts.Add($"SampleCode LIKE '%{safeKeyword}%'");
                        if (dt.Columns.Contains("TenThongSo")) filterParts.Add($"TenThongSo LIKE '%{safeKeyword}%'");
                        if (dt.Columns.Contains("DonVi")) filterParts.Add($"DonVi LIKE '%{safeKeyword}%'");
                        if (dt.Columns.Contains("GiaTri")) filterParts.Add($"Convert(GiaTri, 'System.String') LIKE '%{safeKeyword}%'");
                        if (dt.Columns.Contains("Status")) filterParts.Add($"Status LIKE '%{safeKeyword}%'");

                        dt.DefaultView.RowFilter = string.Join(" OR ", filterParts);
                    }
                }
                catch (Exception ex) { Console.WriteLine("Search Error: " + ex.Message); }
            }
        }

        private void ShowAlert(string message, AlertPanel.AlertType type)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => ShowAlert(message, type)));
                return;
            }
            var mainLayout = this.FindForm() as Mainlayout;
            if (mainLayout != null) mainLayout.ShowGlobalAlert(message, type);
            else MessageBox.Show(message, type.ToString(), MessageBoxButtons.OK, type == AlertPanel.AlertType.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }

        public void UpdateUIText()
        {
            culture = Thread.CurrentThread.CurrentUICulture;

            if (lbContractID != null) lbContractID.Text = rm.GetString("Plan_ContractIDLabel", culture);
            if (btnContracts != null) btnContracts.Text = rm.GetString("Plan_ContractListButton", culture);
            if (btnSave != null) btnSave.Text = rm.GetString("Button_Save", culture);
            if (btnCancel != null) btnCancel.Text = rm.GetString("Button_Cancel", culture);

            if (roundedDataGridView2.Columns.Contains("SampleCode")) roundedDataGridView2.Columns["SampleCode"].HeaderText = rm.GetString("Grid_Sample", culture);
            if (roundedDataGridView2.Columns.Contains("TenThongSo")) roundedDataGridView2.Columns["TenThongSo"].HeaderText = rm.GetString("Grid_ParamName", culture);
            if (roundedDataGridView2.Columns.Contains("DonVi")) roundedDataGridView2.Columns["DonVi"].HeaderText = rm.GetString("Grid_Unit", culture);
            if (roundedDataGridView2.Columns.Contains("GioiHanMin")) roundedDataGridView2.Columns["GioiHanMin"].HeaderText = rm.GetString("Grid_Min", culture);
            if (roundedDataGridView2.Columns.Contains("GioiHanMax")) roundedDataGridView2.Columns["GioiHanMax"].HeaderText = rm.GetString("Grid_Max", culture);
            if (roundedDataGridView2.Columns.Contains("GiaTri")) roundedDataGridView2.Columns["GiaTri"].HeaderText = rm.GetString("Grid_ValueEntry", culture);
            if (roundedDataGridView2.Columns.Contains("Status")) roundedDataGridView2.Columns["Status"].HeaderText = rm.GetString("Grid_Status", culture) ?? "Status";

            roundedDataGridView2.GridColor = Color.Black;
        }

        private void ExperimentContent_Load_1(object sender, EventArgs e)
        {
            roundedDataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            roundedDataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            roundedDataGridView2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            roundedDataGridView2.ForeColor = Color.Black;
            roundedDataGridView2.AllowUserToAddRows = false;
            roundedDataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.None;
            roundedDataGridView2.EditMode = DataGridViewEditMode.EditOnEnter;

            roundedDataGridView2.CurrentCellDirtyStateChanged += RoundedDataGridView2_CurrentCellDirtyStateChanged;
            roundedDataGridView2.CellEndEdit += RoundedDataGridView2_CellEndEdit_SaveGiaTri;
            roundedDataGridView2.EditingControlShowing += RoundedDataGridView2_EditingControlShowing;

            roundedDataGridView2.CellPainting -= RoundedDataGridView2_CellPainting;
            roundedDataGridView2.CellPainting += RoundedDataGridView2_CellPainting;

            // Đăng ký sự kiện Formatting
            roundedDataGridView2.CellFormatting -= RoundedDataGridView2_CellFormatting;
            roundedDataGridView2.CellFormatting += RoundedDataGridView2_CellFormatting;

            roundedDataGridView2.CellMouseEnter += RoundedDataGridView2_CellMouseEnter;
            roundedDataGridView2.CellMouseLeave += RoundedDataGridView2_CellMouseLeave;

            if (btnContracts != null) { btnContracts.Click -= btnContracts_Click; btnContracts.Click += btnContracts_Click; }
            if (btnSave != null) { btnSave.Click -= btnSave_Click; btnSave.Click += btnSave_Click; }
            if (btnCancel != null) { btnCancel.Click -= btnCancel_Click; btnCancel.Click += btnCancel_Click; }

            UpdateUIText();
        }

        // --- CẬP NHẬT: Tô màu và Tắt Highlight ---
        private void RoundedDataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            // Xử lý cho cột Status
            if (roundedDataGridView2.Columns[e.ColumnIndex].Name == "Status")
            {
                string val = e.Value?.ToString() ?? "";
                string passed = rm.GetString("Status_Passed", culture) ?? "Passed"; // Màu Xanh

                // Lấy text Not Available từ resource, nếu không có thì mặc định
                string notAvail = rm.GetString("Status_NotAvailable", culture) ?? "Not Available";

                e.CellStyle.ForeColor = Color.White;

                // Logic tô màu
                if (string.IsNullOrEmpty(val) || val.Contains("N/A") || val == notAvail)
                {
                    e.CellStyle.BackColor = Color.Orange; // [CẬP NHẬT] Màu Cam cho Not Available
                }
                else if (val == passed)
                {
                    e.CellStyle.BackColor = Color.Green;
                }
                else
                {
                    // Out of Range hoặc Invalid -> Màu Đỏ
                    e.CellStyle.BackColor = Color.Red;
                }

                // [QUAN TRỌNG] Tắt Highlight khi chọn dòng
                // Bằng cách set màu Selection giống hệt màu nền bình thường
                e.CellStyle.SelectionBackColor = e.CellStyle.BackColor;
                e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor;
            }
        }

        private void RoundedDataGridView2_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            // Đã xóa logic trỏ chuột cho checkbox
        }

        private void RoundedDataGridView2_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            roundedDataGridView2.Cursor = Cursors.Default;
        }

        private void RoundedDataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox tb)
            {
                tb.BorderStyle = BorderStyle.None;
                tb.BackColor = Color.FromArgb(224, 224, 224);
            }
        }

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

        private string SetStatusForRow(DataRow row)
        {
            object gObj = row["GiaTri"];
            string status = CalculateStatus(gObj, row["GioiHanMin"], row["GioiHanMax"]);
            row["Status"] = status;
            return status;
        }

        private string CalculateStatus(object valueObj, object minObj, object maxObj)
        {
            if (valueObj == DBNull.Value || valueObj == null || string.IsNullOrWhiteSpace(valueObj.ToString()))
            {
                return rm.GetString("Status_NotAvailable", culture) ?? "Not Available";
            }

            if (!TryParseDecimalString(valueObj.ToString(), out decimal val))
            {
                return rm.GetString("Status_InvalidFormat", culture) ?? "Invalid Format";
            }

            decimal? min = null, max = null;
            if (minObj != DBNull.Value && minObj != null) min = Convert.ToDecimal(minObj);
            if (maxObj != DBNull.Value && maxObj != null) max = Convert.ToDecimal(maxObj);

            if (min.HasValue && val < min.Value) return rm.GetString("Status_OutOfRange", culture) ?? "Out of Range";
            if (max.HasValue && val > max.Value) return rm.GetString("Status_OutOfRange", culture) ?? "Out of Range";

            return rm.GetString("Status_Passed", culture) ?? "Passed";
        }

        private async void RoundedDataGridView2_CellEndEdit_SaveGiaTri(object? sender, DataGridViewCellEventArgs e)
        {
            if (isUpdatingCell || e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var col = roundedDataGridView2.Columns[e.ColumnIndex];
            if (col == null || col.Name != "GiaTri") return;

            DataRowView drv = roundedDataGridView2.Rows[e.RowIndex].DataBoundItem as DataRowView;
            if (drv != null)
            {
                SetStatusForRow(drv.Row);
                roundedDataGridView2.InvalidateRow(e.RowIndex);
            }

            var row = roundedDataGridView2.Rows[e.RowIndex];
            object val = row.Cells["GiaTri"].Value;

            if (val == null || val == DBNull.Value || string.IsNullOrWhiteSpace(val.ToString())) return;

            if (!TryParseDecimalString(val.ToString(), out var giaTri))
            {
                ShowAlert(rm.GetString("Error_InvalidValue", culture), AlertPanel.AlertType.Error);
                return;
            }

            if (!roundedDataGridView2.Columns.Contains("ParameterID") || !roundedDataGridView2.Columns.Contains("SampleID")) return;

            int parameterId = Convert.ToInt32(row.Cells["ParameterID"].Value);
            int sampleId = Convert.ToInt32(row.Cells["SampleID"].Value);

            if (sampleId <= 0) { ShowAlert(rm.GetString("Error_SampleIDNotFound", culture), AlertPanel.AlertType.Error); return; }

            isUpdatingCell = true;
            try
            {
                await Task.Run(() =>
                {
                    string checkQ = "SELECT ResultID FROM Results WHERE SampleID = @sampleId AND ParameterID = @parameterId LIMIT 1";
                    object exist = DataProvider.Instance.ExecuteScalar(checkQ, new object[] { sampleId, parameterId });

                    if (exist != null && int.TryParse(exist.ToString(), out int resultId))
                    {
                        string updateQ = "UPDATE Results SET GiaTri = @giaTri, NgayPhanTich = CURRENT_TIMESTAMP WHERE ResultID = @resultId";
                        DataProvider.Instance.ExecuteNonQuery(updateQ, new object[] { giaTri, resultId });
                    }
                    else
                    {
                        string insertQ = "INSERT INTO Results (GiaTri, NgayPhanTich, SampleID, ParameterID) VALUES (@giaTri, CURRENT_TIMESTAMP, @sampleId, @parameterId)";
                        DataProvider.Instance.ExecuteNonQuery(insertQ, new object[] { giaTri, sampleId, parameterId });
                    }
                });
            }
            catch (Exception ex) { ShowAlert(rm.GetString("Error_SavingValue", culture) + ": " + ex.Message, AlertPanel.AlertType.Error); }
            finally { isUpdatingCell = false; }
        }

        private void RoundedDataGridView2_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
        {
            if (roundedDataGridView2.IsCurrentCellDirty && roundedDataGridView2.CurrentCell is DataGridViewCheckBoxCell)
                roundedDataGridView2.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        // Đã xóa hàm RoundedDataGridView2_CellValueChanged vì nó xử lý logic checkbox

        private void LoadExperimentContract(int contractId)
        {
            try
            {
                currentContractId = contractId;
                string q = @"SELECT s.SampleID, s.MaMau AS SampleCode,
                                 p.ParameterID, p.TenThongSo, p.DonVi, p.GioiHanMin, p.GioiHanMax,
                                 p.PhuTrach, p.ONhiem, r.GiaTri
                             FROM EnvironmentalSamples s
                             JOIN TemplateParameters tp ON s.TemplateID = tp.TemplateID
                             JOIN Parameters p ON tp.ParameterID = p.ParameterID
                             LEFT JOIN Results r ON r.SampleID = s.SampleID AND r.ParameterID = p.ParameterID
                             WHERE s.ContractID = @contractId
                             ORDER BY s.MaMau, p.ParameterID";

                DataTable dt = DataProvider.Instance.ExecuteQuery(q, new object[] { contractId });
                if (dt == null) { roundedDataGridView2.DataSource = null; return; }

                if (!dt.Columns.Contains("Status")) dt.Columns.Add("Status", typeof(string));

                foreach (DataRow row in dt.Rows)
                {
                    SetStatusForRow(row);
                }

                roundedDataGridView2.DataSource = dt;

                if (roundedDataGridView2.Columns["ParameterID"] != null) roundedDataGridView2.Columns["ParameterID"].Visible = false;
                if (roundedDataGridView2.Columns["SampleID"] != null) roundedDataGridView2.Columns["SampleID"].Visible = false;
                if (roundedDataGridView2.Columns["PhuTrach"] != null) roundedDataGridView2.Columns["PhuTrach"].Visible = false;
                if (roundedDataGridView2.Columns["ONhiem"] != null) roundedDataGridView2.Columns["ONhiem"].Visible = false;

                if (roundedDataGridView2.Columns["SampleCode"] != null) roundedDataGridView2.Columns["SampleCode"].HeaderText = rm.GetString("Grid_Sample", culture);
                if (roundedDataGridView2.Columns["TenThongSo"] != null) roundedDataGridView2.Columns["TenThongSo"].HeaderText = rm.GetString("Grid_ParamName", culture);
                if (roundedDataGridView2.Columns["DonVi"] != null) roundedDataGridView2.Columns["DonVi"].HeaderText = rm.GetString("Grid_Unit", culture);
                if (roundedDataGridView2.Columns["GioiHanMin"] != null) roundedDataGridView2.Columns["GioiHanMin"].HeaderText = rm.GetString("Grid_Min", culture);
                if (roundedDataGridView2.Columns["GioiHanMax"] != null) roundedDataGridView2.Columns["GioiHanMax"].HeaderText = rm.GetString("Grid_Max", culture);

                if (roundedDataGridView2.Columns["GiaTri"] != null)
                {
                    roundedDataGridView2.Columns["GiaTri"].HeaderText = rm.GetString("Grid_ValueEntry", culture);
                    roundedDataGridView2.Columns["GiaTri"].DefaultCellStyle.Padding = new Padding(2);
                }

                // Cấu hình cột Status
                if (roundedDataGridView2.Columns["Status"] != null)
                {
                    roundedDataGridView2.Columns["Status"].HeaderText = rm.GetString("Grid_Status", culture) ?? "Status";
                    roundedDataGridView2.Columns["Status"].ReadOnly = true;
                    // Đặt vị trí cột Status sau GiaTri
                    if (roundedDataGridView2.Columns["GiaTri"] != null)
                    {
                        roundedDataGridView2.Columns["Status"].DisplayIndex = roundedDataGridView2.Columns["GiaTri"].DisplayIndex + 1;
                    }
                }

                // Đã xóa phần thêm cột Checkbox

                foreach (DataGridViewColumn col in roundedDataGridView2.Columns) col.SortMode = DataGridViewColumnSortMode.NotSortable;

                string[] readOnlyCols = { "SampleCode", "TenThongSo", "DonVi", "GioiHanMin", "GioiHanMax", "Status" };
                foreach (string colName in readOnlyCols)
                {
                    if (roundedDataGridView2.Columns.Contains(colName))
                    {
                        var col = roundedDataGridView2.Columns[colName];
                        col.ReadOnly = true;
                        col.DefaultCellStyle.SelectionBackColor = Color.White;
                        col.DefaultCellStyle.SelectionForeColor = Color.Black;
                    }
                }

                foreach (DataGridViewRow row in roundedDataGridView2.Rows)
                {
                    string phuTrach = row.Cells["PhuTrach"].Value?.ToString() ?? "";
                    DataGridViewCell giaTriCell = row.Cells["GiaTri"];

                    if (phuTrach.Equals("ThiNghiem", StringComparison.OrdinalIgnoreCase))
                    {
                        giaTriCell.ReadOnly = false;
                        giaTriCell.Style.BackColor = Color.FromArgb(224, 224, 224);
                        giaTriCell.Style.ForeColor = Color.Black;
                    }
                    else
                    {
                        giaTriCell.ReadOnly = true;
                        giaTriCell.Style.BackColor = Color.White;
                        giaTriCell.Style.ForeColor = Color.Black;
                    }
                }
            }
            catch (Exception ex) { ShowAlert(rm.GetString("Error_LoadExperimentData", culture) + ": " + ex.Message, AlertPanel.AlertType.Error); }
        }

        // Đã xóa hàm EnsureCheckBoxColumn

        private void btnContracts_Click(object sender, EventArgs e)
        {
            try
            {
                string query = @"SELECT ContractID, MaDon, NgayKy, NgayTraKetQua, Status, IsUnlocked FROM Contracts WHERE TienTrinh = 3";
                DataTable dt = DataProvider.Instance.ExecuteQuery(query);
                using (ContractContent.PopUpContract popup = new ContractContent.PopUpContract(dt))
                {
                    popup.ContractSelected += (contractId) =>
                    {
                        // --- LOGIC MỚI: LẤY TÊN KHÁCH HÀNG ---
                        string cusQuery = @"SELECT cus.TenDoanhNghiep 
                                            FROM Contracts c 
                                            JOIN Customers cus ON c.CustomerID = cus.CustomerID 
                                            WHERE c.ContractID = @cid";
                        object result = DataProvider.Instance.ExecuteScalar(cusQuery, new object[] { contractId });
                        string tenCongTy = result != null ? result.ToString() : "Không xác định";

                        // Hiển thị tên công ty thay vì ID
                        lbContractID.Text = rm.GetString("Plan_ContractIDLabel", culture) + " " + tenCongTy;
                        // -------------------------------------

                        LoadExperimentContract(contractId);
                    };
                    popup.ShowDialog();
                }
            }
            catch (Exception ex) { ShowAlert(rm.GetString("Error_LoadContracts", culture) + ": " + ex.Message, AlertPanel.AlertType.Error); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var dt = roundedDataGridView2.DataSource as DataTable;
                if (dt == null || currentContractId == 0) { ShowAlert(rm.GetString("Exp_NoDataToSave", culture), AlertPanel.AlertType.Error); return; }

                foreach (DataGridViewRow row in roundedDataGridView2.Rows)
                {
                    if (row.IsNewRow) continue;
                    if (row.Cells["ParameterID"].Value == DBNull.Value) continue;

                    object gObj = row.Cells["GiaTri"].Value;
                    if (gObj == DBNull.Value || gObj == null || string.IsNullOrWhiteSpace(gObj.ToString()))
                    {
                        ShowAlert("Vui lòng nhập đầy đủ giá trị kết quả!", AlertPanel.AlertType.Error);
                        return;
                    }
                    if (!TryParseDecimalString(gObj.ToString(), out decimal temp)) { ShowAlert("Giá trị không hợp lệ!", AlertPanel.AlertType.Error); return; }
                }

                int savedCount = 0;
                // HashSet<int> parametersToUpdate = new HashSet<int>(); // Không còn dùng vì đã xóa phần update Parameters.ONhiem

                foreach (DataGridViewRow row in roundedDataGridView2.Rows)
                {
                    if (row.IsNewRow) continue;
                    if (row.Cells["ParameterID"].Value == DBNull.Value) continue;

                    int parameterId = Convert.ToInt32(row.Cells["ParameterID"].Value);
                    int sampleId = Convert.ToInt32(row.Cells["SampleID"].Value);
                    object gObj = row.Cells["GiaTri"].Value;
                    TryParseDecimalString(gObj.ToString(), out decimal g);

                    string checkQ = "SELECT ResultID FROM Results WHERE SampleID = @sampleId AND ParameterID = @parameterId LIMIT 1";
                    object exist = DataProvider.Instance.ExecuteScalar(checkQ, new object[] { sampleId, parameterId });

                    if (exist != null && int.TryParse(exist.ToString(), out int resultId))
                    {
                        string updateQ = "UPDATE Results SET GiaTri = @giaTri, NgayPhanTich = CURRENT_TIMESTAMP WHERE ResultID = @resultId";
                        DataProvider.Instance.ExecuteNonQuery(updateQ, new object[] { g, resultId });
                    }
                    else
                    {
                        string insertQ = "INSERT INTO Results (GiaTri, NgayPhanTich, SampleID, ParameterID) VALUES (@giaTri, CURRENT_TIMESTAMP, @sampleId, @parameterId)";
                        DataProvider.Instance.ExecuteNonQuery(insertQ, new object[] { g, sampleId, parameterId });
                    }

                    savedCount++;
                }

                string query = @"UPDATE Contracts SET TienTrinh = 4 WHERE ContractID = @contractId;";
                DataProvider.Instance.ExecuteNonQuery(query, new object[] { this.currentContractId });

                int ketQuaRoleID = 8;
                string maDon = DataProvider.Instance.ExecuteScalar("SELECT MaDon FROM Contracts WHERE ContractID = @id", new object[] { this.currentContractId }).ToString();
                string noiDungKetQua = $"Hợp đồng '{maDon}' đã phân tích xong, cần xem xét và duyệt kết quả.";
                NotificationService.CreateNotification("ChinhSua", noiDungKetQua, ketQuaRoleID, this.currentContractId, null);

                string successMsg = string.Format(rm.GetString("Exp_SaveSuccess", culture), savedCount);
                ShowAlert(successMsg, AlertPanel.AlertType.Success);
                roundedDataGridView2.DataSource = null;
                lbContractID.Text = rm.GetString("Plan_ContractIDLabel", culture);
                this.currentContractId = 0;
            }
            catch (Exception ex) { ShowAlert(rm.GetString("Error_SaveData", culture) + ": " + ex.Message, AlertPanel.AlertType.Error); }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            roundedDataGridView2.DataSource = null;
            if (lbContractID != null) lbContractID.Text = rm.GetString("Plan_ContractIDLabel", culture);
            this.currentContractId = 0;
        }

        private void RoundedDataGridView2_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            e.Handled = true;
            e.PaintBackground(e.CellBounds, true);

            string colName = roundedDataGridView2.Columns[e.ColumnIndex].Name;
            bool isMergeColumn = (colName == "SampleCode");

            using (Pen gridPen = new Pen(Color.Black, 1))
            {
                if (isMergeColumn)
                {
                    string rawValue = e.Value?.ToString() ?? "";
                    string displayValue = rawValue;
                    if (!string.IsNullOrEmpty(displayValue))
                    {
                        int index = displayValue.IndexOf(" - Template");
                        if (index > 0) displayValue = displayValue.Substring(0, index);
                    }

                    int startIndex = e.RowIndex;
                    while (startIndex > 0)
                    {
                        object prevVal = roundedDataGridView2.Rows[startIndex - 1].Cells[e.ColumnIndex].Value;
                        if (prevVal != null && prevVal.ToString() == rawValue) startIndex--; else break;
                    }

                    int endIndex = e.RowIndex;
                    while (endIndex < roundedDataGridView2.Rows.Count - 1)
                    {
                        object nextVal = roundedDataGridView2.Rows[endIndex + 1].Cells[e.ColumnIndex].Value;
                        if (nextVal != null && nextVal.ToString() == rawValue) endIndex++; else break;
                    }

                    int totalHeight = 0;
                    for (int i = startIndex; i <= endIndex; i++) totalHeight += roundedDataGridView2.Rows[i].Height;
                    int offsetY = 0;
                    for (int i = startIndex; i < e.RowIndex; i++) offsetY -= roundedDataGridView2.Rows[i].Height;

                    Rectangle groupRect = new Rectangle(e.CellBounds.X, e.CellBounds.Y + offsetY, e.CellBounds.Width, totalHeight);
                    TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak | TextFormatFlags.PreserveGraphicsClipping;
                    TextRenderer.DrawText(e.Graphics, displayValue, e.CellStyle.Font, groupRect, Color.Black, flags);

                    e.Graphics.DrawLine(gridPen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);
                    if (e.RowIndex == endIndex) e.Graphics.DrawLine(gridPen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                }
                else
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);
                    e.Graphics.DrawLine(gridPen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);
                    e.Graphics.DrawLine(gridPen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                }
            }
        }

        private void roundedDataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}