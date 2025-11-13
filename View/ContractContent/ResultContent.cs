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
using iText.IO.Font.Constants;
using Emgu.CV.Shape;

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
                                    p.ParameterID, p.TenThongSo, p.DonVi, p.GioiHanMin, p.GioiHanMax,p.ONhiem,
                                    r.GiaTri, r.TrangThaiPheDuyet
                             FROM EnvironmentalSamples s
                             JOIN TemplateParameters tp ON s.TemplateID = tp.TemplateID
                             JOIN Parameters p ON tp.ParameterID = p.ParameterID
                             LEFT JOIN Results r ON r.SampleID = s.SampleID AND r.ParameterID = p.ParameterID
                             WHERE s.ContractID = @contractId
                             ORDER BY s.MaMau, p.TenThongSo";
                DataTable dt = DataProvider.Instance.ExecuteQuery(q, new object[] { contractId });
                this.currentContractId = contractId;
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
                    string res = string.Empty;
                    res = row["ONhiem"].ToString().Equals('1') ? "Ô Nhiễm" : "Không Ô Nhiễm";
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
                if (roundedDataGridView2.Columns["ONhiem"] != null)
                    roundedDataGridView2.Columns["ONhiem"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải kết quả: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPDF_Click(object? sender, EventArgs e)
        {
            if ( currentContractId == 0)
            {
                MessageBox.Show("Vui lòng chọn hợp đồng trước khi xuất PDF.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Title = "Chọn nơi lưu hợp đồng",
                Filter = "PDF files (*.pdf)|*.pdf",
                FileName = "HopDongQuanTrac.pdf"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveDialog.FileName;

                using (PdfWriter writer = new PdfWriter(filePath))
                using (PdfDocument pdf = new PdfDocument(writer))
                using (Document doc = new Document(pdf))
                {
                    string fontPath = Path.Combine(Application.StartupPath, "Resources", "times.ttf");

                    PdfFont font = PdfFontFactory.CreateFont(fontPath, PdfEncodings.IDENTITY_H);
                    string query = @"
                    SELECT *
                    FROM Contracts c
                    JOIN Customers cu ON c.CustomerID = cu.CustomerID
                    WHERE c.ContractID = @ContractID
                    ";

                    DataTable dt = DataProvider.Instance.ExecuteQuery(query, new object[] { currentContractId });

                    // Lấy từng thông tin
                    string nguoiDaiDien = dt.Rows[0]["TenNguoiDaiDien"].ToString();
                    string diaChi = dt.Rows[0]["DiaChi"].ToString();
                    string tenDoanhNghiep = dt.Rows[0]["TenDoanhNghiep"].ToString();
                    string kyHieuDoanhNghiep = dt.Rows[0]["KyHieuDoanhNghiep"].ToString();
                    doc.Add(new Paragraph("HỢP ĐỒNG QUAN TRẮC MÔI TRƯỜNG").SetFont(font).SetFontSize(16).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                    doc.Add(new Paragraph("Doanh Nghiệp: " + tenDoanhNghiep).SetFont(font));
                    doc.Add(new Paragraph("Ký Hiệu Doanh Nghiệp: " + kyHieuDoanhNghiep).SetFont(font));
                    doc.Add(new Paragraph("Đại diện bên A: " + nguoiDaiDien).SetFont(font));
                    doc.Add(new Paragraph("Các Thông Số").SetFont(font).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));

                    // Danh sách cột (có thể đổi theo DataTable của bạn)
                    string[] headers = { "Mã Mẫu", "Thông Số", "Đơn Vị", "Giới Hạn Min", "Giới Hạn Max", "Giá Trị", "Trạng Thái","Kết Quả" };

                    // Tạo bảng có số cột = headers.Length
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

                    string q = @"SELECT s.SampleID, s.MaMau AS SampleCode,
                                    p.ParameterID, p.TenThongSo, p.DonVi, p.GioiHanMin, p.GioiHanMax,p.ONhiem,
                                    r.GiaTri, r.TrangThaiPheDuyet
                             FROM EnvironmentalSamples s
                             JOIN TemplateParameters tp ON s.TemplateID = tp.TemplateID
                             JOIN Parameters p ON tp.ParameterID = p.ParameterID
                             LEFT JOIN Results r ON r.SampleID = s.SampleID AND r.ParameterID = p.ParameterID
                             WHERE s.ContractID = @contractId
                             ORDER BY s.MaMau, p.TenThongSo";
                    DataTable dtInfo = DataProvider.Instance.ExecuteQuery(q, new object[] { this.currentContractId });
                    foreach (DataRow row in dtInfo.Rows)
                    {
                        table.AddCell(new Cell().Add(new Paragraph(row["SampleCode"].ToString()).SetFont(font)));
                        table.AddCell(new Cell().Add(new Paragraph(row["TenThongSo"].ToString()).SetFont(font)));
                        table.AddCell(new Cell().Add(new Paragraph(row["DonVi"].ToString()).SetFont(font)));
                        table.AddCell(new Cell().Add(new Paragraph(row["GioiHanMin"].ToString()).SetFont(font)));
                        table.AddCell(new Cell().Add(new Paragraph(row["GioiHanMax"].ToString()).SetFont(font)));
                        table.AddCell(new Cell().Add(new Paragraph(row["GiaTri"].ToString()).SetFont(font)));
                        table.AddCell(new Cell().Add(new Paragraph(row["TrangThaiPheDuyet"].ToString()).SetFont(font)));
                        table.AddCell(new Cell().Add(new Paragraph(row["ONhiem"].ToString().Equals('1')?"Ô Nhiễm" :"Không Ô Nhiễm" ).SetFont(font)));
                    }
                    doc.Add(table);
                    DateTime now = DateTime.Now;
                    string ngayThang = $"Ngày {now.Day} tháng {now.Month} năm {now.Year}";

                    doc.Add(new Paragraph("\n\n")); // tạo cách dòng

                    // Thêm phần ký tên
                    doc.Add(new Paragraph(ngayThang)
                        .SetFont(font)
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                        .SetMarginRight(40));

                    doc.Add(new Paragraph("ĐẠI DIỆN DOANH NGHIỆP "+ tenDoanhNghiep)
                        .SetFont(font)
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                        .SetMarginTop(30)
                        .SetMarginRight(250));

                    doc.Add(new Paragraph("ĐẠI DIỆN PHỤ TRÁCH")
                        .SetFont(font)
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                        .SetMarginTop(-30)
                        .SetMarginLeft(250));
                }

                MessageBox.Show("Đã lưu hợp đồng", "Thông báo");
            }
            else
            {
                MessageBox.Show("Bạn đã hủy thao tác lưu file", "Thông báo");
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
            if (this.currentContractId == 0)
            {
                MessageBox.Show("Vui lòng chọn hợp đồng trước khi duyệt.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.currentContractId = 0;
            roundedDataGridView2.DataSource = null;
            MessageBox.Show("Đã duyệt hợp đồng", "Thông báo", MessageBoxButtons.OK);
        }

        // designer event placeholders (wired from designer)
        private void btnSearch_Click(object sender, EventArgs e) { }
        private void roundedButton1_Click(object sender, EventArgs e) { }
        private void btnMail_Click(object sender, EventArgs e) { }
        private void btnCancel_Click(object sender, EventArgs e) { }
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
                        lbContractID.Text = "Mã Hợp đồng: " + contractId.ToString();
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
