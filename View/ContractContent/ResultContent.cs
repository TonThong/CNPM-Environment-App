using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Environmental_Monitoring.Controller;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using System.IO;

namespace Environmental_Monitoring.View.ContractContent
{
    public partial class ResultContent : UserControl
    {
        private int currentContractId = 0;

        public ResultContent()
        {
            InitializeComponent();
            roundedDataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            roundedDataGridView2.DefaultCellStyle.ForeColor = Color.Black;

            // wire buttons
            btnPDF.Click += BtnPDF_Click;
            btnMail.Click += BtnMail_Click;
            btnRequest.Click += BtnRequest_Click;
            btnDuyet.Click += BtnDuyet_Click;
        }

        private void BtnContracts_Click(object? sender, EventArgs e)
        {

        }

        private void LoadContract(int contractId)
        {
            try
            {
                // Get samples, parameters and result values
                string q = @"SELECT s.SampleID, s.MaMau AS SampleCode,
                                    p.ParameterID, p.TenThongSo, p.DonVi, p.GioiHanMin, p.GioiHanMax,
                                    r.GiaTri, r.TrangThaiPheDuyet
                             FROM EnvironmentalSamples s
                             JOIN TemplateParameters tp ON s.TemplateID = tp.TemplateID
                             JOIN Parameters p ON tp.ParameterID = p.ParameterID
                             LEFT JOIN Results r ON r.SampleID = s.SampleID AND r.ParameterID = p.ParameterID
                             WHERE s.ContractID = @contractId
                             ORDER BY s.MaMau, p.TenThongSo";

                DataTable dt = DataProvider.Instance.ExecuteQuery(q, new object[] { contractId });
                if (dt == null)
                {
                    roundedDataGridView2.DataSource = null;
                    return;
                }

                // Add display column for result text
                if (!dt.Columns.Contains("KetQua"))
                    dt.Columns.Add("KetQua", typeof(string));

                // Compute KetQua based on GiaTri vs limits
                foreach (DataRow row in dt.Rows)
                {
                    object gObj = row["GiaTri"];
                    object minObj = row["GioiHanMin"];
                    object maxObj = row["GioiHanMax"];

                    string res = string.Empty;
                    if (gObj == DBNull.Value || gObj == null || string.IsNullOrWhiteSpace(gObj.ToString()))
                    {
                        res = "Chưa có";
                    }
                    else if (!decimal.TryParse(gObj.ToString(), out var g))
                    {
                        res = "Sai định dạng";
                    }
                    else
                    {
                        decimal? min = null, max = null;
                        if (minObj != DBNull.Value && minObj != null)
                            min = Convert.ToDecimal(minObj);
                        if (maxObj != DBNull.Value && maxObj != null)
                            max = Convert.ToDecimal(maxObj);

                        if ((min.HasValue && g < min.Value) || (max.HasValue && g > max.Value))
                            res = "Ô Nhiễm";
                        else
                            res = "Không Ô Nhiễm";
                    }

                    row["KetQua"] = res;
                }

                roundedDataGridView2.DataSource = dt;

                // Adjust headers
                if (roundedDataGridView2.Columns["SampleCode"] != null)
                    roundedDataGridView2.Columns["SampleCode"].HeaderText = "Mẫu";
                if (roundedDataGridView2.Columns["TenThongSo"] != null)
                    roundedDataGridView2.Columns["TenThongSo"].HeaderText = "Thông số";
                if (roundedDataGridView2.Columns["DonVi"] != null)
                    roundedDataGridView2.Columns["DonVi"].HeaderText = "Đơn vị đo";
                if (roundedDataGridView2.Columns["GioiHanMin"] != null && roundedDataGridView2.Columns["GioiHanMax"] != null)
                {
                    roundedDataGridView2.Columns["GioiHanMin"].HeaderText = "Giới hạn (Min)";
                    roundedDataGridView2.Columns["GioiHanMax"].HeaderText = "Giới hạn (Max)";
                }
                if (roundedDataGridView2.Columns["GiaTri"] != null)
                    roundedDataGridView2.Columns["GiaTri"].HeaderText = "Giá trị đo";
                if (roundedDataGridView2.Columns["KetQua"] != null)
                {
                    roundedDataGridView2.Columns["KetQua"].HeaderText = "Kết quả";
                    roundedDataGridView2.Columns["KetQua"].ReadOnly = true;
                }

                // Style result column
                if (roundedDataGridView2.Columns["KetQua"] != null)
                {
                    foreach (DataGridViewRow dgRow in roundedDataGridView2.Rows)
                    {
                        var cell = dgRow.Cells["KetQua"];
                        string v = cell.Value?.ToString() ?? string.Empty;
                        if (v == "Ô Nhiễm")
                        {
                            cell.Style.BackColor = Color.Red;
                            cell.Style.ForeColor = Color.White;
                        }
                        else if (v == "Không Ô Nhiễm")
                        {
                            cell.Style.BackColor = Color.Green;
                            cell.Style.ForeColor = Color.White;
                        }
                        else
                        {
                            cell.Style.BackColor = Color.Orange;
                            cell.Style.ForeColor = Color.White;
                        }
                    }
                }

                // Hide internal ids
                if (roundedDataGridView2.Columns["SampleID"] != null)
                    roundedDataGridView2.Columns["SampleID"].Visible = false;
                if (roundedDataGridView2.Columns["ParameterID"] != null)
                    roundedDataGridView2.Columns["ParameterID"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải kết quả: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPDF_Click(object? sender, EventArgs e)
        {
            try
            {
                // Prompt user to select file location
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                    saveFileDialog.Title = "Save PDF File";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveFileDialog.FileName;

                        // Validate file path and directory
                        if (string.IsNullOrWhiteSpace(filePath))
                        {
                            MessageBox.Show("Invalid file path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        string dir = Path.GetDirectoryName(filePath) ?? string.Empty;
                        if (!Directory.Exists(dir))
                        {
                            MessageBox.Show($"Directory does not exist: {dir}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Try to create/overwrite file via FileStream to get clear IO errors
                        try
                        {
                            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                            {
                                // Prepare writer properties and disable smart mode to avoid SmartModePdfObjectsSerializer issues
                                var writerProps = new iText.Kernel.Pdf.WriterProperties();

                                using (PdfWriter writer = new PdfWriter(fs, writerProps))
                                using (PdfDocument pdf = new PdfDocument(writer))
                                using (Document document = new Document(pdf))
                                {
                                    // Add title
                                    document.Add(new Paragraph("Contract Results")
                                        .SetTextAlignment(TextAlignment.CENTER)
                                        .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD))
                                        .SetFontSize(20));

                                    // Add customer information
                                    document.Add(new Paragraph("Customer Information:")
                                        .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD)));
                                    document.Add(new Paragraph("Contract ID: " + currentContractId));

                                    // Add table for data grid view content
                                    Table table = new Table(UnitValue.CreatePercentArray(roundedDataGridView2.Columns.Count))
                                        .UseAllAvailableWidth();

                                    // Add headers
                                    foreach (DataGridViewColumn column in roundedDataGridView2.Columns)
                                    {
                                        table.AddHeaderCell(new Cell().Add(new Paragraph(column.HeaderText)));
                                    }

                                    // Add rows
                                    foreach (DataGridViewRow row in roundedDataGridView2.Rows)
                                    {
                                        foreach (DataGridViewCell cell in row.Cells)
                                        {
                                            string cellValue = cell.Value?.ToString() ?? "";
                                            table.AddCell(new Cell().Add(new Paragraph(cellValue)));
                                        }
                                    }

                                    document.Add(table);
                                }
                            }

                            MessageBox.Show("PDF exported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ioEx)
                        {
                            // Show detailed IO errors which are common root causes for Unknown PdfException
                            MessageBox.Show($"Failed to create/write file. {ioEx.GetType().Name}: {ioEx.Message}\nPath: {filePath}\n\n{ioEx.StackTrace}", "File I/O Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Provide as much detail as possible for troubleshooting
                string inner = ex.InnerException != null ? ex.InnerException.Message : string.Empty;
                MessageBox.Show("Error exporting PDF: " + ex.Message + "\nInner: " + inner + "\n\n" + ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnMail_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Tính năng gửi mail chưa được triển khai.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnRequest_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Tính năng yêu cầu chỉnh sửa chưa được triển khai.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnDuyet_Click(object? sender, EventArgs e)
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

        // designer event placeholders (wired from designer)
        private void btnSearch_Click(object sender, EventArgs e) { }
        private void roundedButton1_Click(object sender, EventArgs e) { }
        private void btnMail_Click(object sender, EventArgs e) { }
        private void btnCancel_Click(object sender, EventArgs e) { }
        private void roundedButton2_Click(object sender, EventArgs e) { }
        private void btnSearch_Click_1(object sender, EventArgs e)
        {

        }

        private void btnContract_Click(object sender, EventArgs e)
        {
            try
            {
                string query = @"SELECT ContractID, MaDon, NgayKy, NgayTraKetQua, Status FROM Contracts";
                DataTable dt = DataProvider.Instance.ExecuteQuery(query);

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
                MessageBox.Show("Lỗi khi lấy danh sách hợp đồng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
