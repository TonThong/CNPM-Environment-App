using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Environmental_Monitoring.Controller;
using Environmental_Monitoring.Controller.Data;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Data;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Windows.Forms;

namespace Environmental_Monitoring.Controller
{
    public class ReportService
    {
        private ResourceManager rm;
        private CultureInfo culture;

        // Mapping từ ký tự subscript Unicode sang số thường
        private static readonly Dictionary<char, char> SubscriptMap = new Dictionary<char, char>
        {
            {'₀', '0'}, {'₁', '1'}, {'₂', '2'}, {'₃', '3'}, {'₄', '4'},
            {'₅', '5'}, {'₆', '6'}, {'₇', '7'}, {'₈', '8'}, {'₉', '9'}
        };

        public ReportService(ResourceManager resourceManager, CultureInfo currentCulture)
        {
            this.rm = resourceManager;
            this.culture = currentCulture;
        }

        /// <summary>
        /// Tạo file PDF báo cáo kết quả từ dữ liệu.
        /// </summary>
        public string GeneratePdfReport(int contractId, DataTable gridData)
        {
            string tempFilePath = Path.Combine(Path.GetTempPath(), $"HopDong_{contractId}_{Path.GetRandomFileName()}.pdf");

            try
            {
                using (PdfWriter writer = new PdfWriter(tempFilePath))
                using (PdfDocument pdf = new PdfDocument(writer))
                using (Document doc = new Document(pdf))
                {
                    // Đường dẫn font từ thư mục Resources
                    string fontPath = Path.Combine(Application.StartupPath, "Resources", "times.ttf");

                    // Kiểm tra file font có tồn tại không
                    PdfFont font;
                    if (File.Exists(fontPath))
                    {
                        font = PdfFontFactory.CreateFont(fontPath, PdfEncodings.IDENTITY_H);
                    }
                    else
                    {
                        font = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.TIMES_ROMAN);
                    }

                    // Lấy thông tin Header
                    DataTable dt = DataProvider.Instance.ExecuteQuery(QueryRepository.GetContractHeaderInfo, new object[] { contractId });

                    if (dt.Rows.Count == 0)
                    {
                        throw new Exception(rm.GetString("Error_ContractInfoNotFound", culture));
                    }

                    string nguoiDaiDien = dt.Rows[0]["TenNguoiDaiDien"].ToString();
                    string tenDoanhNghiep = dt.Rows[0]["TenDoanhNghiep"].ToString();
                    string kyHieuDoanhNghiep = dt.Rows[0]["KyHieuDoanhNghiep"].ToString();

                    // Tiêu đề và thông tin chung
                    doc.Add(new Paragraph(rm.GetString("PDF_Title", culture)).SetFont(font).SetFontSize(16).SetTextAlignment(TextAlignment.CENTER));
                    doc.Add(new Paragraph(rm.GetString("PDF_Company", culture) + ": " + tenDoanhNghiep).SetFont(font));
                    doc.Add(new Paragraph(rm.GetString("PDF_CompanySymbol", culture) + ": " + kyHieuDoanhNghiep).SetFont(font));
                    doc.Add(new Paragraph(rm.GetString("PDF_Representative", culture) + ": " + nguoiDaiDien).SetFont(font));

                    Paragraph title = new Paragraph(rm.GetString("PDF_TableTitle", culture));
                    title.SetFont(font);
                    title.SetTextAlignment(TextAlignment.CENTER);
                    doc.Add(title);

                    // --- TẠO BẢNG VỚI ROWSPAN ---
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

                    float[] columnWidths = { 3, 3, 1, 1, 1, 1, 2, 2 };
                    Table table = new Table(UnitValue.CreatePercentArray(columnWidths)).UseAllAvailableWidth();

                    // Header bảng
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

                    // Xử lý dữ liệu để tính toán Rowspan
                    List<string> processedSampleNames = new List<string>();
                    foreach (DataRow row in gridData.Rows)
                    {
                        string sName = row["MauKiemNghiem"].ToString();
                        int idxTemplate = sName.IndexOf(" - Template");
                        if (idxTemplate > 0) sName = sName.Substring(0, idxTemplate);
                        processedSampleNames.Add(sName);
                    }

                    // Duyệt qua từng dòng dữ liệu
                    for (int i = 0; i < gridData.Rows.Count; i++)
                    {
                        DataRow row = gridData.Rows[i];
                        string currentSampleName = processedSampleNames[i];

                        bool isNewSampleGroup = (i == 0) || (currentSampleName != processedSampleNames[i - 1]);

                        if (isNewSampleGroup)
                        {
                            // Tính toán Rowspan: Đếm xem có bao nhiêu dòng liên tiếp giống nhau kể từ dòng hiện tại
                            int rowspan = 1;
                            for (int j = i + 1; j < gridData.Rows.Count; j++)
                            {
                                if (processedSampleNames[j] == currentSampleName)
                                {
                                    rowspan++;
                                }
                                else
                                {
                                    break; 
                                }
                            }

                            Cell sampleCell = new Cell(rowspan, 1) 
                                .Add(CreateStyledParagraph(currentSampleName, font))
                                .SetVerticalAlignment(VerticalAlignment.MIDDLE) 
                                .SetTextAlignment(TextAlignment.LEFT)
                                .SetPadding(5);

                            table.AddCell(sampleCell);
                        }

                        table.AddCell(new Cell().Add(CreateStyledParagraph(row["TenThongSo"].ToString(), font)).SetVerticalAlignment(VerticalAlignment.MIDDLE));
                        table.AddCell(new Cell().Add(CreateStyledParagraph(row["DonVi"].ToString(), font)).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE));
                        table.AddCell(new Cell().Add(new Paragraph(row["GioiHanMin"].ToString()).SetFont(font)).SetTextAlignment(TextAlignment.RIGHT).SetVerticalAlignment(VerticalAlignment.MIDDLE));
                        table.AddCell(new Cell().Add(new Paragraph(row["GioiHanMax"].ToString()).SetFont(font)).SetTextAlignment(TextAlignment.RIGHT).SetVerticalAlignment(VerticalAlignment.MIDDLE));
                        table.AddCell(new Cell().Add(new Paragraph(row["GiaTri"].ToString()).SetFont(font)).SetTextAlignment(TextAlignment.RIGHT).SetVerticalAlignment(VerticalAlignment.MIDDLE));
                        table.AddCell(new Cell().Add(new Paragraph(row["KetQua"].ToString()).SetFont(font)).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE));
                        table.AddCell(new Cell().Add(new Paragraph(row["TrangThaiHienThi"].ToString()).SetFont(font)).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE));
                    }

                    doc.Add(table);

                    // Ngày tháng và Chữ ký
                    DateTime now = DateTime.Now;
                    string dateString = rm.GetString("PDF_DateFormat", culture);
                    if (string.IsNullOrEmpty(dateString)) dateString = "Day {0} Month {1} Year {2}";
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

        private Paragraph CreateStyledParagraph(string text, PdfFont font)
        {
            Paragraph p = new Paragraph();
            if (string.IsNullOrEmpty(text)) return p;

            StringBuilder buffer = new StringBuilder();

            foreach (char c in text)
            {
                if (SubscriptMap.ContainsKey(c))
                {
                    if (buffer.Length > 0)
                    {
                        p.Add(new Text(buffer.ToString()).SetFont(font));
                        buffer.Clear();
                    }

                    Text subText = new Text(SubscriptMap[c].ToString())
                                    .SetFont(font)
                                    .SetFontSize(8)
                                    .SetTextRise(-3);

                    p.Add(subText);
                }
                else
                {
                    buffer.Append(c);
                }
            }

            if (buffer.Length > 0)
            {
                p.Add(new Text(buffer.ToString()).SetFont(font));
            }

            return p;
        }
    }
}