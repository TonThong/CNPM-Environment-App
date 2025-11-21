using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Environmental_Monitoring.Controller;
using System.IO;
using System.Threading.Tasks;
using System.Resources;
using System.Globalization;
using System.Threading;
using Environmental_Monitoring.View.Components;
using Environmental_Monitoring.Controller.Data;
using Environmental_Monitoring.Controller;
using System.Reflection;
using Environmental_Monitoring.View;

namespace Environmental_Monitoring.View.ContractContent
{
    public partial class ResultContent : UserControl
    {
        private int currentContractId = 0;
        private bool isContractApproved = false;

        private ResourceManager rm;
        private CultureInfo culture;

        public ResultContent()
        {
            InitializeComponent();
            InitializeLocalization();
            this.Load += ResultContent_Load;

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
        }

        private void InitializeLocalization()
        {
            rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(ResultContent).Assembly);
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
                string title;
                MessageBoxIcon icon;
                switch (type)
                {
                    case AlertPanel.AlertType.Success: title = rm.GetString("Alert_SuccessTitle", culture); icon = MessageBoxIcon.Information; break;
                    case AlertPanel.AlertType.Error: title = rm.GetString("Alert_ErrorTitle", culture); icon = MessageBoxIcon.Error; break;
                    default: title = rm.GetString("Alert_InfoTitle", culture) ?? "Information"; icon = MessageBoxIcon.Information; break;
                }
                MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
            }
        }

        public void UpdateUIText()
        {
            culture = Thread.CurrentThread.CurrentUICulture;

            if (lbContractID != null)
            {
                if (currentContractId == 0)
                    lbContractID.Text = rm.GetString("Plan_ContractIDLabel", culture);
                else
                    lbContractID.Text = rm.GetString("Plan_ContractIDLabel", culture) + " " + currentContractId.ToString();
            }
            if (btnContract != null) btnContract.Text = rm.GetString("Plan_ContractListButton", culture);
            if (btnDuyet != null) btnDuyet.Text = rm.GetString("Result_Approve", culture);
            if (btnRequest != null) btnRequest.Text = rm.GetString("Result_RequestEdit", culture);
            if (btnPDF != null) btnPDF.Text = rm.GetString("Result_PDF", culture);
            if (btnMail != null) btnMail.Text = rm.GetString("Result_Mail", culture);

            var btnCancel = this.Controls.Find("btnCancel", true).FirstOrDefault();
            if (btnCancel != null) btnCancel.Text = rm.GetString("Button_Cancel", culture);

            if (roundedDataGridView2.Columns.Contains("MaDon")) roundedDataGridView2.Columns["MaDon"].HeaderText = rm.GetString("Grid_ContractCode", culture);
            if (roundedDataGridView2.Columns.Contains("NgayKy")) roundedDataGridView2.Columns["NgayKy"].HeaderText = rm.GetString("Grid_SignDate", culture);
            if (roundedDataGridView2.Columns.Contains("NgayTraKetQua")) roundedDataGridView2.Columns["NgayTraKetQua"].HeaderText = rm.GetString("Grid_DueDate", culture);
            if (roundedDataGridView2.Columns.Contains("TenDoanhNghiep")) roundedDataGridView2.Columns["TenDoanhNghiep"].HeaderText = rm.GetString("PDF_Company", culture);
            if (roundedDataGridView2.Columns.Contains("TenNguoiDaiDien")) roundedDataGridView2.Columns["TenNguoiDaiDien"].HeaderText = rm.GetString("PDF_Representative", culture);
            if (roundedDataGridView2.Columns.Contains("TenNhanVien")) roundedDataGridView2.Columns["TenNhanVien"].HeaderText = rm.GetString("Grid_Employee", culture);
            if (roundedDataGridView2.Columns.Contains("MauKiemNghiem")) roundedDataGridView2.Columns["MauKiemNghiem"].HeaderText = rm.GetString("Grid_Sample", culture);
            if (roundedDataGridView2.Columns.Contains("TenThongSo")) roundedDataGridView2.Columns["TenThongSo"].HeaderText = rm.GetString("Grid_ParamName", culture);
            if (roundedDataGridView2.Columns.Contains("GioiHanMin")) roundedDataGridView2.Columns["GioiHanMin"].HeaderText = rm.GetString("Grid_Min", culture);
            if (roundedDataGridView2.Columns.Contains("GioiHanMax")) roundedDataGridView2.Columns["GioiHanMax"].HeaderText = rm.GetString("Grid_Max", culture);
            if (roundedDataGridView2.Columns.Contains("DonVi")) roundedDataGridView2.Columns["DonVi"].HeaderText = rm.GetString("Grid_Unit", culture);
            if (roundedDataGridView2.Columns.Contains("GiaTri")) roundedDataGridView2.Columns["GiaTri"].HeaderText = rm.GetString("Grid_Value", culture);
            if (roundedDataGridView2.Columns.Contains("KetQua")) roundedDataGridView2.Columns["KetQua"].HeaderText = rm.GetString("Grid_Result", culture);
            if (roundedDataGridView2.Columns.Contains("TrangThaiHienThi")) roundedDataGridView2.Columns["TrangThaiHienThi"].HeaderText = rm.GetString("Grid_ApprovalStatus", culture);
            if (roundedDataGridView2.Columns.Contains("TrangThaiHopDong")) roundedDataGridView2.Columns["TrangThaiHopDong"].HeaderText = rm.GetString("Grid_ContractStatus", culture);

            if (currentContractId != 0)
            {
                LoadContract(currentContractId);
            }
        }

        private void ResultContent_Load(object sender, EventArgs e)
        {
            roundedDataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            roundedDataGridView2.RowHeadersVisible = false;
            roundedDataGridView2.ScrollBars = ScrollBars.Both;

            roundedDataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.None;
            roundedDataGridView2.ReadOnly = true;
            roundedDataGridView2.Scroll += roundedDataGridView2_Scroll;
            roundedDataGridView2.GridColor = Color.Black;

            roundedDataGridView2.CellPainting -= RoundedDataGridView2_CellPainting;
            roundedDataGridView2.CellPainting += RoundedDataGridView2_CellPainting;

            try { Type dgvType = roundedDataGridView2.GetType(); PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic); if (pi != null) pi.SetValue(roundedDataGridView2, true, null); } catch { }

            roundedDataGridView2.DefaultCellStyle.ForeColor = Color.Black;
            roundedDataGridView2.AllowUserToAddRows = false;

            btnPDF.Click += BtnPDF_Click;
            btnMail.Click += BtnMail_Click;
            btnRequest.Click += BtnRequest_Click;
            btnDuyet.Click += BtnDuyet_Click;
            btnContract.Click -= btnContract_Click;
            btnContract.Click += btnContract_Click;

            UpdateUIStates(false, false);
            UpdateUIText();
        }

        // Đã xóa hàm NormalizeSubscripts để giữ nguyên ký tự subscript Unicode (₂, ₃...)
        // Lưu ý: Để file PDF hiển thị đúng, ReportService phải dùng Font hỗ trợ Unicode (như Arial Unicode MS, Roboto, NotoSans...)

        private void RoundedDataGridView2_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            e.Handled = true;
            e.PaintBackground(e.CellBounds, true);

            string[] mergeColumns = { "MaDon", "NgayKy", "NgayTraKetQua", "TenDoanhNghiep", "TenNguoiDaiDien", "TenNhanVien", "MauKiemNghiem", "TrangThaiHopDong" };
            bool isMergeColumn = mergeColumns.Contains(roundedDataGridView2.Columns[e.ColumnIndex].Name);

            using (Pen gridPen = new Pen(Color.Black, 1))
            {
                if (isMergeColumn)
                {
                    string rawValue = e.Value?.ToString() ?? "";
                    string displayValue = rawValue;

                    // Logic cắt chuỗi đã được thực hiện ở LoadContract

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

                    TextRenderer.DrawText(e.Graphics, displayValue, e.CellStyle.Font, groupRect, e.CellStyle.ForeColor, flags);

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

        private void LoadContract(int contractId)
        {
            try
            {
                this.currentContractId = contractId;

                string q = QueryRepository.LoadContractResults;
                DataTable dt = DataProvider.Instance.ExecuteQuery(q, new object[] { contractId });

                if (dt == null || dt.Rows.Count == 0) { roundedDataGridView2.DataSource = null; UpdateUIStates(false, false); return; }

                if (!dt.Columns.Contains("KetQua")) dt.Columns.Add("KetQua", typeof(string));
                if (!dt.Columns.Contains("TrangThaiHienThi")) dt.Columns.Add("TrangThaiHienThi", typeof(string));
                if (!dt.Columns.Contains("TrangThaiHopDong")) dt.Columns.Add("TrangThaiHopDong", typeof(string));

                string polluted = rm.GetString("Grid_Polluted", culture);
                string soonPolluted = rm.GetString("Grid_SoonPolluted", culture);
                string notPolluted = rm.GetString("Grid_NotPolluted", culture);
                string approved = rm.GetString("Grid_Approved", culture);
                string notApproved = rm.GetString("Grid_NotApproved", culture);
                string status_Completed = rm.GetString("Status_Completed", culture);
                string status_Expired = rm.GetString("Status_Expired", culture);
                string status_Overdue = rm.GetString("Status_Overdue", culture);
                string status_Active = rm.GetString("Status_Active", culture);

                bool tatCaDaDuyet = true;
                foreach (DataRow row in dt.Rows)
                {
                    // --- XỬ LÝ TÊN MẪU (Sample Name) ---
                    // Cắt bỏ phần " - Template..."
                    string rawSample = row["MauKiemNghiem"] != DBNull.Value ? row["MauKiemNghiem"].ToString() : "";
                    int idx = rawSample.IndexOf(" - Template");
                    if (idx > 0)
                    {
                        row["MauKiemNghiem"] = rawSample.Substring(0, idx);
                    }

                    // --- GIỮ NGUYÊN CÔNG THỨC HÓA HỌC ---
                    // Không normalize nữa để giữ subscript (NO₂, O₂...)
                    // Yêu cầu: ReportService phải dùng font hỗ trợ Unicode.

                    // --- Xử lý trạng thái ---
                    int onhiemStatus = (row["ONhiem"] != DBNull.Value) ? Convert.ToInt32(row["ONhiem"]) : 0;
                    switch (onhiemStatus)
                    {
                        case 1: row["KetQua"] = polluted; break;
                        case 2: row["KetQua"] = soonPolluted; break;
                        default: row["KetQua"] = notPolluted; break;
                    }

                    int pheDuyetStatus = 0;
                    if (row["TrangThaiPheDuyet"] != DBNull.Value) int.TryParse(row["TrangThaiPheDuyet"].ToString(), out pheDuyetStatus);

                    if (pheDuyetStatus == 1)
                    {
                        row["TrangThaiHienThi"] = approved;
                    }
                    else
                    {
                        row["TrangThaiHienThi"] = notApproved;
                        tatCaDaDuyet = false;
                    }

                    string statusHopDongDB = row["Status"].ToString();
                    DateTime ngayTra = Convert.ToDateTime(row["NgayTraKetQua"]);
                    if (statusHopDongDB == "Completed") row["TrangThaiHopDong"] = status_Completed;
                    else if (statusHopDongDB == "Expired") row["TrangThaiHopDong"] = status_Expired;
                    else if (DateTime.Now > ngayTra) row["TrangThaiHopDong"] = status_Overdue;
                    else row["TrangThaiHopDong"] = status_Active;
                }

                dt.DefaultView.Sort = "MauKiemNghiem ASC, TenThongSo ASC";
                roundedDataGridView2.DataSource = dt.DefaultView.ToTable();

                isContractApproved = tatCaDaDuyet;

                UpdateUIStates(true, isContractApproved);

                foreach (DataGridViewColumn col in roundedDataGridView2.Columns) col.SortMode = DataGridViewColumnSortMode.NotSortable;

                string[] noHighlightCols = { "MaDon", "NgayKy", "NgayTraKetQua", "TenDoanhNghiep", "TenNguoiDaiDien", "TenNhanVien", "MauKiemNghiem" };
                foreach (string colName in noHighlightCols)
                {
                    if (roundedDataGridView2.Columns.Contains(colName))
                    {
                        roundedDataGridView2.Columns[colName].DefaultCellStyle.SelectionBackColor = Color.White;
                        roundedDataGridView2.Columns[colName].DefaultCellStyle.SelectionForeColor = Color.Black;
                    }
                }

                if (roundedDataGridView2.Columns["KetQua"] != null)
                {
                    roundedDataGridView2.Columns["KetQua"].ReadOnly = true;
                    foreach (DataGridViewRow dgRow in roundedDataGridView2.Rows)
                    {
                        var cell = dgRow.Cells["KetQua"];
                        string v = cell.Value?.ToString() ?? string.Empty;
                        if (v == polluted) { cell.Style.BackColor = Color.Red; cell.Style.ForeColor = Color.White; }
                        else if (v == soonPolluted) { cell.Style.BackColor = Color.Orange; cell.Style.ForeColor = Color.White; }
                        else { cell.Style.BackColor = Color.Green; cell.Style.ForeColor = Color.White; }
                    }
                }

                string[] hiddenCols = { "SampleID", "ParameterID", "ONhiem", "TrangThaiPheDuyet", "Status" };
                foreach (string col in hiddenCols) if (roundedDataGridView2.Columns.Contains(col)) roundedDataGridView2.Columns[col].Visible = false;

                foreach (DataGridViewColumn col in roundedDataGridView2.Columns)
                {
                    if (col.Name == "TenThongSo" || col.Name == "MauKiemNghiem") { col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; col.MinimumWidth = 150; }
                    else if (col.Visible) col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                }
            }
            catch (Exception ex) { ShowAlert(rm.GetString("Result_LoadError", culture) + ": " + ex.Message, AlertPanel.AlertType.Error); UpdateUIStates(false, false); }
        }

        private void roundedDataGridView2_Scroll(object sender, ScrollEventArgs e) { if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll) roundedDataGridView2.Invalidate(); }

        private void UpdateUIStates(bool contractLoaded, bool isApproved)
        {
            bool enabled = contractLoaded;

            btnDuyet.Enabled = enabled && !isApproved;
            btnRequest.Enabled = enabled && !isApproved;

            btnPDF.Enabled = enabled && isApproved;
            btnMail.Enabled = enabled && isApproved;

            btnDuyet.BackColor = btnDuyet.Enabled ? Color.Green : Color.Gray;
            btnRequest.BackColor = btnRequest.Enabled ? Color.FromArgb(119, 136, 153) : Color.Gray;
            btnPDF.BackColor = btnPDF.Enabled ? Color.FromArgb(220, 53, 69) : Color.Gray;
            btnMail.BackColor = btnMail.Enabled ? Color.FromArgb(0, 123, 255) : Color.Gray;
        }

        private void BtnPDF_Click(object? sender, EventArgs e)
        {
            if (currentContractId == 0) { ShowAlert(rm.GetString("Result_SelectContractBeforePDF", culture), AlertPanel.AlertType.Error); return; }
            SaveFileDialog saveDialog = new SaveFileDialog { Title = rm.GetString("Result_SavePDFTitle", culture), Filter = "PDF files (*.pdf)|*.pdf", FileName = $"HopDong_{currentContractId}.pdf" };
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                string savePath = saveDialog.FileName; string tempPdfPath = null;
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    var reportService = new ReportService(rm, culture);
                    DataTable gridData = (DataTable)roundedDataGridView2.DataSource;
                    tempPdfPath = reportService.GeneratePdfReport(currentContractId, gridData);
                    File.Copy(tempPdfPath, savePath, true);
                    this.Cursor = Cursors.Default;
                    ShowAlert(rm.GetString("Result_PDFSaveSuccess", culture), AlertPanel.AlertType.Success);
                }
                catch (Exception ex) { this.Cursor = Cursors.Default; ShowAlert(rm.GetString("Error_GeneratePDF", culture) + ": " + ex.Message, AlertPanel.AlertType.Error); }
                finally { if (!string.IsNullOrEmpty(tempPdfPath) && File.Exists(tempPdfPath)) File.Delete(tempPdfPath); }
            }
        }

        private async void BtnMail_Click(object? sender, EventArgs e)
        {
            if (currentContractId == 0) { ShowAlert(rm.GetString("Result_SelectContractBeforeMail", culture), AlertPanel.AlertType.Error); return; }
            string tempPdfPath = null;
            try
            {
                object emailObj = DataProvider.Instance.ExecuteScalar(QueryRepository.GetCustomerEmail, new object[] { currentContractId });
                if (emailObj == null || emailObj == DBNull.Value) { ShowAlert(rm.GetString("Result_EmailNotFound", culture), AlertPanel.AlertType.Error); return; }
                string customerEmail = emailObj.ToString();
                this.Cursor = Cursors.WaitCursor;
                ShowAlert(rm.GetString("Result_GeneratingPDF", culture), AlertPanel.AlertType.Error);
                var reportService = new ReportService(rm, culture);
                DataTable gridData = (DataTable)roundedDataGridView2.DataSource;
                tempPdfPath = await Task.Run(() => reportService.GeneratePdfReport(currentContractId, gridData));
                string maDon = DataProvider.Instance.ExecuteScalar(QueryRepository.GetMaDon, new object[] { currentContractId }).ToString();
                string subject = string.Format(rm.GetString("Mail_Subject", culture), maDon);
                string body = string.Format(rm.GetString("Mail_Body", culture), maDon, currentContractId);
                ShowAlert(string.Format(rm.GetString("Result_SendingMailTo", culture), customerEmail), AlertPanel.AlertType.Error);
                bool success = await EmailService.SendEmailAsync(customerEmail, subject, body, tempPdfPath);
                if (success) ShowAlert(rm.GetString("Result_MailSendSuccess", culture), AlertPanel.AlertType.Success);
            }
            catch (Exception ex) { ShowAlert(rm.GetString("Error_General", culture) + ": " + ex.Message, AlertPanel.AlertType.Error); }
            finally { this.Cursor = Cursors.Default; if (!string.IsNullOrEmpty(tempPdfPath) && File.Exists(tempPdfPath)) File.Delete(tempPdfPath); }
        }

        private void BtnRequest_Click(object? sender, EventArgs e)
        {
            if (this.currentContractId == 0) { ShowAlert(rm.GetString("Result_SelectContract", culture), AlertPanel.AlertType.Error); return; }
            using (var form = new Requestforcorrection())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    int newTienTrinh = form.SelectedTienTrinh; if (newTienTrinh == 0) return;
                    try
                    {
                        DataProvider.Instance.ExecuteNonQuery(QueryRepository.ResetResultsOnRequest, new object[] { this.currentContractId });
                        DataProvider.Instance.ExecuteNonQuery(QueryRepository.UpdateContractTienTrinh, new object[] { newTienTrinh, this.currentContractId });
                        int recipientRoleID = (newTienTrinh == (int)ContractProcess.Sampling) ? (int)UserRole.Planning : (int)UserRole.Lab;
                        string maDon = DataProvider.Instance.ExecuteScalar(QueryRepository.GetMaDon, new object[] { this.currentContractId }).ToString();
                        string noiDung = $"Hợp đồng '{maDon}' yêu cầu bạn chỉnh sửa.";
                        NotificationService.CreateNotification("ChinhSua", noiDung, recipientRoleID, this.currentContractId, null);
                        string noiDungAdmin = $"Hợp đồng '{maDon}' đã được gửi yêu cầu chỉnh sửa cho {form.SelectedPhongBan}.";
                        NotificationService.CreateNotification("ChinhSua", noiDungAdmin, (int)UserRole.Admin, this.currentContractId, null);
                        string successMsg = string.Format(rm.GetString("Result_RequestSuccess", culture), form.SelectedPhongBan);
                        ShowAlert(successMsg, AlertPanel.AlertType.Success);

                        roundedDataGridView2.DataSource = null;
                        lbContractID.Text = rm.GetString("Plan_ContractIDLabel", culture);
                        UpdateUIStates(false, false);
                        this.currentContractId = 0;
                    }
                    catch (Exception ex) { ShowAlert(rm.GetString("Result_RequestError", culture) + ": " + ex.Message, AlertPanel.AlertType.Error); }
                }
            }
        }

        private void BtnDuyet_Click(object? sender, EventArgs e)
        {
            if (this.currentContractId == 0) { ShowAlert(rm.GetString("Result_SelectContractBeforeApprove", culture), AlertPanel.AlertType.Error); return; }
            try
            {
                DataProvider.Instance.ExecuteNonQuery(QueryRepository.ApproveAllResults, new object[] { this.currentContractId });

                DataProvider.Instance.ExecuteNonQuery(QueryRepository.CompleteContractStatus, new object[] { this.currentContractId });

                int adminRoleID = (int)UserRole.Admin;
                string maDon = DataProvider.Instance.ExecuteScalar(QueryRepository.GetMaDon, new object[] { this.currentContractId }).ToString();
                string noiDungAdmin = $"Hợp đồng '{maDon}' đã được duyệt và hoàn thành.";
                NotificationService.CreateNotification("ChinhSua", noiDungAdmin, adminRoleID, this.currentContractId, null);

                ShowAlert(rm.GetString("Result_ApproveSuccess", culture), AlertPanel.AlertType.Success);

                LoadContract(this.currentContractId);
            }
            catch (Exception ex) { ShowAlert(rm.GetString("Result_ApproveError", culture) + ": " + ex.Message, AlertPanel.AlertType.Error); }
        }

        private void btnContract_Click(object sender, EventArgs e)
        {
            try
            {
                string query = @"SELECT ContractID, MaDon, NgayKy, NgayTraKetQua, Status FROM Contracts WHERE TienTrinh >= 4";
                DataTable dt = DataProvider.Instance.ExecuteQuery(query);
                using (PopUpContract popup = new PopUpContract(dt))
                {
                    popup.ContractSelected += (contractId) => {
                        LoadContract(contractId);
                        UpdateUIText();
                    };
                    popup.ShowDialog();
                }
            }
            catch (Exception ex) { ShowAlert(rm.GetString("Error_LoadContracts", culture) + ": " + ex.Message, AlertPanel.AlertType.Error); }
        }

        private void btnSearch_Click(object sender, EventArgs e) { }
        private void roundedButton1_Click(object sender, EventArgs e) { }
        private void btnSearch_Click_1(object sender, EventArgs e) { }
    }
}