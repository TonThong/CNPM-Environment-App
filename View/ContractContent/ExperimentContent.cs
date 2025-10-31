using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Environmental_Monitoring.Controller;

namespace Environmental_Monitoring.View
{
    public partial class ExperimentContent : UserControl
    {
        private int currentContractId = 0;

        public ExperimentContent()
        {
            InitializeComponent();
            roundedDataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            roundedDataGridView2.ForeColor = Color.Black;
            // wire button to open contract popup at runtime
        }

        private void LoadExperimentContract(int contractId)
        {
            try
            {
                currentContractId = contractId;

                // For the given contract, list distinct parameters from samples' templates
                // and determine if any Result for that parameter in the contract is out of min/max
                string q = @"SELECT p.ParameterID, p.TenThongSo, p.DonVi, p.GioiHanMin, p.GioiHanMax,
                                MAX(CASE 
                                    WHEN r.GiaTri IS NULL THEN 0
                                    WHEN (p.GioiHanMin IS NOT NULL AND r.GiaTri < p.GioiHanMin) OR (p.GioiHanMax IS NOT NULL AND r.GiaTri > p.GioiHanMax) THEN 1
                                    ELSE 0 END) AS IsContaminated
                             FROM EnvironmentalSamples s
                             JOIN TemplateParameters tp ON s.TemplateID = tp.TemplateID
                             JOIN Parameters p ON tp.ParameterID = p.ParameterID
                             LEFT JOIN Results r ON r.SampleID = s.SampleID AND r.ParameterID = p.ParameterID
                             WHERE s.ContractID = @contractId
                             GROUP BY p.ParameterID, p.TenThongSo, p.DonVi, p.GioiHanMin, p.GioiHanMax";

                DataTable dt = DataProvider.Instance.ExecuteQuery(q, new object[] { contractId });

                if (dt == null)
                {
                    roundedDataGridView2.DataSource = null;
                    return;
                }

                // Convert IsContaminated to readable column
                if (dt.Columns.Contains("IsContaminated"))
                {
                    dt.Columns["IsContaminated"].ColumnName = "ÔNHIEM"; // rename
                }

                // Add editable GiaTri column so user can enter a value to save (per contract we will attach to first sample)
                if (!dt.Columns.Contains("GiaTri"))
                {
                    dt.Columns.Add("GiaTri", typeof(decimal));
                }

                roundedDataGridView2.DataSource = dt;

                // Adjust headers
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

                if (roundedDataGridView2.Columns["GiaTri"] != null)
                {
                    roundedDataGridView2.Columns["GiaTri"].HeaderText = "Giá trị (nhập để lưu)";
                    roundedDataGridView2.Columns["GiaTri"].ReadOnly = false;
                }

                if (roundedDataGridView2.Columns["ÔNHIEM"] != null)
                {
                    roundedDataGridView2.Columns["ÔNHIEM"].HeaderText = "Ô nhiễm";
                    // display mapping: 1 -> Good, 0 -> Bad, 2 -> Warning
                    foreach (DataGridViewRow row in roundedDataGridView2.Rows)
                    {
                        var cell = row.Cells["ÔNHIEM"];
                        if (cell.Value != null && int.TryParse(cell.Value.ToString(), out int v))
                        {
                            switch (v)
                            {
                                case 1:
                                    cell.Value = 1;
                                    cell.Style.BackColor = Color.Green;
                                    cell.Style.ForeColor = Color.White;
                                    break;
                                case 0:
                                    cell.Value = 0;
                                    cell.Style.BackColor = Color.Red;
                                    cell.Style.ForeColor = Color.White;
                                    break;
                                case 2:
                                    cell.Value = 2;
                                    cell.Style.BackColor = Color.Orange;
                                    cell.Style.ForeColor = Color.White;
                                    break;
                                default:
                                    cell.Value = v.ToString();
                                    cell.Style.BackColor = Color.Gray;
                                    cell.Style.ForeColor = Color.White;
                                    break;
                            }
                        }
                        else
                        {
                            cell.Value = "Unknown";
                            cell.Style.BackColor = Color.Gray;
                            cell.Style.ForeColor = Color.White;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu thử nghiệm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExperimentContent_Load_1(object sender, EventArgs e)
        {

        }

        private void btnContracts_Click(object sender, EventArgs e)
        {
            try
            {
                string query = @"SELECT ContractID, MaDon, NgayKy, NgayTraKetQua, Status FROM Contracts";
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
                // Save entered GiaTri values into Results table.
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
                    if (!dt.Columns.Contains("GiaTri")) break;

                    var gObj = row["GiaTri"];
                    if (gObj == DBNull.Value || gObj == null) continue;

                    if (!decimal.TryParse(gObj.ToString(), out var g)) continue;

                    if (row["ParameterID"] == DBNull.Value || row["ParameterID"] == null) continue;
                    int parameterId = Convert.ToInt32(row["ParameterID"]);

                    // check existing result for this sample+parameter
                    string checkQ = "SELECT ResultID FROM Results WHERE SampleID = @sampleId AND ParameterID = @parameterId LIMIT 1";
                    object exist = DataProvider.Instance.ExecuteScalar(checkQ, new object[] { sampleId, parameterId });

                    if (exist != null && int.TryParse(exist.ToString(), out int resultId))
                    {
                        string updateQ = "UPDATE Results SET GiaTri = @giaTri, NgayPhanTich = CURRENT_TIMESTAMP WHERE ResultID = @resultId";
                        DataProvider.Instance.ExecuteNonQuery(updateQ, new object[] { g, resultId });
                        savedCount++;
                    }
                    else
                    {
                        string insertQ = "INSERT INTO Results (GiaTri, TrangThaiPheDuyet, NgayPhanTich, SampleID, ParameterID) VALUES (@giaTri, NULL, CURRENT_TIMESTAMP, @sampleId, @parameterId)";
                        DataProvider.Instance.ExecuteNonQuery(insertQ, new object[] { g, sampleId, parameterId });
                        savedCount++;
                    }
                }

                MessageBox.Show($"Đã lưu {savedCount} giá trị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
