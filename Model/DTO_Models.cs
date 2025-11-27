using System.Collections.Generic;

namespace Environmental_Monitoring.Model
{
    // DTO: Data Transfer Object - Dùng để chuyển dữ liệu giữa các Form
    public class ParameterDTO
    {
        public int ParameterID { get; set; }
        public string TenThongSo { get; set; }
        public string DonVi { get; set; }
        public decimal? Min { get; set; }
        public decimal? Max { get; set; }
        public string PhuongPhap { get; set; }
        public string QuyChuan { get; set; }
        public string PhuTrach { get; set; }

        // Property hỗ trợ hiển thị trên GridView
        public string HienThiMinMax
        {
            get
            {
                string minStr = Min.HasValue ? Min.Value.ToString("0.##") : "_";
                string maxStr = Max.HasValue ? Max.Value.ToString("0.##") : "_";
                return $"{minStr} - {maxStr}";
            }
        }
    }

    public class SampleDTO
    {
        public int BaseTemplateID { get; set; } // ID của mẫu gốc (VD: Không khí)
        public string TenNenMau { get; set; }   // Tên hiển thị (VD: Môi trường không khí)
        public string KyHieuMau { get; set; }   // VD: KXQ.01
        public string ViTri { get; set; }       // VD: Cổng ra vào
        public string ToaDoX { get; set; }
        public string ToaDoY { get; set; }

        // Danh sách thông số của mẫu này
        public List<ParameterDTO> Parameters { get; set; } = new List<ParameterDTO>();
    }
}