using Environmental_Monitoring.Controller;
using Environmental_Monitoring.Controller.Data;
using iText.IO.Font;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

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
        /// 
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

                    // Đường dẫn logo (tự động dò nhiều vị trí để tránh thiếu file trong output)
                    string logoFileName = "d39e375b17e2b47022e116931a9df1af13e4f774.png";
                    string[] candidates = new string[] {
                        Path.Combine(Application.StartupPath, "Assets", "icons", logoFileName),
                        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "icons", logoFileName),
                        Path.Combine(Directory.GetCurrentDirectory(), "Assets", "icons", logoFileName),
                        Path.Combine(Environment.CurrentDirectory, "Assets", "icons", logoFileName)
                    };

                    string logoPath = candidates.FirstOrDefault(p => File.Exists(p));

                    // Nếu vẫn chưa tìm thấy, dò trong thư mục chạy (tìm đệ quy trong vài cấp)
                    if (string.IsNullOrEmpty(logoPath))
                    {
                        try
                        {
                            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
                            var found = Directory.GetFiles(baseDir, logoFileName, SearchOption.AllDirectories).FirstOrDefault();
                            if (!string.IsNullOrEmpty(found)) logoPath = found;
                        }
                        catch { /* ignore search errors */ }
                    }

                    // Nếu vẫn chưa có, thử lên các thư mục cha (dùng khi chạy trong IDE)
                    if (string.IsNullOrEmpty(logoPath))
                    {
                        try
                        {
                            var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                            for (int depth = 0; depth < 5 && dir != null; depth++)
                            {
                                var candidate = Path.Combine(dir.FullName, "Assets", "icons", logoFileName);
                                if (File.Exists(candidate)) { logoPath = candidate; break; }
                                dir = dir.Parent;
                            }
                        }
                        catch { }
                    }

                    // Bảng header 2 cột
                    Table headerTable = new Table(UnitValue.CreatePercentArray(new float[] { 30, 70 }))
                        .SetWidth(UnitValue.CreatePercentValue(100));

                    // ===== CỘT LOGO =====
                    if (!string.IsNullOrEmpty(logoPath) && File.Exists(logoPath))
                    {
                        iText.Layout.Element.Image logo = new iText.Layout.Element.Image(ImageDataFactory.Create(logoPath))
                            .ScaleToFit(100, 100);

                        Cell logoCell = new Cell()
                            .Add(logo)
                            .SetBorder(Border.NO_BORDER)
                            .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                            .SetTextAlignment(TextAlignment.CENTER);

                        headerTable.AddCell(logoCell);
                    }
                    else
                    {
                        // Nếu không tìm thấy file, thêm ô rỗng và (tùy chọn) ghi log vào file tạm để debug
                        headerTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
#if DEBUG
                        try { File.WriteAllText(Path.Combine(Path.GetTempPath(), "report_logo_debug.txt"), "Logo not found. Candidates:\r\n" + string.Join("\r\n", candidates) + "\r\nResolved:" + (logoPath ?? "(null)")); } catch { }
#endif
                    }
                    SolidLine solidLine = new SolidLine();
                    solidLine.SetColor(ColorConstants.GREEN);
                    solidLine.SetLineWidth(2f);

                    LineSeparator line = new LineSeparator(solidLine);

                    // ===== CỘT THÔNG TIN DOANH NGHIỆP =====
                    Paragraph companyInfo = new Paragraph()
                        .Add(new Text("Công Ti Cổ Phẩn GreenFlow" + "\n").SetFontSize(13)).SetTextAlignment(TextAlignment.CENTER)
                        .Add("Địa chỉ: " + "Đường Môi Trường, Quận Xanh, TP. Sạch, Tỉnh Đẹp" + "\n").SetTextAlignment(TextAlignment.CENTER)
                        .Add("ĐT: " + "0123456789" + "      ")
                        .Add("Email: " + "noreply@greenflow.com" + "\n")
                        .Add("Giấy chứng nhận hoạt động quan trắc: ANWUXCB 224"); 

                    Cell infoCell = new Cell()
                        .Add(companyInfo.SetFont(font))
                        .SetBorder(Border.NO_BORDER)
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetVerticalAlignment(VerticalAlignment.MIDDLE);

                    headerTable.AddCell(infoCell);

                    // Thêm header vào PDF
                    doc.Add(headerTable);

                    // Lấy thông tin Header
                    DataTable dt = DataProvider.Instance.ExecuteQuery(QueryRepository.GetContractHeaderInfo, new object[] { contractId });

                    if (dt.Rows.Count == 0)
                    {
                        throw new Exception(rm.GetString("Error_ContractInfoNotFound", culture));
                    }

                    string nguoiDaiDien = dt.Rows[0]["TenNguoiDaiDien"].ToString();
                    string tenDoanhNghiep = dt.Rows[0]["TenDoanhNghiep"].ToString();
                    string kyHieuDoanhNghiep = dt.Rows[0]["KyHieuDoanhNghiep"].ToString();
                    string diachi = dt.Rows[0]["DiaChi"].ToString();
                    string ngaytraketqua = dt.Rows[0]["NgayTraKetQua"].ToString();

                    doc.Add(line);

                    // Tiêu đề và thông tin chung
                    doc.Add(new Paragraph(rm.GetString("PDF_Title", culture)).SetFont(font).SetFontSize(16).SetTextAlignment(TextAlignment.CENTER));
                    doc.Add(new Paragraph("I." + rm.GetString("PDF_TITLEI", culture)).SetFont(font).SetFontSize(14));

                    Table infoTable = new Table(UnitValue.CreatePercentArray(new float[] { 30, 70 }))
                        .UseAllAvailableWidth();

                    // Hàm tiện lợi để thêm 1 dòng vào table
                    void AddRow(string label, string value)
                    {
                        infoTable.AddCell(new Cell().Add(new Paragraph(label).SetFont(font)));
                        infoTable.AddCell(new Cell().Add(new Paragraph(value).SetFont(font)));
                    }

                    AddRow(rm.GetString("PDF_Company", culture), tenDoanhNghiep);
                    AddRow(rm.GetString("PDF_CompanySymbol", culture), kyHieuDoanhNghiep);
                    AddRow(rm.GetString("PDF_Address", culture), diachi);
                    AddRow(rm.GetString("PDF_Representative", culture), nguoiDaiDien);
                    AddRow(rm.GetString("PDF_DateReturn", culture), ngaytraketqua);

                    // Thêm table vào document
                    doc.Add(infoTable);

                    Paragraph title = new Paragraph( "II. " + rm.GetString("PDF_TableTitle", culture));
                    title.SetFont(font);
                    title.SetFontSize(14);
                    doc.Add(title);

                    // --- TẠO BẢNG VỚI ROWSPAN ---
                    string[] headers = {
                        rm.GetString("Grid_ParamName", culture),
                        rm.GetString("Grid_Unit", culture),
                        rm.GetString("Grid_Min", culture),
                        rm.GetString("Grid_Max", culture),
                        rm.GetString("Grid_Value", culture),
                        rm.GetString("Grid_ApprovalStatus", culture)
                    };

                    float[] columnWidths = { 3, 3, 1, 1, 1, 1};
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
                        string sName = row["TenNenMau"].ToString();
                        int idxTemplate = sName.IndexOf(" - Template");
                        if (idxTemplate > 0) sName = sName.Substring(0, idxTemplate);
                        processedSampleNames.Add(sName);
                    }

                    // Duyệt qua từng dòng dữ liệu
                    for (int i = 0; i < gridData.Rows.Count; i++)
                    {
                        DataRow row = gridData.Rows[i];

                        // Thêm các cột còn lại của row
                        table.AddCell(new Cell().Add(CreateStyledParagraph(row["TenThongSo"].ToString(), font))
                            .SetVerticalAlignment(VerticalAlignment.MIDDLE));
                        table.AddCell(new Cell().Add(CreateStyledParagraph(row["DonVi"].ToString(), font))
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetVerticalAlignment(VerticalAlignment.MIDDLE));
                        table.AddCell(new Cell().Add(new Paragraph(row["GioiHanMin"].ToString()).SetFont(font))
                            .SetTextAlignment(TextAlignment.RIGHT)
                            .SetVerticalAlignment(VerticalAlignment.MIDDLE));
                        table.AddCell(new Cell().Add(new Paragraph(row["GioiHanMax"].ToString()).SetFont(font))
                            .SetTextAlignment(TextAlignment.RIGHT)
                            .SetVerticalAlignment(VerticalAlignment.MIDDLE));
                        table.AddCell(new Cell().Add(new Paragraph(row["GiaTri"].ToString()).SetFont(font))
                            .SetTextAlignment(TextAlignment.RIGHT)
                            .SetVerticalAlignment(VerticalAlignment.MIDDLE));
                        table.AddCell(new Cell().Add(new Paragraph(row["TrangThaiHienThi"].ToString()).SetFont(font))
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetVerticalAlignment(VerticalAlignment.MIDDLE));
                    }

                    doc.Add(table);

                    Paragraph title_note = new Paragraph("" + "Ghi Chú");
                    title_note.SetFont(font);
                    title_note.SetFontSize(14);
                    title_note.SetUnderline();
                    doc.Add(title_note);

                    List bulletList = new List()
                        .SetListSymbol("   - ")
                        .SetFontSize(11);

                    bulletList.Add(new ListItem("QCVN 05:2013 / BTNMT: Quy chuẩn kỹ thuật quốc gia"));
                    bulletList.Add(new ListItem("(a)QCVN 27:2010 / BTNMT - Quy chuẩn kỹ thuật Quốc gia"));
                    bulletList.Add(new ListItem("(b)QCVN 26:2010 / BTNMT - Quy chuẩn kỹ thuật Quốc gia"));
                    bulletList.Add(new ListItem("(c)QCVN 28:2010 / BTNMT - Quy chuẩn kỹ thuật Quốc gia"));
                    bulletList.Add(new ListItem("(d)QCVN 28:2010 / BTNMT - Quy chuẩn kỹ thuật Quốc gia"));
                    bulletList.Add(new ListItem("(e)QCVN 29:2010 / BTNMT - Quy chuẩn kỹ thuật Quốc gia"));

                    doc.Add(bulletList);

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