using Environmental_Monitoring.Controller;
using Environmental_Monitoring.View.Components;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Resources;
using System.Threading;
using System.Linq; 

namespace Environmental_Monitoring.View.ContractContent
{
    public partial class RealContent : UserControl
    {
        private int currentContractId = 0;

        private ResourceManager rm;
        private CultureInfo culture;

        /// <summary>
        /// Hàm khởi tạo (Constructor) cho UserControl.
        /// </summary>
        public RealContent()
        {
            InitializeComponent();
            InitializeLocalization();
            this.Load += RealContent_Load;
        }

        /// <summary>
        /// Khởi tạo ResourceManager để tải chuỗi đa ngôn ngữ.
        /// </summary>
        private void InitializeLocalization()
        {
            rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(RealContent).Assembly);
            culture = Thread.CurrentThread.CurrentUICulture;
        }

        /// <summary>
        /// Hiển thị thông báo (alert) cho người dùng, ưu tiên qua Mainlayout.
        /// </summary>
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

        /// <summary>
        /// Cập nhật lại tất cả văn bản trên UI (Label, Button, Header của Grid) theo ngôn ngữ hiện tại.
        /// </summary>
        public void UpdateUIText()
        {
            culture = Thread.CurrentThread.CurrentUICulture;

            if (lbContractID != null)
                lbContractID.Text = rm.GetString("Plan_ContractIDLabel", culture);

            if (btnSave != null)
                btnSave.Text = rm.GetString("Button_Save", culture);

            var btnCancel = this.Controls.Find("btnCancel", true).FirstOrDefault();
            if (btnCancel != null)
            {
                btnCancel.Text = rm.GetString("Button_Cancel", culture);
            }

            var matches = this.Controls.Find("btnContracts", true);
            if (matches.Length > 0 && matches[0] is Control ctl)
            {
                ctl.Text = rm.GetString("Plan_ContractListButton", culture);
            }

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
            if (roundedDataGridView2.Columns.Contains("Status"))
                roundedDataGridView2.Columns["Status"].HeaderText = rm.GetString("Grid_Status", culture);
        }

        /// <summary>
        /// Xử lý khi UserControl được tải, gán các sự kiện ban đầu cho DataGridView.
        /// </summary>
        private void RealContent_Load(object? sender, EventArgs e)
        {
            roundedDataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            roundedDataGridView2.AllowUserToAddRows = false;

            roundedDataGridView2.CurrentCellDirtyStateChanged -= roundedDataGridView2_CurrentCellDirtyStateChanged;
            roundedDataGridView2.CurrentCellDirtyStateChanged += roundedDataGridView2_CurrentCellDirtyStateChanged;
            roundedDataGridView2.CellValueChanged -= roundedDataGridView2_CellValueChanged;
            roundedDataGridView2.CellValueChanged += roundedDataGridView2_CellValueChanged;

            var matches = this.Controls.Find("btnContracts", true);
            if (matches.Length > 0 && matches[0] is Control ctl)
            {
                ctl.Click -= new EventHandler(btnContracts_Click); 
                ctl.Click += new EventHandler(btnContracts_Click);
            }

            var btnCancel = this.Controls.Find("btnCancel", true).FirstOrDefault();
            if (btnCancel != null)
            {
                btnCancel.Click -= btnCancel_Click;
                btnCancel.Click += btnCancel_Click;
            }

            var btnSave = this.Controls.Find("btnSave", true).FirstOrDefault();
            if (btnSave != null)
            {
                btnSave.Click -= btnSave_Click;
                btnSave.Click += btnSave_Click;
            }


            UpdateUIText();
        }

        /// <summary>
        /// Chuyển đổi một chuỗi (có thể chứa dấu . hoặc ,) thành số Decimal một cách an toàn.
        /// </summary>
        private bool TryParseDecimalString(string s, out decimal result)
        {
            result = 0;
            if (string.IsNullOrWhiteSpace(s)) return false;

            s = s.Trim();

            bool hasComma = s.Contains(",");
            bool hasDot = s.Contains(".");

            NumberStyles styles = NumberStyles.Number;

            if (hasComma && !hasDot)
            {
                if (decimal.TryParse(s, styles, new CultureInfo("vi-VN"), out result)) return true;
                if (decimal.TryParse(s, styles, CultureInfo.InvariantCulture, out result)) return true;
            }
            else if (hasDot && !hasComma)
            {
                if (decimal.TryParse(s, styles, CultureInfo.InvariantCulture, out result)) return true;
                if (decimal.TryParse(s, styles, new CultureInfo("vi-VN"), out result)) return true;
            }
            else
            {
                if (decimal.TryParse(s, styles, CultureInfo.CurrentCulture, out result)) return true;
                if (decimal.TryParse(s, styles, new CultureInfo("vi-VN"), out result)) return true;
                if (decimal.TryParse(s, styles, CultureInfo.InvariantCulture, out result)) return true;
            }

            return false;
        }

        /// <summary>
        /// Tải dữ liệu của một Hợp đồng cụ thể lên DataGridView.
        /// </summary>
        public void LoadContract(int contractId)
        {
            currentContractId = contractId;

            string q = @"SELECT s.SampleID, s.MaMau AS SampleCode,
                           p.ParameterID, p.TenThongSo, p.DonVi, p.GioiHanMin, p.GioiHanMax,
                           r.GiaTri, p.PhuTrach
                     FROM EnvironmentalSamples s
                     JOIN TemplateParameters tp ON s.TemplateID = tp.TemplateID
                     JOIN Parameters p ON tp.ParameterID = p.ParameterID
                     LEFT JOIN Results r ON r.SampleID = s.SampleID AND r.ParameterID = p.ParameterID
                     WHERE s.ContractID = @contractId
                       AND (p.PhuTrach IS NULL OR p.PhuTrach = 'HienTruong')";

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

            if (roundedDataGridView2.Columns["GiaTri"] != null)
                roundedDataGridView2.Columns["GiaTri"].ReadOnly = false;

            if (roundedDataGridView2.Columns["ParameterID"] != null)
                roundedDataGridView2.Columns["ParameterID"].Visible = false;
            if (roundedDataGridView2.Columns["SampleID"] != null)
                roundedDataGridView2.Columns["SampleID"].Visible = false;
            if (roundedDataGridView2.Columns["PhuTrach"] != null)
                roundedDataGridView2.Columns["PhuTrach"].Visible = false;

            if (roundedDataGridView2.Columns["Status"] != null)
            {
                roundedDataGridView2.Columns["Status"].ReadOnly = true;
                roundedDataGridView2.Columns["Status"].HeaderText = rm.GetString("Grid_Status", culture);

                foreach (DataGridViewRow dgRow in roundedDataGridView2.Rows)
                {
                    DataRowView drv = dgRow.DataBoundItem as DataRowView;
                    if (drv != null)
                    {
                        string statusValue = drv["Status"]?.ToString() ?? string.Empty;
                        UpdateStatusCellStyle(dgRow.Index, statusValue);
                    }
                }
            }
            if (roundedDataGridView2.Columns["SampleCode"] != null)
                roundedDataGridView2.Columns["SampleCode"].HeaderText = rm.GetString("Grid_Sample", culture);
            if (roundedDataGridView2.Columns["TenThongSo"] != null)
                roundedDataGridView2.Columns["TenThongSo"].HeaderText = rm.GetString("Grid_ParamName", culture);
            if (roundedDataGridView2.Columns["DonVi"] != null)
                roundedDataGridView2.Columns["DonVi"].HeaderText = rm.GetString("Grid_Unit", culture);
            if (roundedDataGridView2.Columns["GioiHanMin"] != null)
                roundedDataGridView2.Columns["GioiHanMin"].HeaderText = rm.GetString("Grid_Min", culture);
            if (roundedDataGridView2.Columns["GioiHanMax"] != null)
                roundedDataGridView2.Columns["GioiHanMax"].HeaderText = rm.GetString("Grid_Max", culture);
            if (roundedDataGridView2.Columns["GiaTri"] != null)
                roundedDataGridView2.Columns["GiaTri"].HeaderText = rm.GetString("Grid_ValueEntry", culture);
        }


        /// <summary>
        /// Hàm gốc: Tính toán Trạng Thái dựa trên giá trị 'GiaTri' đã có trong DataRow.
        /// </summary>
        private string SetStatusForRow(DataRow row)
        {
            object gObj = row["GiaTri"];
            return SetStatusForRow(row, gObj, false);
        }

        /// <summary>
        /// Hàm quá tải (overload): Tính Trạng Thái dựa trên giá trị MỚI từ ô Cell.
        /// </summary>
        private string SetStatusForRow(DataRow row, object newValueFromCell)
        {
            return SetStatusForRow(row, newValueFromCell, true);
        }

        /// <summary>
        /// HÀM MASTER: Tính toán và cập nhật Trạng Thái, đồng thời cập nhật luôn 'GiaTri' trong DataRow nếu cần.
        /// </summary>
        private string SetStatusForRow(DataRow row, object gObj, bool updateGiaTriColumn)
        {
            object minObj = row["GioiHanMin"];
            object maxObj = row["GioiHanMax"];
            string status;

            if (gObj == DBNull.Value || gObj == null || string.IsNullOrWhiteSpace(gObj.ToString()))
            {
                status = rm.GetString("Status_NotAvailable", culture);
                if (updateGiaTriColumn)
                {
                    row["GiaTri"] = DBNull.Value;
                }
            }
            else if (!TryParseDecimalString(gObj.ToString(), out var g))
            {
                status = rm.GetString("Status_InvalidFormat", culture);

            }
            else
            {
                if (updateGiaTriColumn)
                {
                    row["GiaTri"] = g;
                }

                decimal? min = null, max = null;
                if (minObj != DBNull.Value && minObj != null)
                    min = Convert.ToDecimal(minObj);
                if (maxObj != DBNull.Value && maxObj != null)
                    max = Convert.ToDecimal(maxObj);

                if (min.HasValue && g < min.Value)
                {
                    status = rm.GetString("Status_OutOfRange", culture);
                }
                else if (max.HasValue && g > max.Value)
                {
                    status = rm.GetString("Status_OutOfRange", culture);
                }
                else
                {
                    status = rm.GetString("Status_Passed", culture);
                }
            }

            row["Status"] = status;
            return status;
        }


        /// <summary>
        /// Kích hoạt ngay khi người dùng bắt đầu chỉnh sửa một ô, dùng để commit giá trị ngay lập tức.
        /// </summary>
        private void roundedDataGridView2_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
        {
            if (roundedDataGridView2.IsCurrentCellDirty)
            {
                roundedDataGridView2.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        /// <summary>
        /// Kích hoạt sau khi giá trị ô được commit, dùng để cập nhật Trạng Thái ngay sau khi nhập Giá Trị Đo.
        /// </summary>
        private void roundedDataGridView2_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string colName = roundedDataGridView2.Columns[e.ColumnIndex].Name;
            if (colName != "GiaTri")
            {
                return;
            }

            try
            {
                DataRowView drv = roundedDataGridView2.Rows[e.RowIndex].DataBoundItem as DataRowView;
                if (drv != null)
                {
                    object newValue = roundedDataGridView2.Rows[e.RowIndex].Cells["GiaTri"].Value;

                    string newStatus = SetStatusForRow(drv.Row, newValue);

                    UpdateStatusCellStyle(e.RowIndex, newStatus);
                }
            }
            catch (Exception ex)
            {
                ShowAlert("Lỗi cập nhật trạng thái: " + ex.Message, AlertPanel.AlertType.Error);
            }
        }


        /// <summary>
        /// Tô màu nền (Xanh, Đỏ, Cam) cho ô Trạng Thái dựa trên giá trị của nó.
        /// </summary>
        private void UpdateStatusCellStyle(int rowIndex, string val)
        {
            if (rowIndex < 0 || rowIndex >= roundedDataGridView2.Rows.Count) return;
            var cell = roundedDataGridView2.Rows[rowIndex].Cells["Status"];

            string passed = rm.GetString("Status_Passed", culture);
            string outOfRange = rm.GetString("Status_OutOfRange", culture);
            string invalidFormat = rm.GetString("Status_InvalidFormat", culture);

            if (string.IsNullOrEmpty(val))
            {
                cell.Style.BackColor = Color.Gray;
                cell.Style.ForeColor = Color.White;
                return;
            }

            if (val == passed)
            {
                cell.Style.BackColor = Color.Green;
                cell.Style.ForeColor = Color.White;
            }
            else if (val == outOfRange || val == invalidFormat)
            {
                cell.Style.BackColor = Color.Red;
                cell.Style.ForeColor = Color.White;
            }
            else
            {
                cell.Style.BackColor = Color.Orange;
                cell.Style.ForeColor = Color.White;
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút 'Danh Sách Hợp Đồng' (mở PopUpContract).
        /// </summary>
        private void btnContracts_Click(object sender, EventArgs e)
        {
            try
            {
                string query = @"SELECT ContractID, MaDon, NgayKy, NgayTraKetQua, Status 
                                 FROM Contracts 
                                 WHERE TienTrinh = 2";
                DataTable dt = DataProvider.Instance.ExecuteQuery(query);

                using (PopUpContract popup = new PopUpContract(dt))
                {
                    popup.ContractSelected += (contractId) =>
                    {
                        lbContractID.Text = rm.GetString("Plan_ContractIDLabel", culture) + " " + contractId.ToString();
                        LoadContract(contractId);
                    };

                    popup.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ShowAlert(rm.GetString("Error_LoadContracts", culture) + ": " + ex.Message, AlertPanel.AlertType.Error);
            }
        }

        /// <summary>
        /// Xử lý sự kiện nhấn nút 'Lưu', lưu các giá trị đã nhập vào CSDL.
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            var dt = roundedDataGridView2.DataSource as DataTable;
            if (dt == null || currentContractId == 0)
            {
                ShowAlert(rm.GetString("Real_NoDataToSave", culture), AlertPanel.AlertType.Error);
                return;
            }

            foreach (DataRow row in dt.Rows)
            {
                object gObj = row["GiaTri"];
                if (gObj != DBNull.Value && gObj != null)
                {
                    decimal g = Convert.ToDecimal(gObj);
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
            }

            string query = @"UPDATE Contracts SET TienTrinh = 3 WHERE ContractID = @contractId;";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { this.currentContractId });

            ShowAlert(rm.GetString("Real_SaveSuccess", culture), AlertPanel.AlertType.Success);

            roundedDataGridView2.DataSource = null;
            lbContractID.Text = rm.GetString("Plan_ContractIDLabel", culture);
            this.currentContractId = 0;
        }

        /// <summary>
        /// Xử lý sự kiện nhấn nút 'Hủy', xóa dữ liệu đang hiển thị trên form.
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            roundedDataGridView2.DataSource = null;
            lbContractID.Text = rm.GetString("Plan_ContractIDLabel", culture);
            this.currentContractId = 0;
        }
    }
}