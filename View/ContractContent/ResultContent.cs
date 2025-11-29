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
using System.Reflection;
using System.Collections.Generic;

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

                        // [CẬP NHẬT] Tìm kiếm theo TenNenMau thay vì MauKiemNghiem
                        if (dt.Columns.Contains("TenNenMau")) filterParts.Add($"TenNenMau LIKE '%{safeKeyword}%'");
                        if (dt.Columns.Contains("TenThongSo")) filterParts.Add($"TenThongSo LIKE '%{safeKeyword}%'");
                        if (dt.Columns.Contains("DonVi")) filterParts.Add($"DonVi LIKE '%{safeKeyword}%'");
                        if (dt.Columns.Contains("GiaTri")) filterParts.Add($"Convert(GiaTri, 'System.String') LIKE '%{safeKeyword}%'");

                        // Bỏ tìm kiếm KetQua vì đã xóa cột này

                        dt.DefaultView.RowFilter = string.Join(" OR ", filterParts);
                    }
                }
                catch (Exception ex) { Console.WriteLine("Search Error: " + ex.Message); }
            }
        }

        private void ShowAlert(string message, AlertPanel.AlertType type)
        {
            var mainLayout = this.FindForm() as Mainlayout;
            if (mainLayout != null) mainLayout.ShowGlobalAlert(message, type);
            else MessageBox.Show(message, type.ToString(), MessageBoxButtons.OK, type == AlertPanel.AlertType.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }

        public void UpdateUIText()
        {
            culture = Thread.CurrentThread.CurrentUICulture;

            if (lbContractID != null)
            {
                if (currentContractId == 0) lbContractID.Text = rm.GetString("Plan_ContractIDLabel", culture);
                // Giữ nguyên text nếu đã load (là tên khách hàng)
            }
            if (btnContract != null) btnContract.Text = rm.GetString("Plan_ContractListButton", culture);
            if (btnDuyet != null) btnDuyet.Text = rm.GetString("Result_Approve", culture);
            if (btnRequest != null) btnRequest.Text = rm.GetString("Result_RequestEdit", culture);
            if (btnPDF != null) btnPDF.Text = rm.GetString("Result_PDF", culture);
            if (btnMail != null) btnMail.Text = rm.GetString("Result_Mail", culture);

            var btnCancel = this.Controls.Find("btnCancel", true).FirstOrDefault();
            if (btnCancel != null) btnCancel.Text = rm.GetString("Button_Cancel", culture);

            // [CẬP NHẬT] Header: Bỏ các cột cũ, thêm TenNenMau
            if (roundedDataGridView2.Columns.Contains("MaDon")) roundedDataGridView2.Columns["MaDon"].HeaderText = rm.GetString("Grid_ContractCode", culture);
            if (roundedDataGridView2.Columns.Contains("NgayKy")) roundedDataGridView2.Columns["NgayKy"].HeaderText = rm.GetString("Grid_SignDate", culture);
            if (roundedDataGridView2.Columns.Contains("NgayTraKetQua")) roundedDataGridView2.Columns["NgayTraKetQua"].HeaderText = rm.GetString("Grid_DueDate", culture);
            if (roundedDataGridView2.Columns.Contains("TenDoanhNghiep")) roundedDataGridView2.Columns["TenDoanhNghiep"].HeaderText = rm.GetString("PDF_Company", culture);
            if (roundedDataGridView2.Columns.Contains("TenNguoiDaiDien")) roundedDataGridView2.Columns["TenNguoiDaiDien"].HeaderText = rm.GetString("PDF_Representative", culture);
            if (roundedDataGridView2.Columns.Contains("TenNhanVien")) roundedDataGridView2.Columns["TenNhanVien"].HeaderText = rm.GetString("Grid_Employee", culture);

            // Thay MauKiemNghiem bằng TenNenMau
            if (roundedDataGridView2.Columns.Contains("TenNenMau")) roundedDataGridView2.Columns["TenNenMau"].HeaderText = "Tên nền mẫu";

            if (roundedDataGridView2.Columns.Contains("TenThongSo")) roundedDataGridView2.Columns["TenThongSo"].HeaderText = rm.GetString("Grid_ParamName", culture);
            if (roundedDataGridView2.Columns.Contains("GioiHanMin")) roundedDataGridView2.Columns["GioiHanMin"].HeaderText = rm.GetString("Grid_Min", culture);
            if (roundedDataGridView2.Columns.Contains("GioiHanMax")) roundedDataGridView2.Columns["GioiHanMax"].HeaderText = rm.GetString("Grid_Max", culture);
            if (roundedDataGridView2.Columns.Contains("DonVi")) roundedDataGridView2.Columns["DonVi"].HeaderText = rm.GetString("Grid_Unit", culture);
            if (roundedDataGridView2.Columns.Contains("GiaTri")) roundedDataGridView2.Columns["GiaTri"].HeaderText = rm.GetString("Grid_Value", culture);
            if (roundedDataGridView2.Columns.Contains("TrangThaiHienThi")) roundedDataGridView2.Columns["TrangThaiHienThi"].HeaderText = rm.GetString("Grid_ApprovalStatus", culture);

            if (currentContractId != 0) LoadContract(currentContractId);
        }

        private void ResultContent_Load(object sender, EventArgs e)
        {
            roundedDataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            roundedDataGridView2.RowHeadersVisible = false;
            roundedDataGridView2.ScrollBars = ScrollBars.Both;
            roundedDataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            roundedDataGridView2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            roundedDataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.None;
            roundedDataGridView2.ReadOnly = true;
            roundedDataGridView2.Scroll += roundedDataGridView2_Scroll;
            roundedDataGridView2.GridColor = Color.Black;

            roundedDataGridView2.CellPainting -= RoundedDataGridView2_CellPainting;
            roundedDataGridView2.CellPainting += RoundedDataGridView2_CellPainting;

            // [CẬP NHẬT] Bỏ đăng ký CellFormatting vì không còn cột Kết quả để tô màu
            roundedDataGridView2.CellFormatting -= RoundedDataGridView2_CellFormatting;

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

        // [CẬP NHẬT] Xóa logic tô màu cột Kết quả (vì đã xóa cột này)
        private void RoundedDataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Không làm gì hoặc xóa hàm này đi
        }

        private void RoundedDataGridView2_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            e.Handled = true;
            e.PaintBackground(e.CellBounds, true);

            // [CẬP NHẬT] Danh sách cột cần gộp ô (Thay MauKiemNghiem bằng TenNenMau)
            string[] mergeColumns = { "MaDon", "NgayKy", "NgayTraKetQua", "TenDoanhNghiep", "TenNguoiDaiDien", "TenNhanVien", "TenNenMau" };
            bool isMergeColumn = mergeColumns.Contains(roundedDataGridView2.Columns[e.ColumnIndex].Name);

            using (Pen gridPen = new Pen(Color.Black, 1))
            {
                if (isMergeColumn)
                {
                    string rawValue = e.Value?.ToString() ?? "";
                    string displayValue = rawValue;

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

                // [CẬP NHẬT] Query SQL: Chọn trực tiếp TenNenMau, bỏ logic tính toán Kết quả/Trạng thái HĐ
                string q = @"SELECT 
                                r.ResultID, r.SampleID, r.ParameterID, r.GiaTri, r.TrangThaiPheDuyet,
                                p.TenThongSo, p.DonVi, p.GioiHanMin, p.GioiHanMax,
                                s.TenNenMau, -- Lấy tên nền mẫu
                                c.MaDon, c.NgayKy, c.NgayTraKetQua, c.Status,
                                cus.TenDoanhNghiep, cus.TenNguoiDaiDien,
                                emp.HoTen AS TenNhanVien
                             FROM Results r
                             JOIN Parameters p ON r.ParameterID = p.ParameterID
                             JOIN EnvironmentalSamples s ON r.SampleID = s.SampleID
                             JOIN Contracts c ON s.ContractID = c.ContractID
                             JOIN Customers cus ON c.CustomerID = cus.CustomerID
                             LEFT JOIN Employees emp ON c.EmployeeID = emp.EmployeeID
                             WHERE s.ContractID = @contractId";

                DataTable dt = DataProvider.Instance.ExecuteQuery(q, new object[] { contractId });

                if (dt == null || dt.Rows.Count == 0) { roundedDataGridView2.DataSource = null; UpdateUIStates(false, false); return; }

                // Chỉ giữ lại cột TrangThaiHienThi
                if (!dt.Columns.Contains("TrangThaiHienThi")) dt.Columns.Add("TrangThaiHienThi", typeof(string));

                string approved = rm.GetString("Grid_Approved", culture);
                string notApproved = rm.GetString("Grid_NotApproved", culture);

                bool tatCaDaDuyet = true;
                foreach (DataRow row in dt.Rows)
                {
                    // Logic duyệt
                    int pheDuyetStatus = 0;
                    if (row["TrangThaiPheDuyet"] != DBNull.Value) int.TryParse(row["TrangThaiPheDuyet"].ToString(), out pheDuyetStatus);

                    if (pheDuyetStatus == 1) row["TrangThaiHienThi"] = approved;
                    else
                    {
                        row["TrangThaiHienThi"] = notApproved;
                        tatCaDaDuyet = false;
                    }
                }

                // Sắp xếp theo Tên nền mẫu -> Thông số
                dt.DefaultView.Sort = "TenNenMau ASC, TenThongSo ASC";
                roundedDataGridView2.DataSource = dt.DefaultView.ToTable();

                isContractApproved = tatCaDaDuyet;
                UpdateUIStates(true, isContractApproved);

                foreach (DataGridViewColumn col in roundedDataGridView2.Columns) col.SortMode = DataGridViewColumnSortMode.NotSortable;

                // [CẬP NHẬT] Các cột ẩn
                string[] hiddenCols = { "ResultID", "SampleID", "ParameterID", "TrangThaiPheDuyet", "Status", "ContractID" };
                foreach (string col in hiddenCols) if (roundedDataGridView2.Columns.Contains(col)) roundedDataGridView2.Columns[col].Visible = false;

                // [CẬP NHẬT] Sắp xếp lại thứ tự cột cho đẹp
                if (roundedDataGridView2.Columns.Contains("TenNenMau")) roundedDataGridView2.Columns["MaDon"].DisplayIndex = 0;
                if (roundedDataGridView2.Columns.Contains("GioiHanMin")) roundedDataGridView2.Columns["TenDoanhNghiep"].DisplayIndex = 1;
                if (roundedDataGridView2.Columns.Contains("GioiHanMax")) roundedDataGridView2.Columns["TenNguoiDaiDien"].DisplayIndex = 2;
                if (roundedDataGridView2.Columns.Contains("TenThongSo")) roundedDataGridView2.Columns["NgayKy"].DisplayIndex = 3;
                if (roundedDataGridView2.Columns.Contains("DonVi")) roundedDataGridView2.Columns["NgayTraKetQua"].DisplayIndex = 4;
                if (roundedDataGridView2.Columns.Contains("GiaTri")) roundedDataGridView2.Columns["TenNhanVien"].DisplayIndex = 5;
                if (roundedDataGridView2.Columns.Contains("TenNenMau")) roundedDataGridView2.Columns["TenNenMau"].DisplayIndex = 6;
                if (roundedDataGridView2.Columns.Contains("TenThongSo")) roundedDataGridView2.Columns["TenThongSo"].DisplayIndex = 7;
                if (roundedDataGridView2.Columns.Contains("DonVi")) roundedDataGridView2.Columns["DonVi"].DisplayIndex = 8;
                if (roundedDataGridView2.Columns.Contains("GioiHanMin")) roundedDataGridView2.Columns["GioiHanMin"].DisplayIndex = 9;
                if (roundedDataGridView2.Columns.Contains("GioiHanMax")) roundedDataGridView2.Columns["GioiHanMax"].DisplayIndex = 10;
                if (roundedDataGridView2.Columns.Contains("GiaTri")) roundedDataGridView2.Columns["GiaTri"].DisplayIndex = 11;
                if (roundedDataGridView2.Columns.Contains("TrangThaiHienThi")) roundedDataGridView2.Columns["TrangThaiHienThi"].DisplayIndex = 12;

                foreach (DataGridViewColumn col in roundedDataGridView2.Columns)
                {
                    if (col.Name == "TenThongSo" || col.Name == "TenNenMau") { col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; col.MinimumWidth = 150; }
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
                        if (lbContractID != null)
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

                string getDateQuery = "SELECT NgayTraKetQua FROM Contracts WHERE ContractID = @id";
                object dateObj = DataProvider.Instance.ExecuteScalar(getDateQuery, new object[] { this.currentContractId });

                string newStatus = "Completed";
                if (dateObj != null && dateObj != DBNull.Value)
                {
                    DateTime ngayTraKetQua = Convert.ToDateTime(dateObj);
                    if (DateTime.Now.Date > ngayTraKetQua.Date) newStatus = "Late Completion";
                }

                string updateStatusQuery = "UPDATE Contracts SET Status = @status, TienTrinh = 5 WHERE ContractID = @id";
                DataProvider.Instance.ExecuteNonQuery(updateStatusQuery, new object[] { newStatus, this.currentContractId });

                int adminRoleID = (int)UserRole.Admin;
                string maDon = DataProvider.Instance.ExecuteScalar(QueryRepository.GetMaDon, new object[] { this.currentContractId }).ToString();
                string noiDungAdmin = $"Hợp đồng '{maDon}' đã được duyệt và hoàn thành ({newStatus}).";
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
                string query = @"SELECT c.ContractID, c.MaDon, c.NgayKy, c.NgayTraKetQua, c.Status, c.IsUnlocked, cus.TenDoanhNghiep 
                 FROM Contracts c 
                 JOIN Customers cus ON c.CustomerID = cus.CustomerID 
                 WHERE c.TienTrinh >= 4";
                DataTable dt = DataProvider.Instance.ExecuteQuery(query);
                using (PopUpContract popup = new PopUpContract(dt))
                {
                    popup.ContractSelected += (contractId) =>
                    {
                        LoadContract(contractId);

                        string cusQuery = @"SELECT cus.TenDoanhNghiep 
                                            FROM Contracts c 
                                            JOIN Customers cus ON c.CustomerID = cus.CustomerID 
                                            WHERE c.ContractID = @cid";
                        object result = DataProvider.Instance.ExecuteScalar(cusQuery, new object[] { contractId });
                        string tenCongTy = result != null ? result.ToString() : "Không xác định";

                        lbContractID.Text = rm.GetString("Plan_ContractIDLabel", culture) + " " + tenCongTy;
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