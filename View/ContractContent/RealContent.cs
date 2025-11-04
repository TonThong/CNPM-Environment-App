using Environmental_Monitoring.Controller;
using Environmental_Monitoring.View.Components;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Environmental_Monitoring.View.ContractContent
{
    public partial class RealContent : UserControl
    {
        private int currentContractId = 0;

        public RealContent()
        {
            InitializeComponent();
            roundedDataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            roundedDataGridView2.CellEndEdit += RoundedDataGridView2_CellEndEdit;
            btnMail.Click += BtnMail_Click; // btnMail is the Save (Lưu) button in designer


            // Attach runtime load to wire btnSave without referencing designer field directly
            this.Load += RealContent_Load;
        }

        private void RealContent_Load(object? sender, EventArgs e)
        {
            // try to find a control named "btnSave" (the 'Danh Sách Hợp Đồng' button in designer)
            var matches = this.Controls.Find("btnSave", true);
            if (matches.Length > 0 && matches[0] is Control ctl)
            {
                ctl.Click += new EventHandler(btnContracts_Click);
            }
        }

        private void btnCreateTemplate_Click(object sender, EventArgs e)
        {
            if (currentContractId <= 0)
            {
                MessageBox.Show("Vui lòng chọn hợp đồng trước khi tạo mẫu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // load parameters where PhuTrach IS NULL OR = 'HienTruong'
            string q = "SELECT ParameterID, TenThongSo FROM Parameters WHERE PhuTrach IS NULL OR PhuTrach = 'HienTruong'";
            DataTable dt = DataProvider.Instance.ExecuteQuery(q);

            using (var dlg = new AddTemplateForm(dt))
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    // create new SampleTemplates entry linked to current contract via EnvironmentalSamples
                    // 1) insert into SampleTemplates
                    string insertTemplate = "INSERT INTO SampleTemplates (TenMau) VALUES (@tenmau)";
                    DataProvider.Instance.ExecuteNonQuery(insertTemplate, new object[] { dlg.TemplateName });

                    // get last inserted template id (MySQL LAST_INSERT_ID)
                    object last = DataProvider.Instance.ExecuteScalar("SELECT LAST_INSERT_ID()", null);
                    int templateId = 0;
                    if (last != null && int.TryParse(last.ToString(), out int tmp)) templateId = tmp;

                    if (templateId > 0)
                    {
                        // 2) link selected parameters in TemplateParameters
                        foreach (var pid in dlg.SelectedParameterIds)
                        {
                            string ins = "INSERT INTO TemplateParameters (TemplateID, ParameterID) VALUES (@t, @p)";
                            DataProvider.Instance.ExecuteNonQuery(ins, new object[] { templateId, pid });
                        }

                        // 3) create an EnvironmentalSamples row for this contract using the new template
                        string insertSample = "INSERT INTO EnvironmentalSamples (MaMau, ContractID, TemplateID) VALUES (@mamau, @contractId, @templateId)";
                        string sampleCode = $"M{currentContractId}_{templateId}_{DateTime.Now.Ticks % 10000}";
                        DataProvider.Instance.ExecuteNonQuery(insertSample, new object[] { sampleCode, currentContractId, templateId });

                        MessageBox.Show("Tạo mẫu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // reload
                        LoadContract(currentContractId);
                    }
                }
            }
        }

        public void LoadContract(int contractId)
        {
            currentContractId = contractId;

            string q = @"SELECT s.SampleID, s.MaMau AS SampleCode,
                                p.ParameterID, p.TenThongSo, p.DonVi, p.GioiHanMin, p.GioiHanMax,
                                r.GiaTri
                         FROM EnvironmentalSamples s
                         JOIN TemplateParameters tp ON s.TemplateID = tp.TemplateID
                         JOIN Parameters p ON tp.ParameterID = p.ParameterID
                         LEFT JOIN Results r ON r.SampleID = s.SampleID AND r.ParameterID = p.ParameterID
                         WHERE s.ContractID = @contractId";

            DataTable dt = DataProvider.Instance.ExecuteQuery(q, new object[] { contractId });

            if (dt == null)
            {
                roundedDataGridView2.DataSource = null;
                return;
            }

            if (!dt.Columns.Contains("Status"))
                dt.Columns.Add("Status", typeof(string));

            foreach (DataRow row in dt.Rows)
            {
                SetStatusForRow(row);
            }

            roundedDataGridView2.DataSource = dt;
            roundedDataGridView2.DefaultCellStyle.ForeColor = Color.Black;

            // Make GiaTri editable
            if (roundedDataGridView2.Columns["GiaTri"] != null)
                roundedDataGridView2.Columns["GiaTri"].ReadOnly = false;

            // Hide ids
            if (roundedDataGridView2.Columns["ParameterID"] != null)
                roundedDataGridView2.Columns["ParameterID"].Visible = false;
            if (roundedDataGridView2.Columns["SampleID"] != null)
                roundedDataGridView2.Columns["SampleID"].Visible = false;

            // Status column style
            if (roundedDataGridView2.Columns["Status"] != null)
            {
                roundedDataGridView2.Columns["Status"].ReadOnly = true;
                foreach (DataGridViewRow dgRow in roundedDataGridView2.Rows)
                {
                    UpdateStatusCellStyle(dgRow.Index);
                }
            }
        }

        private void SetStatusForRow(DataRow row)
        {
            object gObj = row["GiaTri"];
            object minObj = row["GioiHanMin"];
            object maxObj = row["GioiHanMax"];

            if (gObj == DBNull.Value || gObj == null || string.IsNullOrWhiteSpace(gObj.ToString()))
            {
                row["Status"] = "Chưa có";
                return;
            }

            if (!decimal.TryParse(gObj.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var g) &&
                !decimal.TryParse(gObj.ToString(), out g))
            {
                row["Status"] = "Sai định dạng";
                return;
            }

            decimal? min = null, max = null;
            if (minObj != DBNull.Value && minObj != null)
                min = Convert.ToDecimal(minObj);
            if (maxObj != DBNull.Value && maxObj != null)
                max = Convert.ToDecimal(maxObj);

            if (min.HasValue && g < min.Value)
            {
                row["Status"] = "Below Min";
                return;
            }
            if (max.HasValue && g > max.Value)
            {
                row["Status"] = "Above Max";
                return;
            }

            row["Status"] = "OK";
        }

        private void RoundedDataGridView2_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                var dt = roundedDataGridView2.DataSource as DataTable;
                if (dt == null) return;

                var row = dt.Rows[e.RowIndex];
                SetStatusForRow(row);
                UpdateStatusCellStyle(e.RowIndex);
            }
            catch
            {
                // ignore
            }
        }

        private void UpdateStatusCellStyle(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= roundedDataGridView2.Rows.Count) return;
            var cell = roundedDataGridView2.Rows[rowIndex].Cells["Status"];
            var val = cell.Value?.ToString();
            if (string.IsNullOrEmpty(val))
            {
                cell.Style.BackColor = Color.Gray;
                cell.Style.ForeColor = Color.White;
                return;
            }
            switch (val)
            {
                case "OK":
                    cell.Style.BackColor = Color.Green;
                    cell.Style.ForeColor = Color.White;
                    break;
                case "Below Min":
                case "Above Max":
                case "Sai định dạng":
                    cell.Style.BackColor = Color.Red;
                    cell.Style.ForeColor = Color.White;
                    break;
                default:
                    cell.Style.BackColor = Color.Orange;
                    cell.Style.ForeColor = Color.White;
                    break;
            }
        }

        private void BtnMail_Click(object? sender, EventArgs e)
        {
            // Save: iterate rows and upsert Results
            var dt = roundedDataGridView2.DataSource as DataTable;
            if (dt == null) return;

            foreach (DataRow row in dt.Rows)
            {
                object gObj = row["GiaTri"];
                if (gObj == DBNull.Value || gObj == null || string.IsNullOrWhiteSpace(gObj.ToString())) continue;

                if (!decimal.TryParse(gObj.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var g) && !decimal.TryParse(gObj.ToString(), out g))
                    continue;

                int sampleId = Convert.ToInt32(row["SampleID"]);
                int parameterId = Convert.ToInt32(row["ParameterID"]);

                string checkQ = "SELECT ResultID FROM Results WHERE SampleID = @sampleId AND ParameterID = @parameterId LIMIT 1";
                object exist = DataProvider.Instance.ExecuteScalar(checkQ, new object[] { sampleId, parameterId });

                if (exist != null && int.TryParse(exist.ToString(), out int resultId))
                {
                    string updateQ = "UPDATE Results SET GiaTri = @giaTri, NgayPhanTich = CURRENT_TIMESTAMP WHERE ResultID = @resultId";
                    DataProvider.Instance.ExecuteNonQuery(updateQ, new object[] { g, resultId });
                }
                else
                {
                    string insertQ = "INSERT INTO Results (GiaTri, TrangThaiPheDuyet, NgayPhanTich, SampleID, ParameterID) VALUES (@giaTri, NULL, CURRENT_TIMESTAMP, @sampleId, @parameterId)";
                    DataProvider.Instance.ExecuteNonQuery(insertQ, new object[] { g, sampleId, parameterId });
                }
            }

            MessageBox.Show("Lưu giá trị thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // reload to refresh statuses
            if (currentContractId > 0) LoadContract(currentContractId);
        }

        private void btnContracts_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy danh sách hợp đồng cơ bản
                string query = @"SELECT ContractID, MaDon, NgayKy, NgayTraKetQua, Status FROM Contracts";
                DataTable dt = DataProvider.Instance.ExecuteQuery(query);

                // Hiện popup với DataTable
                using (PopUpContract popup = new PopUpContract(dt))
                {
                    // Khi người dùng chọn một hợp đồng trong popup
                    popup.ContractSelected += (contractId) =>
                    {
                        // Load parameters for the selected contract into this RealContent
                        LoadContract(contractId);
                    };

                    popup.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải danh sách hợp đồng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMail_Click_1(object sender, EventArgs e)
        {

        }
    }
}
