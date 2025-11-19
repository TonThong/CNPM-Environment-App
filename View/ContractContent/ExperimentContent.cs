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

namespace Environmental_Monitoring.View
{
    public partial class ExperimentContent : UserControl
    {
        private int currentContractId = 0;
        private bool isUpdatingCell = false;

        private const string COL_ONHIEM = "ColONhiem";
        private const string COL_SAPONHIEM = "ColSapONhiem";
        private const string COL_KHONGONHIEM = "ColKhongONhiem";

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

        public void UpdateUIText()
        {
            culture = Thread.CurrentThread.CurrentUICulture;

            if (lbContractID != null)
                lbContractID.Text = rm.GetString("Plan_ContractIDLabel", culture);
            if (btnContracts != null)
                btnContracts.Text = rm.GetString("Plan_ContractListButton", culture);
            if (btnSave != null)
                btnSave.Text = rm.GetString("Button_Save", culture);

            if (btnCancel != null)
                btnCancel.Text = rm.GetString("Button_Cancel", culture);

            if (roundedDataGridView2.Columns.Contains("SampleCode"))
                roundedDataGridView2.Columns["SampleCode"].HeaderText = rm.GetString("Grid_Sample", culture);
            if (roundedDataGridView2.Columns.Contains("TenThongSo"))
                roundedDataGridView2.Columns["TenThongSo"].HeaderText = rm.GetString("Grid_ParamName", culture);
            if (roundedDataGridView2.Columns.Contains("DonVi"))
                roundedDataGridView2.Columns["DonVi"].HeaderText = rm.GetString("Grid_Unit", culture);
            if (roundedDataGridView2.Columns.Contains("GioiHanMin"))
                roundedDataGridView2.Columns["GioiHanMin"].HeaderText = rm.GetString("Grid_Min", culture);
            if (roundedDataGridView2.Columns.Contains("GioiHanMax"))
                roundedDataGridView2.Columns["GioiHanMax"].HeaderText = rm.GetString("Grid_Max", culture);
            if (roundedDataGridView2.Columns.Contains("GiaTri"))
                roundedDataGridView2.Columns["GiaTri"].HeaderText = rm.GetString("Grid_ValueEntry", culture);

            if (roundedDataGridView2.Columns.Contains(COL_ONHIEM))
                roundedDataGridView2.Columns[COL_ONHIEM].HeaderText = rm.GetString("Grid_Polluted", culture);
            if (roundedDataGridView2.Columns.Contains(COL_SAPONHIEM))
                roundedDataGridView2.Columns[COL_SAPONHIEM].HeaderText = rm.GetString("Grid_SoonPolluted", culture);
            if (roundedDataGridView2.Columns.Contains(COL_KHONGONHIEM))
                roundedDataGridView2.Columns[COL_KHONGONHIEM].HeaderText = rm.GetString("Grid_NotPolluted", culture);
        }

        private void ExperimentContent_Load_1(object sender, EventArgs e)
        {
            roundedDataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            roundedDataGridView2.ForeColor = Color.Black;
            roundedDataGridView2.AllowUserToAddRows = false;

            roundedDataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.Single;

            roundedDataGridView2.CurrentCellDirtyStateChanged += RoundedDataGridView2_CurrentCellDirtyStateChanged;
            roundedDataGridView2.CellValueChanged += RoundedDataGridView2_CellValueChanged;
            roundedDataGridView2.CellEndEdit += RoundedDataGridView2_CellEndEdit_SaveGiaTri;

            if (btnContracts != null)
            {
                btnContracts.Click -= btnContracts_Click;
                btnContracts.Click += btnContracts_Click;
            }
            if (btnSave != null)
            {
                btnSave.Click -= btnSave_Click;
                btnSave.Click += btnSave_Click;
            }
            if (btnCancel != null)
            {
                btnCancel.Click -= btnCancel_Click;
                btnCancel.Click += btnCancel_Click;
            }


            UpdateUIText();
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

        private void RoundedDataGridView2_CellEndEdit_SaveGiaTri(object? sender, DataGridViewCellEventArgs e)
        {
            if (isUpdatingCell || e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var col = roundedDataGridView2.Columns[e.ColumnIndex];
            if (col == null || col.Name != "GiaTri") return;

            isUpdatingCell = true;
            try
            {
                var row = roundedDataGridView2.Rows[e.RowIndex];
                object val = row.Cells["GiaTri"].Value;

                if (val == null || val == DBNull.Value || string.IsNullOrWhiteSpace(val.ToString()))
                {
                    return;
                }

                if (!TryParseDecimalString(val.ToString(), out var giaTri))
                {
                    ShowAlert(rm.GetString("Error_InvalidValue", culture), AlertPanel.AlertType.Error);
                    return;
                }

                if (roundedDataGridView2.Columns.Contains("ParameterID") && roundedDataGridView2.Columns.Contains("SampleID"))
                {
                    int parameterId = Convert.ToInt32(row.Cells["ParameterID"].Value);
                    int sampleId = Convert.ToInt32(row.Cells["SampleID"].Value);

                    if (sampleId <= 0)
                    {
                        ShowAlert(rm.GetString("Error_SampleIDNotFound", culture), AlertPanel.AlertType.Error);
                        return;
                    }

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
                }
            }
            catch (Exception ex)
            {
                ShowAlert(rm.GetString("Error_SavingValue", culture) + ": " + ex.Message, AlertPanel.AlertType.Error);
            }
            finally
            {
                isUpdatingCell = false;
            }
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

            string colName = roundedDataGridView2.Columns[e.ColumnIndex].Name;
            var row = roundedDataGridView2.Rows[e.RowIndex];

            if (colName == COL_ONHIEM || colName == COL_SAPONHIEM || colName == COL_KHONGONHIEM)
            {
                isUpdatingCell = true;
                try
                {
                    bool isChecked = false;
                    var cell = row.Cells[colName];
                    if (cell.Value != null && bool.TryParse(cell.Value.ToString(), out bool b))
                    {
                        isChecked = b;
                    }

                    if (isChecked)
                    {
                        if (colName != COL_ONHIEM)
                            row.Cells[COL_ONHIEM].Value = false;
                        if (colName != COL_SAPONHIEM)
                            row.Cells[COL_SAPONHIEM].Value = false;
                        if (colName != COL_KHONGONHIEM)
                            row.Cells[COL_KHONGONHIEM].Value = false;
                    }
                }
                finally
                {
                    isUpdatingCell = false;
                }
            }
        }

        private void LoadExperimentContract(int contractId)
        {
            try
            {
                currentContractId = contractId;

                string q = @"SELECT s.SampleID, s.MaMau AS SampleCode,
                                 p.ParameterID, p.TenThongSo, p.DonVi, p.GioiHanMin, p.GioiHanMax,
                                 p.PhuTrach, p.ONhiem,
                                 r.GiaTri
                             FROM EnvironmentalSamples s
                             JOIN TemplateParameters tp ON s.TemplateID = tp.TemplateID
                             JOIN Parameters p ON tp.ParameterID = p.ParameterID
                             LEFT JOIN Results r ON r.SampleID = s.SampleID AND r.ParameterID = p.ParameterID
                             WHERE s.ContractID = @contractId
                             ORDER BY s.MaMau, p.ParameterID";

                DataTable dt = DataProvider.Instance.ExecuteQuery(q, new object[] { contractId });

                if (dt == null)
                {
                    roundedDataGridView2.DataSource = null;
                    return;
                }

                dt.Columns.Add(COL_ONHIEM, typeof(bool));
                dt.Columns.Add(COL_SAPONHIEM, typeof(bool));
                dt.Columns.Add(COL_KHONGONHIEM, typeof(bool));

                foreach (DataRow row in dt.Rows)
                {
                    row[COL_ONHIEM] = false;
                    row[COL_SAPONHIEM] = false;
                    row[COL_KHONGONHIEM] = false;
                }

                roundedDataGridView2.DataSource = dt;

                if (roundedDataGridView2.Columns["ParameterID"] != null)
                    roundedDataGridView2.Columns["ParameterID"].Visible = false;
                if (roundedDataGridView2.Columns["SampleID"] != null)
                    roundedDataGridView2.Columns["SampleID"].Visible = false;
                if (roundedDataGridView2.Columns["PhuTrach"] != null)
                    roundedDataGridView2.Columns["PhuTrach"].Visible = false;
                if (roundedDataGridView2.Columns["ONhiem"] != null)
                    roundedDataGridView2.Columns["ONhiem"].Visible = false;

                if (roundedDataGridView2.Columns["SampleCode"] != null)
                    roundedDataGridView2.Columns["SampleCode"].HeaderText = rm.GetString("Grid_Sample", culture);
                if (roundedDataGridView2.Columns["GiaTri"] != null)
                    roundedDataGridView2.Columns["GiaTri"].HeaderText = rm.GetString("Grid_ValueEntry", culture);

                EnsureCheckBoxColumn(COL_ONHIEM, rm.GetString("Grid_Polluted", culture), 10);
                EnsureCheckBoxColumn(COL_SAPONHIEM, rm.GetString("Grid_SoonPolluted", culture), 11);
                EnsureCheckBoxColumn(COL_KHONGONHIEM, rm.GetString("Grid_NotPolluted", culture), 12);

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

                    if (roundedDataGridView2.Columns.Contains(COL_ONHIEM))
                    {
                        roundedDataGridView2.Columns[COL_ONHIEM].DefaultCellStyle.SelectionBackColor = roundedDataGridView2.Columns[COL_ONHIEM].DefaultCellStyle.BackColor;
                        roundedDataGridView2.Columns[COL_SAPONHIEM].DefaultCellStyle.SelectionBackColor = roundedDataGridView2.Columns[COL_SAPONHIEM].DefaultCellStyle.BackColor;
                        roundedDataGridView2.Columns[COL_KHONGONHIEM].DefaultCellStyle.SelectionBackColor = roundedDataGridView2.Columns[COL_KHONGONHIEM].DefaultCellStyle.BackColor;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowAlert(rm.GetString("Error_LoadExperimentData", culture) + ": " + ex.Message, AlertPanel.AlertType.Error);
            }
        }

        private void EnsureCheckBoxColumn(string name, string headerText, int desiredIndex)
        {
            if (roundedDataGridView2.Columns.Contains(name))
            {
                roundedDataGridView2.Columns.Remove(name);
            }

            var chk = new DataGridViewCheckBoxColumn()
            {
                Name = name,
                DataPropertyName = name,
                HeaderText = headerText,
                TrueValue = true,
                FalseValue = false,
                ReadOnly = false,
                Width = 120,
                DefaultCellStyle =
                {
                    SelectionBackColor = roundedDataGridView2.DefaultCellStyle.BackColor,
                    SelectionForeColor = roundedDataGridView2.DefaultCellStyle.ForeColor
                }
            };

            int insertAt = Math.Min(desiredIndex, roundedDataGridView2.Columns.Count);
            roundedDataGridView2.Columns.Insert(insertAt, chk);
        }

        private void btnContracts_Click(object sender, EventArgs e)
        {
            try
            {
                string query = @"SELECT ContractID, MaDon, NgayKy, NgayTraKetQua, Status 
                                 FROM Contracts 
                                 WHERE TienTrinh = 3";
                DataTable dt = DataProvider.Instance.ExecuteQuery(query);

                using (ContractContent.PopUpContract popup = new ContractContent.PopUpContract(dt))
                {
                    popup.ContractSelected += (contractId) =>
                    {
                        lbContractID.Text = rm.GetString("Plan_ContractIDLabel", culture) + " " + contractId.ToString();
                        LoadExperimentContract(contractId);
                    };

                    popup.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ShowAlert(rm.GetString("Error_LoadContracts", culture) + ": " + ex.Message, AlertPanel.AlertType.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var dt = roundedDataGridView2.DataSource as DataTable;
                if (dt == null || currentContractId == 0)
                {
                    ShowAlert(rm.GetString("Exp_NoDataToSave", culture), AlertPanel.AlertType.Error);
                    return;
                }

                int savedCount = 0;
                HashSet<int> parametersToUpdate = new HashSet<int>();

                foreach (DataGridViewRow row in roundedDataGridView2.Rows)
                {
                    if (row.IsNewRow) continue;
                    if (row.Cells["ParameterID"].Value == DBNull.Value || row.Cells["ParameterID"].Value == null) continue;

                    int parameterId = Convert.ToInt32(row.Cells["ParameterID"].Value);

                    if (parametersToUpdate.Contains(parameterId)) continue;

                    bool oNhiem = Convert.ToBoolean(row.Cells[COL_ONHIEM].Value);
                    bool sapONhiem = Convert.ToBoolean(row.Cells[COL_SAPONHIEM].Value);

                    int flag = 0;
                    if (oNhiem) flag = 1;
                    else if (sapONhiem) flag = 2;

                    string updateQuery = "UPDATE Parameters SET ONhiem = @flag WHERE ParameterID = @paramId";
                    DataProvider.Instance.ExecuteNonQuery(updateQuery, new object[] { flag, parameterId });

                    parametersToUpdate.Add(parameterId);
                    savedCount++;
                }

                string query = @"UPDATE Contracts SET TienTrinh = 4 WHERE ContractID = @contractId;";
                DataProvider.Instance.ExecuteNonQuery(query, new object[] { this.currentContractId });

                int ketQuaRoleID = 8;
                if (ketQuaRoleID == 0)
                {
                    ShowAlert("RoleID phòng Kết Quả chưa được thiết lập. Thông báo bị bỏ qua.", AlertPanel.AlertType.Error);
                }
                else
                {
                    string maDon = DataProvider.Instance.ExecuteScalar("SELECT MaDon FROM Contracts WHERE ContractID = @id", new object[] { this.currentContractId }).ToString();
                    string noiDungKetQua = $"Hợp đồng '{maDon}' đã phân tích xong, cần xem xét và duyệt kết quả.";
                    NotificationService.CreateNotification("ChinhSua", noiDungKetQua, ketQuaRoleID, this.currentContractId, null);
                }

                string successMsg = string.Format(rm.GetString("Exp_SaveSuccess", culture), savedCount);
                ShowAlert(successMsg, AlertPanel.AlertType.Success);

                roundedDataGridView2.DataSource = null;
                lbContractID.Text = rm.GetString("Plan_ContractIDLabel", culture);
                this.currentContractId = 0;
            }
            catch (Exception ex)
            {
                ShowAlert(rm.GetString("Error_SaveData", culture) + ": " + ex.Message, AlertPanel.AlertType.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            roundedDataGridView2.DataSource = null;
            if (lbContractID != null)
            {
                lbContractID.Text = rm.GetString("Plan_ContractIDLabel", culture);
            }
            this.currentContractId = 0;
        }


        private void MergeSampleCells()
        {
            roundedDataGridView2.Paint -= roundedDataGridView2_Paint;
            roundedDataGridView2.CellFormatting -= roundedDataGridView2_CellFormatting;

            roundedDataGridView2.Paint += roundedDataGridView2_Paint;
            roundedDataGridView2.CellFormatting += roundedDataGridView2_CellFormatting;
        }

        private void roundedDataGridView2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void roundedDataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
        }
    }
}