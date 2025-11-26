using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Environmental_Monitoring.Model
{
    [Table("Parameters")] // Đảm bảo mapping đúng bảng
    public class Parameter
    {
        [Key]
        public int ParameterID { get; set; }
        public string TenThongSo { get; set; }
        public string DonVi { get; set; }
        public decimal? GioiHanMin { get; set; }
        public decimal? GioiHanMax { get; set; }
        public string PhuTrach { get; set; }

        // --- CÁC TRƯỜNG MỚI THÊM ---
        public string PhuongPhap { get; set; } // Ví dụ: TCVN 5067:1995
        public string QuyChuan { get; set; }   // Ví dụ: QCVN 05:2013
        public int? ONhiem { get; set; }       // Trạng thái ô nhiễm (nếu cần)
    }
}