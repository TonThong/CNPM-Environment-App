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

            if (lbContractID != null) lbContractID.Text = rm.GetString("Plan_ContractIDLabel", culture);
            if (btnContracts != null) btnContracts.Text = rm.GetString("Plan_ContractListButton", culture);
            if (btnAddParameter != null) btnAddParameter.Text = rm.GetString("Plan_AddParamButton", culture);
            if (roundedButton2 != null) roundedButton2.Text = rm.GetString("Button_Save", culture);

            var btnCancel = this.Controls.Find("btnCancel", true).FirstOrDefault();
            if (btnCancel != null) btnCancel.Text = rm.GetString("Button_Cancel", culture);

            if (label1 != null) label1.Text = rm.GetString("Plan_SampleTemplatesLabel", culture);
            if (label2 != null) label2.Text = rm.GetString("Plan_ParametersLabel", culture);

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
            roundedDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            btnAddParameter.Click += btnAddParameter_Click;
            roundedDataGridView1.CellDoubleClick += roundedDataGridView1_CellDoubleClick;

            roundedDataGridView1.CellContentClick -= roundedDataGridView1_CellContentClick;
            roundedDataGridView1.CellContentClick += roundedDataGridView1_CellContentClick;
            roundedDataGridView1.CurrentCellDirtyStateChanged += RoundedDataGridView1_CurrentCellDirtyStateChanged;

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

            try
            {
                label1.Visible = true; label1.ForeColor = Color.Black; label1.BringToFront();
                label2.Visible = true; label2.ForeColor = Color.Black; label2.BringToFront();
            }
            catch { }

            roundedDataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            UpdateUIText();
            UpdateAddButtonState();

            var btnCancel = this.Controls.Find("btnCancel", true).FirstOrDefault();
            if (btnCancel != null) btnCancel.Click += btnCancel_Click;
        }

        private void CheckedListBox1_MouseUp(object sender, MouseEventArgs e)
        {
            int index = checkedListBox1.IndexFromPoint(e.Location);

            if (index != -1)
            {
                var item = checkedListBox1.Items[index] as SampleTemplateDisplayItem;
                if (item != null)
                {
                    LoadParametersForSingleTemplate(item.TemplateID);
                }
                checkedListBox1.SelectedIndex = -1;
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
            if (roundedDataGridView1.IsCurrentCellDirty)
            {
                roundedDataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void roundedDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != roundedDataGridView1.Columns[CHECKBOX_COLUMN_NAME].Index) return;
            if (_currentViewingTemplateId == -1) return;

            bool isChecked = Convert.ToBoolean(roundedDataGridView1.Rows[e.RowIndex].Cells[CHECKBOX_COLUMN_NAME].Value);
            int paramId = Convert.ToInt32(roundedDataGridView1.Rows[e.RowIndex].Cells["ParameterID"].Value);

            if (!_selectedParamsMap.ContainsKey(_currentViewingTemplateId))
            {
                _selectedParamsMap[_currentViewingTemplateId] = new HashSet<int>();
            }

            if (isChecked)
            {
                _selectedParamsMap[_currentViewingTemplateId].Add(paramId);
            }
            else
            {
                _selectedParamsMap[_currentViewingTemplateId].Remove(paramId);
            }
        }

        private void checkedListBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                UpdateAddButtonState();

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
                if (roundedDataGridView1.Columns["TenThongSo"] != null) roundedDataGridView1.Columns["TenThongSo"].HeaderText = rm.GetString("Grid_ParamName", culture);
                if (roundedDataGridView1.Columns["DonVi"] != null) roundedDataGridView1.Columns["DonVi"].HeaderText = rm.GetString("Grid_Unit", culture);
                if (roundedDataGridView1.Columns["GioiHanMin"] != null) roundedDataGridView1.Columns["GioiHanMin"].HeaderText = rm.GetString("Grid_Min", culture);
                if (roundedDataGridView1.Columns["GioiHanMax"] != null) roundedDataGridView1.Columns["GioiHanMax"].HeaderText = rm.GetString("Grid_Max", culture);
                if (roundedDataGridView1.Columns["PhuTrach"] != null) roundedDataGridView1.Columns["PhuTrach"].HeaderText = rm.GetString("Grid_Department", culture);
                if (roundedDataGridView1.Columns.Contains("GiaTri")) roundedDataGridView1.Columns["GiaTri"].Visible = false;

                if (_selectedParamsMap.ContainsKey(templateId))
                {
                    HashSet<int> selectedForThis = _selectedParamsMap[templateId];
                    foreach (DataGridViewRow row in roundedDataGridView1.Rows)
                    {
                        int pId = Convert.ToInt32(row.Cells["ParameterID"].Value);
                        if (selectedForThis.Contains(pId))
                        {
                            row.Cells[CHECKBOX_COLUMN_NAME].Value = true;
                        }
                        else
                        {
                            row.Cells[CHECKBOX_COLUMN_NAME].Value = false;
                        }
                    }
                }
                else
                {
                    foreach (DataGridViewRow row in roundedDataGridView1.Rows)
                        row.Cells[CHECKBOX_COLUMN_NAME].Value = false;
                }

                ClearParameterControls();
            }
            catch (Exception ex)
            {
                ShowAlert(rm.GetString("Error_LoadParameters", culture) + ": " + ex.Message, AlertPanel.AlertType.Error);
            }
        }

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

            foreach (var tpl in selectedBaseTemplates)
            {
                if (!_selectedParamsMap.ContainsKey(tpl.TemplateID) || _selectedParamsMap[tpl.TemplateID].Count == 0)
                {
                    ShowAlert($"Mẫu '{tpl.TenMau}' được chọn nhưng chưa có thông số nào được chọn.", AlertPanel.AlertType.Error);
                    return;
                }
            }

            try
            {
                string maDon = DataProvider.Instance.ExecuteScalar("SELECT MaDon FROM Contracts WHERE ContractID = @contractId", new object[] { currentContractId }).ToString();
                if (string.IsNullOrEmpty(maDon))
                {
                    ShowAlert(rm.GetString("Plan_ContractCodeNotFound", culture), AlertPanel.AlertType.Error);
                    return;
                }

                foreach (var baseTemplate in selectedBaseTemplates)
                {
                    string sampleCode = $"{maDon} - {baseTemplate.TenMau}";
                    int baseTemplateId = baseTemplate.TemplateID;

                    string checkSampleQuery = "SELECT COUNT(*) FROM EnvironmentalSamples WHERE MaMau = @mamau AND ContractID = @contractId";
                    object sampleExists = DataProvider.Instance.ExecuteScalar(checkSampleQuery, new object[] { sampleCode, currentContractId });
                    if (Convert.ToInt32(sampleExists) > 0) continue;

                    string newSpecificTemplateName = $"Template cho {maDon} - {baseTemplate.TenMau}";
                    string insertTemplateQuery = "INSERT INTO SampleTemplates (TenMau) VALUES (@tenmau)";
                    DataProvider.Instance.ExecuteNonQuery(insertTemplateQuery, new object[] { newSpecificTemplateName });

                    int newSpecificTemplateId = Convert.ToInt32(DataProvider.Instance.ExecuteScalar("SELECT LAST_INSERT_ID()", null));

                    if (_selectedParamsMap.ContainsKey(baseTemplateId))
                    {
                        HashSet<int> paramsForThisSample = _selectedParamsMap[baseTemplateId];

                        foreach (int paramId in paramsForThisSample)
                        {
                            string insertParamLinkQuery = "INSERT INTO TemplateParameters (TemplateID, ParameterID) VALUES (@TID, @PID)";
                            DataProvider.Instance.ExecuteNonQuery(insertParamLinkQuery, new object[] { newSpecificTemplateId, paramId });
                        }
                    }

                    string insertSampleQuery = "INSERT INTO EnvironmentalSamples (MaMau, ContractID, TemplateID) VALUES (@mamau, @contractId, @templateId)";
                    DataProvider.Instance.ExecuteNonQuery(insertSampleQuery, new object[] { sampleCode, currentContractId, newSpecificTemplateId });
                }

                string updateContractQuery = @"UPDATE Contracts SET TienTrinh = 2 WHERE ContractID = @contractId;";
                DataProvider.Instance.ExecuteNonQuery(updateContractQuery, new object[] { this.currentContractId });

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
                        UpdateAddButtonState();
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

                    var list = dt.AsEnumerable().Select(r => new SampleTemplateDisplayItem { TemplateID = r.Field<int>("TemplateID"), TenMau = r.Field<string>("TenMau") }).ToList();

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
            this.BeginInvoke((MethodInvoker)delegate
            {
                UpdateAddButtonState();
            });
        }

        private void UpdateAddButtonState()
        {
            bool contractSelected = (currentContractId != 0);
            bool templateSelected = (checkedListBox1.CheckedItems.Count > 0);
            if (btnAddParameter != null) btnAddParameter.Enabled = (contractSelected && templateSelected);
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
                lblDeptValue.Text = row.Cells["PhuTrach"].Value?.ToString() ?? string.Empty;
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
                    try
                    {
                        string checkQuery = "SELECT COUNT(*) FROM Parameters WHERE TenThongSo = @ten";
                        object result = DataProvider.Instance.ExecuteScalar(checkQuery, new object[] { dlg.Parameter.TenThongSo });
                        if (Convert.ToInt32(result) > 0) { ShowAlert(rm.GetString("Plan_ParamAlreadyExists", culture), AlertPanel.AlertType.Error); return; }
                        string insertQuery = @"INSERT INTO Parameters (TenThongSo, DonVi, GioiHanMin, GioiHanMax, PhuTrach, ONhiem) VALUES (@Ten, @DonVi, @Min, @Max, @PhuTrach, 0)";
                        DataProvider.Instance.ExecuteNonQuery(insertQuery, new object[] { dlg.Parameter.TenThongSo, dlg.Parameter.DonVi, dlg.Parameter.GioiHanMin, dlg.Parameter.GioiHanMax, dlg.Parameter.PhuTrach });
                        ShowAlert(rm.GetString("Plan_AddParamSuccess", culture), AlertPanel.AlertType.Success);
                        ReloadCurrentParameters();
                    }
                    catch (Exception ex) { ShowAlert(rm.GetString("Error_SaveNewParam", culture) + ": " + ex.Message, AlertPanel.AlertType.Error); }
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
                    try
                    {
                        string updateQuery = @"UPDATE Parameters SET TenThongSo = @Ten, DonVi = @DonVi, GioiHanMin = @Min, GioiHanMax = @Max, PhuTrach = @PhuTrach WHERE ParameterID = @ID";
                        DataProvider.Instance.ExecuteNonQuery(updateQuery, new object[] { dlg.Parameter.TenThongSo, dlg.Parameter.DonVi, dlg.Parameter.GioiHanMin, dlg.Parameter.GioiHanMax, dlg.Parameter.PhuTrach, dlg.Parameter.ParameterID });
                        ReloadCurrentParameters();
                    }
                    catch (Exception ex) { ShowAlert(rm.GetString("Error_UpdateParam", culture) + ": " + ex.Message, AlertPanel.AlertType.Error); }
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