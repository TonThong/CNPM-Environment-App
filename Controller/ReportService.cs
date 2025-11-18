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
                    string fontPath = Path.Combine(Application.StartupPath, "Resources", "times.ttf");
                    PdfFont font = PdfFontFactory.CreateFont(fontPath, PdfEncodings.IDENTITY_H);

                    // Lấy thông tin Header bằng QueryRepository
                    DataTable dt = DataProvider.Instance.ExecuteQuery(QueryRepository.GetContractHeaderInfo, new object[] { contractId });

                    if (dt.Rows.Count == 0)
                    {
                        throw new Exception(rm.GetString("Error_ContractInfoNotFound", culture));
                    }

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

                    // Dùng gridData được truyền vào
                    foreach (DataRow row in gridData.Rows)
                    {
                        table.AddCell(new Cell().Add(new Paragraph(row["MauKiemNghiem"].ToString()).SetFont(font))); // Thay vì "SampleCode"
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
    }
}
