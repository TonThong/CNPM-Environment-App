using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Environmental_Monitoring.Controller;

namespace Environmental_Monitoring.View.ContractContent
{
    public partial class PlanContent : UserControl
    {
        public PlanContent()
        {
            InitializeComponent();
            roundedDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            btnAddParameter.Click += btnAddParameter_Click;
            roundedDataGridView1.CellDoubleClick += roundedDataGridView1_CellDoubleClick;
        }

        private void lbCustomer_Click(object sender, EventArgs e)
        {

        }

        private void lbContract_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PlanContent_Load(object sender, EventArgs e)
        {
            // Force labels visible and on top in case they are covered or have wrong color
            try
            {
                label1.Visible = true;
                label1.ForeColor = Color.Black;
                label1.BringToFront();

                label2.Visible = true;
                label2.ForeColor = Color.Black;
                label2.BringToFront();
            }
            catch
            {
                // ignore if designer names differ at runtime
            }
            roundedDataGridView1.DefaultCellStyle.ForeColor = Color.Black;
        }

        // Renamed to match designer wiring
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
                        // Load mẫu môi trường liên quan và cập nhật CheckedListBox
                        LoadSamplesForContract(contractId);
                    };

                    popup.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải danh sách hợp đồng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSamplesForContract(int contractId)
        {
            try
            {
                string q = "SELECT SampleID, MaMau, TemplateID, AssignedToHT, AssignedToPTN FROM EnvironmentalSamples WHERE ContractID = @contractId";
                DataTable dt = DataProvider.Instance.ExecuteQuery(q, new object[] { contractId });

                // Clear existing binding/items
                checkedListBox1.BeginUpdate();
                try
                {
                    checkedListBox1.DataSource = null;
                    checkedListBox1.Items.Clear();

                    if (dt == null || dt.Rows.Count == 0)
                    {
                        ClearParameterControls();
                        return;
                    }

                    // Create a simple list of plain objects to avoid DataRowView display
                    var list = dt.AsEnumerable()
                                 .Select(r => new
                                 {
                                     SampleID = r.Field<int>("SampleID"),
                                     MaMau = r.Field<string>("MaMau"),
                                     TemplateID = r.Field<int?>("TemplateID"),
                                     AssignedToHT = r.Field<int?>("AssignedToHT"),
                                     AssignedToPTN = r.Field<int?>("AssignedToPTN")
                                 })
                                 .ToList();

                    checkedListBox1.DataSource = list;
                    checkedListBox1.DisplayMember = "MaMau";
                    checkedListBox1.ValueMember = "SampleID";

                    // Also populate parameters grid based on the first sample's template (single select)
                    var first = list.FirstOrDefault();
                    if (first != null && first.TemplateID.HasValue)
                    {
                        int sampleId = first.SampleID;
                        LoadParametersForTemplate(first.TemplateID.Value, sampleId);
                        // store AssignedToHT on tag for department lookup (use fallback AssignedToPTN)
                        int empId = first.AssignedToHT ?? first.AssignedToPTN ?? 0;
                        roundedDataGridView1.Tag = empId; // store as int
                    }
                    else
                    {
                        ClearParameterControls();
                    }
                }
                finally
                {
                    checkedListBox1.EndUpdate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải mẫu môi trường: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Now accepts sampleId to fetch GiaTri from Results
        private void LoadParametersForTemplate(int templateId, int sampleId = 0)
        {
            try
            {
                // Join TemplateParameters -> Parameters and LEFT JOIN Results for GiaTri of the given sample
                string q = @"SELECT p.ParameterID, p.TenThongSo, p.DonVi, p.GioiHanMin, p.GioiHanMax, r.GiaTri
                             FROM TemplateParameters tp
                             JOIN Parameters p ON tp.ParameterID = p.ParameterID
                             LEFT JOIN Results r ON r.ParameterID = p.ParameterID AND r.SampleID = @sampleId
                             WHERE tp.TemplateID = @templateId";

                // Note: DataProvider expects parameters in the order matches found in query (@sampleId then @templateId)
                DataTable dt = DataProvider.Instance.ExecuteQuery(q, new object[] { sampleId, templateId });

                if (dt == null | dt.Rows.Count == 0)
                {
                    roundedDataGridView1.DataSource = null;
                    ClearParameterControls();
                    return;
                }

                // bind to grid
                roundedDataGridView1.DataSource = dt;

                // Adjust headers
                if (roundedDataGridView1.Columns["ParameterID"] != null)
                    roundedDataGridView1.Columns["ParameterID"].Visible = false;
                if (roundedDataGridView1.Columns["TenThongSo"] != null)
                    roundedDataGridView1.Columns["TenThongSo"].HeaderText = "Tên thông số";
                if (roundedDataGridView1.Columns["DonVi"] != null)
                    roundedDataGridView1.Columns["DonVi"].HeaderText = "Đơn vị";
                if (roundedDataGridView1.Columns["GioiHanMin"] != null)
                    roundedDataGridView1.Columns["GioiHanMin"].HeaderText = "Min";
                if (roundedDataGridView1.Columns["GioiHanMax"] != null)
                    roundedDataGridView1.Columns["GioiHanMax"].HeaderText = "Max";
                if (roundedDataGridView1.Columns["GiaTri"] != null)
                    roundedDataGridView1.Columns["GiaTri"].HeaderText = "Giá trị";

                // clear detail labels
                lblParamNameValue.Text = string.Empty;
                lblUnitValue.Text = string.Empty;
                lblDeptValue.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải thông số của mẫu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearParameterControls()
        {
            roundedDataGridView1.DataSource = null;
            lblParamNameValue.Text = string.Empty;
            lblUnitValue.Text = string.Empty;
            lblDeptValue.Text = string.Empty;
        }

        private void roundedDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                var row = roundedDataGridView1.Rows[e.RowIndex];

                var ten = row.Cells["TenThongSo"].Value?.ToString();
                var donvi = row.Cells["DonVi"].Value?.ToString();
                var giatri = row.Cells["GiaTri"]?.Value; // may be null

                lblParamNameValue.Text = ten ?? string.Empty;
                lblUnitValue.Text = donvi ?? string.Empty;

                // show GiaTri somewhere if you want; for now we can append to unit label or a new label
                if (giatri != null && decimal.TryParse(giatri.ToString(), out var g))
                {
                    lblUnitValue.Text += " (Giá trị: " + g.ToString() + ")";
                }

                // department of AssignedToHT stored in grid tag
                int empId = 0;
                if (roundedDataGridView1.Tag != null)
                {
                    int.TryParse(roundedDataGridView1.Tag.ToString(), out empId);
                }

                if (empId > 0)
                {
                    string q = "SELECT PhongBan FROM Employees WHERE EmployeeID = @id LIMIT 1";
                    object res = DataProvider.Instance.ExecuteScalar(q, new object[] { empId });
                    lblDeptValue.Text = res?.ToString() ?? string.Empty;
                }
                else
                {
                    lblDeptValue.Text = string.Empty;
                }
            }
            catch
            {
                // ignore
            }
        }

        private void cbbParameters_SelectedIndexChanged(object sender, EventArgs e)
        {
            // kept empty; not used now
        }

        private void ClearParameterControls(object v)
        {
            ClearParameterControls();
        }

        private void roundedDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            // When user selects sample in list, update parameters grid to that sample's template
            try
            {
                if (checkedListBox1.SelectedItem == null) return;

                var item = checkedListBox1.SelectedItem;
                // item is anonymous type; use reflection to get TemplateID and AssignedToHT/AssignedToPTN/SampleID
                var t = item.GetType();
                var templateIdProp = t.GetProperty("TemplateID");
                var assignedHTProp = t.GetProperty("AssignedToHT");
                var assignedPTNProp = t.GetProperty("AssignedToPTN");
                var sampleIdProp = t.GetProperty("SampleID");

                int? templateId = templateIdProp?.GetValue(item) as int?;
                int? assignedToHT = assignedHTProp?.GetValue(item) as int?;
                int? assignedToPTN = assignedPTNProp?.GetValue(item) as int?;
                int? sampleId = sampleIdProp?.GetValue(item) as int?;

                if (templateId.HasValue)
                {
                    LoadParametersForTemplate(templateId.Value, sampleId ?? 0);
                }
                else
                {
                    ClearParameterControls();
                }

                // store assigned employee id for dept lookup (HT first, then PTN)
                int empId = assignedToHT ?? assignedToPTN ?? 0;
                roundedDataGridView1.Tag = empId;
            }
            catch
            {
                // ignore
            }
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Nếu người dùng đang check (chọn) 1 item mới
            if (e.NewValue == CheckState.Checked)
            {
                // Bỏ chọn tất cả item khác
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (i != e.Index)
                    {
                        checkedListBox1.SetItemChecked(i, false);
                    }
                }
            }
        }

        private void btnAddParameter_Click(object sender, EventArgs e)
        {
            using (var dlg = new AddEditParameterForm())
            {
                dlg.SetPhuTrachOptions(new[] { "HienTruong", "ThiNghiem" });
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    // Lưu vào DB (giả sử có ParameterRepo)
                    var repo = new Environmental_Monitoring.Controller.Data.ParameterRepo(DataProvider.Instance.GetConnectionString());
                    repo.Add(dlg.Parameter);
                    // Reload grid nếu cần
                    if (checkedListBox1.SelectedItem != null)
                    {
                        var item = checkedListBox1.SelectedItem;
                        var t = item.GetType();
                        var templateIdProp = t.GetProperty("TemplateID");
                        var sampleIdProp = t.GetProperty("SampleID");
                        int? templateId = templateIdProp?.GetValue(item) as int?;
                        int? sampleId = sampleIdProp?.GetValue(item) as int?;
                        if (templateId.HasValue)
                            LoadParametersForTemplate(templateId.Value, sampleId ?? 0);
                    }
                }
            }
        }

        private void roundedDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = roundedDataGridView1.Rows[e.RowIndex];
            int? paramId = row.Cells["ParameterID"].Value as int?;
            if (paramId == null) return;
            var repo = new Environmental_Monitoring.Controller.Data.ParameterRepo(DataProvider.Instance.GetConnectionString());
            var param = repo.GetByID(paramId.Value);
            using (var dlg = new AddEditParameterForm(param))
            {
                dlg.SetPhuTrachOptions(new[] { "HienTruong", "ThiNghiem" });
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    repo.Update(dlg.Parameter);
                    // Reload grid
                    if (checkedListBox1.SelectedItem != null)
                    {
                        var item = checkedListBox1.SelectedItem;
                        var t = item.GetType();
                        var templateIdProp = t.GetProperty("TemplateID");
                        var sampleIdProp = t.GetProperty("SampleID");
                        int? templateId = templateIdProp?.GetValue(item) as int?;
                        int? sampleId = sampleIdProp?.GetValue(item) as int?;
                        if (templateId.HasValue)
                            LoadParametersForTemplate(templateId.Value, sampleId ?? 0);
                    }
                }
            }
        }
    }
}
