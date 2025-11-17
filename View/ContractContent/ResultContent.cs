using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Environmental_Monitoring.Controller;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Font;
using iText.Kernel.Font;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Resources;
using System.Globalization;
using System.Threading;
using Environmental_Monitoring.View.Components;

namespace Environmental_Monitoring.View.ContractContent
{
    public partial class ResultContent : UserControl
    {
        private int currentContractId = 0;
        private bool isContractApproved = false;

        private ResourceManager rm;
        private CultureInfo culture;

        /// <summary>
        /// Hàm khởi tạo (Constructor) cho UserControl.
        /// </summary>
        public ResultContent()
        {
            InitializeComponent();
            InitializeLocalization();
            this.Load += ResultContent_Load;
        }

        /// <summary>
        /// Khởi tạo ResourceManager để tải chuỗi đa ngôn ngữ.
        /// </summary>
        private void InitializeLocalization()
        {
            rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(ResultContent).Assembly);
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
            {
                // Chỉ đặt lại văn bản nếu CHƯA có hợp đồng nào được tải
                if (currentContractId == 0)
                {
                    lbContractID.Text = rm.GetString("Plan_ContractIDLabel", culture);
                }
                // Nếu ĐÃ CÓ hợp đồng, cập nhật văn bản (để hỗ trợ đổi ngôn ngữ)
                else
                {
                    lbContractID.Text = rm.GetString("Plan_ContractIDLabel", culture) + " " + currentContractId.ToString();
                }
            }
            if (btnContract != null)
                btnContract.Text = rm.GetString("Plan_ContractListButton", culture);

            if (btnDuyet != null)
                btnDuyet.Text = rm.GetString("Result_Approve", culture);
            if (btnRequest != null)
                btnRequest.Text = rm.GetString("Result_RequestEdit", culture);
            if (btnPDF != null)
                btnPDF.Text = rm.GetString("Result_PDF", culture);
            if (btnMail != null)
                btnMail.Text = rm.GetString("Result_Mail", culture);

            var btnCancel = this.Controls.Find("btnCancel", true).FirstOrDefault();
            if (btnCancel != null)
            {
                btnCancel.Text = rm.GetString("Button_Cancel", culture);
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
                roundedDataGridView2.Columns["GiaTri"].HeaderText = rm.GetString("Grid_Value", culture);
            if (roundedDataGridView2.Columns.Contains("KetQua"))
                roundedDataGridView2.Columns["KetQua"].HeaderText = rm.GetString("Grid_Result", culture);
            if (roundedDataGridView2.Columns.Contains("TrangThaiHienThi"))
                roundedDataGridView2.Columns["TrangThaiHienThi"].HeaderText = rm.GetString("Grid_ApprovalStatus", culture);
        }

        /// <summary>
        /// Xử lý khi UserControl được tải, gán các sự kiện ban đầu cho các nút.
        /// </summary>
        private void ResultContent_Load(object sender, EventArgs e)
        {
            roundedDataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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

        /// <summary>
        /// Tải dữ liệu của một Hợp đồng cụ thể lên DataGridView.
        /// </summary>
        private void LoadContract(int contractId)
        {
            try
            {
                string q = @"SELECT s.SampleID, s.MaMau AS SampleCode,
                                    p.ParameterID, p.TenThongSo, p.DonVi, p.GioiHanMin, p.GioiHanMax,
                                    p.ONhiem,
                                    r.GiaTri, r.TrangThaiPheDuyet
                                FROM EnvironmentalSamples s
                                JOIN TemplateParameters tp ON s.TemplateID = tp.TemplateID
                                JOIN Parameters p ON tp.ParameterID = p.ParameterID
                                LEFT JOIN Results r ON r.SampleID = s.SampleID AND r.ParameterID = p.ParameterID
                                WHERE s.ContractID = @contractId
                                ORDER BY s.MaMau, p.TenThongSo";

                DataTable dt = DataProvider.Instance.ExecuteQuery(q, new object[] { contractId });
                this.currentContractId = contractId;

                if (dt == null || dt.Rows.Count == 0)
                {
                    roundedDataGridView2.DataSource = null;
                    UpdateUIStates(false, false);
                    return;
                }

                if (!dt.Columns.Contains("KetQua"))
                    dt.Columns.Add("KetQua", typeof(string));
                if (!dt.Columns.Contains("TrangThaiHienThi"))
                    dt.Columns.Add("TrangThaiHienThi", typeof(string));

                bool motMucChuaDuyet = false;

                string polluted = rm.GetString("Grid_Polluted", culture);
                string soonPolluted = rm.GetString("Grid_SoonPolluted", culture);
                string notPolluted = rm.GetString("Grid_NotPolluted", culture);
                string approved = rm.GetString("Grid_Approved", culture);
                string notApproved = rm.GetString("Grid_NotApproved", culture);

                foreach (DataRow row in dt.Rows)
                {
                    int onhiemStatus = 0;
                    if (row["ONhiem"] != DBNull.Value)
                        onhiemStatus = Convert.ToInt32(row["ONhiem"]);

                    switch (onhiemStatus)
                    {
                        case 1: row["KetQua"] = polluted; break;
                        case 2: row["KetQua"] = soonPolluted; break;
                        default: row["KetQua"] = notPolluted; break;
                    }

                    int pheDuyetStatus = 0;
                    if (row["TrangThaiPheDuyet"] != DBNull.Value && row["TrangThaiPheDuyet"] != null)
                    {
                        int.TryParse(row["TrangThaiPheDuyet"].ToString(), out pheDuyetStatus);
                    }

                    if (pheDuyetStatus == 1)
                    {
                        row["TrangThaiHienThi"] = approved;
                    }
                    else
                    {
                        row["TrangThaiHienThi"] = notApproved;
                        motMucChuaDuyet = true;
                    }
                }

                roundedDataGridView2.DataSource = dt;
                isContractApproved = !motMucChuaDuyet;

                UpdateUIText();

                if (roundedDataGridView2.Columns["KetQua"] != null)
                {
                    roundedDataGridView2.Columns["KetQua"].ReadOnly = true;
                    foreach (DataGridViewRow dgRow in roundedDataGridView2.Rows)
                    {
                        var cell = dgRow.Cells["KetQua"];
                        string v = cell.Value?.ToString() ?? string.Empty;

                        if (v == polluted)
                        {
                            cell.Style.BackColor = Color.Red;
                            cell.Style.ForeColor = Color.White;
                        }
                        else if (v == soonPolluted)
                        {
                            cell.Style.BackColor = Color.Orange;
                            cell.Style.ForeColor = Color.White;
                        }
                        else
                        {
                            cell.Style.BackColor = Color.Green;
                            cell.Style.ForeColor = Color.White;
                        }
                    }
                }

                if (roundedDataGridView2.Columns["TrangThaiHienThi"] != null)
                {
                    roundedDataGridView2.Columns["TrangThaiHienThi"].ReadOnly = true;
                }

                if (roundedDataGridView2.Columns["SampleID"] != null)
                    roundedDataGridView2.Columns["SampleID"].Visible = false;
                if (roundedDataGridView2.Columns["ParameterID"] != null)
                    roundedDataGridView2.Columns["ParameterID"].Visible = false;
                if (roundedDataGridView2.Columns["ONhiem"] != null)
                    roundedDataGridView2.Columns["ONhiem"].Visible = false;
                if (roundedDataGridView2.Columns["TrangThaiPheDuyet"] != null)
                    roundedDataGridView2.Columns["TrangThaiPheDuyet"].Visible = false;

                MergeSampleCells();
                UpdateUIStates(true, isContractApproved);
            }
            catch (Exception ex)
            {
                ShowAlert(rm.GetString("Result_LoadError", culture) + ": " + ex.Message, AlertPanel.AlertType.Error);
                UpdateUIStates(false, false);
            }
        }

        /// <summary>
        /// Cập nhật trạng thái (Enabled/Disabled) của các nút bấm.
        /// </summary>
        private void UpdateUIStates(bool contractLoaded, bool isApproved)
        {
            if (!contractLoaded)
            {
                btnDuyet.Enabled = false;
                btnRequest.Enabled = false;
                btnPDF.Enabled = false;
                btnMail.Enabled = false;
                return;
            }

            isContractApproved = isApproved;
            btnDuyet.Enabled = !isApproved;
            btnRequest.Enabled = !isApproved;
            btnPDF.Enabled = isApproved;
            btnMail.Enabled = isApproved;
        }

        /// <summary>
        /// Tạo file PDF báo cáo kết quả từ dữ liệu trên DataGridView.
        /// </summary>
        private string GeneratePdfReport(int contractId)
        {
            string tempFilePath = Path.Combine(Path.GetTempPath(), $"HopDong_{contractId}_{Path.GetRandomFileName()}.pdf");

            try
            {
                using (PdfWriter writer = new PdfWriter(tempFilePath))
                using (PdfDocument pdf = new PdfDocument(writer))
                using (Document doc = new Document(pdf))
                {
                    string fontPath = Path.Combine(Application.StartupPath, "Resources", "times.ttf");
                    PdfFont font = PdfFontFactory.CreateFont(fontPath, PdfEncodings.IDENTITY_H);

                    string query = @"SELECT * FROM Contracts c
                                     JOIN Customers cu ON c.CustomerID = cu.CustomerID
                                     WHERE c.ContractID = @ContractID";
                    DataTable dt = DataProvider.Instance.ExecuteQuery(query, new object[] { contractId });

                    string nguoiDaiDien = dt.Rows[0]["TenNguoiDaiDien"].ToString();
                    string tenDoanhNghiep = dt.Rows[0]["TenDoanhNghiep"].ToString();
                    string kyHieuDoanhNghiep = dt.Rows[0]["KyHieuDoanhNghiep"].ToString();

                    doc.Add(new Paragraph(rm.GetString("PDF_Title", culture)).SetFont(font).SetFontSize(16).SetTextAlignment(TextAlignment.CENTER));
                    doc.Add(new Paragraph(rm.GetString("PDF_Company", culture) + ": " + tenDoanhNghiep).SetFont(font));
                    doc.Add(new Paragraph(rm.GetString("PDF_CompanySymbol", culture) + ": " + kyHieuDoanhNghiep).SetFont(font));
                    doc.Add(new Paragraph(rm.GetString("PDF_Representative", culture) + ": " + nguoiDaiDien).SetFont(font));

                    Paragraph title = new Paragraph(rm.GetString("PDF_TableTitle", culture));
                    title.SetFont(font);
                    title.SetTextAlignment(TextAlignment.CENTER);
                    doc.Add(title);

                    string[] headers = {
                        rm.GetString("Grid_Sample", culture),
                        rm.GetString("Grid_ParamName", culture),
                        rm.GetString("Grid_Unit", culture),
                        rm.GetString("Grid_Min", culture),
                        rm.GetString("Grid_Max", culture),
                        rm.GetString("Grid_Value", culture),
                        rm.GetString("Grid_Result", culture),
                        rm.GetString("Grid_ApprovalStatus", culture)
                    };
                    Table table = new Table(headers.Length).UseAllAvailableWidth();

                    foreach (string header in headers)
                    {
                        table.AddHeaderCell(
                            new Cell().Add(new Paragraph(header).SetFont(font))
                                    .SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY)
                                    .SetTextAlignment(TextAlignment.CENTER)
                                    .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                                    .SetPadding(5)
                        );
                    }

                    DataTable dtInfo = (DataTable)roundedDataGridView2.DataSource;
                    foreach (DataRow row in dtInfo.Rows)
                    {
                        table.AddCell(new Cell().Add(new Paragraph(row["SampleCode"].ToString()).SetFont(font)));
                        table.AddCell(new Cell().Add(new Paragraph(row["TenThongSo"].ToString()).SetFont(font)));
                        table.AddCell(new Cell().Add(new Paragraph(row["DonVi"].ToString()).SetFont(font)));
                        table.AddCell(new Cell().Add(new Paragraph(row["GioiHanMin"].ToString()).SetFont(font)));
                        table.AddCell(new Cell().Add(new Paragraph(row["GioiHanMax"].ToString()).SetFont(font)));
                        table.AddCell(new Cell().Add(new Paragraph(row["GiaTri"].ToString()).SetFont(font)));
                        table.AddCell(new Cell().Add(new Paragraph(row["KetQua"].ToString()).SetFont(font)));
                        table.AddCell(new Cell().Add(new Paragraph(row["TrangThaiHienThi"].ToString()).SetFont(font)));
                    }
                    doc.Add(table);

                    DateTime now = DateTime.Now;
                    string dateString = rm.GetString("PDF_DateFormat", culture);
                    string ngayThang = string.Format(dateString, now.Day, now.Month, now.Year);

                    doc.Add(new Paragraph("\n\n"));
                    doc.Add(new Paragraph(ngayThang)
                        .SetFont(font)
                        .SetTextAlignment(TextAlignment.RIGHT)
                        .SetMarginRight(40));

                    doc.Add(new Paragraph(rm.GetString("PDF_Sign_Company", culture) + " " + tenDoanhNghiep)
                        .SetFont(font)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetMarginTop(30)
                        .SetMarginRight(250));

                    doc.Add(new Paragraph(rm.GetString("PDF_Sign_Manager", culture))
                        .SetFont(font)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetMarginTop(-30)
                        .SetMarginLeft(250));
                }
                return tempFilePath;
            }
            catch (Exception ex)
            {
                throw new Exception(rm.GetString("Error_GeneratePDF", culture) + ": " + ex.Message);
            }
        }

        /// <summary>
        /// Xử lý sự kiện nhấn nút 'PDF', gọi hàm GeneratePdfReport và lưu file.
        /// </summary>
        private void BtnPDF_Click(object? sender, EventArgs e)
        {
            if (currentContractId == 0)
            {
                ShowAlert(rm.GetString("Result_SelectContractBeforePDF", culture), AlertPanel.AlertType.Error);
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Title = rm.GetString("Result_SavePDFTitle", culture),
                Filter = "PDF files (*.pdf)|*.pdf",
                FileName = $"HopDong_{currentContractId}.pdf"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                string savePath = saveDialog.FileName;
                string tempPdfPath = null;
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    tempPdfPath = GeneratePdfReport(currentContractId);
                    File.Copy(tempPdfPath, savePath, true);
                    this.Cursor = Cursors.Default;
                    ShowAlert(rm.GetString("Result_PDFSaveSuccess", culture), AlertPanel.AlertType.Success);
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    ShowAlert(rm.GetString("Error_GeneratePDF", culture) + ": " + ex.Message, AlertPanel.AlertType.Error);
                }
                finally
                {
                    if (!string.IsNullOrEmpty(tempPdfPath) && File.Exists(tempPdfPath))
                    {
                        File.Delete(tempPdfPath);
                    }
                }
            }
        }

        /// <summary>
        /// Xử lý sự kiện nhấn nút 'Mail', tạo PDF và gửi mail (bất đồng bộ).
        /// </summary>
        private async void BtnMail_Click(object? sender, EventArgs e)
        {
            if (currentContractId == 0)
            {
                ShowAlert(rm.GetString("Result_SelectContractBeforeMail", culture), AlertPanel.AlertType.Error);
                return;
            }

            string tempPdfPath = null;
            try
            {
                string query = @"SELECT cu.Email FROM Customers cu
                                 JOIN Contracts c ON c.CustomerID = cu.CustomerID
                                 WHERE c.ContractID = @contractId";
                object emailObj = DataProvider.Instance.ExecuteScalar(query, new object[] { currentContractId });

                if (emailObj == null || emailObj == DBNull.Value || string.IsNullOrWhiteSpace(emailObj.ToString()))
                {
                    ShowAlert(rm.GetString("Result_EmailNotFound", culture), AlertPanel.AlertType.Error);
                    return;
                }
                string customerEmail = emailObj.ToString();

                this.Cursor = Cursors.WaitCursor;
                ShowAlert(rm.GetString("Result_GeneratingPDF", culture), AlertPanel.AlertType.Error);
                tempPdfPath = GeneratePdfReport(currentContractId);

                string maDon = DataProvider.Instance.ExecuteScalar("SELECT MaDon FROM Contracts WHERE ContractID = @contractId", new object[] { currentContractId }).ToString();

                string subject = string.Format(rm.GetString("Mail_Subject", culture), maDon);
                string body = string.Format(rm.GetString("Mail_Body", culture), maDon, currentContractId);

                ShowAlert(string.Format(rm.GetString("Result_SendingMailTo", culture), customerEmail), AlertPanel.AlertType.Error);

                bool success = await EmailService.SendEmailAsync(customerEmail, subject, body, tempPdfPath);

                if (success)
                {
                    ShowAlert(rm.GetString("Result_MailSendSuccess", culture), AlertPanel.AlertType.Success);
                }
            }
            catch (Exception ex)
            {
                ShowAlert(rm.GetString("Error_General", culture) + ": " + ex.Message, AlertPanel.AlertType.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                if (!string.IsNullOrEmpty(tempPdfPath) && File.Exists(tempPdfPath))
                {
                    File.Delete(tempPdfPath);
                }
            }
        }

        /// <summary>
        /// Xử lý sự kiện nhấn nút 'Yêu Cầu Chỉnh Sửa', mở popup và chuyển hợp đồng về bước trước đó.
        /// </summary>
        private void BtnRequest_Click(object? sender, EventArgs e)
        {
            if (this.currentContractId == 0)
            {
                ShowAlert(rm.GetString("Result_SelectContract", culture), AlertPanel.AlertType.Error);
                return;
            }

            using (var form = new Requestforcorrection())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    int newTienTrinh = form.SelectedTienTrinh;
                    if (newTienTrinh == 0) return;

                    try
                    {
                        string resetResultsQuery = @"
                            UPDATE Results r
                            JOIN EnvironmentalSamples es ON r.SampleID = es.SampleID
                            SET r.TrangThaiPheDuyet = NULL 
                            WHERE es.ContractID = @contractId";
                        DataProvider.Instance.ExecuteNonQuery(resetResultsQuery, new object[] { this.currentContractId });

                        string updateContractQuery = "UPDATE Contracts SET TienTrinh = @tienTrinh WHERE ContractID = @contractId";
                        DataProvider.Instance.ExecuteNonQuery(updateContractQuery, new object[] { newTienTrinh, this.currentContractId });

                        int recipientRoleID;
                        if (newTienTrinh == 2) 
                        {
                            recipientRoleID = 10; 
                        }
                        else 
                        {
                            recipientRoleID = 7; 
                        }

                        string maDon = DataProvider.Instance.ExecuteScalar("SELECT MaDon FROM Contracts WHERE ContractID = @id", new object[] { this.currentContractId }).ToString();
                        string noiDung = $"Hợp đồng '{maDon}' yêu cầu bạn chỉnh sửa.";

                        NotificationService.CreateNotification("ChinhSua", noiDung, recipientRoleID, this.currentContractId, null);

                        string noiDungAdmin = $"Hợp đồng '{maDon}' đã được gửi yêu cầu chỉnh sửa cho {form.SelectedPhongBan}.";
                        NotificationService.CreateNotification("ChinhSua", noiDungAdmin, 5, this.currentContractId, null);

                        string successMsg = string.Format(rm.GetString("Result_RequestSuccess", culture), form.SelectedPhongBan);
                        ShowAlert(successMsg, AlertPanel.AlertType.Success);

                        roundedDataGridView2.DataSource = null;
                        lbContractID.Text = rm.GetString("Plan_ContractIDLabel", culture);
                        UpdateUIStates(false, false);
                        this.currentContractId = 0;
                    }
                    catch (Exception ex)
                    {
                        ShowAlert(rm.GetString("Result_RequestError", culture) + ": " + ex.Message, AlertPanel.AlertType.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Xử lý sự kiện nhấn nút 'Duyệt', chốt tất cả kết quả và hoàn thành hợp đồng.
        /// </summary>
        private void BtnDuyet_Click(object? sender, EventArgs e)
        {
            if (this.currentContractId == 0)
            {
                ShowAlert(rm.GetString("Result_SelectContractBeforeApprove", culture), AlertPanel.AlertType.Error);
                return;
            }

            try
            {
                string updateResultsQuery = @"
                    UPDATE Results r
                    JOIN EnvironmentalSamples es ON r.SampleID = es.SampleID
                    SET r.TrangThaiPheDuyet = 1 
                    WHERE es.ContractID = @contractId";
                DataProvider.Instance.ExecuteNonQuery(updateResultsQuery, new object[] { this.currentContractId });

                string updateContractQuery = "UPDATE Contracts SET Status = 'Completed' WHERE ContractID = @contractId";
                DataProvider.Instance.ExecuteNonQuery(updateContractQuery, new object[] { this.currentContractId });

                ShowAlert(rm.GetString("Result_ApproveSuccess", culture), AlertPanel.AlertType.Success);

                LoadContract(this.currentContractId);
            }
            catch (Exception ex)
            {
                ShowAlert(rm.GetString("Result_ApproveError", culture) + ": " + ex.Message, AlertPanel.AlertType.Error);
            }
        }

        /// <summary>
        /// Xử lý sự kiện nhấn nút 'Danh Sách Hợp Đồng' (mở PopUpContract).
        /// </summary>
        private void btnContract_Click(object sender, EventArgs e)
        {
            try
            {
                string query = @"SELECT ContractID, MaDon, NgayKy, NgayTraKetQua, Status FROM Contracts";
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
        /// Gán (hoặc gán lại) các sự kiện Paint và CellFormatting để vẽ các ô được gộp.
        /// </summary>
        private void MergeSampleCells()
        {
            roundedDataGridView2.Paint -= roundedDataGridView2_Paint;
            roundedDataGridView2.CellFormatting -= roundedDataGridView2_CellFormatting;

            roundedDataGridView2.Paint += roundedDataGridView2_Paint;
            roundedDataGridView2.CellFormatting += roundedDataGridView2_CellFormatting;
        }

        /// <summary>
        /// (Logic Hỗ trợ) Tự vẽ lại ô 'SampleCode' (Mã Mẫu) để nó trông như được gộp.
        /// </summary>
        private void roundedDataGridView2_Paint(object sender, PaintEventArgs e)
        {
            if (roundedDataGridView2.Rows.Count == 0) return;

            string colName = "SampleCode";
            if (!roundedDataGridView2.Columns.Contains(colName)) return;

            int colIndex = roundedDataGridView2.Columns[colName].Index;

            Rectangle rect;
            int firstVisibleRow = roundedDataGridView2.FirstDisplayedCell?.RowIndex ?? 0;
            int lastVisibleRow = firstVisibleRow + roundedDataGridView2.DisplayedRowCount(false);
            lastVisibleRow = Math.Min(lastVisibleRow, roundedDataGridView2.RowCount);

            for (int i = firstVisibleRow; i < lastVisibleRow; i++)
            {
                rect = roundedDataGridView2.GetCellDisplayRectangle(colIndex, i, true);

                if (i == 0 || roundedDataGridView2[colIndex, i].Value?.ToString() != roundedDataGridView2[colIndex, i - 1].Value?.ToString())
                {
                }
                else
                {
                    rect.Y -= 1;
                    rect.Height += 1;
                }

                int mergeCount = 0;
                for (int j = i + 1; j < roundedDataGridView2.RowCount; j++)
                {
                    if (roundedDataGridView2[colIndex, i].Value?.ToString() == roundedDataGridView2[colIndex, j].Value?.ToString())
                    {
                        mergeCount++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (mergeCount > 0)
                {
                    for (int k = 1; k <= mergeCount; k++)
                    {
                        if (i + k < roundedDataGridView2.RowCount)
                            rect.Height += roundedDataGridView2.GetCellDisplayRectangle(colIndex, i + k, true).Height;
                    }

                    Color backColor = roundedDataGridView2.Rows[i].Cells[colIndex].Style.BackColor;
                    if (backColor.IsEmpty) backColor = roundedDataGridView2.DefaultCellStyle.BackColor;

                    e.Graphics.FillRectangle(new SolidBrush(backColor), rect);
                    e.Graphics.DrawRectangle(new Pen(roundedDataGridView2.GridColor), rect.X - 1, rect.Y - 1, rect.Width, rect.Height);

                    TextRenderer.DrawText(e.Graphics,
                        roundedDataGridView2[colIndex, i].FormattedValue?.ToString(),
                        roundedDataGridView2.DefaultCellStyle.Font,
                        rect,
                        roundedDataGridView2.DefaultCellStyle.ForeColor,
                        TextFormatFlags.VerticalCenter | TextFormatFlags.Left);

                    i += mergeCount;
                }
            }
        }

        /// <summary>
        /// (Logic Hỗ trợ) Ẩn văn bản ở các ô con (ô đã bị gộp) để chỉ hiển thị văn bản ở ô đầu tiên.
        /// </summary>
        private void roundedDataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == 0 || e.ColumnIndex < 0) return;

            string colName = "SampleCode";
            if (!roundedDataGridView2.Columns.Contains(colName) || e.ColumnIndex != roundedDataGridView2.Columns[colName].Index)
            {
                return;
            }

            try
            {
                var currentValue = e.Value;
                var prevValue = roundedDataGridView2[e.ColumnIndex, e.RowIndex - 1].Value;

                if (currentValue != null && prevValue != null && currentValue.Equals(prevValue))
                {
                    e.Value = "";
                    e.FormattingApplied = true;
                }
            }
            catch { }
        }

        private void btnSearch_Click(object sender, EventArgs e) { }
        private void roundedButton1_Click(object sender, EventArgs e) { }
        private void btnSearch_Click_1(object sender, EventArgs e) { }
    }
}